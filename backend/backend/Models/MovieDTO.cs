﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Models
{
    public class MovieDTO
    {
        public decimal Id { get; set; }
        public decimal? Rating { get; set; }
        public decimal? RatingCount { get; set; }
        public string Title { get; set; }
    }
}
