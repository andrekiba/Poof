using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreshMvvm;
using Poof.Services;

namespace Poof.PageModels
{
    public class PoofPageModel : BasePageModel
    {
        private IAzureService azureService;

        public PoofPageModel(IAzureService azureService)
        {
            this.azureService = azureService;
        }
    }
}
