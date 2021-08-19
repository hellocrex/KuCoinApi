﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using PoissonSoft.CommonUtils.ConsoleUtils;
using PoissonSoft.CommonUtils.Security;
using PoissonSoft.KuCoinApi;

namespace KuCoinApi.Example
{
    internal class NppCryptProvider : ICredentialsProvider
    {
        public KuCoinApiClientCredentials GetCredentials()
        {
            // ReSharper disable once StringLiteralTypo
            const string DEFAULT_FILE_NAME = "credentials.nppcrypt";
            var fileName =
                InputHelper.GetString($"NppCrypt file containing credentials ({DEFAULT_FILE_NAME} by default):");
            if (string.IsNullOrWhiteSpace(fileName)) fileName = DEFAULT_FILE_NAME;
            if (!File.Exists(fileName)) 
                throw new Exception($"File '{fileName}' does not exist");

            var masterPassword = InputHelper.GetSecureData("Password to decrypt file:");

            var fileContent = NppCryptDecoder.ReadAllFileAsText(fileName, masterPassword);
            var lines = fileContent.Split(new[] {"\r\n", "\r", "\n"}, StringSplitOptions.None);
            if (lines.Length < 2) 
                throw new Exception($"Too few ({lines.Length}) lines in the file (min 2 lines expected)");
            var credentials = new KuCoinApiClientCredentials
            {
                ApiKey = lines[0].Trim(),
                SecretKey = lines[1].Trim(),
                PassPhrase = lines[2].Trim()
            };
            if (lines.Length >= 4)
                credentials.ProxyAddress = lines[3].Trim();
            if (lines.Length >= 5)
                credentials.ProxyAddress = lines[4].Trim();

            return credentials;
        }
    }
}
