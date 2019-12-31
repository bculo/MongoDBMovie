using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using TBP.Constants;
using TBP.Contracts.Movie;
using TBP.Interfaces;

namespace TBP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class MovieController : ControllerBase
    {
        private readonly IMovieService _service;
        private readonly IMapper _mapper;

        public MovieController(IMovieService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpGet("list")]
        public async Task<IActionResult> GetAllMovies()
        {
            return Ok(_mapper.Map<List<MovieResponseModel>>(await _service.GetAllMovies()));
        }

        [HttpPost("moviepage")]
        public async Task<IActionResult> GetMoviePage([FromBody] MovieTitleRequestModel model)
        {
            return Ok(_mapper.Map<List<MovieResponseModel>>(await _service.SearchMovie(model.Title, model.Page)));
        }

        [HttpPost("characters")]
        public async Task<IActionResult> GetMovieCharactes([FromBody] MovieRequestModel request)
        {
            return Ok(_mapper.Map<List<CharacterResponseModel>>(await _service.GetMovieCharacters(request.MovieId)));
        }

        [HttpPost("genres")]
        public async Task<IActionResult> GetMovieGenres([FromBody] MovieRequestModel request)
        {
            return Ok(_mapper.Map<List<GenreResponseModel>>(await _service.GetMovieGenres(request.MovieId)));
        }

        [HttpPost("movieforgenre")]
        public async Task<IActionResult> GetMovieForGenre([FromBody] MovieRequestModel request)
        {
            return Ok(_mapper.Map<List<MovieResponseModel>>(await _service.GetAllMoviesForGenre(request.MovieId)));
        }

        [HttpPost("adminnewmovies")]
        [Authorize(Roles = RoleConstants.Admin)]
        public async Task<IActionResult> GetNewMovies([FromBody] MovieTitleRequestModel model)
        {
            return Ok(await _service.GetNewMovies(model.Page));
        }

        [HttpPost("adminaddmovie")]
        [Authorize(Roles = RoleConstants.Admin)]
        public async Task<IActionResult> AddNewMovie([FromBody] NewMovieRequestModel model)
        {
            var result = await _service.AddMovie(model.IMDBId);
            if (result.Success)
                return Ok();
            return BadRequest();
        }

        [HttpPost("admindeletemovie")]
        [Authorize(Roles = RoleConstants.Admin)]
        public async Task<IActionResult> DeleteMovie([FromBody] NewMovieRequestModel model)
        {
            var result = await _service.DeleteMovie(model.IMDBId);
            if (result.Success)
                return Ok();
            return BadRequest();
        }
    }
}