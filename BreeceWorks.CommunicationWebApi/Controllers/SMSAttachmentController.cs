using BreeceWorks.CommunicationWebApi.Services.Interfaces;
using BreeceWorks.Shared.SMS;
using Microsoft.AspNetCore.Mvc;

namespace BreeceWorks.CommunicationWebApi.Controllers
{
    [Tags("Attachment Actions")]
    [Route("api/[controller]")]
    [ApiController]
    public class SMSAttachmentController : ControllerBase
    {
        private readonly IMediaService _mediaService;

        public SMSAttachmentController(IMediaService mediaService)
        {
            _mediaService = mediaService;
        }

        [HttpPost]
        [Route("attachment-upload")]
        public ActionResult<SMSAttachment> Upload([FromForm] IFormFile file)
        {
            Byte[] messageBytes = null;
            String name = GetName(file.FileName);
            String extention = GetExtension(file.FileName);

            using (BinaryReader br = new BinaryReader(file.OpenReadStream()))
            {
                messageBytes = br.ReadBytes((Int32)file.Length);
            }
            Guid attachmentId = _mediaService.SaveMediaAttachment(new MediaAttachment() { data = messageBytes, extension = extention, name = name });
            return new SMSAttachment() { id = attachmentId };
        }
        private string GetExtension(string fileName)
        {
            String fileExtension = String.Empty;
            try
            {
                fileExtension = "." + fileName.Split('.')[1];
            }
            catch (Exception ex)
            {
            }

            return fileExtension;
        }

        private string GetName(string fileName)
        {
            String name = String.Empty;
            try
            {
                name = fileName.Split('.')[0];
            }
            catch (Exception ex)
            {
            }

            return name;
        }

    }
}
