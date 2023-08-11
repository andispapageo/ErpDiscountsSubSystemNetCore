using Application.Shared.ViewModels.TextAreaFields;
using Domain.Core.Entities;
using Domain.Core.Interfaces;
using FluentValidation;

namespace Application.Shared.Commands.DynamicFields
{
    public class DynamicFieldsCommandValidator : AbstractValidator<DynamicFieldsCommand>
    {
        IUnitOfWork<TbView> UowTbView { get; }

        public DynamicFieldsCommandValidator(
            IUnitOfWork<TbView> uowTbView,
            IUnitOfWork<TbField> uowTbField)
        {
            UowTbView = uowTbView;

            RuleFor(v => v.DynamicFieldsPostVm).NotEmpty().NotNull();
            RuleFor(v => v.DynamicFieldsPostVm).Must(NotEmptyFields)
           .WithMessage("'{PropertyName}' must be unique and or not empty.")
           .WithErrorCode("Unique");

            RuleFor(v => v.DynamicFieldsPostVm.AvailableTextFields).Must(AvailableTextFieldsUnique)
              .WithMessage("'{PropertyName}' must be unique.")
              .WithErrorCode("Unique");

            RuleFor(v => v.DynamicFieldsPostVm.DropdownKeyName).Must(DropDownNameExists)
               .WithMessage("'{PropertyName}' must be unique.")
               .WithErrorCode("Unique");

            RuleFor(v => v.DynamicFieldsPostVm).Must(NotEmptyFieldsAndDropdowns)
               .WithMessage("'Dropdown key must not be empty.")
               .WithErrorCode("Unique");

        }

        public bool NotEmptyFields(DynamicFieldsPostVm? dynamicFieldsPostVm)
        {
            if (!dynamicFieldsPostVm.AvailableTextFields?.Any() ?? true) return true;
            var res = dynamicFieldsPostVm.AvailableTextFields?.All(x => string.IsNullOrEmpty(x.FieldName));
            return res.HasValue ? !res.Value : true;
        }

        public bool NotEmptyFieldsAndDropdowns(DynamicFieldsPostVm? dynamicFieldsPostVm)
        {
            return !(!dynamicFieldsPostVm.AvailableTextFields?.Any() ?? true) && string.IsNullOrEmpty(dynamicFieldsPostVm.DropdownKeyName);
        }

        public bool AvailableTextFieldsUnique(IEnumerable<DynamicFieldsVm>? availableTextFields)
        {
            if (!availableTextFields?.Any() ?? true) return true;
            foreach (var view in availableTextFields)
            {
                if (UowTbView.Repository.GetQuery(x => x.Name == view.FieldName)?.Any() ?? false) { return false; }
            }
            return true;
        }

        public bool DropDownNameExists(string? dropdownName)
        {
            if (string.IsNullOrEmpty(dropdownName)) return true;
            if (!UowTbView.Repository.GetQuery(x => x.Name == dropdownName)?.Any() ?? true) return true;
            return false;
        }
    }
}
