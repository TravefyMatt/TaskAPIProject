using System;
using System.ComponentModel.DataAnnotations;

namespace TaskApi.Models
{
    public class TaskItem
    {
        public int Id { get; set; }
        [Required] 
        [MaxLength(500)]
        public string Title { get; set; } = string.Empty;
        public DateTime? DueDate { get; set; }
        public bool IsCompleted { get; set; }
        public DateTime? DateCompleted { get; set; }
    }
}
