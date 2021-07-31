using System;
using System.Collections.Generic;
using System.Text;
using PoissonSoft.KuСoinApi.Contracts.User;

namespace PoissonSoft.KuСoinApi.User
{
    public interface IUserApi
    {
        KuСoinInfo[] AllCoinsInformation(int cacheValidityIntervalSec = 10 * 60);

        /// <summary>
        /// Fetch deposit history.
        /// </summary>
        /// <returns></returns>
        DepositInfo[] DepositHistory(DepositHistoryRequest request);
    }


}
