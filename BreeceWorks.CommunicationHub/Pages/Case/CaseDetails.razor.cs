﻿using BreeceWorks.CommunicationHub.Data;
using BreeceWorks.CommunicationHub.Data.Contracts;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.WebUtilities;

namespace BreeceWorks.CommunicationHub.Pages.Case
{
    public partial class CaseDetails
    {
        private BreeceWorks.Shared.CaseObjects.Case? curCase { get; set; }
        private Guid _curCaseID;
        private Guid curCaseID 
        { 
            get
            {
                return _curCaseID;
            }
            set
            {
                _curCaseID = value;
            }
        }

        [Inject]
        private ICommunicationService CommunicationService
        {
            get;
            set;
        }

        [Inject]
        private NavigationManager NavManager { get; set; }

        public String? AssignOperatorID { get; set; }

        protected override async Task OnInitializedAsync()
        {
            var uri = NavManager.ToAbsoluteUri(NavManager.Uri);
            var queryStrings = QueryHelpers.ParseQuery(uri.Query);
            if (queryStrings.TryGetValue("CaseID", out var caseId))
            {                
                if (Guid.TryParse(caseId, out _curCaseID))
                {
                    curCase = await CommunicationService.GetCaseByID(curCaseID);
                    if (curCase.PrimaryContact != null)
                    {
                        AssignOperatorID = curCase.PrimaryContact.Email;
                    }
                }
            }
        }

        protected void EditCase(Guid? caseId)
        {
            NavManager.NavigateTo(string.Format("/editCase?CaseID={0}", caseId));
        }
        protected void ReturnToList()
        {
            NavManager.NavigateTo("/casemanagement");
        }
        protected async void ReassignCase()
        {
            if (AssignOperatorID != null && curCase != null &&  curCase.CaseData != null && curCase.CaseData.Id != null)
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

        protected void OpenCaseCommunications(Guid caseId)
        {
            NavManager.NavigateTo(string.Format("/opencasecommunications?CaseID={0}", caseId));
        }
    }
}
