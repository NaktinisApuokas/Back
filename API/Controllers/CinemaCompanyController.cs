using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FobumCinema.API.Auth.Model;
using FobumCinema.API.Models.Dtos.CinemaCompany;
using FobumCinema.Core.Entities;
using FobumCinema.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FobumCinema.API.Controllers
{

    [ApiController]
    [Route("api/cinemaCompany")]
    public class CinemaCompanyController : ControllerBase
    {
        private readonly ICinemaCompanyRepository _CinemaCompanyRepository;
        private readonly IMapper _mapper;

        public CinemaCompanyController(ICinemaCompanyRepository CinemaCompanyRepository, IMapper mapper)
        {
            _CinemaCompanyRepository = CinemaCompanyRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<CinemaCompanyDto>> GetAll()
        {
            return (await _CinemaCompanyRepository.GetAll()).Select(o => _mapper.Map<CinemaCompanyDto>(o));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CinemaCompanyDto>> Get(int id)
        {
            var cinemaCompany = await _CinemaCompanyRepository.Get(id);
            if (cinemaCompany == null) return NotFound($"Cinema with id '{id}' not found.");

            return Ok(_mapper.Map<CinemaCompanyDto>(cinemaCompany));
        }

        [HttpPost]
        [Authorize(Roles = UserRoles.Admin)]
        public async Task<ActionResult<CinemaCompanyDto>> Post(CreateCinemaCompanyDto cinemaDto)
        {
            var cinemaCompany = _mapper.Map<CinemaCompany>(cinemaDto);

            await _CinemaCompanyRepository.Create(cinemaCompany);

            return Created($"/api/cinemaCompany/{cinemaCompany.CinemaCompanyID}", _mapper.Map<CinemaCompanyDto>(cinemaCompany));
        }

        [HttpPut("{id}")]
        [Authorize(Roles = UserRoles.Admin)]
        public async Task<ActionResult<CinemaCompanyDto>> Put(int id, UpdateCinemaCompanyDto CinemaDto)
        {
            var cinemaCompany = await _CinemaCompanyRepository.Get(id);
            if (cinemaCompany == null) return NotFound($"Couldn't find a CinemaCompany with id of {id}");

            _mapper.Map(CinemaDto, cinemaCompany);

            await _CinemaCompanyRepository.Put(cinemaCompany);

            return Ok(_mapper.Map<CinemaCompanyDto>(cinemaCompany));
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = UserRoles.Admin)]
        public async Task<ActionResult<CinemaCompanyDto>> Delete(int id)
        {
            var cinemaCompany = await _CinemaCompanyRepository.Get(id);
            if (cinemaCompany == null) return NotFound($"CinemaCompany with id '{id}' not found.");

            await _CinemaCompanyRepository.Delete(cinemaCompany);

            return NoContent();
        }
    }
}