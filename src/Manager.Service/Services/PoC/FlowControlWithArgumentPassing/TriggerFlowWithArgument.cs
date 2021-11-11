using System;
using System.Threading;
using System.Threading.Tasks;
using ExecutionPipeline.MediatRPipeline.ExceptionHandling;
using MediatR;
using Newtonsoft.Json;
using Voyager.Api;

namespace Manager.Service.Services.PoC.FlowControlWithArgumentPassing
{
    [VoyagerRoute(HttpMethod.Post, "api/TriggerFlowWithParametersPassing")]
    public class TriggerFlowWithArgument : IRequest<Response<TriggerFlowWithArgumentHandler.UserDto>>
    {
        // Example of flow in which there are parameters that are send from one step over to another.
    }

    internal class TriggerFlowWithArgumentHandler : IRequestHandler<TriggerFlowWithArgument, Response<TriggerFlowWithArgumentHandler.UserDto>>
    {
        public async Task<Response<UserDto>> Handle(TriggerFlowWithArgument request, CancellationToken cancellationToken)
        {
            var testFlow = await Act1()
                .Act
                (
                    onSuccess: async userInstanceAsResponseFromAct1 => await Act2(userInstanceAsResponseFromAct1.Value.FirstName)
                        .Act(
                            onSuccess: async jobInformationAsResponseFromAct2 => await Act3(name:userInstanceAsResponseFromAct1.Value.FullName, jobPosition:jobInformationAsResponseFromAct2.Value),
                            onFailure: async _ => await ReturnFailure()
                            ),
                    onFailure: async _ => await ReturnFailure()
                );

            return testFlow;
        }

        // Create user instance.
        public async Task<Response<User>> Act1()
        {
            var user = new User()
            {
                FirstName = "Vasilii",
                LastName = "Oleinic"
            };
            
            Console.WriteLine("Act_1_Provide_User_Info : Success");
            Console.WriteLine($"User Object : '{JsonConvert.SerializeObject(user)}'");
            return Response.Ok<User>(user);
        }

        // Get some additional data about the user namely job position.
        public async Task<Response<string>> Act2(string responseFromPreviousAct)
        {
            Console.WriteLine("Act_2_Log_User_First_Name:Success");
            Console.WriteLine($"User {responseFromPreviousAct} has the job title : 'Developer'.");
            return Response.Ok<string>("Developer");
        }

        // Map existing data to a UserDto
        public async Task<Response<UserDto>> Act3(string name, string jobPosition)
        {
            return Response.Ok(new UserDto()
            {
                Name = name,
                Position = jobPosition
            });
        }
        
        // Log failure
        public async Task<Response<UserDto>> ReturnFailure()
        {
            Console.WriteLine("Logged failure");
            return Response.Fail<UserDto>(message:"Not Found",code:"404");
        }
        
        public class User
        {
            public string FirstName { get; set; }

            public string LastName { get; set; }

            public string FullName => $"{FirstName} {LastName}";
        }
        
        public class UserDto
        {
            public string Name { get; set; }
            public string Position { get; set; }
        }
    }
}