using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Service.Evenement.ExpositionAPI.Models
{
    public class EvenementFront
    {
        public long Evenement_id { get; set; }
        public long Utilisateur_id { get; set; }
        public long LieuEvenement_id { get; set; }
        public long Categorie_id { get; set; }
        public DateTime DateEvenement { get; set; }
        public DateTime DateCreation { get; set; }
        public DateTime DateModification { get; set; }
        public DateTime DateFinInscription { get; set; }
        public string TitreEvenement { get; set; }
        public string DescriptionEvenement { get; set; }
        public int MinimumParticipant { get; set; }
        public int MaximumParticipant { get; set; }
        public string Statut { get; set; }
        public int Prix { get; set; }
        public bool Premium { get; set; }
        public DateTime DateMiseEnAvant { get; set; }
        public long Etat_id { get; set; }
    }
}