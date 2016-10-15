using FreshMvvm;
using Poof.Services;
using Poof.PageModels;
using Xamarin.Forms;

namespace Poof
{
	public partial class App : Application
	{
		public App()
		{
			InitializeComponent();

            FreshIOC.Container.Register<IAzureService, AzureService>();

            var tabbedNavigation = new FreshTabbedNavigationContainer();
			tabbedNavigation.AddTab<PoofPageModel>("Poof", "tab_poof_w.png");
			tabbedNavigation.AddTab<PoofListPageModel>("Poofs", "tab_poofList_w.png");

            //tabbedNavigation.BarBackgroundColor = (Color)Resources[@"BarTint"];
            //tabbedNavigation.BarTextColor = (Color)Resources[@"Tint"];

            MainPage = tabbedNavigation;
		}

		protected override void OnStart()
		{
			// Handle when your app starts
		}

		protected override void OnSleep()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume()
		{
			// Handle when your app resumes
		}
	}
}
