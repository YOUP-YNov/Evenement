using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Evenement.Dal.Models
{
    public class EvenementPhoto
    {
        public long EvenementPhoto_id { get; set; }
        public long Evenement_id { get; set; }
        public string Adresse { get; set; }
    }
}
