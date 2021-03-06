﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;

namespace Poof.Authentication
{
    public interface IAuthentication
    {
        Task<MobileServiceUser> LoginAsync(IMobileServiceClient client, MobileServiceAuthenticationProvider provider, IDictionary<string, string> paramameters = null);
        Task<bool> RefreshUser(IMobileServiceClient client);
        void ClearCookies();
    }
}
