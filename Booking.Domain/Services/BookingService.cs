using Booking.Domain.Exceptions;
using Booking.Domain.Models;
using Booking.Domain.Repositories;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Booking.Domain.Services
{
    public class BookingService : IBookingService
    {
        private readonly IBookingRepository<Book> _bookingRepository;

        public BookingService(IBookingRepository<Book> bookingRepository)
        {
            _bookingRepository = bookingRepository;
        }

        /// <summary>
        /// Get all bookings
        /// </summary>
        /// <returns>All placed bookings or an exception if we don't have any</returns>
        public IEnumerable<Book> GetAllBookings()
        {
            var result = _bookingRepository.GetAllBookings();
            if (!result.Any())
                throw new FriendlyException("There are no bookings yet.");

            return result;
        }

        /// <summary>
        /// Get booking by specific id
        /// </summary>
        /// <param name="id">Book id</param>
        /// <returns>A placed book or an exceptino if we don't find it</returns>
        public Book GetBookingById(string id)
        {
            var result = _bookingRepository.GetBookingById(id);
            if (result is null)
                throw new FriendlyException($"There' no booking with id {id}.");

            return result;
        }

        /// <summary>
        /// Create a book for the specified dates
        /// </summary>
        /// <param name="book">New Book values</param>
        public void PlaceBooking(Book book)
        {
            var isBookingValid = BookingValidation(book.InitialDate, book.FinalDate, book.DaysOfReservation);
            var isRoomAvailable = RoomAvailable(book.InitialDate, book.FinalDate);

            if (isBookingValid && isRoomAvailable)
            {
                _bookingRepository.PlaceBooking(book);
            }
            else
                throw new FriendlyException("The room is not available for the selected dates. Please, select a new one.");
        }

        /// <summary>
        /// Delete specific book by id
        /// </summary>
        /// <param name="id">Book id</param>
        public void DeleteBooking(string id)
        {
            var existingBook = _bookingRepository.GetBookingById(id);
            if (existingBook is null)
                throw new FriendlyException($"There is no book with the id {id}");

            _bookingRepository.DeleteBooking(id);
        }

        /// <summary>
        /// Edit specific book by id
        /// </summary>
        /// <param name="id">Book id</param>
        /// <param name="book">Update book</param>
        public void EditBooking(string id, Book book)
        {
            var existingBook = _bookingRepository.GetBookingById(id);
            if (existingBook is null)
                throw new FriendlyException($"There is no book with the id {id}");
            
            _bookingRepository.EditBooking(id, book);
        }

        /// <summary>
        /// Check booking validation according to business logic
        /// </summary>
        /// <param name="initialDate">Initial Date of book</param>
        /// <param name="finalDate">Final Date of book</param>
        /// <param name="daysOfReservation">Days of reservation of Book</param>
        /// <returns></returns>
        private bool BookingValidation(DateTime initialDate, DateTime finalDate, int daysOfReservation)
        {
            var today = DateTime.Today;
            if (initialDate.Date.Equals(today.Date))
                throw new FriendlyException("Unfourtunatelly you cannot place a book for today.");

            if ((today.AddDays(30).Date - initialDate.Date).Days < 1)
                throw new FriendlyException("Unfourtunatelly you cannot place a book for more than 30 days from today.");

            if (daysOfReservation > 3 )
                throw new FriendlyException("Unfourtunatelly you cannot place a book for more than 30 days from today.");

            if ((finalDate.Date - initialDate.Date).Days > 2 || (finalDate.Date - initialDate.Date).Days < 0)
                throw new FriendlyException("Please, make sure you put the correct dates.");

            return true;
        }

        /// <summary>
        /// Check if room is available for specific dates
        /// </summary>
        /// <param name="initialDate">Initial Date of book</param>
        /// <param name="finalDate">Final Date of book</param>
        /// <returns></returns>
        private bool RoomAvailable(DateTime initialDate, DateTime finalDate)
        {
            var books = _bookingRepository.GetBookingsByDates(b => b.InitialDate == initialDate
                                                                || b.InitialDate == finalDate
                                                                || b.FinalDate == initialDate
                                                                || b.FinalDate == finalDate
                                                                || (b.InitialDate <= initialDate && initialDate <= b.FinalDate)
                                                                || (b.InitialDate <= finalDate && finalDate <= b.FinalDate));

            if (books.Any())
                return false;

            return true;
        }
    }
}