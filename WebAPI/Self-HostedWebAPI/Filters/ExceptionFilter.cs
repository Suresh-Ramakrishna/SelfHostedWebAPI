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
            if (context.Exception is UnauthorizedAccessException)
                context.Response = context.Request.CreateResponse(HttpStatusCode.Forbidden, context.Exception.Message);
            else
                context.Response = context.Request.CreateResponse(HttpStatusCode.InternalServerError, context.Exception.Message);
        }
    }
    
}
