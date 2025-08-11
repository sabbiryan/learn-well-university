using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace LearnWellUniversity.Infrastructure.Extensions
{
    public static class QueryableExtensions
    {


        /// <summary>
        /// Apply dynamic search from query string example as search=Admin
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query"></param>
        /// <param name="search"></param>
        /// <returns></returns>
        public static IQueryable<T> ApplyDynamicSearch<T>(this IQueryable<T> query, string? search)
        {
            if (string.IsNullOrWhiteSpace(search))
                return query;

            var searchTerms = search.Split(' ', StringSplitOptions.RemoveEmptyEntries);

            // Get all string properties including nested/navigation properties (up to one level for simplicity)
            var stringProps = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Where(p => p.PropertyType == typeof(string))
                .Select(p => p.Name)
                .ToList();

            // Find navigation properties that are classes (excluding system types like string, DateTime, etc.)
            var navigationProps = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Where(p => p.PropertyType.IsClass && p.PropertyType != typeof(string) && !p.PropertyType.Namespace!.StartsWith("System"))
                .ToList();

            // Include string properties from navigation properties (one level nested)
            var nestedStringProps = new List<string>();
            foreach (var navProp in navigationProps)
            {
                var nestedProps = navProp.PropertyType.GetProperties(BindingFlags.Public | BindingFlags.Instance)
                    .Where(p => p.PropertyType == typeof(string))
                    .Select(p => $"{navProp.Name}.{p.Name}");

                nestedStringProps.AddRange(nestedProps);
            }

            var allSearchableProps = stringProps.Concat(nestedStringProps).ToList();

            if (!allSearchableProps.Any())
                return query;

            // Build predicate for each search term and combine with AND logic
            foreach (var term in searchTerms)
            {
                // Build OR condition across all searchable properties for this term
                var orConditions = string.Join(" OR ", allSearchableProps.Select((prop, index) => $"{prop} != null && {prop}.Contains(@{index})"));

                // The parameters for all props are the same term repeated
                var parameters = Enumerable.Repeat((object)term, allSearchableProps.Count).ToArray();

                query = query.Where(orConditions, parameters);
            }

            return query;
        }





        /// <summary>
        /// Apply dynamic filters from query string value
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query"></param>
        /// <param name="filter">Example filter: "Department:IT;Status:Active|Pending;Age>=30;CreatedAt>=2024-01-01;"</param>
        /// <returns></returns>
        public static IQueryable<T> ApplyDynamicFilter<T>(this IQueryable<T> query, string? filter)
        {
            if (string.IsNullOrWhiteSpace(filter))
                return query;

            var conditions = filter.Split(';', StringSplitOptions.RemoveEmptyEntries);

            foreach (var condition in conditions)
            {
                // Detect operator (>, >=, <, <=, :)
                string[] operators = [">=", "<=", ">", "<", ":"];
                string? op = operators.FirstOrDefault(condition.Contains);

                if (op == null) continue;

                var parts = condition.Split(op, StringSplitOptions.RemoveEmptyEntries);
                if (parts.Length != 2) continue;

                var property = parts[0];
                var value = parts[1];

                var propInfo = typeof(T).GetProperty(property, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
                if (propInfo == null) continue;

                if (op == ":")
                {
                    if (value.Contains('|'))
                    {
                        // OR condition
                        var values = value.Split('|', StringSplitOptions.RemoveEmptyEntries);
                        
                        var orConditions = string.Join(" OR ", values.Select((v, i) => $"{propInfo.Name}.Contains(@{i})"));

                        query = query.Where(orConditions, [.. values.Cast<object>()]);
                    }
                    else
                    {
                        query = query.Where($"{propInfo.Name}.Contains(@0)", value);
                    }
                }
                else
                {
                    // Numeric/Date comparison
                    if (propInfo.PropertyType == typeof(DateTime) || propInfo.PropertyType == typeof(DateTime?))
                    {
                        if (DateTime.TryParse(value, out var dateValue))
                            query = query.Where($"{propInfo.Name} {op} @0", dateValue);
                    }
                    else if (decimal.TryParse(value, out var numValue))
                    {
                        query = query.Where($"{propInfo.Name} {op} @0", numValue);
                    }
                }
            }

            return query;
        }


        /// <summary>
        /// Apply dynamic sorting from query string value
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query"></param>
        /// <param name="sortBy">Column name</param>
        /// <param name="direction">Use ase for ascending sorting or desc for sort by desending.</param>
        /// <returns></returns>
        public static IQueryable<T> ApplyDynamicSort<T>(this IQueryable<T> query, string? sortBy, string? direction)
        {
            if (string.IsNullOrWhiteSpace(sortBy))
                return query;

            direction = string.IsNullOrWhiteSpace(direction) ? "asc" : direction.ToLower();

            query =  query.OrderBy($"{sortBy} {direction}");

            return query;
        }
    }
}
