using AutoMapper;
using Service.Evenement.Dal.Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Service.Evenement.Dal;
using Service.Evenement.Dal.Dao.Request;
using Service.Evenement.Business.Response;
using Service.Evenement.Business.BusinessModels;
using System.Net;
using Newtonsoft.Json;
using System.Web.Helpers;
using System.Configuration;

namespace Service.Evenement.Business
{
    public class EvenementBllService
    {
        /// <summary>
        /// Donnée membre représentant l'accès au service Dal Evenement
        /// </summary>
        private EvenementDalService _evenementDalService;

        /// <summary>
        /// Récupère ou assigne l'accès au service Dal Evenement
        /// </summary>
        public EvenementDalService EvenementDalService
        {
            get
            {
                if (_evenementDalService == null)
                    _evenementDalService = new EvenementDalService();
                return _evenementDalService;
            }
            set
            {
                _evenementDalService = value;
            }
        }

        /// <summary>
        /// Constructeur par défaut EvenementBllService
        /// </summary>
        public EvenementBllService()
        {

        }

        /// <summary>
        /// Crée un Evenement
        /// </summary>
        /// <param name="evenementBll">evenement a crée</param>
        /// <returns>Objet de service, englobant l'évenement ainsi qu'un status d'opération</returns>
        public ResponseObject CreateEvenement(EvenementBll evenementBll, string token)
        {
            ResponseObject response = new ResponseObject();
            WebClient client = new WebClient();
            int idProfil = -1;
            string resultJson = null;
            try
            {
                resultJson = client.DownloadString(ConfigurationManager.AppSettings["ProfilUri"] + "api/Auth/" + Guid.Parse(token).ToString());
            }
            catch (Exception e)
            {
                 response.State = ResponseState.Unauthorized;
                 return response;
            }

            if (!string.IsNullOrWhiteSpace(resultJson))
            {
                dynamic json = Json.Decode(resultJson);
                if (json != null)
                {
                    idProfil = json.Utilisateur_Id;
                }
                else
                {
                    response.State = ResponseState.Unauthorized;
                }
            }

            if (idProfil != -1)
            {
                evenementBll.OrganisateurId = idProfil;

                client.Headers[HttpRequestHeader.ContentType] = "application/json";
                string id_string_topic = null;
                try
                {
                    id_string_topic = client.UploadString(ConfigurationManager.AppSettings["ForumUri"] + "api/Topic",
                    "{\"Nom\": \"" + evenementBll.TitreEvenement + "\",\"DescriptifTopic\": \"" + evenementBll.DescriptionEvenement +
                    "\",\"DateCreation\": " + DateTime.Now +
                    ",\"Utilisateur_id\": " + evenementBll.OrganisateurId + " }");
                }
                catch (Exception e)
                {
                }

                int valeur;
                if (int.TryParse(id_string_topic, out valeur))
                {
                    int id_topic = valeur;
                    evenementBll.Topic_id = id_topic;
                }

            if (evenementBll.EventAdresse.IsValid() && evenementBll.evenementUpdateIsValid())
            {
                EvenementDao daoEvent = Mapper.Map<EvenementBll, EvenementDao>(evenementBll);
                IEnumerable<EvenementDao> result = EvenementDalService.CreateEvenement(new EvenementDalRequest(), daoEvent);
                if (result.Count() > 0)
                {
                    response.State = ResponseState.Created;
                        response.Value = result;
                   
                    try
                    {
                        // Appel de l'api de recherche pour indexer l'événement
                            client.DownloadString(new Uri(ConfigurationManager.AppSettings["RechercheUri"] + string.Format("add/get_event/type?={0}&idP={1}&nameP={2}&town={3}&latitude={4}&longitude={5}&idE={6}&nameE={7}&date={8}&adresse={9}", evenementBll.EventAdresse.Id, evenementBll.EventAdresse.Nom, evenementBll.EventAdresse.Ville, evenementBll.EventAdresse.Latitude, evenementBll.EventAdresse.Longitude, evenementBll.Id, evenementBll.TitreEvenement, evenementBll.Categorie.Libelle, evenementBll.CreateDate, evenementBll.EventAdresse.Adresse)));
                }
                    catch
                    {
                    }
                }
                else
                {
                    response.State = ResponseState.NotModified;
                }
            }
            else
            {
                response.State = ResponseState.BadRequest;
            }
            }
            else
                response.State = ResponseState.Unauthorized;



            
            return response;
        }

