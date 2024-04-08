using BreeceWorks.CommunicationWebApi.RequestObjects;
using BreeceWorks.CommunicationWebApi.Services.Interfaces;
using BreeceWorks.Shared;
using BreeceWorks.Shared.Entities;
using BreeceWorks.Shared.Enums;
using BreeceWorks.Shared.SMS;

namespace BreeceWorks.CommunicationWebApi.Services.Implementations
{
    public class TranslatorService : ITranslatorService
    {

        #region TranslateToObject
        public Objects.Operator? TranslateToObject(OperatorCreateRqst? operatorCreateRqst)
        {
            if (operatorCreateRqst == null) return null;
            return new Objects.Operator()
            {
                First = operatorCreateRqst.firstName,
                Last = operatorCreateRqst.lastName,
                Email = operatorCreateRqst.email,
                Password = operatorCreateRqst.password,
                PhoneNumber = operatorCreateRqst.mobile,
                OfficeNumber = operatorCreateRqst.dfficeNumber,
                Roles = operatorCreateRqst.roles != null ? operatorCreateRqst.roles : null,
                Title = operatorCreateRqst.title,
            };
        }

        public Objects.Operator TranslateToObject(OperatorUpdateRqst operatorUpdateRqst)
        {
            return new Objects.Operator()
            {
                First = operatorUpdateRqst.firstName,
                Last = operatorUpdateRqst.lastName,
                Email = operatorUpdateRqst.email,
                PhoneNumber = operatorUpdateRqst.mobile,
                OfficeNumber = operatorUpdateRqst.dfficeNumber,
                Roles = operatorUpdateRqst.roles != null ? operatorUpdateRqst.roles : null,
                Title = operatorUpdateRqst.title,
            };
        }

        public Objects.Operator? TranslateToObject(OperatorDto? operatorDto)
        {
            if (operatorDto == null) return null;
            return new Objects.Operator()
            {
                Email = operatorDto.Email,
                First = operatorDto.First,
                Last = operatorDto.Last,
                Id = operatorDto.Id,
                Password = operatorDto.Password,
                Roles = TranslateToObject(operatorDto.Roles),
                Title = operatorDto.Title,
                PhoneNumber = operatorDto.PhoneNumber,
                OfficeNumber = operatorDto.PhoneNumber,
                IdentityProvider = operatorDto.IdentityProvider
            };
        }

        public string[]? TranslateToObject(List<OperatorRoleDto>? operatorRoles)
        {
            string[]? result = null;
            if (operatorRoles != null && operatorRoles.Count > 0)
            {
                result = new string[operatorRoles.Count];
                for (int i = 0; i < operatorRoles.Count; i++)
                {
                    result[i] = operatorRoles[i].Role;
                }
            }

            return result;
        }

        public Objects.Case TranslateToObject(CaseCreateRqst caseCreateRqst)
        {
            return new Objects.Case()
            {
                CaseData = TranslateToObject(caseCreateRqst.caseData),
                CaseType = (CaseType)Enum.Parse(typeof(CaseType), caseCreateRqst.caseType),
                BusinessName = caseCreateRqst.businessName,
                CreatedBy = TranslateToObject(caseCreateRqst.createdBy),
                Customer = TranslateToObject(caseCreateRqst.customer),
                LanguagePreference = caseCreateRqst.languagePreference,
                PrimaryContact = TranslateToObject(caseCreateRqst.primaryContact),
                Privacy = caseCreateRqst.privacy,
                SecondaryOperators = TranslateToObject(caseCreateRqst.secondaryOperators),
            };
        }

        public Objects.Case TranslateToObject(CaseUpdateRqst caseUpdateRqst)
        {
            return new Objects.Case()
            {
                CaseData = TranslateToObject(caseUpdateRqst.caseData),
                BusinessName = caseUpdateRqst.businessName,
                CreatedBy = TranslateToObject(caseUpdateRqst.createdBy),
                LanguagePreference = caseUpdateRqst.languagePreference,
                PrimaryContact = TranslateToObject(caseUpdateRqst.primaryContact),
                Privacy = caseUpdateRqst.privacy,
                SecondaryOperators = TranslateToObject(caseUpdateRqst.secondaryOperators),
            };
        }

