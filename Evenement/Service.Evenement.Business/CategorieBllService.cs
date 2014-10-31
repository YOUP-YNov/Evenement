using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Service.Evenement.Dal;
using Service.Evenement.Dal.Dao.Request;

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

        /*public EvenementCategorieBll GetCategories ()
        {
            var result = _evenementDalService.GetAllCategorie;
            return result; 
        }

        public EvenementCategorieBll GetCategorie(long id)
        {
             Dal.Dao.EvenementCategorieDao categ = new  Dal.Dao.EvenementCategorieDao();
            categ.Id = id;
            IEnumerable<Dal.Dao.EvenementCategorieDao> result = _evenementDalService.GetAllCategorie(new EvenementDalRequest(){ Categorie = categ});
            return ((result = null) ? null : result.First);

        }

        public void ExampleAutoMapper(EvenementDao daoEvent)
        {
            Mapper.CreateMap<EvenementDao, EvenementBll>();
            EvenementBll bllEvent = Mapper.Map<EvenementDao, EvenementBll>(daoEvent);
        }*/
    }
}
