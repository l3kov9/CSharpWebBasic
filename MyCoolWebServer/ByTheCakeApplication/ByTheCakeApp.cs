namespace MyCoolWebServer.ByTheCakeApplication
{
    using MyCoolWebServer.ByTheCakeApplication.Controllers;
    using MyCoolWebServer.Server.Contracts;
    using MyCoolWebServer.Server.Routing.Contracts;

    public class ByTheCakeApp : IApplication
    {
        public void Configure(IAppRouteConfig appRouteConfig)
        {
            appRouteConfig
                .Get("/", req => new HomeController().Index());

            appRouteConfig
                .Get("/about", req => new HomeController().About());

            appRouteConfig
                .Get("/add", req => new CakesController().Add());

            appRouteConfig
                .Post("/add", req => new CakesController().Add(req.FormData["name"], req.FormData["price"]));

            appRouteConfig
                .Get("/search", req => new CakesController().Search(req.UrlParameters));

            appRouteConfig
                .Get("/calculator", req => new CalculatorController().Index());

            appRouteConfig
                .Post("/calculator", req => new CalculatorController().Index(req.FormData["firstNumber"], req.FormData["operation"], req.FormData["secondNumber"]));
        }
    }
}
