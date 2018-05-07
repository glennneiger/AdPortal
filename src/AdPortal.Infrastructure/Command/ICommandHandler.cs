using System.Threading.Tasks;

namespace AdPortal.Infrastructure.Command
{
    public interface ICommandHandler<T> where T : ICommand
    {
         Task HandleAsync(T Command);
    }
}