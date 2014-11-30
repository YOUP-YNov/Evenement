using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Evenement.Dal.Dao
{
    public enum EventStateEnum
    {
        AValider = 17,
        Valide = 18,
        Annuler = 19,
        Signaler = 20,
        Reussi = 21,
        Desactiver = 22,
        Unspecified
    }
}
