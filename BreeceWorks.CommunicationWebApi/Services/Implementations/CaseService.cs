using BreeceWorks.CommunicationWebApi.Objects;
using BreeceWorks.CommunicationWebApi.RequestObjects;
using BreeceWorks.CommunicationWebApi.ResponseObjects;
using BreeceWorks.CommunicationWebApi.Services.Interfaces;
using BreeceWorks.Shared;
using BreeceWorks.Shared.DbContexts;
using BreeceWorks.Shared.Entities;
using BreeceWorks.Shared.Enums;
using Microsoft.EntityFrameworkCore;

namespace BreeceWorks.CommunicationWebApi.Services.Implementations
{
    public class CaseService : ICaseService
    {
            
        private readonly CommunicationDbContext _context;
        private readonly ICustomerService _customerService;
        private readonly IOperatorService _operatorService;
        private readonly IMessagingService _messagingService;
        private readonly ITranslatorService _translatorService;

        public CaseService(CommunicationDbContext context, ICustomerService customerService, IOperatorService operatorService, IMessagingService messagingService, ITranslatorService translatorService)
        {
            _context = context;
            _customerService = customerService;
            _operatorService = operatorService;
            _messagingService = messagingService;
            _translatorService = translatorService;
        }

        public CaseDtoRspse AddCase(CaseDto caseDto)
        {
            CaseDtoRspse? duplicateCase = ValidateNotDuplicateCase(caseDto);
            if (duplicateCase != null)
            {
                return duplicateCase;
            }

            caseDto.State = CaseState.open;
            if (caseDto.Privacy == null)
            {
                caseDto.Privacy = Privacy.@public;
            }
            caseDto.ReferenceId = "Case - " + DateTime.Now.Ticks;

            if (caseDto.LineOfBusiness != null)
            {
                LineOfBusinessDto? lineOfBusinessDto = 
                    _context.lineOfBusinesses.Where(t=>t.Type == caseDto.LineOfBusiness.Type 
                    && t.SubType == caseDto.LineOfBusiness.SubType).FirstOrDefault();
                if (lineOfBusinessDto == null)
                {
                    lineOfBusinessDto = new LineOfBusinessDto() 
                    {
                        Id = Guid.NewGuid(),
                        SubType = caseDto.LineOfBusiness.SubType,
                        Type = caseDto.LineOfBusiness.Type
                    };
                    _context.lineOfBusinesses.Add(lineOfBusinessDto);
                }
                caseDto.LineOfBusiness = lineOfBusinessDto;
            }

            CustomerDto? customer = _customerService.GetCustomerDtoByMobile(caseDto.Customer.Mobile);
            if (customer == null)
            {
                customer = _customerService.GetCustomerDtoByEmail(caseDto.Customer.Email);
            }
            if (customer == null)
            {
                customer = caseDto.Customer;
                customer.Id = Guid.NewGuid();
                customer.OptStatus = false;
                customer.OptStatusDetail = Shared.Constants.OptStatus.REQUESTED.ToString();
                customer = _customerService.AddCustomerDto(customer);
            }
            else
            {
                if (customer.Mobile != caseDto.Customer.Mobile
                    || customer.Email.ToUpper() != caseDto.Customer.Email.ToUpper()
                    || customer.First.ToUpper() != caseDto.Customer.First.ToUpper()
                    || customer.Last.ToUpper()  != caseDto.Customer.Last.ToUpper())
                {
                    return new CaseDtoRspse()
                    {
                        caseDto = caseDto,
                        errors = new Error[]
                        {
                            new Error()
                            {
                                code = Constants.ErrorMessages.DuplicateCustomer,
                                category = "DataIntegrityError",
                                retryable = false,
                                status = 400,
                                detail = Constants.ErrorMessages.DuplicateCustomer,
                                path = "/case/actions/open",
                                method = "POST",
                                requestId = caseDto.Id,
                            }
                        }
                    };
                }
            }
            caseDto.Customer = customer;
            if (caseDto.SecondaryOperators != null)
            {
                List<OperatorDto> existingOperatorList = new List<OperatorDto>();
                foreach(OperatorDto op in caseDto.SecondaryOperators)
                {
                    OperatorDto? operatorObject = _operatorService.GetOperatorDto(op.Email);
                    if (operatorObject != null)
                    {
                        existingOperatorList.Add(operatorObject);
                    }                    
                }
                caseDto.SecondaryOperators = existingOperatorList;
            }

            caseDto.SMSNumber = AssignSMSNumber(caseDto.Customer);
            caseDto.CreateTime = DateTime.Now;
            _context.Cases.Add(caseDto);
            _context.SaveChanges();
            List<string> templateParameterValues = new List<string>();
            templateParameterValues.Add(caseDto.ReferenceId);
            TemplateMessage templateMessageResponse = SendTemplatedMessage(caseDto, Constants.MessageTemplates.Welcome, templateParameterValues);
            CaseDtoRspse addCaseRspse = new CaseDtoRspse()
            {
                caseDto = caseDto,
                errors = templateMessageResponse.errors
            };
            return addCaseRspse;
        }

