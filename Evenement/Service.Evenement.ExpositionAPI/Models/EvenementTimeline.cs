using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Service.Evenement.ExpositionAPI.Models
{
    public class EvenementTimelineFront
    {
        public long Evenement_id { get; set; }
        public string DescriptionEvenement { get; set; }
        public int Organisateur_id { get; set; }
        public string OrganisateurPseudo { get; set; }
        public string OrganisateurImageUrl { get; set; }
        public EventLocationFront Adresse { get; set; }
        public string Categorie_Libelle { get; set; }
        public DateTime DateEvenement { get; set; }
        public string TitreEvenement { get; set; }
        public int MaximumParticipant { get; set; }
        public string Statut { get; set; }
        public int Prix { get; set; }
        public bool Premium { get; set; }
        public DateTime DateMiseEnAvant { get; set; }
        public string Etat { get; set; }
        public string ImageUrl { get; set; }
        public int Topic_id { get; set; }
    }
}