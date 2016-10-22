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
        private readonly AzureService azureService;

        #region Properties

        public string Comment { get; set; }
        public bool Justified { get; set; }
        public string LoadingMessage { get; set; }

        #endregion

        public PoofPageModel()
        {
            azureService = new AzureService();
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

                var poof = await azureService.AddPoof(Justified, Comment, Settings.UserId);

                Comment = null;
                Justified = false;

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
