﻿using AutoMapper;
using Common.Exceptions;
using DataAccess.Sql.Interfaces;
using EventBus.Infrastructure.Interfaces;
using ItemService.Domain.Entities;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ItemService.Business.Commands.Items.Update
{
    internal class UpdateItemCommandHandler : IRequestHandler<UpdateItemCommand, Unit>
    {
        private readonly IGenericEfRepository<Item> _repository;
        private readonly IMapper _mapper;
        private readonly IEventBus _eventBus;

        public UpdateItemCommandHandler(IGenericEfRepository<Item> repository, IMapper mapper, IEventBus eventBus)
        {
            _repository = repository;
            _mapper = mapper;
            _eventBus = eventBus;
        }

        public async Task<Unit> Handle(UpdateItemCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var entity = _mapper.Map<Item>(request.Item);

                await _repository.Update(entity, cancellationToken);

                _eventBus.Publish(new ItemUpdatedEvent(request.Item));

                return Unit.Value;
            }
            catch (Exception e)
            {
                throw new UpdateException(request.Item.Id, e);
            }
        }
    }
}