using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace Poof.Pages
{
    public partial class PoofPage : ContentPage
    {
        public PoofPage()
        {
            InitializeComponent();
        }

        private async void PoofTapped(object sender, EventArgs e)
        {
            await PoofImage.ScaleTo(1.3, 50, Easing.CubicOut);
            await PoofImage.ScaleTo(1, 50, Easing.CubicIn);
        }
    }
}
