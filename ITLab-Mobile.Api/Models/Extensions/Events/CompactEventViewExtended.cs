using Models.PublicAPI.Responses.Event;
using System;
using System.Drawing;

namespace ITLab_Mobile.Api.Models.Extensions.Events
{
    public class CompactEventViewExtended : CompactEventView
    {
        public Color BorderColor
            => Participating ? Color.FromArgb(40, 167, 69) : Color.FromArgb(0, 123, 255);

        public double ProgressToBar
            => TargetParticipantsCount <= 0 ? 1 : Convert.ToDouble(CurrentParticipantsCount) / TargetParticipantsCount;

        public string Duration
        {
            get
            {
                var dif = EndTime - BeginTime;
                if (dif.TotalSeconds <= 60)
                {
                    return "Несколько секунд";
                }

                if (dif.TotalMinutes <= 60)
                {
                    return "Меньше часа";
                }

                if (dif.TotalHours <= 24)
                {
                    int hours = Convert.ToInt32(dif.TotalHours);
                    if (hours >= 5 && hours <= 20)
                    {
                        return $"{hours} часов";
                    }

                    if (hours == 1 || hours == 21)
                    {
                        return $"{hours} час";
                    }

                    return $"{hours} часа";
                }

                int daysInt = Convert.ToInt32(dif.TotalDays);
                if (daysInt >= 5 && daysInt <= 20)
                {
                    return $"{daysInt} дней";
                }

                string days = daysInt.ToString();

                if (days.EndsWith("1"))
                {
                    return $"{daysInt} день";
                }

                if (days.EndsWith("2") || days.EndsWith("3") || days.EndsWith("4"))
                {
                    return $"{daysInt} дня";
                }

                return $"{daysInt} дней";
            }
        }
    }
}
