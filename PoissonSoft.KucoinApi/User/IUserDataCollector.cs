using System;
using System.Collections.Generic;
using System.Text;

namespace PoissonSoft.KuCoinApi.User
{
    public interface IUserDataCollector
    {
        /// <summary>
        /// Запуск сбора данных
        /// </summary>
        void Start();

        /// <summary>
        /// Остановка сбора данных
        /// </summary>
        void Stop();
    }
}
