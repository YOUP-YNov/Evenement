using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Evenement.Business.Response
{
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
