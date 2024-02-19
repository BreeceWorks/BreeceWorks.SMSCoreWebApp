using BreeceWorks.CommunicationHub.Data;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.WebUtilities;


namespace BreeceWorks.CommunicationHub.Pages.Customer
{
    public partial class CustomerCases
    {
        [Inject]
        private CommunicationService CommunicationService
        {
            get;
            set;
        }

        [Inject]
        private NavigationManager NavManager { get; set; }

        private BreeceWorks.Shared.CaseObjects.ActiveCases? activeCases;
        protected override async Task OnInitializedAsync()
        {
            var uri = NavManager.ToAbsoluteUri(NavManager.Uri);
            var queryStrings = QueryHelpers.ParseQuery(uri.Query);
            if (queryStrings.TryGetValue("UserID", out var userId))
            {
                Guid curUserID;
                if (Guid.TryParse(userId, out curUserID))
                {
                    activeCases = await CommunicationService.GetCasesForUserByUserID(curUserID);
                }
            }
        }

        protected void GetCaseDetails(Guid caseId)
        {
            NavManager.NavigateTo(string.Format("/caseDetails?CaseID={0}", caseId));
        }
    }
}
