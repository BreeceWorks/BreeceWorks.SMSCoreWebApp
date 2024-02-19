using BreeceWorks.CommunicationHub.Data;
using Microsoft.AspNetCore.Components;

namespace BreeceWorks.CommunicationHub.Pages.Operator
{
    public partial class NewOperator
    {
        [Inject]
        private CommunicationService CommunicationService { get; set; }

        [Inject]
        private NavigationManager NavManager { get; set; }

        public BreeceWorks.Shared.CaseObjects.Operator? CreatingOperator { get; set; }
        public String? ErrorMessage { get; set; }

        protected override async Task OnInitializedAsync()
        {
            CreatingOperator = new BreeceWorks.Shared.CaseObjects.Operator();
        }

        protected void AddRole()
        {
            if (CreatingOperator != null)
            {
                if (CreatingOperator.Roles == null)
                {
                    CreatingOperator.Roles = new List<String>();
                }
                CreatingOperator.Roles.Add(String.Empty);
            }
        }

        protected void RemoveRole(int index)
        {
            if (CreatingOperator != null)
            {
                CreatingOperator.Roles.RemoveAt(index);
            }
        }

        private void UpdateValue(int i, Microsoft.AspNetCore.Components.ChangeEventArgs args)
        {
            if (args != null && args.Value != null)
            {
                if (CreatingOperator != null)
                {
                    CreatingOperator.Roles[i] = (string)args.Value;
                }
            }
        }

        private async void Submit()
        {
            ErrorMessage = string.Empty;
            if (CreatingOperator != null)
            {
                if (CreatingOperator.Roles != null && CreatingOperator.Roles.Count > 0)
                {
                    List<String> roles = new List<String>();
                    foreach (String role in CreatingOperator.Roles)
                    {
                        if (!String.IsNullOrEmpty(role)) roles.Add(role);
                    }
                    CreatingOperator.Roles = roles;
                }
                if (String.IsNullOrEmpty(CreatingOperator.FirstName)
                    || String.IsNullOrEmpty(CreatingOperator.LastName)
                    || String.IsNullOrEmpty(CreatingOperator.Email)
                    || String.IsNullOrEmpty(CreatingOperator.PhoneNumber))
                {
                    ErrorMessage = "Check required fields";
                }
                else
                {
                    CreatingOperator = await CommunicationService.CreateOperator(CreatingOperator);
                }
            }
            if (CreatingOperator != null && CreatingOperator.Errors != null && CreatingOperator.Errors.Count > 0)
            {
                ErrorMessage = CreatingOperator.Errors[0].Detail;
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
