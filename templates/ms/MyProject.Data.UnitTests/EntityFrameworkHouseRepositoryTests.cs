using Microsoft.EntityFrameworkCore;
using MyProject.Business.Models;
using MyProject.Data.Repositories;
using System.Threading.Tasks;
using Xunit;

namespace MyProject.Data.UnitTests
{
    public class EntityFrameworkHouseRepositoryTests
    {
        private readonly DatabaseContext _db;

        public EntityFrameworkHouseRepositoryTests()
        {
            var builder = new DbContextOptionsBuilder<DatabaseContext>()
                .UseInMemoryDatabase("Microservice.Data.Tests");

            _db = new DatabaseContext(builder.Options);
        }

        [Fact]
        public async Task Create_ShouldCreateEntity_WhenItHasValidParams()
        {
            var testId = 1;
            var repo = new EntityFrameworkHouseRepository(_db);
            var house = new House
            {
                Id = testId,
                Description = "This is a nice house!",
                NumberOfRooms = 4
            };

            await repo.Create(house);
            var newHouse = await repo.Get(testId);

            Assert.NotNull(newHouse);
        }

        [Fact]
        public async Task Get_ShouldCreateEntity_WhenItHasValidParams()
        {
            var testId = 2;
            var repo = new EntityFrameworkHouseRepository(_db);
            var house = new House
            {
                Id = testId,
                Description = "This is a nice house!",
                NumberOfRooms = 4
            };

            await repo.Create(house);
            var newHouse = await repo.Get(testId);

            Assert.NotNull(newHouse);
        }

        [Fact]
        public async Task Update_ShouldUpdateEntity_WhenItHasValidParams()
        {
            var testId = 3;
            var testDescription = "This is a REALLY nice house!";
            var repo = new EntityFrameworkHouseRepository(_db);
            var house = new House
            {
                Id = testId,
                Description = "This is a nice house!",
                NumberOfRooms = 4
            };

            await repo.Create(house);
            var newHouse = await repo.Get(testId);
            newHouse.Description = testDescription;
            await repo.Update(newHouse);

            var updatedHouse = await repo.Get(testId);
            Assert.Equal(testDescription, updatedHouse.Description);
        }

        [Fact]
        public async Task Delete_ShouldDeleteEntity_WhenItHasValidParams()
        {
            var testId = 4;
            var repo = new EntityFrameworkHouseRepository(_db);
            var house = new House
            {
                Id = testId,
                Description = "This is a nice house!",
                NumberOfRooms = 4
            };

            await repo.Create(house);
            var newHouse = await repo.Get(testId);
            await repo.Delete(newHouse);

            var shouldBeNull = await repo.Get(testId);
            Assert.Null(shouldBeNull);
        }
    }
}
