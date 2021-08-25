using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Runtime.Serialization;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using NLog;
using PoissonSoft.KuCoinApi.Contracts.Exceptions;
using PoissonSoft.KuCoinApi.Contracts.MarketData.Request;
using PoissonSoft.KuCoinApi.Contracts.Serialization;
using PoissonSoft.KuCoinApi.Trade;
using PoissonSoft.KuCoinApi.Utils;

namespace PoissonSoft.KuCoinApi.Transport.Rest
{
    internal sealed class RestClient
    {
        private readonly ILogger logger;
        private readonly Throttler throttler;
        private readonly string userFriendlyName;

        private readonly HttpClient httpClient;
        private readonly bool useSignature;
        private readonly byte[] secretKey;
        private readonly KuCoinApiClientCredentials Credentials;

        private readonly JsonSerializerSettings serializerSettings;


        public RestClient(ILogger logger, string baseEndpoint, EndpointSecurityType[] securityTypes, KuCoinApiClientCredentials credentials)//, Throttler throttler)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.throttler = throttler ?? throw new ArgumentNullException(nameof(throttler));

            var useApiKey = securityTypes?.Any(x => x != EndpointSecurityType.None) ?? false;
            useSignature = securityTypes?.Any(x =>
                x == EndpointSecurityType.Trade || x == EndpointSecurityType.Margin ||
                x == EndpointSecurityType.UserData) ?? false;
            secretKey = Encoding.UTF8.GetBytes(credentials.SecretKey);
            Credentials = credentials;
            userFriendlyName = $"{nameof(RestClient)} ({baseEndpoint})";

            serializerSettings = new JsonSerializerSettings
            {
                Context = new StreamingContext(StreamingContextStates.All,
                    new SerializationContext { Logger = logger })
            };

            var httpClientHandler = new HttpClientHandler
            {
                AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip,
                Proxy = ProxyHelper.CreateProxy(credentials)
            };

            baseEndpoint = baseEndpoint.Trim();
            if (!baseEndpoint.EndsWith("/")) baseEndpoint += '/';
            httpClient = new HttpClient(httpClientHandler, true)
            {
                Timeout = TimeSpan.FromSeconds(20),
                BaseAddress = new Uri(baseEndpoint),
            };
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            if (useApiKey)
            {
                //httpClient.DefaultRequestHeaders.Add("KC-API-KEY", credentials.ApiKey);
            }


        }

        public RestClient(ILogger logger, string baseEndpoint, EndpointSecurityType[] securityTypes, KuCoinApiClientCredentials credentials, Throttler throttler)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.throttler = throttler ?? throw new ArgumentNullException(nameof(throttler));

            var useApiKey = securityTypes?.Any(x => x != EndpointSecurityType.None) ?? false;
            useSignature = securityTypes?.Any(x =>
                x == EndpointSecurityType.Trade || x == EndpointSecurityType.Margin ||
                x == EndpointSecurityType.UserData) ?? false;
            secretKey = Encoding.UTF8.GetBytes(credentials.SecretKey);
            Credentials = credentials;
            userFriendlyName = $"{nameof(RestClient)} ({baseEndpoint})";

            serializerSettings = new JsonSerializerSettings
            {
                Context = new StreamingContext(StreamingContextStates.All,
                    new SerializationContext { Logger = logger })
            };

            var httpClientHandler = new HttpClientHandler
            {
                AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip,
                Proxy = ProxyHelper.CreateProxy(credentials)
            };

