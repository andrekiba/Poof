using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Android.OS;
using Microsoft.WindowsAzure.MobileServices;
using Poof.Authentication;
using Xamarin.Forms;
using Poof.Droid.Authentication;
using Poof.Helpers;
using Xamarin;

[assembly: Dependency(typeof(SocialAuthDroid))]
namespace Poof.Droid.Authentication
{
    public class SocialAuthDroid : IAuthentication
    {
        public async Task<MobileServiceUser> LoginAsync(IMobileServiceClient client, MobileServiceAuthenticationProvider provider, IDictionary<string, string> parameters = null)
        {
            try
            {
                return await client.LoginAsync(Forms.Context, provider, parameters);
            }
            catch(Exception ex)
            {
                // ignored
                Insights.Report(ex, Insights.Severity.Error);
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
            catch (Exception ex)
            {
                Insights.Report(ex, Insights.Severity.Error);
            }

            return false;
        }

        public void ClearCookies()
        {
            try
            {
                if ((int)Build.VERSION.SdkInt >= 21)
                    Android.Webkit.CookieManager.Instance.RemoveAllCookies(null);
            }
            catch (Exception)
            {
                // ignored
            }
        }
    }
}