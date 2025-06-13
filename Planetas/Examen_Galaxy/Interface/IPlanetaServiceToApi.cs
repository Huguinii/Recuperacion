using Examen_Galaxy.DTO;


namespace LoginRegister.Interface
{
    public interface IPlanetaServiceToApi
    {
        // Obtiene un Planeta desde la API
        Task<IEnumerable<PlanetaDTO>> GetDicatadores();

        // Agrega un Planeta a la API
        Task PostPlaneta(PlanetaDTO dicatador);


    }
}
