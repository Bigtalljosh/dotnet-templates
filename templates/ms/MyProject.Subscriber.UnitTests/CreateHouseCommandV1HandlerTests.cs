using MyProject.Business.House;
using MyProject.Business.Models;
using MyProject.Subscriber.Handlers;
using Microsoft.Extensions.Logging;
using NSubstitute;
using System.Threading.Tasks;
using Xunit;

namespace MyProject.Subscriber.UnitTests
{
    public class CreateHouseCommandV1HandlerTests
    {
        [Fact]
        public async Task HandleAsync_ShouldCallCreateHouse_WhenMessageIsReceived()
        {
            var message = new CreateHouseCommandV1
            {
                Description = "This is a nice house!",
                NumberOfRooms = 5
            };

            var logger = Substitute.For<ILogger<CreateHouseCommandV1Handler>>();
            var houseService = Substitute.For<IHouseService>();
            var handler = new CreateHouseCommandV1Handler(logger, houseService);

            await handler.HandleAsync(message);

            await houseService.Received(1).Create(Arg.Any<House>());
        }
    }
}
