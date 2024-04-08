using BreeceWorks.Shared.SMS;

namespace BreeceWorks.SMSCoreWebApi.Services.Interface
{
    public interface IMediaService
    {
        MediaAttachment? GetMediaAttachment(String attachmentID);
    }
}
