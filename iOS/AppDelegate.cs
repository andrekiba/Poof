using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using UIKit;
using Xamarin.Forms;

namespace Poof.iOS
{
	[Register("AppDelegate")]
	public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
	{
		public override bool FinishedLaunching(UIApplication app, NSDictionary options)
		{
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
