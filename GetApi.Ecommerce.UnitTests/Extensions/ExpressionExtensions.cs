using GetApi.Ecommerce.UnitTests.Helpers;
using System;
using System.Linq.Expressions;

namespace GetApi.Ecommerce.UnitTests.Extensions
{
    public static class ExpressionExtensions
    {
        public static bool AreEquals<T>(this Expression<Func<T, bool>> expression, Expression<Func<T, bool>> expressionToComparer)
        {
            return LambdaComparer.Equals(expression, expressionToComparer);
        }
    }
}
