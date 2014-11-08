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
            IEnumerable <EvenementCategorieBll> result = CategorieBllService.GetCategories();
            Mapper.CreateMap<EvenementCategorieBll, EvenementCategorieFront>();
            IEnumerable<EvenementCategorieFront> lst = (result == null || result.Count() == 0) ? null : Mapper.Map<IEnumerable<EvenementCategorieBll>, IEnumerable<EvenementCategorieFront>>(result);
            HttpResponseMessage response;
            if (lst != null)
            {
                response = Request.CreateResponse(HttpStatusCode.OK, lst);
            }
            else
            {
                response = Request.CreateErrorResponse(HttpStatusCode.NoContent,"");
            }
            return response;
            //return 
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
            EvenementCategorieBll result = CategorieBllService.GetCategorie(id);
            Mapper.CreateMap<EvenementCategorieBll, EvenementCategorieFront>();

            EvenementCategorieFront categ = (result == null ) ? null :  Mapper.Map<EvenementCategorieBll, EvenementCategorieFront>(result);
            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, categ);
            return response;
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

            AutoMapper.Mapper.CreateMap<EvenementCategorieFront, EvenementCategorieBll>();
            EvenementCategorieBll bllEventCategorie = Mapper.Map<EvenementCategorieFront, EvenementCategorieBll>(categorie);

            _categorieBllService.UpdateCategorie(bllEventCategorie);
        }

    }
}
