using AutoMapper;
using MyBookList.Models;
using MyBookList.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyBookList.App_Start
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            Mapper.CreateMap<BookIndexViewModel, Book>();
            Mapper.CreateMap<Book, BookIndexViewModel>();
            Mapper.CreateMap<BookDetailsViewModel, Book>();
            Mapper.CreateMap<Book, BookDetailsViewModel>();
            Mapper.CreateMap<BookFormViewModel, Book>();
            Mapper.CreateMap<Book, BookFormViewModel>();
            
            Mapper.CreateMap<MovieIndexViewModel, Movie>();
            Mapper.CreateMap<Movie, MovieIndexViewModel>();
            Mapper.CreateMap<MovieDetailsViewModel, Movie>();
            Mapper.CreateMap<Movie, MovieDetailsViewModel>();
            Mapper.CreateMap<MovieFormViewModel, Movie>();
            Mapper.CreateMap<Movie, MovieFormViewModel>();
            
            Mapper.CreateMap<GameIndexViewModel, Game>();
            Mapper.CreateMap<Game, GameIndexViewModel>();
            Mapper.CreateMap<GamesDetailsViewModel, Game>();
            Mapper.CreateMap<Game, GamesDetailsViewModel>();
            Mapper.CreateMap<GameFormViewModel, Game>();
            Mapper.CreateMap<Game, GameFormViewModel>();
            
            Mapper.CreateMap<SeriesIndexViewModel, Series>();
            Mapper.CreateMap<Series,SeriesIndexViewModel>();
            Mapper.CreateMap<SeriesDetailsViewModel, Series>();
            Mapper.CreateMap<Series, SeriesDetailsViewModel>();
            Mapper.CreateMap<SeriesFormViewModel, Series>();
            Mapper.CreateMap<Series, SeriesFormViewModel>();
        }
    }
}