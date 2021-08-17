using MediatR;

namespace MessageDispatchingEngine.Contracts
{
    public static class MediatrQueueExtension
    {
        private static IMessageDispatcher _messageDispatcher;
        
        public static void Configure(IMessageDispatcher messageDispatcher)
        {
            _messageDispatcher = messageDispatcher;
        }
        
        public static void Enqueue(this IMediator mediator, string jobIdentifier, IRequest request)
        {
            _messageDispatcher.Dispatch(jobIdentifier, request);
        }
        
        public static void Enqueue<T>(this IMediator mediator, string jobIdentifier, IRequest<T> request)
        {
            _messageDispatcher.Dispatch(jobIdentifier, request);
        }
    }
}