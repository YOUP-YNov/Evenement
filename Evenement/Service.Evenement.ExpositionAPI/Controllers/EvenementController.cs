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
using Service.Evenement.ExpositionAPI.Models.ModelCreate;

namespace Service.Evenement.ExpositionAPI.Controllers
{
    /// <summary>
    /// Contrôleur pour gérer les évènements
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
        [ResponseType(typeof(EvenementTimelineFront))]
        public HttpResponseMessage GetEvenement(long id)
        {
            ResponseObject result = EvenementContext.GetEvenement(id);
            if (result.Value!= null)
            {
                result.Value = Mapper.Map<EvenementBll, EvenementTimelineFront>((EvenementBll)result.Value);
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
        public HttpResponseMessage Put(string token, [FromBody]EvenementUpdate evenement)
        {
            EvenementBll bllEvent = Mapper.Map<EvenementUpdate, EvenementBll>(evenement);
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
        public HttpResponseMessage Create(string token, [FromBody] CustomEvenementCreate evt)
        {
            ResponseObject response = EvenementContext.Create(evt, token);
            return GenerateResponseMessage.initResponseMessage(response);
        }

        private void InviteFriends(InviteFriends invitations)
        {
            //TODO => appeler le profil
            
        }


        /// <summary>
        /// Permet de lister l'ensemble des evenements suivant l'id d'un etat
        /// </summary>
        /// <param name="id_etat">Id de l'etat</param>
        /// <returns></returns>
        public HttpResponseMessage GetEvenementsEtats(int id_etat)
        {
            EventStateFront stateFront = new EventStateFront();

            switch (id_etat)
	        {
		        case 11:
                    stateFront.Nom=EventStateEnumFront.AValider;
                    stateFront.Id = 11;
                    break;
                case 12:
                    stateFront.Nom = EventStateEnumFront.Valide;
                    stateFront.Id = 12;
                    break;
                case 13:
                    stateFront.Nom = EventStateEnumFront.Annuler;
                    stateFront.Id = 13;
                    break;
                case 14:
                    stateFront.Nom = EventStateEnumFront.Signaler;
                    stateFront.Id = 14;
                    break;
                case 15:
                    stateFront.Nom = EventStateEnumFront.Reussi;
                    stateFront.Id = 15;
                    break;
                case 16:
                    stateFront.Nom = EventStateEnumFront.Desactiver;
                    stateFront.Id = 16;
                    break;
	        }
            ResponseObject result = EvenementContext.GetEventsByState(stateFront);
            if (result.Value != null)
            {
                result.Value = Mapper.Map<IEnumerable<EvenementBll>, IEnumerable<EvenementFront>>((IEnumerable<EvenementBll>)result.Value);
            }

            return GenerateResponseMessage.initResponseMessage(result);
        }

        /// <summary>
        /// Permet de lister l'ensemble des evenements suivant un etat
        /// </summary>
        /// <param name="stateFront">Evenement</param>
        /// <returns>Liste d'événements</returns>
        [ResponseType(typeof(IEnumerable<EvenementFront>))]
        [Route("api/Evenement/State")]
        public HttpResponseMessage GetEventsByState(EventStateFront stateFront)
        {
            ResponseObject result = EvenementContext.GetEventsByState(stateFront);
            if (result.Value != null)
            {
                result.Value = Mapper.Map<IEnumerable<EvenementBll>, IEnumerable<EvenementFront>>((IEnumerable<EvenementBll>)result.Value);
            }

            return GenerateResponseMessage.initResponseMessage(result);
        }

        /// <summary>
        /// Retourne le nombre de participants à un événement
        /// </summary>
        /// <param name="id">Id de l'événement</param>
        /// <returns>Nombre de participants</returns>
        [HttpGet]
        [Route("api/Evenement/{id}/ParticipantNb")]
        public int GetParticipantNbByEvent(long id)
        {
            return EvenementContext.GetParticipantNbByEvent(id);
        }
    }
}
