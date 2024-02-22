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
            if (_httpContextAccessor.HttpContext == null)
            {
                throw new InvalidProgramException();
            }
            return _httpContextAccessor.HttpContext.User.Claims.First(x => x.Type == ClaimTypes.Email).Value;
        }
    }
}