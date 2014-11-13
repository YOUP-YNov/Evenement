using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Service.Evenement.ExpositionAPI.Models
{
    public enum EventStateEnumFront
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