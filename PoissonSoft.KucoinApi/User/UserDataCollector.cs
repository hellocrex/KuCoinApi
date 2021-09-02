using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using NLog;
using PoissonSoft.KuCoinApi.Contracts.DataStream;
using PoissonSoft.KuCoinApi.Contracts.Enums;
using PoissonSoft.KuCoinApi.Contracts.User;
using PoissonSoft.KuCoinApi.Contracts.User.Request;
using PoissonSoft.KuCoinApi.Transport;
using PoissonSoft.KuCoinApi.Transport.Rest;

namespace PoissonSoft.KuCoinApi.User
{
    public class UserDataCollector : IUserDataCollector
    {
        private readonly KuCoinApiClient apiClient;
        private readonly object startLock = new object();
        private readonly string userFriendlyName = nameof(UserDataCollector);

        private AccountsList AccountInformation;
        private AccountsList aInformation;
        private long lastUpdateTimestamp;
        private readonly ConcurrentQueue<AccountUpdatePayload> accountUpdatesQueue = new ConcurrentQueue<AccountUpdatePayload>();
        private CancellationTokenSource cancellationTokenSource;
        private Thread queueDispatchingThread;


        public UserDataCollector(KuCoinApiClient apiClient)
        {
            this.apiClient = apiClient ?? throw new ArgumentNullException(nameof(apiClient));
        }
        

        public void Start()
        {
            lock (startLock)
            {
                if (IsStarted) return;

                while (true)
                {
                    try
                    {
                        TryStart();
                        break;
                    }
                    catch (Exception e)
                    {
                        apiClient.Logger.Error($"{userFriendlyName}. Exception when starting Collector\n{e}");
                        Thread.Sleep(TimeSpan.FromSeconds(1));
                    }
                }

                IsStarted = true;
            }
        }

        public void Stop()
        {
            lock (startLock)
            {
                if (!IsStarted) return;

                while (true)
                {
                    try
                    {
                        TryStop();
                        break;
                    }
                    catch (Exception e)
                    {
                        apiClient.Logger.Error($"{userFriendlyName}. Exception when stopping Collector\n{e}");
                        Thread.Sleep(TimeSpan.FromSeconds(1));
                    }
                }

                IsStarted = false;
            }
        }

        public bool IsStarted { get; private set; }

        //public AccountInformation AccountInformation
        //{
        //    get => (AccountInformation)accountInformation?.Clone();
        //    private set => Interlocked.Exchange(ref accountInformation, value);
        //}

        //public AccountsList AccountInformation
        //{
        //    get => (AccountsList)accountInformation?.Clone();
        //    private set => Interlocked.Exchange(ref accountInformation, value);
        //}

