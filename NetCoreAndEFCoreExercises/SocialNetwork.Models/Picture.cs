using System.Collections.Generic;

namespace SocialNetwork.Models
{
    public class Picture
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Caption { get; set; }

        public string Path { get; set; }

        public List<AlbumPicture> Albums { get; set; } = new List<AlbumPicture>();
    }
}
