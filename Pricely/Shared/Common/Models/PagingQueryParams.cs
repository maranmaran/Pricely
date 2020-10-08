﻿namespace Common.Models
{
    public class PagingQueryParams
    {
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 20; // TODO: Config
    }
}
