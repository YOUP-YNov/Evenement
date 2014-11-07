using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Service.Evenement.Dal;
using Service.Evenement.Dal.Dao.Request;
using AutoMapper;
using Service.Evenement.Dal.Dao;
using Service.Evenement.Dal.Interface;

namespace Service.Evenement.Business
{
    public class CategorieBllService
    {
        private IEvenementDalService _evenementDalService;

        public IEvenementDalService EvenementDalService
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
            IEnumerable<EvenementCategorieDao> result = EvenementDalService.GetAllCategorie(new EvenementDalRequest() {});
            Mapper.CreateMap<EvenementCategorieDao, EvenementCategorieBll>();
            return (result.Count() == 0 || result == null) ? null : Mapper.Map<IEnumerable<EvenementCategorieDao>, IEnumerable<EvenementCategorieBll>>(result);
        }

        public EvenementCategorieBll GetCategorie(long id)
        {
            Dal.Dao.EvenementCategorieDao categ = new  EvenementCategorieDao();
            categ.Id = id;
            IEnumerable<EvenementCategorieDao> result = EvenementDalService.GetAllCategorie(new EvenementDalRequest(){ Categorie = categ});
            Mapper.CreateMap<EvenementCategorieDao, EvenementCategorieBll>();
            return (result.Count() == 0 || result == null) ? null :  Mapper.Map<EvenementCategorieDao, EvenementCategorieBll>(result.First());

        }

       
    }
}
