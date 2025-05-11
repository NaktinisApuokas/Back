using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using AutoMapper;
using FobumCinema.API.Models.Dtos.CinemaHall;
using FobumCinema.API.Models.Dtos.SeatType;
using FobumCinema.Core.Entities;
using FobumCinema.Core.Interfaces;
using FobumCinema.Infrastructure.Repositories;
using FobumCinema.Migrations;
using Microsoft.AspNetCore.Mvc;

namespace FobumCinema.API.Controllers
{
    [ApiController]
    [Route("api/cinemas/{CinemaId}/cinemaHalls")]
    public class CinemaHallController : ControllerBase
    {
        private readonly ICinemaHallRepository _CinemaHallRepository;
        private readonly IScreeningRepository _ScreeningRepository;
        private readonly IMovieRepository _MovieRepository;
        private readonly ITicketRepository _TicketRepository;
        private readonly ICinemaRepository _CinemaRepository;
        private readonly IMapper _mapper; 

        public CinemaHallController(ICinemaHallRepository CinemaHallRepository,
            IMapper mapper,
            ICinemaRepository CinemaRepository,
            ITicketRepository ticketRepository,
            IScreeningRepository screeningRepository,
            IMovieRepository movieRepository)
        {
            _CinemaHallRepository = CinemaHallRepository;
            _mapper = mapper;
            _CinemaRepository = CinemaRepository;
            _TicketRepository = ticketRepository;
            _ScreeningRepository = screeningRepository;
            _MovieRepository = movieRepository;
        }

        [HttpGet]
        public async Task<IEnumerable<CinemaHallDto>> GetAllAsync(int cinemaId)
        {
            var cinemaHalls = await _CinemaHallRepository.GetAllAsync(cinemaId);
            return cinemaHalls.Select(o => _mapper.Map<CinemaHallDto>(o));
        }

        [HttpGet("{cinemaHallId}")]
        public async Task<ActionResult<NewCinemaHallDto>> GetAsync(int cinemaHallId)
        {
            var cinemaHall = await _CinemaHallRepository.GetAsync(cinemaHallId);
            if (cinemaHall == null) return NotFound();

            cinemaHall.CellMatrix = JsonSerializer.Deserialize<List<List<SeatTypeDto?>>>(cinemaHall.CellMatrixJson ?? "[]");

            return Ok(_mapper.Map<NewCinemaHallDto>(cinemaHall));
        }

        [HttpGet("ForScreeningForm")]
        public async Task<IEnumerable<CinemaHallDto>> GetForScreeningFormAsync(int MovieID)
        {
            var Movie = await _MovieRepository.GetAsync(MovieID);

            var CinemaHalls = await _CinemaHallRepository.GetAllAsync(Movie.CinemaId);

            return CinemaHalls.Select(o => _mapper.Map<CinemaHallDto>(o));
        }

        [HttpGet("ByScreeningID")]
        public async Task<ActionResult<CinemaHallWithTicketsDto>> GetByScreeningIDAsync(int ScreeningID)
        {
            var cinemaHall = await _CinemaHallRepository.GetByScreeningIDAsync(ScreeningID);
            if (cinemaHall == null) return NotFound();

            var tickets = await _TicketRepository.GetByScreeningIdAsync(ScreeningID);

            var cellMatrix = JsonSerializer.Deserialize<List<List<SeatTypeDto?>>>(cinemaHall.CellMatrixJson ?? "[]");
            if (cellMatrix == null || cellMatrix.Count == 0)
                return BadRequest("Invalid seating layout.");

            int rowCount = cellMatrix.Count;
            int colCount = cellMatrix[0].Count;

            var seatMatrix = new List<List<SeatWithTicketsDto>>();

            for (int r = 0; r < rowCount; r++)
            {
                var seatRow = new List<SeatWithTicketsDto>();
                for (int c = 0; c < colCount; c++)
                {
                    var seat = cellMatrix[r][c];
                    if (seat == null || seat.DefaultPrice == 0)
                    {
                        seatRow.Add(null!); 
                        continue;
                    }

                    seatRow.Add(new SeatWithTicketsDto(
                        id: seat.Id,
                        name: seat.Name,
                        logoData: seat.LogoData,
                        logoPath: seat.LogoPath,
                        defaultPrice: seat.DefaultPrice,
                        width: seat.Width,
                        isTaken: false, 
                        isReserved: false 
                    ));
                }
                seatMatrix.Add(seatRow);
            }

            foreach (var ticket in tickets)
            {
                SeatWithTicketsDto seat = seatMatrix[ticket.Row][ticket.Col];
                if (seat != null)
                {
                    seat.IsTaken = true;
                }
            }

            cinemaHall.Matrix = seatMatrix;
            return Ok(_mapper.Map<CinemaHallWithTicketsDto>(cinemaHall));
        }

        [HttpPost]
        public async Task<ActionResult<CinemaHallDto>> PostAsync(int cinemaId, CreateCinemaHallDto cinemaHallDto)
        {
            var cinema = await _CinemaRepository.Get(cinemaId);
            if (cinema == null) return NotFound($"Couldn't find a cinema with id of {cinemaId}");

            var cinemaHall = _mapper.Map<CinemaHall>(cinemaHallDto);
            cinemaHall.CinemaId = cinemaId;

            await _CinemaHallRepository.InsertAsync(cinemaHall);

            return Created($"/api/cinemas/{cinemaId}/cinemaHalls/{cinemaHall.Id}", "OK");
        }

        [HttpPut("{cinemaHallId}")]
        public async Task<ActionResult<CinemaHallDto>> PutAsync(int cinemaHallId, CreateCinemaHallDto cinemaHallDto)
        {
            var oldcinemaHall = await _CinemaHallRepository.GetAsync(cinemaHallId);
            if (oldcinemaHall == null)
                return NotFound();

            _mapper.Map(cinemaHallDto, oldcinemaHall);

            await _CinemaHallRepository.UpdateAsync(oldcinemaHall);

            return Ok(_mapper.Map<CinemaHallDto>(oldcinemaHall));
        }

        [HttpDelete("{cinemaHallId}")]
        public async Task<ActionResult> DeleteAsync(int cinemaHallId)
        {
            var cinemaHall = await _CinemaHallRepository.GetAsync(cinemaHallId);
            if (cinemaHall == null)
                return NotFound();

            await _CinemaHallRepository.DeleteAsync(cinemaHall);

            return Ok("cinemaHall deleted successfully");
        }
    }
}
