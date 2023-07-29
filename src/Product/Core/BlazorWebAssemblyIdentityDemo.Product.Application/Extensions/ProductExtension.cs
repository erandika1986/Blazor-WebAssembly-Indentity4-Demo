using BlazorWebAssemblyIdentityDemo.Product.Domain.Entities;
using BlazorWebAssemblyIdentityDemo.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BlazorWebAssemblyIdentityDemo.Product.Application.Extensions
{
    public static class ProductExtension
    {
        public static IQueryable<BlazorWebAssemblyIdentityDemo.Product.Domain.Entities.Product> Search(this IQueryable<BlazorWebAssemblyIdentityDemo.Product.Domain.Entities.Product> query, string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
                return query;

            var lowerCaseSearchTerm = searchTerm.Trim().ToLower();

            return query.Where(p =>
                    p.Name.ToLower().Contains(lowerCaseSearchTerm));
        }

        public static IQueryable<BlazorWebAssemblyIdentityDemo.Product.Domain.Entities.Product> Sort(this IQueryable<BlazorWebAssemblyIdentityDemo.Product.Domain.Entities.Product> query, string orderByQueryString)
        {
            if (string.IsNullOrWhiteSpace(orderByQueryString))
                return query.OrderBy(e => e.Name);

            var orderParams = orderByQueryString.Trim().Split(',');
            var propertyInfos = typeof(BlazorWebAssemblyIdentityDemo.Product.Domain.Entities.Product).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            var orderQueryBuilder = new StringBuilder();

            foreach (var param in orderParams)
            {
                if (string.IsNullOrWhiteSpace(param))
                    continue;

                var propertyFromQueryName = param.Split(" ")[0];
                var objectProperty = propertyInfos.FirstOrDefault(pi => pi.Name.Equals(propertyFromQueryName, StringComparison.InvariantCultureIgnoreCase));

                if (objectProperty == null)
                    continue;

                var direction = param.EndsWith(" desc") ? "descending" : "ascending";
                orderQueryBuilder.Append($"{objectProperty.Name} {direction}, ");
            }

            var orderQuery = orderQueryBuilder.ToString().TrimEnd(',', ' ');
            if (string.IsNullOrWhiteSpace(orderQuery))
                return query.OrderBy(e => e.Name);

            return query.OrderBy(orderQuery);
        }

        public static IQueryable<BlazorWebAssemblyIdentityDemo.Product.Domain.Entities.Product> SearchByProductCategoryId(this IQueryable<BlazorWebAssemblyIdentityDemo.Product.Domain.Entities.Product> query, int productCategory)
        {
            if (productCategory == 0)
            {
                return query;
            }

            return query.Where(x => x.ProductCategoryId == productCategory);
        }
    }
}
