using System.Threading.Tasks;
using Meerkat.Web.Models;

namespace Meerkat.Web.Repositories
{
    public interface IEventGroupsRepository : IRepository<EventGroup, long>
    {
        Task<EventGroup> GetByFingerprint(string fingerprint);
    }
}