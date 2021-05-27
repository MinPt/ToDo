using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ToDo.Data.Entities;

namespace ToDo.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class ApiControllerBase : ControllerBase
    {
        protected AppUser CurrentUser
        {
            get
            {
                var id = HttpContext.User.Claims.First(c => c.Type == "Id");
                var userName = HttpContext.User.Claims.First(c => c.Type == "UserName");
                var email = HttpContext.User.Claims.First(c => c.Type == "Email");

                return new AppUser {Id = id.Value, Email = email.Value, UserName = userName.Value};
            }
        }
    }
}