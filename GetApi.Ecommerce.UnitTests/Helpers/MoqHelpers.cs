using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using Moq;
using System;
using System.Linq.Expressions;


namespace GetApi.Ecommerce.UnitTests.Helpers
{
    public static class MoqHelpers
    {
        public static ExpressionFilterDefinition<T> IsValidFilter<T>(Expression<Func<T, bool>> expression)
        {
            return It.Is<ExpressionFilterDefinition<T>>(expr => IsEquals(expr, new ExpressionFilterDefinition<T>(expression)));
        }

        public static FilterDefinition<T> IsValidFilter<T>(FilterDefinition<T> expression)
        {
            return It.Is<FilterDefinition<T>>(expr => IsEquals(expr, expression));
        }

        private static bool IsEquals<T>(ExpressionFilterDefinition<T> expr, ExpressionFilterDefinition<T> comparerExpr)
        {
            var serializerRegistry = BsonSerializer.SerializerRegistry;
            var documentSerializer = serializerRegistry.GetSerializer<T>();

            return expr.Render(documentSerializer, serializerRegistry) == comparerExpr.Render(documentSerializer, serializerRegistry);
        }

        private static bool IsEquals<T>(FilterDefinition<T> expr, FilterDefinition<T> comparerExpr)
        {
            var serializerRegistry = BsonSerializer.SerializerRegistry;
            var documentSerializer = serializerRegistry.GetSerializer<T>();

            return expr.Render(documentSerializer, serializerRegistry) == comparerExpr.Render(documentSerializer, serializerRegistry);
        }
    }
}
