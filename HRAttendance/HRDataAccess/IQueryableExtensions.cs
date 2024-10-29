using System;
using System.Linq;
using System.Linq.Expressions;

namespace HRDataAccess
{
    public static class IQueryableExtensions
    {
        public static IQueryable<T> OrderByDynamic<T>(this IQueryable<T> source, string orderByProperty, bool desc)
        {
            var entityType = typeof(T);
            var propertyInfo = entityType.GetProperty(orderByProperty);

            if (propertyInfo == null)
            {
                throw new ArgumentException($"No property '{orderByProperty}' found on type '{entityType.Name}'");
            }

            var parameter = Expression.Parameter(entityType, "p");
            var propertyAccess = Expression.MakeMemberAccess(parameter, propertyInfo);
            var orderByExpression = Expression.Lambda(propertyAccess, parameter);

            var resultExpression = Expression.Call(
                typeof(Queryable),
                desc ? "OrderByDescending" : "OrderBy",
                new Type[] { entityType, propertyInfo.PropertyType },
                source.Expression,
                Expression.Quote(orderByExpression));

            return source.Provider.CreateQuery<T>(resultExpression);
        }
    }
}
