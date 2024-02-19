using BreeceWorks.CommunicationWebApi.RequestObjects;
using BreeceWorks.Shared.Entities;

namespace BreeceWorks.CommunicationWebApi.Services.Interfaces
{
    public interface IOperatorService
    {
        public OperatorDto AddOperatorDto(OperatorDto operatorCreateRqst);
        public OperatorDto? GetOperatorDto(string emailOrPhone);
        public OperatorDto? GetOperatorDto(Guid? id);
        public Boolean DeleteOperatorDto(OperatorDto operatorDto);
        public OperatorRoleDto AddOperatorRoleDto(string role);
        public OperatorRoleDto? GetOperatorRoleDto(string role);
        public OperatorDto[]? GetOperatorDtos();
        public OperatorDto UpdateOperatorDto(OperatorDto operatorDto);
    }
}
