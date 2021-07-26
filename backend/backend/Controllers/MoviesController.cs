using AutoMapper;
using backend.context;
using backend.Models;
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
        public MoviesController(DatabaseContext context, IMapper mapper)
        {
            _movieService = new MovieService(context, mapper);
        }
        [HttpGet]
        [Route("toprated")]
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
        
        [HttpPost]
        [Route("updateRating/{id}")]
        [Authorize]
        public async Task<IActionResult> UpdateRating([FromBody] RatingDTO ratingRequest)
        {
            try
            {
                return Ok(await _movieService.UpdateMovieRating(ratingRequest));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
