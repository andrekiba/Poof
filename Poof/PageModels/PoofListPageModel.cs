using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreshMvvm;
using Poof.Services;

namespace Poof.PageModels
{
    public class PoofListPageModel : BasePageModel
    {
        private IAzureService azureService;

        public PoofListPageModel(IAzureService azureService)
        {
            this.azureService = azureService;
        }
    }
}
