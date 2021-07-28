using System;
using System.Collections.Generic;
using System.Text;
using PoissonSoft.KucoinApi;

namespace KucoinApi.Example
{
    interface ICredentialsProvider
    {
        KucoinApiClientCredentials GetCredentials();
    }
}
