using GetApi.Ecommerce.Core.Catalog.Dtos;
using GetApi.Ecommerce.Core.Catalog.Entities;
using GetApi.Ecommerce.Core.Catalog.Mappers;
using GetApi.Ecommerce.Core.Catalog.Repositories;
using GetApi.Ecommerce.Core.Catalog.Requests;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace GetApi.Ecommerce.Core.Catalog.Services
{
    public class CategoryService : ICategoryService
    {
        private ICategoryRepository _repository;

        public CategoryService(ICategoryRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public async Task Create(CategoryRequest request, CancellationToken cancellationToken)
        {
            if (request is null)
                throw new ValidationException($"This argument: {nameof(request)} can't be null");

            if (await HasCategory(request, cancellationToken) is false)
                throw new ValidationException("Parent category id was not found");

            await _repository.Create(
                Category.Create(request.Name, request.FriendlyUrl, request.ParentId),
                cancellationToken
            );
        }

        private async Task<bool> HasCategory(CategoryRequest request, CancellationToken cancellationToken)
        {
            if (request.ParentId.HasValue is false) return true;

            var category = await _repository.Find(x => x.Id == request.ParentId, cancellationToken);

            return category != null;
        }

        public async Task Update(Guid id, CategoryRequest request, CancellationToken cancellationToken)
        {
            var category = await _repository.Find(x => x.Id == id, cancellationToken);

            category.Update(request.ToDto());

            await _repository.Update((x) => x.Id == category.Id, category, cancellationToken);
        }

        public async Task<IEnumerable<CategoryDto>> List(CancellationToken cancellationToken)
        {
            var categories = await _repository.List(cancellationToken);

            return categories.Select(x => x.MapToDto()).ToArray();
        }
    }
}
