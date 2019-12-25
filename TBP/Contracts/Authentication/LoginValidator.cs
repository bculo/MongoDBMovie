using FluentValidation;

namespace TBP.Contracts.Authentication
{
    public class LoginValidator : AbstractValidator<AuthLoginRequestModel>
    {
        public LoginValidator()
        {
            RuleFor(x => x.UserName).NotEmpty();
            RuleFor(x => x.Password).NotEmpty();
        }
    }
}
