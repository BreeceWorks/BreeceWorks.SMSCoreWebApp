using BreeceWorks.CommunicationWebApi.Objects;
using BreeceWorks.CommunicationWebApi.ResponseObjects;
using BreeceWorks.CommunicationWebApi.Services.Interfaces;
using BreeceWorks.Shared.DbContexts;
using BreeceWorks.Shared.Entities;
using BreeceWorks.Shared.Enums;
using Microsoft.EntityFrameworkCore;
using System.CodeDom;

namespace BreeceWorks.CommunicationWebApi.Services.Implementations
{
    public class CustomerService:ICustomerService
    {
        private readonly CommunicationDbContext _context;
        private readonly IOperatorService _operatorService;
        private readonly ITranslatorService _translatorService;


        public CustomerService(CommunicationDbContext context, ITranslatorService translatorService, IOperatorService operatorService)
        {
            _context = context;
            _translatorService = translatorService;
            _operatorService = operatorService;
        }
        public CustomerDto AddCustomerDto(CustomerDto customer)
        {

            customer.Id = Guid.NewGuid();
            
            _context.Customers.Add(customer);
            _context.SaveChanges();

            customer = _context.Customers
                .First(u => u.Id == customer.Id);

            return  customer;
        }

        public CustomerDto? UpdateCustomerDto(CustomerDto customerDto)
        {
            CustomerDto? customer = _context.Customers
                .FirstOrDefault(u => u.Id == customerDto.Id);
            if (customer == null)
            {
                return null;
            }
            customer.Id = customerDto.Id;
            customer.First = customerDto.First;
            customer.Last = customerDto.Last;   
            customer.Email = customerDto.Email;
            customer.Mobile = customerDto.Mobile;
            customer.OptStatus = customerDto.OptStatus;
            customer.OptStatusDetail = customerDto.OptStatusDetail;
            _context.Customers.Update(customer);
            _context.SaveChanges();
            return customer;
        }

        public ResponseObjects.Case[]? GetCustomerActiveCasesByEmail(string email)
        {
            CaseDto[]? activeCasesDto = null;
            ResponseObjects.Case[]? activeCases = null;
            if (_context.Cases.Where(c => c.Customer.Email == email && c.State == CaseState.open).Any())
            {
                activeCasesDto = _context
                    .Cases.Include(c => c.Customer).Include(c => c.SecondaryOperators).Include(c => c.LineOfBusiness).Where(c => c.Customer.Email == email && c.State == CaseState.open).ToArray();
                activeCases = new ResponseObjects.Case[activeCasesDto.Length];
                for (int i = 0; i < activeCasesDto.Length; i++)
                {
                    Objects.Case caseObject = _translatorService.TranslateToObject(activeCasesDto[i]);
                    if (caseObject.PrimaryContact != null)
                    {
                        caseObject.PrimaryContact = _translatorService.TranslateToObject(_operatorService.GetOperatorDto(caseObject.PrimaryContact.Id));
                    }
                    if (caseObject.CreatedBy != null)
                    {
                        caseObject.CreatedBy = _translatorService.TranslateToObject(_operatorService.GetOperatorDto(caseObject.CreatedBy.Id));
                    }
                    activeCases[i] = _translatorService.TranslateToRspse(caseObject);
                }
            }
            return activeCases;
        }

        public ResponseObjects.Case[]? GetCustomerActiveCasesByMobile(string mobile)
        {
            CaseDto[]? activeCasesDto = null;
            ResponseObjects.Case[]? activeCases = null;
            if (_context.Cases.Where(c => c.Customer.Mobile == mobile && c.State == CaseState.open).Any())
            {
                activeCasesDto = _context
                    .Cases.Include(c => c.Customer).Include(c => c.SecondaryOperators).Include(c => c.LineOfBusiness).Where(c => c.Customer.Mobile == mobile && c.State == CaseState.open).ToArray();
                activeCases = new ResponseObjects.Case[activeCasesDto.Length];
                for (int i = 0; i < activeCasesDto.Length; i++)
                {
                    Objects.Case caseObject = _translatorService.TranslateToObject(activeCasesDto[i]);
                    if (caseObject.PrimaryContact != null)
                    {
                        caseObject.PrimaryContact = _translatorService.TranslateToObject(_operatorService.GetOperatorDto(caseObject.PrimaryContact.Id));
                    }
                    if (caseObject.CreatedBy != null)
                    {
                        caseObject.CreatedBy = _translatorService.TranslateToObject(_operatorService.GetOperatorDto(caseObject.CreatedBy.Id));
                    }
                    activeCases[i] = _translatorService.TranslateToRspse(caseObject);
                }
            }
            return activeCases;
        }

        public ResponseObjects.Case[]? GetCustomerActiveCasesByUserId(Guid userId)
        {
            CaseDto[]? activeCasesDto = null;
            ResponseObjects.Case[]? activeCases = null;
            if (_context.Cases.Where(c => c.Customer.Id == userId && c.State == CaseState.open).Any())
            {
                activeCasesDto = _context
                    .Cases.Include(c => c.Customer).Include(c => c.SecondaryOperators).Include(c => c.LineOfBusiness).Where(c => c.Customer.Id == userId && c.State == CaseState.open).ToArray();
                activeCases = new ResponseObjects.Case[activeCasesDto.Length];
                for (int i = 0; i < activeCasesDto.Length; i++)
                {
                    Objects.Case caseObject = _translatorService.TranslateToObject(activeCasesDto[i]);
                    if (caseObject.PrimaryContact != null)
                    {
                        caseObject.PrimaryContact = _translatorService.TranslateToObject(_operatorService.GetOperatorDto(caseObject.PrimaryContact.Id));
                    }
                    if (caseObject.CreatedBy != null)
                    {
                        caseObject.CreatedBy = _translatorService.TranslateToObject(_operatorService.GetOperatorDto(caseObject.CreatedBy.Id));
                    }
                    activeCases[i] = _translatorService.TranslateToRspse(caseObject);
                }
            }
            return activeCases;
        }

        public CustomerDto? GetCustomerDtoByEmail(string email)
        {
            CustomerDto? customerDto = _context.Customers
                .FirstOrDefault(u => u.Email == email);
            if (customerDto == null)
            {
                return null;
            }
            return customerDto;
        }

        public CustomerDto? GetCustomerDtoById(Guid customerId)
        {
            CustomerDto? customerDto = _context.Customers
                .FirstOrDefault(u => u.Id == customerId);
            if (customerDto == null)
            {
                return null;
            }
            return customerDto;
        }

        public CustomerDto[]? GetCustomerDtos()
        {
            CustomerDto? customerDto = _context.Customers
                .FirstOrDefault();
            if (customerDto == null)
            {
                return null;
            }
            return _context.Customers.ToArray(); ;
        }

        public CustomerDto? GetCustomerDtoByMobile(string mobile)
        {

            CustomerDto? customerDto = _context.Customers
                .FirstOrDefault(u => u.Mobile == mobile);
            if (customerDto == null)
            {
                return null;
            }
            return customerDto;
        }

        
    }
}
