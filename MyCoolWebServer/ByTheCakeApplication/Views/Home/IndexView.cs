namespace MyCoolWebServer.ByTheCakeApplication.Views.Home
{
    using MyCoolWebServer.Server.Contracts;

    public class IndexView : IView
    {
        private readonly string htmlFile;

        public IndexView(string htmlFile)
        {
            this.htmlFile = htmlFile;
        }

        public string View() => this.htmlFile;
    }
}
