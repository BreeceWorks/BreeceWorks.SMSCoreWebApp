using BreeceWorks.Shared.SMS;

namespace BreeceWorks.CommunicationWebApi.Adapters.Interfaces
{
    public interface ISMSAdapter
    {
        Task<SMSIncomingeMessage> SendSMS(SMSOutgoingMessage message);
    }
}
