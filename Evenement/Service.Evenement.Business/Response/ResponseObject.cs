using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Evenement.Business.Response
{
    /// <summary>
    /// Classe de service englobante, contient l'objet service renvoyer ainsi
    /// qu'un code status représentant le bon déroulement du process
    /// </summary>
    public class ResponseObject
    {
        public Object Value { get; set; }
        public ResponseState State { get; set; }
    }
}
