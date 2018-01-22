namespace MyCoolWebServer.GameStoreApplication.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    public class User
    {
        public int Id { get; set; }

        [Required]
        [MinLength(2)]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(50)]
        public string Email { get; set; }

        [Required]
        [MinLength(6)]
        [MaxLength(50)]
        public string Password { get; set; }

        public bool IsAdmin { get; set; }
    }
}
