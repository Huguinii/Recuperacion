using RestAPI.Models.Entity;

namespace RestAPI.Repository.IRepository
{
    public interface IFranjaHorariaRepository : IRepository<FranjaHorariaEntity>
    {
        Task<ICollection<FranjaHorariaEntity>> GetDisponiblesAsync();

    }
}
