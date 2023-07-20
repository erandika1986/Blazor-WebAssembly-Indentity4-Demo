using BlazorWebAssemblyIdentityDemo.Shared.DTO.Common;
using Microsoft.AspNetCore.Components;

namespace BlazorWebAssemblyIdentityDemo.ClientApp.Components
{
    public partial class DropDown
    {
        [Parameter]
        public string DefaultValue { get; set; } = string.Empty;

        [Parameter]
        public List<DropDownDto>  Items { get; set; } = new List<DropDownDto>();

        [Parameter]
        public EventCallback<string> OnSelectedItemChanged { get; set; }
        
        private async Task SelectedItemChanged(ChangeEventArgs eventArgs)
        {
            if (eventArgs.Value.ToString() == "0")
                return;
            await OnSelectedItemChanged.InvokeAsync(eventArgs.Value.ToString());
        }
    }
}
