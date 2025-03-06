

using FluentValidation;
using static Application.CQRS.Users.Handlers.Register;

namespace Application.CQRS.Users.Validations
{
    public class RegisterUserValidator:AbstractValidator<RegisterCommand>
    {

        //validationlari oyren
        //user in diger crudlari ve bookun diger crudlari yazilmalidir
        //backgroundserver mail gondersin
        //requuest rate limit Middleware
        //Postman bax
        //JWT bax ve login yaz



        public RegisterUserValidator()
        {
       
            RuleFor(u => u.Email).EmailAddress();
            RuleFor(u => u.Name).NotEmpty().MinimumLength(3).NotNull();
            RuleFor(u => u.Surname).NotEmpty().MinimumLength(3).NotNull();
            RuleFor(u => u.CardNumber).NotEmpty().MinimumLength(16).NotNull().MaximumLength(16).CreditCard();
            RuleFor(u => u.PasswordHash).NotEmpty().MinimumLength(4);
            RuleFor(u => u.MobilePhone).NotEmpty().NotNull().Must(phone => phone.StartsWith("+994")).WithMessage("Nomre +994 ile baslamalidir");
        }
    }
}



