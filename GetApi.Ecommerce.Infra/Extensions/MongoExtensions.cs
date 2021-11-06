using GetApi.Ecommerce.Core.Catalog.Dtos;
using GetApi.Ecommerce.Infra.Wrappers;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace GetApi.Ecommerce.Infra.Extensions
{
    public static class MongoExtensions
    {
        public static async Task<PaginationDto<T>> QueryByPageAsync<T>(this IMongoCollection<T> collection,
            int page,
            int pageSize,
            CancellationToken cancellationToken,
            Expression<Func<T, bool>> filter = null,
            SortDefinition<T> sort = null)
        {
            var countFacet = AggregateFacet.Create("count",
                PipelineDefinition<T, AggregateCountResult>.Create(new[]
                {
                    PipelineStageDefinitionBuilder.Count<T>()
                }));

            var pipeline = new List<PipelineStageDefinition<T, T>>();

            if (sort != null) pipeline.Add(PipelineStageDefinitionBuilder.Sort(sort));

            pipeline.Add(PipelineStageDefinitionBuilder.Skip<T>(CalculatePage(page, pageSize)));
            pipeline.Add(PipelineStageDefinitionBuilder.Limit<T>(pageSize));

            var dataFacet = AggregateFacet.Create("data", PipelineDefinition<T, T>.Create(pipeline));

            var aggregation = await collection
                                        .Aggregate()
                                        .Match(filter ?? Builders<T>.Filter.Empty)
                                        .Facet(countFacet, dataFacet)
                                        .ToListAsync(cancellationToken);

            var count = aggregation
                            .First().Facets.First(x => x.Name == "count")
                            .Output<AggregateCountResult>()?
                            .FirstOrDefault()?
                            .Count ?? 0;

            var totalPages = (int)count / pageSize;

            totalPages = totalPages == 0 ? 1 : totalPages;

            var data = aggregation.First()
                .Facets.First(x => x.Name == "data")
                .Output<T>();

            var pageNumber = CalculatePage(page, pageSize) + 1;

            return new PaginationDto<T>()
            {
                Data = data,
                TotalPages = totalPages,
                TotalItems = (int)count,
                PageSize = pageSize,
                PageNumber = pageNumber,
                HasNextPage = pageNumber < totalPages,
                HasPreviousPage = pageNumber != 1,
                NextPageNumber = ((pageNumber + 1) < totalPages) ? pageNumber + 1 : pageNumber,
                PreviousPageNumber = ((pageNumber - 1) > 1) ? pageNumber - 1 : pageNumber,
            };
        }

        private static int CalculatePage(int page, int pageSize)
        {
            return (page - 1) * pageSize;
        }

        public static IServiceCollection AddWrappers(this IServiceCollection services)
        {
            services.AddScoped<IMongoPagginationWrapper, MongoPagginationWrapper>();

            return services;
        }
    }
}
