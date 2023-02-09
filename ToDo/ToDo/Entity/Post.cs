using System.ComponentModel.DataAnnotations;

namespace ToDo.Entity
{
    public class Post
    {
        [Key]
        public int Id { get; set; }

        public string Title { get; set; }

        public bool IsCompleted { get; set; }

        public User User { get; set; }

        public Tag Tag { get; set; }

        public virtual List<Notification> Notifications { get; set; } = new List<Notification>();

    }
}
