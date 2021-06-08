using System;
using Yamaanco.Application.Enums;
using Yamaanco.Application.Interfaces;
using Yamaanco.Application.Structs;

namespace Yamaanco.Infrastructure.Shared.ManageDateTime
{
    public class ElapsedTime : IElapsedTime
    {
        public ElapsedTimeValue Calculate(DateTime date)
        {
            TimeSpan different = new MachineDateTime().Now - date;
            if (different.TotalSeconds == 1)
            {
                return new ElapsedTimeValue(
                    ElapsedTimeType.OneSecond,
                    (int)different.TotalSeconds,
                     string.Format(" {0} second ago",
                     (int)different.TotalSeconds));
            }
            else if (different.TotalSeconds <= 59)
            {
                return new ElapsedTimeValue(ElapsedTimeType.MoreThanOneSecond,
                  (int)different.TotalSeconds,
                   string.Format(" {0} seconds ago",
                   (int)different.TotalSeconds));
            }
            else if (different.TotalMinutes == 1)
            {
                return new ElapsedTimeValue(ElapsedTimeType.OneMinute,
               (int)different.TotalMinutes,
                string.Format(" {0} minute ago",
                (int)different.TotalMinutes));
            }
            else if (different.TotalMinutes <= 59)
            {
                return new ElapsedTimeValue(ElapsedTimeType.MoreThanOneSecond,
          (int)different.TotalMinutes,
           string.Format(" {0} minutes ago",
           (int)different.TotalMinutes));
            }
            else if (different.TotalHours == 1)
            {
                return new ElapsedTimeValue(ElapsedTimeType.OneHour,
         (int)different.TotalHours,
          string.Format(" {0} hour ago",
          (int)different.TotalHours));
            }
            else if (different.TotalHours <= 23)
            {
                return new ElapsedTimeValue(ElapsedTimeType.MoreThanOneHour,
        (int)different.TotalHours,
         string.Format(" {0} hours ago",
         (int)different.TotalHours));
            }
            else if (different.TotalDays == 1)
            {
                return new ElapsedTimeValue(ElapsedTimeType.OneDay,
       (int)different.TotalDays,
        string.Format(" {0} Day ago",
        (int)different.TotalDays));
            }
            else if (different.TotalDays <= 30)
            {
                return new ElapsedTimeValue(ElapsedTimeType.MoreThanOneDay,
         (int)different.TotalDays,
          string.Format(" {0} Days ago",
          (int)different.TotalDays));
            }
            else
            {
                return new ElapsedTimeValue(ElapsedTimeType.DateOnly,
           (int)different.TotalDays,
           date.ToShortDateString());
            }
        }
    }
}