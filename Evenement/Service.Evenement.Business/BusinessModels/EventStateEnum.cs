using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Evenement.Business
{
    public enum EventStateEnum
    {
        AValider = 11,
        Valide = 12,
        Annuler = 13,
        Signaler = 14,
        Reussi = 15,
        Desactiver = 16,
        Unspecified
    }
}
