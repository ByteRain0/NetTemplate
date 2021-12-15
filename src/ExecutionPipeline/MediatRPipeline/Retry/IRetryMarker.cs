namespace ExecutionPipeline.MediatRPipeline.Retry;

/// <summary>
/// Marker that indicates the fact that this request should have retry policies assigned to it.
/// </summary>
public interface IRetryMarker
{
}