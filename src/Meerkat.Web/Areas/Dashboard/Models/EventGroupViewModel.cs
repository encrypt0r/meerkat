using Meerkat.Web.Models;
using System;
using System.Collections.Generic;

namespace Meerkat.Web.Areas.Dashboard.Models
{
    public class EventGroupViewModel
    {
        public EventGroupViewModel(EventGroup model, Dictionary<long, int> hits)
        {
            Name = model.LastSeen.Name;
            Message = model.LastSeen.Message;
            Level = Enum.GetName(typeof(EventLevel), model.LastSeen.Level);
            Type = model.LastSeen.Type;
            Date = FormatDate(model.LastSeen.Date, DateTime.UtcNow);
            Hits = hits[model.Id];
        }

        public string Name { get; }
        public string Message { get; }
        public string Level { get; }
        public string Type { get; }
        public string Date { get; }
        public int Hits { get; set; }

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
    }
}
