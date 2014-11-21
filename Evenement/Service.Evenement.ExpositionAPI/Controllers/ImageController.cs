using AutoMapper;
using Service.Evenement.Business;
using Service.Evenement.Business.Response;
using Service.Evenement.ExpositionAPI.Context;
using Service.Evenement.ExpositionAPI.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Description;

namespace Service.Evenement.ExpositionAPI.Controllers
{
    /// <summary>
    /// Contrôleur pour gérer les images des évènements
    /// </summary>
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class ImageController : ApiController
    {
        /// <summary>
        /// Retourne les détails des images d'un évènement
        /// </summary>
        /// <param name="id">L'id de l'évènement</param>
        /// <returns>Un événement</returns>
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
        /// Associe une image à un évènement
        /// </summary>
        /// <param name="fileName">Nom du fichier</param>
        /// <param name="evenement_id">Id de l'évènement</param>
        /// <param name="content">Byte array de l'image</param>
        /// <returns>Image uploadée</returns>
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
        /// Associe une image à un évènement
        /// </summary>
        /// <param name="urlFile">Url du fichier</param>
        /// <param name="EventId">Id de l'évènement</param>
        /// <returns>Image uploadée</returns>
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
        /// Permet de supprimer une image associé à un évènement
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