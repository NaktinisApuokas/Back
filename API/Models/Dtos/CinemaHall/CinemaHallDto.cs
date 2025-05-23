﻿using FobumCinema.API.Models.Dtos.Screening;
using System.ComponentModel.DataAnnotations;

namespace FobumCinema.API.Models.Dtos.CinemaHall
{
    public record CinemaHallDto(int Id,
        [Required] string Name,
        string Description,
        int NumberOfSeats,
        bool HasDisabledSeats,
        [Required] int HallTypeId);
}
