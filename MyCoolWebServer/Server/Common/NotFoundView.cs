namespace MyCoolWebServer.Server.Common
{
    using MyCoolWebServer.Server.Contracts;

    public class NotFoundView : IView
    {
        public string View()
        {
            return "<h2>404 This page was not found :/</h2>";
        }
    }
}
