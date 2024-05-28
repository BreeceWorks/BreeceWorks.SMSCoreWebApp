using BreeceWorks.CommunicationHub.Data.Contracts;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.WebUtilities;

namespace BreeceWorks.CommunicationHub.Pages.Operator
{
    public partial class EditOperator
    {
        [Inject]
        private ICommunicationService CommunicationService { get; set; }

        [Inject]
        private NavigationManager NavManager { get; set; }

        public BreeceWorks.Shared.CaseObjects.Operator? EditingOperator { get; set; }
        public String? ErrorMessage { get; set; }

        protected override async Task OnInitializedAsync()
        {
            var uri = NavManager.ToAbsoluteUri(NavManager.Uri);
            var queryStrings = QueryHelpers.ParseQuery(uri.Query);
            if (queryStrings.TryGetValue("OperatorID", out var operatorId))
            {
                Guid curOperatorID;
                if (Guid.TryParse(operatorId, out curOperatorID))
                {
                    EditingOperator = await CommunicationService.GetOperatorByID(curOperatorID);
                }
            }
        }

        protected void AddRole()
        {
            if (EditingOperator != null)
            {
                if (EditingOperator.Roles == null)
                {
                    EditingOperator.Roles = new List<String>();
                }
                EditingOperator.Roles.Add(String.Empty);
            }
        }

        protected void RemoveRole(int index)
        {
            if (EditingOperator != null)
            {
                EditingOperator.Roles.RemoveAt(index);
            }
        }

        private void UpdateValue(int i, Microsoft.AspNetCore.Components.ChangeEventArgs args)
        {
            if (args != null && args.Value != null)
            {
                if (EditingOperator != null)
                {
                    EditingOperator.Roles[i] = (string)args.Value;
                }
            }
        }

        private async void Submit()
        {
            if (EditingOperator != null)
            {
                if (EditingOperator.Roles != null && EditingOperator.Roles.Count > 0)
                {
                    List<String> roles = new List<String>();
                    foreach (String role in EditingOperator.Roles)
                    {
                        if (!String.IsNullOrEmpty(role)) roles.Add(role);
                    }
                    EditingOperator.Roles = roles;
                }
                if (String.IsNullOrEmpty(EditingOperator.FirstName)
                    || String.IsNullOrEmpty(EditingOperator.LastName)
                    || String.IsNullOrEmpty(EditingOperator.Email)
                    || String.IsNullOrEmpty(EditingOperator.PhoneNumber))
                {
                    ErrorMessage = "Check required fields";
                }
                else
                {
                    EditingOperator = await CommunicationService.UpdateOperator(EditingOperator);
                }
            }
            if (EditingOperator != null && EditingOperator.Errors != null && EditingOperator.Errors.Count > 0)
            {
                ErrorMessage = EditingOperator.Errors[0].Detail;
                StateHasChanged();
            }
            else if (String.IsNullOrEmpty(ErrorMessage))
            {
                NavManager.NavigateTo("/operatorManagement");
            }
        }

        private void Cancel()
        {
                NavManager.NavigateTo("/operatorManagement");
        }

    }
}
