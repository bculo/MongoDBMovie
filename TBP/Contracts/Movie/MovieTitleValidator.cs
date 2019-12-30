using FluentValidation;

namespace TBP.Contracts.Movie
{
    public class MovieTitleValidator : AbstractValidator<MovieTitleRequestModel>
    {
        public MovieTitleValidator()
        {
            RuleFor(x => x.Title).NotNull();
            RuleFor(x => x.Page).GreaterThan(0);
        }
    }
}
