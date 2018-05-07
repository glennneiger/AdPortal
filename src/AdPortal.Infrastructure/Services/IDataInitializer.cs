using System.Threading.Tasks;

namespace AdPortal.Infrastructure.Services
{
    public interface IDataInitializer : IService
    {
         Task SeedAsync();
    }
}