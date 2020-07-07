using Models.PublicAPI.Responses.Event;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace ITLab_Mobile.Api.Models.Extensions.Events
{
    public class ShiftViewExtended : ShiftView
    {
        public string BeginEndTime
        {
            get
            {
                var dif = EndTime - BeginTime;
                if (dif.TotalDays > 1)
                {
                    string rusDay = "дней";
                    string days = Convert.ToInt32(dif.TotalDays).ToString();

                    if (days.EndsWith("1"))
                    {
                        rusDay = "день";
                    }

                    if (days.EndsWith("2") || days.EndsWith("3") || days.EndsWith("4"))
                    {
                        rusDay = "дня";
                    }

                    return $"{BeginTime.ToString("ddd, dd.MM.yyyy HH:mm", CultureInfo.CreateSpecificCulture("ru-RU"))} - {EndTime.ToString("ddd, dd.MM.yyyy HH:mm", CultureInfo.CreateSpecificCulture("ru-RU"))} ({Convert.ToInt32(dif.TotalDays)} {rusDay})";
                }

                return $"{BeginTime.ToString("ddd, dd.MM.yyyy HH:mm", CultureInfo.CreateSpecificCulture("ru-RU"))} - {EndTime.ToString("HH:mm")}";
            }
        }
    }
}
