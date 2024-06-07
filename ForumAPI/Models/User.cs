using Microsoft.AspNetCore.Identity;

namespace ForumAPI.Models
{
    public class User : IdentityUser
    {
        public string Nickname { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? ProfileImage { get; set; }
        public int isBanned { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
