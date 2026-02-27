using System;
using System.Collections.Generic;
using System.Text;

namespace Tarker.Booking.Application.External.GetTokenJwt
{
    public interface IGetTokenJwtService
    {
        string Execute(string id);
    }
}
