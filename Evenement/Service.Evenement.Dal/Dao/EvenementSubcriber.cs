﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Evenement.Dal.Dao
{
    public class EvenementSubcriber
    {
        public long ParticipationId { get; set; }

        public long EvenementId { get; set; }

        public long UtilisateurId { get; set; }

        public DateTime DateInscription { get; set; }

        public DateTime DateAnnulation { get; set; }

        public bool Annulation { get; set; }
    }
}
