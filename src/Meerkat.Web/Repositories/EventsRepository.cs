using Meerkat.Web.Data;
using Meerkat.Web.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Meerkat.Web.Repositories
{
    public class EventsRepository : IRepository<Event, long>, IEventsRepository
    {
        private readonly ApplicationDbContext _context;

        public EventsRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Add(Event item)
        {
            _context.Add(item);
        }

        public Task<Event> Get(long id)
        {
            return _context.Events.Include(e => e.StackTrace).FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<ICollection<Event>> Get(Expression<Func<Event, bool>> predicate)
        {
            var list = await _context.Events.Include(e => e.StackTrace).Where(predicate).ToListAsync();
            return list;
        }

        public async Task<ICollection<Event>> GetAll()
        {
            var list = await _context.Events.Include(e => e.StackTrace).ToListAsync();
            return list;
        }

        public async Task<ICollection<Event>> GetLastNEvents(int n)
        {
            var list = await _context.Events.OrderByDescending(e => e.Date).Take(n).ToListAsync();
            return list;
        }

        public void Remove(Event item)
        {
            _context.Events.Remove(item);
        }
    }
}
