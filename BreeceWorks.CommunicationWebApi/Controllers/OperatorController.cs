using BreeceWorks.CommunicationWebApi.Objects;
using BreeceWorks.CommunicationWebApi.RequestObjects;
using BreeceWorks.CommunicationWebApi.ResponseObjects;
using BreeceWorks.CommunicationWebApi.Services.Interfaces;
using BreeceWorks.Shared.Entities;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BreeceWorks.CommunicationWebApi.Controllers
{
    [Tags("Operator Actions")]
    [Route("api/operators")]
    [ApiController]
    public class OperatorController : ControllerBase
    {
        private readonly IOperatorService _operatorService;
        private readonly ITranslatorService _translatorService;

        public OperatorController(IOperatorService operatorService, ITranslatorService translatorService)
        {
            _operatorService = operatorService ??
                throw new ArgumentNullException(nameof(operatorService));
            _translatorService = translatorService;
        }



        /// <summary>
        /// Create operator
        /// </summary>
        /// <remarks>Create operator</remarks>
        /// <response code="200"></response>
        /// <response code="400"></response>

        [HttpPost]
        public ActionResult<ResponseObjects.Operator> Post([FromBody] OperatorCreateRqst operatorCreateRqst)
        {
            try
            {
                Objects.Operator operatorObject = _translatorService.TranslateToObject(operatorCreateRqst);


                operatorObject = _translatorService.TranslateToObject(_operatorService.AddOperatorDto(_translatorService.TranslateToDto(operatorObject)));

                return _translatorService.TranslateToRspse(operatorObject);
            }
            catch (Exception ex)
            {
                return new ResponseObjects.Operator()
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
                                    path = "/api/operators",
                                    method = "POST",
                                }
                        }
                };
            }
        }




        /// <summary>
        /// Get operator
        /// </summary>
        /// <remarks>Get operator</remarks>
        /// <response code="200"></response>
        /// <response code="400"></response>

        [HttpGet]
        public ActionResult<ResponseObjects.Operator> Get([FromHeader] string email)
        {
            try
            {
                Objects.Operator? operatorObject = _translatorService.TranslateToObject(_operatorService.GetOperatorDto(email));

                return _translatorService.TranslateToRspse(operatorObject);
            }
            catch (Exception ex)
            {
                if (ex.Message == "not found")
                {
                    return NotFound();
                }
                else
                {
                    return new ResponseObjects.Operator()
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
                                    path = "/api/operators",
                                    method = "GET",
                                }
                        }
                    };
                }
            }
        }

        /// <summary>
        /// Get operators
        /// </summary>
        /// <remarks>Get operators</remarks>
        /// <response code="200"></response>
        /// <response code="400"></response>

        [HttpGet]
        [Route("all")]
        public ActionResult<Operators> GetOperators()
        {
            Operators operatorRspses = new Operators();
            try
            {
                OperatorDto[]? operatorDtos = _operatorService.GetOperatorDtos();
                if (operatorDtos != null)
                {
                    operatorRspses.operators = new ResponseObjects.Operator[operatorDtos.Length];
                    for (int i = 0; i < operatorDtos.Length; i++)
                    {
                        Objects.Operator? operatorObject = _translatorService.TranslateToObject(operatorDtos[i]);
                        if (operatorObject != null)
                        {
                            ResponseObjects.Operator? operatorRspse = _translatorService.TranslateToRspse(operatorObject);
                            if (operatorRspse != null)
                            {
                                operatorRspses.operators[i] = operatorRspse;
                            }
                        }
                    }
                }
                return operatorRspses;
            }
            catch (Exception ex)
            {
                if (ex.Message == "not found")
                {
                    return NotFound();
                }
                else
                {
                    return new ResponseObjects.Operators()
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
                                    path = "/api/operators/all",
                                    method = "GET",
                                }
                        }
                    };
                }
            }
        }

        /// <summary>
        /// Update operator
        /// </summary>
        /// <remarks>Update operator</remarks>
        /// <response code="200"></response>
        /// <response code="400"></response>

        [HttpPatch]
        [Route("{id}")]
        public ActionResult<ResponseObjects.Operator?> UpdateWithIDFromAQuery([FromRoute] Guid id, [FromBody] OperatorUpdateRqst operatorUpdateRqst)
        {
            try
            {
                Objects.Operator? operatorObject = _translatorService.TranslateToObject(operatorUpdateRqst);

                operatorObject.Id = id;

                operatorObject = _translatorService.TranslateToObject(_operatorService.UpdateOperatorDto(_translatorService.TranslateToDto(operatorObject)));

                return _translatorService.TranslateToRspse(operatorObject);
            }
            catch (Exception ex)
            {
                if (ex.Message == "not found")
                {
                    return NotFound();
                }
                else
                {
                    return new ResponseObjects.Operator()
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
                                    path = "/api/operators/{id}",
                                    method = "PATCH",
                                }
                        }
                    };
                }
            }
        }


        /// <summary>
        /// Delete operator
        /// </summary>
        /// <remarks>Delete operator</remarks>
        /// <response code="200"></response>
        /// <response code="400"></response>

        [HttpDelete]
        [Route("{id}")]
        public ActionResult<ResponseObjects.Operator?> DeleteWithIDFromAQuery([FromRoute] Guid id)
        {
            try
            {
                OperatorDto? operatorDto = _operatorService.GetOperatorDto(id);
                if (operatorDto == null)
                {
                    throw new Exception("not found");
                }

                _operatorService.DeleteOperatorDto(operatorDto);
                Objects.Operator? operatorObject = _translatorService.TranslateToObject(operatorDto);
                return _translatorService.TranslateToRspse(operatorObject);
            }
            catch (Exception ex)
            {
                if (ex.Message == "not found")
                {
                    return NotFound();
                }
                else
                {
                    return new ResponseObjects.Operator()
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
                                    path = "/api/operators/{id}",
                                    method = "DELETE",
                                }
                        }
                    };
                }
            }
        }


        /// <summary>
        /// Get operator
        /// </summary>
        /// <remarks>Get operator</remarks>
        /// <response code="200"></response>
        /// <response code="400"></response>

        [HttpGet]
        [Route("{id}")]
        public ActionResult<ResponseObjects.Operator> GetWithIDFromAQuery([FromRoute] Guid id)
        {
            try
            {
                Objects.Operator? operatorObject = _translatorService.TranslateToObject(_operatorService.GetOperatorDto(id));

                return _translatorService.TranslateToRspse(operatorObject);
            }
            catch (Exception ex)
            {
                if (ex.Message == "not found")
                {
                    return NotFound();
                }
                else
                {
                    return new ResponseObjects.Operator()
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
                                    path = "/api/operators/{id}",
                                    method = "GET",
                                }
                        }
                    };
                }
            }
        }

    }
}
