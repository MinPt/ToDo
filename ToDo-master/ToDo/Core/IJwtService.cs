using ToDo.Data.Entities;

namespace ToDo.Core
{
    public interface IJwtService
    {
        string CreateToken(AppUser user);
    }
}