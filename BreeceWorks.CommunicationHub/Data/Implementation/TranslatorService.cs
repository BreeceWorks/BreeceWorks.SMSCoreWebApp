using BreeceWorks.CommunicationHub.Data.Contracts;
using BreeceWorks.CommunicationHub.Pages.Components;

namespace BreeceWorks.CommunicationHub.Data.Implementation
{
    public class TranslatorService : ITranslatorService
    {
        #region TranslateToModel

        public BreeceWorks.Shared.CaseObjects.Users TranslateToModel(Dispatcher.Proxies.Users users)
        {
            return new BreeceWorks.Shared.CaseObjects.Users()
            {
                Customers = TranslateToModel(users.Customers),
                Errors = TranslateToModel(users.Errors)
            };
        }

        public List<BreeceWorks.Shared.CaseObjects.Customer> TranslateToModel(List<Dispatcher.Proxies.Customer> customers)
        {
            if (customers == null)
            {
                return null;
            }
            else
            {
                List<BreeceWorks.Shared.CaseObjects.Customer> customers1 = new List<BreeceWorks.Shared.CaseObjects.Customer>();
                foreach (Dispatcher.Proxies.Customer customer in customers)
                {
                    customers1.Add(TranslateToModel(customer));
                }
                return customers1;
            }
        }

        public List<BreeceWorks.Shared.CaseObjects.Error> TranslateToModel(List<Dispatcher.Proxies.Error> errors)
        {
            if (errors == null)
            {
                return null;
            }
            else
            {
                List<BreeceWorks.Shared.CaseObjects.Error> erros1 = new List<BreeceWorks.Shared.CaseObjects.Error>();
                foreach (Dispatcher.Proxies.Error customer in errors)
                {
                    erros1.Add(new BreeceWorks.Shared.CaseObjects.Error()
                    {
                        Category = customer.Category,
                        Code = customer.Code,
                        Detail = customer.Detail,
                        Meta = TranslateToModel(customer.Meta),
                        Method = customer.Method,
                        Path = customer.Path,
                        RequestId = customer.RequestId,
                        Retryable = customer.Retryable,
                        Status = customer.Status,
                    });
                }
                return erros1;
            }
        }

        public BreeceWorks.Shared.CaseObjects.ErrorRspseMeta TranslateToModel(Dispatcher.Proxies.ErrorRspseMeta errorRspseMeta)
        {
            if (errorRspseMeta == null)
            {
                return null;
            }
            else
            {
                BreeceWorks.Shared.CaseObjects.ErrorRspseMeta errorRspseMeta1 = new BreeceWorks.Shared.CaseObjects.ErrorRspseMeta()
                {
                    ExistingCase = TranslateToModel(errorRspseMeta.ExistingCase)
                };
                return errorRspseMeta1;
            }
        }

        public BreeceWorks.Shared.CaseObjects.CaseData TranslateToModel(Dispatcher.Proxies.CaseData caseData)
        {
            if (caseData == null)
            {
                return null;
            }
            else
            {
                BreeceWorks.Shared.CaseObjects.CaseData caseData1 = new BreeceWorks.Shared.CaseObjects.CaseData()
                {
                    Archived = caseData.Archived,
                    Brand = caseData.Brand,
                    ClaimNumber = caseData.ClaimNumber,
                    DateOfLoss = caseData.DateOfLoss,
                    Deductible = caseData.Deductible,
                    Id = caseData.Id,
                    PolicyNumber = caseData.PolicyNumber,
                    LineOfBusiness = TranslateToModel(caseData.LineOfBusiness)
                };
                return caseData1;
            }
        }

        public BreeceWorks.Shared.CaseObjects.LineOfBusiness TranslateToModel(Dispatcher.Proxies.LineOfBusiness lineOfBusiness)
        {
            if (lineOfBusiness == null)
            {
                return null;
            }
            else
            {
                BreeceWorks.Shared.CaseObjects.LineOfBusiness lineOfBusiness1 = new BreeceWorks.Shared.CaseObjects.LineOfBusiness()
                {
                    SubType = lineOfBusiness.SubType,
                    Type = lineOfBusiness.Type,
                };
                return lineOfBusiness1;
            }
        }

