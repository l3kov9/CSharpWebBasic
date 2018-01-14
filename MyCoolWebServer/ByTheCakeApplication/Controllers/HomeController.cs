namespace MyCoolWebServer.ByTheCakeApplication.Controllers
{
    using Server.Http.Contracts;
    using Helpers;

    public class HomeController : Controller
    {
        public IHttpResponse Index()
            => this.FileViewResponse(@"Home\index");

        public IHttpResponse About()
            => this.FileViewResponse(@"Home\about");
    }
}
