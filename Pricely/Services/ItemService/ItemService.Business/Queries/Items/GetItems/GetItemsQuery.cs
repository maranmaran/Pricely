using Common.Models;
using ItemService.Persistence.DTOModels;
using MediatR;

namespace ItemService.Business.Queries.Items.GetItems
{
    public class GetItemsQuery : IRequest<PagedList<ItemDto>>
    {
        public GetItemsQuery(PagingQueryParams pagingParams, SortingQueryParams sortingParams, FilterQueryParams filteringParams)
        {
            PagingParams = pagingParams;
            SortingParams = sortingParams;
            FilteringParams = filteringParams;
        }

        public PagingQueryParams PagingParams { get; set; }
        public SortingQueryParams SortingParams { get; set; }
        public FilterQueryParams FilteringParams { get; set; }
    }
}
