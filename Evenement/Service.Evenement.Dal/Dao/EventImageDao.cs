﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Evenement.Dal.Dao
{
    public class EventImageDao
    {
        /// <summary>
        /// Assigne ou récupère l'url de l'image
        /// </summary>
        public StringBuilder Url { get; set; }

        /// <summary>
        /// Assigne ou récupère l'id de l'image
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Assigne ou récupère l'id de l'evenement
        /// </summary>
        public long EvenementId { get; set; }
    }
}
