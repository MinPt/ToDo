using System;
using System.Collections.Generic;

namespace ToDo.Data.Entities
{
    public class ToDoList
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateChanged { get; set; }

        public AppUser Owner { get; set; }
        public string OwnerId { get; set; }
        
        public ICollection<ToDoItem> ToDoItems { get; set; }
    }
}