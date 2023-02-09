using System;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace ToDo.Entity
{
    public class Notification
    {
        [Key]
        public int Id { get; set; }

        public Post Post { get; set; }

        public DateTime DataTimeNotification { get; set; }
    }
}
