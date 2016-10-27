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
			//TabBar.Items.ToList().ForEach(t =>
			//{
			//	t.Image = t.Image.ImageWithRenderingMode(UIImageRenderingMode.AlwaysOriginal);

			//});
			var tabs = Element as TabbedPage;
			if (tabs != null)
			{
				for (int i = 0; i < TabBar.Items.Length; i++)
				{
					var tabItem = TabBar.Items[i];

					UpdateItem(tabItem, tabs.Children[i].Icon);
					tabItem.Image = tabItem.Image.ImageWithRenderingMode(UIImageRenderingMode.AlwaysOriginal);
				}
			}
		}

		void UpdateItem(UITabBarItem item, string icon)
		{
			if (item == null)
				return;
			try
			{
				icon = icon.Replace(".png", "Sel.png");
				if (item?.SelectedImage?.AccessibilityIdentifier == icon)
					return;
				item.SelectedImage = UIImage.FromBundle(icon);
				item.SelectedImage.AccessibilityIdentifier = icon;
			}
			catch (Exception ex)
			{
				Console.WriteLine("Unable to set selected icon: " + ex);
			}

		}

	}
}
