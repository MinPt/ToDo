using System;
using System.Collections.Generic;

namespace ToDo.DTO
{
    public class ToDoListDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateChanged { get; set; }
        
        
        public string UserId { get; set; }
        
        public ICollection<ToDoItemDto> ToDoItems { get; set; }
    }
}