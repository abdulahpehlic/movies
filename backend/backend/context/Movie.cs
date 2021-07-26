using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.context
{
    public class Movie
    {
        public long Id { get; set; }
        public decimal? Rating { get; set; }
        public decimal? RatingCount { get; set; }
        public int ReleaseYear { get; set; }
        public string Title { get; set; }
        public string Duration { get; set; }
    }
}
