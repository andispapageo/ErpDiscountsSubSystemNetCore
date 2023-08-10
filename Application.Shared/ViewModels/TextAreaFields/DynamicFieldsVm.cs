using Application.Shared.ViewModels.TextAreaFields.Fields;

namespace Application.Shared.ViewModels.TextAreaFields
{
    public class DynamicFieldsVm
    {
        public string FieldName { get; set; }
        public int Type { get; set; }
        public IEnumerable<DropdownFields> DropdownFields { get; set; }
    }
}
