using AutoMapper;
using Service.Evenement.Business;
using Service.Evenement.Business.Response;
using Service.Evenement.ExpositionAPI.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace Service.Evenement.ExpositionAPI.Controllers
{
    public class GenerateResponseMessage
    {
        public static HttpResponseMessage initResponseMessage(ResponseObject responseO)
        {
            HttpResponseMessage response = new HttpResponseMessage();
          
            switch (responseO.State)
            {

                case ResponseState.Ok:
                    {
                        if (responseO.Value != null)
                        {
                            response.Content = new ObjectContent(responseO.Value.GetType(), responseO.Value, GlobalConfiguration.Configuration.Formatters.JsonFormatter);
                        }
                       response.StatusCode = HttpStatusCode.OK;
                        break;
                    }
                case ResponseState.NoContent:
                    {
                        if (responseO.Value != null)
                        {
                            response.Content = new ObjectContent(responseO.Value.GetType(), responseO.Value, GlobalConfiguration.Configuration.Formatters.JsonFormatter);
                        }
                        response.StatusCode = HttpStatusCode.NoContent;
                        break;
                    }
                case ResponseState.NotFound:
                    {
                        break;
                    }

            }
            return response;
        }

    }
}