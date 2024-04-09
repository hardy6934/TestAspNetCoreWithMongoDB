using MongoDB.Bson;
using TestAspNetCoreWithMongoDB.Models;

namespace TestAspNetCoreWithMongoDB.Services
{
    public interface ICarService
    {
        IEnumerable<Car> GetAllCars();
        Car? GetCarById(ObjectId id);

        void AddCar(Car newCar);

        void EditCar(Car updatedCar);

        void DeleteCar(Car carToDelete);

    }
}
