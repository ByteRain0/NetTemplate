using System.Threading.Tasks;
using MediatR;

namespace MessageDispatcher.Contracts
{
    public interface IMessageDispatcher
    {
        Task Dispatch(string jobIdentifier, IRequest request);
        
        Task Dispatch<T>(string jobIdentifier, IRequest<T> request);
    }
}