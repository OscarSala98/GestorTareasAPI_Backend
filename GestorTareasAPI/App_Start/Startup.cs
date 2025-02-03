using System;
using System.Text;
using Microsoft.Owin;
using Microsoft.Owin.Security.Jwt;
using Microsoft.Owin.Security;
using Owin;
using Microsoft.Owin.Security.DataHandler.Encoder;
using System.Configuration;
using System.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Owin.Cors; // <-- IMPORTANTE: Agregar esta línea

[assembly: OwinStartup(typeof(GestorTareasAPI.App_Start.Startup))]
namespace GestorTareasAPI.App_Start
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // HABILITAR CORS PARA PERMITIR SOLICITUDES DESDE EL FRONTEND
            app.UseCors(CorsOptions.AllowAll);

            var issuer = ConfigurationManager.AppSettings["JwtIssuer"];
            var secret = ConfigurationManager.AppSettings["JwtSecret"];

            app.UseJwtBearerAuthentication(new JwtBearerAuthenticationOptions
            {
                AuthenticationMode = AuthenticationMode.Active,
                TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = issuer,
                    ValidAudience = issuer,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret)),
                    ValidateIssuerSigningKey = true,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                }
            });
        }
    }
}
