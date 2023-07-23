using BlazorWebAssemblyIdentityDemo.OAuth.Model;
using BlazorWebAssemblyIdentityDemo.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;

namespace BlazorWebAssemblyIdentityDemo.OAuth.Data.Extensions
{
    public static class UserExtension
    {
        public static IQueryable<User> SearchByRoleId(this IQueryable<User> query, string? roleId)
        {
            if(string.IsNullOrEmpty(roleId) || roleId == "0")
            {
                return query;
            }
        
            return query.Where(x => x.UserRoles.Any(x => x.RoleId == roleId));
        }

        public static IQueryable<User> SearchByPositionId(this IQueryable<User> query, string? positionId)
        {
            if (string.IsNullOrEmpty(positionId) || positionId == "0")
            {
                return query;
            }

            var position = (Position)(int.Parse(positionId));

            return query.Where(x => x.Position == position);
        }

        public static IQueryable<User> Search(this IQueryable<User> query, string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
                return query;
            var lowerCaseSearchTerm = searchTerm.Trim().ToLower();
            return query.Where(p => 
            p.FirstName.ToLower().Contains(lowerCaseSearchTerm) || 
            p.LastName.ToLower().Contains(lowerCaseSearchTerm) || 
            p.Email.ToLower().Contains(lowerCaseSearchTerm));
        }

        public static IQueryable<User> Sort(this IQueryable<User> products, string orderByQueryString)
        {
            if (string.IsNullOrWhiteSpace(orderByQueryString))
                return products.OrderBy(e => e.FirstName);

            var orderParams = orderByQueryString.Trim().Split(',');
            var propertyInfos = typeof(User).GetProperties(BindingFlags.Public | BindingFlags.Instance);
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
                return products.OrderBy(e => e.FirstName);

            return products.OrderBy(orderQuery);
        }
    }
}
