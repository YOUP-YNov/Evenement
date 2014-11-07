using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Evenement.ExpositionAPI.Models
{
    public class EventLocationFront
    {
        public long Id { get; set; }

        /// <summary>
        /// Assigne ou récupère le nom de la ville
        /// </summary>
        public string Ville { get; set; }

        /// <summary>
        /// Assigne ou récupère le code postale de l'adresse
        /// </summary>
        public string CodePostale { get; set; }

        /// <summary>
        /// Assigne ou récupère l'adresse
        /// </summary>
        public string Adresse { get; set; }

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
        public string Pays { get; set; }

        /// <summary>
        /// Assigne ou récupère le nom choisi par l'utilisateur
        /// </summary>
        public string Nom { get; set; }

        public EventLocationFront(decimal latitude, decimal longitude, string adresse, string pays, string code_postale, string ville)
        {
            this.Latitude = latitude;
            this.Longitude = longitude;
            this.Adresse = adresse;
            this.Pays = pays;
            this.CodePostale = code_postale;
            this.Ville = ville;
        }

        public EventLocationFront()
        {

        }
    }
}
