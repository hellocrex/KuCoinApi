using System;
using System.Collections.Generic;
using System.Text;
using PoissonSoft.KuCoinApi;

namespace KuCoinApi.Example
{
    interface ICredentialsProvider
    {
        KuCoinApiClientCredentials GetCredentials();
    }
}
