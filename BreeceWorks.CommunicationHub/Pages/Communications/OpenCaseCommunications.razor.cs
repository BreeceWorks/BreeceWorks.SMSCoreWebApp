using BreeceWorks.CommunicationHub.Data.Contracts;
using BreeceWorks.Shared.CaseObjects;
using BreeceWorks.Shared.Enums;
using BreeceWorks.Shared.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.JSInterop;
using System.Net.Http.Headers;
using System.Reflection;
using static BreeceWorks.Shared.Constants;

namespace BreeceWorks.CommunicationHub.Pages.Communications
{
    public partial class OpenCaseCommunications
    {
        private HubConnection? hubConnection;
        private string? sender;
        private string? recipient;
        private string? messageInput;


        private BreeceWorks.Shared.CaseObjects.CaseTranscript? caseTranscript { get; set; }
        private String? SMSCoreWebApi { get; set; }
        private String? curMessageText {  get; set; }
        private String? curAttachmentID {  get; set; } 
        private String ErrorMessage { get; set; }

        private string _inputFileId = Guid.NewGuid().ToString();

        [Inject]
        private IJSRuntime JsRuntime { get; set; }


        [Inject]
        private ICommunicationService CommunicationService
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
            await SetUpSignalRHub();

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
                        ErrorMessage = caseTranscript.Errors[0].Detail;
                    }
                }
            }

        }

        private async Task SetUpSignalRHub()
        {
            String? smsSimulatorURL = ConfigureService.GetValue("BreeceWorks.CommunicationWebApi");
            String? webApiKey = ConfigureService.GetValue("CommunicationWebApiKey");

            if (!String.IsNullOrEmpty(smsSimulatorURL) && !String.IsNullOrEmpty(webApiKey))
            {

                hubConnection = new HubConnectionBuilder()
                .WithUrl(smsSimulatorURL + "/communicationhub", (opts) =>
                {
                    opts.Headers.Add(SystemConstants.Headers.XAPIKEY, webApiKey);
                })
                .Build();

                hubConnection.On<CaseMessage>("ReceiveMessage", (caseMessage) =>
                {
                    if (caseTranscript != null && caseTranscript.CaseData != null && caseTranscript.CaseData.Id == caseMessage.CaseId) 
                    {
                        caseTranscript.Messages.Add(caseMessage);
                        GoToBottomOfListAsync();
                    }                    
                });
                await hubConnection.StartAsync();
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

        private void GoToBottomOfListAsync()
        {
            AfterRenderAsyncJobs.Add(ScrollToBottomOfList);
            InvokeAsync(StateHasChanged);
        }
        private async Task ScrollToBottomOfList()
        {
            await JsRuntime.InvokeVoidAsync("EndOfList");
        }

        private void SendMessage()
        {
            if (!String.IsNullOrEmpty(curMessageText))
            {
                BreeceWorks.Shared.SMS.SMSOutgoingCommunication sMSOutgoingCommunication = new BreeceWorks.Shared.SMS.SMSOutgoingCommunication()
                {
                    message = curMessageText,
                    source = TemplateMessageSource.assigned.ToString(),
                };

                if (caseTranscript != null && caseTranscript.CaseData != null && caseTranscript.CaseData.Id != null)
                {

                    sMSOutgoingCommunication.caseId = caseTranscript.CaseData.Id.Value.ToString();
                }
                if (!String.IsNullOrEmpty(curAttachmentID))
                {
                    sMSOutgoingCommunication.attachmentIDs = new List<string>();
                    sMSOutgoingCommunication.attachmentIDs.Add(curAttachmentID);
                }

                CommunicationService.SendMessage(sMSOutgoingCommunication);
                _inputFileId = Guid.NewGuid().ToString();
                curMessageText = String.Empty;
                GoToBottomOfList();
            }
            StateHasChanged();
        }

        public async Task FileUploaded(InputFileChangeEventArgs e)
        {
            var browserFile = e.File;
            if (browserFile != null)
            {

                try
                {

                    String randomFile = Path.GetTempFileName();
                    String? extension = Path.GetExtension(browserFile.Name);
                    String? targetFilePath = Path.ChangeExtension(randomFile, extension);



                    using (var content = new MultipartFormDataContent())
                    {
                        var fileStream = browserFile.OpenReadStream(MAX_FILESIZE);
                        content.Add(new StreamContent(fileStream), "file", targetFilePath);

                        curAttachmentID = await CommunicationService.UploadAttachment(content);
                    }



                }
                catch (Exception ex)
                {
                    ErrorMessage = ex.Message;
                }

            }
        }

        public async ValueTask DisposeAsync()
        {
            if (hubConnection is not null)
            {
                await hubConnection.DisposeAsync();
            }
        }
    }

    public class Conversation
    {
        public Conversation()
        {
            Messages = new List<Message>();
        }
        public String FirstParty { get; set; }
        public String SecondParty { get; set; }
        public List<Message> Messages { get; set; }
    }

    public class Message
    {
        public Message()
        {
            AttachmentURLs = new List<String>();
        }
        public String To { get; set; }
        public String From { get; set; }
        public String MessageContent { get; set; }
        public List<String> AttachmentURLs { get; set; }
    }
}
