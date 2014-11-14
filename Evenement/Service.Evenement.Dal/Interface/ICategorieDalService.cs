using Service.Evenement.Dal.Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Evenement.Dal.Interface
{
    public interface ICategorieDalService
    {
        IEnumerable<EvenementCategorieDao> UpdateCategorie(EvenementCategorieDao categorieDao);
    }
}
