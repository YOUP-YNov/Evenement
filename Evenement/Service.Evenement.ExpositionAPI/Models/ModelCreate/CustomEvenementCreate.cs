using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Service.Evenement.ExpositionAPI.Models.ModelCreate
{
    public class CustomEvenementCreate
    {
        public EvenementCreate evenement { get; set; }
        public List<long> friends { get; set; }

    }
}