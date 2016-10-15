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
        public ICommand PoofCommand => poofCommand ?? (poofCommand = new Command(async () => await ExecutePoofCommand()));
        private async Task ExecutePoofCommand()
        {
            if (IsBusy || !(await LoginAsync()))
                return;

            try
            {
                LoadingMessage = "Adding Poof...";
                IsBusy = true;

                //var poof = await azureService.AddPoof(Justified, Comment, Settings.UserId);
 
            }
            catch (Exception ex)
            {
                Debug.WriteLine("OH NO!" + ex);
            }
            finally
            {
                LoadingMessage = string.Empty;
                IsBusy = false;
            }

        }

        public Task<bool> LoginAsync()
        {
            return Task.FromResult(true);
            //return Settings.IsLoggedIn ? Task.FromResult(true) : azureService.LoginAsync();
        }

        #endregion 
    }
}
