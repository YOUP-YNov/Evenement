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
using System.Web.Http.Cors;
using Service.Evenement.ExpositionAPI.Context;

namespace Service.Evenement.ExpositionAPI.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class ImageController : Controller
    {
        /// <summary>
        /// retourne les détail des images d'un evenement
        /// </summary>
        /// <param name="id">l'id de l'événement</param>
        /// <returns>un événement</returns>
        [HttpGet]
        [ResponseType(typeof(EventImageFront))]
        public HttpResponseMessage GetImagesEvenement ( long id )
        {
            var result = ImageContext.GetImagesEvenement(id);
            if ( result.Value != null )
            {
                result.Value = Mapper.Map < IEnumerable<EventImageBll>, IEnumerable<EventImageFront>>((IEnumerable<EventImageBll>) result.Value);
            }
            return GenerateResponseMessage.initResponseMessage(result);
        }

        /// <summary>
        /// Associe une image a un evenement
        /// </summary>
        /// <param name="fileName">nom du fichier</param>
        /// <param name="evenement_id">id de l'évènement</param>
        /// <param name="content">byte array de l'image</param>
        /// <returns>Image uploadé</returns>
        [HttpPost]
        [ResponseType(typeof(EventImageFront))]
        public HttpResponseMessage PostImage ( string fileName, long evenement_id, byte[] content )
        {
            var result = ImageContext.PostImage(fileName, evenement_id, content);
            if ( result.Value != null )
            {
                result.Value = Mapper.Map<IEnumerable<EventImageBll>, IEnumerable<EventImageFront>>((IEnumerable<EventImageBll>) result.Value);
            }
            return GenerateResponseMessage.initResponseMessage(result);
        }

        /// <summary>
        /// Associe une image a un evenement
        /// </summary>
        /// <param name="urlFile">url du fichier</param>
        /// <param name="EventId">id de l'évènement</param>
        /// <returns>Image uploadé</returns>
        [HttpPost]
        [ResponseType(typeof(EventImageFront))]
        public HttpResponseMessage PostImageByUri ( string urlFile, long EventId )
        {
            var result = ImageContext.PostImageByUri(urlFile, EventId);
            if ( result.Value != null )
            {
                result.Value = Mapper.Map<IEnumerable<EventImageBll>, IEnumerable<EventImageFront>>((IEnumerable<EventImageBll>) result.Value);
            }
            return GenerateResponseMessage.initResponseMessage(result);
        }

        /// <summary>
        /// Permet de delete une image associé a un évènement
        /// </summary>
        /// <param name="ImageId">Id de l'image</param>
        /// <returns>CodeEnum de résultat</returns>
        [HttpDelete]
        [ResponseType(typeof(string))]
        public HttpResponseMessage DeleteImage ( long ImageId )
        {
            long? imageId = ImageId;
            var result = ImageContext.DeleteImage(imageId);
            return GenerateResponseMessage.initResponseMessage(result);
        }
    }
}