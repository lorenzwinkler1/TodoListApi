using System;
using System.ComponentModel.DataAnnotations;

namespace TodoClassLib
{
    public class Todo
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(255)]
        public string Title { get; set; }
        public bool IsDone { get; set; }
        public DateTime Created { get; set; } = DateTime.UtcNow;
        public DateTime Due { get; set; } = DateTime.UtcNow.AddDays(1);
    }
}
  