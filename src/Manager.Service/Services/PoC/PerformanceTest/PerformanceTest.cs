using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using ExecutionPipeline.MediatRPipeline.ExceptionHandling;
using Manager.Service.Services.History.Commands.RecordEvent;
using MediatR;
using Voyager.Api;

namespace Manager.Service.Services.PoC.PerformanceTest
{
    /// <summary>
    /// Small basic timed test to test the performance of running 1k request in sequence.
    /// </summary>
    [VoyagerRoute(HttpMethod.Get, "api/performance")]
    public class PerformanceTest : IRequest<Response<long>>
    {
        public class PerformanceTestHandler : IRequestHandler<PerformanceTest, Response<long>>
        {
            private readonly IMediator _mediator;

            public PerformanceTestHandler(IMediator mediator)
            {
                _mediator = mediator;
            }

            public async Task<Response<long>> Handle(PerformanceTest request, CancellationToken cancellationToken)
            {
                var template = new RecordEvent()
                {
                    Message = "Test",
                    EntityType = "Test",
                    EventName = "Test",
                    EntityPrimaryKey = "Test"
                };
                var timer = new Stopwatch();
                
                timer.Start();

                for (int i = 0; i < 1000; i++)
                {
                    await _mediator.Send(template);
                }
                
                timer.Stop();

                Console.WriteLine($"Sequential run the mediatr commands : {timer.ElapsedMilliseconds}");

                return Response.Ok(timer.ElapsedMilliseconds);
            }
        }
    }
}