using BreeceWorks.Shared.SMS;

namespace BreeceWorks.CommunicationWebApi.Services.Interfaces
{
    public interface IMediaService
    {
        Guid SaveMediaAttachment(MediaAttachment mediaAttachment);

    }
}
