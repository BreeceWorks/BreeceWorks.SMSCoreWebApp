
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
        BreeceWorks.Shared.CaseObjects.Operator? TranslateToModel(Dispatcher.Proxies.Operator? operator1);
        BreeceWorks.Shared.CaseObjects.Case TranslateToModel(Dispatcher.Proxies.Case curCase);
        BreeceWorks.Shared.CaseObjects.CaseRspses TranslateToModel(Dispatcher.Proxies.CaseRspses caseRspses);
        BreeceWorks.Shared.CaseObjects.ActiveCases TranslateToModel(Dispatcher.Proxies.ActiveCases activeCases);
        BreeceWorks.Shared.CaseObjects.CaseTranscript TranslateToModel(Dispatcher.Proxies.CaseTranscript caseTranscript);
        List<BreeceWorks.Shared.CaseObjects.Message> TranslateToModel(List<Dispatcher.Proxies.Message> messages);
        BreeceWorks.Shared.CaseObjects.Message TranslateToModel(Dispatcher.Proxies.Message message);
        BreeceWorks.Shared.CaseObjects.MessageAuthor TranslateToModel(Dispatcher.Proxies.MessageAuthor messageAuthor);
        BreeceWorks.Shared.Enums.MessageChannelSource? TranslateToModel(Dispatcher.Proxies.MessageChannelSource? channelSource);
        List<BreeceWorks.Shared.CaseObjects.MessageAttachment> TranslateToModel(List<Dispatcher.Proxies.MessageAttachment> messageAttachments);
        BreeceWorks.Shared.CaseObjects.MessageAttachment TranslateToModel(Dispatcher.Proxies.MessageAttachment messageAttachment);
        BreeceWorks.Shared.Entities.MessageFormatting? TranslateToModel(Dispatcher.Proxies.MessageFormatting? messageFormatting);
        BreeceWorks.Shared.Entities.MessageType? TranslateToModel(Dispatcher.Proxies.MessageType? messageType);
        BreeceWorks.Shared.CaseObjects.MessageAuthorProfile TranslateToModel(Dispatcher.Proxies.MessageAuthorProfile messageAuthorProfile);
        BreeceWorks.Shared.Enums.MessageAuthorRole? TranslateToModel(Dispatcher.Proxies.MessageAuthorRole? messageAuthorRole);
        BreeceWorks.Shared.SMS.SMSIncomingeMessage TranslateToModel(Dispatcher.Proxies.SMSIncomingeMessage sMSIncomingeMessage);
        Dispatcher.Proxies.CaseCreateRqst TranslateToProxy(BreeceWorks.Shared.CaseObjects.CaseCreateRqst caseCreateRqst);
        Dispatcher.Proxies.CaseUpdateRqst TranslateToProxy(BreeceWorks.Shared.CaseObjects.Case updatedCase);
        Dispatcher.Proxies.SMSOutgoingCommunication TranslateToProxy(BreeceWorks.Shared.SMS.SMSOutgoingCommunication sMSOutgoingCommunication);
        Dispatcher.Proxies.Customer TranslateToProxy(BreeceWorks.Shared.CaseObjects.Customer customer);

    }
}
