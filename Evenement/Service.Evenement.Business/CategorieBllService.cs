using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Service.Evenement.Dal;
using Service.Evenement.Dal.Dao.Request;
using AutoMapper;
using Service.Evenement.Dal.Dao;

namespace Service.Evenement.Business
{
    public class CategorieBllService
    {
        private EvenementDalService _evenementDalService;

        public EvenementDalService EvenementDalService
        {
            get
            {
                if ( _evenementDalService == null )
                    _evenementDalService = new EvenementDalService();
                return _evenementDalService;
            }
            set
            {
                _evenementDalService = value;
            }
        }

        public IEnumerable<EvenementCategorieBll> GetCategories()
        {
            IEnumerable<EvenementCategorieDao> result = _evenementDalService.GetAllCategorie(new EvenementDalRequest() { Categorie = new EvenementCategorieDao() });
            Mapper.CreateMap<EvenementCategorieDao, EvenementCategorieBll>();
            return result == null ? null : Mapper.Map<IEnumerable<EvenementCategorieDao>, IEnumerable<EvenementCategorieBll>>(result);
        }

        public EvenementCategorieBll GetCategorie(long id)
        {
            Dal.Dao.EvenementCategorieDao categ = new  EvenementCategorieDao();
            categ.Id = id;
            IEnumerable<EvenementCategorieDao> result = _evenementDalService.GetAllCategorie(new EvenementDalRequest(){ Categorie = categ});
            Mapper.CreateMap<EvenementCategorieDao, EvenementCategorieBll>();
            return result == null ? null :  Mapper.Map<EvenementCategorieDao, EvenementCategorieBll>(result.First());

        }

       
    }
}