        public Objects.CaseData TranslateToObject(CaseDataCreateUpdateRqst caseDataCreateRqst)
        {
            return new Objects.CaseData()
            {
                Brand = caseDataCreateRqst.brand,
                ClaimNumber = caseDataCreateRqst.claimNumber,
                DateOfLoss = caseDataCreateRqst.dateOfLoss,
                Deductible = caseDataCreateRqst.deductible,
                LineOfBusiness = TranslateToObject(caseDataCreateRqst.lineOfBusiness),
                PolicyNumber = caseDataCreateRqst.policyNumber,
                Archived = false
            };
        }

        public Objects.LineOfBusiness? TranslateToObject(LineOfBusinessRqst? lineOfBusinessRqst)
        {
            if (lineOfBusinessRqst == null)
                return null;
            return new Objects.LineOfBusiness() { Type = lineOfBusinessRqst.Type, SubType = lineOfBusinessRqst.SubType };
        }

        public Objects.Operator? TranslateToObject(OperatorBaseRqst? caseOperator)
        {
            if (caseOperator == null)
                return null;
            return new Objects.Operator() { Email = caseOperator.email };
        }

        public Objects.Customer TranslateToObject(CustomerCreateUpdateRqst customer)
        {
            return new Objects.Customer()
            {
                Email = customer.email,
                First = customer.first,
                Last = customer.last,
                Mobile = customer.mobile,
            };
        }


        public Objects.Operator[]? TranslateToObject(SecondaryOperatorCreateUpdateRqst[]? secondaryOperators)
        {
            if (secondaryOperators == null)
                return null;
            List<Objects.Operator> operatorList = new List<Objects.Operator>();
            foreach (var secondaryOperator in secondaryOperators)
            {
                Objects.Operator curOperator = new Objects.Operator() { Email = secondaryOperator.email };
                operatorList.Add(curOperator);
            }
            return operatorList.ToArray();
        }

        public Objects.Operator[]? TranslateToObject(List<OperatorDto>? secondaryOperators)
        {
            if (secondaryOperators == null)
                return null;
            List<Objects.Operator> operatorList = new List<Objects.Operator>();
            foreach (var secondaryOperator in secondaryOperators)
            {
                Objects.Operator curOperator = new Objects.Operator() { Email = secondaryOperator.Email };
                operatorList.Add(curOperator);
            }
            return operatorList.ToArray();
        }

        public List<Objects.Message>? TranslateToObject(List<MessageDto>? messages)
        {
            if (messages == null || !messages.Any()) return null;
            List<Objects.Message> messages1 = new List<Objects.Message>();
            foreach (MessageDto message in messages)
            {
                messages1.Add(new Objects.Message()
                {
                    author = TranslateToObject(message.author),
                    createdAt = message.createdAt,
                    messageAttachments = TranslateToObject(message.messageAttachments),
                    needsAction = message.needsAction,
                    needsAttention = message.needsAttention,
                    channelSource = message.channelSource,
                    data = message.text,
                    formatting = message.formatting,
                    id = message.id,
                    sMSId = message.sMSId,
                    status = message.status,
                    type = message.type,
                });
            }
            return messages1;
        }

        public List<Objects.MessageAttachment>? TranslateToObject(List<MessageAttachmentDto>? messageAttachments)
        {
            if (messageAttachments == null) return null;
            List<Objects.MessageAttachment> messageAttachments1 = new List<Objects.MessageAttachment>();
            foreach (MessageAttachmentDto messageAttachment in messageAttachments)
            {
                messageAttachments1.Add(new Objects.MessageAttachment()
                {
                    contentType = HelperMethods.GetMimeTypeByWindowsRegistry(messageAttachment.extension),
                    data = messageAttachment.data,
                    extension = messageAttachment.extension,
                    id = messageAttachment.id,
                    name = messageAttachment.name,
                    sourceUrl = messageAttachment.sourceUrl
                });
            }
            return messageAttachments1;
        }

        public Objects.MessageAuthor? TranslateToObject(MessageAuthorDto? messageAuthor)
        {
            if (messageAuthor == null) return null;
            Objects.MessageAuthor messageAuthor1 = new Objects.MessageAuthor()
            {
                profile = new Objects.MessageAuthorProfile() { firstName = messageAuthor.firstName, lastName = messageAuthor.lastName },
                role = messageAuthor.role,
                id = messageAuthor.id
            };
            return messageAuthor1;
        }