        /// <summary>
        /// Mettre à jours un évènement
        /// </summary>
        /// <param name="evenementBll">Evenement a mettre a jours</param>
        /// <returns>Objet de service, englobant l'évenement ainsi qu'un status d'opération</returns>
        public ResponseObject PutEvenement(EvenementBll evenementBll, string token)
        {
            EvenementBll evenement = (EvenementBll)(this.GetEvenementById(evenementBll.Id)).Value;
            ResponseObject response = new ResponseObject();
            if (evenement != null)
            {
                WebClient client = new WebClient();
                int idProfil = -1;
                string resultJson = null;
                try
                {
                    resultJson = client.DownloadString(ConfigurationManager.AppSettings["Profil"] + "Auth/" + Guid.Parse(token).ToString());
                }
                catch (Exception e)
                {
                   
                }
                
                if (!string.IsNullOrWhiteSpace(resultJson))
                {
                    dynamic json = Json.Decode(resultJson);
                    if (json != null)
                    {
                        idProfil = json.Utilisateur_Id;
                    }
                }

                if (idProfil != evenement.OrganisateurId)
                {
                    response.State = ResponseState.Unauthorized;
                }
                else
                {

                    if (evenementBll.EventAdresse.IsValid() && evenementBll.evenementUpdateIsValid())
                    {
                        EvenementDao daoEvent = Mapper.Map<EvenementBll, EvenementDao>(evenementBll);
                        IEnumerable<EvenementDao> result = EvenementDalService.UpdateEvenement(daoEvent);
                        if (result.Count() > 0)
                        {
                            response.State = ResponseState.Ok;
                        }
                        else
                        {
                            response.State = ResponseState.NotModified;
                        }
                    }
                    else
                    {
                        response.State = ResponseState.BadRequest;
                    }
                }
            }
            else
            {
                response.State = ResponseState.NotFound;
            }
            return response;
        }
        /// <summary>
        /// retourne la liste des évènements en fonction d'une date, d'une catégorie, de son statut, de son nom.
        /// </summary>
        /// <param name="date_search"></param>
        /// <param name="max_result"></param>
        /// <param name="categorie"></param>
        /// <param name="max_id"></param>
        /// <param name="premium"></param>
        /// <param name="text_search"></param>
        /// <param name="orderby"></param>
        /// <returns>liste d'évènements</returns>
        public ResponseObject GetEvenements(DateTime? date_search = null, int max_result = 10, long? categorie = -1, long? max_id = null, bool? premium = null, string text_search = null, string orderby = null, DateTime? startRange = null, DateTime? endRange = null)
        {
            IEnumerable<Dal.Dao.EvenementDao> tmp = EvenementDalService.GetAllEvenement(date_search, premium, max_result, categorie, max_id, text_search, orderby, startRange, endRange);
            IEnumerable<EvenementBll> bllEvent = null;
            try
            {
                bllEvent = Mapper.Map<IEnumerable<EvenementDao>, IEnumerable<EvenementBll>>(tmp);
            }
            catch (Exception e)
            {
                int t = 0;
            }
            WebClient client = new WebClient();

            foreach (EvenementBll e in bllEvent)
            {
                try
                {
                    string result = client.DownloadString(ConfigurationManager.AppSettings["ProfilUri"] + "api/UserSmall/" + e.OrganisateurId);
                if (!string.IsNullOrWhiteSpace(result))
                {
                    dynamic json = Json.Decode(result);
                    if (json != null)
                    {
                        e.OrganisateurPseudo = json.Pseudo;
                        e.OrganisateurImageUrl = json.PhotoChemin;
                    }
                    }
                    e.NbParticipant = tmp.First(x => x.Id == e.Id).Participants != null ? tmp.First(x => x.Id == e.Id).Participants.Count() : 0;
                }
                catch
                {

                }
            }

            ResponseObject response = new ResponseObject();
            if (bllEvent != null)
            {
                if (bllEvent.Count() > 0)
                {
                    response.State = ResponseState.Ok;
                    response.Value = bllEvent;
                }
                else
                {
                    response.State = ResponseState.NoContent;
                }
            }
            else
            {
                response.State = ResponseState.NotFound;
            }
            return response;
        }

