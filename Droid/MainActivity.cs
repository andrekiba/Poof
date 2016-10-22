using System;
using Android.App;
using Android.Content.PM;
using Android.OS;
using Poof.Services;
using System.IO;
using ImageCircle.Forms.Plugin.Droid;
using Xamarin;
using Xamarin.Forms;

namespace Poof.Droid
{
	[Activity(Label = "Poof", Icon = "@drawable/poofBigRosa", Theme = "@style/MyTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
	public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
	{
		protected override void OnCreate(Bundle bundle)
		{
			TabLayoutResource = Resource.Layout.Tabbar;
			ToolbarResource = Resource.Layout.Toolbar;

			base.OnCreate(bundle);

            Insights.Initialize("416420e0a779226dd8a0b72004d24af465e6a844", this);
			Insights.ForceDataTransmission = true;

			Microsoft.WindowsAzure.MobileServices.CurrentPlatform.Init();

			#if ENABLE_TEST_CLOUD

			Forms.ViewInitialized += (object sender, ViewInitializedEventArgs e) =>
			{
				if (string.IsNullOrEmpty(e.View.StyleId))
				{
					e.NativeView.ContentDescription = e.View.StyleId;
				}
			};
			#endif

			//sqlite db
			AzureService.DbPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), AzureService.DbPath);
			if (!File.Exists(AzureService.DbPath))
			{
				File.Create(AzureService.DbPath).Dispose();
			}

			global::Xamarin.Forms.Forms.Init(this, bundle);
			ImageCircleRenderer.Init();

			LoadApplication(new App());
		}
	}
}
