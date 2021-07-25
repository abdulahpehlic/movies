using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.context
{
    public class MovieContext : DbContext
    {
        private IConfiguration _configuration { get; }
        public MovieContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public DbSet<User> User { get; set; }
        public DbSet<Movie> Movie { get; set; }
        public DbSet<UserRating> UserRating { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL(_configuration.GetConnectionString("DefaultConnection"));
        }
    }
}
