namespace MyCoolWebServer.ByTheCakeApplication.Controllers
{
    using MyCoolWebServer.ByTheCakeApplication.Helpers;
    using MyCoolWebServer.Server.Http.Contracts;
    using System.Collections.Generic;

    public class CalculatorController : Controller
    {
        public IHttpResponse Index()
        {
            this.ViewData["showResult"] = "none";

            return this.FileViewResponse(@"\Calculator\index");
        }

        public IHttpResponse Index(string firstNumber, string operation, string secondNumber)
        {
            if (firstNumber == null || secondNumber == null || operation == null)
            {
                return this.Index();
            }

            double result = 0;
            var firstNum = double.Parse(firstNumber);
            var secondNum = double.Parse(secondNumber);

            switch (operation)
            {
                case "+":
                    result = firstNum + secondNum;
                    break;
                case "-":
                    result = firstNum - secondNum;
                    break;
                case "*":
                    result = firstNum * secondNum;
                    break;
                case "/":
                    result = firstNum / secondNum;
                    break;
                default:
                    break;
            }

            this.ViewData["result"] = result.ToString();
            this.ViewData["showResult"] = "block";

            return this.FileViewResponse(@"\Calculator\index");
        }
    }
}
