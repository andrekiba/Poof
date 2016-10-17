using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;
using Poof.Authentication;
using Poof.Helpers;
using Poof.UWP.Authentication;
using Xamarin.Forms;

[assembly: Dependency(typeof(SocialAuthUWP))]
namespace Poof.UWP.Authentication
{
    public class SocialAuthUWP : IAuthentication
    {
        public Task<MobileServiceUser> LoginAsync(IMobileServiceClient client, MobileServiceAuthenticationProvider provider, IDictionary<string, string> parameters = null)
        {
            try
            {
                return client.LoginAsync(provider, parameters);
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
                Debug.WriteLine("Unable to refresh user: " + e);
            }

            return false;
        }

        public void ClearCookies()
        {

        }
    }
}
