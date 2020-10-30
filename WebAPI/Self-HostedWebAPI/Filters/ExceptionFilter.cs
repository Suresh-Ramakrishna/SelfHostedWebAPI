using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Filters;

namespace Self_HostedWebAPI.Filters
{
    public class ExceptionActionFilter : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext context)
        {
            var exceptionType = context.Exception.GetType().ToString();
            HttpResponseMessage httpResponseMessage;
            switch (exceptionType)
            {
                case "System.Data.DuplicateNameException":
                    httpResponseMessage = context.Request.CreateResponse(HttpStatusCode.BadRequest, context.Exception.Message);
                    break;
                case "System.Data.ObjectNotFoundException":
                    httpResponseMessage = context.Request.CreateResponse(HttpStatusCode.NotFound, context.Exception.Message);
                    break;

                case "System.UnauthorizedAccessException":
                    httpResponseMessage = context.Request.CreateResponse(HttpStatusCode.Forbidden, context.Exception.Message);
                    break;
                default:
                    httpResponseMessage = context.Request.CreateResponse(HttpStatusCode.InternalServerError, context.Exception.Message);
                    break;
            }
            context.Response = httpResponseMessage;
        }
    }
}
