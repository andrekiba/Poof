﻿using FreshMvvm;
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
			tabbedNavigation.AddTab<PoofPageModel>("Poof", "tab_poof.png");
			tabbedNavigation.AddTab<PoofListPageModel>("Poof List", "tab_poofList.png");

            //tabbedNavigation.BarBackgroundColor = (Color)Resources[@"LightGreen"];
            //tabbedNavigation.BarTextColor = Color.White;

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
