using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using Service.Evenement.Business;
using Service.Evenement.Business.Response;

namespace Service.Evenement.ExpositionAPI.Context
{
    public static class CategorieContext
    {
        private static Lazy<CategorieBllService> _categorieBllService;

        public static CategorieBllService _CategorieBllService
        {
            get
            {
                if ( _categorieBllService == null || _categorieBllService.Value == null )
                {
                    _categorieBllService = new Lazy<CategorieBllService>(() => { return new CategorieBllService(); });
                }
                return _categorieBllService.Value;
            }
        }

        /// <summary>
        /// Retourne la liste des catégories
        /// </summary>
        /// <returns>liste des catégories</returns>
        public static ResponseObject Get ()
        {
            ResponseObject result = _CategorieBllService.GetCategories();
            return result;
        }

        /// <summary>
        /// Retourne la catégorie dont l'id est passé en paramètre
        /// </summary>
        /// <param name="id">Id de la catégorie</param>
        /// <returns>une catégorie</returns>
        public static ResponseObject Get ( long id )
        {
            ResponseObject result = _CategorieBllService.GetCategorie(id);
            return result;
        }

        /// <summary>
        /// Supprime une catégorie
        /// </summary>
        /// <param name="id">Id de la catégorie à supprimer</param>
        public static void Delete ( long id )
        {
            _CategorieBllService.DeleteCategorie(id);
        }

        /// <summary>
        /// Permet de mettre à jour le libelle d'une catégorie
        /// </summary>
        /// <param name="id">Id de la catégorie à modifier</param>
        /// <param name="libelle">Nouveau libelle</param>
        public static ResponseObject UpdateCategorie ( EvenementCategorieBll Categorie )
        {
           return  _CategorieBllService.UpdateCategorie(Categorie);
        }
    }
}