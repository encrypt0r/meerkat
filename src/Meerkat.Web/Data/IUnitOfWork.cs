using Meerkat.Web.Repositories;
using System.Threading.Tasks;

namespace Meerkat.Web.Data
{
    public interface IUnitOfWork
    {
        IEventsRepository Events { get; }
        IEventGroupsRepository EventGroups { get; }

        Task CompleteAsync();
    }
}
