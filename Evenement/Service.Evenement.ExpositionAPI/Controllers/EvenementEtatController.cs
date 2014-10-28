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
        public IEnumerable<EvenementEtat> GetEtats()
        {
            return new EvenementEtat[] { new EvenementEtat(), new EvenementEtat() };
        }

       /// <summary>
       /// un etat en particulié 
       /// </summary>
       /// <param name="id">id de l'etat</param>
       /// <returns></returns>
        public EvenementEtat GetEtat(int id)
        {
            return new EvenementEtat();
        }
    }
}
