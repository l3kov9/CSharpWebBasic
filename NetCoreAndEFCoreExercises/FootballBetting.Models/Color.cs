namespace FootballBetting.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Color
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public List<Team> PrimaryKitColors { get; set; } = new List<Team>();

        public List<Team> SecondaryKitColors { get; set; } = new List<Team>();
    }
}
