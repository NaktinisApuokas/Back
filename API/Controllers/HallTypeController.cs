using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FobumCinema.API.Auth.Model;
using FobumCinema.API.Models.Dtos.HallType;
using FobumCinema.Core.Entities;
using FobumCinema.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FobumCinema.API.Controllers
{
    [ApiController]
    [Route("api/cinemas/{CinemaId}/hallType")]
    public class HallTypeController : ControllerBase
    {
        private readonly IHallTypeRepository _HallTypeRepository;
        private readonly IMapper _mapper;

        public HallTypeController(IHallTypeRepository HallTypeRepository,
            IMapper mapper)
        {
            _HallTypeRepository = HallTypeRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<HallTypeDto>> GetAllAsync(int cinemaId)
        {
            var hallTypes = await _HallTypeRepository.GetAllAsync(cinemaId);
            return hallTypes.Select(o => _mapper.Map<HallTypeDto>(o));
        }

        [HttpGet("{hallTypeId}")]
        public async Task<ActionResult<HallTypeDto>> GetAsync(int hallTypeId)
        {
            var hallType = await _HallTypeRepository.GetAsync(hallTypeId);
            if (hallType == null) return NotFound();

            return Ok(_mapper.Map<HallTypeDto>(hallType));
        }

        [HttpPost]
        [Authorize(Roles = UserRoles.Admin)]
        public async Task<ActionResult<HallTypeDto>> PostAsync(CreateHallTypeDto hallTypeDto)
        {
            var hallType = _mapper.Map<HallType>(hallTypeDto);

            await _HallTypeRepository.InsertAsync(hallType);

            return Created($"/api/cinemaCompany/{hallTypeDto.CinemaCompanyId}/hallTypes/{hallType.Id}", _mapper.Map<HallTypeDto>(hallType));
        }

        [HttpPut("{hallTypeId}")]
        [Authorize(Roles = UserRoles.Admin)]
        public async Task<ActionResult<HallTypeDto>> PutAsync(int hallTypeId, UpdateHallTypeDto hallTypeDto)
        {
            var oldhallType = await _HallTypeRepository.GetAsync(hallTypeId);
            if (oldhallType == null)
                return NotFound();

            _mapper.Map(hallTypeDto, oldhallType);

            await _HallTypeRepository.UpdateAsync(oldhallType);

            return Ok(_mapper.Map<HallTypeDto>(oldhallType));
        }

        [HttpDelete("{hallTypeId}")]
        [Authorize(Roles = UserRoles.Admin)]
        public async Task<ActionResult> DeleteAsync(int hallTypeId)
        {
            var hallType = await _HallTypeRepository.GetAsync(hallTypeId);
            if (hallType == null)
                return NotFound();

            await _HallTypeRepository.DeleteAsync(hallType);

            return Ok("hallType deleted successfully");
        }
    }
}
