using Arch.EntityFrameworkCore.UnitOfWork;
using Microsoft.AspNetCore.Mvc;
using MyToDo.Api.Context;
using MyToDo.Api.Services;
using MyToDo.Shared.Dtos;
using MyToDo.Shared.Parameters;
using System.Threading.Tasks;

namespace MyToDo.Api.Controllers
{
    /// <summary>
    /// 待办事项控制器
    /// </summary>
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class LoginController : ControllerBase
    {
        private readonly ILoginService service;

        public LoginController(ILoginService service)
        {
            this.service = service;
        }

        [HttpPost]
        public Task<ApiResponse> Register([FromBody] UserDto param) => service.Register(param);

        [HttpGet]
        public Task<ApiResponse> Login(string account, string password) => service.LoginAsync(account, password);



    }
}
