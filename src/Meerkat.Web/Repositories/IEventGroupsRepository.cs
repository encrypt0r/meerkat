using System.Collections.Generic;
using System.Threading.Tasks;
using Meerkat.Web.Models;

namespace Meerkat.Web.Repositories
{
    public interface IEventGroupsRepository : IRepository<EventGroup, long>
    {
        Task<EventGroup> GetByFingerprint(string fingerprint);
        Task<ICollection<EventGroup>> GetLatestNAsync(int n);
        Task<ICollection<EventGroup>> GetLatestAsync();
        Task<Dictionary<long, int>> GetHitsAsync(IEnumerable<long> ids);
        Task<Dictionary<long, int>> GetNumberOfAffectedUsersAsync(IEnumerable<long> ids);
    }
}