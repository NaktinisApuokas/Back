﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FobumCinema.API.Models.Dtos.HallType;
using FobumCinema.Core.Entities;
using FobumCinema.Core.Interfaces;
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
        public async Task<IEnumerable<SeatTypeDto>> GetAllAsync(int cinemaId)
        {
            var hallTypes = await _HallTypeRepository.GetAllAsync(cinemaId);
            return hallTypes.Select(o => _mapper.Map<SeatTypeDto>(o));
        }

        [HttpGet("{hallTypeId}")]
        public async Task<ActionResult<SeatTypeDto>> GetAsync(int hallTypeId)
        {
            var hallType = await _HallTypeRepository.GetAsync(hallTypeId);
            if (hallType == null) return NotFound();

            return Ok(_mapper.Map<SeatTypeDto>(hallType));
        }

        [HttpPost]
        public async Task<ActionResult<SeatTypeDto>> PostAsync(CreateHallTypeDto hallTypeDto)
        {
            var hallType = _mapper.Map<HallType>(hallTypeDto);

            await _HallTypeRepository.InsertAsync(hallType);

            return Created($"/api/cinemaCompany/{hallTypeDto.CinemaCompanyId}/hallTypes/{hallType.Id}", _mapper.Map<SeatTypeDto>(hallType));
        }

        [HttpPut("{hallTypeId}")]
        public async Task<ActionResult<SeatTypeDto>> PutAsync(int hallTypeId, UpdateHallTypeDto hallTypeDto)
        {
            var oldhallType = await _HallTypeRepository.GetAsync(hallTypeId);
            if (oldhallType == null)
                return NotFound();

            _mapper.Map(hallTypeDto, oldhallType);

            await _HallTypeRepository.UpdateAsync(oldhallType);

            return Ok(_mapper.Map<SeatTypeDto>(oldhallType));
        }

        [HttpDelete("{hallTypeId}")]
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
