using System.Collections.Generic;
using System.Threading.Tasks;
using Meerkat.Web.Models;

namespace Meerkat.Web.Services
{
    public interface IEventStatisticsService
    {
        void DataModified();
        Task<List<EventGroupSummary>> GetEventGroupSummariesAsync();
    }
}