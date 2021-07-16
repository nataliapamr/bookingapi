using Booking.Domain.Interfaces;
using Booking.Domain.Models;
using Booking.Infrastructure.Repositories;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Moq;
using NSubstitute;
using System;
using Xunit;

namespace Booking.Infrastructure.Tests
{
    public class BookingRepositoryTests
    {

        private readonly IBookingDBSettings _mongoSettings;

        private Mock<IMongoDatabase> _mockDB;

        private Mock<IMongoClient> _mockClient;

        public BookingRepositoryTests()
        {
            _mongoSettings = Substitute.For<IBookingDBSettings>();
            _mockDB = new Mock<IMongoDatabase>();
            _mockClient = new Mock<IMongoClient>();
        }

        [Fact]
        public void Test1()
        {
            //Arrange
            var settings = new BookingDBSettings()
            {
                ConnectionString = "mongodb://tes123 ",
                DatabaseName = "BookingDB",
                CollectionName = "Bookings"
            };

            _mockClient.Setup(c => c.GetDatabase(_mongoSettings.DatabaseName, null)).Returns(_mockDB.Object);

            //Act 
            var context = new BookingRepository<Book>(_mongoSettings);

            //Assert 
            Assert.NotNull(myCollection);
        }
    }
}
