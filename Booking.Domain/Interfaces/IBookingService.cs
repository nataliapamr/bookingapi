using Booking.Domain.Models;
using System.Collections.Generic;

namespace Booking.Domain.Services
{
    public interface IBookingService
    {
        IEnumerable<Book> GetAllBookings();
        Book GetBookingById(string id);
        void PlaceBooking(Book book);
        void EditBooking(string id, Book book);
        void DeleteBooking(string id);
    }
}