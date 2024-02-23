using AutoMapper;
using BreeceWorks.CommunicationWebApi.Objects;
using BreeceWorks.CommunicationWebApi.ResponseObjects;
using BreeceWorks.CommunicationWebApi.Services.Interfaces;
using BreeceWorks.Shared.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BreeceWorks.CommunicationWebApi.Controllers
{
    /// <summary>
    /// Download Transcripts
    /// </summary>
    [Tags("Transcript Actions")]
    [Route("api/[controller]")]
    [ApiController]
    public class TranscriptController : ControllerBase
    {
        private readonly ICaseService _caseService;
        private readonly IOperatorService _operatorService;
        private readonly ITranslatorService _translatorService;
        private readonly IMapper _mapper;

        public TranscriptController(ICaseService caseService, IOperatorService operatorService, ITranslatorService translatorService, IMapper mapper)
        {
            _caseService = caseService;
            _operatorService = operatorService;
            _translatorService = translatorService;
            _mapper = mapper;
        }

        /// <summary>
        /// Download Transcript - Download the transcript of the case
        /// </summary>
        /// <remarks>Based on passed in caseId, this initiates the direct download of a case transcript.</remarks>
        /// <response code="200"></response>
        /// <response code="400"></response>

        [HttpGet]
        [Route("actions/download/{caseId}")]
        public ActionResult<ResponseObjects.CaseTranscript> GetTranscript(string caseId)
        {
            CaseDto? caseDto = _caseService.GetCaseTranscript(caseId);
            Objects.CaseTranscript caseTranscriptObject = new Objects.CaseTranscript();
            ResponseObjects.CaseTranscript caseRspse = new ResponseObjects.CaseTranscript();
            if (caseDto != null)
            {
                if (caseDto.Messages != null)
                {
                    caseDto.Messages = caseDto.Messages.OrderBy(m=>m.createdAt).ToList();
                }
                CaseTranscriptDto caseTranscriptDto = _mapper.Map<CaseTranscriptDto>(caseDto);
                caseTranscriptObject = _translatorService.TranslateToObject(caseTranscriptDto);
                if (caseTranscriptObject.PrimaryContact != null)
                {
                    caseTranscriptObject.PrimaryContact = _translatorService.TranslateToObject(_operatorService.GetOperatorDto(caseTranscriptObject.PrimaryContact.Id));
                }
                if (caseTranscriptObject.CreatedBy != null)
                {
                    caseTranscriptObject.CreatedBy = _translatorService.TranslateToObject(_operatorService.GetOperatorDto(caseTranscriptObject.CreatedBy.Id));
                }
                caseRspse = _translatorService.TranslateToRspse(caseTranscriptObject);
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
                    path = "/case/actions/{caseid}"
                };
            }
            return caseRspse;
        }

    }
}
