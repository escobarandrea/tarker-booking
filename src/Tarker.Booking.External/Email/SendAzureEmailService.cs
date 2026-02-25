using Azure;
using Azure.Communication.Email;
using Microsoft.Extensions.Configuration;
using Tarker.Booking.Application.External.Email;
using Tarker.Booking.Domain.Models.Email;

namespace Tarker.Booking.External.Email
{
    public class SendAzureEmailService : ISendAzureEmailService
    {
        private readonly IConfiguration _configuration;

        public SendAzureEmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<bool> ExecuteAsync(SendEmailRequestModel model)
        {
            bool isSent = false;
            string apiKey = _configuration["SendAzureEmailKey"]!;

            var emailClient = new EmailClient(apiKey);

            var emailContent = new EmailContent(model.Subject)
            {
                PlainText = model.PlainTextContent,
                Html = model.HtmlContent
            };

            var toRecipients = model.To.Select(email => new EmailAddress(email)).ToList();

            var emailRecipients = new EmailRecipients(toRecipients);

            var emailMessage = new EmailMessage(
                senderAddress: model.From,
                emailRecipients,
                emailContent);

            try
            {
                EmailSendOperation emailSendOperation = emailClient.Send(WaitUntil.Completed, emailMessage);

                EmailSendResult emailSendResult = emailSendOperation.Value;
                isSent = emailSendResult.Status == EmailSendStatus.Succeeded;
            }
            catch (RequestFailedException ex)
            {
                throw;
            }

            return isSent;
        }
    }
}
