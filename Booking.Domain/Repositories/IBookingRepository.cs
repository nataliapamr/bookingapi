using Booking.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Booking.Domain.Repositories
{
    public interface IBookingRepository<T> where T : Book
    {
        IEnumerable<T> GetAllBookings();
        T GetBookingById(string id);
        IEnumerable<T> GetBookingsByDates(Expression<Func<T, bool>>  filter);
        void PlaceBooking(T entity);
        void EditBooking(string id, T book);
        void DeleteBooking(string id);

    }
}
