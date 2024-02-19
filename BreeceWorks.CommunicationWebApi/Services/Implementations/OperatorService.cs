using BreeceWorks.CommunicationWebApi.Services.Interfaces;
using BreeceWorks.Shared.DbContexts;
using BreeceWorks.Shared.Entities;
using Microsoft.EntityFrameworkCore;

namespace BreeceWorks.CommunicationWebApi.Services.Implementations
{
    public class OperatorService : IOperatorService
    {
        private readonly CommunicationDbContext _context;

        public OperatorService(CommunicationDbContext context)
        {
            _context = context ??
                throw new ArgumentNullException(nameof(context));
        }
        public OperatorDto AddOperatorDto(OperatorDto operatorCreateRqst)
        {
            if (operatorCreateRqst == null)
            {
                throw new ArgumentNullException(nameof(operatorCreateRqst));
            }

            if (_context.Operators.Any(u => u.Email == operatorCreateRqst.Email))
            {
                throw new Exception("Email must be unique");
            }

            operatorCreateRqst.Id = Guid.NewGuid();
            if (operatorCreateRqst.Roles != null)
            {
                List<OperatorRoleDto> roles = new List<OperatorRoleDto>();
                foreach (OperatorRoleDto roleDto in operatorCreateRqst.Roles)
                {
                    OperatorRoleDto? operatorRoleDto = GetOperatorRoleDto(roleDto.Role);
                    if (operatorRoleDto == null)
                    {
                        operatorRoleDto = AddOperatorRoleDto(roleDto.Role);
                    }
                    if (operatorRoleDto != null)
                    roles.Add(operatorRoleDto);
                };
                operatorCreateRqst.Roles = roles;
            }
            _context.Operators.Add(operatorCreateRqst);
            _context.SaveChanges();

            operatorCreateRqst = _context.Operators
                .First(u => u.Id == operatorCreateRqst.Id);

            return operatorCreateRqst;
        }

        public bool DeleteOperatorDto(OperatorDto operatorDto)
        {
            if (_context.Cases.Include(c=>c.SecondaryOperators).AsEnumerable().Any(c => (c.PrimaryContact == operatorDto.Id)
                || (c.SecondaryOperators != null && c.SecondaryOperators.Where(s=>s.Id == operatorDto.Id).FirstOrDefault() != null)))
            {
                throw new Exception("Cannot delete assigned operator");
            }
            _context.Operators.Remove(operatorDto);
            _context.SaveChanges();
            return true;
        }

        public OperatorDto UpdateOperatorDto(OperatorDto operatorDto)
        {
            if (_context.Operators.Any(u => u.Email == operatorDto.Email && u.Id != operatorDto.Id))
            {
                throw new Exception("Email must be unique");
            }
            OperatorDto? currentOperator = GetOperatorDto(operatorDto.Id);
            if (currentOperator == null)
            {
                throw new Exception("not found");
            }
            currentOperator.Email = operatorDto.Email;
            currentOperator.First = operatorDto.First;
            currentOperator.Last = operatorDto.Last;
            currentOperator.IdentityProvider = operatorDto.IdentityProvider;
            currentOperator.OfficeNumber = operatorDto.OfficeNumber;    
            currentOperator.PhoneNumber = operatorDto.PhoneNumber;
            currentOperator.Title = operatorDto.Title;
            if (currentOperator.Roles != null)
            {
                currentOperator.Roles.Clear();
                _context.SaveChanges(true);
            }
            if (operatorDto.Roles != null)
            {
                List<OperatorRoleDto> roles = new List<OperatorRoleDto>();
                foreach (OperatorRoleDto roleDto in operatorDto.Roles)
                {
                    OperatorRoleDto? operatorRoleDto = GetOperatorRoleDto(roleDto.Role);
                    if (operatorRoleDto == null)
                    {
                        operatorRoleDto = AddOperatorRoleDto(roleDto.Role);
                    }
                    if (operatorRoleDto != null)
                        roles.Add(operatorRoleDto);
                };
                currentOperator.Roles = roles;
            }
            _context.SaveChanges();

            operatorDto = _context.Operators
                .First(u => u.Id == operatorDto.Id);

            return operatorDto;

        }

        public OperatorRoleDto AddOperatorRoleDto(string role)
        {
            _context.OperatorRoles.Add(new OperatorRoleDto() { Id = Guid.NewGuid(), Role = role });
            _context.SaveChanges();
            return _context.OperatorRoles.Where(r => r.Role == role).First();
        }

        public OperatorDto? GetOperatorDto(string emailOrPhone)
        {
            if (emailOrPhone is null)
            {
                throw new ArgumentNullException(nameof(emailOrPhone));
            }

            if (!_context.Operators.Any(u => u.Email == emailOrPhone) && !_context.Operators.Any(o=>o.PhoneNumber == emailOrPhone))
            {
                return null;
            }

            OperatorDto operatorDto = _context.Operators
                .First(u => u.Email == emailOrPhone || u.PhoneNumber == emailOrPhone);


            return operatorDto;
        }

        public OperatorDto? GetOperatorDto(Guid? id)
        {
            if (id is null)
            {
                return null;
            }

            if (!_context.Operators.Any(u => u.Id == id))
            {
                return null;
            }

            OperatorDto operatorDto = _context.Operators.Include(o=>o.Roles)
                .First(u => u.Id == id);

            return operatorDto;
        }

        public OperatorDto[]? GetOperatorDtos()
        {
            if (_context.Operators.Any())
            {
                return _context.Operators.Include(o => o.Roles).ToArray();
            }
            return null;
        }

        public OperatorRoleDto? GetOperatorRoleDto(string role)
        {
            OperatorRoleDto? operatorRoleDto = _context.OperatorRoles.Where(r => r.Role == role).FirstOrDefault();

            return operatorRoleDto;
        }
        
    }
}
