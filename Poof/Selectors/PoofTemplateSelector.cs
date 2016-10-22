using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Poof.CustomCells;
using Poof.PageModels;
using Xamarin.Forms;

namespace Poof.Selectors
{
    internal class PoofTemplateSelector : DataTemplateSelector
    {
        private readonly DataTemplate justifiedDataTemplate;
		private readonly DataTemplate notJustifiedDataTemplate;

		public PoofTemplateSelector()
        {
            // Retain instances!
            justifiedDataTemplate = new DataTemplate(typeof(PoofJustifiedCell));
            notJustifiedDataTemplate = new DataTemplate(typeof(PoofNotJustifiedCell));
        }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            var poof = item as Model.Poof;
            if (poof == null)
                return null;
            var template = poof.Justified ? justifiedDataTemplate : notJustifiedDataTemplate;
			template.SetValue(BaseCell.ParentBindingContextProperty, (PoofListPageModel)container.BindingContext);
            return template;
        }
    }
}
