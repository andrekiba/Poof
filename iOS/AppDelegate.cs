using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using ImageCircle.Forms.Plugin.iOS;
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

			var tint = UIColor.FromRGB(65, 187, 195); //#41bbc3 acqua
			var barTint = UIColor.FromRGB(230, 24, 115); //#e61873 magenta
			var back = UIColor.FromRGB(255, 255, 255); //ffffff bianco
			var accent = UIColor.FromRGB(18, 113, 121); //#127179 verdone
            var jet = UIColor.FromRGB(107, 109, 118); //#6b6d76 grigio

            //navigation bar

            UINavigationBar.Appearance.BarTintColor = barTint; //navigation bar background
			UINavigationBar.Appearance.TintColor = back; //navigation bar tint
			UINavigationBar.Appearance.SetTitleTextAttributes(new UITextAttributes
			{
				//Font = UIFont.FromName("MarkerFelt-Thin", 18f),
				TextColor = back
			});
			UINavigationBar.Appearance.TitleTextAttributes = new UIStringAttributes
			{
			    //Font = UIFont.FromName("MarkerFelt-Thin", 18f),
			    ForegroundColor = back
			};

			//tab bar

			UITabBar.Appearance.TintColor = back;
			UITabBar.Appearance.BarTintColor = barTint;
			//UITabBar.Appearance.SelectedImageTintColor = tint;
			UITabBar.Appearance.BackgroundColor = barTint;

			UITabBarItem.Appearance.SetTitleTextAttributes(new UITextAttributes
			{
				//Font = UIFont.FromName("MarkerFelt-Thin", 12f),
				TextColor = back
				             
			}, UIControlState.Normal);
			UITabBarItem.Appearance.SetTitleTextAttributes(new UITextAttributes
			{
				//Font = UIFont.FromName("MarkerFelt-Thin", 12f),
				TextColor = back
			}, UIControlState.Selected);

			//bar buttons

			UIBarButtonItem.Appearance.TintColor = back; //Tint color of button items
		    UIBarButtonItem.Appearance.SetTitleTextAttributes(new UITextAttributes
		    {
		     	//Font = UIFont.FromName("MarkerFelt-Thin", 18f),
		     	TextColor = barTint
		    }, UIControlState.Normal);

			//switch

			UISwitch.Appearance.OnTintColor = barTint;
			UISwitch.Appearance.ThumbTintColor = tint;
			UISwitch.Appearance.TintColor = tint;

            #endregion

            Microsoft.WindowsAzure.MobileServices.CurrentPlatform.Init();
			Forms.Init();
			ImageCircleRenderer.Init();

			#if ENABLE_TEST_CLOUD
			Xamarin.Calabash.Start();
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
