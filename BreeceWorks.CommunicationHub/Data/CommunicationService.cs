using BreeceWorks.CommunicationHub.Data.Contracts;
using BreeceWorks.CommunicationHub.Dispatcher.Contracts;
using BreeceWorks.Shared.Services;

namespace BreeceWorks.CommunicationHub.Data.Implementation
{
    public class CommunicationService: ICommunicationService
    {
        private IDispatcher _dispatcher { get; set; }
        private readonly ITranslatorService _translatorService;
        private readonly IConfigureService _configureService;



        public CommunicationService(IDispatcher dispatcher, ITranslatorService translatorService, IConfigureService configureService)
        {
            _dispatcher = dispatcher;
            _translatorService = translatorService;
            _configureService = configureService;
        }
        public async Task<BreeceWorks.Shared.CaseObjects.Users> GetAllUsers()
        {
            Dispatcher.Proxies.Users usersResponse = await _dispatcher.DispatchRequest<Dispatcher.Proxies.Users, Dispatcher.Proxies.ApiClient>(x => x.UserAsync());
            return _translatorService.TranslateToModel(usersResponse);
        }
        public async Task<BreeceWorks.Shared.CaseObjects.Operators> GetAllOperators()
        {
            Dispatcher.Proxies.Operators operatorsResponse = await _dispatcher.DispatchRequest<Dispatcher.Proxies.Operators, Dispatcher.Proxies.OperatorsClient>(x => x.AllAsync());
            return _translatorService.TranslateToModel(operatorsResponse);
        }

        public async Task<BreeceWorks.Shared.CaseObjects.Operator> CreateOperator(BreeceWorks.Shared.CaseObjects.Operator newOperator)
        {
            Dispatcher.Proxies.OperatorCreateRqst operatorCreateRqst = new Dispatcher.Proxies.OperatorCreateRqst()
            {
                DfficeNumber = newOperator.OfficeNumber,
                Email = newOperator.Email,
                FirstName = newOperator.FirstName,
                LastName = newOperator.LastName,
                Mobile = newOperator.PhoneNumber,
                Roles = newOperator.Roles,
                Title = newOperator.Title
            };
            Dispatcher.Proxies.Operator operatorResponse = await _dispatcher.DispatchRequest<Dispatcher.Proxies.Operator, Dispatcher.Proxies.ApiClient>(x => x.OperatorsPostAsync(operatorCreateRqst));
            return _translatorService.TranslateToModel(operatorResponse);
        }

        public async Task<BreeceWorks.Shared.CaseObjects.Operator> DeleteOperator(Guid operatorId)
        {
            Dispatcher.Proxies.Operator operatorResponse = await _dispatcher.DispatchRequest<Dispatcher.Proxies.Operator, Dispatcher.Proxies.ApiClient>(x => x.OperatorsDeleteAsync(operatorId));
            return _translatorService.TranslateToModel(operatorResponse);
        }

        public async Task<BreeceWorks.Shared.CaseObjects.ActiveCases> GetCasesForUserByUserID(Guid userID)
        {
            Dispatcher.Proxies.ActiveCases userResponse = await _dispatcher.DispatchRequest<Dispatcher.Proxies.ActiveCases, Dispatcher.Proxies.ActiveCasesClient>(x => x.IdAsync(userID));
            return _translatorService.TranslateToModel(userResponse);
        }

        public async Task<BreeceWorks.Shared.CaseObjects.Case> GetCaseByID(Guid caseId)
        {
            Dispatcher.Proxies.Case caseResponse = await _dispatcher.DispatchRequest<Dispatcher.Proxies.Case, Dispatcher.Proxies.ApiClient>(x => x.CaseGetAsync(caseId.ToString()));
            return _translatorService.TranslateToModel(caseResponse);
        }

        public async Task<BreeceWorks.Shared.CaseObjects.Case> UpdateCase(BreeceWorks.Shared.CaseObjects.Case updatedCase)
        {
            Dispatcher.Proxies.Case caseResponse = await _dispatcher.DispatchRequest<Dispatcher.Proxies.Case, Dispatcher.Proxies.ApiClient>(x => x.CasePutAsync(updatedCase.CaseData.Id.ToString(), _translatorService.TranslateToProxy(updatedCase)));
            return _translatorService.TranslateToModel(caseResponse);
        }

        public async Task<BreeceWorks.Shared.CaseObjects.CaseRspses> GetAllCases()
        {
            Dispatcher.Proxies.CaseRspses caseResponse = await _dispatcher.DispatchRequest<Dispatcher.Proxies.CaseRspses, Dispatcher.Proxies.ApiClient>(x => x.CaseGetAsync());
            return _translatorService.TranslateToModel(caseResponse);
        }


        public async Task<BreeceWorks.Shared.CaseObjects.Case> AssignCase(Guid caseId, String? operatorEmail)
        {
            if (operatorEmail != null)
            {
                Dispatcher.Proxies.Case caseResponse = await _dispatcher.DispatchRequest<Dispatcher.Proxies.Case, Dispatcher.Proxies.ActionsClient>(x => x.AssignAsync(caseId.ToString(), new Dispatcher.Proxies.AssignRequest() { PrimaryContact = new Dispatcher.Proxies.OperatorAssignRqst() { Email = operatorEmail } }));
                return _translatorService.TranslateToModel(caseResponse);
            }
            else
            {
                Dispatcher.Proxies.Case caseResponse = await _dispatcher.DispatchRequest<Dispatcher.Proxies.Case, Dispatcher.Proxies.ActionsClient>(x => x.AssignAsync(caseId.ToString(), null));
                return _translatorService.TranslateToModel(caseResponse);

            }
        }

