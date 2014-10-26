using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Evenement.Dal.Dao
{
    public class EventImage
    {
        /// <summary>
        /// Assigne ou récupère l'url de l'image
        /// </summary>
        public StringBuilder Url { get; set; }

        /// <summary>
        /// Assigne ou récupère l'id de l'image
        /// </summary>
        public int Id { get; set; }
    }
}
