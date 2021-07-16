using Booking.Domain.Interfaces;

namespace Booking.Domain.Models
{
    public class BookingDBSettings : IBookingDBSettings
    {
        public string CollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}
