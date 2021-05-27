using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using ToDo.Core;
using ToDo.Core.Common;
using ToDo.Data.Entities;
using ToDo.DTO;

namespace ToDo.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IJwtService _jwtService;

        public AuthService(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager,
            IJwtService jwtService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtService = jwtService;
        }

        public async Task<Result<AppUserDto>> Register(string userName, string email, string password)
        {
            var user = new AppUser {UserName = userName, Email = email};
            var result = await _userManager.CreateAsync(user, password);
            if (!result.Succeeded)
            {
                var error = result.Errors.First();
                return Result<AppUserDto>.Failure("", error.Description);
            }

            var dto = new AppUserDto {Email = user.Email, UserName = user.UserName};
            return Result<AppUserDto>.Success(dto);
        }

        public async Task<Result<AppUserLoggedDto>> Login(string email, string password)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return Result<AppUserLoggedDto>.Failure(nameof(email), "Incorrect email");
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, password, false);
            if (!result.Succeeded)
            {
                return Result<AppUserLoggedDto>.Failure(nameof(password), "Incorrect password");
            }

            var dto = new AppUserLoggedDto
            {
                Email = user.Email,
                UserName = user.UserName,
                Token = _jwtService.CreateToken(user)
            };
            return Result<AppUserLoggedDto>.Success(dto);
        }
    }
}