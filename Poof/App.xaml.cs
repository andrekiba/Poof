using FreshMvvm;
using Poof.Services;
using Poof.ViewModel;
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
            tabbedNavigation.AddTab<PoofViewModel>("Poof", "");
            tabbedNavigation.AddTab<PoofListViewModel>("Poof List", "");
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
