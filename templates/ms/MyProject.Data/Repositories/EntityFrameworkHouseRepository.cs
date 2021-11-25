using MyProject.Business.House;
using MyProject.Business.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace MyProject.Data.Repositories
{
    public class EntityFrameworkHouseRepository : IHouseRepository
    {
        private readonly DatabaseContext _dbContext;

        public EntityFrameworkHouseRepository(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Create(House house)
        {
            await _dbContext.AddAsync(house);
            await _dbContext.SaveChangesAsync();
        }

        public async Task Delete(House house)
        {
            _dbContext.Remove(house);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<House> Get(int id)
        {
            var customer = await _dbContext.Houses
                .Where(c => c.Id.Equals(id))
                .SingleOrDefaultAsync();

            return customer;
        }

        public async Task Update(House house)
        {
            _dbContext.Update(house);
            await _dbContext.SaveChangesAsync();
        }
    }
}