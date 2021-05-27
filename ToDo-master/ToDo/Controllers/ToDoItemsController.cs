using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ToDo.Core;
using ToDo.DTO;
using ToDo.Extensions;
using ToDo.Requests;

namespace ToDo.Controllers
{
    public class ToDoItemsController : ApiControllerBase
    {
        private readonly IToDoItemService _toDoItemService;

        public ToDoItemsController(IToDoItemService toDoItemService)
        {
            _toDoItemService = toDoItemService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateItem([FromBody] CreateToDoItemRequest request)
        {
            var dto = new ToDoItemDto
            {
                Text = request.Text,
                IsCompleted = request.IsCompleted,
                ToDoListId = request.ToDoListId
            };
            var result = await _toDoItemService.CreateItem(CurrentUser, dto);
            return result.Succeeded
                ? (IActionResult) Ok(result.ToApiResponse())
                : BadRequest(result.ToApiError());
        }

        [HttpPut]
        public async Task<IActionResult> EditItem([FromBody] EditToDoItemRequest request)
        {
            var dto = new ToDoItemDto
            {
                Id = request.Id,
                Text = request.Text,
                IsCompleted = request.IsCompleted
            };
            var result = await _toDoItemService.EditItem(CurrentUser, dto);
            return result.Succeeded
                ? (IActionResult) Ok(result.ToApiResponse())
                : BadRequest(result.ToApiError());
        }

        [HttpDelete("{id:min(1)}")]
        public async Task<IActionResult> DeleteItem([FromRoute] int id)
        {
            var result = await _toDoItemService.DeleteItem(CurrentUser, id);
            return result.Succeeded
                ? (IActionResult) Ok(result.ToApiResponse())
                : BadRequest(result.ToApiError());
        }
    }
}