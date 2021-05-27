using System.Threading.Tasks;
using ToDo.Core.Common;
using ToDo.DTO;

namespace ToDo.Core
{
    public interface IAuthService
    {
        Task<Result<AppUserDto>> Register(string userName, string email, string password);
        Task<Result<AppUserLoggedDto>> Login(string email, string password);
    }
}