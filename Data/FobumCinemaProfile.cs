using AutoMapper;
using FobumCinema.Data.Dtos.Auth;
using FobumCinema.Data.Dtos.Cinema;
using FobumCinema.Data.Dtos.Comment;
using FobumCinema.Data.Dtos.CommentRating;
using FobumCinema.Data.Dtos.Movie;
using FobumCinema.Data.Dtos.MovieMark;
using FobumCinema.Data.Dtos.Review;
using FobumCinema.Data.Dtos.Screening;
using FobumCinema.Data.Entities;

namespace FobumCinema.Data
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

            CreateMap<FobumCinemaUser, UserDto>();
        }
    }
}