        public BreeceWorks.Shared.CaseObjects.Operators TranslateToModel(Dispatcher.Proxies.Operators operators)
        {
            if (operators == null)
            {
                return null;
            }
            else
            {
                BreeceWorks.Shared.CaseObjects.Operators operators1 = new BreeceWorks.Shared.CaseObjects.Operators()
                {
                    Errors = TranslateToModel(operators.Errors)
                };
                if (operators.Operators1 != null)
                {
                    operators1.Operators1 = new List<BreeceWorks.Shared.CaseObjects.Operator>();
                    foreach (Dispatcher.Proxies.Operator curOpeartor in operators.Operators1)
                    {
                        operators1.Operators1.Add(TranslateToModel(curOpeartor));
                    }
                }
                return operators1;
            }
        }

        public BreeceWorks.Shared.CaseObjects.Operator? TranslateToModel(Dispatcher.Proxies.Operator? operator1)
        {
            if (operator1 == null)
            {
                return null;
            }
            else
            {
                BreeceWorks.Shared.CaseObjects.Operator curOperator = new BreeceWorks.Shared.CaseObjects.Operator()
                {
                    Email = operator1.Email,
                    FirstName = operator1.FirstName,
                    Id = operator1.Id,
                    LastName = operator1.LastName,
                    IdentityProvider = operator1.IdentityProvider,
                    OfficeNumber = operator1.OfficeNumber,
                    PhoneNumber = operator1.PhoneNumber,
                    Roles = operator1.Roles,
                    Title = operator1.Title,
                    Errors = TranslateToModel(operator1.Errors)
                };
                return curOperator;
            }
        }

        public BreeceWorks.Shared.CaseObjects.Case TranslateToModel(Dispatcher.Proxies.Case curCase)
        {
            if (curCase == null)
            {
                return null;
            }
            else
            {
                return new BreeceWorks.Shared.CaseObjects.Case()
                {
                    BusinessName = curCase.BusinessName,
                    CaseData = TranslateToModel(curCase.CaseData),
                    CaseType = curCase.CaseType,
                    CreatedBy = TranslateToModel(curCase.CreatedBy),
                    Customer = TranslateToModel(curCase.Customer),
                    Errors = TranslateToModel(curCase.Errors),
                    LanguagePreference = curCase.LanguagePreference,
                    PrimaryContact = TranslateToModel(curCase.PrimaryContact),
                    Privacy = curCase.Privacy,
                    SecondaryOperators = TranslateToModel(curCase.SecondaryOperators),
                    State = curCase.State,
                };
            }
        }

        public BreeceWorks.Shared.CaseObjects.CaseRspses TranslateToModel(Dispatcher.Proxies.CaseRspses caseRspses)
        {
            if (caseRspses == null)
            {
                return null;
            }
            else
            {
                return new BreeceWorks.Shared.CaseObjects.CaseRspses()
                {
                    Cases = TranslateToModel(caseRspses.Cases),
                    Errors = TranslateToModel(caseRspses.Errors)
                };
            }
        }
        public BreeceWorks.Shared.CaseObjects.ActiveCases TranslateToModel(Dispatcher.Proxies.ActiveCases activeCases)
        {
            if (activeCases == null)
            {
                return null;
            }
            else
            {
                return new BreeceWorks.Shared.CaseObjects.ActiveCases()
                {
                    Cases = TranslateToModel(activeCases.Cases),
                    Errors = TranslateToModel(activeCases.Errors)
                };
            }
        }

        List<BreeceWorks.Shared.CaseObjects.Case> TranslateToModel(List<Dispatcher.Proxies.Case> cases)
        {
            if (cases == null)
            {
                return null;
            }
            else
            {
                List<BreeceWorks.Shared.CaseObjects.Case> cases1 = new List<BreeceWorks.Shared.CaseObjects.Case>();
                foreach (Dispatcher.Proxies.Case curCase in cases)
                {
                    cases1.Add(TranslateToModel(curCase));
                }
                return cases1;
            }
        }

        List<BreeceWorks.Shared.CaseObjects.Operator> TranslateToModel(List<Dispatcher.Proxies.Operator> operators)
        {
            if (operators == null)
            {
                return null;
            }
            else
            {
                List<BreeceWorks.Shared.CaseObjects.Operator> operators1 = new List<BreeceWorks.Shared.CaseObjects.Operator>();
                foreach (Dispatcher.Proxies.Operator curOperator in operators)
                {
                    operators1.Add(TranslateToModel(curOperator));
                }
                return operators1;
            }
        }

