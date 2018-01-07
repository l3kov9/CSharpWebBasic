namespace FootballBetting.Models
{
    using System;
    using System.Collections.Generic;

    public class Game
    {
        public int Id { get; set; }

        public int HomeTeamId { get; set; }

        public Team HomeTeam { get; set; }

        public int AwayTeamId { get; set; }

        public Team AwayTeam { get; set; }

        public int HomeGoals { get; set; }

        public int AwayGoals { get; set; }

        public DateTime GameTime { get; set; }

        public double HomeTeamBetRate { get; set; }

        public double DrawBetRate { get; set; }

        public double AwayTeamBetRate { get; set; }

        public int RoundId { get; set; }

        public Round Round { get; set; }

        public int CompetitionId { get; set; }

        public Competition Competition { get; set; }

        public List<BetGame> Bets { get; set; } = new List<BetGame>();

        public List<PlayerStatistic> PlayerStatistics { get; set; } = new List<PlayerStatistic>();
    }
}
