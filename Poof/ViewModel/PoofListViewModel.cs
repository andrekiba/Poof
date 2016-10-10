using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreshMvvm;
using Poof.Services;

namespace Poof.ViewModel
{
    public class PoofListViewModel : BaseViewModel
    {
        private IAzureService azureService;

        public PoofListViewModel(IAzureService azureService)
        {
            this.azureService = azureService;
        }
    }
}
