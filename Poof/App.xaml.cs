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
			//tabbedNavigation.AddTab<PoofPageModel>("Poof", Device.OnPlatform("poofTab.png", "poofTab.png", "Assets/poofTab.png") );
			//tabbedNavigation.AddTab<PoofListPageModel>("Poofs", Device.OnPlatform("poofListTab.png", "poofListTab.png", "Assets/poofListTab.png"));

			tabbedNavigation.AddTab<PoofPageModel>("Poof", Device.OnPlatform("poofTab.png", "", ""));
			tabbedNavigation.AddTab<PoofListPageModel>("Poofs", Device.OnPlatform("poofListTab.png", "", ""));

            //tabbedNavigation.BarBackgroundColor = (Color)Resources[@"BarTint"];
            //tabbedNavigation.BarTextColor = (Color)Resources[@"Tint"];
		    //tabbedNavigation.Title = "POOF";
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
