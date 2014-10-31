using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Service.Evenement.Dal;
namespace Service.Evenement.Business
{
    public class CategorieBllService
    {
         private EvenementDalService evenementDalService;

         public CategorieBllService()
        {
            evenementDalService = new EvenementDalService();
             
        }


       /* public EvenementCategorieBll GetCategorie(int id)
        {

        }*/
    }
}
