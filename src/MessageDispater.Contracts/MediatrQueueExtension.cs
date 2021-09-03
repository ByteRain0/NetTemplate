using ExecutionPipeline.MediatRPipeline.ExceptionHandling;
using MediatR;

namespace MessageDispatcher.Contracts
{
    public static class MediatrQueueExtension
    {
        private static IMessageDispatcher _messageDispatcher;
        
        public static void Configure(IMessageDispatcher messageDispatcher)
        {
            _messageDispatcher = messageDispatcher;
        }
        
        public static Response Enqueue(this IMediator mediator, string jobIdentifier, IRequest request)
        {
            var operation = _messageDispatcher.Dispatch(jobIdentifier, request);
            return operation;
        }
        
        public static Response Enqueue<T>(this IMediator mediator, string jobIdentifier, IRequest<T> request)
        {
            var operation = _messageDispatcher.Dispatch(jobIdentifier, request);
            return operation;
        }
    }
}