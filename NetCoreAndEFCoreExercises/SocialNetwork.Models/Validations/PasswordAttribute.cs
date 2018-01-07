namespace SocialNetwork.Models.Validations
{
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

    public class PasswordAttribute : ValidationAttribute
    {
        private readonly char[] requiredSymbols = new[] { '!', '@', '#', '$', '%', '^', '&', '*', '(', ')', '_', '+', '<', '>', '?' };

        public PasswordAttribute()
        {
            this.ErrorMessage = "Password is not valid. It should contain at least one lowercase letter, uppercase letter, digit and special symbol.";
        }

        public override bool IsValid(object value)
        {
            var password = value as string;
            if (password == null)
            {
                return true;
            }

            return password.All(s => char.IsLower(s) || char.IsUpper(s) || char.IsDigit(s) || this.requiredSymbols.Contains(s));
        }
    }
}
