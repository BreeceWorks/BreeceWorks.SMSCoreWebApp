using BreeceWorks.CommunicationWebApi.RequestObjects;
using BreeceWorks.CommunicationWebApi.ResponseObjects;
using BreeceWorks.CommunicationWebApi.Services.Interfaces;
using BreeceWorks.Shared.Services;
using BreeceWorks.Shared.SMS;
using Microsoft.AspNetCore.Mvc;

namespace BreeceWorks.CommunicationWebApi.Controllers
{
    [Tags("Message Actions")]
    [Route("api/case/actions")]
    [ApiController]
    public class MessageController : ControllerBase
    {

        private readonly IMessagingService _messageService;
        private readonly IConfigureService _configureService;


        public MessageController(IMessagingService messageService, IConfigureService configureService)
        {
            _messageService = messageService ??
                throw new ArgumentNullException(nameof(messageService));
            _configureService = configureService ?? throw new ArgumentNullException(nameof(configureService));
        }


        /// <summary>
        /// Send Message to a Case
        /// </summary>
        /// <remarks>Send a message via API to a case with a string text body</remarks>
        /// <response code="200">API Call Successful</response>
        /// <response code="400"></response>
        [HttpPost]
        [Route("send-message")]
        public SMSIncomingeMessage SendMessage(SMSOutgoingCommunication sMSMessage)
        {
            return _messageService.SendMessage(sMSMessage);
        }

        [HttpPost]
        [Route("send-template-message/{templateId}")]
        public TemplateMessage SendTemplateMessage (TemplatedMessageRequest sMSMessage, Int32 templateId)
        {
            return _messageService.SendTemplateMessage(sMSMessage, templateId);
        }


        /// <summary>
        /// Incoming Message webhook
        /// </summary>
        /// <remarks>Webhook for BreeceWorks.SMSCoreWebApi to call when relaying incoming messages</remarks>
        /// <response code="200">API Call Successful</response>
        /// <response code="400"></response>
        [HttpPost]
        [Route("incoming-message-webhook")]
        public void IncomingMessage(SMSIncomingeMessage sMSMessage)
        {
            _messageService.SaveIncomingMessage(sMSMessage);
        }


        /// <summary>
        /// Incoming Message webhook
        /// </summary>
        /// <remarks>Webhook for BreeceWorks.SMSCoreWebApi to call when relaying message status</remarks>
        /// <response code="200">API Call Successful</response>
        /// <response code="400"></response>
        [HttpPost]
        [Route("sms_status_callback")]
        public void sms_status_callback(SMSMessageStatus sMSMessageStatus)
        {
            _messageService.UpdateMessageStatus(sMSMessageStatus);
        }
    }

    public class EmailObject
    {
        public String email { get; set; }
    }
}
