using Common.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Reflection;
using System.Text;

namespace Common.Helpers
{
    /// <summary>
    /// Generic sorter for generic classes
    /// </summary>
    /// <example>new Sorter<CLASSTYPE>(LOGGER)</example>
    public class Sorter<T> where T : class
    {
        private readonly ILogger _logger;

        public Sorter(ILogger logger = null)
        {
            _logger = logger;
        }

        /// <summary>
        /// Dynamically sorts input items by given sorting query 
        /// </summary>
        /// <param name="items">Data set</param>
        /// <param name="sortingQueryString">Sorting query</param>
        /// <returns>Sorted data set or non changed data set in case of error</returns>
        /// <remarks>Logs validation errors on provided query if logger is provided</remarks>
        public IQueryable<T> Sort(IQueryable<T> items, string sortingQueryString)
        {
            // validate if we have any items to sort
            if (!items.Any())
                return items;

            // validate, deserialize and get sorting params from query
            List<(string sortBy, SortDirection direction)> sortParams;
            try
            {
                sortParams = DeserializeSortingQueryString(sortingQueryString);
            }
            catch (Exception e)
            {
                _logger?.LogWarning(e.Message);
                return items;
            }

            // fetch class props
            var classProps = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            // match class props and build query
            var dynamicSortQueryBuilder = new StringBuilder();
            foreach (var (sortBy, sortDirection) in sortParams)
            {
                var classProp = classProps
                    .FirstOrDefault(x => x.Name.Trim().ToLowerInvariant() == sortBy);

                if (classProp == null)
                {
                    _logger?.LogWarning($"Param name not found for sorting - {sortBy} param name of {nameof(T)} class");
                    continue;
                }

                dynamicSortQueryBuilder.Append($"{classProp.Name} {sortDirection.ToString().ToLower()}, ");
            }

            dynamicSortQueryBuilder.Length -= 2; // remove last ", "
            var sortQuery = dynamicSortQueryBuilder.ToString();

            if (string.IsNullOrWhiteSpace(sortQuery))
                return items;

            // execute query
            return items.OrderBy(sortQuery);
        }

        /// <summary>
        /// Takes sorting query string, validates and deserializes it to list of parameters
        /// </summary>
        /// <example>"Name asc, Category desc, Order asc"</example>
        /// <param name="sortingQueryString">Full query string containing whole query</param>
        /// <returns>List of sort by and direction parameters</returns>
        public List<(string sortBy, SortDirection direction)> DeserializeSortingQueryString(string sortingQueryString)
        {
            // validate sorting query
            if (string.IsNullOrWhiteSpace(sortingQueryString))
                throw new ArgumentException("Sorting query params cannot be null or empty.");

            // extract sorting pairs... <prop_name> <direction>
            var sortingPairs = sortingQueryString.Split(',');

            var sortParams = new List<(string sortBy, SortDirection direction)>();
            foreach (var sortingPair in sortingPairs)
            {
                var pairSplit = sortingPair.Split(' ', StringSplitOptions.RemoveEmptyEntries);

                // normalize params
                var sortBy = pairSplit[0]?.Trim()?.ToLowerInvariant();
                if (string.IsNullOrWhiteSpace(sortBy))
                    throw new ArgumentException($"Invalid sort query parameter sortBy. {sortingQueryString}");

                var success = Enum.TryParse(typeof(SortDirection), pairSplit[1]?.Trim()?.ToLowerInvariant(), ignoreCase: true, out var sortDirection);
                if (!success)
                    throw new ArgumentException($"Invalid sort query parameter sortDirection. {sortingQueryString}");

                sortParams.Add((sortBy, (SortDirection)sortDirection));
            }

            return sortParams;
        }
    }
}
