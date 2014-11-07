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
       /// un etat en particulié 
       /// </summary>
       /// <param name="id">id de l'evenement</param>
       /// <returns></returns>
        public EventStateFront GetEtat(int id)
        {
            return new EventStateFront();
        }
    }
}
