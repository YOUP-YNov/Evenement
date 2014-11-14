using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http.Description;
using System.Web.Mvc;
using AutoMapper;
using Service.Evenement.Business;
using Service.Evenement.Business.Response;
using Service.Evenement.ExpositionAPI.Models;

namespace Service.Evenement.ExpositionAPI.Controllers
{
    public class ImageController : Controller
    {
        private ImageBllService _evenementBllService;

        public ImageBllService EvenementBllService
        {
            get
            {
                if ( _evenementBllService == null )
                    _evenementBllService = new ImageBllService();
                return _evenementBllService;
            }
            set
            {
                _evenementBllService = value;
            }
        }

        /// <summary>
        /// retourne le détail d'un événement
        /// </summary>
        /// <param name="id">l'id de l'événement</param>
        /// <returns>un événement</returns>
        [HttpGet]
        [ResponseType(typeof(EventImageFront))]
        public HttpResponseMessage GetImageEvenement ( long id )
        {
            ResponseObject result = new ResponseObject() { Value = EvenementBllService.GetAllImageByEvent(id) };
            if ( result.Value != null )
            {
                result.Value = Mapper.Map < IEnumerable<EventImageBll>, IEnumerable<EventImageFront>>((IEnumerable<EventImageBll>) result.Value);
            }
            return GenerateResponseMessage.initResponseMessage(result);
        }
    }
}