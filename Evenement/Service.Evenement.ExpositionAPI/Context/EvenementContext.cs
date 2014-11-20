using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using AutoMapper;
using Service.Evenement.Business;
using Service.Evenement.Business.Response;
using Service.Evenement.ExpositionAPI.Controllers;
using Service.Evenement.ExpositionAPI.Models;

namespace Service.Evenement.ExpositionAPI.Context
{
    /// <summary>
    /// Classe statique threadSafe avec initialisation tardive pour l'accès au service Evenement Bll
    /// </summary>
    public static class EvenementContext
    {
        /// <summary>
        /// Donnée membre représentant l'accés au service evenement
        /// </summary>
        private static Lazy<EvenementBllService> _eventBusinessService  = new Lazy<EvenementBllService>();

        /// <summary>
        /// Récupère l'accés au service evenement
        /// </summary>
        public static EvenementBllService EventBusinessService
        {
            get 
            {
                if (_eventBusinessService == null || _eventBusinessService.Value == null)
                {
                    _eventBusinessService = new Lazy<EvenementBllService>(() => { return new EvenementBllService(); });
                }
                return _eventBusinessService.Value; 
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
        public static ResponseObject Get ( DateTime? date_search = null, long? id_Categorie = null, bool? prenium = null, int max_result = 10, long? max_id = null, string text_search = null, string orderby = null, DateTime? startRange = null, DateTime? endRange = null )
        {
            ResponseObject result = EventBusinessService.GetEvenements(date_search, max_result, id_Categorie, max_id, prenium, text_search, orderby, startRange, endRange);

            return result;
        }

        /// <summary>
        /// Retourne la liste des événements d'un profil 
        /// </summary>
        /// <param name="id_profil">Id du profil</param>
        /// <returns>Liste d'événements</returns>
        public static ResponseObject GetByProfil ( int id_profil )
        {
            ResponseObject result = EventBusinessService.GetByProfil(id_profil);
            return result;
        }

        /// <summary>
        /// Retourne la liste des événements signalés
        /// </summary>
        /// <returns>Liste d'événements</returns>
        public static ResponseObject GetReportedEvents ()
        {
            ResponseObject result = EventBusinessService.GetReportedEvents();
            return result;
        }

        /// <summary>
        /// Retourne le détail d'un événement
        /// </summary>
        /// <param name="id">L'id de l'événement</param>
        /// <returns>Un événement</returns>
        public static ResponseObject GetEvenement ( long id )
        {
            ResponseObject result = EventBusinessService.GetEvenementById(id);
            return result;
        }

        /// <summary>
        /// Retourne les évènements d'un département 
        /// </summary>
        /// <param name="dept"></param>
        /// <returns>Liste d'évènements</returns>
        public static ResponseObject GetEvenement ( int dept )
        {
            ResponseObject result = EventBusinessService.GetEvenementByDept(dept);
            return result;
        }

        /// <summary>
        /// Modification de l'évènement
        /// </summary>
        /// <param name="id">Id de l'évènement à modifier</param>
        /// <param name="evenement">Evènement</param>
        public static ResponseObject Put (Guid token, EvenementUpdate evenement )
        {
            EvenementBll bllEvent = Mapper.Map<EvenementUpdate, EvenementBll>(evenement);
            ResponseObject response = EventBusinessService.PutEvenement(bllEvent,token);
            return response;
        }

        /// <summary>
        /// Permet l'inscription et la desincription
        /// </summary>
        /// <param name="idEvenement">Id de l'evenement</param>
        /// <param name="idProfil">Id du profil</param>
        public static void PostInscriptionDeinscription ( long idEvenement, long idProfil )
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Desactivation de l'evenement 
        /// c'est une mise en archive. L'évenement n'est plus consultable. 
        /// </summary>
        /// <param name="id">Id de l'evenement</param>
        /// <param name="id_profil">Id du profil</param>
        public static ResponseObject DesactivateEvenement ( long id_evenement, long id_profil )
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
        public static ResponseObject Create ( EvenementCreate evt )
        {
            EvenementBll bllEvent = Mapper.Map<EvenementFront, EvenementBll>(evt.evenement);

            WebClient client = new WebClient();
            client.Headers[HttpRequestHeader.ContentType] = "application/json";
            //la création de topic est censé retourner un entier, l'id du topic
            //En l'état actuel ce n'est pas le cas, mais ils travaillent dessus
            string id_string_topic = client.UploadString("http://forumyoup.apphb.com/api/Topic",
                "{Nom:" + bllEvent.TitreEvenement + ",DescriptifTopic:" + bllEvent.DescriptionEvenement +
                ",DateCreation:" + DateTime.Now +
                ",Utilisateur_id:" + bllEvent.OrganisateurId + " }");

            int valeur;
            if ( int.TryParse(id_string_topic, out valeur) )
            {
                int id_topic = valeur;
                bllEvent.Topic_id = id_topic;
            }

            ResponseObject response = EventBusinessService.CreateEvenement(bllEvent);
            return response;
        }

        private static void InviteFriends ( InviteFriends invitations )
        {
            //TODO => appeler le profil
        }

        /// <summary>
        /// méthode d'appel de l'api profil
        /// </summary>
        /// <param name="id_profil">id du profil admin</param>
        /// <param name="nb_min_signalement">nb de signalement minimum</param>
        /// <returns>liste d'evenement signalé</returns>
        public static IEnumerable<EvenementTimelineFront> GetEvenementsSignale ( int id_profil, int nb_min_signalement = 1 )
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
        public static void PutEvenemenntEtat ( int id_profil, int id_evenement, int id_etat )
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Permet de lister l'ensemble de evenements suivant un etat (ADmin)
        /// </summary>
        /// <param name="id_profil">Id du profil</param>
        /// <param name="id_etat">Id de l'etat</param>
        /// <returns></returns>
        public static IEnumerable<EvenementTimelineFront> GetEvenementsEtats ( int id_profil, int id_etat )
        {
            throw new NotImplementedException();
            return new EvenementTimelineFront[] { new EvenementTimelineFront(), new EvenementTimelineFront() };
        }

        /// <summary>
        /// Retourne le nombre de participants à un événement
        /// </summary>
        /// <param name="id">Id de l'événement</param>
        /// <returns>Nombre de participants</returns>
        public static int GetParticipantNbByEvent(long id)
        {
            return EventBusinessService.GetParticipantNbByEvent(id);
        }
    }
}