using Meerkat.Web.Models;
using Meerkat.Web.Repositories;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Meerkat.Web.Services
{
    public class EventStatisticsService : IEventStatisticsService
    {
        private readonly IEventGroupsRepository _repository;
        private readonly IMemoryCache _memoryCache;

        private const string LastModifiedDateKey = "EventStatisticsLastModifiedDate";
        private const string LastFetchDateKey = "EventStatisticsLastFetchDate";
        private const string EventGroupsKey = "EventStatiticsEventGroups";

        public EventStatisticsService(IEventGroupsRepository repository, IMemoryCache memoryCache)
        {
            _repository = repository;
            _memoryCache = memoryCache;
        }

        public async Task<List<EventGroupSummary>> GetEventGroupSummariesAsync()
        {
            _memoryCache.TryGetValue<DateTime?>(LastModifiedDateKey, out var lastModifiedDate);
            _memoryCache.TryGetValue<DateTime?>(LastFetchDateKey, out var lastFetchedDate);
            _memoryCache.TryGetValue<List<EventGroupSummary>>(EventGroupsKey, out var cachedEventGroups);

            if (cachedEventGroups == null || lastFetchedDate < lastModifiedDate)
            {
                var list = await FetchFreshData();

                _memoryCache.Set(EventGroupsKey, list);
                _memoryCache.Set(LastFetchDateKey, DateTime.UtcNow);

                return list;
            }
            else
            {
                return cachedEventGroups;
            }
        }

        private async Task<List<EventGroupSummary>> FetchFreshData()
        {
            var groups = await _repository.GetLatestAsync();
            var hits = await _repository.GetHitsAsync(groups.Select(g => g.Id));
            var users = await _repository.GetNumberOfAffectedUsersAsync(groups.Select(g => g.Id));

            return groups.Select(g => new EventGroupSummary(g, users[g.Id], hits[g.Id])).ToList();
        }

        public void DataModified()
        {
            _memoryCache.Set(LastModifiedDateKey, DateTime.UtcNow);
        }
    }
}
