using System.Threading.Tasks;
using Hangfire;
using MediatR;
using MessageDispatchingEngine.Contracts;

namespace MessageDispatchingEngine.Service.Infrastructure
{
    public class HangFireDispatcher : IMessageDispatcher
    {
        public Task Dispatch(string jobIdentifier, IRequest request)
        {
            var backgroundJobClient = new BackgroundJobClient();
            backgroundJobClient.Enqueue<MediatRBridge>(x => x.Send(jobIdentifier,request));
            return Task.CompletedTask;
        }

        public Task Dispatch<T>(string jobIdentifier, IRequest<T> request)
        {
            var backgroundJobClient = new BackgroundJobClient();
            backgroundJobClient.Enqueue<MediatRBridge>(x => x.Send(jobIdentifier,request));
            return Task.CompletedTask;
        }
    }
}