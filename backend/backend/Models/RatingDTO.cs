using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Models
{
    public class RatingDTO
    {
        public int UserRating { get; set; }
        public string Username { get; set; }
        public int MovieId { get; set; }
    }
}
