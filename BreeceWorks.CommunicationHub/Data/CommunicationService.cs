using BreeceWorks.CommunicationHub.Data.Contracts;
using BreeceWorks.CommunicationHub.Dispatcher.Contracts;
using BreeceWorks.CommunicationHub.Dispatcher.Proxies;
using BreeceWorks.Shared.CaseObjects;

namespace BreeceWorks.CommunicationHub.Data
{
    public class CommunicationService
    {
        private IDispatcher _dispatcher { get; set; }
        private readonly ITranslatorService _translatorService;

        public CommunicationService(IDispatcher dispatcher, ITranslatorService translatorService)
        {
            _dispatcher = dispatcher;
            _translatorService = translatorService;
        }
        public async Task<BreeceWorks.Shared.CaseObjects.Users> GetAllUsers()
        {
            Dispatcher.Proxies.Users usersResponse = await _dispatcher.DispatchRequest<Dispatcher.Proxies.Users, ApiClient>(x => x.UserAsync());
            return _translatorService.TranslateToModel(usersResponse);
        }
        public async Task<BreeceWorks.Shared.CaseObjects.Operators> GetAllOperators()
        {
            Dispatcher.Proxies.Operators operatorsResponse = await _dispatcher.DispatchRequest<Dispatcher.Proxies.Operators, OperatorsClient>(x => x.AllAsync());
            return _translatorService.TranslateToModel(operatorsResponse);
        }

        public async Task<BreeceWorks.Shared.CaseObjects.Operator> CreateOperator(BreeceWorks.Shared.CaseObjects.Operator newOperator)
        {
            OperatorCreateRqst operatorCreateRqst = new OperatorCreateRqst()
            {
                DfficeNumber = newOperator.OfficeNumber,
                Email = newOperator.Email,
                FirstName = newOperator.FirstName,
                LastName = newOperator.LastName,
                Mobile = newOperator.PhoneNumber,
                Roles = newOperator.Roles,
                Title = newOperator.Title
            };
            Dispatcher.Proxies.Operator operatorResponse = await _dispatcher.DispatchRequest<Dispatcher.Proxies.Operator, ApiClient>(x => x.OperatorsPostAsync(operatorCreateRqst));
            return _translatorService.TranslateToModel(operatorResponse);
        }

        public async Task<BreeceWorks.Shared.CaseObjects.Operator> DeleteOperator(Guid operatorId)
        {
            Dispatcher.Proxies.Operator operatorResponse = await _dispatcher.DispatchRequest<Dispatcher.Proxies.Operator, ApiClient>(x => x.OperatorsDeleteAsync(operatorId));
            return _translatorService.TranslateToModel(operatorResponse);
        }

        public async Task<BreeceWorks.Shared.CaseObjects.ActiveCases> GetCasesForUserByUserID(Guid userID)
        {
            Dispatcher.Proxies.ActiveCases userResponse = await _dispatcher.DispatchRequest<Dispatcher.Proxies.ActiveCases, ActiveCasesClient>(x => x.IdAsync(userID));
            return _translatorService.TranslateToModel(userResponse);
        }

        public async Task<BreeceWorks.Shared.CaseObjects.Case> GetCaseByID(Guid caseId)
        {
            Dispatcher.Proxies.Case caseResponse = await _dispatcher.DispatchRequest<Dispatcher.Proxies.Case, ApiClient>(x => x.CaseGetAsync(caseId.ToString()));
            return _translatorService.TranslateToModel(caseResponse);
        }

        public async Task<BreeceWorks.Shared.CaseObjects.Case> UpdateCase(BreeceWorks.Shared.CaseObjects.Case updatedCase)
        {
            Dispatcher.Proxies.Case caseResponse = await _dispatcher.DispatchRequest<Dispatcher.Proxies.Case, ApiClient>(x => x.CasePutAsync(updatedCase.CaseData.Id.ToString(), _translatorService.TranslateToProxy(updatedCase)));
            return _translatorService.TranslateToModel(caseResponse);
        }

