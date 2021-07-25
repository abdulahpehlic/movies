using AutoMapper;
using backend.context;
using backend.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Services
{
    public class MovieService
    {
        readonly MovieContext _context;
        readonly IMapper _mapper;
        public MovieService(MovieContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<MovieDTO>> GetTopRatedMovies()
        {
            //var movies = _context.Movie.Where(x => x.Title.ToLower().Contains())
            var movies = await _context.Movie.ToListAsync();
            movies = movies.OrderByDescending(x => x.Rating).ToList();
            return _mapper.Map<List<MovieDTO>>(movies.Take(10));
        }

        public async Task<MovieDTO> UpdateMovieRating(decimal id, decimal rating)
        {
            var movie = await _context.Movie.FirstOrDefaultAsync(x => x.Id == id);
            movie.Rating = (movie.Rating * movie.RatingCount + rating) / (movie.RatingCount + 1);
            movie.RatingCount++;
            //UserRating rating = new UserRating()
            //{
            //    Id = 0,
            //    Movie = movie
            //    User = 
            //};
            //_context.UserRating.Add(rating);
            _context.SaveChanges();
            return _mapper.Map<MovieDTO>(movie);
            
        }
    }
}
