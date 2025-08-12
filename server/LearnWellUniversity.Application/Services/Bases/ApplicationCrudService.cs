using LearnWellUniversity.Application.Contracts.Bases;
using LearnWellUniversity.Application.Contracts.UoW;
using LearnWellUniversity.Application.Exceptions;
using LearnWellUniversity.Application.Models.Common.Paginations;
using LearnWellUniversity.Application.Models.Dtos.Bases;
using LearnWellUniversity.Application.Models.Requestes.Bases;
using LearnWellUniversity.Domain.Entities.Bases;
using MapsterMapper;
using System.Linq.Expressions;

namespace LearnWellUniversity.Application.Services
{
    public abstract class ApplicationCrudService<TEntity, TDto, TPk, TCRequest, TURequest>(
        IUnitOfWork unitOfWork,
        IMapper mapper
    ) : ApplicationService, IApplicationCrudService<TDto, TPk, TCRequest, TURequest>
        where TEntity : EntityBase<TPk>
        where TDto : DtoBase<TPk>
        where TCRequest : class
        where TURequest : RequestBase<TPk>
    {

        /// <summary>
        /// Use this property to define a custom selector for the query. It will be used to project the entity to the DTO. And injected this value to GetPagedAsync method.
        /// </summary>
        protected Expression<Func<TEntity, TDto>>? Selector { get; set; } = null;

        /// <summary>
        /// Use this property to define a list of includes for the query. It will be used to include related entities in the query. And injected this value to GetPagedAsync method.
        /// </summary>
        protected List<Expression<Func<TEntity, object>>>? Includes { get; set; } = null;



        public virtual async Task<PaginatedResult<TDto>> GetPagedAsync(DynamicQueryRequest request)
        {

            var result = await unitOfWork.Repository<TEntity>().GetPagedAsync<TDto>(request, Selector, Includes);

            return result;
        }



        public virtual async Task<TDto?> GetByIdAsync(TPk id)
        {
            var entity = await unitOfWork.Repository<TEntity>().GetByIdAsync(id);

            var result = entity != null ? mapper.Map<TDto>(entity) : null;

            return result;
        }

        public virtual async Task<TPk> AddAsync(TCRequest request)
        {
            var entity = mapper.Map<TEntity>(request);

            await unitOfWork.Repository<TEntity>().AddAsync(entity);

            await unitOfWork.SaveChangesAsync();

            //var id = (TPk)typeof(TEntity).GetProperty(nameof(EntityBase<TPk>.Id))!.GetValue(entity)!;
            var id = entity.Id;

            return id;
        }


        public virtual async Task UpdateAsync(TURequest request)
        {
            //var id = (TPk)typeof(TURequest).GetProperty("Id")!.GetValue(request)!;
            var id = request.Id;

            var entity = await unitOfWork.Repository<TEntity>().GetByIdAsync(id);

            if (entity == null) throw new EntityNotFoundException(typeof(TEntity).Name, id!);


            mapper.Map(request, entity);

            unitOfWork.Repository<TEntity>().Update(entity);

            await unitOfWork.SaveChangesAsync();
        }


        public virtual async Task DeleteAsync(TPk id)
        {
            var entity = await unitOfWork.Repository<TEntity>().GetByIdAsync(id);
            
            if (entity == null) throw new EntityNotFoundException(typeof(TEntity).Name, id!);

            unitOfWork.Repository<TEntity>().Remove(entity);

            await unitOfWork.SaveChangesAsync();
        }

    }
}
