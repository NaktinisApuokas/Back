using AutoMapper;
using FobumCinema.API.Models.Dtos.Auth;
using FobumCinema.API.Models.Dtos.Cinema;
using FobumCinema.API.Models.Dtos.Comment;
using FobumCinema.API.Models.Dtos.CommentRating;
using FobumCinema.API.Models.Dtos.Movie;
using FobumCinema.API.Models.Dtos.MovieMark;
using FobumCinema.API.Models.Dtos.Review;
using FobumCinema.API.Models.Dtos.Screening;
using FobumCinema.Core.Entities;

namespace FobumCinema.Infrastructure.Data
{
    public class FobumCinemaProfile : Profile
    {
        public FobumCinemaProfile()
        {
            CreateMap<Cinema, CinemaDto>();
            CreateMap<CreateCinemaDto, Cinema>();
            CreateMap<UpdateCinemaDto, Cinema>();

            CreateMap<CreateMovieDto, Movie>();
            CreateMap<UpdateMovieDto, Movie>();
            CreateMap<Movie, MovieDto>();
            CreateMap<Movie, GeneralMovieDto>();
 
            CreateMap<CreateScreeningDto, Screening>();
            CreateMap<UpdateScreeningDto, Screening>();
            CreateMap<Screening, ScreeningDto>();


            CreateMap<CreateCommentDto, Comment>();
            CreateMap<UpdateCommentDto, Comment>();
            CreateMap<Comment, CommentDto>();

            CreateMap<CreateReviewDto, Review>();
            CreateMap<UpdateReviewDto, Review>();
            CreateMap<Review, ReviewDto>();


            CreateMap<CreateMovieMarkDto, MovieMark>();
            CreateMap<MovieMark, MovieMarkDto>();

            CreateMap<ManageCommentRatingDto, CommentRating>();
            CreateMap<CommentRating, CommentRatingDto>();

            CreateMap<User, UserDto>();
        }
    }
}