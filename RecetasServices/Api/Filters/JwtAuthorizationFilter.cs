using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace RecetasServices.Api.Filters
{
    public class JwtAuthorizationFilter : AuthorizationFilterAttribute
    {

        public override void OnAuthorization(HttpActionContext actionContext)
        {
            if (!actionContext.Request.Headers.Contains("Authorization"))
            {
                actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized, "Missing Authorization Header");
                return;
            }

            var token = actionContext.Request.Headers.GetValues("Authorization").FirstOrDefault();

            if (string.IsNullOrWhiteSpace(token) || !token.StartsWith("Bearer "))
            {
                actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized, "Invalid Token Format");
                return;
            }

            token = token.Substring("Bearer ".Length).Trim();

            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes("YourSuperSecretKeyThatHasAtLeast32Characters!"); // La misma clave que usaste para firmar el token

                var validationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key), // Usar la clave simétrica aquí
                    ValidateIssuer = false,
                    ValidateAudience = false
                };

                // Intentar validar el token
                ClaimsPrincipal principal = tokenHandler.ValidateToken(token, validationParameters, out SecurityToken validatedToken);

                actionContext.RequestContext.Principal = principal;
            }
            catch (Exception ex)
            {
                actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized, $"Token validation failed: {ex.Message}");
            }
        }

    }
}