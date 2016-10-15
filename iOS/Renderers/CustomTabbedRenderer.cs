using System;
using System.Linq;
using Poof.iOS.Renderers;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(TabbedPage), typeof(CustomTabbedRenderer))]
namespace Poof.iOS.Renderers
{
	public class CustomTabbedRenderer : TabbedRenderer
	{
		public CustomTabbedRenderer()
		{
			TabBar.Translucent = false;
			TabBar.Opaque = true;
		}

		public override void ViewWillAppear(bool animated)
		{
			base.ViewWillAppear(animated);
			TabBar.Items.ToList().ForEach(t =>
			{
				t.Image = t.Image.ImageWithRenderingMode(UIImageRenderingMode.AlwaysOriginal);
			});
		}

	}
}
