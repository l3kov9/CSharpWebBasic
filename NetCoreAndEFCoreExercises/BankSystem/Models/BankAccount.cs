namespace BankSystem.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public abstract class BankAccount
    {
        public int Id { get; set; }

        [Required]
        public string AccountNumber { get; set; }

        public decimal Balance { get; set; }

        public int UserId { get; set; }

        public User User { get; set; }

        public virtual void Deposit(decimal amount)
        {
            this.Balance += amount;
        }

        public virtual void WithDraw(decimal amount)
        {
            if (this.Balance >= amount)
            {
                this.Balance -= amount;
            }

            throw new ArgumentException("Balance cant be less than zero.");
        }
    }
}
