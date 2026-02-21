using System;
using System.Collections.Generic;
using System.Text;
using Tarker.Booking.Domain.Models;

namespace Tarker.Booking.Application.Features
{
    public static class ResponseApiService
    {
        public static BaseResponseModel Response(int statusCode, object? data = null, string? message = null) =>
        new()
        {
            StatusCode = statusCode,
            Success = statusCode is >= 200 and < 300,
            Message = message,
            Data = data
        };
    }
}