            baseEndpoint = baseEndpoint.Trim();
            if (!baseEndpoint.EndsWith("/")) baseEndpoint += '/';
            httpClient = new HttpClient(httpClientHandler, true)
            {
                Timeout = TimeSpan.FromSeconds(20),
                BaseAddress = new Uri(baseEndpoint),
            };
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            if (useApiKey)
            {
                //httpClient.DefaultRequestHeaders.Add("KC-API-KEY", credentials.ApiKey);
            }

            
        }

        /// <summary>
        /// Выполнить запрос
        /// </summary>
        /// <typeparam name="TResp">Тип возвращаемого значения</typeparam>
        /// <returns></returns>
        public TResp MakeRequest<TResp>(RequestParameters requestParameters)
        {
            if (requestParameters.RequestWeight > 0)
                throttler.ThrottleRest(requestParameters.RequestWeight, requestParameters.IsHighPriority, requestParameters.IsOrderRequest);


            void checkResponse(HttpResponseMessage resp, string body)
            {
                throttler.ApplyRestResponseHeaders(resp.Headers);

                if (resp.StatusCode == HttpStatusCode.OK) return;

                string msg;

                // HTTP 429 return code is used when breaking a request rate limit.
                // HTTP 418 return code is used when an IP has been auto-banned for continuing to send requests after receiving 429 codes.
                // A Retry-After header is sent with a 418 or 429 responses and will give the number of seconds required to wait, in the case of a 429,
                // to prevent a ban, or, in the case of a 418, until the ban is over.
                if (resp.StatusCode == (HttpStatusCode)429 || resp.StatusCode == (HttpStatusCode)418)
                {
                    //var retryAfter = resp.Headers.RetryAfter?.Delta;
                    //throttler.StopAllRequestsDueToRateLimit(retryAfter);

                    //msg = $"{userFriendlyName}. Обнаружено превышение лимита запросов. " +
                    //      $"{(int)resp.StatusCode} ({resp.ReasonPhrase}). Retry-After={retryAfter}";
                    //logger.Error(msg);
                    //throw new RequestRateLimitBreakingException(msg);
                }

                msg = $"{userFriendlyName}. На запрос {requestParameters.SpecialPath} от сервера получен код ответа" +
                      $" {(int)resp.StatusCode} ({resp.StatusCode})\nТело ответа:\n{body}";
                logger.Error(msg);
                throw new EndpointCommunicationException(msg);
            }

            string strResp = "";
            try
            {
                string url;

                if (requestParameters.Method == HttpMethod.Post || requestParameters.Method == HttpMethod.Put)
                {
                    string body = null;

                    body = JsonConvert.SerializeObject(requestParameters.Parameters);
                    
                    url = $"{requestParameters.SpecialPath}{body}";

                    SignHttpWebRequest(requestParameters.Method.ToString(), url);

                    using (var content =
                        new StringContent(body, Encoding.UTF8, "application/json"))
                    {
                        using (var result = requestParameters.Method == HttpMethod.Post
                            ? httpClient.PostAsync(requestParameters.SpecialPath, content).Result
                            : httpClient.PutAsync(requestParameters.SpecialPath, null).Result)
                        {
                            strResp = result.Content.ReadAsStringAsync().Result;
                            checkResponse(result, strResp);
                        }
                    }


                }
                else if (requestParameters.Method == HttpMethod.Get || requestParameters.Method == HttpMethod.Delete)
                {
                    string body = "";

                    if (RequestParameters.SpecialBuildPath)
                    {
                        body = requestParameters.Parameters["UrlString"] ?? string.Empty;
                        RequestParameters.SpecialBuildPath = false;
                    }

                    if (body != string.Empty)
                    {
                        url = $"{requestParameters.SpecialPath}{($"/{body}")}";
                    }
                    else
                    {
                        string queryString = BuildQueryString(requestParameters.Parameters);
                        url =
                            $"{requestParameters.SpecialPath}{(string.IsNullOrEmpty(queryString) ? string.Empty : $"?{queryString}")}";
                    }

                    SignHttpWebRequest(requestParameters.Method.ToString(), url);
                    
                    using (var result = requestParameters.Method == HttpMethod.Get
                        ? httpClient.GetAsync(url).Result
                        : httpClient.DeleteAsync(url).Result)
                    {
                        strResp = result.Content.ReadAsStringAsync().Result;

                        // превышение лимита количества вызовов метода
                        if (result.StatusCode != HttpStatusCode.OK)
                        {
                            checkResponse(result, strResp);
                        }
                        checkResponse(result, strResp);
                    }
                }
                else
                {
                    throw new Exception($"Unsupporded HTTP-method {requestParameters.Method}");
                }
            }
            catch (EndpointCommunicationException)
            {
                throw;
            }
            catch (HttpRequestException e)
            {
                logger.Error($"{userFriendlyName}. При отправке HTTP-запроса возникло исключение\n{e}");
                throw;
            }
            catch (AggregateException e) when (e.InnerExceptions.Count == 1 &&
                                               e.InnerExceptions[0] is TaskCanceledException)
            {
                var msg = $"{userFriendlyName}. Возникло исключение {nameof(TaskCanceledException)} в связи с истечением таймаута запроса. " +
                          "Возможно, сервер временно не доступен";
                logger.Error(msg);
                throw new TimeoutException(msg, e);
            }
            catch (Exception e)
            {
                logger.Error($"{userFriendlyName}. При запросе данных с сервера возникло исключение\n{e}");
                throw;
            }

            if (string.IsNullOrWhiteSpace(strResp)) return default;
            if (typeof(TResp) == typeof(string)) return (TResp)(object)strResp;

            try
            {
                return JsonConvert.DeserializeObject<TResp>(strResp, serializerSettings);
            }
            catch (Exception e)
            {
                logger.Error($"{userFriendlyName}. Попытка десериализации строки, полученной от сервера вызвала исключение\n" +
                             $"Ответ сервера: {strResp}\n{e}");
                throw;
            }
            //return JsonConvert.DeserializeObject<TResp>(strResp, serializerSettings);
        }
       
        private void SignHttpWebRequest(string method, string urlPath)
        {
            var nonce = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
            var msg = Encoding.UTF8.GetBytes($"{nonce}{method}/api/v1/{urlPath}");

            httpClient.DefaultRequestHeaders.Clear();
            httpClient.DefaultRequestHeaders.Add("KC-API-KEY", Credentials.ApiKey);
            httpClient.DefaultRequestHeaders.Add("KC-API-SIGN", Convert.ToBase64String(new HMACSHA256(secretKey).ComputeHash(msg)));
            httpClient.DefaultRequestHeaders.Add("KC-API-TIMESTAMP", nonce.ToString());
            httpClient.DefaultRequestHeaders.Add("KC-API-PASSPHRASE", Credentials.PassPhrase);
            httpClient.DefaultRequestHeaders.Add("KC-API-VERSION", 2.ToString());
        }

        private string BuildQueryString(Dictionary<string, string> paramDic)
        {
            var mainParams = paramDic?.Any() != true
                ? string.Empty
                //: string.Join("&", paramDic.Select(x => $"{x.Key}={HttpUtility.UrlEncode(x.Value)}"));
                : string.Join("&", paramDic.Select(x => $"{x.Key}={x.Value}"));

            //if (!useSignature) return mainParams;

            return mainParams;
        }

        private string Sign(string queryString)
        {
            var messageBytes = Encoding.UTF8.GetBytes(queryString);
            var hash = new HMACSHA256(secretKey);
            var computedHash = hash.ComputeHash(messageBytes);
            return BitConverter.ToString(computedHash).Replace("-", "").ToLower();
        }

    }

    internal sealed class RequestParameters
    {
        /// <summary>
        /// HTTP-метод
        /// </summary>
        public HttpMethod Method { get; }

        /// <summary>
        /// Особое построение строки запроса
        /// </summary>
        public static bool SpecialBuildPath { get; set; }

        /// <summary>
        /// Путь к ресурсу (без базового адреса эндпоинта)
        /// </summary>
        public string SpecialPath { get; }

        /// <summary>
        /// Вес запроса в баллах
        /// </summary>
        public int RequestWeight { get; }

        /// <summary>
        /// Признак того, что высокого приоритета запроса
        /// </summary>
        public bool IsHighPriority { get; set; }

        /// <summary>
        /// Это запрос на работу с ордерами (размещение/удаление/модификация)
        /// </summary>
        public bool IsOrderRequest { get; set; }

        /// <summary>
        /// Передавать все параметры через QueryString URL
        /// </summary>
        public bool PassAllParametersInQueryString { get; set; }

        /// <summary>
        /// Параметры запроса
        /// </summary>
        public Dictionary<string, string> Parameters { get; set; }

        public RequestParameters(HttpMethod method, string specialPath, int requestWeight)
        {
            Method = method;
            SpecialPath = specialPath;
            RequestWeight = requestWeight;
        }

        private static readonly JsonSerializerSettings jsonSerializerSettings = new JsonSerializerSettings
        {
            NullValueHandling = NullValueHandling.Ignore,
            FloatParseHandling = FloatParseHandling.Decimal,
            Formatting = Formatting.Indented
        };
        public static Dictionary<string, string> GenerateParametersFromObject(object obj)
        {
            if (obj == null) return null;
            try
            {
                if (obj is SpecialBuildQuery)
                    SpecialBuildPath = true;

                jsonSerializerSettings.Converters.Add
                    (new Newtonsoft.Json.Converters.StringEnumConverter());

                var jObject = JObject.Parse(JsonConvert.SerializeObject(obj, jsonSerializerSettings));

                return jObject.Children()
                    .Cast<JProperty>()
                    .Where(x => x.Value.Type != JTokenType.Null)
                    .ToDictionary(x => x.Name, x => x.Value.ToString());
            }
            catch
            {
                return null;
            }
        }
    }
}
