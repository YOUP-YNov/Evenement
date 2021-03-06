﻿using Service.Evenement.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Evenement.ExpositionAPI.Models
{
    public class EventStateFront
    {
        /// <summary>
        /// Assigne ou récupère le libelle de l'état de l'évenement
        /// </summary>
        public EventStateEnumFront Nom { get; set; }

        /// <summary>
        /// Assigne ou récupère l'id de l'état de l'évenement
        /// </summary>
        public long Id { get; set; }

        public EventStateFront(EventStateEnumFront name)
        {
            Nom = name;
            Id = (long)name;
        }

        public EventStateFront()
        {

        }
    }
}
