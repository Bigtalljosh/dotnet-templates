using Microsoft.Extensions.Logging;
using NSubstitute;
using MyProject.Business.House;
using System.Threading.Tasks;
using Xunit;

namespace MyProject.Business.UnitTests
{
    public class HouseServiceTests
    {
        [Fact]
        public async Task Create_ShouldCallCreate_WhenPassedValidParams()
        {
            var repo = Substitute.For<IHouseRepository>();
            var logger = Substitute.For<ILogger<HouseService>>();
            var houseService = new HouseService(logger, repo);
            var house = new Models.House()
            {
                Description = "This is a nice house!",
                NumberOfRooms = 5
            };

            await houseService.Create(house);
            await repo.Received(1).Create(house);
        }

        [Fact]
        public async Task Get_ShouldReturnAHouseEntity_WhenPassedValidId()
        {
            var testId = 2;
            var repo = Substitute.For<IHouseRepository>();
            var logger = Substitute.For<ILogger<HouseService>>();
            var houseService = new HouseService(logger, repo);
            var house = new Models.House()
            {
                Id = testId,
                Description = "This is a nice house!",
                NumberOfRooms = 5
            };

            await houseService.Create(house);
            await houseService.Get(testId);
            await repo.Received(1).Get(testId);
        }

        [Fact]
        public async Task Update_ShouldUpdateAhouseEntity_WhenPassedValidParams()
        {
            var testId = 3;
            var updatedDescription = "This is a REALLY nice house!";
            var repo = Substitute.For<IHouseRepository>();
            var logger = Substitute.For<ILogger<HouseService>>();
            var houseService = new HouseService(logger, repo);
            var house = new Models.House()
            {
                Id = testId,
                Description = "This is a nice house!",
                NumberOfRooms = 5
            };

            await houseService.Create(house);
            house.Description = updatedDescription;
            await houseService.Update(house);
            await repo.Received(1).Update(house);
        }

        [Fact]
        public async Task Delete_ShouldDeleteAhouseEntity_WhenPassedValidParams()
        {
            var repo = Substitute.For<IHouseRepository>();
            var logger = Substitute.For<ILogger<HouseService>>();
            var houseService = new HouseService(logger, repo);
            var house = new Models.House()
            {
                Description = "This is a nice house!",
                NumberOfRooms = 5
            };
            await houseService.Create(house);
            await houseService.Delete(house);
            await repo.Received(1).Delete(house);
        }
    }
}
