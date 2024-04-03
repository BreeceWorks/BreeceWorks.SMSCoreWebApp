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

    }
}
