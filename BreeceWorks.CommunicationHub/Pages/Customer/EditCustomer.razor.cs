using BreeceWorks.CommunicationHub.Data.Contracts;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.WebUtilities;

namespace BreeceWorks.CommunicationHub.Pages.Customer
{
    public partial class EditCustomer
    {
        [Inject]
        private ICommunicationService CommunicationService { get; set; }

        [Inject]
        private NavigationManager NavManager { get; set; }

        public BreeceWorks.Shared.CaseObjects.Customer? EditingCustomer { get; set; }
        public String? ErrorMessage { get; set; }
        private Boolean generalFormValid { get; set; }


        protected override async Task OnInitializedAsync()
        {
            var uri = NavManager.ToAbsoluteUri(NavManager.Uri);
            var queryStrings = QueryHelpers.ParseQuery(uri.Query);
            if (queryStrings.TryGetValue("CustomerID", out var operatorId))
            {
                Guid curCustomerID;
                if (Guid.TryParse(operatorId, out curCustomerID))
                {
                    EditingCustomer = await CommunicationService.GetUserById(curCustomerID);
                }
            }
        }
        private async void Submit(EditContext editContext)
        {
            generalFormValid = editContext.Validate();
            ErrorMessage = string.Empty;
            if (generalFormValid)
            {
                if (EditingCustomer != null)
                {
                    EditingCustomer = await CommunicationService.SaveUser(EditingCustomer);
                }
                if (EditingCustomer != null && EditingCustomer.Errors != null && EditingCustomer.Errors.Count > 0)
                {
                    ErrorMessage = EditingCustomer.Errors[0].Detail;
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