        /// <summary>
        /// retourne un évènement par rapport à son ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns>évènement</returns>
        public ResponseObject GetEvenementById(long id)
        {
            ResponseObject response = new ResponseObject();
            if (id == null)
            {
                response.State = ResponseState.BadRequest;
            }
            else
            {
                EvenementDalRequest request = new EvenementDalRequest();
                request.EvenementId = id;
                var evt = EvenementDalService.getEvenementId(request);
                EvenementBll evtBLL = Mapper.Map<EvenementDao, EvenementBll>(evt);
                evtBLL.Participants = GetSubscribersByEvent(Convert.ToInt32(id));
                if (evtBLL != null)
                {
                    WebClient client = new WebClient();
                    try
                    {
                        string result = client.DownloadString(ConfigurationManager.AppSettings["ProfilUri"] + "api/UserSmall/" + evtBLL.OrganisateurId);
                    if (!string.IsNullOrWhiteSpace(result))
                    {
                        dynamic json = Json.Decode(result);
                        if (json != null)
                        {
                            evtBLL.OrganisateurPseudo = json.Pseudo;
                            evtBLL.OrganisateurImageUrl = json.PhotoChemin;
                        }
                    }
                    }
                    catch
                    {

                    }
                    foreach (EvenementSubscriberBll s in evtBLL.Participants)
                    {
                        string res = client.DownloadString(ConfigurationManager.AppSettings["ProfilUri"] + "api/UserSmall/" + s.UtilisateurId);
                        if (!string.IsNullOrWhiteSpace(res))
                        {
                            dynamic json = Json.Decode(res);
                            if (json != null)
                            {
                                s.Pseudo = json.Pseudo;
                                s.ImageUrl = json.PhotoChemin;
                            }
                        }
                    }
                    evtBLL.NbParticipant = evtBLL.Participants.Count();
                    response.State = ResponseState.Ok;
                    response.Value = evtBLL;
                }
                else
                {
                    response.State = ResponseState.NoContent;
                }
            }

            return response;
        }

        /// <summary>
        /// retourne la liste des évènements d'un département
        /// </summary>
        /// <param name="dept"></param>
        /// <returns>liste d'évènements</returns>
        public ResponseObject GetEvenementByDept(int[] dept, DateTime? startTime, DateTime? endTime)
        {
            ResponseObject response = new ResponseObject();
            if (dept == null)
                response.State = ResponseState.BadRequest;
            else
            {
                var evt = EvenementDalService.GetEvenementByDept(dept);
                IEnumerable<EvenementBll> evtBLL = Mapper.Map<IEnumerable<EvenementDao>, IEnumerable<EvenementBll>>(evt);
                if (evtBLL != null)
                {
                    response.State = ResponseState.Ok;
                    response.Value = evtBLL;

                    foreach (var Ev in evtBLL)
                    {
                        var subscribers = EvenementDalService.GetSubscribersByEvent(new EvenementDalRequest() { EvenementId = Ev.Id });
                        List<EvenementSubscriberBll> mySubscribers = null;

                        if (subscribers != null && subscribers.Count() > 0)
                        {
                            mySubscribers = new List<EvenementSubscriberBll>();

                            foreach (var sub in subscribers)
                            {
                                mySubscribers.Add(Mapper.Map<EvenementSubcriberDao, EvenementSubscriberBll>(sub));
                            }
                        }

                        Ev.Participants = mySubscribers;
                        Ev.NbParticipant = mySubscribers != null ? mySubscribers.Count() : 0;
                }
                }
                else response.State = ResponseState.NoContent;
            }
            return response;
        }