        BreeceWorks.Shared.CaseObjects.Customer TranslateToModel(Dispatcher.Proxies.Customer customer)
        {
            if (customer == null)
            {
                return null;
            }
            else
            {
                return new BreeceWorks.Shared.CaseObjects.Customer()
                {
                    Email = customer.Email,
                    First = customer.First,
                    Id = customer.Id,
                    Last = customer.Last,
                    Mobile = customer.Mobile,
                    OptStatus = customer.OptStatus,
                    OptStatusDetail = customer.OptStatusDetail,
                    Role = customer.Role,
                };
            }
        }

        public BreeceWorks.Shared.CaseObjects.CaseTranscript TranslateToModel(Dispatcher.Proxies.CaseTranscript caseTranscript)
        {
            if (caseTranscript == null)
            {
                return null;
            }
            BreeceWorks.Shared.CaseObjects.CaseTranscript caseTranscript1 = new BreeceWorks.Shared.CaseObjects.CaseTranscript()
            {
                BusinessName = caseTranscript.BusinessName,
                CaseData = TranslateToModel(caseTranscript.CaseData),
                CaseType = caseTranscript.CaseType,
                CreatedBy = TranslateToModel(caseTranscript.CreatedBy),
                Customer = TranslateToModel(caseTranscript.Customer),   
                Errors = TranslateToModel(caseTranscript.Errors),
                LanguagePreference = caseTranscript.LanguagePreference,
                Messages = TranslateToModel(caseTranscript.Messages),
                PrimaryContact = TranslateToModel(caseTranscript.PrimaryContact),
                Privacy = caseTranscript.Privacy,
                SecondaryOperators = TranslateToModel(caseTranscript.SecondaryOperators),
                State = caseTranscript.State,
            };
            return caseTranscript1;
        }

        public List<BreeceWorks.Shared.CaseObjects.Message> TranslateToModel(List<Dispatcher.Proxies.Message> messages)
        {
            if (messages == null)
            {
                return null;
            }
            else
            {
                List<BreeceWorks.Shared.CaseObjects.Message> messages1 = new List<BreeceWorks.Shared.CaseObjects.Message>();

                foreach(Dispatcher.Proxies.Message message in messages)
                {
                    messages1.Add(TranslateToModel(message));
                }

                return messages1;
            }
        }

        public BreeceWorks.Shared.CaseObjects.Message TranslateToModel(Dispatcher.Proxies.Message message)
        {
            if (message == null)
            {
                return null;
            }
            else
            {
                BreeceWorks.Shared.CaseObjects.Message message1 = new BreeceWorks.Shared.CaseObjects.Message()
                {
                    CreatedAt = message.CreatedAt,
                    NeedsAction = message.NeedsAction,
                    NeedsAttention = message.NeedsAttention,
                    ChannelSource = TranslateToModel(message.ChannelSource),
                    Id = message.Id,
                    SMSId = message.SMSId,
                    Data = message.Data,
                    Status = message.Status,
                    Author = TranslateToModel(message.Author),
                    MessageAttachments = TranslateToModel(message.MessageAttachments),
                    Formatting = TranslateToModel(message.Formatting),
                    Type = TranslateToModel(message.Type)
                };
                return message1;
            }
        }

        public BreeceWorks.Shared.CaseObjects.MessageAuthor TranslateToModel(Dispatcher.Proxies.MessageAuthor messageAuthor)
        {
            if (messageAuthor == null)
            {
                return null;
            }
            else
            {
                BreeceWorks.Shared.CaseObjects.MessageAuthor messageAuthor1 = new BreeceWorks.Shared.CaseObjects.MessageAuthor()
                {
                    Id = messageAuthor.Id,
                    Profile = TranslateToModel(messageAuthor.Profile),
                    Role = TranslateToModel(messageAuthor.Role),
                };
                return messageAuthor1;
            }
        }

        public BreeceWorks.Shared.CaseObjects.MessageAuthorProfile TranslateToModel(Dispatcher.Proxies.MessageAuthorProfile messageAuthorProfile)
        {
            if (messageAuthorProfile == null)
            {
                return null;
            }
            else
            {
                BreeceWorks.Shared.CaseObjects.MessageAuthorProfile messageAuthorProfile1 = new BreeceWorks.Shared.CaseObjects.MessageAuthorProfile()
                {
                    FirstName = messageAuthorProfile.FirstName,
                    LastName = messageAuthorProfile.LastName,
                };
                return messageAuthorProfile1;
            }
        }

