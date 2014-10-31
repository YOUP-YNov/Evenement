using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Service.Evenement.ExpositionAPI.Models
{
    public class EvenementTimelineFront
    {
        public long Evenement_id { get; set; }
        public long LieuEvenement_id { get; set; }
        public long Categorie_id { get; set; }
        public DateTime DateEvenement { get; set; }
        public string TitreEvenement { get; set; }
        public int MaximumParticipant { get; set; }
        public string Statut { get; set; }
        public int Prix { get; set; }
        public bool Premium { get; set; }
        public DateTime DateMiseEnAvant { get; set; }
        public long Etat_id { get; set; }

        public long EvenementPhoto_id { get; set; }
        public string Adresse { get; set; }

    }
}