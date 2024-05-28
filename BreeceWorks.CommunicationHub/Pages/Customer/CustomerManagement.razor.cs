using BreeceWorks.CommunicationHub.Data.Contracts;
using BreeceWorks.Shared.CaseObjects;
using Microsoft.AspNetCore.Components;

namespace BreeceWorks.CommunicationHub.Pages.Customer
{
    public partial class CustomerManagement
    {
        private Users users;
        
        private String Error { get; set; }


        [Inject]
        private ICommunicationService CommunicationService
        {
            get;
            set;
        }

        [Inject]
        private NavigationManager NavManager { get; set; }


        protected override async Task OnInitializedAsync()
        {
            users = await CommunicationService.GetAllUsers();
            if (users != null && users.Errors != null && users.Errors.Count > 0)
            {
                Error = users.Errors[0].Detail;
            }
        }

        protected void GetClaims(Guid userId)
        {
            NavManager.NavigateTo(string.Format("/customercases?UserID={0}", userId));
        }

        protected void EditCustomer(Guid customerId)
        {
            Error = String.Empty;
            NavManager.NavigateTo(string.Format("/editCustomer?CustomerID={0}", customerId));

        }

        protected void CreateCustomer()
        {
            NavManager.NavigateTo("/newCustomer");
        }

    }
}
