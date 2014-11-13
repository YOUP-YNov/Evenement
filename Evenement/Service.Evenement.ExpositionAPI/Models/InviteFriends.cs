using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Service.Evenement.ExpositionAPI.Models
{
    public class InviteFriends
    {
        /// <summary>
        /// id de l'évènement
        /// </summary>
        public long idEvent;

        /// <summary>
        /// id de l'utilisateur qui invite ses amis
        /// </summary>
        public long idUser;

        /// <summary>
        /// ids amis à inviter
        /// </summary>
        public List<long> idFriends;
    }
}