        public CaseDto? GetCase(String caseId)
        {
            return _context.Cases.Include(c=>c.Customer).Include(c=>c.SecondaryOperators).Include(c=>c.LineOfBusiness).Where(c=>c.Id.ToString() == caseId).FirstOrDefault();
        }

        public CaseDto[]? GetCases()
        {
            if (_context.Cases == null || !_context.Cases.Any())
            {
                return null;
            }
            return _context.Cases.Include(c => c.Customer).Include(c => c.SecondaryOperators).Include(c => c.LineOfBusiness).ToArray();
        }

        public CaseDto? GetCaseTranscript(String caseId)
        {
            return _context.Cases.Include(c=>c.Customer).Include(c=>c.SecondaryOperators).Include(c=>c.LineOfBusiness).Include(c => c.Messages).ThenInclude(m => m.author).Include(c => c.Messages).ThenInclude(m=>m.messageAttachments).Where(c => c.Id.ToString() == caseId).FirstOrDefault();
        }

        public CaseDto UpdateCase(CaseDto caseObject)
        {
            _context.Cases.Update(caseObject);
            _context.SaveChanges();
            return caseObject;
        }

        public CaseDto? UpdateCase(string caseId, CaseUpdateRqst caseUpdateRqst)
        {
            CaseDto? caseDto = GetCase(caseId);
            if (caseDto != null)
            {
                caseDto.ClaimNumber = caseUpdateRqst.caseData.claimNumber;
                caseDto.DateOfLoss = caseUpdateRqst.caseData.dateOfLoss;
                caseDto.PolicyNumber = caseUpdateRqst.caseData.policyNumber;
                caseDto.Deductible = caseUpdateRqst.caseData?.deductible;
                caseDto.Brand = caseUpdateRqst?.caseData?.brand;
                caseDto.LineOfBusiness = _translatorService.TranslateToDto(_translatorService.TranslateToObject(caseUpdateRqst.caseData.lineOfBusiness));
                caseDto.BusinessName = caseUpdateRqst.businessName;
                caseDto.Privacy = caseUpdateRqst.privacy;
                caseDto.LanguagePreference = caseUpdateRqst.languagePreference;
                caseDto.CreatedBy = null;
                caseDto.PrimaryContact = null;
                caseDto.SecondaryOperators = null;
                if (caseUpdateRqst.primaryContact != null && !String.IsNullOrEmpty(caseUpdateRqst.primaryContact.email))
                {
                    OperatorDto? operatorDto = _operatorService.GetOperatorDto(caseUpdateRqst.primaryContact.email);
                    if (operatorDto != null)
                    {
                        caseDto.PrimaryContact = operatorDto.Id;
                    }
                }
                if (caseUpdateRqst.createdBy != null && !String.IsNullOrEmpty(caseUpdateRqst.createdBy.email))
                {
                    OperatorDto? operatorDto = _operatorService.GetOperatorDto(caseUpdateRqst.createdBy.email);
                    if (operatorDto != null)
                    {
                        caseDto.CreatedBy = operatorDto.Id;
                    }
                }
                if (caseUpdateRqst.secondaryOperators != null)
                {
                    List<OperatorDto> existingOperatorList = new List<OperatorDto>();
                    foreach (SecondaryOperatorCreateUpdateRqst op in caseUpdateRqst.secondaryOperators)
                    {
                        OperatorDto? operatorObject = _operatorService.GetOperatorDto(op.email);
                        if (operatorObject != null)
                        {
                            existingOperatorList.Add(operatorObject);
                        }
                    }
                    caseDto.SecondaryOperators = existingOperatorList;
                }
                caseDto = UpdateCase(caseDto);
            }
            return caseDto;
        }

