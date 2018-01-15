namespace MyCoolWebServer.ByTheCakeApplication.Controllers
{
    using Helpers;
    using MyCoolWebServer.ByTheCakeApplication.Models;
    using MyCoolWebServer.Server.Http;
    using Server.Http.Contracts;
    using Server.Http.Response;
    using System.Collections.Generic;
    using System;

    public class AccountController : Controller
    {
        public IHttpResponse Login()
        {
            this.ViewData["showError"] = "none";
            this.ViewData["authDisplay"] = "none";

            return this.FileViewResponse(@"account\login");
        }

        public IHttpResponse Login(IHttpRequest req)
        {
            const string formNameKey = "name";
            const string formPasswordKey = "password";

            if (!req.FormData.ContainsKey(formNameKey) || !req.FormData.ContainsKey(formPasswordKey))
            {
                return new BadRequestResponse();
            }

            var name = req.FormData[formNameKey];
            var password = req.FormData[formPasswordKey];

            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(password))
            {
                this.ViewData["showError"] = "block";
                this.ViewData["error"] = "Invalid name or password";

                return this.FileViewResponse(@"account\login");
            }

            req.Session.Add(SessionStore.CurrentUserKey, name);
            req.Session.Add(ShoppingCart.SessionKey, new ShoppingCart());

            return new RedirectResponse("/");
        }

        public  IHttpResponse Logout(IHttpRequest req)
        {
            req.Session.Clear();

            return new RedirectResponse("/login");
        }
    }
}
