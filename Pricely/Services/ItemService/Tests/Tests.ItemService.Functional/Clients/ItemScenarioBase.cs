using Common.Models;
using Microsoft.AspNetCore.WebUtilities;
using System;
using System.Collections.Generic;
using Tests.ItemService.Functional.Infrastructure;

namespace Tests.ItemService.Functional.Clients
{
    public class ItemScenarioBase : ScenarioBase
    {
        public static string BaseUrl => "api/Item";

        public static class Get
        {
            public static string GetById(Guid id) => $"{BaseUrl}/{id}";

            public static string GetAll(
                PagingQueryParams paging = null,
                SortingQueryParams sorting = null,
                FilterQueryParams filtering = null)
            {
                var baseUrl = $"{BaseUrl}";

                var queryParams = new Dictionary<string, string>();

                if (paging != null)
                {
                    queryParams.Add(nameof(PagingQueryParams.Page), $"{paging.Page}");
                    queryParams.Add(nameof(PagingQueryParams.PageSize), $"{paging.PageSize}");
                }
                if (filtering != null)
                {
                    queryParams.Add(nameof(SortingQueryParams.SortQueryExpression), $"{sorting.SortQueryExpression}");
                }
                if (sorting != null)
                {
                    queryParams.Add(nameof(FilterQueryParams.SearchTerm), $"{filtering.SearchTerm}");
                }

                return QueryHelpers.AddQueryString(baseUrl, queryParams);
            }
        }

        public static class Post
        {
            public static string CreateItem = $"{BaseUrl}";
        }

        public static class Put
        {
            public static string UpdateItem => $"{BaseUrl}";
        }

        public static class Delete
        {
            public static string DeleteItem(Guid id) => $"{BaseUrl}/{id}";
        }
    }
}
