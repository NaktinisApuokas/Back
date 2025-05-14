using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FobumCinema.API.Auth.Model;
using FobumCinema.API.Models.Dtos.SeatTypePrice;
using FobumCinema.Core.Entities;
using FobumCinema.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FobumCinema.API.Controllers
{
    [ApiController]
    [Route("api/movie/{MovieId}/seatTypePrice")]
    public class SeatTypePriceController : ControllerBase
    {
        private readonly ISeatTypePriceRepository _SeatTypePriceRepository;
        private readonly IMapper _mapper;

        public SeatTypePriceController(ISeatTypePriceRepository SeatTypePriceRepository,
            IMapper mapper)
        {
            _SeatTypePriceRepository = SeatTypePriceRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<SeatTypePriceDto>> GetAllAsync(int ScreeningId)
        {
            var seatTypePrices = await _SeatTypePriceRepository.GetAllAsync(ScreeningId);
            return seatTypePrices.Select(o => _mapper.Map<SeatTypePriceDto>(o));
        }

        [HttpGet("{seatTypePriceId}")]
        public async Task<ActionResult<SeatTypePriceDto>> GetAsync(int seatTypePriceId)
        {
            var seatTypePrice = await _SeatTypePriceRepository.GetAsync(seatTypePriceId);
            if (seatTypePrice == null) return NotFound();

            return Ok(_mapper.Map<SeatTypePriceDto>(seatTypePrice));
        }

        [HttpPost]
        [Authorize(Roles = UserRoles.Admin)]
        public async Task<ActionResult<SeatTypePriceDto>> PostAsync(CreateSeatTypePriceDto seatTypePriceDto)
        {
            var seatTypePrice = _mapper.Map<SeatTypePrice>(seatTypePriceDto);

            await _SeatTypePriceRepository.InsertAsync(seatTypePrice);

            return Created($"/api/movie/{seatTypePriceDto.ScreeningId}/seatTypePrices/{seatTypePrice.Id}", _mapper.Map<SeatTypePriceDto>(seatTypePrice));
        }

        [HttpPut("{seatTypePriceId}")]
        [Authorize(Roles = UserRoles.Admin)]
        public async Task<ActionResult<SeatTypePriceDto>> PutAsync(int seatTypePriceId, UpdateSeatTypePriceDto seatTypePriceDto)
        {
            var oldseatTypePrice = await _SeatTypePriceRepository.GetAsync(seatTypePriceId);
            if (oldseatTypePrice == null)
                return NotFound();

            _mapper.Map(seatTypePriceDto, oldseatTypePrice);

            await _SeatTypePriceRepository.UpdateAsync(oldseatTypePrice);

            return Ok(_mapper.Map<SeatTypePriceDto>(oldseatTypePrice));
        }

        [HttpDelete("{seatTypePriceId}")]
        [Authorize(Roles = UserRoles.Admin)]
        public async Task<ActionResult> DeleteAsync(int seatTypePriceId)
        {
            var seatTypePrice = await _SeatTypePriceRepository.GetAsync(seatTypePriceId);
            if (seatTypePrice == null)
                return NotFound();

            await _SeatTypePriceRepository.DeleteAsync(seatTypePrice);

            return Ok("SeatTypePrice deleted successfully");
        }
    }
}
