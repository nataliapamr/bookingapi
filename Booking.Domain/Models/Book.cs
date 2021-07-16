
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace Booking.Domain.Models
{
    public class Book
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public int RoomNumber { get; set; }
        public int DaysOfReservation { get; set; }
        public DateTime InitialDate { get; set; }
        public DateTime FinalDate { get; set; }
    }
}
