using AutoMapper;
using backend.context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Models
{
    public class MapperConfig
    {
        public MapperConfig()
        {

        }

        public class MovieProfile : Profile
        {
            public MovieProfile()
            {
                CreateMap<Movie, MovieDTO>();
                CreateMap<MovieDTO, Movie>();
            }
        }

    }
}
