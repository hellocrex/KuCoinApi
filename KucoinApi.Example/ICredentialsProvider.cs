using System;
using System.Collections.Generic;
using System.Text;
using PoissonSoft.KuСoinApi;

namespace KuСoinApi.Example
{
    interface ICredentialsProvider
    {
        KuСoinApiClientCredentials GetCredentials();
    }
}
