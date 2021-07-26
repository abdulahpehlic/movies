using AutoMapper;
using backend.context;
using backend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Controllers
{
    [Route("api/movies")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        readonly MovieService _movieService;
        public MoviesController(MovieContext context, IMapper mapper)
        {
            _movieService = new MovieService(context, mapper);
        }
        [HttpGet]
        [Route("toprated")]
        [Authorize]
        public async Task<IActionResult> GetTopRated()
        {
            try
            {
                return Ok(await _movieService.GetTopRatedMovies());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        
        [HttpGet]
        [Route("updateRating/{id}/{rating}")]
        public async Task<IActionResult> UpdateRating(decimal id, decimal rating)
        {
            try
            {
                return Ok(await _movieService.UpdateMovieRating(id, rating));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
