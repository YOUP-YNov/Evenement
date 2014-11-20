using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Evenement.Business.Response
{
    /// <summary>
    /// Enum représentant le résultat d'une opération de service
    /// </summary>
    public enum ResponseState
    {
        Ok,
        NotFound,
        Created,
        NoContent,
        Unauthorized,
        NotModified,
        BadRequest
    }
}
