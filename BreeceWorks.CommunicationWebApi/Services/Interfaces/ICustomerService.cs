using BreeceWorks.CommunicationWebApi.ResponseObjects;
using BreeceWorks.Shared.Entities;

namespace BreeceWorks.CommunicationWebApi.Services.Interfaces
{
    public interface ICustomerService
    {
        public CustomerDto AddCustomerDto(CustomerDto customer);
        public CustomerDto? GetCustomerDtoByMobile(string mobile);
        public CustomerDto? GetCustomerDtoByEmail(string email);
        public CustomerDto? GetCustomerDtoById(Guid customerId);
        public CustomerDto[]? GetCustomerDtos();
        public Case[]? GetCustomerActiveCasesByEmail(string email);
        public Case[]? GetCustomerActiveCasesByMobile(string mobile);
        public Case[]? GetCustomerActiveCasesByUserId(Guid userId);
        public CustomerDto? UpdateCustomerDto(CustomerDto customerDto);
    }
}
