using System.Collections.Generic;
using System.Threading.Tasks;
using ToDo.Core.Common;
using ToDo.Data.Entities;
using ToDo.DTO;

namespace ToDo.Core
{
    public interface IToDoListsService
    {
        Task<Result<IEnumerable<ToDoListDto>>> GetLists(AppUser user);
        Task<Result<ToDoListDto>> GetList(AppUser user, int listId);
        Task<Result<ToDoListDto>> CreateList(AppUser user, ToDoListDto listDto);
        Task<Result<ToDoListDto>> EditList(AppUser user, ToDoListDto listDto);
        Task<Result<object>> DeleteList(AppUser user, int listId);
    }
}