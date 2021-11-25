using Microsoft.Extensions.Logging;
using MyProject.Business.House;
using Pat.Subscriber;
using System.Threading.Tasks;

namespace MyProject.Subscriber.Handlers
{
    public class CreateHouseCommandV1Handler : IHandleEvent<CreateHouseCommandV1>
    {
        private readonly ILogger _log;
        private readonly IHouseService _houseService;

        public CreateHouseCommandV1Handler(ILogger<CreateHouseCommandV1Handler> log, IHouseService houseService)
        {
            _log = log;
            _houseService = houseService;
        }

        public Task HandleAsync(CreateHouseCommandV1 message)
        {
            _log.LogInformation($"Received CreateHouseCommandV1 for Name:{message.OwnerName}");
            return Task.CompletedTask;
        }
    }
}
