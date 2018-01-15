using System.Collections.Generic;

namespace MyCoolWebServer.ByTheCakeApplication.Models
{
    public class ShoppingCart
    {
        public const string SessionKey = "^%Current_Shopping_Cart%^";

        public List<Cake> Orders { get; private set; } = new List<Cake>();
    }
}
