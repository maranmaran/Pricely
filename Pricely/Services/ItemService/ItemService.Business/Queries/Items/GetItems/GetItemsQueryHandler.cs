using AutoMapper;
using Common.Helpers;
using Common.Models;
using DataAccess.Sql.Interfaces;
using ItemService.Domain.Entities;
using ItemService.Persistence.DTOModels;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace ItemService.Business.Queries.Items.GetItems
{
    internal class GetItemsQueryHandler : IRequestHandler<GetItemsQuery, PagedList<ItemDto>>
    {
        private readonly IGenericEfRepository<Item> _repository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetItemsQueryHandler> _logger;

        public GetItemsQueryHandler(IGenericEfRepository<Item> repository, IMapper mapper, ILogger<GetItemsQueryHandler> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<PagedList<ItemDto>> Handle(GetItemsQuery request, CancellationToken cancellationToken)
        {
            var includeFn = GetIncludeFn();
            var sortFn = GetSortFn(request.SortingParams);
            var filterFn = GetFilterFn(request.FilteringParams);

            var pagedEntities = await _repository.GetPaged(
                page: request.PagingParams.Page,
                pageSize: request.PagingParams.PageSize,
                filter: filterFn,
                sortBy: sortFn,
                include: includeFn,
                cancellationToken: cancellationToken
            );

            return new PagedList<ItemDto>(
                items: _mapper.Map<IEnumerable<ItemDto>>(pagedEntities.Items),
                pagedEntities.TotalItems,
                pagedEntities.CurrentPage,
                pagedEntities.PageSize);
        }

        /// <summary>
        /// Builds and retrieves Item include function for extending with navigation props
        /// </summary>
        /// <returns>Function which when executed returns extended input with included navigation props from EF</returns>
        internal Func<IQueryable<Item>, IQueryable<Item>> GetIncludeFn()
        {
            return source => source
                .Include(x => x.Ingredients)
                .ThenInclude(x => x.Ingredient)
                .Include(x => x.Allergens)
                .ThenInclude(x => x.Allergen)
                .Include(x => x.Category);
        }

        /// <summary>
        /// Builds and retrieves Item sorting function
        /// </summary>
        /// <param name="sortBy">Describes sorting parameter name</param>
        /// <param name="direction">Describes sorting direction</param>
        /// <returns>Function which when executed returns sorted input</returns>
        internal Func<IQueryable<Item>, IQueryable<Item>> GetSortFn(SortingQueryParams parameters)
        {
            if (string.IsNullOrWhiteSpace(parameters.SortQueryExpression))
                return null;

            return items => new Sorter<Item>(_logger).Sort(items, parameters.SortQueryExpression);
        }

        /// <summary>
        /// Builds and retrieves Item filter function
        /// </summary>
        /// <param name="parameters">Describes search term for name, description or category name for item</param>
        /// <returns>Expression which is used in LINQ to SQL on database level for filtering</returns>
        internal Expression<Func<Item, bool>> GetFilterFn(FilterQueryParams parameters)
        {
            if (string.IsNullOrWhiteSpace(parameters.SearchTerm))
                return null;

            // normalize search term
            var searchTerm = parameters.SearchTerm.Trim().ToLower();

            // define expression input item => item....
            var input = Expression.Parameter(typeof(Item));

            // create expression constant to be used
            var searchTermConstant = Expression.Constant(searchTerm);

            // define all conditions 

            Expression<Func<Item, string, bool>> nameMatchDelegate = (item, value) => !string.IsNullOrWhiteSpace(item.Name) && item.Name.Trim().ToLower().Contains(value);
            var nameMatchInvoke = Expression.Invoke(nameMatchDelegate, input, searchTermConstant);

            Expression<Func<Item, string, bool>> descriptionMatchDelegate = (item, value) => !string.IsNullOrWhiteSpace(item.Description) && item.Description.Trim().ToLower().Contains(value);
            var descriptionMatchInvoke = Expression.Invoke(descriptionMatchDelegate, input, searchTermConstant);

            Expression<Func<Item, string, bool>> categoryMatchDelegate = (item, value) => item.Category != null && !string.IsNullOrWhiteSpace(item.Category.Name) && item.Category.Name.Trim().ToLower().Contains(value);
            var categoryMatchInvoke = Expression.Invoke(categoryMatchDelegate, input, searchTermConstant);

            var conditions = new List<Expression> { nameMatchInvoke, descriptionMatchInvoke, categoryMatchInvoke };

            // item => nameMatch(item) || descriptionMatch(item) || categoryMatch(item)
            var body = conditions
                .Skip(1)
                .Aggregate(
                    seed: conditions.FirstOrDefault(),
                    func: Expression.Or);

            return Expression.Lambda<Func<Item, bool>>(body, input);
        }
    }

}