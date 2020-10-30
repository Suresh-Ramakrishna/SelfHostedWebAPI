using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace Self_HostedWebAPI.Filters
{
    public class LoggerActionFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            Console.WriteLine($"Action Method {actionContext.ActionDescriptor.ActionName} executing at {DateTime.Now}");
        }
        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            Console.WriteLine($"Action Method {actionExecutedContext.ActionContext.ActionDescriptor.ActionName} executed at {DateTime.Now}");
        }
    }
}

