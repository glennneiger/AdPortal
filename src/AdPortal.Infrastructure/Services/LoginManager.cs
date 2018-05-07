using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using AdPortal.Infrastructure.Settings;

namespace AdPortal.Infrastructure.Services
{
    public class LoginManager : ILoginManager
    {
        private CookiesAuthSettings _settings;
        private IUserService _userService;
        public LoginManager(CookiesAuthSettings settings, IUserService userService)
        {
            _settings = settings;
            _userService = userService;
        }
        public  string GetAuthName()
        {
            return _settings.AuthName;
        }

        public async Task<ClaimsPrincipal> GetClaimsPrincipalAsync(string email)
        {
            var user = await _userService.GetUserDTO(email);
            var claims = new List<Claim>
			{
                new Claim(ClaimTypes.NameIdentifier , user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName),
				new Claim(ClaimTypes.Email, email),
				new Claim(ClaimTypes.Role, user.Role.ToString())
			};
 
			var userIdentity = new ClaimsIdentity(claims,"Name");
 
		    return new ClaimsPrincipal(userIdentity);       
        }

       
    }
}