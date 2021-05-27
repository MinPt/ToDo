using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bogus;
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
    public class ToDoListsService : IToDoListsService
    {
        private readonly ToDoDbContext _dbContext;
        private readonly UserManager<AppUser> _userManager;

        public ToDoListsService(UserManager<AppUser> userManager, ToDoDbContext dbContext)
        {
            _userManager = userManager;
            _dbContext = dbContext;
        }

        public async Task<Result<IEnumerable<ToDoListDto>>> GetLists(AppUser user)
        {
            var userInDb = await _userManager.FindByEmailAsync(user.Email);
            if (userInDb == null)
            {
                return Result<IEnumerable<ToDoListDto>>.Failure(nameof(user), "User doesn't exist");
            }

            var lists = await _dbContext.ToDoLists
                .AsNoTracking()
                .Where(l => l.OwnerId == user.Id)
                .ToListAsync();

            return Result<IEnumerable<ToDoListDto>>.Success(lists.Select(l => l.MapToDto()));
        }

        public async Task<Result<ToDoListDto>> GetList(AppUser user, int listId)
        {
            var userInDb = await _userManager.FindByEmailAsync(user.Email);
            if (userInDb == null)
            {
                return Result<ToDoListDto>.Failure(nameof(user), "User doesn't exist");
            }

            var list = await _dbContext.ToDoLists
                .AsNoTracking()
                .Where(l => l.OwnerId == user.Id)
                .Include(l => l.ToDoItems)
                .SingleOrDefaultAsync(l => l.Id == listId);

            return list == null
                ? Result<ToDoListDto>.Failure(nameof(listId), "List doesn't exist")
                : Result<ToDoListDto>.Success(list.MapToDto());
        }

        public async Task<Result<ToDoListDto>> CreateList(AppUser user, ToDoListDto listDto)
        {
            var userInDb = await _userManager.FindByEmailAsync(user.Email);
            if (userInDb == null)
            {
                return Result<ToDoListDto>.Failure(nameof(user), "User doesn't exist");
            }

            var list = new ToDoList
            {
                Name = listDto.Name ?? new Faker().Lorem.Sentence(3),
                Owner = userInDb,
                DateCreated = DateTime.UtcNow
            };

            await _dbContext.ToDoLists.AddAsync(list);
            await _dbContext.SaveChangesAsync();

            return Result<ToDoListDto>.Success(list.MapToDto());
        }

        public async Task<Result<ToDoListDto>> EditList(AppUser user, ToDoListDto listDto)
        {
            var userInDb = await _userManager.FindByEmailAsync(user.Email);
            if (userInDb == null)
            {
                return Result<ToDoListDto>.Failure(nameof(user), "User doesn't exist");
            }

            var listInDb = await _dbContext.ToDoLists
                .Where(l => l.OwnerId == userInDb.Id)
                .SingleOrDefaultAsync(l => l.Id == listDto.Id);
            if (listInDb == null)
            {
                return Result<ToDoListDto>.Failure(nameof(listDto.Id), "List doesn't exist");
            }

            listInDb.Name = listDto.Name;
            listInDb.DateChanged = DateTime.UtcNow;

            await _dbContext.SaveChangesAsync();

            return Result<ToDoListDto>.Success(listInDb.MapToDto());
        }

        public async Task<Result<object>> DeleteList(AppUser user, int listId)
        {
            var userInDb = await _userManager.FindByEmailAsync(user.Email);
            if (userInDb == null)
            {
                return Result<object>.Failure(nameof(user), "User doesn't exist");
            }

            var listInDb = await _dbContext.ToDoLists
                .Where(l => l.OwnerId == userInDb.Id)
                .SingleOrDefaultAsync(l => l.Id == listId);
            if (listInDb == null)
            {
                return Result<object>.Failure(nameof(listId), "List doesn't exist");
            }

            _dbContext.ToDoLists.Remove(listInDb);
            await _dbContext.SaveChangesAsync();

            return Result<object>.Success();
        }
    }
}