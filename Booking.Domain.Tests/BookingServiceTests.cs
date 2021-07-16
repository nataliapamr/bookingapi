using Booking.Domain.Exceptions;
using Booking.Domain.Models;
using Booking.Domain.Repositories;
using Booking.Domain.Services;
using NSubstitute;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Xunit;

namespace Booking.Domain.Tests
{
    public class BookingServiceTests
    {
        private readonly IBookingRepository<Book> _bookingRepository;
        private readonly BookingService _sut;
        public BookingServiceTests()
        {
            _bookingRepository = Substitute.For<IBookingRepository<Book>>();
            _sut = new BookingService(_bookingRepository);    
        }

        [Fact]
        public void It_Should_Return_All_Bookings()
        {
            //Arrange
            var bookings = new List<Book>() {
                new Book { Id = "", RoomNumber = 1, DaysOfReservation = 3, InitialDate = DateTime.Today, FinalDate = DateTime.Today.AddDays(2) },
                new Book { Id = "", RoomNumber = 1, DaysOfReservation = 3, InitialDate = DateTime.Today, FinalDate = DateTime.Today.AddDays(2) }
            };

            _bookingRepository.GetAllBookings().Returns(bookings);

            //Act
            var result = _sut.GetAllBookings();

            //Assert
            Assert.NotNull(result);
            Assert.Equal(result.First().Id, bookings.First().Id);
        }

        [Fact]
        public void It_Should_Return_Exception_When_Searching_For_All_Bookings()
        {
            //Arrange
            var bookings = new List<Book>();

            _bookingRepository.GetAllBookings().Returns(bookings);

            //Act && Assert
            Assert.Throws<FriendlyException>(() => _sut.GetAllBookings());
        }

        [Fact]
        public void It_Should_Return_Booking_By_Id()
        {
            //Arrange
            var id = "60f0a44054530fe4e4a28f6f";
            var booking = new Book { Id = "60f0a44054530fe4e4a28f6f", RoomNumber = 1, DaysOfReservation = 3, InitialDate = DateTime.Today, FinalDate = DateTime.Today.AddDays(2) };

            _bookingRepository.GetBookingById(Arg.Any<string>()).Returns(booking);

            //Act
            var result = _sut.GetBookingById(id);

            //Assert
            Assert.NotNull(result);
            Assert.Equal(result.Id, booking.Id);
        }

        [Fact]
        public void It_Should_Return_Exception_When_Searching_For_Booking_By_Id()
        {
            //Arrange
            var id = "60f0a44054530fe4e4a28f6f";

            _bookingRepository.GetBookingById(Arg.Any<string>()).Returns((Book)null);

            //Act && Assert
            Assert.Throws<FriendlyException>(() => _sut.GetBookingById(id));
        }

        [Fact]
        public void It_Should_Place_A_Booking()
        {
            //Arrange
            var booking = new Book { RoomNumber = 1, DaysOfReservation = 3, InitialDate = DateTime.Today.AddDays(3), FinalDate = DateTime.Today.AddDays(5) };
            var bookings = new List<Book>();

            _bookingRepository.GetBookingsByDates(Arg.Any<Expression<Func<Book, bool>>>()).Returns(bookings);
            _bookingRepository.PlaceBooking(Arg.Any<Book>());

            //Act
            _sut.PlaceBooking(booking);
        }

        [Fact]
        public void It_Should_Return_Exception_When_Booking_For_Already_Booked_Dates()
        {
            //Arrange
            var booking = new Book { RoomNumber = 1, DaysOfReservation = 3, InitialDate = DateTime.Today.AddDays(1), FinalDate = DateTime.Today.AddDays(2) };
            var bookings = new List<Book>() { new Book { Id = "60f0a44054530fe4e4a28f6f", RoomNumber = 1, DaysOfReservation = 2, InitialDate = DateTime.Today.AddDays(1), FinalDate = DateTime.Today.AddDays(2) } };

            _bookingRepository.GetBookingsByDates(Arg.Any<Expression<Func<Book, bool>>>()).Returns(bookings);
            _bookingRepository.PlaceBooking(Arg.Any<Book>());

            //Act && Assert
            Assert.Throws<FriendlyException>(() => _sut.PlaceBooking(booking));
        }

        [Fact]
        public void It_Should_Return_Exception_When_Booking_For_Today()
        {
            //Arrange
            var booking = new Book { RoomNumber = 1, DaysOfReservation = 3, InitialDate = DateTime.Today, FinalDate = DateTime.Today.AddDays(2) };

            _bookingRepository.PlaceBooking(Arg.Any<Book>());

            //Act && Assert
            Assert.Throws<FriendlyException>(() => _sut.PlaceBooking(booking));
        }

        [Fact]
        public void It_Should_Delete_A_Booking()
        {
            //Arrange
            var id = "60f0a44054530fe4e4a28f6f";
            var booking = new Book { Id = "60f0a44054530fe4e4a28f6f", RoomNumber = 1, DaysOfReservation = 3, InitialDate = DateTime.Today, FinalDate = DateTime.Today.AddDays(2) };

            _bookingRepository.GetBookingById(Arg.Any<string>()).Returns(booking);
            _bookingRepository.DeleteBooking(Arg.Any<string>());

            //Act
            _sut.DeleteBooking(id);
        }

        [Fact]
        public void It_Should_Return_Exception_When_Deleting_A_Booking()
        {
            //Arrange
            var id = "60f0a44054530fe4e4a28f6f";

            _bookingRepository.GetBookingById(Arg.Any<string>()).Returns((Book)null);

            //Act && Assert
            Assert.Throws<FriendlyException>(() => _sut.DeleteBooking(id));
        }

        [Fact]
        public void It_Should_Edit_A_Booking()
        {
            //Arrange
            var id = "60f0a44054530fe4e4a28f6f";
            var booking = new Book { Id = "60f0a44054530fe4e4a28f6f", RoomNumber = 1, DaysOfReservation = 3, InitialDate = DateTime.Today, FinalDate = DateTime.Today.AddDays(2) };

            _bookingRepository.GetBookingById(Arg.Any<string>()).Returns(booking);
            _bookingRepository.EditBooking(Arg.Any<string>(), booking);

            //Act
            _sut.EditBooking(id, booking);
        }

        [Fact]
        public void It_Should_Return_Exception_When_Editing_A_Book()
        {
            //Arrange
            var id = "60f0a44054530fe4e4a28f6f";
            var booking = new Book { Id = "60f0a44054530fe4e4a28f6f", RoomNumber = 1, DaysOfReservation = 3, InitialDate = DateTime.Today, FinalDate = DateTime.Today.AddDays(2) };

            _bookingRepository.GetBookingById(Arg.Any<string>()).Returns((Book)null);

            //Act && Assert
            Assert.Throws<FriendlyException>(() => _sut.EditBooking(id, booking));
        }
    }
}
