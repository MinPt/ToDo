using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ToDo.Core;
using ToDo.DTO;
using ToDo.Extensions;
using ToDo.Requests;

namespace ToDo.Controllers
{
    public class ToDoListsController : ApiControllerBase
    {
        private readonly IToDoListsService _toDoListsService;

        public ToDoListsController(IToDoListsService toDoListsService)
        {
            _toDoListsService = toDoListsService;
        }

        [HttpGet]
        public async Task<IActionResult> Lists()
        {
            var result = await _toDoListsService.GetLists(CurrentUser);
            return result.Succeeded
                ? (IActionResult) Ok(result.ToApiResponse())
                : BadRequest(result.ToApiError());
        }

        [HttpGet("{id:min(1)}")]
        public async Task<IActionResult> List([FromRoute] int id)
        {
            var result = await _toDoListsService.GetList(CurrentUser, id);
            return result.Succeeded
                ? (IActionResult) Ok(result.ToApiResponse())
                : BadRequest(result.ToApiError());
        }

        [HttpPost]
        public async Task<IActionResult> CreateList([FromBody] CreateToDoListRequest request)
        {
            var dto = new ToDoListDto {Name = request.Name};
            var result = await _toDoListsService.CreateList(CurrentUser, dto);

            return result.Succeeded
                ? (IActionResult) CreatedAtAction(nameof(List), new {id = result.Data.Id}, result.ToApiResponse())
                : BadRequest(result.ToApiError());
        }

        [HttpPut]
        public async Task<IActionResult> EditList([FromBody] EditToDoListRequest request)
        {
            var dto = new ToDoListDto {Id = request.Id, Name = request.Name};
            var result = await _toDoListsService.EditList(CurrentUser, dto);

            return result.Succeeded
                ? (IActionResult) Ok(result.ToApiResponse())
                : BadRequest(result.ToApiError());
        }

        [HttpDelete("{id:min(1)}")]
        public async Task<IActionResult> DeleteList([FromRoute] int id)
        {
            var result = await _toDoListsService.DeleteList(CurrentUser, id);
            return result.Succeeded
                ? (IActionResult) Ok(result.ToApiResponse())
                : BadRequest(result.ToApiError());
        }
    }
}