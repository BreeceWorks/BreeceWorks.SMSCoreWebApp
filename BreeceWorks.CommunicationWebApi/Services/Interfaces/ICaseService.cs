using BreeceWorks.CommunicationWebApi.Objects;
using BreeceWorks.CommunicationWebApi.RequestObjects;
using BreeceWorks.CommunicationWebApi.ResponseObjects;
using BreeceWorks.Shared.Entities;
using Microsoft.AspNetCore.Mvc;

namespace BreeceWorks.CommunicationWebApi.Services.Interfaces
{
    public interface ICaseService
    {
        public CaseDtoRspse AddCase(CaseDto caseObject);
        public CaseDtoRspse AssignCase(String caseId, CaseAssignmentRequest? assignmentRequest = null);
        public CaseDtoRspse ReopenCase(String caseId);
        public CaseDtoRspse CloseCase(String caseId);
        public CaseDto? GetCase(String caseId);
        public CaseDto UpdateCase(CaseDto caseObject);
        public CaseDto? UpdateCase(string caseId, CaseUpdateRqst caseUpdateRqst);
        public CaseDto? GetCaseTranscript(String caseId);
        public CaseDto[]? GetCases();
    }
}