        public async Task<BreeceWorks.Shared.CaseObjects.CaseTranscript> GetCaseTranscript(Guid caseId)
        {
            Dispatcher.Proxies.CaseTranscript caseTranscript = await _dispatcher.DispatchRequest<Dispatcher.Proxies.CaseTranscript, Dispatcher.Proxies.ActionsClient>(x => x.DownloadAsync(caseId.ToString()));
            return _translatorService.TranslateToModel(caseTranscript);
        }

        public async Task<BreeceWorks.Shared.CaseObjects.Case> ReopenCase(Guid caseId)
        {
            Dispatcher.Proxies.Case caseResponse = await _dispatcher.DispatchRequest<Dispatcher.Proxies.Case, Dispatcher.Proxies.ActionsClient>(x => x.ReopenAsync(caseId.ToString()));
            return _translatorService.TranslateToModel(caseResponse);
        }

        public async Task<BreeceWorks.Shared.CaseObjects.Case> CloseCase(Guid caseId)
        {
            Dispatcher.Proxies.Case caseResponse = await _dispatcher.DispatchRequest<Dispatcher.Proxies.Case, Dispatcher.Proxies.ActionsClient>(x => x.CloseAsync(caseId.ToString()));
            return _translatorService.TranslateToModel(caseResponse);
        }

        public async Task<BreeceWorks.Shared.CaseObjects.Case> OpenNewCase(BreeceWorks.Shared.CaseObjects.CaseCreateRqst caseCreateRqst)
        {
            Dispatcher.Proxies.Case caseResponse = await _dispatcher.DispatchRequest<Dispatcher.Proxies.Case, Dispatcher.Proxies.ActionsClient>(x => x.OpenAsync(_translatorService.TranslateToProxy(caseCreateRqst)));
            return _translatorService.TranslateToModel(caseResponse);
        }

        public async Task<BreeceWorks.Shared.CaseObjects.Operator?> GetOperatorByID(Guid curOperatorID)
        {
            Dispatcher.Proxies.Operator operatorResponse = await _dispatcher.DispatchRequest<Dispatcher.Proxies.Operator, Dispatcher.Proxies.ApiClient>(x => x.OperatorsGetAsync(curOperatorID));
            return _translatorService.TranslateToModel(operatorResponse);
        }

        public async Task<BreeceWorks.Shared.CaseObjects.Operator?> GetOperatorByEmail(String curOperatorEmail)
        {
            Dispatcher.Proxies.Operator operatorResponse = await _dispatcher.DispatchRequest<Dispatcher.Proxies.Operator, Dispatcher.Proxies.ApiClient>(x => x.OperatorsGetAsync(curOperatorEmail));
            return _translatorService.TranslateToModel(operatorResponse);
        }

        public async Task<BreeceWorks.Shared.CaseObjects.Operator> UpdateOperator(BreeceWorks.Shared.CaseObjects.Operator curOperator)
        {
            Dispatcher.Proxies.OperatorUpdateRqst operatorUpdateRqst = new Dispatcher.Proxies.OperatorUpdateRqst() 
            {
                DfficeNumber = curOperator.OfficeNumber,
                Email = curOperator.Email,
                FirstName = curOperator.FirstName,
                LastName = curOperator.LastName,
                Mobile = curOperator.PhoneNumber,
                Roles = curOperator.Roles,
                Title = curOperator.Title
            };
            Dispatcher.Proxies.Operator operatorResponse = await _dispatcher.DispatchRequest<Dispatcher.Proxies.Operator, Dispatcher.Proxies.ApiClient>(x => x.OperatorsPatchAsync(curOperator.Id.Value, operatorUpdateRqst));
            return _translatorService.TranslateToModel(operatorResponse);
        }

        public async Task<String?> UploadAttachment(MultipartFormDataContent content)
        {
            String NewFileURL = String.Empty;
            
            try
            {
                Dispatcher.Proxies.SMSAttachment smsAttachment = await _dispatcher.DispatchRequest<Dispatcher.Proxies.SMSAttachment, Dispatcher.Proxies.SMSAttachmentClient>(x => x.AttachmentUploadAsync(content));
                NewFileURL = smsAttachment.Id.ToString();
            }
            catch (Exception ex)
            {

            }
            return NewFileURL;
        }

        public async Task<BreeceWorks.Shared.SMS.SMSIncomingeMessage> SendMessage(BreeceWorks.Shared.SMS.SMSOutgoingCommunication sMSMessage)
        {
            Dispatcher.Proxies.SMSIncomingeMessage messageResponse = await _dispatcher.DispatchRequest<Dispatcher.Proxies.SMSIncomingeMessage, Dispatcher.Proxies.ActionsClient>(x => x.SendMessageAsync(_translatorService.TranslateToProxy(sMSMessage)));
            return _translatorService.TranslateToModel(messageResponse);

        }
    }
}
