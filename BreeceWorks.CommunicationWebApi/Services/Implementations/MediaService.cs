using BreeceWorks.CommunicationWebApi.Objects;
using BreeceWorks.CommunicationWebApi.Services.Interfaces;
using BreeceWorks.Shared;
using BreeceWorks.Shared.DbContexts;
using BreeceWorks.Shared.Entities;

namespace BreeceWorks.CommunicationWebApi.Services.Implementations
{
    public class MediaService : IMediaService
    {
        private readonly CommunicationDbContext _context;

        public MediaService(CommunicationDbContext context)
        {
            _context = context;
        }

        public Guid SaveMediaAttachment(MediaAttachment mediaAttachment)
        {
            MessageAttachmentDto messageAttachmentDto = new MessageAttachmentDto()
            {
                data = mediaAttachment.data,
                extension = mediaAttachment.extension,
                id = Guid.NewGuid(),
                name = mediaAttachment.name
            };

            _context.MessageAttachments.Add(messageAttachmentDto);
            _context.SaveChanges();
            return messageAttachmentDto.id;
        }
    }
}
