﻿using BreeceWorks.Shared;
using BreeceWorks.Shared.DbContexts;
using BreeceWorks.Shared.Entities;
using BreeceWorks.Shared.SMS;
using BreeceWorks.SMSCoreWebApi.Services.Interface;

namespace BreeceWorks.SMSCoreWebApi.Services.Implementation
{
    public class MediaService : IMediaService
    {
        private readonly CommunicationDbContext _context;

        public MediaService(CommunicationDbContext context)
        {
            _context = context;
        }

        public MediaAttachment? GetMediaAttachment(string attachmentID)
        {
            MediaAttachment? messageAttachment = null;
            MessageAttachmentDto? attachment = _context.MessageAttachments.Where(ma => ma.id.ToString() == attachmentID).FirstOrDefault();
            if (attachment != null)
            {
                messageAttachment = new MediaAttachment()
                {
                    data = attachment.data,
                    extension = attachment.extension,
                    id = attachment.id,
                    name = attachment.name,
                    contentType = HelperMethods.GetMimeTypeByWindowsRegistry(attachment.extension)
                };
            }
            return messageAttachment;
        }       

    }
}
