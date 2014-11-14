using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Service.Evenement.ExpositionAPI.Models;
using Service.Evenement.Business;
using AutoMapper;
using System.Web.Http.Cors;

namespace Service.Evenement.ExpositionAPI.Controllers
{
    /// <summary>
    /// controller pour les Etat de l'évènement
    /// </summary>
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class EvenementEtatController : ApiController
    {
        EvenementBllService bllService;

       /// <summary>
       /// l'etat d'un evenement particulier 
       /// </summary>
       /// <param name="id">id de l'evenement</param>
       /// <returns></returns>
        public EventStateFront GetEventState(int id)
        {
            return Mapper.Map<Business.EventStateBll, EventStateFront>(bllService.GetEventState(id));
        }

        /// <summary>
        /// signaler un evenement
        /// </summary>
        /// <param name="id">id de l'evenement</param>
        /// <returns></returns>
        public void SignalEvent(int id)
        {
            bllService.ModifyEventState(id, new Business.EventStateBll(Business.EventStateEnum.Signaler));
        }

        /// <summary>
        /// desactiver un evenement
        /// </summary>
        /// <param name="id">id de l'evenement</param>
        /// <returns></returns>
        public void DesactivateEvent(int id)
        {
            bllService.ModifyEventState(id, new EventStateBll(Business.EventStateEnum.Desactiver));
        }
    }
}
