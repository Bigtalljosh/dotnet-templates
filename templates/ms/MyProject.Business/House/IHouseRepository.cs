using System.Threading.Tasks;

namespace MyProject.Business.House
{
    public interface IHouseRepository
    {
        Task Create(Models.House house);
        Task<Models.House> Get(int id);
        Task Update(Models.House house);
        Task Delete(Models.House house);
    }
}
