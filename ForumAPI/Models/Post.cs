using System.ComponentModel.DataAnnotations;

namespace ForumAPI.Models
{
    public class Post
    {
        public int Id { get; set; }
        public int ThreadId { get; set; }
        public string Author { get; set; }
        public string Text { get; set; }

        public int isFlagged { get; set; } = 0;
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public Thread Thread { get; set; }
    }
}
