using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using FreshMvvm;
using MvvmHelpers;
using Poof.Helpers;
using Poof.Services;
using Xamarin;
using Xamarin.Forms;

namespace Poof.PageModels
{
    public class PoofPageModel : BasePageModel
    {
        private readonly IAzureService azureService;

        #region Properties

        public string Comment { get; set; }
        public bool Justified { get; set; }
        public string LoadingMessage { get; set; }

        #endregion

        public PoofPageModel(IAzureService azureService)
        {
            this.azureService = azureService;
        }

        #region Commands

        private ICommand poofCommand;
        public ICommand PoofCommand => poofCommand ?? (poofCommand = new Command(async () => await ExecutePoofCommand(), () => !IsBusy));
        private async Task ExecutePoofCommand()
        {
            try
            {
                if (!await LoginAsync())
                    return;

                LoadingMessage = "Adding Poof...";
                IsBusy = true;

				//await Task.Delay(4000);
				await azureService.AddPoof(Justified, Comment, Settings.UserId);

                Comment = null;
                Justified = false;

                //esiste overload per proprietà e metriche
                HockeyApp.MetricsManager.TrackEvent("Add Poof");

            }
            catch (Exception ex)
            {
                Insights.Report(ex, Insights.Severity.Error);
                await CoreMethods.DisplayAlert("Service Error", ex.Message, "OK");
            }
            finally
            {
                LoadingMessage = string.Empty;
                IsBusy = false;
            }

        }

        public Task<bool> LoginAsync()
        {
            //return Task.FromResult(true);
            return Settings.IsLoggedIn ? Task.FromResult(true) : azureService.LoginAsync();
        }

        #endregion 
    }
}
