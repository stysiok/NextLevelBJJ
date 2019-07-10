using System.Threading.Tasks;

namespace NextLevelBJJ.Application
{
    public interface ICommandHandler<T> where T : ICommand
    {
        Task HandleAsync(T command);
    }
}
