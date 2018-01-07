using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FootballBetting.Models
{
    public class Team
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public byte[] Logo { get; set; }

        [StringLength(3), MinLength(3)]
        public string Initials { get; set; }

        public int PrimaryKitColorId { get; set; }

        public Color PrimaryKitColor { get; set; }

        public int? SecondaryKitColorId { get; set; }

        public Color SecondaryKitColor { get; set; }

        public int TownId { get; set; }

        public Town Town { get; set; }

        public decimal Budget { get; set; }

        public List<Player> Players { get; set; }

        public List<Game> Hometeams { get; set; } = new List<Game>();

        public List<Game> AwayTeams { get; set; } = new List<Game>();
    }
}
