namespace WebServer.Application
{
    using WebServer.Application.Controllers;
    using WebServer.Server.Contracts;
    using WebServer.Server.Routing.Contracts;

    public class MainApplication : IApplication
    {
        public void Configure(IAppRouteConfig appRoute)
        {
            appRoute.Get("/", request => new HomeController().Index());
        }
    }
}
