using AutoMapper;
using MyHobbyList.Models;
using MyHobbyList.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyHobbyList.App_Start
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            Mapper.CreateMap<BookIndexViewModel, Book>();//.ForMember(x => x.Comments, opt => opt.Ignore());
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
            Mapper.CreateMap<GameDetailsViewModel, Game>();
            Mapper.CreateMap<Game, GameDetailsViewModel>();
            Mapper.CreateMap<GameFormViewModel, Game>();
            Mapper.CreateMap<Game, GameFormViewModel>();

            Mapper.CreateMap<Comment, CommentViewModel>();
        }
    }
}