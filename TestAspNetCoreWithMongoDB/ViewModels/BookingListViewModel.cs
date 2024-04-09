using TestAspNetCoreWithMongoDB.Models;

namespace TestAspNetCoreWithMongoDB.ViewModels
{
    public class BookingListViewModel
    {
        public IEnumerable<Booking> Bookings { get; set; }
    }
}
