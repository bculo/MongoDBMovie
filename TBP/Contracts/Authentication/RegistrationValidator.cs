using FluentValidation;

namespace TBP.Contracts.Authentication
{
    public class RegistrationValidator : AbstractValidator<AuthRegistrationRequestModel>
    {
        public RegistrationValidator()
        {
            RuleFor(x => x.UserName).NotEmpty();
            RuleFor(x => x.Password).NotEmpty();
            RuleFor(x => x.Email).NotEmpty();
        }
    }
}
