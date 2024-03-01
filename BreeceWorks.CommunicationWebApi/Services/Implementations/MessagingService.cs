using BreeceWorks.CommunicationWebApi.Adapters.Interfaces;
using BreeceWorks.CommunicationWebApi.Controllers;
using BreeceWorks.CommunicationWebApi.Objects;
using BreeceWorks.CommunicationWebApi.RequestObjects;
using BreeceWorks.CommunicationWebApi.ResponseObjects;
using BreeceWorks.CommunicationWebApi.Services.Interfaces;
using BreeceWorks.Shared;
using BreeceWorks.Shared.DbContexts;
using BreeceWorks.Shared.Entities;
using BreeceWorks.Shared.Enums;
using BreeceWorks.Shared.Services;
using BreeceWorks.Shared.SMS;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace BreeceWorks.CommunicationWebApi.Services.Implementations
{
    public class MessagingService : IMessagingService
    {
        private readonly CommunicationDbContext _context;
        private readonly IOperatorService _operatorService;
        private readonly IConfigureService _configureService;
        private readonly ITranslatorService _translatorService;

        private readonly ISMSAdapter _adapter;


        public MessagingService(CommunicationDbContext context, ISMSAdapter sMSAdapter, 
            IOperatorService operatorService, ITranslatorService translatorService, IConfigureService configureService)
        {
            _context = context;
            _adapter = sMSAdapter;
            _operatorService = operatorService;
            _translatorService = translatorService;
            _configureService = configureService;
        }
        public CompanyPhoneNumber GetNewCompanyNumberForCustomer(Objects.Customer customer)
        {
            throw new NotImplementedException();
        }

        public TemplateMessage SendTemplateMessage(TemplatedMessageRequest templatedSMSRequest, Int32 templateId)
        {
            SMSOutgoingMessage sMSRequest = new SMSOutgoingMessage();
            MessageDto messageDto = new MessageDto() 
            { 
                type = MessageType.text,
                formatting = MessageFormatting.ai,
                channelSource = MessageChannelSource.company,
            };

            TemplateMessage templateMessageResponse = new TemplateMessage()
            {
                _id = messageDto.id,
                isActive = true,
                createdAt = DateTime.UtcNow,
            };


            try
            {
                MessageTemplateDto? messageTemplateDto = _context.MessageTemplates.Include(t => t.TemplateValues).Where(t => t.TemplateId == templateId).FirstOrDefault();
                messageDto.messageTemplate = messageTemplateDto;
                if (messageTemplateDto == null)
                {
                    templateMessageResponse.errors = new Error[] {
                        new Error()
                        {
                            code = Constants.ErrorMessages.TemplateNotFound,
                            category = "NotFound",
                            retryable = false,
                            status = 404,
                            detail = "Failed to retrieve template.",
                            path = "/case/actions/send-template-message/{template}",
                            method = "POST",
                            requestId = Guid.Parse(messageDto.id),
                        }
                    };
                }
                sMSRequest.message = messageTemplateDto.TemplateText;

                foreach (TemplateValueDto templateValue in messageTemplateDto.TemplateValues)
                {
                    sMSRequest.message = sMSRequest.message.Replace("@" + templateValue.Name, templatedSMSRequest.templateValues[templateValue.Name]);
                }
                messageDto.text = sMSRequest.message;
                CaseDto caseDto = _context.Cases.Include(m => m.Messages).Include(cu => cu.Customer).Where(c => c.Id == Guid.Parse(templatedSMSRequest.caseId)).First();
                sMSRequest.fromNumber = caseDto.SMSNumber;
                sMSRequest.toNumber = caseDto.Customer.Mobile;
                sMSRequest.SMSProcessor = _context.CompanyPhoneNumbers.Where(n => n.PhoneNumber == caseDto.SMSNumber).First().SMSProcessor;

                DetermineSource(templatedSMSRequest.source, messageDto, caseDto);

                SMSIncomingeMessage sMSResponseMessage = _adapter.SendSMS(sMSRequest).Result;
                if (sMSResponseMessage != null)
                {
                    templateMessageResponse.chatId = sMSResponseMessage.messageID;
                    messageDto.sMSId = sMSResponseMessage.messageID;

                    if (!String.IsNullOrEmpty(sMSResponseMessage.errorMessage))
                    {
                        messageDto.status = sMSResponseMessage.errorMessage;
                        HandleTemplateMessageErrors(messageDto, templateMessageResponse, sMSResponseMessage);
                    }
                    if (!String.IsNullOrEmpty(sMSResponseMessage.initialStatus))
                    {
                        messageDto.status = sMSResponseMessage.initialStatus;
                    }
                }
                caseDto.Messages.Add(messageDto);
                _context.SaveChanges();

            }
            catch (Exception ex)
            {
                
                    throw;
            }


            return templateMessageResponse;
        }

        private void DetermineSource(Object messageSource, MessageDto messageDto, CaseDto caseDto)
        {
            String? source = String.Empty;
            String sourceEmail = String.Empty;
            if (messageSource.GetType() == typeof(String))
            {
                source = messageSource.ToString();
            }
            else if (messageSource.GetType() == typeof(System.Text.Json.JsonElement))
            {
                if (((System.Text.Json.JsonElement)messageSource).ValueKind == System.Text.Json.JsonValueKind.String)
                {
                    source = messageSource.ToString();
                }
                if (((System.Text.Json.JsonElement)messageSource).ValueKind == System.Text.Json.JsonValueKind.Object)
                {
                    source = RequestObjects.TemplateMessageSource.email.ToString();
                    System.Text.Json.JsonElement responseMessage = (System.Text.Json.JsonElement)messageSource;
                    EmailObject? email = JsonConvert.DeserializeObject<EmailObject>(responseMessage.GetRawText());
                    if (email != null)
                    {
                        sourceEmail = email.email;
                    }
                }
            }
            if (source == RequestObjects.TemplateMessageSource.assigned.ToString())
            {
                if (caseDto.PrimaryContact != null)
                {
                    OperatorDto? assignedContact = _operatorService.GetOperatorDto(caseDto.PrimaryContact);
                    if (assignedContact != null)
                    {
                        messageDto.author = new MessageAuthorDto()
                        {
                            firstName = assignedContact.First,
                            lastName = assignedContact.Last,
                            role = MessageAuthorRole.@operator,
                            id = Guid.NewGuid().ToString()
                        };
                    }
                }


            }
            if (source == RequestObjects.TemplateMessageSource.email.ToString() && !String.IsNullOrEmpty(sourceEmail))
            {
                OperatorDto? assignedContact = _operatorService.GetOperatorDto(sourceEmail);
                if (assignedContact != null)
                {
                    messageDto.author = new MessageAuthorDto()
                    {
                        firstName = assignedContact.First,
                        lastName = assignedContact.Last,
                        role = MessageAuthorRole.@operator,
                        id = Guid.NewGuid().ToString()
                    };
                }
            }
        }

        private static void HandleTemplateMessageErrors(MessageDto messageDto, TemplateMessage templateMessageResponse, SMSIncomingeMessage sMSResponseMessage)
        {
            if (sMSResponseMessage.errorMessage == Constants.ErrorMessages.PhoneIsLandLine)
            {
                templateMessageResponse.errors = new Error[] {
                            new Error()
                            {
                                code = Constants.ErrorMessages.PhoneIsLandLine,
                                category = "DataIntegrityError",
                                retryable = false,
                                status = 400,
                                detail = "The customer.mobile was a landline.",
                                path = "/case/actions/send-template-message/{template}",
                                method = "POST",
                                requestId = Guid.Parse(messageDto.id),
                            }
                        };
            }
            else if (sMSResponseMessage.errorMessage == Constants.ErrorMessages.PhoneIsVoip)
            {
                templateMessageResponse.errors = new Error[] {
                            new Error()
                            {
                                code = Constants.ErrorMessages.PhoneIsVoip,
                                category = "DataIntegrityError",
                                retryable = false,
                                status = 400,
                                detail = "The customer.mobile was a VOIP.",
                                path = "/case/actions/send-template-message/{template}",
                                method = "POST",
                                requestId = Guid.Parse(messageDto.id),
                            }
                        };
            }
            else if (!String.IsNullOrEmpty(sMSResponseMessage.errorMessage))
            {
                templateMessageResponse.errors = new Error[] {
                            new Error()
                            {
                                code = sMSResponseMessage.errorMessage,
                                category = "DataIntegrityError",
                                retryable = false,
                                status = 500,
                                detail = sMSResponseMessage.errorMessage,
                                path = "/case/actions/send-template-message/{template}",
                                method = "POST",
                                requestId = Guid.Parse(messageDto.id),
                            }
                        };
            }
        }

        public void UpdateMessageStatus(SMSMessageStatus sMSMessageStatus)
        {
            MessageDto? messageDto = _context.Messages.Where(m=>m.sMSId == sMSMessageStatus.messageID).FirstOrDefault();
            
            if (messageDto != null)
            {

                // sometimes the "SENT" status update is received after the "DELIVERED" status update
                _context.Entry<MessageDto>(messageDto).Reload();
                if (messageDto.status != Constants.MessageStatus.DELIVERED)
                {
                    messageDto.status = sMSMessageStatus.status.ToUpper();
                    _context.SaveChanges();
                }
            }
        }

        public void SaveIncomingMessage(SMSIncomingeMessage sMSMessage)
        {
            CaseDto? caseDto = _context.Cases
                .Include(m => m.Messages)
                .Include(cu => cu.Customer)
                .Where(c => c.SMSNumber == sMSMessage.toNumber && c.Customer.Mobile == sMSMessage.fromNumber && c.Archived == false && c.State == CaseState.open).FirstOrDefault();
            if (caseDto == null)
            {   // this is to preserve messages sent by a customer for an old case
                caseDto = _context.Cases
                .Include(m => m.Messages)
                .Include(cu => cu.Customer)
                .Where(c => c.SMSNumber == sMSMessage.toNumber && c.Customer.Mobile == sMSMessage.fromNumber).FirstOrDefault();
            }
            if (caseDto != null)
            {
                MessageAuthorDto? messageAuthorDto = _context.MessageAuthors.Where(ma=>ma.id == caseDto.Customer.Id.ToString()).FirstOrDefault();
                if (messageAuthorDto == null)
                {
                    _context.MessageAuthors.Add(new MessageAuthorDto() { firstName = caseDto.Customer.First, lastName = caseDto.Customer.Last, id = caseDto.Customer.Id.ToString(), role = MessageAuthorRole.enduser });
                    _context.SaveChanges();
                    messageAuthorDto = _context.MessageAuthors.Where(ma => ma.id == caseDto.Customer.Id.ToString()).FirstOrDefault();
                }
                MessageDto? existingMessage = caseDto.Messages.Where(m=>m.sMSId == sMSMessage.messageID).FirstOrDefault();
                if (existingMessage == null) //this is to handle if the SMS service posts the same message to the webhook multiple times
                {
                    MessageDto messageDto = new MessageDto()
                    {
                        author = messageAuthorDto,
                        createdAt = DateTime.Now,
                        channelSource = MessageChannelSource.mobile,
                        formatting = MessageFormatting.standard,
                        id = Guid.NewGuid().ToString(),
                        sMSId = sMSMessage.messageID,
                        status = Constants.MessageStatus.DELIVERED,
                        text = sMSMessage.message,
                        type = GetMessageType(sMSMessage.attachmentUrls),
                    };
                    List<MessageAttachmentDto>? messageAttachments = _translatorService.TranslateToDto(sMSMessage.attachmentUrls);
                    if (messageAttachments != null)
                    {
                        foreach (var attachment in messageAttachments)
                        {
                            attachment.data = GetData(attachment.sourceUrl);
                            _context.MessageAttachments.Add(attachment);
                            messageDto.messageAttachments.Add(attachment);
                        }
                    }

                    caseDto.Messages.Add(messageDto);
                    _context.SaveChanges();

                    HandleOptInOut(caseDto, sMSMessage);
                }
            }
            else{
                //TODO: for now, ignores texts not mapped to a campaign as they are probably randomn, spam or otherwise invalid
            }
        }

        private MessageType GetMessageType(List<SMSAttachment>? attachmentUrls)
        {
            if (attachmentUrls == null || attachmentUrls.Count == 0)
            {
                return MessageType.text;
            }
            else if(Constants.imageExtenstions.Contains(attachmentUrls.First().extension))
            {
                return MessageType.image;
            }
            else
            {
                return MessageType.file;
            }
        }

        private void HandleOptInOut(CaseDto caseDto, SMSIncomingeMessage sMSMessage)
        {
            CustomerDto customer = caseDto.Customer;

            String? keyWord = Constants.optKeyWords.Where(k=>k == sMSMessage.message.Trim().ToUpper()).FirstOrDefault();

            if (!String.IsNullOrEmpty(keyWord))
            {
                String messageTemplateName = String.Empty;

                switch (customer.OptStatusDetail)
                {
                    case Constants.OptStatus.REQUESTED:
                        if (keyWord == Constants.OptKeyWords.YES || keyWord == Constants.OptKeyWords.ACCEPT)
                        {
                            messageTemplateName = CustomerOptIn(customer);
                            customer.OptStatus = true;
                            customer.OptStatusDetail = Constants.OptStatus.OPTED_IN;
                            _context.SaveChanges();
                        }
                        if (keyWord == Constants.OptKeyWords.STOP)
                        {
                            messageTemplateName = CustomerOptOut(customer);
                            customer.OptStatus = false;
                            customer.OptStatusDetail = Constants.OptStatus.OPTED_OUT;
                            _context.SaveChanges();
                        }
                        break;
                    case Constants.OptStatus.OPTED_IN:
                        if (keyWord == Constants.OptKeyWords.STOP)
                        {
                            messageTemplateName = CustomerOptOut(customer);
                            customer.OptStatus = false;
                            customer.OptStatusDetail = Constants.OptStatus.OPTED_OUT;
                            _context.SaveChanges();
                        }
                        break;
                    case Constants.OptStatus.OPTED_OUT:
                        if (keyWord == Constants.OptKeyWords.YES || keyWord == Constants.OptKeyWords.ACCEPT || keyWord == Constants.OptKeyWords.START)
                        {
                            messageTemplateName = CustomerOptIn(customer);
                            customer.OptStatus = true;
                            customer.OptStatusDetail = Constants.OptStatus.OPTED_IN;
                            _context.SaveChanges();
                        }
                        break;
                    default:
                        break;
                }


                MessageTemplateDto templatedMessageDto;
                try
                {
                    if (!String.IsNullOrEmpty(messageTemplateName))
                    {
                        templatedMessageDto = _context.MessageTemplates.Include(t => t.TemplateValues).Where(t => t.Name == messageTemplateName).First();
                        TemplatedMessageRequest templatedMessageRequest = new TemplatedMessageRequest()
                        {
                            caseId = caseDto.Id.ToString(),
                            referenceID = caseDto.ReferenceId,
                            templateValues = new Dictionary<string, string>(),
                            source = RequestObjects.TemplateMessageSource.ai.ToString()
                        };

                        SendTemplateMessage(templatedMessageRequest, templatedMessageDto.TemplateId);
                    }
                }
                catch (Exception ex)
                {

                }
            }
        }

        private string CustomerOptOut(CustomerDto customer)
        {
            string messageTemplateName = Constants.MessageTemplates.OPTED_OUT_RESPONSE;
            customer.OptStatus = false;
            customer.OptStatusDetail = Constants.OptStatus.OPTED_OUT;
            _context.SaveChanges();
            return messageTemplateName;
        }

        private string CustomerOptIn(CustomerDto customer)
        {
            string messageTemplateName = Constants.MessageTemplates.OPTED_IN_RESPONSE;
            customer.OptStatus = true;
            customer.OptStatusDetail = Constants.OptStatus.OPTED_IN;
            _context.SaveChanges();
            return messageTemplateName;
        }

        private byte[] GetData(string url)
        {
            Byte[] messageBytes;

            using (var client = new HttpClient())
            {
                var response = client.GetAsync(url).Result;
                var httpStream = response.Content.ReadAsStream();

                using (BinaryReader br = new BinaryReader(httpStream))
                {
                    messageBytes = br.ReadBytes((Int32)httpStream.Length);
                }
            }
            return messageBytes;
        }

        public SMSIncomingeMessage SendMessage(SMSOutgoingCommunication sMSMessage)
        {
            SMSOutgoingMessage sMSRequest = new SMSOutgoingMessage() { message = sMSMessage.message };

            MessageDto messageDto = new MessageDto()
            {
                type = GetMessageType(sMSRequest.attachmentUrls),
                formatting = MessageFormatting.standard,
                channelSource = MessageChannelSource.company,
                id = Guid.NewGuid().ToString(),
            };

            foreach (String attachmentID in sMSMessage.attachmentIDs)
            {
                MessageAttachmentDto? attachment = _context.MessageAttachments.Where(ma => ma.id.ToString() == attachmentID).FirstOrDefault();
                if (attachment != null)
                {
                    if (messageDto.messageAttachments == null)
                    {
                        messageDto.messageAttachments = new List<MessageAttachmentDto>();
                    }
                    messageDto.messageAttachments.Add(attachment);
                    sMSRequest.attachmentUrls.Add(new SMSAttachment() { extension = attachment.extension, name = attachment.name, url = GetAttachmentURL(attachment.id) });
                }
            }

            SMSIncomingeMessage sMSResponseMessage = new SMSIncomingeMessage();


            try
            {
                messageDto.text = sMSRequest.message;
                CaseDto caseDto = _context.Cases.Include(m => m.Messages).Include(cu => cu.Customer).Where(c => c.Id == Guid.Parse(sMSMessage.caseId)).First();
                sMSRequest.fromNumber = caseDto.SMSNumber;
                sMSRequest.toNumber = caseDto.Customer.Mobile;
                sMSRequest.SMSProcessor = _context.CompanyPhoneNumbers.Where(n => n.PhoneNumber == caseDto.SMSNumber).First().SMSProcessor;

                DetermineSource(sMSMessage.source, messageDto, caseDto);

                sMSResponseMessage = _adapter.SendSMS(sMSRequest).Result;
                if (sMSResponseMessage != null)
                {
                    messageDto.sMSId = sMSResponseMessage.messageID;

                    if (!String.IsNullOrEmpty(sMSResponseMessage.errorMessage))
                    {
                        messageDto.status = sMSResponseMessage.errorMessage;
                    }
                    if (!String.IsNullOrEmpty(sMSResponseMessage.initialStatus))
                    {
                        messageDto.status = sMSResponseMessage.initialStatus;
                    }
                }
                caseDto.Messages.Add(messageDto);
                _context.SaveChanges();

            }
            catch (Exception ex)
            {

                throw;
            }


            return sMSResponseMessage;
        }

        private string GetAttachmentURL(Guid id)
        {
            return _configureService.GetValue("BreeceWorks.SMSCoreWebApi") + String.Format(Constants.URLTemplates.AttachmentDownloadURL, id.ToString());
        }
    }
}
