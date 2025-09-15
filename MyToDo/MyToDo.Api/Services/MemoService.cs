using Arch.EntityFrameworkCore.UnitOfWork;
using AutoMapper;
using MyToDo.Api.Context;
using MyToDo.Shared.Dtos;
using MyToDo.Shared.Parameters;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MyToDo.Api.Services
{
    public class MemoService : IMemoService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public MemoService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<ApiResponse> AddAsync(MemoDto model)
        {
            try
            {

                var memo = mapper.Map<Memo>(model);
                await unitOfWork.GetRepository<Memo>().InsertAsync(memo);
                if (await unitOfWork.SaveChangesAsync() > 0)
                {
                    return new ApiResponse(true, model);
                }

                return new ApiResponse("添加数据失败");
            }
            catch (Exception e)
            {

                return new ApiResponse(e.Message);
            }

        }

        public async Task<ApiResponse> DeleteAsync(int id)
        {
            try
            {
                var repository = unitOfWork.GetRepository<Memo>();
                var memo = await repository.GetFirstOrDefaultAsync(predicate: x => x.Id.Equals(id));

                repository.Delete(memo);

                if (await unitOfWork.SaveChangesAsync() > 0)
                {
                    return new ApiResponse(true, "");
                }

                return new ApiResponse("删除数据失败");
            }
            catch (Exception e)
            {

                return new ApiResponse(e.Message);
            }
        }

        public async Task<ApiResponse> GetAllAsync(QueryParameter parameter)
        {
            try
            {
                var repository = unitOfWork.GetRepository<Memo>();
                var todo = await repository.GetPagedListAsync(
                    predicate: x => string.IsNullOrWhiteSpace(parameter.Search) ? true : x.Title.Equals(parameter.Search),
                    pageIndex: parameter.PageIndex,
                    pageSize: parameter.PageSize,
                    orderBy: source => source.OrderByDescending(t => t.CreateDate)
                    );
                return new ApiResponse(true, todo);
            }
            catch (Exception e)
            {

                return new ApiResponse(e.Message);
            }
        }

        public async Task<ApiResponse> GetOneAsync(int id)
        {
            try
            {
                var repository = unitOfWork.GetRepository<Memo>();
                var memo = await repository.GetFirstOrDefaultAsync(predicate: x => x.Id.Equals(id));
                return new ApiResponse(true, memo);
            }
            catch (Exception e)
            {

                return new ApiResponse(e.Message);
            }
        }

        public async Task<ApiResponse> UpdateAsync(MemoDto model)
        {
            try
            {
                var dbMemo = mapper.Map<Memo>(model);

                var repository = unitOfWork.GetRepository<Memo>();
                var memo = await repository.GetFirstOrDefaultAsync(predicate: x => x.Id.Equals(dbMemo.Id));

                memo.Title = dbMemo.Title;
                memo.Content = dbMemo.Content;
                memo.UpdateDate = DateTime.Now;

                repository.Update(memo);

                if (await unitOfWork.SaveChangesAsync() > 0)
                {
                    return new ApiResponse(true, memo);
                }

                return new ApiResponse("更新数据异常!");
            }
            catch (Exception e)
            {

                return new ApiResponse(e.Message);
            }
        }
    }
}
