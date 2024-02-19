using AutoMapper;
using BreeceWorks.CommunicationWebApi.Objects;
using BreeceWorks.CommunicationWebApi.RequestObjects;
using BreeceWorks.CommunicationWebApi.ResponseObjects;
using BreeceWorks.CommunicationWebApi.Services.Interfaces;
using BreeceWorks.Shared.Entities;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BreeceWorks.CommunicationWebApi.Controllers
{
    [Tags("Case Actions")]
    [Route("api/case")]
    [ApiController]
    public class CaseController : ControllerBase
    {
        private readonly ICaseService _caseService;
        private readonly IOperatorService _operatorService;
        private readonly ITranslatorService _translatorService;
        private readonly IMapper _mapper;

        public CaseController(ICaseService caseService, IOperatorService operatorService, ITranslatorService translatorService, IMapper mapper)
        {
            _caseService = caseService;
            _operatorService = operatorService;
            _translatorService = translatorService;
            _mapper = mapper;
        }




        /// <summary>
        /// Open new case - Opens a new case and triggers the workflow associated with the case type.
        /// </summary>
        /// <remarks>Open new case - Opens a new case and triggers the workflow associated with the case type.</remarks>
        /// <response code="200"></response>
        /// <response code="400"></response>
        [HttpPost]
        [Route("actions/open")]
        public ActionResult<ResponseObjects.Case> Open([FromBody] CaseCreateRqst caseCreateRqst)
        {
            Objects.Case caseObject = new Objects.Case();
            try
            {
                caseObject = _translatorService.TranslateToObject(caseCreateRqst);
                caseObject.CaseData.Id = Guid.NewGuid();
                if (caseObject.PrimaryContact != null)
                {
                    caseObject.PrimaryContact = _translatorService.TranslateToObject(_operatorService.GetOperatorDto(caseObject.PrimaryContact.Email));
                }
                if (caseObject.CreatedBy != null)
                {
                    caseObject.CreatedBy = _translatorService.TranslateToObject(_operatorService.GetOperatorDto(caseObject.CreatedBy.Email));
                }
                CaseDtoRspse addCaseRspse = _caseService.AddCase(_translatorService.TranslateToDto(caseObject));
                ResponseObjects.Case caseRspse = new ResponseObjects.Case();
                if (addCaseRspse.caseDto != null)
                {
                    caseObject = _translatorService.TranslateToObject(addCaseRspse.caseDto);
                    if (caseObject.PrimaryContact != null)
                    {
                        caseObject.PrimaryContact = _translatorService.TranslateToObject(_operatorService.GetOperatorDto(caseObject.PrimaryContact.Id));
                    }
                    if (caseObject.CreatedBy != null)
                    {
                        caseObject.CreatedBy = _translatorService.TranslateToObject(_operatorService.GetOperatorDto(caseObject.CreatedBy.Id));
                    }
                    caseRspse = _translatorService.TranslateToRspse(caseObject);
                }
                if (addCaseRspse.errors != null)
                {
                    caseRspse.errors = new Error[addCaseRspse.errors.Length];

                    for (int i = 0; i < addCaseRspse.errors.Length; i++)
                    {
                        caseRspse.errors[i] = new Error()
                        {   
                            code = addCaseRspse.errors[i].code,
                            category = addCaseRspse.errors[i].category,
                            detail = addCaseRspse.errors[i].detail,
                            method = addCaseRspse.errors[i].method,
                            requestId = caseObject.CaseData.Id,
                            retryable = addCaseRspse.errors[i].retryable,
                            status = addCaseRspse.errors[i].status,
                            path = "/case/actions/open"
                        };
                    }
                }
                return caseRspse;
            }
            catch (Exception ex)
            {
                return new ResponseObjects.Case()
                {
                    errors = new Error[]
                    {
                            new Error()
                            {
                                code = ex.Message,
                                category = "DataIntegrityError",
                                retryable = false,
                                status = 500,
                                detail = ex.Message,
                                path = "/case/actions/open",
                                method = "POST",
                                requestId = caseObject.CaseData.Id
                            }
                    }
                };
            }
        }


        /// <summary>
        /// Assign primary contact - Assigns/reassigns the primary contact for the case and triggers the associated workflow.
        /// </summary>
        /// <remarks><table><tr><td><h1>Assign Case</h1></td></tr>
        /// <tr><td>The body of the request must include a primaryContact and either email, mobile, or id of the operator</td></tr>
        /// </table>
        /// <table><tr><td><h1>Unassign Case</h1></td></tr>
        /// <tr><td>The case can be unassigned if the request body is empty</td></tr>
        /// </table>
        /// <table><tr><td><h1><strong>Assign case without automated notification</strong></h1></td></tr>
        /// <tr><td><p>To reassign a case without initiating a notification message for the insured, use the PUT <i><b>/api/case/{caseId}</b></i> endpoint instead of assign.</p></td></tr>
        /// </table>
        /// </remarks>
        /// <param name="caseId">The UUID of the case you would like to update primary contact for</param>
        /// <param name="assignRequest"></param>
        /// <response code="200"></response>
        /// <response code="400"></response>
        [HttpPut]
        [Route("actions/assign/{caseId}")]
        public ActionResult<ResponseObjects.Case> Assign(string caseId, [FromBody] AssignRequest? assignRequest = null)
        {
            Objects.Case caseObject = new Objects.Case();
            CaseDtoRspse assignResponse = new CaseDtoRspse();
            try
            {
                if (assignRequest != null)
                {
                    assignResponse = _caseService.AssignCase(caseId, new CaseAssignmentRequest() { email = assignRequest.primaryContact.email, id = assignRequest.primaryContact.id, phoneNumber = assignRequest.primaryContact.mobile });
                }
                else
                {
                    assignResponse = _caseService.AssignCase(caseId);
                }
                ResponseObjects.Case caseRspse = new ResponseObjects.Case();
                if (assignResponse.caseDto != null)
                {
                    caseObject = _translatorService.TranslateToObject(assignResponse.caseDto);
                    if (caseObject.PrimaryContact != null)
                    {
                        caseObject.PrimaryContact = _translatorService.TranslateToObject(_operatorService.GetOperatorDto(caseObject.PrimaryContact.Id));
                    }
                    if (caseObject.CreatedBy != null)
                    {
                        caseObject.CreatedBy = _translatorService.TranslateToObject(_operatorService.GetOperatorDto(caseObject.CreatedBy.Id));
                    }
                    caseRspse = _translatorService.TranslateToRspse(caseObject);
                }
                if (assignResponse.errors != null)
                {
                    caseRspse.errors = new Error[assignResponse.errors.Length];

                    for (int i = 0; i < assignResponse.errors.Length; i++)
                    {
                        caseRspse.errors[i] = new Error()
                        {
                            code = assignResponse.errors[i].code,
                            category = assignResponse.errors[i].category,
                            detail = assignResponse.errors[i].detail,
                            method = assignResponse.errors[i].method,
                            requestId = caseObject.CaseData.Id,
                            retryable = assignResponse.errors[i].retryable,
                            status = assignResponse.errors[i].status,
                            path = "/case/actions/assign"
                        };
                    }
                }
                return caseRspse;
            }
            catch (Exception ex)
            {
                return new ResponseObjects.Case()
                {
                    errors = new Error[]
                    {
                            new Error()
                            {
                                code = ex.Message,
                                category = "DataIntegrityError",
                                retryable = false,
                                status = 500,
                                detail = ex.Message,
                                path = "/case/actions/assign",
                                method = "POST",
                            }
                    }
                };
            }
        }

        /// <summary>
        /// Reopen Case - Updates specific case to status of 'open'.
        /// </summary>
        /// <remarks><table><tr><td><h1>Reopen Case</h1></td></tr>
        /// <tr><td>Reopening a closed case simply removes the closed flag and makes the case available for chat.</td></tr>
        /// </table>
        /// </remarks>
        /// <param name="caseId">The UUID of the case you would like to update</param>
        /// <response code="200"></response>
        /// <response code="400"></response>
        [HttpPut]
        [Route("actions/reopen/{caseId}")]
        public ActionResult<ResponseObjects.Case> Reopen(string caseId)
        {
            Objects.Case caseObject = new Objects.Case();
            CaseDtoRspse reopenResponse = new CaseDtoRspse();
            try
            {
                reopenResponse = _caseService.ReopenCase(caseId);
                ResponseObjects.Case caseRspse = new ResponseObjects.Case();
                if (reopenResponse.caseDto != null)
                {
                    caseObject = _translatorService.TranslateToObject(reopenResponse.caseDto);
                    if (caseObject.PrimaryContact != null)
                    {
                        caseObject.PrimaryContact = _translatorService.TranslateToObject(_operatorService.GetOperatorDto(caseObject.PrimaryContact.Id));
                    }
                    if (caseObject.CreatedBy != null)
                    {
                        caseObject.CreatedBy = _translatorService.TranslateToObject(_operatorService.GetOperatorDto(caseObject.CreatedBy.Id));
                    }
                    caseRspse = _translatorService.TranslateToRspse(caseObject);
                }
                if (reopenResponse.errors != null)
                {
                    caseRspse.errors = new Error[reopenResponse.errors.Length];

                    for (int i = 0; i < reopenResponse.errors.Length; i++)
                    {
                        caseRspse.errors[i] = new Error()
                        {
                            code = reopenResponse.errors[i].code,
                            category = reopenResponse.errors[i].category,
                            detail = reopenResponse.errors[i].detail,
                            method = reopenResponse.errors[i].method,
                            requestId = caseObject.CaseData.Id,
                            retryable = reopenResponse.errors[i].retryable,
                            status = reopenResponse.errors[i].status,
                            path = "/case/actions/reopen"
                        };
                    }
                }
                return caseRspse;
            }
            catch (Exception ex)
            {
                return new ResponseObjects.Case()
                {
                    errors = new Error[]
                    {
                            new Error()
                            {
                                code = ex.Message,
                                category = "DataIntegrityError",
                                retryable = false,
                                status = 500,
                                detail = ex.Message,
                                path = "/case/actions/reopen",
                                method = "POST",
                            }
                    }
                };
            }
        }


        /// <summary>
        /// Close Case - Updates specific case to status of 'closed'.
        /// </summary>
        /// <remarks><table><tr><td><h1>Close Case</h1></td></tr>
        /// <tr><td>Closing a case simply flags the case as closed for easier organization of cases in the UI.</td></tr>
        /// </table>
        /// </remarks>
        /// <param name="caseId">The UUID of the case you would like to update</param>
        /// <response code="200"></response>
        /// <response code="400"></response>
        [HttpPut]
        [Route("actions/close/{caseId}")]
        public ActionResult<ResponseObjects.Case> Close(string caseId)
        {
            Objects.Case caseObject = new Objects.Case();
            CaseDtoRspse reopenResponse = new CaseDtoRspse();
            try
            {
                reopenResponse = _caseService.CloseCase(caseId);
                ResponseObjects.Case caseRspse = new ResponseObjects.Case();
                if (reopenResponse.caseDto != null)
                {
                    caseObject = _translatorService.TranslateToObject(reopenResponse.caseDto);
                    if (caseObject.PrimaryContact != null)
                    {
                        caseObject.PrimaryContact = _translatorService.TranslateToObject(_operatorService.GetOperatorDto(caseObject.PrimaryContact.Id));
                    }
                    if (caseObject.CreatedBy != null)
                    {
                        caseObject.CreatedBy = _translatorService.TranslateToObject(_operatorService.GetOperatorDto(caseObject.CreatedBy.Id));
                    }
                    caseRspse = _translatorService.TranslateToRspse(caseObject);
                }
                if (reopenResponse.errors != null)
                {
                    caseRspse.errors = new Error[reopenResponse.errors.Length];

                    for (int i = 0; i < reopenResponse.errors.Length; i++)
                    {
                        caseRspse.errors[i] = new Error()
                        {
                            code = reopenResponse.errors[i].code,
                            category = reopenResponse.errors[i].category,
                            detail = reopenResponse.errors[i].detail,
                            method = reopenResponse.errors[i].method,
                            requestId = caseObject.CaseData.Id,
                            retryable = reopenResponse.errors[i].retryable,
                            status = reopenResponse.errors[i].status,
                            path = "/case/actions/close"
                        };
                    }
                }
                return caseRspse;
            }
            catch (Exception ex)
            {
                return new ResponseObjects.Case()
                {
                    errors = new Error[]
                    {
                            new Error()
                            {
                                code = ex.Message,
                                category = "DataIntegrityError",
                                retryable = false,
                                status = 500,
                                detail = ex.Message,
                                path = "/case/actions/close",
                                method = "POST",
                            }
                    }
                };
            }
        }

        /// <summary>
        /// Get case - Gets the case object specified in the caseId path parameter
        /// </summary>
        /// <remarks>Gets the case object specified in the caseId path parameter</remarks>
        /// <response code="200"></response>
        /// <response code="400"></response>

        [HttpGet("{caseId}")]
        public ActionResult<ResponseObjects.Case> Get(string caseId)
        {
            CaseDto? caseDto = _caseService.GetCase(caseId);
            Objects.Case caseObject = new Objects.Case();
            ResponseObjects.Case caseRspse = new ResponseObjects.Case();
            if (caseDto != null)
            {
                caseObject = _translatorService.TranslateToObject(caseDto);
                if (caseObject.PrimaryContact != null)
                {
                    caseObject.PrimaryContact = _translatorService.TranslateToObject(_operatorService.GetOperatorDto(caseObject.PrimaryContact.Id));
                }
                if (caseObject.CreatedBy != null)
                {
                    caseObject.CreatedBy = _translatorService.TranslateToObject(_operatorService.GetOperatorDto(caseObject.CreatedBy.Id));
                }
                caseRspse = _translatorService.TranslateToRspse(caseObject);
            }
            if (caseDto == null)
            {
                caseRspse.errors = new Error[1];
                caseRspse.errors[0] = new Error()
                {
                    code = "CaseNotFound",
                    category = "NotFound",
                    detail = String.Format("No case found for caseId {0}", caseId) ,
                    method = "GET",
                    retryable = false,
                    status = 404,
                    path = "/case/actions/{caseid}"
                };
            }
            return caseRspse;
        }

        /// <summary>
        /// Get cases
        /// </summary>
        /// <remarks>Gets the cases</remarks>
        /// <response code="200"></response>
        /// <response code="400"></response>

        [HttpGet]
        public ActionResult<CaseRspses> GetAllCases()
        {
            CaseDto[]? caseDtos = _caseService.GetCases();
            CaseRspses caseRspse = new CaseRspses();
            if (caseDtos != null)
            {
                caseRspse.cases = new ResponseObjects.Case[caseDtos.Length];
                Objects.Case caseObject = new Objects.Case();
                for (int i = 0; i < caseDtos.Length; i++)
                {
                    caseObject = _translatorService.TranslateToObject(caseDtos[i]);
                    if (caseObject.PrimaryContact != null)
                    {
                        caseObject.PrimaryContact = _translatorService.TranslateToObject(_operatorService.GetOperatorDto(caseObject.PrimaryContact.Id));
                    }
                    if (caseObject.CreatedBy != null)
                    {
                        caseObject.CreatedBy = _translatorService.TranslateToObject(_operatorService.GetOperatorDto(caseObject.CreatedBy.Id));
                    }
                    caseRspse.cases[i] = _translatorService.TranslateToRspse(caseObject);
                }
            }
            if (caseDtos == null)
            {
                caseRspse.errors = new Error[1];
                caseRspse.errors[0] = new Error()
                {
                    code = "CasesNotFound",
                    category = "NotFound",
                    detail = "No cases found",
                    method = "GET",
                    retryable = false,
                    status = 404,
                    path = "/case/actions/{caseid}"
                };
            }
            return caseRspse;
        }


        /// <summary>
        /// Open new case - Opens a new case and triggers the workflow associated with the case type.
        /// </summary>
        /// <remarks>Open new case - Opens a new case and triggers the workflow associated with the case type.</remarks>
        /// <response code="200"></response>
        /// <response code="400"></response>
        [HttpPut]
        [Route("{caseId}")]
        public ActionResult<ResponseObjects.Case> Update([FromRoute] string caseId, [FromBody] CaseUpdateRqst caseUpdateRqst)
        {
            CaseDto? caseDto = _caseService.GetCase(caseId);
            Objects.Case caseObject = new Objects.Case();
            ResponseObjects.Case caseRspse = new ResponseObjects.Case();
            if (caseDto != null)
            {
                caseObject = _translatorService.TranslateToObject(caseDto);
                if (caseObject.PrimaryContact != null)
                {
                    caseObject.PrimaryContact = _translatorService.TranslateToObject(_operatorService.GetOperatorDto(caseObject.PrimaryContact.Id));
                }
                if (caseObject.CreatedBy != null)
                {
                    caseObject.CreatedBy = _translatorService.TranslateToObject(_operatorService.GetOperatorDto(caseObject.CreatedBy.Id));
                }
                caseRspse = _translatorService.TranslateToRspse(caseObject);
            }
            if (caseDto == null)
            {
                caseRspse.errors = new Error[1];
                caseRspse.errors[0] = new Error()
                {
                    code = "CaseNotFound",
                    category = "NotFound",
                    detail = String.Format("No case found for caseId {0}", caseId),
                    method = "PUT",
                    retryable = false,
                    status = 404,
                    path = "/case/actions/{caseid}"
                };
            }
            return caseRspse;
        }

    }
}
