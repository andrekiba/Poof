using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using Social;
using UIKit;
using Xamarin.Forms;

namespace Poof.iOS
{
	[Register("AppDelegate")]
	public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
	{
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
		{
            #region Colors

            var tint = UIColor.FromRGB(241, 196, 15); //#F1C40F
		    var barTint = UIColor.FromRGB(231, 76, 60); //#E74C3C

            UINavigationBar.Appearance.BarTintColor = barTint; //navigation bar background
            UINavigationBar.Appearance.TintColor = tint; //navigation bar buttons color
            UITabBar.Appearance.TintColor = tint;
            UITabBar.Appearance.BarTintColor = barTint;
            UISwitch.Appearance.OnTintColor = tint;

            UINavigationBar.Appearance.SetTitleTextAttributes(new UITextAttributes
            {
                Font = UIFont.FromName("Avenir-Medium", 17f),
                TextColor = tint
            });

            //UINavigationBar.Appearance.TitleTextAttributes = new UIStringAttributes
            //{
            //    Font = UIFont.FromName("Avenir-Medium", 17f),
            //    ForegroundColor = tint
            //};

            //UIBarButtonItem.Appearance.TintColor = tint; //Tint color of button items

            //NavigationBar Buttons 
            //UIBarButtonItem.Appearance.SetTitleTextAttributes(new UITextAttributes
            //{
            //    Font = UIFont.FromName("Avenir-Medium", 17f),
            //    TextColor = tint
            //}, UIControlState.Normal);

            ////TabBar
            //UITabBarItem.Appearance.SetTitleTextAttributes(new UITextAttributes
            //{
            //    Font = UIFont.FromName("Avenir-Book", 10f)
            //}, UIControlState.Normal);


            #endregion

            Microsoft.WindowsAzure.MobileServices.CurrentPlatform.Init();
			Forms.Init();

			// Code for starting up the Xamarin Test Cloud Agent
            #if ENABLE_TEST_CLOUD
			Xamarin.Calabash.Start();
			//Mapping StyleId to iOS Labels
			Forms.ViewInitialized += (object sender, ViewInitializedEventArgs e) =>
			{
				if (null != e.View.StyleId)
				{
					e.NativeView.AccessibilityIdentifier = e.View.StyleId;
				}
			};
            #endif

			LoadApplication(new App());

			return base.FinishedLaunching(app, options);
		}
	}
}
