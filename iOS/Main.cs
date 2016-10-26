using UIKit;
using Xamarin;

namespace Poof.iOS
{
	public class Application
	{
		// This is the main entry point of the application.
		static void Main(string[] args)
		{
            // if you want to use a different Application Delegate class from "AppDelegate"
            // you can specify it here.

            #region Insights

            Insights.Initialize("416420e0a779226dd8a0b72004d24af465e6a844");
			Insights.ForceDataTransmission = true;

            #endregion

            UIApplication.Main(args, null, "AppDelegate");
		}
	}
}
