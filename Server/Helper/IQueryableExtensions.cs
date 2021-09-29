using Services.Server.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Services.Server.Helper
{
    public static class IQueryableExtensions
    {
        public static IQueryable<T> Paginate<T>(this IQueryable<T> queryable,
           PaginationParameters parameters)
        {
            return queryable.Skip((parameters.Page - 1) * parameters.QuantityPerPage).
                Take(parameters.QuantityPerPage);
        }
    }
}
