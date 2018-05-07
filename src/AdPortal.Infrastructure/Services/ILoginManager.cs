using System.Security.Claims;
using System.Threading.Tasks;

namespace AdPortal.Infrastructure.Services
{
    public interface ILoginManager : IService
    {
         Task<ClaimsPrincipal> GetClaimsPrincipalAsync(string email);
         string GetAuthName();

    }
}