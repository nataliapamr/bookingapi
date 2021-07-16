namespace Booking.Domain.Interfaces
{
    public interface IBookingDBSettings
    {
        string CollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}
