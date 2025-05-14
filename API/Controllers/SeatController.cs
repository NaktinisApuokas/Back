using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FobumCinema.API.Auth.Model;
using FobumCinema.API.Models.Dtos.Seat;
using FobumCinema.Core.Entities;
using FobumCinema.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FobumCinema.API.Controllers
{
    [ApiController]
    [Route("api/cinemaCompany/{CinemaCompanyId}/seat")]
    public class SeatController : ControllerBase
    {
        private readonly ISeatRepository _SeatRepository;
        private readonly IMapper _mapper;

        public SeatController(ISeatRepository SeatRepository,
            IMapper mapper)
        {
            _SeatRepository = SeatRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<SeatDto>> GetAllAsync(int cinemaCompanyId)
        {
            var seats = await _SeatRepository.GetAllAsync(cinemaCompanyId);
            return seats.Select(o => _mapper.Map<SeatDto>(o));
        }

        [HttpGet("{seatId}")]
        public async Task<ActionResult<SeatDto>> GetAsync(int seatId)
        {
            var seat = await _SeatRepository.GetAsync(seatId);
            if (seat == null) return NotFound();

            return Ok(_mapper.Map<SeatDto>(seat));
        }

        [HttpPost]
        [Authorize(Roles = UserRoles.Admin)]
        public async Task<ActionResult<SeatDto>> PostAsync(CreateSeatDto seatDto)
        {
            var seat = _mapper.Map<Seat>(seatDto);

            await _SeatRepository.InsertAsync(seat);

            return Created($"/api/cinemaCompany/{seatDto.CinemaCompanyId}/seats/{seat.Id}", _mapper.Map<SeatDto>(seat));
        }

        [HttpPut("{seatId}")]
        [Authorize(Roles = UserRoles.Admin)]
        public async Task<ActionResult<SeatDto>> PutAsync(int seatId, UpdateSeatDto seatDto)
        {
            var oldseat = await _SeatRepository.GetAsync(seatId);
            if (oldseat == null)
                return NotFound();

            _mapper.Map(seatDto, oldseat);

            await _SeatRepository.UpdateAsync(oldseat);

            return Ok(_mapper.Map<SeatDto>(oldseat));
        }

        [HttpDelete("{seatId}")]
        [Authorize(Roles = UserRoles.Admin)]
        public async Task<ActionResult> DeleteAsync(int seatId)
        {
            var seat = await _SeatRepository.GetAsync(seatId);
            if (seat == null)
                return NotFound();

            await _SeatRepository.DeleteAsync(seat);

            return Ok("Seat deleted successfully");
        }
    }
}
