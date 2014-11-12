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
using Service.Evenement.Business.Response;

namespace Service.Evenement.Business
{
    public class CategorieBllService
    {
        private IEvenementDalService _evenementDalService;

        public IEvenementDalService EvenementDalService
        {
            get
            {
                if (_evenementDalService == null)
                    _evenementDalService = new EvenementDalService();
                return _evenementDalService;
            }
            set
            {
                _evenementDalService = value;
            }
        }

        public ResponseObject GetCategories()
        {
            IEnumerable<EvenementCategorieDao> result = EvenementDalService.GetAllCategorie(new EvenementDalRequest() { });
            ResponseObject response = new ResponseObject();
            if (result == null)
            {
                response.State = ResponseState.NoContent;
            }
            else
            {
                response.State = ResponseState.Ok;
                response.Value =  Mapper.Map<IEnumerable<EvenementCategorieDao>, IEnumerable<EvenementCategorieBll>>(result);
            }
            return response;
        }

        public ResponseObject GetCategorie(long id)
        {
            Dal.Dao.EvenementCategorieDao categ = new EvenementCategorieDao();
            categ.Id = id;
            IEnumerable<EvenementCategorieDao> result = EvenementDalService.GetAllCategorie(new EvenementDalRequest() { Categorie = categ });
            ResponseObject response = new ResponseObject();
            if (result == null)
            {
                response.State = ResponseState.NoContent;
            }
            else
            {
                response.State = ResponseState.Ok;
                response.Value = Mapper.Map<EvenementCategorieDao, EvenementCategorieBll>(result.First());
            }
            return response;
        }

        public void DeleteCategorie(long id)
        {
            ((EvenementDalService)_evenementDalService).CategorieDalService.DeleteCategorie(id);
        }

        public void UpdateCategorie(EvenementCategorieBll categoriebll)
        {
             EvenementCategorieDao daoEventCategorie = Mapper.Map<EvenementCategorieBll, EvenementCategorieDao>(categoriebll);

            // TODO : corriger cette ligne :
            //_evenementDalService.CategorieDalService.UpdateCategorie(daoEventCategorie.Id,daoEventCategorie.Libelle.ToString());

            throw new NotImplementedException();
        }
       
    }
}
