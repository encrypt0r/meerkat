using System.Threading.Tasks;
using Meerkat.Web.Repositories;

namespace Meerkat.Web.Data
{
    public class EfUnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public EfUnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            Events = new EventsRepository(context);
            EventGroups = new EventGroupsRepository(context);
        }

        public IEventsRepository Events { get; }
        public IEventGroupsRepository EventGroups { get; }

        public Task CompleteAsync()
        {
            return _context.SaveChangesAsync();
        }
    }
}
