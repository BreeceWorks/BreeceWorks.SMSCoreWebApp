
namespace BreeceWorks.CommunicationHub.Data.Contracts
{
    public interface ITranslatorService
    {
        BreeceWorks.Shared.CaseObjects.Users TranslateToModel(Dispatcher.Proxies.Users users);
        List<BreeceWorks.Shared.CaseObjects.Customer> TranslateToModel(List<Dispatcher.Proxies.Customer> customers);
        List<BreeceWorks.Shared.CaseObjects.Error> TranslateToModel(List<Dispatcher.Proxies.Error> customers);
        BreeceWorks.Shared.CaseObjects.ErrorRspseMeta TranslateToModel(Dispatcher.Proxies.ErrorRspseMeta errorRspseMeta);
        BreeceWorks.Shared.CaseObjects.CaseData TranslateToModel(Dispatcher.Proxies.CaseData caseData);
        BreeceWorks.Shared.CaseObjects.LineOfBusiness TranslateToModel(Dispatcher.Proxies.LineOfBusiness lineOfBusiness);
        BreeceWorks.Shared.CaseObjects.Operators TranslateToModel(Dispatcher.Proxies.Operators operators);
        BreeceWorks.Shared.CaseObjects.Operator TranslateToModel(Dispatcher.Proxies.Operator operator1);
        BreeceWorks.Shared.CaseObjects.Case TranslateToModel(Dispatcher.Proxies.Case curCase);
        BreeceWorks.Shared.CaseObjects.CaseRspses TranslateToModel(Dispatcher.Proxies.CaseRspses caseRspses);
        BreeceWorks.Shared.CaseObjects.ActiveCases TranslateToModel(Dispatcher.Proxies.ActiveCases activeCases);
        Dispatcher.Proxies.CaseCreateRqst TranslateToProxy(BreeceWorks.Shared.CaseObjects.CaseCreateRqst caseCreateRqst);
    }
}