        public Objects.Customer? TranslateToObject(CustomerDto? customerDto)
        {
            if (customerDto == null) return null;
            return new Objects.Customer()
            {
                Email = customerDto.Email,
                First = customerDto.First,
                Last = customerDto.Last,
                Mobile = customerDto.Mobile,
                Id = customerDto.Id,
                OptStatus = customerDto.OptStatus,
                OptStatusDetail = customerDto.OptStatusDetail,
                Role = customerDto.Role
            };
        }

        public Objects.Case TranslateToObject(CaseDto caseDto)
        {
            return new Objects.Case()
            {
                CaseData = TranslateToCaseDataObject(caseDto),
                CaseType = caseDto.CaseType,
                CreateTime = caseDto.CreateTime,
                BusinessName = caseDto.BusinessName,
                LanguagePreference = caseDto.LanguagePreference,
                Customer = TranslateToObject(caseDto.Customer),
                Privacy = caseDto.Privacy,
                ReferenceId = caseDto.ReferenceId,
                SMSNumber = caseDto.SMSNumber,
                State = caseDto.State,
                CreatedBy = caseDto.CreatedBy != null ? new Objects.Operator() { Id = caseDto.CreatedBy.Value } : null,
                PrimaryContact = caseDto.PrimaryContact != null ? new Objects.Operator() { Id = caseDto.PrimaryContact.Value } : null,
                SecondaryOperators = TranslateToObject(caseDto.SecondaryOperators)
            };
        }

        public Objects.CaseTranscript TranslateToObject(CaseTranscriptDto caseDto)
        {
            return new Objects.CaseTranscript()
            {
                CaseData = TranslateToCaseDataObject(caseDto),
                CaseType = caseDto.CaseType,
                CreateTime = caseDto.CreateTime,
                BusinessName = caseDto.BusinessName,
                LanguagePreference = caseDto.LanguagePreference,
                Customer = TranslateToObject(caseDto.Customer),
                Privacy = caseDto.Privacy,
                ReferenceId = caseDto.ReferenceId,
                SMSNumber = caseDto.SMSNumber,
                State = caseDto.State,
                CreatedBy = caseDto.CreatedBy != null ? new Objects.Operator() { Id = caseDto.CreatedBy.Value } : null,
                PrimaryContact = caseDto.PrimaryContact != null ? new Objects.Operator() { Id = caseDto.PrimaryContact.Value } : null,
                SecondaryOperators = TranslateToObject(caseDto.SecondaryOperators),
                Messages = TranslateToObject(caseDto.Messages)
            };
        }



        public Objects.LineOfBusiness? TranslateToObject(LineOfBusinessDto? lineOfBusiness)
        {
            if (lineOfBusiness == null)
                return null;
            return new Objects.LineOfBusiness()
            {
                Id = lineOfBusiness.Id,
                Type = lineOfBusiness.Type,
                SubType = lineOfBusiness.SubType
            };
        }


        #endregion


        #region TranslateToDto
        public OperatorDto? TranslateToDto(Objects.Operator? operatorObject)
        {
            if (operatorObject == null)
                return null;
            return new OperatorDto()
            {
                First = operatorObject.First,
                Last = operatorObject.Last,
                Email = operatorObject.Email,
                Password = operatorObject.Password,
                PhoneNumber = operatorObject.PhoneNumber,
                OfficeNumber = operatorObject.OfficeNumber,
                Roles = TranslateToDto(operatorObject.Roles),
                Title = operatorObject.Title,
                Id = operatorObject.Id
            };
        }

        public List<OperatorRoleDto>? TranslateToDto(string[]? roles)
        {
            List<OperatorRoleDto>? result = null;

            if (roles != null && roles.Any())
            {
                result = new List<OperatorRoleDto>();
                foreach (string role in roles)
                {
                    OperatorRoleDto operatorRoleDto = new OperatorRoleDto() { Role = role };
                    result.Add(operatorRoleDto);
                }
            }

            return result;
        }

        public List<OperatorDto>? TranslateToDto(Objects.Operator[]? secondaryOperators)
        {
            if (secondaryOperators == null)
                return null;
            List<OperatorDto> operatorList = new List<OperatorDto>();
            foreach (var secondaryOperator in secondaryOperators)
            {
                OperatorDto curOperator = new OperatorDto() { Email = secondaryOperator.Email };
                operatorList.Add(curOperator);
            }
            return operatorList;
        }

