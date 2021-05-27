using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ToDo.Core;
using ToDo.Core.Common;
using ToDo.Data;
using ToDo.Data.Entities;
using ToDo.DTO;
using ToDo.Extensions;

namespace ToDo.Services
{
    public class ToDoItemService : IToDoItemService
    {
        private readonly ToDoDbContext _dbContext;
        private readonly UserManager<AppUser> _userManager;

        public ToDoItemService(ToDoDbContext dbContext, UserManager<AppUser> userManager)
        {
            _dbContext = dbContext;
            _userManager = userManager;
        }

        public async Task<Result<ToDoItemDto>> CreateItem(AppUser user, ToDoItemDto toDoItemDto)
        {
            var userInDb = await _userManager.FindByEmailAsync(user.Email);
            if (userInDb == null)
            {
                return Result<ToDoItemDto>.Failure(nameof(user), "User doesn't exist");
            }

            var list = await _dbContext.ToDoLists
                .Where(l => l.OwnerId == userInDb.Id)
                .SingleOrDefaultAsync(l => l.Id == toDoItemDto.ToDoListId);
            if (list == null)
            {
                return Result<ToDoItemDto>.Failure(nameof(toDoItemDto.ToDoListId), "List doesn't exist");
            }

            var item = new ToDoItem
            {
                Text = toDoItemDto.Text,
                IsCompleted = toDoItemDto.IsCompleted
            };
            list.ToDoItems = new List<ToDoItem> {item};
            list.DateChanged = DateTime.UtcNow;
            
            await _dbContext.SaveChangesAsync();

            return Result<ToDoItemDto>.Success(item.MapToDto());
        }

        public async Task<Result<ToDoItemDto>> EditItem(AppUser user, ToDoItemDto toDoItemDto)
        {
            var userInDb = await _userManager.FindByEmailAsync(user.Email);
            if (userInDb == null)
            {
                return Result<ToDoItemDto>.Failure(nameof(user), "User doesn't exist");
            }

            var item = await _dbContext.ToDoItems
                .Include(t => t.ToDoList)
                .Where(t => t.ToDoList.OwnerId == user.Id)
                .SingleOrDefaultAsync(t => t.Id == toDoItemDto.Id);
            if (item == null)
            {
                return Result<ToDoItemDto>.Failure(nameof(toDoItemDto.Id), "Item doesn't exist");
            }

            item.Text = toDoItemDto.Text;
            item.IsCompleted = toDoItemDto.IsCompleted;
            item.ToDoList.DateChanged = DateTime.UtcNow;
            await _dbContext.SaveChangesAsync();

            return Result<ToDoItemDto>.Success(item.MapToDto());
        }

        public async Task<Result<object>> DeleteItem(AppUser user, int id)
        {
            var userInDb = await _userManager.FindByEmailAsync(user.Email);
            if (userInDb == null)
            {
                return Result<object>.Failure(nameof(user), "User doesn't exist");
            }

            var item = await _dbContext.ToDoItems
                .AsNoTracking()
                .Include(t => t.ToDoList)
                .Where(t => t.ToDoList.OwnerId == user.Id)
                .SingleOrDefaultAsync(t => t.Id == id);
            if (item == null)
            {
                return Result<object>.Failure(nameof(id), "Item doesn't exist");
            }

            _dbContext.ToDoItems.Remove(item);
            await _dbContext.SaveChangesAsync();

            return Result<object>.Success();
        }
    }
}