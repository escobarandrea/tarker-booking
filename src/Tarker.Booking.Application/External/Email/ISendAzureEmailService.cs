using Tarker.Booking.Domain.Models.Email;

namespace Tarker.Booking.Application.External.Email
{
    public interface ISendAzureEmailService
    {
        Task<bool> ExecuteAsync(SendEmailRequestModel model);
    }
}
