namespace WebServer
{
    using Server;
    using Server.Contracts;
    using Server.Routing;
    using WebServer.Application;

    public class Startup : IRunnable
    {
        private ServerWeb webServer;

        public static void Main()
        {
            new Startup().Run();
        }

        public void Run()
        {
            var mainApplication = new MainApplication();
            var appRouteConfig = new AppRouteConfig();
            mainApplication.Configure(appRouteConfig);

            this.webServer = new ServerWeb(1337, appRouteConfig);
            this.webServer.Run();
        }
    }
}
