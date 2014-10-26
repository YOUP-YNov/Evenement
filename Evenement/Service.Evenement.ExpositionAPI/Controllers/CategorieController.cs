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
        public IEnumerable<object> GetAll()
        {
            return new string[] { "value1", "value2" };
        }

        /// <summary>
        /// Retourne la catégorie dont l'id est passé en parémètre
        /// </summary>
        /// <param name="id">Id de la catégorie</param>
        /// <returns>une catégorie</returns>
        public string GetById(int id)
        {
            return "value";
        }

    }
}
