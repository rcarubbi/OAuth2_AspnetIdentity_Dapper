using Microsoft.Owin;
using Owin;
using System.Web.Http;

[assembly: OwinStartupAttribute(typeof(Itanio.Autenticacao.WebServer.Startup))]
namespace Itanio.Autenticacao.WebServer
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseWebApi(new HttpConfiguration());
            ConfigureAuth(app);
            ConfigureCors(app);
            
        }
    }
}