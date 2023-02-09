using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace ToDo.Entity
{

    public class User
    {
        [Key]
        [NotNull]
        public int TelegramId { get; set; }
        public string? firstName { get; set; }
        public string? secondName { get; set; }
        public string? email { get; set; }

        public virtual List<Post> Posts { get; set; } = new List<Post>();
    }
}
