using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.IO;
namespace Service.Evenement.ExpositionAPI.Controllers
{
    public class EvenementController : ApiController
    {
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
        public IEnumerable<object> GetAll(DateTime? date_search, int max_result = 10, int categorie = -1, string text_search = null, int max_id = -1, string orderby = null)
        {
            return new string[] { "value1", "value2" };
        }

        /// <summary>
        /// retourne la liste des événements d'un profil 
        /// </summary>
        /// <param name="id_profil">id du profil</param>
        /// <returns>liste d'événements</returns>
        public IEnumerable<object> GetByProfil(int id_profil)
        {
            return new string[] { "value1", "value2" };
        }

        /// <summary>
        /// retourne le détail d'un événement
        /// </summary>
        /// <param name="id">l'id de l'événement</param>
        /// <returns>un événement</returns>
        public string GetById(int id)
        {
            return "value";
        }

        /// <summary>
        /// supprime un événement
        /// </summary>
        /// <param name="id">id de l'événement</param>
        /// <param name="id_profil">id du profil de l'utilisateur connecté</param>
        public void DeleteById(int id, int id_profil)
        {

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
        public void PutById(int id, bool? prenium, DateTime? end_inscription, int total_people = -1, string description = null, List<Stream> lstPicture = null, object location = null)
        {

        }

    }
}
