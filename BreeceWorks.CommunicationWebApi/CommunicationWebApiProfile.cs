using AutoMapper;
using BreeceWorks.Shared.Entities;

namespace BreeceWorks.CommunicationWebApi
{
    public class CommunicationWebApiProfile : Profile
    {
        public CommunicationWebApiProfile()
        {
            CreateMap<CaseDto, CaseTranscriptDto>();
        }
    }
}
