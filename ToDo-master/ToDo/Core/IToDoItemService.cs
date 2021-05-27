using System.Threading.Tasks;
using ToDo.Core.Common;
using ToDo.Data.Entities;
using ToDo.DTO;

namespace ToDo.Core
{
    public interface IToDoItemService
    {
        Task<Result<ToDoItemDto>> CreateItem(AppUser user, ToDoItemDto toDoItemDto);
        Task<Result<ToDoItemDto>> EditItem(AppUser user, ToDoItemDto toDoItemDto);
        Task<Result<object>> DeleteItem(AppUser user, int id);
    }
}