using Meerkat.Core.Models;
using Meerkat.Web.Models;
using System;

namespace Meerkat.Web.Areas.Dashboard.Models
{
    public class EventViewModel
    {
        public EventViewModel(Event model)
        {
            Name = model.Name;
            Message = model.Message;
            Level = Enum.GetName(typeof(EventLevel), model.Level);
            Type = model.Type;
            Date = FormatDate(model.Date, DateTime.UtcNow);
        }

        public string Name { get; }
        public string Message { get; }
        public string Level { get; }
        public string Type { get; }
        public string Date { get; }

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
