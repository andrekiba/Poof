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
        //private static readonly UIColor TintColor = UIColor.FromRGB(247, 247, 247);
        private static readonly UIColor BarTintColor = UIColor.FromRGBA(145, 202, 71, 255);

        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
		{
            //UINavigationBar.Appearance.BarTintColor = BarTintColor;
            //UINavigationBar.Appearance.TintColor = UIColor.White;

            //UITabBar.Appearance.BarTintColor = BarTintColor;
            //UITabBar.Appearance.TintColor = UIColor.White;
            //UITabBar.Appearance.SelectedImageTintColor = UIColor.Green;

            //UIBarButtonItem.Appearance.TintColor = TintColor;
            //UISwitch.Appearance.OnTintColor = TintColor;
            //UIAlertView.Appearance.TintColor = TintColor;



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
