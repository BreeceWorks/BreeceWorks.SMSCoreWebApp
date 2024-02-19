using BreeceWorks.CommunicationWebApi.Objects;
using BreeceWorks.CommunicationWebApi.RequestObjects;
using BreeceWorks.CommunicationWebApi.ResponseObjects;
using BreeceWorks.CommunicationWebApi.Services.Implementations;
using BreeceWorks.CommunicationWebApi.Services.Interfaces;
using BreeceWorks.Shared.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BreeceWorks.CommunicationWebApi.Controllers
{
    [Tags("User Actions")]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ICustomerService _customerService;
        private readonly ITranslatorService _translatorService;

        public UserController(ICustomerService customerService, ITranslatorService translatorService)
        {
            _customerService = customerService;
            _translatorService = translatorService;
        }



        /// <summary>
        /// Gets the active cases of a user with their email.
        /// </summary>
        /// <remarks>Returns an array of caseIds for currently open cases for a given user.</remarks>
        /// <response code="200"></response>
        /// <response code="400"></response>
        [HttpGet]
        [Route("actions/active-cases/email/{email}")]
        public ActionResult<ActiveCases> GetCasesByEmail([FromRoute] String email)
        {
            try
            {
                ActiveCases caseRspse = new ActiveCases();
                ResponseObjects.Case[]? activeCases = _customerService.GetCustomerActiveCasesByEmail(email);
                if (activeCases != null)
                {
                    caseRspse.Cases = activeCases;
                }
                else
                {
                    caseRspse.errors = new Error[]
                    {
                            new Error()
                            {
                                category = "NotFound",
                                retryable = false,
                                status = 404,
                                detail = "No Active Cases Found For Customer",
                                path = "/user/actions/active-cases/{property}/{value}",
                                method = "GET",
                            }
                    };
                }
                return caseRspse;
            }
            catch (Exception ex)
            {
                return new ActiveCases()
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
                                path = "/user/actions/active-cases/{property}/{value}",
                                method = "GET",
                            }
                    }
                };
            }
        }

        /// <summary>
        /// Gets the active cases of a user with their mobile.
        /// </summary>
        /// <remarks>Returns an array of caseIds for currently open cases for a given user.</remarks>
        /// <response code="200"></response>
        /// <response code="400"></response>
        [HttpGet]
        [Route("actions/active-cases/mobile/{mobile}")]
        public ActionResult<ActiveCases> GetCasesByMobile([FromRoute] String mobile)
        {
            try
            {
                ActiveCases caseRspse = new ActiveCases();
                ResponseObjects.Case[]? activeCases = _customerService.GetCustomerActiveCasesByMobile(mobile);
                if (activeCases != null)
                {
                    caseRspse.Cases = activeCases;
                }
                else
                {
                    caseRspse.errors = new Error[]
                    {
                            new Error()
                            {
                                category = "NotFound",
                                retryable = false,
                                status = 404,
                                detail = "No Active Cases Found For Customer",
                                path = "/user/actions/active-cases/{property}/{value}",
                                method = "GET",
                            }
                    };
                }
                return caseRspse;
            }
            catch (Exception ex)
            {
                return new ActiveCases()
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
                                path = "/user/actions/active-cases/{property}/{value}",
                                method = "GET",
                            }
                    }
                };
            }
        }

        /// <summary>
        /// Gets the active cases of a user with their userId.
        /// </summary>
        /// <remarks>Returns an array of caseIds for currently open cases for a given user.</remarks>
        /// <response code="200"></response>
        /// <response code="400"></response>
        [HttpGet]
        [Route("actions/active-cases/id/{id}")]
        public ActionResult<ActiveCases> GetCasesByID([FromRoute] Guid id)
        {
            try
            {
                ActiveCases caseRspse = new ActiveCases();
                ResponseObjects.Case[]? activeCases = _customerService.GetCustomerActiveCasesByUserId(id);
                if (activeCases != null)
                {
                    caseRspse.Cases = activeCases;
                }
                else
                {
                    caseRspse.errors = new Error[]
                    {
                            new Error()
                            {
                                category = "NotFound",
                                retryable = false,
                                status = 404,
                                detail = "No Active Cases Found For Customer",
                                path = "/user/actions/active-cases/{property}/{value}",
                                method = "GET",
                            }
                    };
                }
                return caseRspse;
            }
            catch (Exception ex)
            {
                return new ActiveCases()
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
                                path = "/user/actions/active-cases/{property}/{value}",
                                method = "GET",
                            }
                    }
                };
            }
        }


        /// <summary>
        /// Get user - By email
        /// </summary>
        /// <remarks>Returns user information based on their email.</remarks>
        /// <response code="200"></response>
        /// <response code="400"></response>
        [HttpGet]
        [Route("email/{email}")]
        public ActionResult<Users> GetUserByEmail([FromRoute] String email)
        {
            try
            {
                Users userRspse = new Users();
                CustomerDto? customerDto = _customerService.GetCustomerDtoByEmail(email);
                if (customerDto != null)
                {
                    Objects.Customer? customer = _translatorService.TranslateToObject(customerDto);
                    ResponseObjects.Customer? customerRspse = _translatorService.TranslateToRspse(customer);
                    userRspse.customers = customerRspse != null ? new ResponseObjects.Customer[] { customerRspse } : null;
                }
                else
                {
                    userRspse.errors = new Error[]
                    {
                            new Error()
                            {
                                category = "NotFound",
                                retryable = false,
                                status = 404,
                                detail = "Customer Not Found",
                                path = "/user/{property}/{value}",
                                method = "GET",
                            }
                    };
                }
                return userRspse;
            }
            catch (Exception ex)
            {
                return new Users()
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
                                path = "/user/{property}/{value}",
                                method = "GET",
                            }
                    }
                };
            }
        }

        /// <summary>
        /// Get user - By mobile
        /// </summary>
        /// <remarks>Returns user information based on their mobile.</remarks>
        /// <response code="200"></response>
        /// <response code="400"></response>
        [HttpGet]
        [Route("mobile/{mobile}")]
        public ActionResult<Users> GetUserByMobile([FromRoute] String mobile)
        {
            try
            {
                Users userRspse = new Users();
                CustomerDto? customerDto = _customerService.GetCustomerDtoByMobile(mobile);
                if (customerDto != null)
                {
                    Objects.Customer? customer = _translatorService.TranslateToObject(customerDto);
                    ResponseObjects.Customer? customerRspse = _translatorService.TranslateToRspse(customer);
                    userRspse.customers = customerRspse != null ? new ResponseObjects.Customer[] { customerRspse } : null;
                }
                else
                {
                    userRspse.errors = new Error[]
                    {
                            new Error()
                            {
                                category = "NotFound",
                                retryable = false,
                                status = 404,
                                detail = "Customer Not Found",
                                path = "/user/{property}/{value}",
                                method = "GET",
                            }
                    };
                }
                return userRspse;
            }
            catch (Exception ex)
            {
                return new Users()
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
                                path = "/user/{property}/{value}",
                                method = "GET",
                            }
                    }
                };
            }
        }

        /// <summary>
        /// Get user - By userId
        /// </summary>
        /// <remarks>Returns user information based on their userId.</remarks>
        /// <response code="200"></response>
        /// <response code="400"></response>
        [HttpGet]
        [Route("id/{id}")]
        public ActionResult<Users> GetUserById([FromRoute] Guid id)
        {
            try
            {
                Users userRspse = new Users();
                CustomerDto? customerDto = _customerService.GetCustomerDtoById(id);
                if (customerDto != null)
                {
                    Objects.Customer? customer = _translatorService.TranslateToObject(customerDto);
                    ResponseObjects.Customer? customerRspse = _translatorService.TranslateToRspse(customer);
                    userRspse.customers = customerRspse != null ? new ResponseObjects.Customer[] { customerRspse } : null;
                }
                else
                {
                    userRspse.errors = new Error[]
                    {
                            new Error()
                            {
                                category = "NotFound",
                                retryable = false,
                                status = 404,
                                detail = "Customer Not Found",
                                path = "/user/{property}/{value}",
                                method = "GET",
                            }
                    };
                }
                return userRspse;
            }
            catch (Exception ex)
            {
                return new Users()
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
                                path = "/user/{property}/{value}",
                                method = "GET",
                            }
                    }
                };
            }
        }

        /// <summary>
        /// Get user - all
        /// </summary>
        /// <remarks>Returns user information for all customers.</remarks>
        /// <response code="200"></response>
        /// <response code="400"></response>
        [HttpGet]
        public ActionResult<Users> GetUsers()
        {
            try
            {
                Users userRspse = new Users();
                CustomerDto[]? customerDtos = _customerService.GetCustomerDtos();
                if (customerDtos != null)
                {
                    userRspse.customers = new ResponseObjects.Customer[customerDtos.Length];
                    for (int i = 0; i < customerDtos.Length; i++)
                    {
                        Objects.Customer? customerObject = _translatorService.TranslateToObject(customerDtos[i]);
                        ResponseObjects.Customer? customerRspse1 = _translatorService.TranslateToRspse(customerObject);
                        if (customerRspse1 != null)
                        {
                            userRspse.customers[i] = customerRspse1;
                        }
                    }
                }
                else
                {
                    userRspse.errors = new Error[]
                    {
                            new Error()
                            {
                                category = "NotFound",
                                retryable = false,
                                status = 404,
                                detail = "Users Not Found",
                                path = "/user/{property}/{value}",
                                method = "GET",
                            }
                    };
                }
                return userRspse;
            }
            catch (Exception ex)
            {
                return new Users()
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
                                path = "/user/{property}/{value}",
                                method = "GET",
                            }
                    }
                };
            }
        }

    }
}
