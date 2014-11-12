using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Evenement.Dal.Dao
{
    public class EventStateDao
    {
        /// <summary>
        /// Assigne ou récupère le libelle de l'état de l'évenement
        /// </summary>
        public EventStateEnum Nom { get; set; }

        /// <summary>
        /// Assigne ou récupère l'id de l'état de l'évenement
        /// </summary>
        public long Id { get; set; }

        public EventStateDao(EventStateEnum name)
        {
            Nom = name;
            Id = (long)name;
        }

        public EventStateDao()
        {

        }
    }
}
