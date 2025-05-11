using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FobumCinema.Core.Entities;
using FobumCinema.Core.Interfaces;
using FobumCinema.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FobumCinema.Infrastructure.Repositories
{

    public class TicketRepository : ITicketRepository
    {
        private readonly FobumCinemaContext _FobumCinemaContext;

        public TicketRepository(FobumCinemaContext fobumCinemaContext)
        {
            _FobumCinemaContext = fobumCinemaContext;
        }

        public async Task<Ticket> GetAsync(int ticketId)
        {
            return await _FobumCinemaContext.Ticket.FirstOrDefaultAsync(o => o.Id == ticketId);
        }
        
        public async Task<List<Ticket>> GetByScreeningIdAsync(int ScreeningId)
        {
            return await _FobumCinemaContext.Ticket.Where(o => o.ScreeningId == ScreeningId).ToListAsync();
        }

        public async Task<List<Ticket>> GetAllByUsernameAsync(string Username)
        {
            return await _FobumCinemaContext.Ticket.Where(o => o.User == Username).ToListAsync();
        }

        public async Task<Ticket> InsertAsync(Ticket ticket)
        {
            _FobumCinemaContext.Ticket.Add(ticket);
            await _FobumCinemaContext.SaveChangesAsync();
            return ticket;
        }

        public async Task UpdateAsync(Ticket ticket)
        {
            _FobumCinemaContext.Ticket.Update(ticket);
            await _FobumCinemaContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Ticket ticket)
        {
            _FobumCinemaContext.Ticket.Remove(ticket);
            await _FobumCinemaContext.SaveChangesAsync();
        }
    }
}
