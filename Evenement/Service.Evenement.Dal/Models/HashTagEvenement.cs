using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Evenement.Dal.Models
{
    public class HashTagEvenement
    {
        public long HashTagEvenement_id { get; set; }
        public long Evenement_id { get; set; }
        public string Mots { get; set; }
    }
}
