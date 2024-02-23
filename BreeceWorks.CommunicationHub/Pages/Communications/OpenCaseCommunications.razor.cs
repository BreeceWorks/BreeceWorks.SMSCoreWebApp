using BreeceWorks.CommunicationHub.Data;
using BreeceWorks.Shared.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.JSInterop;

namespace BreeceWorks.CommunicationHub.Pages.Communications
{
    public partial class OpenCaseCommunications
    {
        private BreeceWorks.Shared.CaseObjects.CaseTranscript? caseTranscript { get; set; }
        private String? SMSCoreWebApi { get; set; }
        private String Error { get; set; }

        [Inject]
        private IJSRuntime JsRuntime { get; set; }


        [Inject]
        private CommunicationService CommunicationService
        {
            get;
            set;
        }

        [Inject]
        private IConfigureService ConfigureService
        {
            get;
            set;
        }

        [Inject]
        private NavigationManager NavManager { get; set; }

        List<Func<Task>> AfterRenderAsyncJobs = new();

        protected override async Task OnInitializedAsync()
        {
            SMSCoreWebApi = ConfigureService.GetValue("BreeceWorks.SMSCoreWebApi");
            var uri = NavManager.ToAbsoluteUri(NavManager.Uri);
            var queryStrings = QueryHelpers.ParseQuery(uri.Query);
            if (queryStrings.TryGetValue("CaseID", out var caseId))
            {
                Guid curCaseID;
                if (Guid.TryParse(caseId, out curCaseID))
                {
                    caseTranscript = await CommunicationService.GetCaseTranscript(curCaseID);
                    if (caseTranscript != null && caseTranscript.Errors != null && caseTranscript.Errors.Count > 0)
                    {
                        Error = caseTranscript.Errors[0].Detail;
                    }
                }
            }

        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            while (AfterRenderAsyncJobs.Any())
            {
                var job = AfterRenderAsyncJobs.First();
                AfterRenderAsyncJobs.Remove(job);
                await job.Invoke();
            }
        }

        private void GoToBottomOfList()
        {
            AfterRenderAsyncJobs.Add(ScrollToBottomOfList);
            StateHasChanged();
        }

        private async Task ScrollToBottomOfList()
        {
            await JsRuntime.InvokeVoidAsync("EndOfList");
        }
    }
}