        private CaseDtoRspse? ValidateNotDuplicateCase(CaseDto caseDto)
        {
            switch (caseDto.CaseType)
            {
                case CaseType.claim:
                    CaseDto? duplicateClaimCase = _context.Cases.Include(c => c.Customer)
                        .Where(c => c.ClaimNumber == caseDto.ClaimNumber
                        && (c.Customer.Email == caseDto.Customer.Email || c.Customer.Mobile == caseDto.Customer.Mobile)).FirstOrDefault();
                    if (duplicateClaimCase != null)
                    {
                        return new CaseDtoRspse()
                        {
                            errors = new Error[]
                            {
                                new Error()
                                {
                                    code = Constants.ErrorMessages.ClaimExists,
                                    category = "DataIntegrityError",
                                    retryable = false,
                                    status = 400,
                                    detail = "Claim already exists for your requests customer mobile and claim number combination.",
                                    path = "/case/actions/open",
                                    method = "POST",
                                    requestId = Guid.NewGuid(),
                                    meta = new ErrorRspseMeta()
                                    {
                                        existingCase = new ResponseObjects.CaseData()
                                        {
                                            Id = caseDto.Id,
                                            brand = caseDto.Brand,
                                            claimNumber = caseDto.ClaimNumber,
                                            dateOfLoss = caseDto.DateOfLoss,
                                            deductible = caseDto.Deductible,
                                            policyNumber = caseDto.PolicyNumber
                                        }
                                    }
                                }
                            }
                        };
                    }
                    break;
                case CaseType.policy:
                    CaseDto? duplicatePolicyCase = _context.Cases.Include(c => c.Customer)
                        .Where(c => c.PolicyNumber == caseDto.PolicyNumber
                        && (c.Customer.Email == caseDto.Customer.Email || c.Customer.Mobile == caseDto.Customer.Mobile)).FirstOrDefault();
                    if (duplicatePolicyCase != null)
                    {
                        return new CaseDtoRspse()
                        {
                            errors = new Error[]
                            {
                                new Error()
                                {
                                    code = Constants.ErrorMessages.ClaimExists,
                                    category = "DataIntegrityError",
                                    retryable = false,
                                    status = 400,
                                    detail = "Claim already exists for your requests customer mobile and claim number combination.",
                                    path = "/case/actions/open",
                                    method = "POST",
                                    requestId = Guid.NewGuid(),
                                    meta = new ErrorRspseMeta()
                                    {
                                        existingCase = new ResponseObjects.CaseData()
                                        {
                                            Id = caseDto.Id,
                                            brand = caseDto.Brand,
                                            claimNumber = caseDto.ClaimNumber,
                                            dateOfLoss = caseDto.DateOfLoss,
                                            deductible = caseDto.Deductible,
                                            policyNumber = caseDto.PolicyNumber
                                        }
                                    }
                                }
                            }
                        };
                    }
                    break;
                default:
                    return null;
            }
            return null;
        }

