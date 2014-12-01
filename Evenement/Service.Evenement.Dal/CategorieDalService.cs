using Service.Evenement.Dal.Dal.EventDalServiceTableAdapters;
using Service.Evenement.Dal.Dao;
using Service.Evenement.Dal.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Service.Evenement.Dal.Mappeur;
using Service.Evenement.Dal.Properties;
using System.Configuration;

namespace Service.Evenement.Dal
{
    /// <summary>
    /// Cette Classe est voué a disparaître, Une partie du code présent ici est dupliqué du EvenementDalService.cs
    /// </summary>
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

        public CategorieDalService()
        {
            Settings.Default["YoupDEVConnectionStringLucas"] = ConfigurationManager.ConnectionStrings["Service.Evenement.Dal.Properties.Settings.ConnectionString"].ConnectionString;
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
