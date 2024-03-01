using BreeceWorks.CommunicationHub.Data.Contracts;
using BreeceWorks.Shared.CaseObjects;
using Microsoft.AspNetCore.Components;


namespace BreeceWorks.CommunicationHub.Pages.Case
{
    public partial class CaseManagement
    {
        [Inject]
        private ICommunicationService CommunicationService
        {
            get;
            set;
        }

        [Inject]
        private NavigationManager NavManager { get; set; }

        private CaseRspses? allCases;

        protected override async Task OnInitializedAsync()
        {
            allCases = await CommunicationService.GetAllCases();
        }

        protected void GetCaseDetails(Guid caseId)
        {
            NavManager.NavigateTo(string.Format("/caseDetails?CaseID={0}", caseId));
        }

        protected void CreateCase()
        {
            NavManager.NavigateTo("/newCase");
        }

    }
}