        public BreeceWorks.Shared.Enums.MessageAuthorRole? TranslateToModel(Dispatcher.Proxies.MessageAuthorRole? messageAuthorRole)
        {
            if (messageAuthorRole == null)
            {
                return null;
            }
            else
            {
                return (BreeceWorks.Shared.Enums.MessageAuthorRole?)messageAuthorRole;
            }
        }

        public BreeceWorks.Shared.Enums.MessageChannelSource? TranslateToModel(Dispatcher.Proxies.MessageChannelSource? channelSource)
        {
            if(channelSource == null)
            {
                return null;
            }
            else
            {
                return (BreeceWorks.Shared.Enums.MessageChannelSource?)channelSource;
            }
        }

        public List<BreeceWorks.Shared.CaseObjects.MessageAttachment> TranslateToModel(List<Dispatcher.Proxies.MessageAttachment> messageAttachments)
        {
            if (messageAttachments == null)
            {
                return null;
            }
            else
            {
                List<BreeceWorks.Shared.CaseObjects.MessageAttachment> messageAttachments1 = new List<BreeceWorks.Shared.CaseObjects.MessageAttachment>();

                foreach (Dispatcher.Proxies.MessageAttachment messageAttachment in messageAttachments)
                {
                    messageAttachments1.Add(TranslateToModel(messageAttachment));
                }

                return messageAttachments1;
            }
        }

        public BreeceWorks.Shared.CaseObjects.MessageAttachment TranslateToModel(Dispatcher.Proxies.MessageAttachment messageAttachment)
        {
            if (messageAttachment == null)
            {
                return null;
            }
            else
            {
                BreeceWorks.Shared.CaseObjects.MessageAttachment messageAttachment1 = new BreeceWorks.Shared.CaseObjects.MessageAttachment()
                {
                    ContentType = messageAttachment.ContentType,
                    Data = messageAttachment.Data,
                    Extension = messageAttachment.Extension,
                    Id = messageAttachment.Id,
                    Name = messageAttachment.Name,
                    SourceUrl = messageAttachment.SourceUrl,
                };
                return messageAttachment1;
            }
        }

        public BreeceWorks.Shared.Entities.MessageFormatting? TranslateToModel(Dispatcher.Proxies.MessageFormatting? messageFormatting)
        {
            if (messageFormatting == null)
            { 
                return null; 
            }
            else
            {
                return (BreeceWorks.Shared.Entities.MessageFormatting?)messageFormatting;
            }
        }

        public BreeceWorks.Shared.Entities.MessageType? TranslateToModel(Dispatcher.Proxies.MessageType? messageType)
        {
            if (messageType == null)
            {
                return null;
            }
            else
            {
                return (BreeceWorks.Shared.Entities.MessageType?)messageType;
            }
        }

        public BreeceWorks.Shared.SMS.SMSIncomingeMessage TranslateToModel(Dispatcher.Proxies.SMSIncomingeMessage sMSIncomingeMessage)
        {
            if (sMSIncomingeMessage == null)
            {
                return null;
            }
            else
            {
                BreeceWorks.Shared.SMS.SMSIncomingeMessage sMSIncomingeMessage1 = new BreeceWorks.Shared.SMS.SMSIncomingeMessage()
                {
                    attachmentUrls = TranslateToModel(sMSIncomingeMessage.AttachmentUrls),
                    errorMessage = sMSIncomingeMessage.ErrorMessage,
                    fromNumber = sMSIncomingeMessage.FromNumber,
                    initialStatus = sMSIncomingeMessage.InitialStatus,
                    message = sMSIncomingeMessage.Message,
                    messageID = sMSIncomingeMessage.MessageID,
                    toNumber = sMSIncomingeMessage.ToNumber,
                };
                return sMSIncomingeMessage1;
            }
        }

        List<BreeceWorks.Shared.SMS.SMSAttachment>? TranslateToModel(List<Dispatcher.Proxies.SMSAttachment>? sMSAttachments)
        {
            if (sMSAttachments == null)
            {
                return null;
            }
            else
            {
                List<BreeceWorks.Shared.SMS.SMSAttachment> sMSAttachments1 = new List<BreeceWorks.Shared.SMS.SMSAttachment>();

                foreach (Dispatcher.Proxies.SMSAttachment sMSAttachment in sMSAttachments)
                {
                    sMSAttachments1.Add(new BreeceWorks.Shared.SMS.SMSAttachment()
                    {
                        data = sMSAttachment.Data,
                        extension = sMSAttachment.Extension,
                        name = sMSAttachment.Name,
                        url = sMSAttachment.Url,
                    });
                }

                return sMSAttachments1;
            }
        }