        private TemplateMessage SendTemplatedMessage(CaseDto caseDto, string templateName, List<string>? messageTemplateParameterValues = null)
        {
            TemplatedMessageRequest templatedMessageRequest = new TemplatedMessageRequest()
            {
                caseId = caseDto.Id.ToString(),
                referenceID = caseDto.ReferenceId,
                templateValues = new Dictionary<string, string>()
            };
            MessageTemplateDto templatedMessageDto;
            try
            {
                templatedMessageDto = _context.MessageTemplates.Include(t=>t.TemplateValues).Where(t => t.Name == templateName).First();
            }
            catch (Exception ex)
            {
                throw new Exception(Constants.ErrorMessages.TemplateNotFound);
            }

            if (messageTemplateParameterValues != null)
            {
                for (int i = 0; i < messageTemplateParameterValues.Count; i++)
                {

                    templatedMessageRequest.templateValues.Add(templatedMessageDto.TemplateValues[i].Name, messageTemplateParameterValues[i]);
                }
            }
            if (caseDto.PrimaryContact != null)
            {
                templatedMessageRequest.source = RequestObjects.TemplateMessageSource.assigned.ToString();
            }
            else
            {
                templatedMessageRequest.source = RequestObjects.TemplateMessageSource.ai.ToString();
            }
            return _messagingService.SendTemplateMessage(templatedMessageRequest, templatedMessageDto.TemplateId);
        }

        private string? AssignSMSNumber(CustomerDto customer)
        {
            // We don't want to duplicate assigned numbers for the same customer.
            // We check for open cases because we assume numbers for closed cases are safe to reuse.  
            if (_context.Cases.Where(c=>c.Customer == customer && c.State == CaseState.open).Any())
            {
                List<String?> assignedNumbers = _context
                    .Cases.Where(c => c.Customer == customer && c.State == CaseState.open).ToList()
                    .Select(c => c.SMSNumber).ToList();
                
                foreach(CompanyPhoneNumberDto companyNumberDto in _context.CompanyPhoneNumbers)
                {
                    if (!assignedNumbers.Contains(companyNumberDto.PhoneNumber))
                    {
                        return companyNumberDto.PhoneNumber;
                    }
                }
            }
            // If the customer has no open cases, just pick one.
            return _context.CompanyPhoneNumbers.First().PhoneNumber;
        }

        public CaseDtoRspse AssignCase(String caseId, CaseAssignmentRequest? assignmentRequest = null)
        {
            CaseDtoRspse caseRspse = new CaseDtoRspse();

            CaseDto? caseDto = GetCase(caseId);
            Guid? updatedPrimaryContactID = null;
            if (assignmentRequest != null)
            {
                OperatorDto? operatorDto = null;

                if (assignmentRequest.id != null)
                {
                    updatedPrimaryContactID = assignmentRequest.id;
                }
                else if (!String.IsNullOrEmpty(assignmentRequest.phoneNumber))
                {
                    operatorDto = _operatorService.GetOperatorDto(assignmentRequest.phoneNumber);
                    if (operatorDto != null)
                    {
                        updatedPrimaryContactID = operatorDto.Id;
                    }
                }
                else if (!String.IsNullOrEmpty(assignmentRequest.email))
                {
                    operatorDto = _operatorService.GetOperatorDto(assignmentRequest.email);
                    if (operatorDto != null)
                    {
                        updatedPrimaryContactID = operatorDto.Id;
                    }
                }
                if (updatedPrimaryContactID == null)
                {
                    caseRspse.errors = new Error[1];
                    caseRspse.errors[0] = new Error()
                    {
                        code = "UserNotFound",
                        category = "NotFound",
                        detail = String.Format("No user found for primaryContact"),
                        method = "PUT",
                        retryable = false,
                        status = 404,
                    };
                    return caseRspse;
                }
            }

            if (caseDto != null)
            {
                if (updatedPrimaryContactID != null || (updatedPrimaryContactID == null && caseDto.Privacy == Shared.Enums.Privacy.@public))
                {
                    caseDto.PrimaryContact = updatedPrimaryContactID;
                    caseDto = UpdateCase(caseDto);
                    if (caseDto.PrimaryContact != null)
                    {
                        OperatorDto? operatorDto = _operatorService.GetOperatorDto(caseDto.PrimaryContact);
                        if (operatorDto != null)
                        {
                            List<string> templateParameterValues = new List<string>
                            {
                                operatorDto.Email
                            };
                            try
                            {
                                SendTemplatedMessage(caseDto, Constants.MessageTemplates.PRIMARY_CONTACT_ASSIGNED, templateParameterValues);
                            }
                            catch
                            {
                                caseRspse.errors = new Error[1];
                                caseRspse.errors[0] = new Error()
                                {
                                    code = "FailedToSendMessage",
                                    category = "ServerError",
                                    detail = "Failed To Send Message.",
                                    method = "PUT",
                                    retryable = false,
                                    status = 400,
                                    path = "/case/actions/assign/{caseId}"
                                };
                            }
                        }
                    }
                    else
                    {
                        try
                        {
                            SendTemplatedMessage(caseDto, Constants.MessageTemplates.PRIMARY_CONTACT_UNASSIGNED);
                        }
                        catch
                        {
                            caseRspse.errors = new Error[1];
                            caseRspse.errors[0] = new Error()
                            {
                                code = "FailedToSendMessage",
                                category = "ServerError",
                                detail = "Failed To Send Message.",
                                method = "PUT",
                                retryable = false,
                                status = 400,
                                path = "/case/actions/assign/{caseId}"
                            };
                        }
                    }
                }
                else
                {
                    caseRspse.errors = new Error[1];
                    caseRspse.errors[0] = new Error()
                    {
                        code = "PrivateCaseCannotBeUnassigned",
                        category = "DataValidationError",
                        detail = "Private case cannot be unassigned.",
                        method = "PUT",
                        retryable = false,
                        status = 400,
                        path = "/case/actions/assign/{caseId}"
                    };
                }
            }
            if (caseDto == null)
            {
                caseRspse.errors = new Error[1];
                caseRspse.errors[0] = new Error()
                {
                    code = "CaseNotFound",
                    category = "NotFound",
                    detail = String.Format("No case found for caseId {0}", caseId),
                    method = "GET",
                    retryable = false,
                    status = 404,
                    path = "/case/actions/assign/{caseId}"
                };
            }

            caseRspse.caseDto = caseDto;
            return caseRspse;
        }

