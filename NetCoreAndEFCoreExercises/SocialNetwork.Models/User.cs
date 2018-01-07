namespace SocialNetwork.Models
{
    using SocialNetwork.Models.Validations;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class User
    {
        public int Id { get; set; }

        [Required]
        [MinLength(4)]
        [MaxLength(30)]
        public string Username { get; set; }

        [Required]
        [Password]
        [MinLength(6)]
        [MaxLength(50)]
        public string Password { get; set; }

        [Required]
        [Email]
        public string Email { get; set; }

        [MaxLength(1024)]
        public byte[] ProfilePicture { get; set; }

        public DateTime? RegisteredOn { get; set; }

        public DateTime? LastTimeLoggedIn { get; set; }

        [MinLength(1)]
        [MaxLength(120)]
        public int? Age { get; set; }

        public bool IsDeleted { get; set; }

        public List<Friendship> ToFriends { get; set; } = new List<Friendship>();

        public List<Friendship> FromFriends { get; set; } = new List<Friendship>();

        public List<Album> Albums { get; set; } = new List<Album>();
    }
}
