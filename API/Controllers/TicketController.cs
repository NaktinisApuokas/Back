using AutoMapper;
using FobumCinema.API.Models.Dtos.Screening;
using FobumCinema.API.Models.Dtos.Ticket;
using FobumCinema.Core.Entities;
using FobumCinema.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FobumCinema.API.Controllers
{
    [ApiController]
    [Route("api/screening/{ScreeningId}/Ticket")]
    public class TicketController : ControllerBase
    {
        private readonly ITicketRepository _TicketRepository;
        private readonly IMapper _mapper;
        private readonly IMovieRepository _MovieRepository;
        private readonly ICinemaRepository _CinemaRepository;
        private readonly IScreeningRepository _ScreeningRepository;

        public TicketController(ITicketRepository TicketRepository,
            IMapper mapper,
            IMovieRepository MovieRepository,
            ICinemaRepository CinemaRepository,
            IScreeningRepository ScreeningRepository
            )
        {
            _MovieRepository = MovieRepository;
            _CinemaRepository = CinemaRepository;
            _TicketRepository = TicketRepository;
            _ScreeningRepository = ScreeningRepository;
            _mapper = mapper;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<TicketInfoDto>>> GetByUsernameAsync(string Username)
        {
            try
            {
                var Tickets = await _TicketRepository.GetAllByUsernameAsync(Username);

                List<TicketInfoDto> TicketsDto = new List<TicketInfoDto>();

                foreach (var ticket in Tickets)
                {
                    var screening = await _ScreeningRepository.GetAsync(ticket.ScreeningId);
                    var movie = await _MovieRepository.GetAsync(screening.MovieId);
                    var cinema = await _CinemaRepository.Get(movie.CinemaId);

                    var dto = new TicketInfoDto(
                        ScreeningId: screening.Id,
                        Time: screening.Time,
                        MovieTitle: movie.Title,
                        MovieTitleEng: movie.TitleEng,
                        CinemaName: cinema.Name,
                        ScreeningDateTime: screening.Date + " " + screening.Time,
                        Price: ticket.Price,
                        Row: ticket.Row,
                        Col: ticket.Col,
                        IsScanned: ticket.IsScanned
                    );

                    TicketsDto.Add(dto);
                }

                return Ok(TicketsDto);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Status = "Failed to retrieve tickets", Error = ex.Message });
            }
        }


        [HttpPost]
        public async Task<ActionResult> PostAsync(int ScreeningId, CreateTicketDto TicketDto)
        {

            try
            {
                var tickets = new List<Ticket>();

                foreach (var seat in TicketDto.SelectedSeats)
                {
                    var ticket = new Ticket
                    {
                        ScreeningId = ScreeningId,
                        SeatTypeId = seat.Id,
                        Price = seat.DefaultPrice,
                        Row = int.Parse(seat.location.Split('-')[0]),
                        Col = int.Parse(seat.location.Split('-')[1]),
                        IsScanned = false,
                        User = TicketDto.Username,
                        DateCreated = DateTime.Now
                    };

                    await _TicketRepository.InsertAsync(ticket);
                }

                return Ok(new { Status = "Tickets successfully created" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Status = "Failed to create tickets", Error = ex.Message });
            }
        }


        [HttpPost("scan")]
        public async Task<IActionResult> ScanTicket(int id)
        {
            var ticket = await _TicketRepository.GetAsync(id);

            if (ticket == null)
                return NotFound("Ticket not found");

            if (ticket.IsScanned)
                return BadRequest("Ticket already used");

            ticket.IsScanned = true;
            await _TicketRepository.UpdateAsync(ticket);

            return Ok("Ticket scanned successfully");
        }


    }
}
