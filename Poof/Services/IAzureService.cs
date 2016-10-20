using System.Collections.Generic;
using System.Threading.Tasks;

namespace Poof.Services
{
    public interface IAzureService
    {
        Task<IEnumerable<Model.Poof>> GetPoofs(bool sync);

        Task<Model.Poof> AddPoof(bool justified, string comment, string userId, bool sync);

        Task DeletePoof(Model.Poof poof, bool sync);

        Task<bool> LoginAsync();
    }
}
