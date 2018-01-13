﻿namespace MyCoolWebServer.ByTheCakeApplication.Views.Home
{
    using MyCoolWebServer.Server.Contracts;

    public class AboutView : IView
    {
        private readonly string htmlFile;

        public AboutView(string htmlFile)
        {
            this.htmlFile = htmlFile;
        }

        public string View() => this.htmlFile;
    }
}
