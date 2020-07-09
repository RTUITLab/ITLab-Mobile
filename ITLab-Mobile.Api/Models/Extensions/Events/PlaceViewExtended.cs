using Models.PublicAPI.Responses.Event;
using System;
using System.Collections.Generic;
using System.Text;

namespace ITLab_Mobile.Api.Models.Extensions.Events
{
    public class PlaceViewExtended : PlaceView
    {
        public string PlaceCount { get; set; }
        public bool IsDescription
            => !string.IsNullOrEmpty(Description);

        public List<UserAndEventRole> Users
        {
            get
            {
                var list = new List<UserAndEventRole>();
                foreach(var user in Participants)
                {
                    list.Add(user);
                }
                foreach (var user in Invited)
                {
                    list.Add(user);
                }
                foreach (var user in Wishers)
                {
                    list.Add(user);
                }
                foreach (var user in Unknowns)
                {
                    list.Add(user);
                }
                return list;
            }
        }

        public string ParticipantsCount
        {
            get
            {
                if (TargetParticipantsCount <= 0)
                    return "Участники не требуются";

                if (Users.Count == 0)
                {
                    if (TargetParticipantsCount >= 5 && TargetParticipantsCount <= 20)
                    {
                        return $"Нужно {TargetParticipantsCount} участников";
                    }

                    string target = TargetParticipantsCount.ToString();
                    if (target.EndsWith("1"))
                    {
                        return $"Нужен {TargetParticipantsCount} участник";
                    }

                    if (target.EndsWith("2") || target.EndsWith("3") || target.EndsWith("4"))
                    {
                        return $"Нужно {TargetParticipantsCount} участника";
                    }

                    return $"Нужно {TargetParticipantsCount} участников";
                }

                return $"Участников: {Users.Count} из {TargetParticipantsCount}";
            }
        }
    }
}
