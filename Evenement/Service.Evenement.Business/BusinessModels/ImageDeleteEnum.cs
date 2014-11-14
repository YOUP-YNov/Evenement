using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Evenement.Business.BusinessModels
{
    public enum ImageDeleteEnum
    {
        Deleted = 1,
        ExceptionError = 3,
        RequestError = 2,
        UnknowError = 0
    }
}
