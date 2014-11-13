using Service.Evenement.ExpositionAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Service.Evenement.Business;
using AutoMapper;
using System.Web.Http.Description;
using Service.Evenement.Business.Response;

namespace Service.Evenement.ExpositionAPI.Controllers
{
    public class CategoriesController : ApiController
    {

        private CategorieBllService _categorieBllService;

        public CategorieBllService CategorieBllService
        {
            get
            {
                if (_categorieBllService == null)
                    _categorieBllService = new CategorieBllService();
                return _categorieBllService;
            }
            set
            {
                _categorieBllService = value;
            }
        }
        /// <summary>
        /// Retourne la liste des catégories
        /// </summary>
        /// <returns>liste des catégories</returns>
        [HttpGet]
        [ResponseType(typeof(IEnumerable<EvenementCategorieFront>))]
        public HttpResponseMessage Get()
        {
            ResponseObject result = CategorieBllService.GetCategories();
            if (result.Value is IEnumerable<EvenementCategorieBll>)
            {
                result.Value = Mapper.Map<IEnumerable<EvenementCategorieBll>, IEnumerable<EvenementCategorieFront>>((IEnumerable<EvenementCategorieBll>)result.Value);
            }

            return GenerateResponseMessage.initResponseMessage(result);

           
        }

        /// <summary>
        /// Retourne la catégorie dont l'id est passé en parémètre
        /// </summary>
        /// <param name="id">Id de la catégorie</param>
        /// <returns>une catégorie</returns>
        
        [HttpGet]
        [ResponseType(typeof(EvenementCategorieFront))]
        public HttpResponseMessage Get(long id)
        {

            ResponseObject result = CategorieBllService.GetCategorie(id);
            if (result.Value is IEnumerable<EvenementCategorieBll>)
            {
                result.Value = Mapper.Map<EvenementCategorieBll, EvenementCategorieFront>((EvenementCategorieBll)result.Value);
            }

            return GenerateResponseMessage.initResponseMessage(result);
        }

        /// <summary>
        /// Supprime une catégorie
        /// </summary>
        /// <param name="id"></param>
        [HttpDelete]
        public void Delete(long id)
        {
            _categorieBllService.DeleteCategorie(id);
        }

        public void UpdateCategorie(long id, String libelle)
        {
            EvenementCategorieFront categorie = new EvenementCategorieFront();
            categorie.Id = id;
            categorie.Libelle = libelle;

            EvenementCategorieBll bllEventCategorie = Mapper.Map<EvenementCategorieFront, EvenementCategorieBll>(categorie);

            _categorieBllService.UpdateCategorie(bllEventCategorie);
        }

    }
}
