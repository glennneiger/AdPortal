using System.Threading.Tasks;

namespace AdPortal.Infrastructure.Command
{
    public interface ICommandDispatcher : ICommand
    {
        Task DispatcheAsync<T>(T command) where T : ICommand;
    }
}