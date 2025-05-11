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
using System.Text.Json;

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
            CreateMap<UpcomingMovieDto, Movie>();
            CreateMap<Movie, ReturnUpcomingMovieDto>();
            CreateMap<UpdateMovieDto, Movie>();
            CreateMap<Movie, MovieDto>();
            CreateMap<Movie, GeneralMovieDto>();
 
            CreateMap<CreateScreeningDto, Screening>();
            CreateMap<UpdateScreeningDto, Screening>();
            CreateMap<Screening, ScreeningDto>();
            CreateMap<Screening, ScreeningInfoDto>();

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

            CreateMap<CreateCinemaHallDto, CinemaHall>()
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => ""))
            .ForMember(dest => dest.CellMatrixJson, opt => opt.MapFrom<CellMatrixJsonResolver>())
            .ForMember(dest => dest.NumberOfSeats, opt => opt.MapFrom<NumberOfSeatsResolver>())
            .ForMember(dest => dest.HasDisabledSeats, opt => opt.MapFrom<HasDisabledSeatsResolver>())
            .ForMember(dest => dest.HallTypeId, opt => opt.MapFrom(src => 0));

            CreateMap<UpdateCinemaHallDto, CinemaHall>();
            CreateMap<CreateHallTypeDto, CinemaHall>();
            CreateMap<UpdateHallTypeDto, CinemaHall>();
            CreateMap<CinemaHall, CinemaHallDto>();
            CreateMap<CinemaHall, HallTypeDto>();
            CreateMap<CinemaHall, CinemaHallWithTicketsDto>()
            .ForMember(dest => dest.Rows, opt => opt.MapFrom(src => src.CellMatrix != null ? src.CellMatrix.Count : 0))
            .ForMember(dest => dest.Cols, opt => opt.MapFrom(src => (src.CellMatrix != null && src.CellMatrix.Count > 0) ? src.CellMatrix[0].Count : 0));

            CreateMap<CinemaHall, NewCinemaHallDto>()
            .ForMember(dest => dest.Rows, opt => opt.MapFrom(src => src.CellMatrix != null ? src.CellMatrix.Count : 0))
            .ForMember(dest => dest.Cols, opt => opt.MapFrom(src => (src.CellMatrix != null && src.CellMatrix.Count > 0) ? src.CellMatrix[0].Count : 0))
            .ForMember(dest => dest.Matrix, opt => opt.MapFrom(src => src.CellMatrix));

            CreateMap<CreateSeatDto, Seat>();
            CreateMap<UpdateSeatDto, Seat>();
            CreateMap<Seat, SeatDto>();

            CreateMap<CreateSeatTypeDto, SeatType>();
            CreateMap<UpdateSeatTypeDto, SeatType>();
            CreateMap<SeatType, SeatTypeDto>();
            CreateMap<SeatType, CreatedSeatTypeDto>();
            CreateMap<SeatTypeDto, SeatWithTicketsDto>()
             .ConstructUsing(src => new SeatWithTicketsDto(
                 src.Id,               
                 src.Name,            
                 src.LogoData,       
                 src.LogoPath,        
                 src.DefaultPrice,    
                 src.Width,          
                 false,              
                 false               
             ));

            CreateMap<CreateSeatTypePriceDto, SeatTypePrice>();
            CreateMap<UpdateSeatTypePriceDto, SeatTypePrice>();
            CreateMap<SeatTypePrice, SeatTypePriceDto>();

            CreateMap<CinemaCompany, CinemaCompanyDto>();
            CreateMap<CreateCinemaCompanyDto, CinemaCompany>();
            CreateMap<UpdateCinemaCompanyDto, CinemaCompany>();

            CreateMap<User, UserDto>();
        }


        public class CellMatrixJsonResolver : IValueResolver<CreateCinemaHallDto, CinemaHall, string>
        {
            public string Resolve(CreateCinemaHallDto source, CinemaHall destination, string destMember, ResolutionContext context)
            {
                return JsonSerializer.Serialize(source.CellMatrix);
            }
        }

        public class NumberOfSeatsResolver : IValueResolver<CreateCinemaHallDto, CinemaHall, int>
        {
            public int Resolve(CreateCinemaHallDto source, CinemaHall destination, int destMember, ResolutionContext context)
            {
                return source.CellMatrix
                    .SelectMany(row => row)
                    .Count(seat => seat != null && seat.DefaultPrice > 0);
            }
        }

        public class HasDisabledSeatsResolver : IValueResolver<CreateCinemaHallDto, CinemaHall, bool>
        {
            public bool Resolve(CreateCinemaHallDto source, CinemaHall destination, bool destMember, ResolutionContext context)
            {
                return source.CellMatrix
                    .Any(row => row.Any(seat => seat != null && seat.Id == 2002));
            }
        }
    }
}