namespace FootballBetting.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Country
    {
        [Key]
        [StringLength(3), MinLength(3)]
        public string Id { get; set; }

        [Required]
        public string Name { get; set; }

        public int ContinentId { get; set; }

        public Continent Continent { get; set; }

        public List<Town> Towns { get; set; } = new List<Town>();
    }
}
