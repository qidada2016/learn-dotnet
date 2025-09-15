using MyToDo.Shared.Dtos;
using System.Threading.Tasks;

namespace MyToDo.Api.Services
{
    public interface ILoginService
    {
        Task<ApiResponse> LoginAsync(string account, string password);

        Task<ApiResponse> Register(UserDto user);
    }
}
