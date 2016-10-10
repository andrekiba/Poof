using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreshMvvm;
using Poof.Services;

namespace Poof.ViewModel
{
    public class PoofViewModel : BaseViewModel
    {
        private IAzureService azureService;

        public PoofViewModel(IAzureService azureService)
        {
            this.azureService = azureService;
        }
    }
}
