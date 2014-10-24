using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Service.Evenement.ExpositionAPI.Controllers
{
    public class EvenementController : ApiController
    {
        /// <summary>
        /// test yolo hgshjhsskjb qdfsd
        /// </summary>
        /// <param name="max_id"></param>
        /// <param name="max_result"></param>
        /// <param name="categorie"></param>
        /// <param name="text_search"></param>
        /// <returns></returns>
        public IEnumerable<object> GetAll(int max_id = -1, int max_result = 10, object categorie = null, string text_search = null )
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
