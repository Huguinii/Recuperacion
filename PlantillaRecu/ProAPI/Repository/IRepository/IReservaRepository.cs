using RestAPI.Models.Entity;

namespace RestAPI.Repository.IRepository
{
    public interface IReservaRepository : IRepository<ReservaEntity>
    {
        Task<ICollection<ReservaEntity>> GetByProfesorAsync(string nombreProfesor);
        Task<ICollection<ReservaEntity>> GetReservasPendientesAsync();
        Task<bool> ExisteReserva(DateOnly fecha, TimeOnly inicio, TimeOnly fin);
    }
}
