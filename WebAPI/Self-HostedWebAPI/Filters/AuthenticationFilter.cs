using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Filters;
using System.Web.Http.Results;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Self_HostedWebAPI.Filters
{
    public class BasicAuthenticationAttribute : Attribute, IAuthenticationFilter
    {
        public bool AllowMultiple => false;
        public Task AuthenticateAsync(HttpAuthenticationContext context, CancellationToken cancellationToken)
        {
            return Task.Factory.StartNew(() =>
            {
                HttpRequestMessage request = context.Request;
                AuthenticationHeaderValue authorization = request.Headers.Authorization;

                if (authorization == null || authorization.Scheme != "Basic" || string.IsNullOrEmpty(authorization.Parameter))
                {
                    context.ErrorResult = new UnauthorizedResult(new[] { authorization }, request);
                    return;
                }
                (string user, string password) = ExtractUserNameAndPassword(authorization.Parameter);

                if (password != "World")
                    context.ErrorResult = new UnauthorizedResult(new[] { authorization }, request);

                var identity = new GenericIdentity(user);
                context.Principal = new GenericPrincipal(identity, null);
            });
        }

        private (string, string) ExtractUserNameAndPassword(string authenticationString)
        {
            string parameter = Encoding.UTF8.GetString(Convert.FromBase64String(authenticationString));
            string usrename = parameter.Split(':')[0];
            string password = parameter.Split(':')[1];
            return (usrename, password);
        }

        public async Task ChallengeAsync(HttpAuthenticationChallengeContext context, CancellationToken cancellationToken)
        {
            var response = await context.Result.ExecuteAsync(cancellationToken);
            if (response.StatusCode == HttpStatusCode.Unauthorized)
                response.Headers.WwwAuthenticate.Add(new AuthenticationHeaderValue("Basic"));
            context.Result = new ResponseMessageResult(response);
        }
    }
}
