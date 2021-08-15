using System.Net;
using System.Threading.Tasks;
using FluentAssertions;
using HistoryAccessor.Contracts;
using HistoryAccessorService.Service.Commands.RecordEvent;
using IntegrationTestsBase.Infrastructure;
using MediatR;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IntegrationTestsBase.AccessorTests.Commands
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
            var command = new RecordEvent();
            var mediatr = GetInstance<IMediator>();

            //Act
            var operation = await mediatr.Send(command);
            
            //Assert
            operation.IsSuccess.Should().BeFalse();
            operation.ResponseCode.Should().Be(HttpStatusCode.Forbidden.ToString());
        }
    }
}