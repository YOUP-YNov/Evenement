using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Evenement.ExpositionAPI.Models
{
    public class EventLocationFront
    {
        /// <summary>
        /// Assigne ou récupère l'id de l'adresse de l'évenement
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Assigne ou récupère le nom de la ville
        /// </summary>
        public String Ville { get; set; }

        /// <summary>
        /// Assigne ou récupère le code postale de l'adresse
        /// </summary>
        public String CodePostale { get; set; }

        /// <summary>
        /// Assigne ou récupère l'adresse
        /// </summary>
        public String Adresse { get; set; }

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
        public String Pays { get; set; }

        /// <summary>
        /// Assigne ou récupère le nom choisi par l'utilisateur
        /// </summary>
        public String Nom { get; set; }
    }
}