        /// <summary>
        /// retourne la liste des événements d'un profil 
        /// </summary>
        /// <param name="id_profil">id du profil</param>
        /// <returns>liste d'événements</returns>
        public ResponseObject GetByProfil(long id_profil)
        {
            // pour l'instant les event dont le profil est organisateur (api profil pour gerer les event ou le profil est inscrit)
            ResponseObject response = new ResponseObject();
            if (id_profil == default(long))
            {
                response.State = ResponseState.BadRequest;
            }
            else
            {
                IEnumerable<EvenementDao> daoEventList = EvenementDalService.GetEvenementByProfil(id_profil);
                if (daoEventList != null)
                {
                    if (daoEventList.Count() > 0)
                    {
                        response.State = ResponseState.Ok;
                        response.Value = Mapper.Map<IEnumerable<EvenementDao>, IEnumerable<EvenementBll>>(daoEventList);
                    }
                    else
                    {
                        response.State = ResponseState.NoContent;
                    }
                }
                else
                {

                    response.State = ResponseState.NotFound;
                }
            }

            return response;
        }

        /// <summary>
        /// Retourne la liste des événements signalés
        /// </summary>
        /// <returns>Liste d'événements</returns>
        public ResponseObject GetReportedEvents()
        {
            ResponseObject response = new ResponseObject();

            IEnumerable<Dal.Dao.EvenementDao> reportedEvents = EvenementDalService.GetReportedEvents();
            if (reportedEvents != null)
            {
                if (reportedEvents.Count() > 0)
                {
                    response.State = ResponseState.Ok;
                    response.Value = Mapper.Map<IEnumerable<EvenementDao>, IEnumerable<EvenementBll>>(reportedEvents);
                }
                else
                {
                    response.State = ResponseState.NoContent;
                }
            }
            else
            {
                response.State = ResponseState.NotFound;
             }
            return response;
        
        }
        /// <summary>
        /// récupère tous les événements selon un état
        /// </summary>
        /// <param name="stateBll"></param>
        /// <returns></returns>
        public ResponseObject GetEventsByState(EventStateBll stateBll)
        {
            ResponseObject response = new ResponseObject();
            EventStateDao stateDao = Mapper.Map<EventStateBll, EventStateDao>(stateBll);
            IEnumerable<Dal.Dao.EvenementDao> events = EvenementDalService.GetEvenementByState(stateDao);
            if (events != null)
            {
                if (events.Count() > 0)
                {
                    response.State = ResponseState.Ok;
                    response.Value = Mapper.Map<IEnumerable<EvenementDao>, IEnumerable<EvenementBll>>(events);
                }
                else
                {
                    response.State = ResponseState.NoContent;
                }
            }
            else
            {
                response.State = ResponseState.NotFound;
            }
            return response;
        }

        /// <summary>
        /// Retourne l'état d'un événement
        /// </summary>
        /// <param name="id">id de l'événement</param>
        /// <returns>état de l'événement</returns>
        public EventStateBll GetEventState(int id)
        {
            //return GetEvenementById(id).EtatEvenement;
            return null;
        }
        /// <summary>
        /// Permet de modifier l'état d'un événement 
        /// </summary>
        /// <param name="id">id de l'événement</param>
        /// <returns>état de l'événement</returns>
        public void ModifyEventState(int id, EventStateBll state)
        {
            EvenementDalRequest request = new EvenementDalRequest();
            request.EvenementId = id;
            EvenementDao eventDao = EvenementDalService.getEvenementId(request);

            switch (state.Nom)
            {
                case EventStateEnum.Annuler:
                    eventDao.EtatEvenement = new EventStateDao(Dal.Dao.EventStateEnum.Annuler);
                    break;
                case EventStateEnum.AValider:
                    eventDao.EtatEvenement = new EventStateDao(Dal.Dao.EventStateEnum.AValider);
                    break;
                case EventStateEnum.Desactiver:
                    eventDao.EtatEvenement = new EventStateDao(Dal.Dao.EventStateEnum.Desactiver);
                    break;
                case EventStateEnum.Reussi:
                    eventDao.EtatEvenement = new EventStateDao(Dal.Dao.EventStateEnum.Reussi);
                    break;
                case EventStateEnum.Signaler:
                    eventDao.EtatEvenement = new EventStateDao(Dal.Dao.EventStateEnum.Signaler);
                    break;
                case EventStateEnum.Valide:
                    eventDao.EtatEvenement = new EventStateDao(Dal.Dao.EventStateEnum.Valide);
                    break;
                default:
                    break;
            }

            EvenementDalService.UpdateStateEvenement(eventDao);
        }

