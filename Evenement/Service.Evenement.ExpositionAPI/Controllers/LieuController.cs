using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Service.Evenement.ExpositionAPI.Controllers
{
    public class LieuController : ApiController
    {
        // GET api/lieu
        public IEnumerable<object> GetAll()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/lieu/5
        public string GetIById(int id)
        {
            return "value";
        }
    }
}
