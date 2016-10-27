using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using MvvmHelpers;
using Plugin.Connectivity;
using Poof.Extensions;
using Poof.Helpers;
using Poof.Services;
using Xamarin;
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

        public bool IsOfflineStackVisible { get; set; }

        #endregion

        #region Constructor

        public PoofListPageModel(IAzureService azureService)
        {
            this.azureService = azureService;

            var search = this
                .ToObservable(() => SearchText)
                .Throttle(TimeSpan.FromSeconds(1))
                .Where(x => !string.IsNullOrEmpty(x) && x.Length > 2)
                .Publish();

            var empty = this
                .ToObservable(() => SearchText)
                .Throttle(TimeSpan.FromSeconds(1))
                .Where(x => string.IsNullOrEmpty(x) || x.Length < 3)
                .Publish();

            search
                .ObserveOn(SynchronizationContext.Current)
                .Subscribe(x => ExecuteSearchPoofsCommand());

            empty
                .ObserveOn(SynchronizationContext.Current)
				.Subscribe(async (x) => await ExecuteRestorePoofsCommand());

            search.Connect();
            empty.Connect();

            if (Device.OS == TargetPlatform.iOS || Device.OS == TargetPlatform.Android)
                return;

            //if (ToolbarItems == null)
            //    ToolbarItems = new ObservableCollection<ToolbarItem>();
            //ToolbarItems.Add(new ToolbarItem
            //{
            //    Text = "Refresh",
            //    Icon = "poofTabSel.png",
            //    Command = LoadPoofsCommand
            //});
        }

        #endregion

        #region Commands

        private ICommand loadPoofsCommand;
        public ICommand LoadPoofsCommand => loadPoofsCommand ?? (loadPoofsCommand = new Command(async () => await ExecuteLoadPoofsCommand()));
        private async Task ExecuteLoadPoofsCommand()
        {
            if (IsBusy || !(await LoginAsync()))
                return;

			HockeyApp.MetricsManager.TrackEvent("Laod Poofs");

            try
            {
                LoadingMessage = "Loading Poofs...";
                IsBusy = true;
                var poofs = await azureService.GetPoofs(true);
                //var poofs = new List<Model.Poof>()
                //{
                //    new Model.Poof { Justified = true, Comment = "ciao ciao", DateUtc = DateTime.UtcNow},
                //    new Model.Poof { Justified = false, Comment = "ciao1 ciao1 ciao questo è un commento lungo lungo lungo ciao1 ciao1 ciao questo è un commento lungo lungo lungo", DateUtc = DateTime.UtcNow},
                //    new Model.Poof { Justified = false, Comment = "cavolo", DateUtc = DateTime.UtcNow.AddDays(-1)},
                //    new Model.Poof { Justified = false, Comment = "pasticcio", DateUtc = DateTime.UtcNow.AddDays(-2)},
                //    new Model.Poof { Justified = true, Comment = "ok", DateUtc = DateTime.UtcNow.AddDays(-3)}
                //};
              
                Poofs.ReplaceRange(poofs);

                FilterPoofs();

                SortPoofs();

            }
            catch (Exception ex)
            {
                Insights.Report(ex, Insights.Severity.Error);
                await CoreMethods.DisplayAlert("Sync Error", "Unable to sync Poofs, you may be offline", "OK");
            }
            finally
            {
                IsBusy = false;
            }

        }

        private ICommand searchPoofsCommand;
        public ICommand SearchPoofsCommand => searchPoofsCommand ?? (searchPoofsCommand = new Command(ExecuteSearchPoofsCommand));
        private void ExecuteSearchPoofsCommand()
        {

            if (IsBusy || !Poofs.Any())
                return;

			HockeyApp.MetricsManager.TrackEvent("Search Poofs");

            try
            {
                LoadingMessage = "Searching Poofs...";
                IsBusy = true;

                FilterPoofs();
                SortPoofs();

            }
            catch (Exception ex)
            {
                Insights.Report(ex, Insights.Severity.Error);
            }
            finally
            {
                IsBusy = false;
            }

        }

        private ICommand restorePoofsCommand;
        public ICommand RestorePoofsCommand => restorePoofsCommand ?? (restorePoofsCommand = new Command(async () => await ExecuteRestorePoofsCommand()));
        private async Task ExecuteRestorePoofsCommand()
        {
            if (IsBusy)
                return;

            try
            {
                LoadingMessage = "Searching Poofs...";
                IsBusy = true;

                var poofs = await azureService.GetPoofs();
                Poofs.ReplaceRange(poofs);
                
                SortPoofs();

            }
            catch (Exception ex)
            {
                Insights.Report(ex, Insights.Severity.Error);
            }
            finally
            {
                IsBusy = false;
            }

        }

        private ICommand deletePoofCommand;
        public ICommand DeletePoofCommand => deletePoofCommand ?? (deletePoofCommand = new Command<Model.Poof>(async poof => await ExecuteDeletePoofCommand(poof)));
        private async Task ExecuteDeletePoofCommand(Model.Poof poof)
        {
            if (IsBusy)
                return;

			HockeyApp.MetricsManager.TrackEvent("Delete Poof");

            try
            {
                LoadingMessage = "Deleting Poof...";
                IsBusy = true;
                Poofs.Remove(poof);
                FilterPoofs();
                SortPoofs();

                await azureService.DeletePoof(poof);

            }
            catch (Exception ex)
            {
                Insights.Report(ex, Insights.Severity.Error);
                await CoreMethods.DisplayAlert("Sync Error", "Unable to sync Poofs, you may be offline", "OK");
            }
            finally
            {
                IsBusy = false;
            }

        }

        private void FilterPoofs()
        {
            if (string.IsNullOrEmpty(SearchText) || SearchText.Length <= 2)
                return;

			var poofs = Poofs.Where(p => (!string.IsNullOrEmpty(p.Comment) && p.Comment.ToLower().Contains(SearchText.ToLower())) || p.DateDisplay.ToLower().Contains(SearchText.ToLower())).ToList();
            Poofs.ReplaceRange(poofs);
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
            //return Task.FromResult(true);
            return Settings.IsLoggedIn ? Task.FromResult(true) : azureService.LoginAsync();
        }

        #endregion

        #region LifeCycle

        protected override async void ViewIsAppearing(object sender, EventArgs e)
        {
            base.ViewIsAppearing(sender, e);

           CrossConnectivity.Current.ConnectivityChanged += ConnecitvityChanged;
           IsOfflineStackVisible = !CrossConnectivity.Current.IsConnected;

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

        private void ConnecitvityChanged(object sender, Plugin.Connectivity.Abstractions.ConnectivityChangedEventArgs e)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                IsOfflineStackVisible = !e.IsConnected;
            });
        }

        #endregion
    }
}
