using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Service.Evenement.ExpositionAPI.Models
{
    public class Subscriber
    {
        /// <summary>
        /// Récupère ou assigne l'id de l'utilisateur inscrit
        /// </summary>
        public long UtilisateurId { get; set; }

        /// <summary>
        /// Récupère le pseudo de l'utilisateur inscrit
        /// </summary>
        public string Pseudo { get; set; }

        /// <summary>
        /// Récûpère l'image de profil de l'utilisateur inscrit
        /// </summary>
        public string ImageUrl { get; set; }

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