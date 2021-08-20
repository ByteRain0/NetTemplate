using System;

namespace ExecutionPipeline.MediatRPipeline.ExceptionHandling
{
    /// <summary>
    /// At this point in time this part is pretty raw and still not really all that much useful.
    /// I have a small plan for the use of this in the near future, but since this is just a small PoC part of the whole solution it will be left for later.
    /// </summary>
    public static class ResponseExtensions
    {
        public static Response OnSuccess(this Response response, Action action)
        {
            if (response.IsSuccess)
            {
                action.Invoke();
            }

            return response;
        }
        
        public static Response OnFailure(this Response response, Action action)
        {
            if (response.IsFailure)
            {
                action.Invoke();
            }

            return response;
        }
    }
}