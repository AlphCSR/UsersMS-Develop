using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UsersMS.Commons.Dtos.Request;

namespace UsersMS.Application.Validator
{
    public class CreateTechnicalSupportValidator : ValidatorBase<CreateTechnicalSupportDto>
    {
        public CreateTechnicalSupportValidator()
        {
            RuleFor(s => s.Email).NotNull().WithMessage("Email no puede ser nulo").WithErrorCode("010");
            RuleFor(s => s.Email).EmailAddress().WithMessage("Email debe ser un correo electronico valido").WithErrorCode("011");
            RuleFor(s => s.Email).NotEmpty().WithMessage("Email no puede estar vacío").WithErrorCode("012");
            RuleFor(s => s.Email).MaximumLength(100).WithMessage("Email no puede tener más de 100 caracteres").WithErrorCode("013");
            RuleFor(s => s.Password).NotNull().WithMessage("Password no puede ser nulo").WithErrorCode("020");
            RuleFor(s => s.Password).MinimumLength(6).WithMessage("Password debe tener al menos 6 caracteres").WithErrorCode("021");
            RuleFor(s => s.Password).Matches(@"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{8,}$").WithMessage("Password debe tener al menos 8 caracteres, incluyendo una letra y un número").WithErrorCode("022");
            RuleFor(s => s.Name).NotNull().WithMessage("Name no puede ser nulo").WithErrorCode("040");
            RuleFor(s => s.LastName).NotNull().WithMessage("Apellido no puede ser nulo").WithErrorCode("050");
            RuleFor(s => s.Id).NotNull().WithMessage("Cedula no puede ser nula").WithErrorCode("030");
            RuleFor(s => s.Role).NotNull().WithMessage("Rol no puede ser nulo").WithErrorCode("060");
            RuleFor(s => s.Phone).NotNull().WithMessage("Telefono no puede ser nulo").WithErrorCode("080");
            RuleFor(s => s.Phone).NotEmpty().WithMessage("Telefono no puede estar vacío").WithErrorCode("081");
            RuleFor(s => s.Address).NotNull().WithMessage("Direccion no puede ser nulo").WithErrorCode("090");
            RuleFor(s => s.State).NotNull().WithMessage("State no puede ser nulo").WithErrorCode("070");

        }
    }
}
