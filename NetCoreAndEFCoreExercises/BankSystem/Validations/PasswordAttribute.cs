namespace BankSystem.Validations
{
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

    public class PasswordAttribute : ValidationAttribute
    {
        public PasswordAttribute()
        {
            this.ErrorMessage = "Password must contains at least 1 digit, 1 upper letter and 1 lower letter";
        }

        public override bool IsValid(object value)
        {
            var password = value as string;

            if (password == null)
            {
                return true;
            }

            return password.Any(s => char.IsDigit(s)) && password.Any(s => char.IsUpper(s)) && password.Any(s => char.IsLower(s));
        }
    }
}
