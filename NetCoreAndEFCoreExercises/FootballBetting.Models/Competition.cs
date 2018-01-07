namespace FootballBetting.Models
{
    using FootballBetting.Models.Enum;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Competition
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public CompetitionType Type { get; set; }

        public List<Game> Games { get; set; }
    }
}
