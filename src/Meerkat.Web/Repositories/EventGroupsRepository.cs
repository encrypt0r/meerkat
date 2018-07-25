using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Meerkat.Web.Data;
using Meerkat.Web.Models;
using Microsoft.EntityFrameworkCore;

namespace Meerkat.Web.Repositories
{
    public class EventGroupsRepository : IEventGroupsRepository
    {
        private readonly ApplicationDbContext _context;

        public EventGroupsRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Add(EventGroup item)
        {
            _context.Add(item);
        }

        public Task<EventGroup> Get(long id)
        {
            return _context.EventGroups.Include(g => g.FirstSeen)
                                       .Include(g => g.LastSeen)
                                       .FirstOrDefaultAsync(g => g.Id == id);
        }

        public async Task<ICollection<EventGroup>> Get(Expression<Func<EventGroup, bool>> predicate)
        {
            var list = await _context.EventGroups.Include(g => g.FirstSeen)
                                       .Include(g => g.LastSeen)
                                       .Where(predicate)
                                       .ToListAsync();

            return list;
        }

        public async Task<ICollection<EventGroup>> GetAll()
        {
            var list = await _context.EventGroups.Include(g => g.FirstSeen)
                                       .Include(g => g.LastSeen)
                                       .ToListAsync();

            return list;
        }

        public Task<EventGroup> GetByFingerprint(string fingerprint)
        {
            return _context.EventGroups.Include(g => g.FirstSeen)
                                      .Include(g => g.LastSeen)
                                      .FirstOrDefaultAsync(g => g.Fingerprint == fingerprint);
        }

        public async Task<Dictionary<long, int>> GetHits(IEnumerable<long> enumerable)
        {
            var list = await _context.EventGroups.Where(g => enumerable.Contains(g.Id))
                                     .Select(g => new { g.Id, g.Events.Count })
                                     .ToListAsync();

            return list.ToDictionary(i => i.Id, i => i.Count);
        }

        public async Task<ICollection<EventGroup>> GetLatestN(int n)
        {
            var list = await _context.EventGroups.Include(g => g.FirstSeen)
                                       .Include(g => g.LastSeen)
                                       .OrderByDescending(g => g.LastSeen.Date)
                                       .Take(n)
                                       .ToListAsync();
            return list;
        }

        public void Remove(EventGroup item)
        {
            _context.Remove(item);
        }
    }
}
