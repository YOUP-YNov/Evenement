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

namespace Service.Evenement.ExpositionAPI.Controllers
{
    /// <summary>
    /// controller d'évènement.
    /// </summary>
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
        /// retourne la liste des evenements
        /// </summary>
        /// <param name="date_search">parametre optionnel pour la recherche des evenements à une date</param>
        /// <param name="max_result">le maximum de résultat </param>
        /// <param name="categorie"> l'id de la catégorie</param>
        /// <param name="text_search">le text de la recherche</param>
        /// <param name="max_id">l'id du derniers evenements</param>
        /// <param name="orderby">le nom du trie (date, categorie, disponnible)</param>
        /// <returns>la liste des événements</returns>
        public IEnumerable<EvenementTimelineFront> GetEvenements(DateTime? date_search, int max_result = 10, int categorie = -1, string text_search = null, int max_id = -1, string orderby = null, bool? premium = null)
        {
            IEnumerable<Business.EvenementBll> list = EvenementBllService.GetEvenements(date_search, max_result, categorie, text_search, max_id, orderby, premium);
            List<EvenementTimelineFront> ret = new List<EvenementTimelineFront>();

            foreach (var item in list)
            {
                ret.Add(Mapper.Map<Business.EvenementBll, EvenementTimelineFront>(item));
            }

            return ret;
        }

        /// <summary>
        /// retourne la liste des événements d'un profil 
        /// </summary>
        /// <param name="id_profil">id du profil</param>
        /// <returns>liste d'événements</returns>
        public IEnumerable<EvenementTimelineFront> GetByProfil(int id_profil)
        {
            IEnumerable<Business.EvenementBll> bllEventList = EvenementBllService.GetByProfil(id_profil);
            IEnumerable<EvenementTimelineFront> timelineFrontEventList = null;
            foreach(var e in bllEventList)
            {
                timelineFrontEventList.ToList().Add(Mapper.Map<Business.EvenementBll, EvenementTimelineFront>(e));
            }
            return timelineFrontEventList;
        }

        /// <summary>
        /// Retourne la liste des événements signalés
        /// </summary>
        /// <returns>Liste d'événements</returns>
        /// 
        [Route("api/Evenements/Reported")]
        public IEnumerable<EvenementFront> GetReportedEvents()
        {
            IEnumerable<EvenementBll> tmp = EvenementBllService.GetReportedEvents();

            Mapper.CreateMap<EvenementBll, EvenementFront>();
            Mapper.CreateMap<EventLocationBll, EventLocationFront>();
            Mapper.CreateMap<EvenementCategorieBll, EvenementCategorieFront>();
            Mapper.CreateMap<EventStateBll, EventStateFront>();
            List<EvenementFront> events = new List<EvenementFront>();

            foreach (EvenementBll e in tmp)
            {
                events.Add(Mapper.Map<EvenementBll, EvenementFront>(e));
            }

            return events;
        }

        /// <summary>
        /// retourne le détail d'un événement
        /// </summary>
        /// <param name="id">l'id de l'événement</param>
        /// <returns>un événement</returns>
        public Service.Evenement.ExpositionAPI.Models.EvenementFront GetEvenement(int id)
        {
            return null;
        }

        /// <summary>
        /// Modification d'une annonce
        /// </summary>
        /// <param name="id"></param>
        /// <param name="prenium"></param>
        /// <param name="end_inscription"></param>
        /// <param name="total_people"></param>
        /// <param name="description"></param>
        /// <param name="lstPicture"></param>
        /// <param name="location"></param>
        [HttpPut]
        public void Put(int id, [FromBody]EvenementUpdate evenement)
        {
            
            EvenementBll bllEvent = Mapper.Map<EvenementUpdate, EvenementBll>(evenement);

            EvenementBllService.PutEvenement(bllEvent);

        }
        /// <summary>
        /// Permet l'inscription et la desincription
        /// </summary>
        /// <param name="idEvenement">id de l'evenement</param>
        /// <param name="idProfil">id du profil</param>
        public void PostInscriptionDeinscription(int idEvenement, int idProfil)
        {

        }

        /// <summary>
        /// desactivation de l'evenement 
        /// c'est une mise en archive. L'évenement n'est plus consultable. 
        /// </summary>
        /// <param name="id">id de l'evenement</param>
        /// <param name="id_profil">id du profil</param>
        public void DesactivateEvenement(int id, int id_profil)
        {
            var evts = EvenementBllService.GetByProfil(id_profil);
            var existsEvt = evts.FirstOrDefault(evt => evt.Id == id);
            if (existsEvt != null)
            {
                EvenementBllService.ModifyEventState(id, new EventStateBll(EventStateEnum.Desactiver));
            }
        }

       /// <summary>
       /// fonction de création de l'évènement
       /// </summary>
       /// <param name="evt">l'évènement à creer</param>
        public void CreateEvenement([FromBody] EvenementCreate evt)
        {
            EvenementBll bllEvent = Mapper.Map<EvenementFront, EvenementBll>(evt.evenement);

            EvenementBllService.PutEvenement(bllEvent);

            InviteFriends invitations = new InviteFriends();
            invitations.idEvent = bllEvent.Id;
            invitations.idUser = bllEvent.OrganisateurId;
            invitations.idFriends = evt.friends;
           
        }

        /// <summary>
        /// méthode d'appel de l'api profil
        /// </summary>
        /// <param name="invitations">classe de liaison entre evenement et profil</param>
        private void InviteFriends(InviteFriends invitations)
        {
            //TODO => appeler le profil
            //GETINVITEVENT(int,int,int)
        }
            
        /// <summary>
        /// permet de modifier l'etat d'un evenement (Admin)
        /// </summary>
        /// <param name="id_profil">id de profil</param>
        /// <param name="id_evenement">id de l'evenement</param>
        /// <param name="id_etat">id de l'etat</param>
        public void PutEvenemenntEtat(int id_profil, int id_evenement, int id_etat)
        {

        }

        /// <summary>
        /// Permet de lister l'ensemble de evenements suivant un etat (ADmin)
        /// </summary>
        /// <param name="id_profil">id du profil</param>
        /// <param name="id_etat">id de l'etat</param>
        /// <returns></returns>
        public IEnumerable<EvenementTimelineFront> GetEvenementsEtats(int id_profil, int id_etat)
        {
            return new EvenementTimelineFront[] { new EvenementTimelineFront(), new EvenementTimelineFront() };
        }
    }
}