        public CustomerDto TranslateToDto(Objects.Customer customerObject)
        {
            return new CustomerDto()
            {
                Email = customerObject.Email,
                First = customerObject.First,
                Last = customerObject.Last,
                Mobile = customerObject.Mobile,
                Id = customerObject.Id,
                OptStatus = customerObject.OptStatus,
                OptStatusDetail = customerObject.OptStatusDetail,
                Role = customerObject.Role
            };
        }

        public List<MessageAttachmentDto>? TranslateToDto(List<SMSAttachment>? attachments)
        {
            if (attachments == null || attachments.Count == 0)
            {
                return null;
            }
            var result = new List<MessageAttachmentDto>();

            foreach (var attachment in attachments)
            {
                result.Add(new MessageAttachmentDto()
                {
                    data = attachment.data,
                    extension = attachment.extension,
                    id = Guid.NewGuid(),
                    name = attachment.name,
                    sourceUrl = attachment.url
                });
            }

            return result;
        }

        public LineOfBusinessDto? TranslateToDto(Objects.LineOfBusiness? lineOfBusiness)
        {
            if (lineOfBusiness == null)
                return null;
            return new LineOfBusinessDto()
            {
                Id = lineOfBusiness.Id,
                Type = lineOfBusiness.Type,
                SubType = lineOfBusiness.SubType
            };
        }

        public CaseDto TranslateToDto(Objects.Case caseObject)
        {
            if (caseObject == null) return null;
            return new CaseDto()
            {
                Archived = caseObject.CaseData.Archived,
                Brand = caseObject.CaseData.Brand,
                CaseType = caseObject.CaseType,
                ClaimNumber = caseObject.CaseData.ClaimNumber,
                CreateTime = caseObject.CreateTime,
                DateOfLoss = caseObject.CaseData.DateOfLoss,
                Deductible = caseObject.CaseData.Deductible,
                Id = caseObject.CaseData.Id,
                BusinessName = caseObject.BusinessName,
                LanguagePreference = caseObject.LanguagePreference,
                LineOfBusiness = TranslateToDto(caseObject.CaseData.LineOfBusiness),
                PolicyNumber = caseObject.CaseData.PolicyNumber,
                Customer = TranslateToDto(caseObject.Customer),
                Privacy = caseObject.Privacy,
                ReferenceId = caseObject.ReferenceId,
                SMSNumber = caseObject.SMSNumber,
                State = caseObject.State,
                CreatedBy = caseObject.CreatedBy != null ? caseObject.CreatedBy.Id : null,
                PrimaryContact = caseObject.PrimaryContact != null ? caseObject.PrimaryContact.Id : null,
                SecondaryOperators = TranslateToDto(caseObject.SecondaryOperators)
            };
        }


        #endregion


        #region TranslateToRspse
        public ResponseObjects.Operator? TranslateToRspse(Objects.Operator? operatorObject)
        {
            if (operatorObject == null) return null;
            return new ResponseObjects.Operator()
            {
                email = operatorObject.Email,
                firstName = operatorObject.First,
                id = operatorObject.Id,
                identityProvider = operatorObject.IdentityProvider,
                lastName = operatorObject.Last,
                phoneNumber = operatorObject.PhoneNumber,
                officeNumber = operatorObject.OfficeNumber,
                roles = operatorObject.Roles != null ? operatorObject.Roles.ToArray() : null,
                title = operatorObject.Title
            };
        }

        public List<ResponseObjects.Message>? TranslateToRspse(List<Objects.Message>? messages)
        {
            if (messages == null || !messages.Any()) return null;
            List<ResponseObjects.Message> messages1 = new List<ResponseObjects.Message>();
            foreach (Objects.Message message in messages)
            {
                messages1.Add(new ResponseObjects.Message()
                {
                    author = TranslateToRspse(message.author),
                    createdAt = message.createdAt,
                    messageAttachments = TranslateToRspse(message.messageAttachments),
                    needsAction = message.needsAction,
                    needsAttention = message.needsAttention,
                    channelSource = message.channelSource,
                    data = message.data,
                    formatting = message.formatting,
                    id = message.id,
                    sMSId = message.sMSId,
                    status = message.status,
                    type = message.type,
                });
            }
            return messages1;
        }

