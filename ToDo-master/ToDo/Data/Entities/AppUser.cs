using Microsoft.AspNetCore.Identity;

namespace ToDo.Data.Entities
{
    public class AppUser : IdentityUser
    {
        public string Name { get; set; }
    }
}