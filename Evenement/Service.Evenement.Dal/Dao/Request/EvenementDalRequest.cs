using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Evenement.Dal.Dao.Request
{
    public class EvenementDalRequest
    {
        public long EvenementId { get; set; }

        public StringBuilder CodePostale { get; set; }

        public EvenementCategorieDao Categorie { get; set; }

        public StringBuilder Ville { get; set; }

        public long LieuEvenementId { get; set; }

        public long UserId { get; set; }

        public long ImageId { get; set; }
    }
}
