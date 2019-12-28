using FluentValidation;

namespace TBP.Contracts.Movie
{
    public class GenreValidator : AbstractValidator<MovieRequestModel>
    {
        public GenreValidator()
        {
            RuleFor(x => x.MovieId).NotEmpty();
        }
    }
}
