using System;
using System.Collections.Generic;
using System.Text;
using PoissonSoft.KuCoinApi.Contracts.DataStream;
using PoissonSoft.KuCoinApi.Contracts.PublicWebSocket;
using PoissonSoft.KuCoinApi.Transport;

namespace PoissonSoft.KuCoinApi.DataStream
{
    public interface IUserDataStream
    {
        /// <summary>
        /// Событие обновления балансов аккаунта
        /// </summary>
        
        //event EventHandler<AccountUpdatePayload> OnAccountUpdate;
        /// <summary>
        /// Текущее состояние потока
        /// </summary>
        DataStreamStatus Status { get; }

        /// <summary>
        /// Открытие (запуск) потока
        /// </summary>
        void Open();

        /// <summary>
        /// Закрытие потока
        /// </summary>
         void Close();
        
        /// <summary>
        /// Требуется ли закрывать созданный ListenKey при закрытии потока
        /// </summary>
        bool NeedCloseListenKeyOnClosing { get; set; }

        /// <summary>
        /// Событие обновления балансов аккаунта
        /// </summary>
        event EventHandler<AccountBalanceUpdatePayload> OnAccountUpdate;

        /// <summary>
        /// Событие обновления балансов аккаунта в результате ввода/вывода или внутреннего перевода
        /// между счетами
        /// </summary>
        event EventHandler<BalanceUpdatePayload> OnBalanceUpdate;

        /// <summary>
        /// Событие изменения ордера
        /// </summary>
        event EventHandler<OrderExecutionReportPayload> OnOrderExecuteEvent;
    }
}
