using Models.PublicAPI.Responses.Event;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace ITLab_Mobile.Api.Models.Extensions.Events
{
    public class PlaceViewExtended : PlaceView
    {
        public string PlaceCount { get; set; }
        public bool IsDescription
            => !string.IsNullOrEmpty(Description);

        public Command WishCommand { get; }

        public PlaceViewExtended()
        {
            WishCommand = new Command(() =>
            {
                MessagingCenter.Send<PlaceViewExtended>(this, "wish");
            });
        }

        public List<UserAndEventRoleExtended> Users
        {
            get
            {
                var list = new List<UserAndEventRoleExtended>();
                foreach(var user in Participants)
                {
                    list.Add(new UserAndEventRoleExtended
                    {
                        User = user.User,
                        EventRole = user.EventRole,
                        CreationTime = user.CreationTime,
                        DoneTime = user.DoneTime,
                        PlaceRole = "Участник"
                    });
                }
                foreach (var user in Invited)
                {
                    list.Add(new UserAndEventRoleExtended
                    {
                        User = user.User,
                        EventRole = user.EventRole,
                        CreationTime = user.CreationTime,
                        DoneTime = user.DoneTime,
                        PlaceRole = "Приглашённый"
                    });
                }
                foreach (var user in Wishers)
                {
                    list.Add(new UserAndEventRoleExtended
                    {
                        User = user.User,
                        EventRole = user.EventRole,
                        CreationTime = user.CreationTime,
                        DoneTime = user.DoneTime,
                        PlaceRole = "Желающий"
                    });
                }
                foreach (var user in Unknowns)
                {
                    list.Add(new UserAndEventRoleExtended
                    {
                        User = user.User,
                        EventRole = user.EventRole,
                        CreationTime = user.CreationTime,
                        DoneTime = user.DoneTime,
                        PlaceRole = "Неизвестный"
                    });
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
