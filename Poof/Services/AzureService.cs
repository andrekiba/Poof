using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;
using Microsoft.WindowsAzure.MobileServices.SQLiteStore;
using Microsoft.WindowsAzure.MobileServices.Sync;
using Plugin.Connectivity;
using Poof.Authentication;
using Poof.Helpers;
using Poof.Services;
using Xamarin.Forms;

[assembly: Dependency(typeof(AzureService))]
namespace Poof.Services
{
    public class AzureService : IAzureService
    {
        private IMobileServiceSyncTable<Model.Poof> poofTable;

        #region Properties

        public MobileServiceClient Client { get; set; }
        public static bool UseAuth { get; set; } = false;
		public static string DbPath { get; set; } = "syncstore2.db";

        #endregion

        private async Task Initialize()
        {
            if (Client?.SyncContext?.IsInitialized ?? false)
                return;

            const string appUrl = "https://poof.azurewebsites.net";

            #if AUTH      
            Client = new MobileServiceClient(appUrl, new AuthHandler());

            if (!string.IsNullOrWhiteSpace (Settings.AuthToken) && !string.IsNullOrWhiteSpace (Settings.UserId)) {
                Client.CurrentUser = new MobileServiceUser(Settings.UserId)
                {
                    MobileServiceAuthenticationToken = Settings.AuthToken
                };
            }
            #else
            //Create our client
            Client = new MobileServiceClient(appUrl);
            #endif

            //setup our local sqlite store and intialize our table
			SQLitePCL.Batteries.Init();
			var store = new MobileServiceSQLiteStore(DbPath);

            //Define table
            store.DefineTable<Model.Poof>();

            //Initialize SyncContext
            await Client.SyncContext.InitializeAsync(store, new MobileServiceSyncHandler());

            //Get our sync table that will call out to azure
            poofTable = Client.GetSyncTable<Model.Poof>();
        }

        public async Task<IEnumerable<Model.Poof>> GetPoofs()
        {
            await Initialize();
            await SyncPoof();
            return await poofTable.OrderBy(c => c.DateUtc).ToEnumerableAsync();
        }

        public async Task<Model.Poof> AddPoof()
        {
            await Initialize();

            var poof = new Model.Poof
            {
                DateUtc = DateTime.UtcNow
            };

            await poofTable.InsertAsync(poof);

            await SyncPoof();

            return poof;
        }

        public async Task SyncPoof()
        {
            try
            {
                if (!CrossConnectivity.Current.IsConnected)
                    return;

                //pull down all latest changes and then push current coffees up
                await Client.SyncContext.PushAsync();
                await poofTable.PullAsync("allPoofs", poofTable.CreateQuery());
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Unable to sync poofs, that is alright as we have offline capabilities: " + ex);
            }

        }

        public async Task<bool> LoginAsync()
        {
            await Initialize();

            var auth = DependencyService.Get<IAuthentication>();
            var user = await auth.LoginAsync(Client, MobileServiceAuthenticationProvider.Google);

            if (user == null)
            {
                Settings.AuthToken = string.Empty;
                Settings.UserId = string.Empty;
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await Application.Current.MainPage.DisplayAlert("Login Error", "Unable to login, please try again", "OK");
                });
                return false;
            }

            Settings.AuthToken = user.MobileServiceAuthenticationToken;
            Settings.UserId = user.UserId;

            return true;
        }
    }
}
