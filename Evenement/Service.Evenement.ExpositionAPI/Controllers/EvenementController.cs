using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.IO;
using Service.Evenement.ExpositionAPI.Models;
using Service.Evenement.Business;
using AutoMapper;
using System.Text;
using Service.Evenement.ExpositionAPI.Models.ModelsUpdate;
using Service.Evenement.Business.Response;
using System.Web.Http.Description;
using System.Web.Http.Cors;
using Service.Evenement.ExpositionAPI.Context;

namespace Service.Evenement.ExpositionAPI.Controllers
{
    /// <summary>
    /// Controleur d'évènement.
    /// </summary>
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class EvenementController : ApiController
    {
        /// <summary>
        /// Retourne la liste des evenements
        /// </summary>
        /// <param name="date_search">Parametre optionnel pour la recherche des evenements à une date</param>
        /// <param name="max_result">Le maximum de résultat </param>
        /// <param name="categorie"> L'id de la catégorie</param>
        /// <param name="text_search">Le text de la recherche</param>
        /// <param name="max_id">L'id du derniers evenements</param>
        /// <param name="orderby">Le nom du tri (date, categorie, disponnible)</param>
        /// <param name="startRange">Date de début de la plage</param>
        /// <param name="endRange">Date de fin de la plage</param>
        /// <returns>La liste des événements</returns>
        [HttpGet]
        [ResponseType(typeof(IEnumerable<EvenementTimelineFront>))]
        public HttpResponseMessage Get(DateTime? date_search = null, long? id_Categorie = null,  bool? prenium = null , int max_result = 10,[FromUri] long? max_id = null,string text_search = null, string orderby = null, DateTime? startRange = null, DateTime? endRange = null)
        {
            ResponseObject result = EvenementContext.Get(
                date_search, 
                id_Categorie, 
                prenium, max_result, 
                max_id, text_search, 
                orderby, 
                startRange, 
                endRange);
            if (result.Value != null)
            {
                result.Value = Mapper.Map<IEnumerable<EvenementBll>, IEnumerable<EvenementTimelineFront>>((IEnumerable<EvenementBll>)result.Value);
            }

             return GenerateResponseMessage.initResponseMessage(result);
        }

        /// <summary>
        /// Retourne la liste des événements d'un profil 
        /// </summary>
        /// <param name="id_profil">Id du profil</param>
        /// <returns>Liste d'événements</returns>
        [HttpGet]
        [ResponseType(typeof(IEnumerable<EvenementTimelineFront>))]
        [Route("api/Profil/{id_profil}/Evenements")]
        public HttpResponseMessage GetByProfil(int id_profil)
        {
            ResponseObject result = EvenementContext.GetByProfil(id_profil);
            if (result.Value != null)
            {
                result.Value = Mapper.Map<IEnumerable<EvenementBll>, IEnumerable<EvenementTimelineFront>>((IEnumerable<EvenementBll>)result.Value);
            }

            return GenerateResponseMessage.initResponseMessage(result);
        }

        /// <summary>
        /// Retourne la liste des événements signalés
        /// </summary>
        /// <returns>Liste d'événements</returns>
        /// 
        [Route("api/Evenements/Reported")]
        [ResponseType(typeof(IEnumerable<EvenementTimelineFront>))]
        public HttpResponseMessage GetReportedEvents()
        {
            ResponseObject result = EvenementContext.GetReportedEvents();
            if (result.Value != null)
            {
                result.Value = Mapper.Map<IEnumerable<EvenementBll>, IEnumerable<EvenementTimelineFront>>((IEnumerable<EvenementBll>)result.Value);
            }

            return GenerateResponseMessage.initResponseMessage(result);
        }

        /// <summary>
        /// Retourne le détail d'un événement
        /// </summary>
        /// <param name="id">L'id de l'événement</param>
        /// <returns>Un événement</returns>
        [HttpGet]
        [ResponseType(typeof(EvenementFront))]
        public HttpResponseMessage GetEvenement(long id)
        {
            ResponseObject result = EvenementContext.GetEvenement(id);
            if (result.Value!= null)
            {
                result.Value = Mapper.Map<EvenementBll, EvenementFront>((EvenementBll)result.Value);
            }
            return GenerateResponseMessage.initResponseMessage(result);
        }

        /// <summary>
        /// Retourne les évènements d'un département 
        /// </summary>
        /// <param name="dept"></param>
        /// <returns>Liste d'évènements</returns>
        [HttpGet]
        [ResponseType(typeof(EvenementTimelineFront))]
        public HttpResponseMessage GetEvenement(int dept)
        {
            ResponseObject result = EvenementContext.GetEvenement(dept);
            if (result.Value != null)
            {
                result.Value = Mapper.Map<IEnumerable<EvenementBll>, IEnumerable<EvenementTimelineFront>>((IEnumerable<EvenementBll>)result.Value);
            }

            return GenerateResponseMessage.initResponseMessage(result);
        }

        /// <summary>
        /// Modification de l'évènement
        /// </summary>
        /// <param name="token">Token de l'utilisateur courant</param>
        /// <param name="evenement">Evènement</param>
        [HttpPut]
        public HttpResponseMessage Put(Guid token, [FromBody]EvenementUpdate evenement)
        {
            ResponseObject response = EvenementContext.Put(token, evenement);
            return GenerateResponseMessage.initResponseMessage(response);
        }

        /// <summary>
        /// Permet l'inscription et la desincription
        /// </summary>
        /// <param name="id">Id de l'evenement</param>
        /// <param name="idProfil">Id du profil</param>
        [HttpPost]
        public void PostInscriptionDeinscription(long id, long idProfil)
        {
            EvenementContext.PostInscriptionDeinscription(id, idProfil);
        }

        /// <summary>
        /// Desactivation de l'evenement 
        /// c'est une mise en archive. L'évenement n'est plus consultable. 
        /// </summary>
        /// <param name="id">Id de l'évenement</param>
        /// <param name="id_profil">Id du profil</param>
        [HttpDelete]
        public HttpResponseMessage DeleteEvenement(long id, [FromBody] long id_profil)
        {
            ResponseObject response = EvenementContext.DesactivateEvenement(id, id_profil);
            return GenerateResponseMessage.initResponseMessage(response);
        }

        /// <summary>
        /// Fonction de création de l'évènement
        /// </summary>
        /// <param name="evt">L'évènement à créer</param>
        [HttpPost]
        public HttpResponseMessage Create([FromBody] EvenementCreate evt)
        {
            ResponseObject response = EvenementContext.Create(evt);
            return GenerateResponseMessage.initResponseMessage(response);
        }

        private void InviteFriends(InviteFriends invitations)
        {
            //TODO => appeler le profil
            
        }

        /// <summary>
        /// méthode d'appel de l'api profil
        /// </summary>
        /// <param name="id_profil">id du profil admin</param>
        /// <param name="nb_min_signalement">nb de signalement minimum</param>
        /// <returns>liste d'evenement signalé</returns>
        public IEnumerable<EvenementTimelineFront> GetEvenementsSignale(int id_profil, int nb_min_signalement = 1)
        {
            throw new NotImplementedException();
            //TODO => appeler le profil
            //GETINVITEVENT(int,int,int)
            return null;
        }

        /// <summary>
        /// Permet de modifier l'etat d'un evenement (Admin)
        /// </summary>
        /// <param name="id_profil">id de profil</param>
        /// <param name="id_evenement">id de l'evenement</param>
        /// <param name="id_etat">id de l'etat</param>
        public void PutEvenemenntEtat(int id_profil, int id_evenement, int id_etat)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Permet de lister l'ensemble de evenements suivant un etat (ADmin)
        /// </summary>
        /// <param name="id_profil">Id du profil</param>
        /// <param name="id_etat">Id de l'etat</param>
        /// <returns></returns>
        public IEnumerable<EvenementTimelineFront> GetEvenementsEtats(int id_profil, int id_etat)
        {
            throw new NotImplementedException();
            return new EvenementTimelineFront[] { new EvenementTimelineFront(), new EvenementTimelineFront() };
        }
    }
}
