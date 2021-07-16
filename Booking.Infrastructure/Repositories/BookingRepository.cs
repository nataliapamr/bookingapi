using Booking.Domain.Interfaces;
using Booking.Domain.Models;
using Booking.Domain.Repositories;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Booking.Infrastructure.Repositories
{
    public class BookingRepository<T> : IBookingRepository<T>
        where T : Book
    {
        private readonly IMongoCollection<T> _booking;

        public BookingRepository(IBookingDBSettings settings)
        {
            var setting = MongoClientSettings.FromConnectionString(settings.ConnectionString);
            var client = new MongoClient(setting);
            var database = client.GetDatabase(settings.DatabaseName);

            _booking = database.GetCollection<T>(settings.CollectionName);
        }

        public IEnumerable<T> GetAllBookings()
        {
            var result = _booking.Find(book => true);    
            
            return result.ToEnumerable();
        }

        public T GetBookingById(string id)
        {
            var result = _booking.Find(book => book.Id == id);

            return result.ToEnumerable().FirstOrDefault();
        }

        public IEnumerable<T> GetBookingsByDates(Expression<Func<T, bool>> filter)
        {
            var result = _booking.Find(filter).ToEnumerable();

            return result;
        }

        public void PlaceBooking(T book)
        {
            _booking.InsertOne(book);
        }

        public void EditBooking(string id, T book)
        {
            book.Id = id;
            _booking.ReplaceOne(b => b.Id == book.Id, book, new ReplaceOptions { IsUpsert = true } );
        }

        public void DeleteBooking(string id)
        {
            _booking.DeleteOne(book => book.Id == id);
        }
    }
}
