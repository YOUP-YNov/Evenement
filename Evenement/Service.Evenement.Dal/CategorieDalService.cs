using Service.Evenement.Dal.Dal.EventDalServiceTableAdapters;
using Service.Evenement.Dal.Dao;
using Service.Evenement.Dal.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Service.Evenement.Dal.Mappeur;

namespace Service.Evenement.Dal
{
    public class CategorieDalService : ICategorieDalService
    {
        private CategorieTableAdapter _cateDalService;

        public CategorieTableAdapter CateDalService
        {
            get
            {
                if (_cateDalService == null)
                    _cateDalService = new CategorieTableAdapter();
                return _cateDalService;
            }
            set
            {
                _cateDalService = value;
            }
        }

        public IEnumerable<EvenementCategorieDao> UpdateCategorie(EvenementCategorieDao categorieDao)
        {
            if (categorieDao == null)
                return null;
            var result = CateDalService.UpdateCategorie(
                                categorieDao.Id,
                                categorieDao.Libelle.ToString());
            return result.ToCategorieDao();

        }
    }
}
