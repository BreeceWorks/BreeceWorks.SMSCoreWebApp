using BreeceWorks.CommunicationWebApi.Objects;
using BreeceWorks.CommunicationWebApi.RequestObjects;
using BreeceWorks.CommunicationWebApi.ResponseObjects;
using BreeceWorks.Shared.SMS;

namespace BreeceWorks.CommunicationWebApi.Services.Interfaces
{
    public interface IMessagingService
    {
        CompanyPhoneNumber GetNewCompanyNumberForCustomer(Objects.Customer customer);
        TemplateMessage SendTemplateMessage(TemplatedMessageRequest sMSMessage, Int32 templateId);
        SMSIncomingeMessage SendMessage(SMSOutgoingCommunication sMSMessage);
        void UpdateMessageStatus(SMSMessageStatus sMSMessageStatus);
        void SaveIncomingMessage(SMSIncomingeMessage sMSMessage);
    }
}
