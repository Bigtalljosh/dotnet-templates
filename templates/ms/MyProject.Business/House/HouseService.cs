using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace MyProject.Business.House
{
    public class HouseService : IHouseService
    {
        private readonly ILogger _logger;
        private readonly IHouseRepository _repository;
        public HouseService(ILogger<HouseService> logger, IHouseRepository repository)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task Create(Models.House house) => await _repository.Create(house);

        public async Task Delete(Models.House house) => await _repository.Delete(house);

        public async Task<Models.House> Get(int id) => await _repository.Get(id);

        public async Task Update(Models.House house) => await _repository.Update(house);
    }
}
