using System.Security.Claims;
using API.Interfaces;

namespace API.Services
{
    public class UserService : IUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public UserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public string FindEmailByClaims()
        {
            return _httpContextAccessor.HttpContext.User.Claims.First(x => x.Type == ClaimTypes.Email).Value;
        }
    }
}