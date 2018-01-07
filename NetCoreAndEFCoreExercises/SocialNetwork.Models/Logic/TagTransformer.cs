using System.Text;

namespace SocialNetwork.Models.Logic
{
    public static class TagTransformer
    {
        public static string Transform(string value)
        {
            var sb = new StringBuilder();
            sb.Append(value);

            if (!value.StartsWith('#'))
            {
                sb = sb.Insert(0, '#');
            }

            if (sb.Length > 20)
            {
                sb.Length = 20;
            }

            sb.Replace(" ", "");

            return sb.ToString();
        }
    }
}
