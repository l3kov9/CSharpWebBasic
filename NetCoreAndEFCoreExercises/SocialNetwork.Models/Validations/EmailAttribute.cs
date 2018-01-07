namespace SocialNetwork.Models.Validations
{
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

    public class EmailAttribute : ValidationAttribute
    {
        private readonly char[] allowedUserSymbols = new char[] { '.', '-', '_' };

        public EmailAttribute()
        {
            this.ErrorMessage = "Email is not valid";
        }

        public override bool IsValid(object value)
        {
            var email = value as string;

            if(email == null)
            {
                return true;
            }

            var parts = email
                .Split('@');

            if(parts.Length != 2)
            {
                return false;
            }

            var user = parts[0];
            var host = parts[1];

            if(!char.IsLetterOrDigit(user[0]) || (!char.IsLetterOrDigit(user[user.Length - 1])))
            {
                return false;
            }

            for (int i = 1; i < user.Length-1; i++)
            {
                var symbol = user[i];

                if(!char.IsLetterOrDigit(symbol) || !allowedUserSymbols.Contains(symbol))
                {
                    return false;
                }
            }

            if(host.Length<3 || host[0]=='.' || host[host.Length - 1] == '.')
            {
                return false;
            }

            return true;
        }
    }
}

