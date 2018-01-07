namespace FootballBetting.Models
{
    using FootballBetting.Models.Validations;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class User
    {
        public int Id { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        [Password]
        [MinLength(4)]
        public string Password { get; set; }

        [Required]
        [Email]
        public string Email { get; set; }

        [Required]
        public string FullName { get; set; }

        public decimal Balance { get; set; }

        public List<Bet> Bets { get; set; }
    }
}
