using BreeceWorks.Shared.SMS;

namespace BreeceWorks.SMSCoreWebApi.IControllers
{
    public interface ISMSController
    {
        Task<SMSIncomingeMessage> Outgoing(SMSOutgoingMessage sMSMessage);
        Task<MobileNumberValidationResponse> VerifyValidMobile(String phoneNumber);
        void CleanUpIncomingMessage(SMSIncomingeMessage sMSMessage);
    }
}