        #endregion


        #region TranslateToProxy

        public Dispatcher.Proxies.CaseCreateRqst TranslateToProxy(BreeceWorks.Shared.CaseObjects.CaseCreateRqst caseCreateRqst)
        {
            if (caseCreateRqst == null)
            {
                return null;
            }
            else
            {
                return new Dispatcher.Proxies.CaseCreateRqst()
                {
                    BusinessName = caseCreateRqst.BusinessName,
                    CaseData = TranslateToProxy(caseCreateRqst.CaseData),
                    CaseType = caseCreateRqst.CaseType,
                    CreatedBy = TranslateToProxy(caseCreateRqst.CreatedBy),
                    Customer = TranslateToProxy(caseCreateRqst.Customer),
                    LanguagePreference = TranslateToProxy(caseCreateRqst.LanguagePreference),
                    PrimaryContact = TranslateToProxy(caseCreateRqst.PrimaryContact),
                    Privacy = TranslateToProxy(caseCreateRqst.Privacy),
                    SecondaryOperators = TranslateToProxy(caseCreateRqst.SecondaryOperators)
                };
            }
        }



        Dispatcher.Proxies.LanguagePreference? TranslateToProxy(BreeceWorks.Shared.Enums.LanguagePreference? languagePreference)
        {
            if (languagePreference == null)
            {
                return null;
            }
            else
            {
                return (Dispatcher.Proxies.LanguagePreference)languagePreference;
            }
        }

        Dispatcher.Proxies.OperatorBaseRqst TranslateToProxy(BreeceWorks.Shared.CaseObjects.OperatorBaseRqst operatorBaseRqst)
        {
            if (operatorBaseRqst == null)
            {
                return null;
            }
            else
            {
                return new Dispatcher.Proxies.OperatorBaseRqst()
                {
                    Email = operatorBaseRqst.Email
                };
            }
        }

        Dispatcher.Proxies.CustomerCreateUpdateRqst TranslateToProxy(BreeceWorks.Shared.CaseObjects.CustomerCreateUpdateRqst customerCreateUpdateRqst)
        {
            if (customerCreateUpdateRqst == null)
            {
                return null;
            }
            else
            {
                return new Dispatcher.Proxies.CustomerCreateUpdateRqst()
                {
                    Email = customerCreateUpdateRqst.Email,
                    First = customerCreateUpdateRqst.First,
                    Last = customerCreateUpdateRqst.Last,
                    Mobile = customerCreateUpdateRqst.Mobile,
                };
            }
        }
        Dispatcher.Proxies.Privacy? TranslateToProxy(BreeceWorks.Shared.Enums.Privacy? privacy)
        {
            if (privacy == null)
            {
                return null;
            }
            else
            {
                return (Dispatcher.Proxies.Privacy)privacy;
            }
        }

        Dispatcher.Proxies.PrimaryContactCaseCreateUpdateRqst TranslateToProxy(BreeceWorks.Shared.CaseObjects.PrimaryContactCaseCreateUpdateRqst primaryContactCaseCreateUpdateRqst)
        {
            if (primaryContactCaseCreateUpdateRqst == null)
            {
                return null;
            }
            else
            {
                return new Dispatcher.Proxies.PrimaryContactCaseCreateUpdateRqst()
                {
                    Email = primaryContactCaseCreateUpdateRqst.Email,
                    First = primaryContactCaseCreateUpdateRqst.First,
                    Last = primaryContactCaseCreateUpdateRqst.Last
                };
            }
        }

        List<Dispatcher.Proxies.SecondaryOperatorCreateUpdateRqst> TranslateToProxy(List<BreeceWorks.Shared.CaseObjects.SecondaryOperatorCreateUpdateRqst> secondaryOperatorCreateUpdateRqsts)
        {
            if (secondaryOperatorCreateUpdateRqsts == null)
            {
                return null;
            }
            else
            {
                List<Dispatcher.Proxies.SecondaryOperatorCreateUpdateRqst> secondaryOperatorCreateUpdateRqsts1 = new List<Dispatcher.Proxies.SecondaryOperatorCreateUpdateRqst>();
                foreach (BreeceWorks.Shared.CaseObjects.SecondaryOperatorCreateUpdateRqst secondary in secondaryOperatorCreateUpdateRqsts)
                {
                    secondaryOperatorCreateUpdateRqsts1.Add(new Dispatcher.Proxies.SecondaryOperatorCreateUpdateRqst()
                    {
                        Email = secondary.Email
                    });
                }

                return secondaryOperatorCreateUpdateRqsts1;
            }
        }

