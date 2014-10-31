using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Evenement.Business.Request
{
    public class EvenementBllRequest
    {
        public long EvenementId { get; set; }

        public StringBuilder CodePostale { get; set; }

        public EvenementCategorieBll Categorie { get; set; }

        public StringBuilder Ville { get; set; }

        public long LieuEvenementId { get; set; }

        public long UserId { get; set; }
    }
}
