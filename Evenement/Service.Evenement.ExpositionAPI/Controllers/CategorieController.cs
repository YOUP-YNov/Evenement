using Service.Evenement.ExpositionAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Service.Evenement.ExpositionAPI.Controllers
{
    public class CategorieController : ApiController
    {
        /// <summary>
        /// Retourne la liste des catégories
        /// </summary>
        /// <returns>liste des catégories</returns>
        public IEnumerable<CategorieFront> GetCategories()
        {
            return new CategorieFront[] { new CategorieFront(), new CategorieFront() };
        }

        /// <summary>
        /// Retourne la catégorie dont l'id est passé en parémètre
        /// </summary>
        /// <param name="id">Id de la catégorie</param>
        /// <returns>une catégorie</returns>
        public CategorieFront GetCategorie(int id)
        {
            return new CategorieFront();
        }

    }
}
