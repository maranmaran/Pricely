using AutoMapper;
using Common.Exceptions;
using Grpc.Core;
using ItemService.API.Protos;
using ItemService.Business.Commands.Allergens.Create;
using ItemService.Business.Commands.Allergens.Delete;
using ItemService.Business.Commands.Allergens.Update;
using ItemService.Business.Queries.Allergens.GetAllergen;
using ItemService.Business.Queries.Allergens.GetAllergens;
using ItemService.Persistence.DTOModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Allergen = ItemService.API.Protos.Allergen;

namespace ItemService.API.Controllers
{
    public class AllergenGrpcService : AllergenService.AllergenServiceBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public AllergenGrpcService(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        /// <summary>Retrieves allergen</summary>
        /// <returns>Single allergen object</returns>
        /// <exception cref="NotFoundException">Not found</exception>
        /// <remarks>Retrieves single allergen </remarks>
        public override async Task<Allergen> Get(GetRequest request, ServerCallContext context)
        {
            var allergenDto = await _mediator.Send(new GetAllergenQuery(Guid.Parse(request.Id)), context.CancellationToken);

            return _mapper.Map<Allergen>(allergenDto);
        }

        /// <summary>
        /// Retrieves allergens
        /// </summary>
        /// <remarks>
        /// Retrieves all allergens if no query parameters are specified
        /// </remarks>
        public override async Task<GetAllResponse> GetAll(GetAllRequest request, ServerCallContext context)
        {
            var allergens = await _mediator.Send(new GetAllergensQuery(), context.CancellationToken);

            return new GetAllResponse()
            {
                Allergens = { _mapper.Map<IEnumerable<Allergen>>(allergens) }
            };
        }

        /// <summary>
        /// Creates allergen
        /// </summary>
        /// <remarks>
        /// Creates allergen that can be placed on menu
        /// </remarks>
        public override async Task<CreateResponse> Create(CreateRequest request, ServerCallContext context)
        {
            var allergen = new AllergenDto()
            {
                Name = request.Name,
                Description = request.Description
            };

            var id = await _mediator.Send(new CreateAllergenCommand(allergen), context.CancellationToken);

            return new CreateResponse()
            {
                Id = id.ToString()
            };
        }

        /// <summary>
        /// Updates allergen
        /// </summary>
        /// <remarks>
        /// Updates allergen that can be placed on menu
        /// Publishes event that allergen has been changed
        /// </remarks>
        public override async Task<UpdateResponse> Update(UpdateRequest request, ServerCallContext context)
        {
            var allergen = new AllergenDto()
            {
                Id = Guid.Parse(request.Id),
                Name = request.Name,
                Description = request.Description
            };

            await _mediator.Send(new UpdateAllergenCommand(allergen), context.CancellationToken);

            return new UpdateResponse();
        }

        /// <summary>
        /// Delete allergen
        /// </summary>
        /// <remarks>
        /// Deletes allergen
        /// Publishes event that allergen has been changed
        /// </remarks>
        public override async Task<DeleteResponse> Delete(DeleteRequest request, ServerCallContext context)
        {
            await _mediator.Send(new DeleteAllergenCommand(Guid.Parse(request.Id)), context.CancellationToken);

            return new DeleteResponse();
        }

    }
}