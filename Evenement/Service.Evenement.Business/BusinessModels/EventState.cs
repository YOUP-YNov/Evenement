using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Evenement.Business
{
    public class EventStateBll
    {
        /// <summary>
        /// Assigne ou récupère le libelle de l'état de l'évenement
        /// </summary>
        public EventStateEnum Nom { get; set; }

        /// <summary>
        /// Assigne ou récupère l'id de l'état de l'évenement
        /// </summary>
        public long Id { get; set; }
    }
}
