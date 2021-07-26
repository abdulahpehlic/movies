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
        readonly DatabaseContext _context;
        readonly IMapper _mapper;
        public MovieService(DatabaseContext context, IMapper mapper)
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

        public async Task<MovieDTO> UpdateMovieRating(RatingDTO rating)
        {
            var movieId = rating.MovieId;
            var movie = await _context.Movie.FirstOrDefaultAsync(x => x.Id == movieId);
            movie.Rating = (movie.Rating * movie.RatingCount + rating.UserRating) / (movie.RatingCount + 1);
            movie.RatingCount++;
            var user = await _context.User.FirstOrDefaultAsync(x => x.Username == rating.Username);
            UserRating userRating = new UserRating()
            {
                Movie = movie,
                User = user,
                Rating = rating.UserRating
            };
            var exitingUserRating = await _context.UserRating.FirstOrDefaultAsync(x => x.Movie.Id == movieId && x.User.Id == user.Id);
            if (exitingUserRating != null) {
                movie.Rating = (movie.Rating * movie.RatingCount - exitingUserRating.Rating +rating.UserRating) / (movie.RatingCount);
                movie.RatingCount--;
                _context.UserRating.Remove(exitingUserRating);
            }

            _context.UserRating.Add(userRating);
            _context.SaveChanges();
            return _mapper.Map<MovieDTO>(movie);
            
        }
    }
}
