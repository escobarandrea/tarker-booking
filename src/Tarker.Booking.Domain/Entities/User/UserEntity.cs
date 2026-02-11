using System;
using System.Collections.Generic;
using System.Text;

namespace Tarker.Booking.Domain.Entities.User
{
    public class UserEntity
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
    }
}