        private long locationExistOrCreate(EventLocationBll loc)
        {
            if (loc != null)
            {
                //Penser a set l id
                Mapper.CreateMap<EventLocationBll, EventLocationDao>();

                EventLocationDao locDAO = Mapper.Map<EventLocationBll, EventLocationDao>(loc);
                IEnumerable<EvenementDao> e = EvenementDalService.CreateLieuEvenement(new EvenementDalRequest(), locDAO);
                if (e != null)
                {
                    EvenementDao evenement = e.First();

                    return evenement.EventAdresse.Id;
                }
            }
            return 0;
        }

         public ResponseObject DeactivateEvent(int eventId, string token)
        {
            ResponseObject response = new ResponseObject();
            WebClient client = new WebClient();
            int idProfil = -1;
                string resultJson = null;
                try
                {
                resultJson = client.DownloadString(ConfigurationManager.AppSettings["Profil"] + "Auth/" + Guid.Parse(token).ToString());
                }
                catch (Exception e)
                {
                    response.State = ResponseState.Unauthorized;
                }
                
                if (!string.IsNullOrWhiteSpace(resultJson))
                {
                    dynamic json = Json.Decode(resultJson);
                    if (json != null)
                    {
                        idProfil = json.Utilisateur_Id;
                    }
                }

                if (idProfil != -1)
                {
                    ResponseObject responseEvt = this.GetEvenementById(eventId);
                    EvenementBll eventDelete = null;
                    if (responseEvt != null)
                    {
                        eventDelete = (EvenementBll)responseEvt.Value;
                    }
                    else
                    {
                        response.State = ResponseState.NotFound;
                    }

                    if (eventDelete != null)
                    {
                        if (idProfil == eventDelete.OrganisateurId)
        {
            EvenementDao eventDao = new EvenementDao();
            eventDao.Id = eventId;
            eventDao.EtatEvenement = new EventStateDao(Service.Evenement.Dal.Dao.EventStateEnum.Desactiver);
            eventDao.DateModification = DateTime.Now;

            EvenementDalService.UpdateStateEvenement(eventDao);
                            response.State = ResponseState.Ok;
                        }
                        else
                        {
                            response.State = ResponseState.Unauthorized;
                        }
                    }
                }
                else
                {
                    response.State = ResponseState.Unauthorized;
                }
                return response;
        }

     
        /// <summary>
        /// Permet de récupèrer la liste de toutes les inscriptions d'un utilisateur
        /// </summary>
        /// <param name="UserId">Id de l'utilisateur</param>
        /// <returns>Liste des evenements souscrit par l'utilisateur</returns>
        public IEnumerable<EvenementSubscriberBll> GetSubscriptionByUser(int UserId)
        {
            IEnumerable<EvenementSubscriberBll> result = null;
            EvenementDalRequest request = new EvenementDalRequest()
            {
                UserId = UserId
            };

            IEnumerable<EvenementSubcriberDao> daoResult = EvenementDalService.GetSubscriptionByUser(request);

            if (daoResult != null)
            {
                result = Mapper.Map<IEnumerable<EvenementSubcriberDao>, IEnumerable<EvenementSubscriberBll>>(daoResult);
            }
            return result;
        }


