﻿using Application.Shared.Events;
using Domain.Core.Entities;
using Domain.Core.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Shared.Handlers.EventHandlers
{
    internal class TbCustomerFieldsEvenHandler : INotificationHandler<TbCustomerFieldsEvent>
    {
        private readonly ILogger<TbCustomerFieldsEvenHandler> _logger;

        public TbCustomerFieldsEvenHandler(ILogger<TbCustomerFieldsEvenHandler> logger,
                                           IUnitOfWork<TbCustomer> uowCustomer,
                                           IUnitOfWork<TbCustomerField> uowCustomerField)
        {
            _logger = logger;
            UowCustomer = uowCustomer;
            UowCustomerField = uowCustomerField;
        }

        public IUnitOfWork<TbCustomer> UowCustomer { get; }
        public IUnitOfWork<TbCustomerField> UowCustomerField { get; }

        public async Task Handle(TbCustomerFieldsEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Domain Event: {DomainEvent}", notification.GetType().Name);
            foreach (var customer in UowCustomer.Repository.GetQuery())
            {
                var res = await UowCustomerField.Repository.InsertOrUpdate(new TbCustomerField()
                {
                    CustomerId = customer.Id,
                    ViewId = notification.Item.Id,
                    ViewValue = null,
                });
                _logger.LogInformation("Domain Event TbCustomerFields {result}", res.Item2 >=0 ? "Added : Successfully" : "Not Added Successfully" );
            }
        }
    }
}
