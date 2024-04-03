using BreeceWorks.SMSCoreWebApi.Objects;

namespace BreeceWorks.SMSCoreWebApi.Services.Interface
{
    public interface IMediaService
    {
        MediaAttachment? GetMediaAttachment(String attachmentID);
    }
}
