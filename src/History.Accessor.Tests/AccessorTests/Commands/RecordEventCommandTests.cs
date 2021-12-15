using System.Net;
using System.Threading.Tasks;
using FluentAssertions;
using History.Accessor.Contracts.Commands;
using History.Accessor.Service.Service.Commands.RecordEvent;
using History.Accessor.Tests.Infrastructure;
using MediatR;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace History.Accessor.Tests.AccessorTests.Commands
{
    [TestClass]
    public class RecordEventCommandTests : TestBase
    {
        [ClassInitialize]
        public static void Initialization(TestContext context)
        {
            InitialSetup(context);
        }

        [TestMethod]
        public async Task CommandShouldBeValidated()
        {
            //Assert
            var command = new RecordEventCommand();
            var mediatr = GetService<IMediator>();

            //Act
            var operation = await mediatr.Send(command);
            
            //Assert
            operation.IsSuccess.Should().BeFalse();
            operation.ResponseCode.Should().Be(HttpStatusCode.Forbidden.ToString());
        }
    }
}