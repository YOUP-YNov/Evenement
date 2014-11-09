using Service.Evenement.ExpositionAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AutoMapper;
using Service.Evenement.Business;

namespace Service.Evenement.ExpositionAPI.Controllers
{
    public class LieuController : ApiController
    {

        /*private LieuBllService _lieuBllService;

        public LieuBllService LieuBllService
        {
            get
            {
                if (_lieuBllService == null)
                    _lieuBllService = new LieuBllService();
                return _lieuBllService;
            }
            set
            {
                _lieuBllService = value;
            }
        }

        /// <summary>
        /// récupère la liste de tout les lieux
        /// </summary>
        /// <returns>liste de lieux</returns>
        public IEnumerable<EventLocationFront> GetAll()
        {
            //var result = LieuBllService.
            return null;
        }

        /// <summary>
        /// récupère le lieux correspond a l'id passé en parametre
        /// </summary>
        /// <param name="id">id du lieu</param>
        /// <returns>lieu</returns>
        public EventLocationFront GetLieu(int id)
        {
            return new EventLocationFront();
        }


        public void PostLieu(decimal latitude, decimal longitude, string adresse, string ville, string code_postale, string pays )
        {
            EventLocationFront location = new EventLocationFront(latitude,longitude,adresse,pays,code_postale,ville);
            LieuBllService.PostLieu(Mapper.Map<EventLocationFront, EventLocationBll>(location));

        }*/
    }
}