        private void TryStart()
        {
            var timeout = TimeSpan.Zero;

            apiClient.UserDataStream.OnAccountUpdate += OnAccountUpdate;

            // Включение SpotDataStream
            var nextProblemInform = DateTimeOffset.UtcNow.AddMinutes(1);
            while (true)
            {
                if (apiClient.UserDataStream.Status == DataStreamStatus.Active) break;

                if (apiClient.UserDataStream.Status == DataStreamStatus.Closed)
                {
                    apiClient.UserDataStream.Open();
                    timeout = TimeSpan.FromSeconds(1);
                }
                else if (timeout.TotalSeconds < 15)
                {
                    timeout += TimeSpan.FromSeconds(1);
                }

                Thread.Sleep(timeout);

                if (DateTimeOffset.UtcNow > nextProblemInform)
                {
                    apiClient.Logger.Warn($"{userFriendlyName}. Проблема инициализации: Не удаётся включить {nameof(apiClient.UserDataStream)}");
                    nextProblemInform = DateTimeOffset.UtcNow.AddMinutes(1);
                }
            }
            apiClient.Logger.Info($"{userFriendlyName}. Инициализация: {nameof(apiClient.UserDataStream)} успешно включен");

            // Загрузка стартового снапшота AccountInformation
            timeout = TimeSpan.Zero;
            nextProblemInform = DateTimeOffset.UtcNow.AddMinutes(1);
            while (true)
            {
                AccountsList snapshot = null;
                try
                {
                    var req = new ReqAccount
                    {
                        Currency = null,
                        AccountType = AccountType.Trade
                    };
                    snapshot = apiClient.UserApi.GetListAccounts(req);
                }
                catch (Exception e)
                {
                    apiClient.Logger.Error($"{userFriendlyName}. Проблема инициализации: Исключение при попытке загрузить стартовый снапшот {nameof(AccountInformation)}\n{e}");
                }

                if (snapshot != null)
                {
                    lastUpdateTimestamp = DateTime.UtcNow.Millisecond;
                    AccountInformation = snapshot;
                    break;
                }

                if (timeout.TotalSeconds < 15)
                {
                    timeout += TimeSpan.FromSeconds(1);
                }

                Thread.Sleep(timeout);

                if (DateTimeOffset.UtcNow > nextProblemInform)
                {
                   // apiClient.Logger.Warn($"{userFriendlyName}. Проблема инициализации: Не удаётся загрузить стартовый снапшот {nameof(AccountInformation)}");
                    nextProblemInform = DateTimeOffset.UtcNow.AddMinutes(1);
                }
            }

            cancellationTokenSource = new CancellationTokenSource();
            queueDispatchingThread = new Thread(QueueDispatching);
            queueDispatchingThread.Start(cancellationTokenSource.Token);
        }


        private void TryStop()
        {
            apiClient.UserDataStream.OnAccountUpdate -= OnAccountUpdate;
            cancellationTokenSource?.Cancel();
            queueDispatchingThread?.Join(TimeSpan.FromSeconds(30));
            AccountInformation = null;
        }

        private void QueueDispatching(object ct)
        {
            var cancellationToken = (CancellationToken)ct;

            while (!cancellationToken.IsCancellationRequested)
            {
                if (!accountUpdatesQueue.IsEmpty)
                {
                    try
                    {
                        ProcessUpdates();
                    }
                    catch (Exception e)
                    {
                        apiClient.Logger.Error($"{userFriendlyName}. Exception when processing Account Updates\n{e}");
                    }
                }

                Task.Delay(TimeSpan.FromMilliseconds(100), cancellationToken)
                    // ReSharper disable once MethodSupportsCancellation
                    .ContinueWith(task => { })
                    // ReSharper disable once MethodSupportsCancellation
                    .Wait();
            }
        }

        private void ProcessUpdates()
        {
            var updates = new List<AccountUpdatePayload>(accountUpdatesQueue.Count);
            while (accountUpdatesQueue.TryDequeue(out var update))
            {
                if (update.LastAccountUpdateTime > lastUpdateTimestamp)
                {
                    updates.Add(update);
                }
            }

            if (!updates.Any()) return;

            //var snapshot = AccountInformation;
            //if (snapshot == null) return;

            //foreach (var update in updates)
            //{
            //    if (update.ChangedBalances?.Any() != true) continue;

            //    foreach (var actualBalance in update.ChangedBalances)
            //    {
            //        var balanceToEdit = snapshot.Balances.FirstOrDefault(x => x.Asset == actualBalance.Asset);
            //        if (balanceToEdit == null)
            //        {
            //            balanceToEdit = new Balance
            //            {
            //                Asset = actualBalance.Asset
            //            };
            //            snapshot.Balances.Add(balanceToEdit);
            //        }

            //        balanceToEdit.Free = actualBalance.Free;
            //        balanceToEdit.Locked = actualBalance.Locked;
            //    }

            //    snapshot.UpdateTimestamp = update.LastAccountUpdateTime;
            //}

            //AccountInformation = snapshot;
        }

        private void OnAccountUpdate(object sender, AccountUpdatePayload update)
        {
            accountUpdatesQueue.Enqueue(update);
        }

        
        public void Dispose()
        {
            if (IsStarted) Stop();

            apiClient?.Dispose();
            cancellationTokenSource?.Dispose();
        }
    }
}
