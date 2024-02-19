using BreeceWorks.CommunicationHub.Data;
using BreeceWorks.Shared.CaseObjects;
using Microsoft.AspNetCore.Components;

namespace BreeceWorks.CommunicationHub.Pages.Customer
{
    public partial class CustomerManagement
    {
        private Users users;
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
            users = await CommunicationService.GetAllUsers();
        }

        protected void GetClaims(Guid userId)
        {
            NavManager.NavigateTo(string.Format("/customercases?UserID={0}", userId));
        }
    }
}
