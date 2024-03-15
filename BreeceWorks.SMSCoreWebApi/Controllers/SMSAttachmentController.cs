using BreeceWorks.Shared.Services;
using BreeceWorks.SMSCoreWebApi.Objects;
using BreeceWorks.SMSCoreWebApi.Services.Interface;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BreeceWorks.SMSCoreWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SMSAttachmentController : ControllerBase
    {
        private readonly IMediaService _mediaService;

        public SMSAttachmentController(IMediaService mediaService)
        {
            _mediaService = mediaService;
        }

        [HttpGet]
        [Route("attachment-download/{attachmentID}")]
        public IActionResult AttachmentDownload(String attachmentID)
        {
            MediaAttachment? downloadAttachment = _mediaService.GetMediaAttachment(attachmentID);
            if (downloadAttachment == null)
            {
                return NotFound();
            }
            else
            {
                return File(downloadAttachment.data, downloadAttachment.contentType);
            }
        }

        [HttpPost]
        [Route("attachment-upload")]
        public ActionResult<String> Upload([FromForm] IFormFile file)
        {
            Byte[] messageBytes = null;
            String name = GetName(file.FileName);
            String extention = GetExtension(file.FileName);

            using (BinaryReader br = new BinaryReader(file.OpenReadStream()))
            {
                messageBytes = br.ReadBytes((Int32)file.Length);
            }
            String attachmentId = _mediaService.SaveMediaAttachment(new MediaAttachment() { data = messageBytes, extension = extention, name = name });
            return attachmentId;
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
