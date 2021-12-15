using System.Threading.Tasks;
using ExecutionPipeline.MediatRPipeline.ExceptionHandling;
using MediatR;

namespace MessageDispatcher.Contracts;

public interface IMessageDispatcher
{
    Response Dispatch(string jobIdentifier, IRequest request);
        
    Response Dispatch<T>(string jobIdentifier, IRequest<T> request);
}