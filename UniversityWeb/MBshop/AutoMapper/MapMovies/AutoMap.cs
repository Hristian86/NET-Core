using System.Collections.Generic;
using AutoMapper;
using MBshop.Models;
using MBshop.Models.ViewBooks;
using MBshop.Models.ViewMovies;

namespace MBshop.AutoMapper.MapMovies
{
    public class AutoMap : Profile
    {
        public AutoMap()
        {
            CreateMap<Movies, OutPutViewMovies>().ReverseMap();

            CreateMap<Books, OutPutViewBooks>().ReverseMap();
        }
    }
}
