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

			var tint = UIColor.FromRGB(241, 196, 15); //#F1C40F giallo
			var barTint = UIColor.FromRGB(231, 76, 60); //#E74C3C rosso
			var back = UIColor.FromRGB(243, 206, 58); //F3CE3A giallo back
			var accent = UIColor.FromRGB(52, 152, 219); //#3498DB azzurro

			//navigation bar

			UINavigationBar.Appearance.BarTintColor = barTint; //navigation bar background
			UINavigationBar.Appearance.TintColor = tint; //navigation bar tint
			UINavigationBar.Appearance.SetTitleTextAttributes(new UITextAttributes
			{
				Font = UIFont.FromName("MarkerFelt-Thin", 18f),
				TextColor = tint
			});
			UINavigationBar.Appearance.TitleTextAttributes = new UIStringAttributes
			{
			    Font = UIFont.FromName("MarkerFelt-Thin", 18f),
			    ForegroundColor = tint
			};

			//tab bar

			UITabBar.Appearance.TintColor = tint;
			UITabBar.Appearance.BarTintColor = barTint;
			UITabBar.Appearance.SelectedImageTintColor = tint;
			UITabBar.Appearance.BackgroundColor = barTint;

			UITabBarItem.Appearance.SetTitleTextAttributes(new UITextAttributes
			{
				Font = UIFont.FromName("MarkerFelt-Thin", 12f),
				TextColor = UIColor.White
				             
			}, UIControlState.Normal);
			UITabBarItem.Appearance.SetTitleTextAttributes(new UITextAttributes
			{
				Font = UIFont.FromName("MarkerFelt-Thin", 12f),
				TextColor = tint
			}, UIControlState.Selected);

			//bar buttons

			UIBarButtonItem.Appearance.TintColor = tint; //Tint color of button items
		    UIBarButtonItem.Appearance.SetTitleTextAttributes(new UITextAttributes
		    {
		     	Font = UIFont.FromName("MarkerFelt-Thin", 18f),
		     	TextColor = tint
		    }, UIControlState.Normal);

			//switch

			UISwitch.Appearance.OnTintColor = barTint;
			UISwitch.Appearance.ThumbTintColor = accent;
			UISwitch.Appearance.TintColor = barTint;

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
