using BreeceWorks.CommunicationWebApi.Objects;

namespace BreeceWorks.CommunicationWebApi.Services.Interfaces
{
    public interface IMediaService
    {
        Guid SaveMediaAttachment(MediaAttachment mediaAttachment);

    }
}
