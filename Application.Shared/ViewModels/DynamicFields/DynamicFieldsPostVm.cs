using Application.Shared.ViewModels.TextAreaFields.Fields;

namespace Application.Shared.ViewModels.TextAreaFields
{
    public class DynamicFieldsPostVm
    {
        public IEnumerable<DynamicFieldsVm>? AvailableTextFields { get; set; }
        public IEnumerable<DropdownFields>? DropdownValues { get; set; }
        public string? DropdownKeyName { get; set; }
        public Result Result { get; set; }
    }
}
