using AutoMapper;
using FobumCinema.API.Models.Dtos.Payment;
using FobumCinema.Core.Entities;
using FobumCinema.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Stripe;
using Stripe.Checkout;
using Stripe.V2;
using System.Text.Json;

[Route("api/payment")]
[ApiController]
public class PaymentController : ControllerBase
{
    private readonly ITicketRepository _TicketRepository;
    private readonly IMapper _mapper;

    public PaymentController(ITicketRepository TicketRepository,
        IMapper mapper)
    {
        _TicketRepository = TicketRepository;
        _mapper = mapper;
    }

    [HttpPost("create-checkout-session")]
    public ActionResult CreateCheckoutSession([FromBody] PaymentRequestDto request)
    {
        StripeConfiguration.ApiKey = "x";

        var metadata = new Dictionary<string, string>
        {
            { "screeningId", request.ScreeningId.ToString() },
            { "seats", JsonSerializer.Serialize(request.Seats) } 
        };

        var options = new SessionCreateOptions
        {
            PaymentMethodTypes = new List<string> { "card" },
            LineItems = request.Seats.Select(seat => new SessionLineItemOptions
            {
                PriceData = new SessionLineItemPriceDataOptions
                {
                    Currency = "eur",
                    UnitAmount = (long)(seat.DefaultPrice * 100), 
                    ProductData = new SessionLineItemPriceDataProductDataOptions
                    {
                        Name = $"Filmas: {request.MovieTitle} ({request.MovieTitleEng})",
                        Description =
                                    $"Vieta: {seat.Location.Split('-')[0]} | " +
                                    $"Eilė: {seat.Location.Split('-')[1]} | " +
                                    $"Kinas: {request.CinemaName} | " +
                                    $"Laikas: {request.ScreeningDateTime}"
                    }
                },
                Quantity = 1
            }).ToList(),
            Mode = "payment",
            Metadata = metadata,
            SuccessUrl = "http://localhost:3000/payment-success?session_id={CHECKOUT_SESSION_ID}",
            CancelUrl = "http://localhost:3000/payment-cancel",
        };

        var service = new SessionService();
        Session session = service.Create(options);

        return Ok(new { sessionUrl = session.Url });
    }
   
    
    [HttpGet("session/{sessionId}")]
    public async Task<IActionResult> GetSession(string sessionId)
    {
        var service = new SessionService();
        Session session = await service.GetAsync(sessionId);

        var screeningId = int.Parse(session.Metadata["screeningId"]);
        var seats = JsonSerializer.Deserialize<List<SeatDto>>(session.Metadata["seats"]);

        return Ok(new { screeningId, seats });
    }
}

public class SeatDto
{
    public int id { get; set; }
    public string Location { get; set; }
    public decimal DefaultPrice { get; set; }
}
