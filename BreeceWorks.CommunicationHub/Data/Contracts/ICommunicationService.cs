namespace BreeceWorks.CommunicationHub.Data.Contracts
{
    public interface ICommunicationService
    {
        Task<BreeceWorks.Shared.CaseObjects.Users> GetAllUsers();
        Task<BreeceWorks.Shared.CaseObjects.Operators> GetAllOperators();
        Task<BreeceWorks.Shared.CaseObjects.Operator> CreateOperator(BreeceWorks.Shared.CaseObjects.Operator newOperator);
        Task<BreeceWorks.Shared.CaseObjects.Operator> DeleteOperator(Guid operatorId);
        Task<BreeceWorks.Shared.CaseObjects.ActiveCases> GetCasesForUserByUserID(Guid userID);
        Task<BreeceWorks.Shared.CaseObjects.Case> GetCaseByID(Guid caseId);
        Task<BreeceWorks.Shared.CaseObjects.Case> UpdateCase(BreeceWorks.Shared.CaseObjects.Case updatedCase);
        Task<BreeceWorks.Shared.CaseObjects.CaseRspses> GetAllCases();
        Task<BreeceWorks.Shared.CaseObjects.Case> AssignCase(Guid caseId, String? operatorEmail);
        Task<BreeceWorks.Shared.CaseObjects.CaseTranscript> GetCaseTranscript(Guid caseId);
        Task<BreeceWorks.Shared.CaseObjects.Case> ReopenCase(Guid caseId);
        Task<BreeceWorks.Shared.CaseObjects.Case> CloseCase(Guid caseId);
        Task<BreeceWorks.Shared.CaseObjects.Case> OpenNewCase(BreeceWorks.Shared.CaseObjects.CaseCreateRqst caseCreateRqst);
        Task<BreeceWorks.Shared.CaseObjects.Operator?> GetOperatorByID(Guid curOperatorID);
        Task<BreeceWorks.Shared.CaseObjects.Operator?> GetOperatorByEmail(String curOperatorEmail);
        Task<BreeceWorks.Shared.CaseObjects.Operator> UpdateOperator(BreeceWorks.Shared.CaseObjects.Operator curOperator);
        Task<String> UploadAttachment(MultipartFormDataContent content);
        Task<BreeceWorks.Shared.SMS.SMSIncomingeMessage> SendMessage(BreeceWorks.Shared.SMS.SMSOutgoingCommunication sMSMessage);

    }
}
