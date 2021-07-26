using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Models
{
    public class MovieDTO
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        public decimal? Rating { get; set; }
        public decimal? RatingCount { get; set; }
        public string Title { get; set; }
        public int ReleaseYear { get; set; }
        public string Duration { get; set; }
    }
}
