using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Service.Evenement.ExpositionAPI.Models;

namespace Service.Evenement.ExpositionAPI.Controllers
{
    public class EvenementEtatController : ApiController
    {
        /// <summary>
        /// liste des etats d'un evenement
        /// </summary>
        /// <returns>liste d'etats</returns>
        public IEnumerable<EventStateFront> GetEtats()
        {
            return new EventStateFront[] { new EventStateFront(), new EventStateFront() };
        }

       /// <summary>
       /// un etat en particulié 
       /// </summary>
       /// <param name="id">id de l'etat</param>
       /// <returns></returns>
        public EventStateFront GetEtat(int id)
        {
            return new EventStateFront();
        }
    }
}
