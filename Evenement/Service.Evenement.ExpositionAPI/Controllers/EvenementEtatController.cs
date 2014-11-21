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
    /// Contrôleur pour gérer les états des évènements
    /// </summary>
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class EvenementEtatController : ApiController
    {
        EvenementBllService bllService;

       /// <summary>
       /// Permet de récupérer l'etat d'un evenement particulier 
       /// </summary>
       /// <param name="id">id de l'evenement</param>
       /// <returns></returns>
        public EventStateFront GetEventState(int id)
        {
            return Mapper.Map<Business.EventStateBll, EventStateFront>(bllService.GetEventState(id));
        }

        /// <summary>
        /// Permet de signaler un evenement
        /// </summary>
        /// <param name="id">Id de l'evenement</param>
        [HttpPut]
        [Route("api/EvenementEtat/Report/{id}")]
        public void SignalEvent(int id)
        {
            bllService.ModifyEventState(id, new Business.EventStateBll(Business.EventStateEnum.Signaler));
        }

        /// <summary>
        /// Permet de désactiver un evenement
        /// </summary>
        /// <param name="id">id de l'evenement</param>
        [HttpPut]
        [Route("api/EvenementEtat/Deactivate/{id}")]
        public void DesactivateEvent(int id)
        {
            bllService.ModifyEventState(id, new EventStateBll(Business.EventStateEnum.Desactiver));
        }
    }
}
