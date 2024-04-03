using BreeceWorks.CommunicationHub.Data.Contracts;
using BreeceWorks.Shared.CaseObjects;
using Microsoft.AspNetCore.Components;

namespace BreeceWorks.CommunicationHub.Pages.Communications
{
    public partial class CommunicationManager
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

        protected void OpenCaseCommunications(Guid caseId)
        {
            NavManager.NavigateTo(string.Format("/opencasecommunications?CaseID={0}", caseId));
        }

    }
}
