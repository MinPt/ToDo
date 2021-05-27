using System.Linq;
using ToDo.Data.Entities;
using ToDo.DTO;

namespace ToDo.Extensions
{
    public static class EntityExtensions
    {
        public static ToDoListDto MapToDto(this ToDoList toDoList)
        {
            return new ToDoListDto
            {
                Id = toDoList.Id,
                Name = toDoList.Name,
                DateCreated = toDoList.DateCreated,
                DateChanged = toDoList.DateChanged,
                UserId = toDoList.OwnerId,
                ToDoItems = toDoList.ToDoItems?.Select(i => i.MapToDto()).ToList()
            };
        }

        public static ToDoItemDto MapToDto(this ToDoItem toDoItem)
        {
            return new ToDoItemDto
            {
                Id = toDoItem.Id,
                Text = toDoItem.Text,
                IsCompleted = toDoItem.IsCompleted,
                ToDoListId = toDoItem.ToDoListId
            };
        }
    }
}