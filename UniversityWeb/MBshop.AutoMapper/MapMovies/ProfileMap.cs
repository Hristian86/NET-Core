using System;
using AutoMapper;
using MBshop.Models;
using MBshop.ViewModels;

namespace MBshop.AutoMapper.MapMovies
{
    public class ProfileMap : Profile
    {
        public ProfileMap()
        {
            CreateMap<Movies, OutputMoviesViewModel>();
        }
    }
}
