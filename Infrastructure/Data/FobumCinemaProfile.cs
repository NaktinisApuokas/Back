using AutoMapper;
using FobumCinema.API.Models.Dtos.Auth;
using FobumCinema.API.Models.Dtos.Cinema;
using FobumCinema.API.Models.Dtos.CinemaCompany;
using FobumCinema.API.Models.Dtos.CinemaHall;
using FobumCinema.API.Models.Dtos.Comment;
using FobumCinema.API.Models.Dtos.CommentRating;
using FobumCinema.API.Models.Dtos.HallType;
using FobumCinema.API.Models.Dtos.Movie;
using FobumCinema.API.Models.Dtos.MovieMark;
using FobumCinema.API.Models.Dtos.Review;
using FobumCinema.API.Models.Dtos.Screening;
using FobumCinema.API.Models.Dtos.Seat;
using FobumCinema.API.Models.Dtos.SeatType;
using FobumCinema.API.Models.Dtos.SeatTypePrice;
using FobumCinema.Core.Entities;

namespace FobumCinema.Infrastructure.Data
{
    public class FobumCinemaProfile : Profile
    {
        public FobumCinemaProfile()
        {
            CreateMap<Cinema, CinemaDto>();
            CreateMap<Cinema, DetailedCinemaDto>()
            .ForCtorParam("Id", opt => opt.MapFrom(src => src.Id))
            .ForCtorParam("Name", opt => opt.MapFrom(src => src.Name))
            .ForCtorParam("Img", opt => opt.MapFrom(src => src.Img))
            .ForCtorParam("Address", opt => opt.MapFrom(src => src.Address))
            .ForCtorParam("CinemaHallsCount", opt => opt.MapFrom(src => 0))
            .ForCtorParam("HasDisabledSeats", opt => opt.MapFrom(src => false));
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

            CreateMap<CreateCinemaHallDto, CinemaHall>();
            CreateMap<UpdateCinemaHallDto, CinemaHall>();
            CreateMap<CinemaHall, CinemaHallDto>();

            CreateMap<CreateHallTypeDto, CinemaHall>();
            CreateMap<UpdateHallTypeDto, CinemaHall>();
            CreateMap<CinemaHall, HallTypeDto>();

            CreateMap<CreateSeatDto, Seat>();
            CreateMap<UpdateSeatDto, Seat>();
            CreateMap<Seat, SeatDto>();

            CreateMap<CreateSeatTypeDto, SeatType>();
            CreateMap<UpdateSeatTypeDto, SeatType>();
            CreateMap<SeatType, SeatTypeDto>();

            CreateMap<CreateSeatTypePriceDto, SeatTypePrice>();
            CreateMap<UpdateSeatTypePriceDto, SeatTypePrice>();
            CreateMap<SeatTypePrice, SeatTypePriceDto>();

            CreateMap<CinemaCompany, CinemaCompanyDto>();
            CreateMap<CreateCinemaCompanyDto, CinemaCompany>();
            CreateMap<UpdateCinemaCompanyDto, CinemaCompany>();

            CreateMap<User, UserDto>();
        }
    }
}