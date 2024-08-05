using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FobumCinema.API.Models.Dtos.SeatType;
using FobumCinema.Core.Entities;
using FobumCinema.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FobumCinema.API.Controllers
{
    [ApiController]
    [Route("api/cinemaCompany/{CinemaCompanyId}/seatType")]
    public class SeatTypeController : ControllerBase
    {
        private readonly ISeatTypeRepository _SeatTypeRepository;
        private readonly IMapper _mapper;

        public SeatTypeController(ISeatTypeRepository SeatTypeRepository,
            IMapper mapper)
        {
            _SeatTypeRepository = SeatTypeRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<SeatTypeDto>> GetAllAsync(int cinemaCompanyId)
        {
            var seatTypes = await _SeatTypeRepository.GetAllAsync(cinemaCompanyId);
            return seatTypes.Select(o => _mapper.Map<SeatTypeDto>(o));
        }

        [HttpGet("{seatTypeId}")]
        public async Task<ActionResult<SeatTypeDto>> GetAsync(int seatTypeId)
        {
            var seatType = await _SeatTypeRepository.GetAsync(seatTypeId);
            if (seatType == null) return NotFound();

            return Ok(_mapper.Map<SeatTypeDto>(seatType));
        }

        [HttpPost]
        public async Task<ActionResult<SeatTypeDto>> PostAsync(CreateSeatTypeDto seatTypeDto)
        {
            var seatType = _mapper.Map<SeatType>(seatTypeDto);

            await _SeatTypeRepository.InsertAsync(seatType);

            return Created($"/api/cinemaCompany/{seatTypeDto.CinemaCompanyId}/seatTypes/{seatType.Id}", _mapper.Map<SeatTypeDto>(seatType));
        }

        [HttpPut("{seatTypeId}")]
        public async Task<ActionResult<SeatTypeDto>> PutAsync(int seatTypeId, UpdateSeatTypeDto seatTypeDto)
        {
            var oldseatType = await _SeatTypeRepository.GetAsync(seatTypeId);
            if (oldseatType == null)
                return NotFound();

            _mapper.Map(seatTypeDto, oldseatType);

            await _SeatTypeRepository.UpdateAsync(oldseatType);

            return Ok(_mapper.Map<SeatTypeDto>(oldseatType));
        }

        [HttpDelete("{seatTypeId}")]
        public async Task<ActionResult> DeleteAsync(int seatTypeId)
        {
            var seatType = await _SeatTypeRepository.GetAsync(seatTypeId);
            if (seatType == null)
                return NotFound();

            await _SeatTypeRepository.DeleteAsync(seatType);

            return Ok("SeatType deleted successfully");
        }
    }
}
