using Service.Evenement.ExpositionAPI.Models;
using Service.Evenement.ExpositionAPI.Models.ModelsUpdate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Service.Evenement.ExpositionAPI.Controllers
{
    public class EvenementUpdate
    {
        /// <summary>
        /// Assigne ou récupère l'id de l'évenement.
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Assigne ou récupère la liste des HashTag associé à l'évenement
        /// </summary>
        //public IEnumerable<string> HashTag { get; set; }

        /// <summary>
        /// Assigne ou récupère la catégorie de l'évenement
        /// </summary>
        public CategorieUpdate Categorie { get; set; }

        /// <summary>
        /// Assigne ou récupère l'adresse de l'évenement
        /// </summary>
        public EventLocationFront EventAdresse { get; set; }

        /// <summary>
        /// Assigne ou récupère la Date à partir de laquelle les utilisateurs ne peuvent plus
        /// s'incrirent.
        /// </summary>
        public DateTime DateFinInscription { get; set; }

        /// <summary>
        /// Assigne ou récupère le titre de l'évenement
        /// </summary>
        public string TitreEvenement { get; set; }

        /// <summary>
        /// Assigne ou récupère la description de l'évenement
        /// </summary>
        public string DescriptionEvenement { get; set; }

        /// <summary>
        /// Assigne ou récupère le nombre de participant maximum à l'évenement
        /// </summary>
        public int MaximumParticipant { get; set; }

        /// <summary>
        /// Assigne ou récupère le nombre de participant minimum à l'évenement
        /// </summary>
        public int MinimumParticipant { get; set; }

        /// <summary>
        /// Assigne ou récupère le price a payé pour participé à l'évenement
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// Assigne ou récupère l'attestation que l'évenement est premium
        /// </summary>
        public bool Premium { get; set; }

        /// <summary>
        /// Assigne ou récupère la visibilité de l'évènement
        /// </summary>
        public bool Public { get; set; }

        /// <summary>
        /// Assigne ou récupère la Date à laquelle l'évenement a lieu.
        /// </summary>
        public DateTime DateEvenement { get; set; }

    }
}