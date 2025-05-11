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
            return seatTypes.Select(o => new SeatTypeDto(
                o.Id,
                o.Name!,
                GetLogoBinary(o.LogoPath),
                null,
                o.DefaultPrice,
                o.CinemaCompanyId,
                o.Width,
                o.Height
            ));
        }

        public string? GetLogoBinary(string? logoPath)
        {
            if (string.IsNullOrEmpty(logoPath) || !System.IO.File.Exists(logoPath))
                return null;

            var bytes = System.IO.File.ReadAllBytes(logoPath);
            return Convert.ToBase64String(bytes);
        }


        [HttpGet("{seatTypeId}")]
        public async Task<ActionResult<SeatTypeDto>> GetAsync(int seatTypeId)
        {
            var seatType = await _SeatTypeRepository.GetAsync(seatTypeId);
            if (seatType == null) return NotFound();

            return Ok(_mapper.Map<SeatTypeDto>(seatType));
        }

        [HttpPost]
        public async Task<ActionResult<SeatTypeDto>> PostAsync([FromForm] CreateSeatTypeDto seatTypeDto)
        {
            string? filePath = null;

            if (seatTypeDto.Logo != null)
            {
                filePath = Path.Combine("Uploads", "Logos", Guid.NewGuid() + Path.GetExtension(seatTypeDto.Logo.FileName));

                Directory.CreateDirectory(Path.GetDirectoryName(filePath)!);

                await using var stream = new FileStream(filePath, FileMode.Create);
                await seatTypeDto.Logo.CopyToAsync(stream);
            }

            var seatType = new SeatType
            {
                Name = seatTypeDto.Name,
                DefaultPrice = seatTypeDto.DefaultPrice,
                CinemaCompanyId = 1,
                LogoPath = filePath ,
                Width = seatTypeDto.Width,
                Height = 1
            };

            await _SeatTypeRepository.InsertAsync(seatType);

            return Created($"/api/cinemaCompany/1/seatTypes/{seatType.Id}", _mapper.Map<CreatedSeatTypeDto>(seatType));
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
