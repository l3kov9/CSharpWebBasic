namespace MyCoolWebServer.ByTheCakeApplication.Helpers
{
    using MyCoolWebServer.ByTheCakeApplication.Views.Home;
    using MyCoolWebServer.Server.Enums;
    using MyCoolWebServer.Server.Http.Contracts;
    using MyCoolWebServer.Server.Http.Response;
    using System.IO;

    public abstract class Controller
    {
        public IHttpResponse FileViewResponse(string fileName)
        {
            var fileHtml = File.ReadAllText(fileName);

            return new ViewResponse(HttpStatusCode.Ok, new IndexView(fileHtml));
        }
    }
}
