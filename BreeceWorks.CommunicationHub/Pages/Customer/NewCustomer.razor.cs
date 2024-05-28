using BreeceWorks.CommunicationHub.Data.Contracts;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace BreeceWorks.CommunicationHub.Pages.Customer
{
    public partial class NewCustomer
    {
        [Inject]
        private ICommunicationService CommunicationService { get; set; }

        [Inject]
        private NavigationManager NavManager { get; set; }

        public BreeceWorks.Shared.CaseObjects.Customer? CreatingCustomer { get; set; }
        public String? ErrorMessage { get; set; }
        private Boolean generalFormValid { get; set; }


        protected override async Task OnInitializedAsync()
        {
            CreatingCustomer = new BreeceWorks.Shared.CaseObjects.Customer();
        }


        private async void Submit(EditContext editContext)
        {
            generalFormValid = editContext.Validate();
            ErrorMessage = string.Empty;
            if (generalFormValid)
            {
                if (CreatingCustomer != null)
                {
                    CreatingCustomer = await CommunicationService.SaveUser(CreatingCustomer);
                }
                if (CreatingCustomer != null && CreatingCustomer.Errors != null && CreatingCustomer.Errors.Count > 0)
                {
                    ErrorMessage = CreatingCustomer.Errors[0].Detail;
                    StateHasChanged();
                }
                else if (String.IsNullOrEmpty(ErrorMessage))
                {
                    NavManager.NavigateTo("/customermanagement");
                }
            }
        }

        private void Cancel()
        {
            NavManager.NavigateTo("/customermanagement");
        }

    }
}
