using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace PoissonSoft.KuCoinApi.Contracts.MarketData.Request
{
    /// <summary>
    /// Класс, который участвует в особое построение строки запроса
    /// Параметр не требуется преобразовывать в json
    /// </summary>
    public class SpecialBuildQuery
    {
        /// <summary>
        /// Параметр
        /// </summary>
        public string Parameter { get; set; }
    }
}
