using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace ITLab_Mobile.Api.Models.Helpers
{
    public static class DurationConverter
    {
        public static string GetDuration(DateTime begin, DateTime end, bool isShift = false)
        {
            var dif = end - begin;
            if (dif.TotalSeconds <= 60)
            {
                string toReturn = "Несколько секунд";
                if (!isShift)
                    return toReturn;
                else
                    return $"{begin.ToString("ddd, dd.MM.yyyy HH:mm", CultureInfo.CreateSpecificCulture("ru-RU"))} - " +
                        $"{end.ToString("HH:mm")} ({toReturn.ToLower()})";
            }

            if (dif.TotalMinutes <= 60)
            {
                string toReturn = "Меньше часа";
                if (!isShift)
                    return toReturn;
                else
                    return $"{begin.ToString("ddd, dd.MM.yyyy HH:mm", CultureInfo.CreateSpecificCulture("ru-RU"))} - " +
                        $"{end.ToString("HH:mm")} ({toReturn.ToLower()})";
            }

            if (dif.TotalHours <= 24)
            {
                int hours = Convert.ToInt32(dif.TotalHours);
                if (hours >= 5 && hours <= 20)
                {
                    if (!isShift)
                        return $"{hours} часов";
                    else
                        return $"{begin.ToString("ddd, dd.MM.yyyy HH:mm", CultureInfo.CreateSpecificCulture("ru-RU"))} - " +
                        $"{end.ToString("HH:mm")} ({hours} часов)";
                }

                if (hours == 1 || hours == 21)
                {
                    if (!isShift)
                        return $"{hours} час";
                    else
                        return $"{begin.ToString("ddd, dd.MM.yyyy HH:mm", CultureInfo.CreateSpecificCulture("ru-RU"))} - " +
                        $"{end.ToString("HH:mm")} ({hours} час)";
                }

                if (!isShift)
                    return $"{hours} часа";
                else
                    return $"{begin.ToString("ddd, dd.MM.yyyy HH:mm", CultureInfo.CreateSpecificCulture("ru-RU"))} - " +
                        $"{end.ToString("HH:mm")} ({hours} часа)";
            }

            int daysInt = Convert.ToInt32(dif.TotalDays);
            if (daysInt >= 5 && daysInt <= 20)
            {
                if (!isShift)
                    return $"{daysInt} дней";
                else
                    return $"{begin.ToString("ddd, dd.MM.yyyy HH:mm", CultureInfo.CreateSpecificCulture("ru-RU"))} - " +
                        $"{end.ToString("HH:mm")} ({daysInt} дней)";
            }

            string days = daysInt.ToString();

            if (days.EndsWith("1"))
            {
                if (!isShift)
                    return $"{daysInt} день";
                else
                    return $"{begin.ToString("ddd, dd.MM.yyyy HH:mm", CultureInfo.CreateSpecificCulture("ru-RU"))} - " +
                        $"{end.ToString("HH:mm")} ({daysInt} день)";
            }

            if (days.EndsWith("2") || days.EndsWith("3") || days.EndsWith("4"))
            {
                if (!isShift)
                    return $"{daysInt} дня";
                else
                    return $"{begin.ToString("ddd, dd.MM.yyyy HH:mm", CultureInfo.CreateSpecificCulture("ru-RU"))} - " +
                        $"{end.ToString("HH:mm")} ({daysInt} дня)";
            }

            if (!isShift)
                return $"{daysInt} дней";
            else
                return $"{begin.ToString("ddd, dd.MM.yyyy HH:mm", CultureInfo.CreateSpecificCulture("ru-RU"))} - " +
                        $"{end.ToString("HH:mm")} ({daysInt} дней)";
        }
    }
}
