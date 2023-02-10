using ToDo.Entity;

namespace ToDo.Model
{
    public class PostModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public bool IsCompleted { get; set; }

        public Tag Tag { get; set; }
    }
}
