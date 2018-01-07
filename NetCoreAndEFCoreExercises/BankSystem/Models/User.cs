namespace BankSystem.Models
{
    using BankSystem.Validations;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class User
    {
        public int Id { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        [Password]
        public string Password { get; set; }

        [Required]
        [Email]
        public string Email { get; set; }

        public List<CheckingAccount> CheckingAccounts { get; set; } = new List<CheckingAccount>();

        public List<SavingAccount> SavingAccounts { get; set; } = new List<SavingAccount>();
    }
}
