using Application.Shared.Commands.DynamicFields;
using Application.Shared.Commands.Orders;
using Application.Shared.Events;
using Application.Shared.ViewModels;
using AutoMapper;
using Domain.Core.Entities;
using Domain.Core.Enums;
using Domain.Core.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Shared.Handlers.DynamicFields
{
    internal class DynamicFieldsCommandHandler : IRequestHandler<DynamicFieldsCommand, Result>
    {

        public DynamicFieldsCommandHandler(
            IUnitOfWork<TbView> uowTbView,
            IUnitOfWork<TbField> uowTbField)
        {
            UowTbView = uowTbView;
            UowTbField = uowTbField;
        }

        IUnitOfWork<TbView> UowTbView { get; }
        IUnitOfWork<TbField> UowTbField { get; }
        public async Task<Result> Handle(DynamicFieldsCommand request, CancellationToken cancellationToken)
        {
            var textFieldRequests = request.DynamicFieldsPostVm.AvailableTextFields?.Any() ?? false;
            if (textFieldRequests)
            {
                foreach (var view in request.DynamicFieldsPostVm.AvailableTextFields?.Where(x => !string.IsNullOrEmpty(x.FieldName)))
                {
                    var entity = new TbView()
                    {
                        TypeId = 1,
                        Name = view.FieldName
                    };
                    await UowTbView.Repository.InsertOrUpdate(entity);
                    entity.AddDomainEvent(new TbCustomerFieldsEvent(entity));
                }
            }

            if (!string.IsNullOrEmpty(request.DynamicFieldsPostVm.DropdownKeyName) && (request.DynamicFieldsPostVm.DropdownValues?.Any() ?? false))
            {
                var res = await UowTbView.Repository.InsertOrUpdate(new TbView()
                {
                    TypeId = 2,
                    Name = request.DynamicFieldsPostVm.DropdownKeyName
                });

                if (res.Item2 <= 0) return new Result(false, default); 
                foreach (var fields in request.DynamicFieldsPostVm.DropdownValues.Where(x => !string.IsNullOrEmpty(x.DropdownFieldName)))
                {
                    await UowTbField.Repository.InsertOrUpdate(new TbField()
                    {
                        Name = fields.DropdownFieldName,
                        ViewId = res.Item2
                    });
                }
            }
            else
            {

            }
            return  new Result(false, default);
        }
    }
}
