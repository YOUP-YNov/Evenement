using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Service.Evenement.ExpositionAPI.Models
{
    public class InviteFriends
    {
        public long idEvent;
        public long idUser;
        public List<long> idFriends;
    }
}