        public List<ResponseObjects.MessageAttachment>? TranslateToRspse(List<Objects.MessageAttachment>? messageAttachments)
        {
            if (messageAttachments == null) return null;
            List<ResponseObjects.MessageAttachment> messageAttachmentRspses = new List<ResponseObjects.MessageAttachment>();
            foreach (Objects.MessageAttachment messageAttachment in messageAttachments)
            {
                messageAttachmentRspses.Add(new ResponseObjects.MessageAttachment()
                {
                    contentType = HelperMethods.GetMimeTypeByWindowsRegistry(messageAttachment.extension),
                    data = messageAttachment.data,
                    extension = messageAttachment.extension,
                    id = messageAttachment.id,
                    name = messageAttachment.name,
                    sourceUrl = messageAttachment.sourceUrl
                });
            }
            return messageAttachmentRspses;
        }

        public ResponseObjects.MessageAuthor? TranslateToRspse(Objects.MessageAuthor? messageAuthor)
        {
            if (messageAuthor == null) return null;
            ResponseObjects.MessageAuthor messageAuthor1 = new ResponseObjects.MessageAuthor()
            {
                profile = new ResponseObjects.MessageAuthorProfile() { firstName = messageAuthor.profile.firstName, lastName = messageAuthor.profile.lastName },
                role = messageAuthor.role,
                id = messageAuthor.id
            };
            return messageAuthor1;
        }

        public ResponseObjects.Case TranslateToRspse(Objects.Case caseObject)
        {
            return new ResponseObjects.Case()
            {
                businessName = caseObject.BusinessName,
                caseType = caseObject.CaseType.ToString(),
                languagePreference = caseObject.LanguagePreference.ToString(),
                privacy = caseObject.Privacy.ToString(),
                caseData = TranslateToRspse(caseObject.CaseData),
                customer = TranslateToRspse(caseObject.Customer),
                primaryContact = TranslateToRspse(caseObject?.PrimaryContact),
                createdBy = TranslateToRspse(caseObject?.CreatedBy),
                secondaryOperators = TranslateToRspse(caseObject.SecondaryOperators),
                state = caseObject.State.ToString(),
            };
        }

        private ResponseObjects.CaseData TranslateToRspse(Objects.CaseData caseData)
        {
            return new ResponseObjects.CaseData()
            {
                archived = caseData.Archived,
                brand = caseData.Brand,
                claimNumber = caseData.ClaimNumber,
                dateOfLoss = caseData.DateOfLoss,
                deductible = caseData.Deductible,
                Id = caseData.Id,
                lineOfBusiness = TranslateToRspse(caseData.LineOfBusiness),
                policyNumber = caseData.PolicyNumber
            };
        }

        public ResponseObjects.Customer? TranslateToRspse(Objects.Customer? customer)
        {
            if (customer == null) return null;
            return new ResponseObjects.Customer()
            {
                email = customer.Email,
                first = customer.First,
                last = customer.Last,
                id = customer.Id,
                mobile = customer.Mobile,
                optStatus = customer.OptStatus,
                optStatusDetail = customer.OptStatusDetail,
                role = customer.Role.ToString()
            };
        }

        private ResponseObjects.Operator[]? TranslateToRspse(Objects.Operator[]? secondaryOperators)
        {
            if (secondaryOperators == null || secondaryOperators.Length == 0) return null;

            List<ResponseObjects.Operator> result = new List<ResponseObjects.Operator>();
            foreach (Objects.Operator operatorObject in secondaryOperators)
            {
                result.Add(new ResponseObjects.Operator()
                {
                    email = operatorObject.Email,
                    firstName = operatorObject.First,
                    id = operatorObject.Id,
                    identityProvider = operatorObject.IdentityProvider,
                    lastName = operatorObject.Last,
                    phoneNumber = operatorObject.PhoneNumber,
                    officeNumber = operatorObject.OfficeNumber,
                    roles = operatorObject.Roles,
                    title = operatorObject.Title
                });
            }
            return result.ToArray();
        }

        ResponseObjects.LineOfBusiness? TranslateToRspse(Objects.LineOfBusiness? lineOfBusiness)
        {
            if (lineOfBusiness == null) return null;

            return new ResponseObjects.LineOfBusiness()
            {
                SubType = lineOfBusiness.SubType,
                Type = lineOfBusiness.Type
            };
        }

