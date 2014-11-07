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

namespace Service.Evenement.ExpositionAPI.Controllers
{
    public class EvenementController : ApiController
    {
        EvenementBllService serviceBll;
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
            IEnumerable<Business.EvenementBll> list = serviceBll.GetEvenements(date_search, max_result, categorie, text_search, max_id, orderby, premium);
            List<EvenementTimelineFront> ret = new List<EvenementTimelineFront>();

            Mapper.CreateMap<Business.EvenementBll, EvenementTimelineFront>();

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
            Mapper.CreateMap<Business.EvenementBll, EvenementTimelineFront>();

            Business.EvenementBllService evenementBllService = new Business.EvenementBllService();
            IEnumerable<Business.EvenementBll> bllEventList = evenementBllService.GetByProfil(id_profil);
            IEnumerable<EvenementTimelineFront> timelineFrontEventList = null;
            foreach(var e in bllEventList)
            {
                timelineFrontEventList.ToList().Add(Mapper.Map<Business.EvenementBll, EvenementTimelineFront>(e));
            }
            return timelineFrontEventList;
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
        public void PutEvenement(int id, bool? prenium, DateTime? end_inscription, int total_people = -1, string description = null, List<Stream> lstPicture = null, object location = null)
        {
            serviceBll = new EvenementBllService();

            EvenementFront evenement = new EvenementFront();
            evenement.Id = id;
            evenement.Premium = prenium ?? false;
            //la date de fin d'inscription est notée comme nullable, mais
            //coté front l'utilisateur sera forcé de noter une date de fin, donc je force dateTime.now pour gérer le nullable
            evenement.DateFinInscription = end_inscription ?? DateTime.Now;
            evenement.MaximumParticipant = total_people;
            evenement.DescriptionEvenement = description;
            // la liste de photos n'est pas encore prise en compte 

            //la gestion des adresse n'est pas encore établie
            evenement.EventAdresse = new EventLocationFront() ;



            AutoMapper.Mapper.CreateMap<EvenementFront, EvenementBll>();
            EvenementBll bllEvent = Mapper.Map<EvenementFront, EvenementBll>(evenement);

            serviceBll.PutEvenement(bllEvent);




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
        /// supression de l'evenement 
        /// </summary>
        /// <param name="id">id de l'evenement</param>
        /// <param name="id_profil">id du profil</param>
        public void DeleteEvenement(int id, int id_profil)
        {

        }

        /// <summary>
        /// Création d'un evenement 
        /// </summary>
        /// <param name="end_inscription"> date de fin d'inscription a un evenement</param>
        /// <param name="date_event">date de l'evenement</param>
        /// <param name="keys_words">liste des mots clés de l'evenement</param>
        /// <param name="friends">liste des amis</param>
        /// <param name="total_people">nombre de personne max pour l'evenement</param>
        /// <param name="description">description de l'evenement</param>
        /// <param name="title">titre de l'evenement</param>
        /// <param name="location">localisation de l'evenement</param>
        /// <param name="prenium">evenement prenium par defaut il ne l est pas</param>
        /// <param name="payant">evenement payant par defaut il est gratuit</param>
        /// <param name="isPublic">evenement ouvert au public</param>
        /// <param name="lstPicture">liste des images</param>
        public void CreateEvenement( DateTime end_inscription, DateTime date_event, List<String> keys_words, List<object> friends, int total_people, string description, string title,
                            object location, bool? prenium, bool? payant, bool? isPublic, List<Stream> lstPicture = null)
        {
            EvenementFront newEvt = new EvenementFront(end_inscription, date_event, keys_words, friends, total_people
                , description, title, location, prenium, payant, isPublic, lstPicture);
            
        }

        /// <summary>
        /// retourne la liste des evenements signalés ( admnin)
        /// </summary>
        /// <param name="id_profil">id du profil admin</param>
        /// <param name="nb_min_signalement">nb de signalement minimum</param>
        /// <returns>liste d'evenement signalé</returns>
        public IEnumerable<EvenementTimelineFront> GetEvenementsSignale(int id_profil, int nb_min_signalement = 1)
        {
             return new EvenementTimelineFront[] { new EvenementTimelineFront(), new EvenementTimelineFront() };
        }
            
        /// <summary>
        /// permet de modifier l'etat d'un evenement (Admin)
        /// </summary>
        /// <param name="id_profil">id de profil</param>
        /// <param name="id_evenement">id de l'evenement</param>
        /// <param name="id_etat">id de l'etat</param>
        public void PutEvenementEtat(int id_profil, int id_evenement, int id_etat)
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
