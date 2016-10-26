using FreshMvvm;
using PropertyChanged;

namespace Poof.PageModels
{
    [ImplementPropertyChanged]
    public class BasePageModel : FreshBasePageModel
    {
        public bool IsBusy { get; set; }

        public bool IsNotBusy => !IsBusy;
    }
}
