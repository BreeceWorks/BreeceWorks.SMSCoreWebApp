using BreeceWorks.CommunicationHub.Data;
using BreeceWorks.Shared.Enums;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.WebUtilities;

namespace BreeceWorks.CommunicationHub.Pages.Case
{
    public partial class EditCase
    {
        private BreeceWorks.Shared.CaseObjects.Case? curCase { get; set; }
        private String? ErrorMessage { get; set; }

        [Inject]
        private CommunicationService CommunicationService
        {
            get;
            set;
        }

        [Inject]
        private NavigationManager NavManager { get; set; }

        public String? AssignOperatorID { get; set; }
        public String? PrivacySelection { get; set; }

        public String? LanguagePreferenceSelection { get; set; }


        protected override async Task OnInitializedAsync()
        {
            var uri = NavManager.ToAbsoluteUri(NavManager.Uri);
            var queryStrings = QueryHelpers.ParseQuery(uri.Query);
            if (queryStrings.TryGetValue("CaseID", out var caseId))
            {
                Guid curCaseID;
                if (Guid.TryParse(caseId, out curCaseID))
                {
                    curCase = await CommunicationService.GetCaseByID(curCaseID);
                    if (curCase.PrimaryContact != null)
                    {
                        AssignOperatorID = curCase.PrimaryContact.Email;
                    }
                    PrivacySelection = curCase.Privacy;
                    LanguagePreferenceSelection = curCase.LanguagePreference;
                }
            }
        }

        protected void Cancel()
        {
            NavManager.NavigateTo(string.Format("/caseDetails?CaseID={0}", curCase.CaseData.Id));
        }
        protected async void SaveCase()
        {
            if (!String.IsNullOrEmpty(LanguagePreferenceSelection))
            {
                curCase.LanguagePreference = LanguagePreferenceSelection;
            }
            if (!String.IsNullOrEmpty(PrivacySelection))
            {
                curCase.Privacy = PrivacySelection;
            }
            curCase = await CommunicationService.UpdateCase(curCase);
            NavManager.NavigateTo(string.Format("/caseDetails?CaseID={0}", curCase.CaseData.Id));
        }

        protected void AddLineOfBusiness()
        {
            if (curCase != null && curCase.CaseData != null)
            {
                curCase.CaseData.LineOfBusiness = new BreeceWorks.Shared.CaseObjects.LineOfBusiness();
                StateHasChanged();
            }
        }

        protected void RemoveLineOfBusiness()
        {
            if (curCase != null && curCase.CaseData != null)
            {
                curCase.CaseData.LineOfBusiness = null;
                StateHasChanged();
            }
        }

        protected async void ReassignCase()
        {
            if (AssignOperatorID != null && curCase != null && curCase.CaseData != null && curCase.CaseData.Id != null)
            {
                curCase = await CommunicationService.AssignCase(curCase.CaseData.Id.Value, AssignOperatorID);
                if (curCase.PrimaryContact != null)
                {
                    AssignOperatorID = curCase.PrimaryContact.Email;
                }
                else
                {
                    AssignOperatorID = null;
                }
                StateHasChanged();
            }
        }
        protected async void UnassignCase()
        {
            if (curCase != null && curCase.CaseData != null && curCase.CaseData.Id != null)
            {
                curCase = await CommunicationService.AssignCase(curCase.CaseData.Id.Value, null);
                if (curCase.PrimaryContact != null)
                {
                    AssignOperatorID = curCase.PrimaryContact.Email;
                }
                else
                {
                    AssignOperatorID = null;
                }
                StateHasChanged();
            }
        }

        protected async void ReopenCase()
        {
            if (curCase != null && curCase.CaseData != null && curCase.CaseData.Id != null)
            {

                curCase = await CommunicationService.ReopenCase(curCase.CaseData.Id.Value);
                StateHasChanged();
            }
        }
        protected async void CloseCase()
        {
            if (curCase != null && curCase.CaseData != null && curCase.CaseData.Id != null)
            {
                curCase = await CommunicationService.CloseCase(curCase.CaseData.Id.Value);
                StateHasChanged();
            }
        }
    }
}
