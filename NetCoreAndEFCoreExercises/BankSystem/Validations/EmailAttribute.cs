namespace BankSystem.Validations
{
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

    public class EmailAttribute : ValidationAttribute
    {
        public EmailAttribute()
        {
            this.ErrorMessage = "Email must contains @";
        }

        public override bool IsValid(object value)
        {
            var email = value as string;

            if (email == null)
            {
                return true;
            }

            if (!email.Any(s => s == '@'))
            {
                return false;
            }

            return true;
        }
    }
}
