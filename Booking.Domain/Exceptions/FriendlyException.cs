using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Booking.Domain.Exceptions
{
    public class FriendlyException : Exception
    {
        public FriendlyException()
        {
        }

        public FriendlyException(string message) : base(message)
        {
        }
    }
}