        /// <summary>
        /// Permet à un utilisateurs de s'incrire ou de se désincrire à un évènement
        /// </summary>
        /// <param name="UserId">Id de l'utilisateur</param>
        /// <param name="EvenementId">Id de l'evenement</param>
        /// <returns>L'inscription de l'utilisateur à l'évènement</returns>
        public ResponseObject SubscribeEvenement(string token, int _evenementId)
        {
            ResponseObject response = new ResponseObject();
            WebClient client = new WebClient();
            int idProfil = -1;
            string resultJson = null;
            try
            {
                resultJson = client.DownloadString(ConfigurationManager.AppSettings["Profil"] + "Auth/" + Guid.Parse(token).ToString());
            }
            catch (Exception e)
            {
                response.State = ResponseState.Unauthorized;
                return response;
            }

            if (!string.IsNullOrWhiteSpace(resultJson))
            {
                dynamic json = Json.Decode(resultJson);
                if (json != null)
                {
                    idProfil = json.Utilisateur_Id;
                }

                if (idProfil != -1)
                {
                    EvenementDalRequest request = new EvenementDalRequest()
                    {
                        UserId = idProfil,
                        EvenementId = _evenementId
                    };
                    //On récupère le nombre d'utilisateurs déjà inscrits et le nombre d'utilsateurs
                    //max pour vérifier s'il reste de la place sur l'événement
                    EvenementDalRequest requestNombreUtilisateurs = new EvenementDalRequest() { EvenementId = _evenementId };
                    EvenementDao eventActuel = EvenementDalService.getEvenementId(requestNombreUtilisateurs);
                    if (eventActuel != null)
                    {
                        int nbDispo = -1;
                        //    if(eventActuel.Participants !=null)
                        //    {
                        //EvenementDalRequest requestNombreMax = new EvenementDalRequest() { EvenementId = _evenementId };
                        //        int nombreMax = EvenementDalService.getEvenementId(requestNombreMax).MaximumParticipant;
                        //        nbDispo = nombreMax - eventActuel.Participants.Count();
                        //    }

                        if (nbDispo > 0 || nbDispo == -1)
                    {
                        var daoResult = EvenementDalService.SubscribeEvenement(request).FirstOrDefault();
                        if (daoResult == null)
                        {
                            response.State = ResponseState.NotFound;
                        }
                            
                        EvenementSubscriberBll result = Mapper.Map<EvenementSubcriberDao, EvenementSubscriberBll>(daoResult);
                        response.State = ResponseState.Ok;
                        response.Value = result;
                        return response;
                    }
                    else
                    {
                        response.State = ResponseState.BadRequest;
                        throw new System.InvalidOperationException("Le nombre d'utilisateurs maximum est déjà atteint");
                    }
                    }
                }
                    
                }
            return response;
        }

        /// <summary>
        /// Permet de récupèrer la liste des personnes inscrites à un évènement
        /// </summary>
        /// <param name="EvenementId">Id de l'évènement</param>
        /// <returns>Liste des inscriptions relative à cette évènement</returns>
        public IEnumerable<EvenementSubscriberBll> GetSubscribersByEvent(int EvenementId)
        {
            EvenementDalRequest request = new EvenementDalRequest()
            {
                EvenementId = EvenementId
            };

            var daoResult = EvenementDalService.GetSubscribersByEvent(request);

            if (daoResult == null)
                return null;

            IEnumerable<EvenementSubscriberBll> result = Mapper.Map<IEnumerable<EvenementSubcriberDao>, IEnumerable<EvenementSubscriberBll>>(daoResult);

            return result;
        }

        /// <summary>
        /// Retourne le nombre de participants à un événement
        /// </summary>
        /// <param name="id">Id de l'événement</param>
        /// <returns>Nombre de participants</returns>
        public int GetParticipantNbByEvent(long id)
        {
            return EvenementDalService.GetParticipantNbByEvent(id);
        }
    }
}
