using MyToDo.Shared.Parameters;
using System.Threading.Tasks;

namespace MyToDo.Api.Services
{
    public interface IBaseService<T>
    {
        Task<ApiResponse> GetAllAsync(QueryParameter parameter);

        Task<ApiResponse> GetOneAsync(int id);

        Task<ApiResponse> AddAsync(T model);
        Task<ApiResponse> UpdateAsync(T model);
        Task<ApiResponse> DeleteAsync(int id);
    }
}
