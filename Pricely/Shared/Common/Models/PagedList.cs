using System;
using System.Collections.Generic;

namespace Common.Models
{
    public class PagedList<T> where T : class
    {
        public IEnumerable<T> Items { get; set; }
        public bool HasNext { get; set; }
        public bool HasPrevious { get; set; }
        public int TotalItems { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int PageSize { get; set; }

        public PagedList(IEnumerable<T> items, int totalItems, int currentPage, int pageSize)
        {
            Items = items;
            TotalItems = totalItems;
            CurrentPage = currentPage;
            PageSize = pageSize;

            TotalPages = (int)Math.Ceiling(totalItems / (decimal)pageSize);
            HasNext = currentPage < TotalPages;
            HasPrevious = currentPage <= 1;

            if (currentPage < 1 || currentPage > TotalPages)
                throw new ArgumentException($"Invalid {nameof(currentPage)} value of {currentPage}");
        }
    }
}
