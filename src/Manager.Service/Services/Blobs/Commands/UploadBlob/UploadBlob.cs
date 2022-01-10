using System.Collections.Generic;
using ExecutionPipeline.MediatRPipeline.ExceptionHandling;
using ExecutionPipeline.MediatRPipeline.Retry;
using MediatR;
using Voyager.Api;

namespace Manager.Service.Services.Blobs.Commands.UploadBlob;

[VoyagerRoute(HttpMethod.Post,"api/UploadBlob")]
public class UploadBlob : IRequest<Response>, IRetryMarker
{
    public string Stream { get; set; }
    public string CustomPath { get; set; }

    public string ContentType { get; set; }

    public string Extension { get; set; }

    public string FileName { get; set; }

    public Dictionary<string,string> Tags { get; set; }
}