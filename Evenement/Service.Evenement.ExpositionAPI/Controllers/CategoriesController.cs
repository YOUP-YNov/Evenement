﻿using Service.Evenement.ExpositionAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Service.Evenement.Business;
using AutoMapper;

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
        public IEnumerable<EvenementCategorieFront> GetCategories()
        {
            IEnumerable <EvenementCategorieBll> result = CategorieBllService.GetCategories();

            return (result == null || result.Count() == 0) ? null : Mapper.Map<IEnumerable<EvenementCategorieBll>, IEnumerable<EvenementCategorieFront>>(result);
        }

        /// <summary>
        /// Retourne la catégorie dont l'id est passé en parémètre
        /// </summary>
        /// <param name="id">Id de la catégorie</param>
        /// <returns>une catégorie</returns>
        public EvenementCategorieFront GetCategorie(long id)
        {
            EvenementCategorieBll result = CategorieBllService.GetCategorie(id);

            return (result == null ) ? null :  Mapper.Map<EvenementCategorieBll, EvenementCategorieFront>(result);
        }

        public void DeleteCategorie(long id)
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
