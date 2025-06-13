using RestAPI.Models.Entity;

namespace RestAPI.Repository.IRepository
{
    public interface IDiaRepository : IRepository<DiaEntity>
    {
        Task<bool> EsDiaNoLectivo(DateTime fecha);
    }
}
