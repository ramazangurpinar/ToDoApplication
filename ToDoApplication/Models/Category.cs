using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ToDoApplication.Models
{
    public class Category
    {
        public int CategoryId { get; set; }
        [Required]
        public string Name { get; set; }
        public IList<TodoList> ToDoLists { get; set; }
    }
}
