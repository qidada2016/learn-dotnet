using Arch.EntityFrameworkCore.UnitOfWork;
using AutoMapper;
using MyToDo.Api.Context;
using MyToDo.Shared.Dtos;
using System.Threading.Tasks;

namespace MyToDo.Api.Services
{
    public class LoginService : ILoginService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public LoginService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<ApiResponse> LoginAsync(string account, string password)
        {
            try
            {
                var model = await unitOfWork.GetRepository<User>().GetFirstOrDefaultAsync(predicate: x => (x.Account.Equals(account)) && (x.Password.Equals(password)));
                if (model == null)
                {
                    return new ApiResponse("账号或密码错误, 请重试");
                }

                return new ApiResponse(true, model);
            }
            catch (System.Exception)
            {

                return new ApiResponse("登录失败!");
            }
        }

        public async Task<ApiResponse> Register(UserDto user)
        {
            try
            {
                var model = mapper.Map<User>(user); // dto 转换 实体
                var repository = unitOfWork.GetRepository<User>();
                var userModel = await repository.GetFirstOrDefaultAsync(predicate: x => x.Account.Equals(model.Account));

                if (userModel != null)
                {
                    return new ApiResponse($"当前账号: {model.Account}已存在, 请重新注册!");
                }

                await repository.InsertAsync(model);

                if (await unitOfWork.SaveChangesAsync() > 0)
                {
                    return new ApiResponse(true, model);
                }

                return new ApiResponse("注册失败!");

            }
            catch (System.Exception)
            {

                return new ApiResponse("注册失败!");
            }
        }
    }
}
