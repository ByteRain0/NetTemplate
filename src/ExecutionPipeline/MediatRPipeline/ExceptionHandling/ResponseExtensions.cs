using System;
using System.Threading.Tasks;

namespace ExecutionPipeline.MediatRPipeline.ExceptionHandling;

/// <summary>
/// At this point in time this part is pretty raw and still not really all that much useful.
/// I have a small plan for the use of this in the near future, but since this is just a small PoC part of the whole solution it will be left for later.
/// </summary>
public static class ResponseActExtensions
{
    public static async Task<T> Act<T>(this Task<Response> response, Func<Task<T>> onSuccess,
        Func<Task<T>>? onFailure)
    {
        if (response == null)
        {
            if (onFailure == null)
            {
                return default;
            }
                
            return await onFailure.Invoke();
        }

        var wasSuccessfullyExecuted = await response;

        if (wasSuccessfullyExecuted.IsSuccess)
        {
            return await onSuccess.Invoke();
        }

        if (onFailure == null)
        {
            return default;
        }
            
        return await onFailure.Invoke();
    }
        
    public static async Task<T> Act<T, R>(this Task<Response<R>> response, Func<Task<T>> onSuccess,
        Func<Task<T>>? onFailure)
    {
        if (response == null)
        {
            if (onFailure == null)
            {
                return default;
            }
                
            return await onFailure.Invoke();
        }

        var wasSuccessfullyExecuted = await response;

        if (wasSuccessfullyExecuted.IsSuccess)
        {
            return await onSuccess.Invoke();
        }

        if (onFailure == null)
        {
            return default;
        }
            
        return await onFailure.Invoke();
    }
        
    public static async Task<ACT_Response> Act<ACT_Response, PREVIOUS_RESPONSE>(this Task<Response<PREVIOUS_RESPONSE>> response, Func<Response<PREVIOUS_RESPONSE>,Task<ACT_Response>> onSuccess,
        Func<Response<PREVIOUS_RESPONSE>,Task<ACT_Response>>? onFailure)
    {
        if (response == null)
        {
            if (onFailure == null)
            {
                return default;
            }
                
            return await onFailure.Invoke(default);
        }

        var wasSuccessfullyExecuted = await response;

        if (wasSuccessfullyExecuted.IsSuccess)
        {
            return await onSuccess.Invoke(wasSuccessfullyExecuted);
        }

        if (onFailure == null)
        {
            return default;
        }
            
        return await onFailure.Invoke(default);
    }

    public static void Act<PREVIOUS_RESPONSE>(this Response<PREVIOUS_RESPONSE> response, Action<PREVIOUS_RESPONSE> onSuccess, Action onFailure = null)
    {
        if (response == null)
        {
            if (onFailure == null)
            {
                onFailure();
            }

            onFailure.Invoke();
        }

        var wasSuccessfullyExecuted = response;

        if (wasSuccessfullyExecuted.IsSuccess)
        {
            onSuccess.Invoke(wasSuccessfullyExecuted.Value);
            return;
                
        }

        if (onFailure == null)
        {
            return;
        }
            
        onFailure.Invoke();
    }
        
    public static async void Act<PREVIOUS_RESPONSE>(this Task<Response<PREVIOUS_RESPONSE>> response, Action<PREVIOUS_RESPONSE> onSuccess, Action onFailure = null)
    {
        if (response == null)
        {
            if (onFailure == null)
            {
                onFailure();
            }

            onFailure.Invoke();
        }

        var wasSuccessfullyExecuted = await response;

        if (wasSuccessfullyExecuted.IsSuccess)
        {
            onSuccess.Invoke(wasSuccessfullyExecuted.Value);
            return;
                
        }

        if (onFailure == null)
        {
            return;
        }
            
        onFailure.Invoke();
    }
        
}