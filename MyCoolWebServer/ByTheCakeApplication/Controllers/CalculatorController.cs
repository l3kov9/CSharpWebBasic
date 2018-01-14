namespace MyCoolWebServer.ByTheCakeApplication.Controllers
{
    using MyCoolWebServer.ByTheCakeApplication.Helpers;
    using MyCoolWebServer.Server.Http.Contracts;
    using System.Collections.Generic;

    public class CalculatorController : Controller
    {
        public IHttpResponse Index()
            => this.FileViewResponse(@"\Calculator\index", new Dictionary<string, string>
            {
                ["showResult"] = "none"
            });

        public IHttpResponse Index(string firstNumber, string operation, string secondNumber)
        {
            if(firstNumber == null || secondNumber == null || operation == null)
            {
                return this.Index();
            }

            double result = 0;
            var firstNum = double.Parse(firstNumber);
            var secondNum = double.Parse(secondNumber);

            switch (operation)
            {
                case "+": result = firstNum + secondNum;
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

            return this.FileViewResponse(@"\Calculator\index", new Dictionary<string, string>
            {
                ["result"] = result.ToString(),
                ["showResult"] = "block"
            });
        }
    }
}