        Dispatcher.Proxies.CaseDataCreateUpdateRqst TranslateToProxy(BreeceWorks.Shared.CaseObjects.CaseDataCreateUpdateRqst caseDataCreateUpdateRqst)
        {
            if (caseDataCreateUpdateRqst == null)
            {
                return null;
            }
            else
            {
                return new Dispatcher.Proxies.CaseDataCreateUpdateRqst()
                {
                    Brand = caseDataCreateUpdateRqst.Brand,
                    ClaimNumber = caseDataCreateUpdateRqst.ClaimNumber,
                    DateOfLoss = caseDataCreateUpdateRqst.DateOfLoss,
                    Deductible = caseDataCreateUpdateRqst.Deductible,
                    LineOfBusiness = TranslateToProxy(caseDataCreateUpdateRqst.LineOfBusiness),
                    PolicyNumber = caseDataCreateUpdateRqst.PolicyNumber
                };
            }
        }

        Dispatcher.Proxies.LineOfBusinessRqst TranslateToProxy(BreeceWorks.Shared.CaseObjects.LineOfBusinessRqst lineOfBusinessRqst)
        {
            if (lineOfBusinessRqst == null)
            {
                return null;
            }
            else
            {
                return new Dispatcher.Proxies.LineOfBusinessRqst()
                {
                    SubType = lineOfBusinessRqst.SubType,
                    Type = lineOfBusinessRqst.Type,
                };
            }
        }

        public Dispatcher.Proxies.CaseUpdateRqst TranslateToProxy(BreeceWorks.Shared.CaseObjects.Case updatedCase)
        {
            if (updatedCase == null)
            {
                return null;
            }
            else
            {
                Dispatcher.Proxies.CaseUpdateRqst caseUpdateRqst = new Dispatcher.Proxies.CaseUpdateRqst()
                {
                    BusinessName = updatedCase.BusinessName,
                    CaseData = TranslateToProxy(updatedCase.CaseData),
                    CreatedBy = TranslateToProxy(updatedCase.CreatedBy),
                    LanguagePreference = TranslateToLanguagePreferenceProxy(updatedCase.LanguagePreference),
                    PrimaryContact = TranslateToPrimaryContactCaseCreateUpdateRqstProxy(updatedCase.PrimaryContact),
                    Privacy = TranslateToPrivacyProxy(updatedCase.Privacy),
                    SecondaryOperators = TranslateToProxy(updatedCase.SecondaryOperators)
                };
                return caseUpdateRqst;
            }
        }

        Dispatcher.Proxies.CaseDataCreateUpdateRqst TranslateToProxy(BreeceWorks.Shared.CaseObjects.CaseData caseData)
        {
            if (caseData == null)
            {
                return null;
            }
            else
            {
                Dispatcher.Proxies.CaseDataCreateUpdateRqst caseDataCreateUpdateRqst = new Dispatcher.Proxies.CaseDataCreateUpdateRqst()
                {
                    Brand = caseData.Brand,
                    ClaimNumber = caseData.ClaimNumber,
                    DateOfLoss = caseData.DateOfLoss,
                    Deductible = caseData.Deductible,
                    LineOfBusiness = TranslateToProxy(caseData.LineOfBusiness),
                    PolicyNumber = caseData.PolicyNumber,
                };
                return caseDataCreateUpdateRqst;
            }
        }

        Dispatcher.Proxies.LineOfBusinessRqst TranslateToProxy(BreeceWorks.Shared.CaseObjects.LineOfBusiness lineOfBusiness)
        {
            if (lineOfBusiness == null)
            {
                return null;
            }
            else
            {
                Dispatcher.Proxies.LineOfBusinessRqst lineOfBusinessRqst = new Dispatcher.Proxies.LineOfBusinessRqst()
                {
                    SubType = lineOfBusiness.SubType,
                    Type = lineOfBusiness.Type,
                };
                return lineOfBusinessRqst;
            }
        }

