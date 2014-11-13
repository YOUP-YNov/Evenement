using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Evenement.Dal.Dao
{
    public class EventLocationDao
    {
        /// <summary>
        /// Assigne ou récupère l'id de l'adresse de l'évenement
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Assigne ou récupère le nom de la ville
        /// </summary>
        public StringBuilder Ville { get; set; }

        /// <summary>
        /// Assigne ou récupère le code postale de l'adresse
        /// </summary>
        public StringBuilder CodePostale { get; set; }

        /// <summary>
        /// Assigne ou récupère l'adresse
        /// </summary>
        public StringBuilder Adresse { get; set; }

        /// <summary>
        /// Assigne ou récupère la longitude du rendez vous
        /// </summary>
        public decimal Longitude { get; set; }

        /// <summary>
        /// Assigne ou récupère la latitude du rendez vous
        /// </summary>
        public decimal Latitude { get; set; }

        /// <summary>
        /// Assigne ou récupère le nom du pays
        /// </summary>
        public StringBuilder Pays { get; set; }

        /// <summary>
        /// Assigne ou récupère le nom choisi par l'utilisateur
        /// </summary>
        public StringBuilder Nom { get; set; }
    }
}