        public CaseDtoRspse ReopenCase(String caseId)
        {
            CaseDtoRspse caseRspse = new CaseDtoRspse();

            CaseDto? caseDto = GetCase(caseId);

            if (caseDto != null)
            {
                caseDto.State = CaseState.open;
                // When reopening a case, you ideally want to keep using the same SMS number to 
                // prevent customer confusion.  However, if that number is currently being used for
                // another open case, we will have no choice but to assign another number to the reopened case.
                if (_context.Cases.Where(c => c.Customer == caseDto.Customer && c.SMSNumber == caseDto.SMSNumber && c.State == CaseState.open).Any())
                {
                    caseDto.SMSNumber = AssignSMSNumber(caseDto.Customer);
                }
                caseDto = UpdateCase(caseDto);
            }
            if (caseDto == null)
            {
                caseRspse.errors = new Error[1];
                caseRspse.errors[0] = new Error()
                {
                    code = "CaseNotFound",
                    category = "NotFound",
                    detail = String.Format("No case found for caseId {0}", caseId),
                    method = "GET",
                    retryable = false,
                    status = 404,
                    path = "/case/actions/reopen/{caseId}"
                };
            }

            caseRspse.caseDto = caseDto;
            return caseRspse;
        }


        public CaseDtoRspse CloseCase(String caseId)
        {
            CaseDtoRspse caseRspse = new CaseDtoRspse();

            CaseDto? caseDto = GetCase(caseId);

            if (caseDto != null)
            {
                caseDto.State = CaseState.closed;
                caseDto = UpdateCase(caseDto);
            }
            if (caseDto == null)
            {
                caseRspse.errors = new Error[1];
                caseRspse.errors[0] = new Error()
                {
                    code = "CaseNotFound",
                    category = "NotFound",
                    detail = String.Format("No case found for caseId {0}", caseId),
                    method = "GET",
                    retryable = false,
                    status = 404,
                    path = "/case/actions/close/{caseId}"
                };
            }

            caseRspse.caseDto = caseDto;
            return caseRspse;
        }

    }
}