        Dispatcher.Proxies.OperatorBaseRqst TranslateToProxy(BreeceWorks.Shared.CaseObjects.Operator curOperator)
        {
            if ( curOperator == null)
            {
                return null;
            }
            else
            {
                Dispatcher.Proxies.OperatorBaseRqst operatorBaseRqst = new Dispatcher.Proxies.OperatorBaseRqst()
                {
                    Email = curOperator.Email,
                };
                return operatorBaseRqst;
            }
        }

        Dispatcher.Proxies.PrimaryContactCaseCreateUpdateRqst TranslateToPrimaryContactCaseCreateUpdateRqstProxy(BreeceWorks.Shared.CaseObjects.Operator curOperator)
        {
            if (curOperator == null)
            {
                return null;
            }
            else
            {
                Dispatcher.Proxies.PrimaryContactCaseCreateUpdateRqst operatorBaseRqst = new Dispatcher.Proxies.PrimaryContactCaseCreateUpdateRqst()
                {
                    Email = curOperator.Email,
                    First = curOperator.FirstName,
                    Last = curOperator.LastName,
                };
                return operatorBaseRqst;
            }
        }

        Dispatcher.Proxies.LanguagePreference? TranslateToLanguagePreferenceProxy(String languagePreference)
        {
            if (String.IsNullOrEmpty(languagePreference))
            {
                return null;
            }
            else
            {
                Dispatcher.Proxies.LanguagePreference languagePreference1 = (Dispatcher.Proxies.LanguagePreference)Enum.Parse(typeof(BreeceWorks.Shared.Enums.LanguagePreference), languagePreference);
                return languagePreference1;
            }
        }

        Dispatcher.Proxies.Privacy? TranslateToPrivacyProxy(String privacyPreference)
        {
            if (String.IsNullOrEmpty(privacyPreference))
            {
                return null;
            }
            else
            {
                Dispatcher.Proxies.Privacy privacy = (Dispatcher.Proxies.Privacy)Enum.Parse(typeof(BreeceWorks.Shared.Enums.Privacy), privacyPreference);
                return privacy;
            }
        }

        List<Dispatcher.Proxies.SecondaryOperatorCreateUpdateRqst> TranslateToProxy(List<BreeceWorks.Shared.CaseObjects.Operator> secondaryOperators)
        {
            if (secondaryOperators == null)
            {
                return null;
            }
            else
            {
                List<Dispatcher.Proxies.SecondaryOperatorCreateUpdateRqst> secondaryOperatorCreateUpdateRqsts = new List<Dispatcher.Proxies.SecondaryOperatorCreateUpdateRqst>();
                foreach(BreeceWorks.Shared.CaseObjects.Operator curOperator in secondaryOperators)
                {
                    secondaryOperatorCreateUpdateRqsts.Add(TranslateToSecondaryOperatorCreateUpdateRqstProxy(curOperator));
                }
                return secondaryOperatorCreateUpdateRqsts;
            }
        }
        Dispatcher.Proxies.SecondaryOperatorCreateUpdateRqst TranslateToSecondaryOperatorCreateUpdateRqstProxy(BreeceWorks.Shared.CaseObjects.Operator curOperator)
        {
            Dispatcher.Proxies.SecondaryOperatorCreateUpdateRqst secondaryOperatorCreateUpdateRqst = new Dispatcher.Proxies.SecondaryOperatorCreateUpdateRqst()
            {
                Email = curOperator.Email,
            };
            return secondaryOperatorCreateUpdateRqst;
        }

        public Dispatcher.Proxies.SMSOutgoingCommunication TranslateToProxy(BreeceWorks.Shared.SMS.SMSOutgoingCommunication sMSOutgoingCommunication)
        {
            if (sMSOutgoingCommunication == null)
            {
                return null;
            }
            else
            {
                Dispatcher.Proxies.SMSOutgoingCommunication sMSOutgoingCommunication1 = new Dispatcher.Proxies.SMSOutgoingCommunication()
                {
                    AttachmentIDs = sMSOutgoingCommunication.attachmentIDs,
                    CaseId = sMSOutgoingCommunication.caseId,
                    Message = sMSOutgoingCommunication.message,
                    Source = sMSOutgoingCommunication.source,
                };
                return sMSOutgoingCommunication1;
            }
        }



        #endregion
    }
}

