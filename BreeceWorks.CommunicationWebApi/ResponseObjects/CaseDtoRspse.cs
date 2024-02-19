using BreeceWorks.Shared.Entities;

namespace BreeceWorks.CommunicationWebApi.ResponseObjects
{
    public class CaseDtoRspse
    {
        public CaseDto? caseDto { get; set; }
        public Error[]? errors { get; set; }

    }
}
