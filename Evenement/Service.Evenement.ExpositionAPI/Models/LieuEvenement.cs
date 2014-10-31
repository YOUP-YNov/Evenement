﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Service.Evenement.ExpositionAPI.Models
{
    public class LieuEvenement
    {
        public long LieuEvenement_id { get; set; }
        public string Ville { get; set; }
        public int CodePostal { get; set; }
        public string Adresse { get; set; }
        public int Longitude { get; set; }
        public int Latitude { get; set; }
        public string Pays { get; set; }
    }
}