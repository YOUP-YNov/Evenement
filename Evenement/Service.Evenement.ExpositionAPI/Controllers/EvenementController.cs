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

namespace Service.Evenement.ExpositionAPI.Controllers
{
    /// <summary>
    /// Controleur d'évènement.
    /// </summary>
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class EvenementController : ApiController
    {
        private EvenementBllService _evenementBllService;

        public EvenementBllService EvenementBllService
        {
            get
            {
                if (_evenementBllService == null)
                    _evenementBllService = new EvenementBllService();
                return _evenementBllService;
            }
            set
            {
                _evenementBllService = value;
            }
        }

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
            ResponseObject result = EvenementBllService.GetEvenements(date_search, max_result, id_Categorie, max_id, prenium, text_search, orderby, startRange, endRange);
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
            ResponseObject result = EvenementBllService.GetByProfil(id_profil);
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
            ResponseObject result = EvenementBllService.GetReportedEvents();
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
            ResponseObject result =  EvenementBllService.GetEvenementById(id);
            if (result.Value!= null)
            {
                result.Value = Mapper.Map<EvenementBll, EvenementFront>((EvenementBll)result.Value); // TODO : voir pourquoi ça crash
            }
            return GenerateResponseMessage.initResponseMessage(result);
        }

        /// <summary>
        /// Retourne les évènements d'un département 
        /// </summary>
        /// <param name="dept"></param>
        /// <returns>Liste d'évènements</returns>
        [HttpGet]
        [ResponseType(typeof(EvenementFront))]
        public HttpResponseMessage GetEvenement(int dept)
        {
            ResponseObject result = EvenementBllService.GetEvenementByDept( dept );
            if (result.Value != null)
            {
                result.Value = Mapper.Map<IEnumerable<EvenementBll>, IEnumerable<EvenementTimelineFront>>((IEnumerable<EvenementBll>)result.Value);
            }

            return GenerateResponseMessage.initResponseMessage(result);
        }

        /// <summary>
        /// Modification de l'évènement
        /// </summary>
        /// <param name="id">Id de l'évènement à modifier</param>
        /// <param name="evenement">Evènement</param>
        [HttpPut]
        public HttpResponseMessage Put(long id_evenement, [FromBody]EvenementUpdate evenement)
        {
            EvenementBll bllEvent = Mapper.Map<EvenementUpdate, EvenementBll>(evenement);
            ResponseObject response = EvenementBllService.PutEvenement(bllEvent);
            return GenerateResponseMessage.initResponseMessage(response);
        }

        /// <summary>
        /// Permet l'inscription et la desincription
        /// </summary>
        /// <param name="idEvenement">Id de l'evenement</param>
        /// <param name="idProfil">Id du profil</param>
        public void PostInscriptionDeinscription(long idEvenement, long idProfil)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Desactivation de l'evenement 
        /// c'est une mise en archive. L'évenement n'est plus consultable. 
        /// </summary>
        /// <param name="id">Id de l'evenement</param>
        /// <param name="id_profil">Id du profil</param>
        [HttpDelete]
        [Route("Evenements/{id_evenement}/Desactiver")]
        public HttpResponseMessage DesactivateEvenement(long id_evenement, [FromBody] long id_profil)
        {
            /*var evts = EvenementBllService.GetByProfil(id_profil);
            var existsEvt = evts.FirstOrDefault(evt => evt.Id == id_evenement);
            if (existsEvt != null)
            {
                EvenementBllService.DeactivateEvent(id_evenement);
            }*/
            return null;
        }

        /// <summary>
        /// Fonction de création de l'évènement
        /// </summary>
        /// <param name="evt">L'évènement à créer</param>
        [HttpPost]
        public HttpResponseMessage Create([FromBody] EvenementCreate evt)
        {
            EvenementBll bllEvent = Mapper.Map<EvenementFront, EvenementBll>(evt.evenement);

            WebClient client = new WebClient();
            client.Headers[HttpRequestHeader.ContentType] = "application/json";
            //la création de topic est censé retourner un entier, l'id du topic
            //En l'état actuel ce n'est pas le cas, mais ils travaillent dessus
            string id_string_topic=client.UploadString("http://forumyoup.apphb.com/Help/Api/POST-api-Topic",
                "{Nom:"+bllEvent.TitreEvenement+",DescriptifTopic:"+bllEvent.DescriptionEvenement+
                ",DateCreation:"+DateTime.Now+
                ",Utilisateur_id:"+bllEvent.OrganisateurId+" }");

            int valeur;
            if(int.TryParse(id_string_topic,out valeur))
            {
                int id_topic = valeur;
                bllEvent.Topic_id = id_topic;
            }
            
            ResponseObject response = EvenementBllService.CreateEvenement(bllEvent);
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
