

using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Application.CQRS.Users.Handlers.Register;

namespace Application.CQRS.Users.Validations
{
    public class RegisterUserValidator:AbstractValidator<RegisterCommand>
    {
        public RegisterUserValidator()
        {
            RuleFor(u => u.Email).EmailAddress();
        }
    }
}
