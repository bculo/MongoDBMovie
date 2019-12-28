using FluentValidation;

namespace TBP.Contracts.Movie
{
    public class ActorValidator : AbstractValidator<MoviePaginationRequestModel>
    {
        public ActorValidator()
        {
            RuleFor(x => x.MovieId).NotEmpty();
            RuleFor(x => x.Page).GreaterThan(0);
        }
    }
}
