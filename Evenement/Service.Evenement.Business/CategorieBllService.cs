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
        #region Donnée membre
        /// <summary>
        /// Donnée membre représentant l'accès au service Dal Evenement
        /// </summary>
        private IEvenementDalService _evenementDalService;

        /// <summary>
        /// Donnée membre représentant l'accès au service Dal categorie
        /// </summary>
        private ICategorieDalService _categorieDalService;

        #endregion

        #region Propriétées
        /// <summary>
        /// Récupère ou assigne l'accès au service Dal Evenement
        /// </summary>
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

        /// <summary>
        /// Récupère ou assigne l'accès au service Dal Categorie
        /// </summary>
        public ICategorieDalService CategorieDalService
        {
            get
            {
                if (_categorieDalService == null)
                    _categorieDalService = new CategorieDalService();
                return _categorieDalService;
            }
            set
            {
                _categorieDalService = value;
            }
        }

        #endregion

        #region Méthodes Publiques

        /// <summary>
        /// Récupère toutes les catégories
        /// </summary>
        /// <returns>Objet de service, englobant les catégories ainsi qu'un status d'opération</returns>
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

        /// <summary>
        /// Récupère une categorie en fonction de son Id
        /// </summary>
        /// <param name="id">Id de la catégorie</param>
        /// <returns>Objet de service, englobant la catégorie ainsi qu'un status d'opération</returns>
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

        /// <summary>
        /// Supprime une catégorie
        /// </summary>
        /// <param name="id">Id de la catégorie a supprimé</param>
        public void DeleteCategorie(long id)
        {
            ((EvenementDalService)EvenementDalService).CategorieDalService.DeleteCategorie(id);
        }

        /// <summary>
        /// Mets à jours une catégorie
        /// </summary>
        /// <param name="categoriebll">Informations relative à la catégorie</param>
        public ResponseObject UpdateCategorie(EvenementCategorieBll categoriebll)
        {
            ResponseObject response = new ResponseObject();
            EvenementCategorieDao daoEventCategorie = Mapper.Map<EvenementCategorieBll, EvenementCategorieDao>(categoriebll);
            try
            {
                IEnumerable<EvenementCategorieDao> result = CategorieDalService.UpdateCategorie(daoEventCategorie);
                if (result != null)
                {
                    if (result.Count() >0)
                    {
                        IEnumerable<EvenementCategorieBll> categEventBll = Mapper.Map<IEnumerable<EvenementCategorieDao>, IEnumerable<EvenementCategorieBll>>(result);
                        response.State = ResponseState.Ok;
                        response.Value = categEventBll;
                        return response;
                    }
                }

            }
            catch (Exception e)
            {
                response.State = ResponseState.NotModified;
            }
            response.State = ResponseState.NotModified;
            return response;
        }

        #endregion

    }
}
