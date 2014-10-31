using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Service.Evenement.ExpositionAPI.Controllers
{
    public class EvenementEtatController : ApiController
    {
        /// <summary>
        /// liste des etats d'un evenement
        /// </summary>
        /// <returns>liste d'etats</returns>
        public IEnumerable<EvenementEtatFront> GetEtats()
        {
            return new EvenementEtatFront[] { new EvenementEtatFront(), new EvenementEtatFront() };
        }

       /// <summary>
       /// un etat en particulié 
       /// </summary>
       /// <param name="id">id de l'etat</param>
       /// <returns></returns>
        public EvenementEtatFront GetEtat(int id)
        {
            return new EvenementEtatFront();
        }
    }
}
