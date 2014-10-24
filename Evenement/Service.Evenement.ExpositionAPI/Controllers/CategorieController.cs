using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Service.Evenement.ExpositionAPI.Controllers
{
    public class CategorieController : ApiController
    {
        // GET api/Categorie
        public IEnumerable<object> GetAll()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/Categorie/5
        public string GetById(int id)
        {
            return "value";
        }

    }
}
