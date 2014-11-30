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
            if (responseO.Value != null)
            {
                response.Content = new ObjectContent(responseO.Value.GetType(), responseO.Value, GlobalConfiguration.Configuration.Formatters.JsonFormatter);
            }

            switch (responseO.State)
            {
                case ResponseState.Ok:
                    response.StatusCode = HttpStatusCode.OK;
                    break;
                case ResponseState.Created:
                    response.StatusCode = HttpStatusCode.Created;
                    break;
                case ResponseState.BadRequest:
                    response.StatusCode = HttpStatusCode.BadRequest;
                    break;
                case ResponseState.NotModified:
                    response.StatusCode = HttpStatusCode.NotModified;
                    break;
                case ResponseState.Unauthorized:
                    response.StatusCode = HttpStatusCode.Unauthorized;
                    break;
                case ResponseState.NoContent:
                    response.StatusCode = HttpStatusCode.NoContent;
                    break;
                case ResponseState.NotFound:
                    response.StatusCode = HttpStatusCode.NotFound;
                    break;
            }
            return response;
        }

    }
}