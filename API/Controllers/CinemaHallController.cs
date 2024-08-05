using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FobumCinema.API.Models.Dtos.CinemaHall;
using FobumCinema.Core.Entities;
using FobumCinema.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FobumCinema.API.Controllers
{
    [ApiController]
    [Route("api/cinemas/{CinemaId}/cinemaHalls")]
    public class CinemaHallController : ControllerBase
    {
        private readonly ICinemaHallRepository _CinemaHallRepository;
        private readonly ICinemaCompanyRepository _CinemaRepository;
        private readonly IMapper _mapper;

        public CinemaHallController(ICinemaHallRepository CinemaHallRepository,
            IMapper mapper,
            ICinemaCompanyRepository CinemaRepository)
        {
            _CinemaHallRepository = CinemaHallRepository;
            _mapper = mapper;
            _CinemaRepository = CinemaRepository;
        }

        [HttpGet]
        public async Task<IEnumerable<CinemaHallDto>> GetAllAsync(int cinemaId)
        {
            var cinemaHalls = await _CinemaHallRepository.GetAllAsync(cinemaId);
            return cinemaHalls.Select(o => _mapper.Map<CinemaHallDto>(o));
        }

        [HttpGet("{cinemaHallId}")]
        public async Task<ActionResult<CinemaHallDto>> GetAsync(int cinemaHallId)
        {
            var cinemaHall = await _CinemaHallRepository.GetAsync(cinemaHallId);
            if (cinemaHall == null) return NotFound();

            return Ok(_mapper.Map<CinemaHallDto>(cinemaHall));
        }

        [HttpPost]
        public async Task<ActionResult<CinemaHallDto>> PostAsync(int cinemaId, CreateCinemaHallDto cinemaHallDto)
        {
            var cinema = await _CinemaRepository.Get(cinemaId);
            if (cinema == null) return NotFound($"Couldn't find a cinemaHall with id of {cinemaId}");

            var cinemaHall = _mapper.Map<CinemaHall>(cinemaHallDto);
            cinemaHall.CinemaId = cinemaId;

            await _CinemaHallRepository.InsertAsync(cinemaHall);

            return Created($"/api/cinemas/{cinemaId}/cinemaHalls/{cinemaHall.Id}", _mapper.Map<CinemaHallDto>(cinemaHall));
        }

        [HttpPut("{cinemaHallId}")]
        public async Task<ActionResult<CinemaHallDto>> PutAsync(int CinemaId, int cinemaHallId, UpdateCinemaHallDto cinemaHallDto)
        {
            var cinema = await _CinemaRepository.Get(CinemaId);
            if (cinema == null) return NotFound($"Couldn't find a cinemaHall with id of {CinemaId}");

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
