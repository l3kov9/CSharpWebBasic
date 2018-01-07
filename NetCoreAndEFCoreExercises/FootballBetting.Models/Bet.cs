namespace FootballBetting.Models
{
    using System;
    using System.Collections.Generic;

    public class Bet
    {
        public int Id { get; set; }

        public decimal Money { get; set; }

        public DateTime BetTime { get; set; }

        public int UserId { get; set; }

        public User User { get; set; }

        public List<BetGame> BetGames { get; set; }
    }
}
