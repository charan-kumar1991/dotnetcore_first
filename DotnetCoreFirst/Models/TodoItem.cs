using System;
using System.ComponentModel.DataAnnotations;

namespace DotnetCoreFirst.Models
{
    public class TodoItem
    {
        public Guid Id { get; set; }
        
        [Required]
        [MinLength(6)]
        public string Title { get; set; }

        [Required]
        public bool Completed { get; set; }
        public TodoItem(Guid id, string title, bool completed)
        {
            Id = id;
            Title = title;
            Completed = completed;
        }
    }
}
