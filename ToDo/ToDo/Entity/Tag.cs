using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace ToDo.Entity
{
    public class Tag
    {
        [Key]
        public int id { get; set; }
        public string title { get; set; }



        public virtual List<Post> Posts { get; set; } = new List<Post>();
    }
}
