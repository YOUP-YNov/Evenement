using Service.Evenement.ExpositionAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Service.Evenement.ExpositionAPI.Controllers
{
    public class LieuController : ApiController
    {
        /// <summary>
        /// récupère la liste de tout les lieux
        /// </summary>
        /// <returns>liste de lieux</returns>
        public IEnumerable<LieuEvenementFront> GetAll()
        {
            return null;
        }

        /// <summary>
        /// récupère le lieux correspond a l'id passé en parametre
        /// </summary>
        /// <param name="id">id du lieu</param>
        /// <returns>lieu</returns>
        public LieuEvenementFront GetLieu(int id)
        {
            return new LieuEvenementFront();
        }
    }
}
