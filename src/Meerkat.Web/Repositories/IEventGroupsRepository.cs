using System.Collections.Generic;
using System.Threading.Tasks;
using Meerkat.Web.Models;

namespace Meerkat.Web.Repositories
{
    public interface IEventGroupsRepository : IRepository<EventGroup, long>
    {
        Task<EventGroup> GetByFingerprint(string fingerprint);
        Task<ICollection<EventGroup>> GetLatestN(int n);
        Task<Dictionary<long, int>> GetHits(IEnumerable<long> enumerable);
    }
}