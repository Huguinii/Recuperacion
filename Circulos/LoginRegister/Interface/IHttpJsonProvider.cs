using LoginRegister.Models;

namespace LoginRegister.Interface
{
    public interface IHttpJsonProvider<T> where T : class
    {
        Task<IEnumerable<T>> GetAsync(string path);
        Task<T> PostAsync(string path, T data);
        Task<T> LoginPostAsync(string path, LoginDTO data);
        Task<T> RegisterPostAsync(string path, UserRegistroDTO data);
        Task<T> PutAsync(string path, T data);
        Task<T> DeleteAsync(string path, int id);
    }
}
