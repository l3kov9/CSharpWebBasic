namespace MyCoolWebServer.ByTheCakeApplication.Controllers
{
    using Server.Http.Contracts;
    using Helpers;

    public class HomeController : Controller
    {
        public IHttpResponse Index()
        {
            return this.FileViewResponse(@"home\index");
        }

        public IHttpResponse About()
            => this.FileViewResponse(@"Home\about");
    }
}