        public ResponseObjects.CaseTranscript TranslateToRspse(Objects.CaseTranscript caseObject)
        {
            return new ResponseObjects.CaseTranscript()
            {
                businessName = caseObject.BusinessName,
                caseType = caseObject.CaseType.ToString(),
                languagePreference = caseObject.LanguagePreference.ToString(),
                privacy = caseObject.Privacy.ToString(),
                caseData = TranslateToRspse(caseObject.CaseData),
                customer = TranslateToRspse(caseObject.Customer),
                primaryContact = TranslateToRspse(caseObject?.PrimaryContact),
                createdBy = TranslateToRspse(caseObject?.CreatedBy),
                secondaryOperators = TranslateToRspse(caseObject.SecondaryOperators),
                state = caseObject.State.ToString(),
                Messages = TranslateToRspse(caseObject.Messages)
            };
        }

        public Shared.CaseObjects.CaseMessage TranslateToModel(MessageDto messageDto)
        {
            if (messageDto == null)
            {
                return null;
            }
            else
            {
                Shared.CaseObjects.CaseMessage caseMessage = new Shared.CaseObjects.CaseMessage()
                {
                    CreatedAt = messageDto.createdAt,
                    NeedsAction = messageDto.needsAction,
                    Id = messageDto.id,
                    Data = messageDto.text,
                    SMSId = messageDto.sMSId,
                    Author = TranslateToModel(messageDto.author),
                    MessageAttachments = TranslateToModel(messageDto.messageAttachments),
                    NeedsAttention = messageDto.needsAttention,
                    ChannelSource = messageDto.channelSource,
                    Formatting = messageDto.formatting,
                    Status = messageDto.status,
                    Type = messageDto.type,
                };
                return caseMessage;
            }
        }

        public Shared.CaseObjects.MessageAuthor? TranslateToModel(MessageAuthorDto? messageAuthorDto)
        {
            if (messageAuthorDto == null)
            { return null; }
            else
            {
                Shared.CaseObjects.MessageAuthor messageAuthor = new Shared.CaseObjects.MessageAuthor()
                {
                    Id = messageAuthorDto.id,
                    Profile = GetProfileFromAuthorDto(messageAuthorDto),
                    Role = messageAuthorDto.role,
                };
                return messageAuthor;
            }
        }

        private Shared.CaseObjects.MessageAuthorProfile? GetProfileFromAuthorDto(MessageAuthorDto messageAuthorDto)
        {
            if (messageAuthorDto == null)
            {
                return null;
            }
            else
            {
                Shared.CaseObjects.MessageAuthorProfile messageAuthorProfile = new Shared.CaseObjects.MessageAuthorProfile()
                {
                    FirstName = messageAuthorDto.firstName,
                    LastName = messageAuthorDto.lastName,
                };
                return messageAuthorProfile;
            }
        }

        public List<Shared.CaseObjects.MessageAttachment>? TranslateToModel(List<MessageAttachmentDto>? messageAttachments)
        {
            if (messageAttachments == null)
            {
                return null;
            }
            else
            {
                List<Shared.CaseObjects.MessageAttachment> messageAttachments1 = new List<Shared.CaseObjects.MessageAttachment>();
                foreach(MessageAttachmentDto messageAttachmentDto in messageAttachments)
                {
                    messageAttachments1.Add(TranslateToModel(messageAttachmentDto));
                }
                return messageAttachments1;
            }
        }

        public Shared.CaseObjects.MessageAttachment TranslateToModel(MessageAttachmentDto messageAttachmentDto)
        {
            return new Shared.CaseObjects.MessageAttachment() 
            {
                Id = messageAttachmentDto.id,
                ContentType = HelperMethods.GetMimeTypeByWindowsRegistry(messageAttachmentDto.extension),
                Data = messageAttachmentDto.data,
                Extension = messageAttachmentDto.extension,
                Name = messageAttachmentDto.name,
                SourceUrl = messageAttachmentDto.sourceUrl
            };
        }
        #endregion

        #region privare
        private Objects.CaseData TranslateToCaseDataObject(CaseDto caseDto)
        {
            return new Objects.CaseData()
            {
                Id = caseDto.Id,
                Brand = caseDto.Brand,
                ClaimNumber = caseDto.ClaimNumber,
                DateOfLoss = caseDto.DateOfLoss,
                Deductible = caseDto.Deductible,
                LineOfBusiness = TranslateToObject(caseDto.LineOfBusiness),
                PolicyNumber = caseDto.PolicyNumber,
                Archived = caseDto.Archived
            };
        }


        #endregion


    }
}
