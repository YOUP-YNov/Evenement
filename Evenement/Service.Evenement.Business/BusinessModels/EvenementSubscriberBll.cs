using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Evenement.Business.BusinessModels
{
    public class EvenementSubscriberBll
    {
        /// <summary>
        /// Récupère ou assigne l'id de la participation
        /// </summary>
        public long ParticipationId { get; set; }

        /// <summary>
        /// Récupère ou assigne l'id de l'évenement 
        /// </summary>
        public long EvenementId { get; set; }

        /// <summary>
        /// Récupère ou assigne l'id de l'utilisateur inscrit
        /// </summary>
        public long UtilisateurId { get; set; }

        /// <summary>
        /// Récupère ou assigne la date d'inscription de l'utilisateur
        /// </summary>
        public DateTime DateInscription { get; set; }

        /// <summary>
        /// Récupère ou assigne la date d'annulation de l'utilisateur
        /// </summary>
        public DateTime DateAnnulation { get; set; }

        /// <summary>
        /// Récupère ou assigne si oui ou non sa participation est annulé
        /// </summary>
        public bool Annulation { get; set; }
    }
}
