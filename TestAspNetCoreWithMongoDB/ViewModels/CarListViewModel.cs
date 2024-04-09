using TestAspNetCoreWithMongoDB.Models;

namespace TestAspNetCoreWithMongoDB.ViewModels
{
    public class CarListViewModel
    {
        public IEnumerable<Car> Cars { get; set; }
    }
}
