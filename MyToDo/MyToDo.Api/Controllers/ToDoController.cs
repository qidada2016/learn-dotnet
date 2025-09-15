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
    public class ToDoController : ControllerBase
    {
        private readonly IToDoService service;

        public ToDoController(IToDoService service)
        {
            this.service = service;
        }

        [HttpGet]
        public Task<ApiResponse> Get(int id)
        {
            return service.GetOneAsync(id);
        }

        [HttpGet]
        public async Task<ApiResponse> GetAll([FromQuery] QueryParameter param) => await service.GetAllAsync(param);

        [HttpPost]
        public Task<ApiResponse> Add([FromBody] ToDoDto model) => service.AddAsync(model);

        [HttpPost]
        public Task<ApiResponse> Update([FromBody] ToDoDto model) => service.UpdateAsync(model);

        [HttpDelete]
        public Task<ApiResponse> Delete(int id) => service.DeleteAsync(id);


    }
}
