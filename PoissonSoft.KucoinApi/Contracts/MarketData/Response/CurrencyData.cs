using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace PoissonSoft.KuСoinApi.Contracts.MarketData.Response
{
    public class CurrencyData
    {
        /// <summary>
        /// A unique currency code that will never change
        /// </summary>
        [JsonProperty("currency")]
        public string Currency { get; set; }

        /// <summary>
        /// Currency name, will change after renaming
        /// </summary>
        [JsonProperty("name")]
        public string Ticker { get; set; }

        /// <summary>
        /// Full name of a currency, will change after renaming
        /// </summary>
        [JsonProperty("fullName")]
        public string CoinName { get; set; }

        /// <summary>
        /// Full name of a currency, will change after renaming
        /// </summary>
        [JsonProperty("contractAddress")]
        public string ContractAddress { get; set; }

        /// <summary>
        /// Currency precision
        /// </summary>
        [JsonProperty("precision")]
        public int Precision { get; set; }

        /// <summary>
        /// Minimum withdrawal amount
        /// </summary>
        [JsonProperty("withdrawalMinSize")]
        public double WithdrawalMinSize { get; set; }

        /// <summary>
        /// Minimum fees charged for withdrawal
        /// </summary>
        [JsonProperty("withdrawalMinFee")]
        public double WithdrawalMinFee { get; set; }

        /// <summary>
        /// Support withdrawal or not
        /// </summary>
        [JsonProperty("isWithdrawEnabled")]
        public bool isWithdrawEnabled { get; set; }

        /// <summary>
        /// Support deposit or not
        /// </summary>
        [JsonProperty("isDepositEnabled")]
        public bool isDepositEnabled { get; set; }

        /// <summary>
        /// Support margin or not
        /// </summary>
        [JsonProperty("isMarginEnabled")]
        public bool isMarginEnabled { get; set; }

        /// <summary>
        /// Support debit or not
        /// </summary>
        [JsonProperty("isDebitEnabled")]
        public bool isDebitEnabled { get; set; }

        /// <summary>
        /// Number of block confirmations
        /// </summary>
        [JsonProperty("confirms")]
        public long Confirms { get; set; }
    }
}
