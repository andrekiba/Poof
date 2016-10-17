using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Microsoft.WindowsAzure.MobileServices;
using Poof.Authentication;
using Xamarin.Forms;
using Poof.Droid.Authentication;
using Poof.Helpers;

[assembly: Dependency(typeof(SocialAuthDroid))]
namespace Poof.Droid.Authentication
{
    public class SocialAuthDroid : IAuthentication
    {
        public Task<MobileServiceUser> LoginAsync(IMobileServiceClient client, MobileServiceAuthenticationProvider provider, IDictionary<string, string> parameters = null)
        {
            try
            {
                return client.LoginAsync(Forms.Context, provider, parameters);
            }
            catch
            {
                // ignored
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
                System.Diagnostics.Debug.WriteLine("Unable to refresh user: " + e);
            }

            return false;
        }

        public void ClearCookies()
        {
            try
            {
                if ((int)global::Android.OS.Build.VERSION.SdkInt >= 21)
                    global::Android.Webkit.CookieManager.Instance.RemoveAllCookies(null);
            }
            catch (Exception ex)
            {
            }
        }
    }
}