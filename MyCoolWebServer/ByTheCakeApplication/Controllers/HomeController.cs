namespace MyCoolWebServer.ByTheCakeApplication.Controllers
{
    using MyCoolWebServer.ByTheCakeApplication.Views.Home;
    using MyCoolWebServer.Server.Enums;
    using MyCoolWebServer.Server.Http.Contracts;
    using MyCoolWebServer.Server.Http.Response;
    using System.IO;

    public class HomeController
    {
        public IHttpResponse Index()
        {
            var indexHtml = File.ReadAllText(@"ByTheCakeApplication\Resources\Index.html");

            return new ViewResponse(HttpStatusCode.Ok, new IndexView(indexHtml));
        }

        public IHttpResponse About()
        {
            var indexHtml = File.ReadAllText(@"ByTheCakeApplication\Resources\about.html");

            return new ViewResponse(HttpStatusCode.Ok, new AboutView(indexHtml));
        }
    }
}
