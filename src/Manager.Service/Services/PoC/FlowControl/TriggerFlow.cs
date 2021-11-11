using System;
using System.Threading;
using System.Threading.Tasks;
using ExecutionPipeline.MediatRPipeline.ExceptionHandling;
using MediatR;
using Voyager.Api;

namespace Manager.Service.Services.PoC.FlowControl
{
    [VoyagerRoute(HttpMethod.Post,"api/TriggerFlow")]
    public class TriggerFlow : IRequest<Response>
    {
        // Example of simple flow where the steps have no dependencies on each other.
    }

    internal class TriggerFlowHandler : IRequestHandler<TriggerFlow, Response>
    {
        public async Task<Response> Handle(TriggerFlow request, CancellationToken cancellationToken)
        {
            return
                await Action1()
                    .Act(
                        onSuccess: async () => await Action2()
                            .Act(
                                onSuccess: async () => await Action4(),
                                onFailure: async () => await Action5()
                            ),
                        onFailure: async () => await Action3()
                            .Act(
                                onSuccess: async () => await Action6(),
                                onFailure:async () => await Action7()
                            )
                    );
        }
        
        public async Task<Response> Action1()
        {
            Console.WriteLine("Run_Action_1 : Response.Ok");
            return Response.Ok();
        }
        
        public async Task<Response> Action2()
        {
            Console.WriteLine("Run_Action_2 : Response.Fail");
            return Response.Fail("Error");
        }
        
        public async Task<Response> Action3()
        {
            Console.WriteLine("Run_Action_3");
            return Response.Fail("Something went wrong");
        }
        
        public async Task<Response> Action4()
        {
            Console.WriteLine("Run_Action_4");
            return Response.Ok();
        }
        
        public async Task<Response> Action5()
        {
            Console.WriteLine("Run_Action_5 : Revert_Action_1 : Response.Ok");
            return Response.Ok();
        }
        
        public async Task<Response> Action6()
        {
            Console.WriteLine("Run_Action_6");
            return Response.Ok();
        }
        
        public async Task<Response> Action7()
        {
            Console.WriteLine("Run_Action_7");
            return Response.Ok();
        }
    }
}