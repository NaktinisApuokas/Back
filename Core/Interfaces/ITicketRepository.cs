using FobumCinema.Core.Entities;

namespace FobumCinema.Core.Interfaces
{
    public interface ITicketRepository
    {
        Task<Ticket> GetAsync(int ticketID);

        Task<List<Ticket>> GetByScreeningIdAsync(int ScreeningID); 

        Task<List<Ticket>> GetAllByUsernameAsync(string Username);

        Task<Ticket> InsertAsync(Ticket ticket);

        Task UpdateAsync(Ticket ticket);

        Task DeleteAsync(Ticket ticket);
    }
}
