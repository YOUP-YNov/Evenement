﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Service.Evenement.Dal.Models;
namespace Service.Evenement.ExpositionAPI.Controllers
{
    public class LieuController : ApiController
    {
        /// <summary>
        /// récupère la liste de tout les lieux
        /// </summary>
        /// <returns>liste de lieux</returns>
        public IEnumerable<LieuEvenement> GetAll()
        {
            return new LieuEvenement[]{};
        }

        /// <summary>
        /// récupère le lieux correspond a l'id passé en parametre
        /// </summary>
        /// <param name="id">id du lieu</param>
        /// <returns>lieu</returns>
        public LieuEvenement GetIById(int id)
        {
            return new LieuEvenement();
        }
    }
}