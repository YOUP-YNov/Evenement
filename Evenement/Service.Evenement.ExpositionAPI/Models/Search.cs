using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Service.Evenement.ExpositionAPI.Models
{
    public class Search
    {
        public DateTime Date_search { get; set; }
        public long Id_Categorie { get; set; }
        public string Text { get; set; }
        public string OrderBy { get; set; }
        public bool Prenium { get; set; }
    }
}