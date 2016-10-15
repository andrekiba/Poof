using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using FreshMvvm;
using MvvmHelpers;
using Plugin.Connectivity;
using Poof.Helpers;
using Poof.Services;
using Xamarin.Forms;

namespace Poof.PageModels
{
    public class PoofListPageModel : BasePageModel
    {
        private readonly IAzureService azureService;

        #region Properties

        public ObservableRangeCollection<Model.Poof> Poofs { get; } = new ObservableRangeCollection<Model.Poof>();
        public ObservableRangeCollection<Grouping<string, Model.Poof>> PoofsGrouped { get; } = new ObservableRangeCollection<Grouping<string, Model.Poof>>();

        public string SearchText { get; set; }

        public string LoadingMessage { get; set; }

        #endregion

        #region Constructor

        public PoofListPageModel()
        {
        }

        public PoofListPageModel(IAzureService azureService)
        {
            this.azureService = azureService;
        }

        #endregion

        #region Commands

        private ICommand loadPoofsCommand;
        public ICommand LoadPoofsCommand => loadPoofsCommand ?? (loadPoofsCommand = new Command(async () => await ExecuteLoadPoofsCommand()));
        private async Task ExecuteLoadPoofsCommand()
        {
            if (IsBusy || !(await LoginAsync()))
                return;

            try
            {
                LoadingMessage = "Loading Poofs...";
                IsBusy = true;
                //var poofs = await azureService.GetPoofs();
                var poofs = new List<Model.Poof>()
                {
                    new Model.Poof { Justified = true, Comment = "ciao ciao", DateUtc = DateTime.UtcNow},
                    new Model.Poof { Justified = false, Comment = "cavolo", DateUtc = DateTime.UtcNow.AddDays(-1)},
                    new Model.Poof { Justified = false, Comment = "pasticcio", DateUtc = DateTime.UtcNow.AddDays(-2)},
                    new Model.Poof { Justified = true, Comment = "ok", DateUtc = DateTime.UtcNow.AddDays(-3)}
                };

                Poofs.ReplaceRange(poofs);

                SortPoofs();

            }
            catch (Exception ex)
            {
                Debug.WriteLine("OH NO!" + ex);

                await Application.Current.MainPage.DisplayAlert("Sync Error", "Unable to sync Poofs, you may be offline", "OK");
            }
            finally
            {
                IsBusy = false;
            }

        }

        private void SortPoofs()
        {
            var groups = from poof in Poofs
                         orderby poof.DateUtc descending
                         group poof by poof.DateDisplay
                         into poofGroup
                         select new Grouping<string, Model.Poof>($"{poofGroup.Key} ({poofGroup.Count()})", poofGroup);

            PoofsGrouped.ReplaceRange(groups);
        }

        public Task<bool> LoginAsync()
        {
            return Task.FromResult(true);
            //return Settings.IsLoggedIn ? Task.FromResult(true) : azureService.LoginAsync();
        }

        #endregion

        #region LifeCycle

        protected override async void ViewIsAppearing(object sender, EventArgs e)
        {
            base.ViewIsAppearing(sender, e);

            if (Device.OS != TargetPlatform.iOS && Device.OS != TargetPlatform.Android)
            {
                ToolbarItems.Add(new ToolbarItem
                {
                    Text = "Refresh",
                    Command = LoadPoofsCommand
                });
            }

            CrossConnectivity.Current.ConnectivityChanged += ConnecitvityChanged;
            //OfflineStack.IsVisible = !CrossConnectivity.Current.IsConnected;

            if (Poofs.Count == 0 && Settings.IsLoggedIn)
                LoadPoofsCommand.Execute(null);
            else
            {
                await LoginAsync();
                if (Settings.IsLoggedIn)
                    LoadPoofsCommand.Execute(null);
            }
        }

        protected override void ViewIsDisappearing(object sender, EventArgs e)
        {
            base.ViewIsDisappearing(sender, e);
            CrossConnectivity.Current.ConnectivityChanged -= ConnecitvityChanged;
        }

        public override void Init(object data)
        {
            
        }

        private static void ConnecitvityChanged(object sender, Plugin.Connectivity.Abstractions.ConnectivityChangedEventArgs e)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                //OfflineStack.IsVisible = !e.IsConnected;
            });
        }

        #endregion
    }
}
