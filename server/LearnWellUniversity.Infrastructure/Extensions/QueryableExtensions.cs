using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace LearnWellUniversity.Infrastructure.Extensions
{
    public static class QueryableExtensions
    {
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
                        
                        var orConditions = string.Join(" OR ", values.Select((v, i) => $"{propInfo.Name}.ToString().Contains(@{i})"));

                        query = query.Where(orConditions, [.. values.Cast<object>()]);
                    }
                    else
                    {
                        query = query.Where($"{propInfo.Name}.ToString().Contains(@0)", value);
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
