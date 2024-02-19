using BreeceWorks.CommunicationHub.Data;
using BreeceWorks.Shared.CaseObjects;
using Microsoft.AspNetCore.Components;
using System.Text;


namespace BreeceWorks.CommunicationHub.Pages.Operator
{
    public partial class OperatorManagement
    {
        private Operators? operators { get; set; }

        private String Error { get; set; }

        [Inject]
        private CommunicationService CommunicationService
        {
            get;
            set;
        }

        [Inject]
        private NavigationManager NavManager { get; set; }


        protected override async Task OnInitializedAsync()
        {
            Error = String.Empty;
            operators = await CommunicationService.GetAllOperators();
            if (operators != null && operators.Errors != null && operators.Errors.Count > 0)
            {
                Error = operators.Errors[0].Detail;
            }
        }

        protected String GetRoles(List<string>? roles)
        {
            StringBuilder rolesString = new StringBuilder();
            if (roles != null)
            {
                Int32 curCount = 1;
                foreach (var role in roles)
                {
                    rolesString.Append(role);
                    if (curCount < roles.Count)
                    {
                        rolesString.Append(", ");
                    }
                    curCount++;
                }
            }
            return rolesString.ToString();
        }

        protected void EditOperator(Guid? operatorId)
        {
            Error = String.Empty;
            NavManager.NavigateTo(string.Format("/editOperator?OperatorIdID={0}", operatorId));

        }

        protected async void DeleteOperator(Guid? operatorId)
        {
            Error = String.Empty;
            if (operatorId != null && operators != null)
            {
                BreeceWorks.Shared.CaseObjects.Operator? operatorToDelete = operators.Operators1.Where(o => o.Id == operatorId).FirstOrDefault();
                if (operatorToDelete != null && operatorToDelete.Id.HasValue)
                {
                    operatorToDelete = await CommunicationService.DeleteOperator(operatorToDelete.Id.Value);
                    if (operatorToDelete.Errors != null && operatorToDelete.Errors.Count > 0 )
                    {
                        Error = operatorToDelete.Errors[0].Detail;
                    }
                }
            }
            operators = await CommunicationService.GetAllOperators();
            if (operators != null && operators.Errors != null && operators.Errors.Count > 0)
            {
                Error = operators.Errors[0].Detail;
            }
            StateHasChanged();
        }

        protected void CreateOperator()
        {
            Error = String.Empty;
            NavManager.NavigateTo("/newOperator");
        }

        private async void InitializeModel()
        {
            
        }
    }

}