        public async Task<BreeceWorks.Shared.CaseObjects.CaseRspses> GetAllCases()
        {
            Dispatcher.Proxies.CaseRspses caseResponse = await _dispatcher.DispatchRequest<Dispatcher.Proxies.CaseRspses, ApiClient>(x => x.CaseGetAsync());
            return _translatorService.TranslateToModel(caseResponse);
        }


        public async Task<BreeceWorks.Shared.CaseObjects.Case> AssignCase(Guid caseId, String? operatorEmail)
        {
            if (operatorEmail != null)
            {
                Dispatcher.Proxies.Case caseResponse = await _dispatcher.DispatchRequest<Dispatcher.Proxies.Case, ActionsClient>(x => x.AssignAsync(caseId.ToString(), new AssignRequest() { PrimaryContact = new OperatorAssignRqst() { Email = operatorEmail } }));
                return _translatorService.TranslateToModel(caseResponse);
            }
            else
            {
                Dispatcher.Proxies.Case caseResponse = await _dispatcher.DispatchRequest<Dispatcher.Proxies.Case, ActionsClient>(x => x.AssignAsync(caseId.ToString(), null));
                return _translatorService.TranslateToModel(caseResponse);

            }
        }

        public async Task<BreeceWorks.Shared.CaseObjects.Case> ReopenCase(Guid caseId)
        {
            Dispatcher.Proxies.Case caseResponse = await _dispatcher.DispatchRequest<Dispatcher.Proxies.Case, ActionsClient>(x => x.ReopenAsync(caseId.ToString()));
            return _translatorService.TranslateToModel(caseResponse);
        }

        public async Task<BreeceWorks.Shared.CaseObjects.Case> CloseCase(Guid caseId)
        {
            Dispatcher.Proxies.Case caseResponse = await _dispatcher.DispatchRequest<Dispatcher.Proxies.Case, ActionsClient>(x => x.CloseAsync(caseId.ToString()));
            return _translatorService.TranslateToModel(caseResponse);
        }

        public async Task<BreeceWorks.Shared.CaseObjects.Case> OpenNewCase(BreeceWorks.Shared.CaseObjects.CaseCreateRqst caseCreateRqst)
        {
            Dispatcher.Proxies.Case caseResponse = await _dispatcher.DispatchRequest<Dispatcher.Proxies.Case, ActionsClient>(x => x.OpenAsync(_translatorService.TranslateToProxy(caseCreateRqst)));
            return _translatorService.TranslateToModel(caseResponse);
        }

        public async Task<BreeceWorks.Shared.CaseObjects.Operator?> GetOperatorByID(Guid curOperatorID)
        {
            Dispatcher.Proxies.Operator operatorResponse = await _dispatcher.DispatchRequest<Dispatcher.Proxies.Operator, ApiClient>(x => x.OperatorsGetAsync(curOperatorID));
            return _translatorService.TranslateToModel(operatorResponse);
        }

        public async Task<BreeceWorks.Shared.CaseObjects.Operator?> GetOperatorByEmail(String curOperatorEmail)
        {
            Dispatcher.Proxies.Operator operatorResponse = await _dispatcher.DispatchRequest<Dispatcher.Proxies.Operator, ApiClient>(x => x.OperatorsGetAsync(curOperatorEmail));
            return _translatorService.TranslateToModel(operatorResponse);
        }

        public async Task<BreeceWorks.Shared.CaseObjects.Operator> UpdateOperator(BreeceWorks.Shared.CaseObjects.Operator curOperator)
        {
            OperatorUpdateRqst operatorUpdateRqst = new OperatorUpdateRqst() 
            {
                DfficeNumber = curOperator.OfficeNumber,
                Email = curOperator.Email,
                FirstName = curOperator.FirstName,
                LastName = curOperator.LastName,
                Mobile = curOperator.PhoneNumber,
                Roles = curOperator.Roles,
                Title = curOperator.Title
            };
            Dispatcher.Proxies.Operator operatorResponse = await _dispatcher.DispatchRequest<Dispatcher.Proxies.Operator, ApiClient>(x => x.OperatorsPatchAsync(curOperator.Id.Value, operatorUpdateRqst));
            return _translatorService.TranslateToModel(operatorResponse);
        }
    }
}
