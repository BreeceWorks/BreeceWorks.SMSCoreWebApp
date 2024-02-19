using BreeceWorks.CommunicationWebApi.Objects;
using BreeceWorks.CommunicationWebApi.RequestObjects;
using BreeceWorks.CommunicationWebApi.ResponseObjects;
using BreeceWorks.Shared.Entities;
using BreeceWorks.Shared.SMS;

namespace BreeceWorks.CommunicationWebApi.Services.Interfaces
{
    public interface ITranslatorService
    {
        #region TranslateToObject
        Objects.Operator? TranslateToObject(OperatorCreateRqst? operatorCreateRqst);
        Objects.Operator TranslateToObject(OperatorUpdateRqst operatorCreateRqst);
        Objects.Customer? TranslateToObject(CustomerDto? customerObject);
        Objects.Operator? TranslateToObject(OperatorDto? operatorDto);
        string[]? TranslateToObject(List<OperatorRoleDto> operatorRoles);
        Objects.Case TranslateToObject(CaseCreateRqst caseCreateRqst);
        Objects.Case TranslateToObject(CaseUpdateRqst caseUpdateRqst);
        Objects.Case TranslateToObject(CaseDto caseDto);
        Objects.CaseTranscript TranslateToObject(CaseTranscriptDto caseDto);
        Objects.CaseData? TranslateToObject(CaseDataCreateUpdateRqst? caseDataCreateRqst);
        Objects.LineOfBusiness? TranslateToObject(LineOfBusinessRqst? lineOfBusinessRqst);
        Objects.LineOfBusiness? TranslateToObject(LineOfBusinessDto? lineOfBusiness);
        Objects.Operator? TranslateToObject(OperatorBaseRqst? createdBy);
        Objects.Customer? TranslateToObject(CustomerCreateUpdateRqst? customer);
        Objects.Operator[]? TranslateToObject(SecondaryOperatorCreateUpdateRqst[]? secondaryOperators);
        Objects.Operator[]? TranslateToObject(List<OperatorDto>? operators);
        List<Objects.Message>? TranslateToObject(List<MessageDto>? messages);
        List<Objects.MessageAttachment>? TranslateToObject(List<MessageAttachmentDto>? messageAttachments);
        Objects.MessageAuthor? TranslateToObject(MessageAuthorDto? messageAuthor);

        #endregion


        #region TranslateToDto
        OperatorDto? TranslateToDto(Objects.Operator? operatorObject);
        CustomerDto TranslateToDto(Objects.Customer customerObject);
        CaseDto TranslateToDto(Objects.Case caseObject);
        LineOfBusinessDto? TranslateToDto(Objects.LineOfBusiness? lineOfBusiness);
        List<OperatorDto>? TranslateToDto(Objects.Operator[]? operators);
        List<MessageAttachmentDto>? TranslateToDto(List<SMSAttachment>? attachments);


        #endregion

        #region TranslateToRspse
        ResponseObjects.Operator? TranslateToRspse(Objects.Operator? operatorDto);
        List<ResponseObjects.MessageAttachment>? TranslateToRspse(List<Objects.MessageAttachment>? messageAttachments);
        ResponseObjects.MessageAuthor? TranslateToRspse(Objects.MessageAuthor? messageAuthor);
        List<ResponseObjects.Message>? TranslateToRspse(List<Objects.Message>? messages);
        ResponseObjects.Case TranslateToRspse(Objects.Case caseObject);
        ResponseObjects.Customer? TranslateToRspse(Objects.Customer? customer);
        ResponseObjects.CaseTranscript TranslateToRspse(Objects.CaseTranscript caseObject);


        #endregion


    }
}
