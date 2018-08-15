using Meerkat.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Meerkat.Web.Areas.Dashboard.Dtos
{
    public class EventGroupDto
    {
        public EventGroupDto(EventGroup model, Dictionary<long, int> hits)
        {
            Id = model.Id;
            Message = model.LastSeen.Message;
            Level = Enum.GetName(typeof(EventLevel), model.LastSeen.Level);
            Type = model.LastSeen.Type;
            Date = FormatDate(model.LastSeen.Date, DateTime.UtcNow);
            Hits = hits[model.Id];
            RootCause = model.LastSeen.RootCause;
            Age = FormatAge(model.LastSeen.Date, DateTime.UtcNow);
            Module = model.LastSeen.Module;
        }

        public long Id { get; set; }
        public string Message { get; }
        public string Age { get; set; }
        public string Level { get; }
        public string Type { get; }
        public string Date { get; }
        public int Hits { get; }
        public string Module { get; set; }
        public string RootCause { get; }

        private string FormatDate(DateTime date, DateTime now)
        {
            var interval = now - date;

            if (interval < TimeSpan.Zero)
            {
                return $"{date:MMMM} {date.Day} at {date:hh:mm tt}";
            }
            if (interval.TotalSeconds <= 5)
            {
                return "Just now";
            }
            else if (interval.TotalSeconds < 60)
            {
                return "A few seconds ago";
            }
            else if (interval.TotalMinutes < 60)
            {
                return $"{interval.TotalMinutes:N0} minutes ago";
            }
            else if (interval.TotalHours < 24)
            {
                return $"{interval.TotalHours:N0} hours ago";
            }
            else if (interval.TotalHours < 48)
            {
                return $"Yesterday at {date:hh:mm tt}";
            }
            else
            {
                return $"{date:MMMM} {date.Day} at {date:hh:mm tt}";
            }
        }

        private string FormatAge(DateTime date, DateTime now)
        {
            var interval = now - date;

            if (interval < TimeSpan.Zero)
            {
                return "From the future!";
            }
            else if (interval.TotalDays < 1)
            {
                return "Less than a day old.";
            }
            else if (interval.TotalDays < 90)
            {
                return $"{Math.Round(interval.TotalDays)} days old.";
            }
            else if (interval.TotalDays < 365)
            {
                return $"About {Math.Floor(interval.TotalDays / 30)} months old.";
            }
            else if (Math.Floor(interval.TotalDays / 365) == 1)
            {
                return $"About one year old.";
            }
            else
            {
                return $"About {Math.Floor(interval.TotalDays / 365)} years old.";
            }
        }
    }
}
