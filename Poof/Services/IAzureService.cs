using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poof.Services
{
    public interface IAzureService
    {
        Task<IEnumerable<Model.Poof>> GetPoofs();

        Task<Model.Poof> AddPoof(bool justified, string comment, string userId);

        Task<bool> LoginAsync();
    }
}
