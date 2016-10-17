using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace Poof.Pages
{
    public partial class PoofListPage : ContentPage
    {
        public PoofListPage()
        {
            InitializeComponent();
            PoofsView.ItemTapped += (sender, e) =>
            {
                if (Device.OS == TargetPlatform.iOS || Device.OS == TargetPlatform.Android)
                    PoofsView.SelectedItem = null;
            };

            PoofsView.ItemSelected += (sender, e) =>
            {
                PoofsView.SelectedItem = null;
            };
        }
    }
}
