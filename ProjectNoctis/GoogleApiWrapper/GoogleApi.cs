using Google.Apis.Auth.OAuth2;
using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;

namespace GoogleApiWrapper
{
    public class GoogleApi
    {

        private TokenObject UserToken = Token.CreateTokens();
        public SheetsService Service { get; set; }
        public GoogleApi()
        {
            Service = ServiceStart();
        }

        public SheetsService ServiceStart()
        {
            return new SheetsService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = UserToken.Credentials,
                ApplicationName = UserToken.Application
            });
        }
    }
}
