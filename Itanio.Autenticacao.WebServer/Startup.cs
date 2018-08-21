using System.Web.Http;
using Itanio.Autenticacao.WebServer;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(Startup))]

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