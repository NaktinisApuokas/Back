using FobumCinema.API.Models.Dtos.SeatType;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;

namespace FobumCinema.Core.Entities
{
    public class CinemaHall
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }
         
        public int NumberOfSeats { get; set; }

        public bool HasDisabledSeats { get; set; }

        public int CinemaId { get; set; }

        public int HallTypeId { get; set; }

        public string RowSorting { get; set; }

        public string CollumnSorting { get; set; }

        public string? CellMatrixJson { get; set; }

        [NotMapped]
        public List<List<SeatTypeDto?>> CellMatrix
        {
            get => string.IsNullOrEmpty(CellMatrixJson)
                ? new()
                : JsonSerializer.Deserialize<List<List<SeatTypeDto?>>>(CellMatrixJson);
            set => CellMatrixJson = JsonSerializer.Serialize(value);
        }
        [NotMapped]
        public List<List<SeatWithTicketsDto>> Matrix { get; set; }

        public Cinema Cinema { get; set; }

    }
}
