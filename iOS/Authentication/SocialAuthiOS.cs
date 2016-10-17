using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;
using Poof.Authentication;
using Poof.Helpers;
using Poof.iOS.Authentication;
using Xamarin.Forms;

[assembly: Dependency(typeof(SocialAuthiOS))]
namespace Poof.iOS.Authentication
{
    class SocialAuthiOS : IAuthentication
    {
        public Task<MobileServiceUser> LoginAsync(IMobileServiceClient client, MobileServiceAuthenticationProvider provider, IDictionary<string, string> parameters = null)
        {
            try
            {
                return client.LoginAsync(GetController(), provider, parameters);

            }
            catch (Exception e)
            {
                e.Data["method"] = "LoginAsync";
            }

            return null;
        }

        public virtual async Task<bool> RefreshUser(IMobileServiceClient client)
        {
            try
            {
                var user = await client.RefreshUserAsync();

                if (user != null)
                {
                    client.CurrentUser = user;
                    Settings.AuthToken = user.MobileServiceAuthenticationToken;
                    Settings.UserId = user.UserId;
                    return true;
                }
            }
            catch (System.Exception e)
            {
                Debug.WriteLine("Unable to refresh user: " + e);
            }

            return false;
        }

        public void ClearCookies()
        {
            var store = Foundation.NSHttpCookieStorage.SharedStorage;
            var cookies = store.Cookies;

            foreach (var c in cookies)
            {
                store.DeleteCookie(c);
            }
        }

        private static UIKit.UIViewController GetController()
        {
            var window = UIKit.UIApplication.SharedApplication.KeyWindow;
            var root = window.RootViewController;
            if (root == null)
                return null;

            var current = root;
            while (current.PresentedViewController != null)
            {
                current = current.PresentedViewController;
            }

            return current;
        }     
    }
}
