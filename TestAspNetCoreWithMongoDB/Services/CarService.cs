﻿using Microsoft.EntityFrameworkCore;
using MongoDB.Bson;
using TestAspNetCoreWithMongoDB.Models;

namespace TestAspNetCoreWithMongoDB.Services
{
    public class CarService: ICarService
    {
        private readonly CarBookingDbContext _carDbContext;
        public CarService(CarBookingDbContext carDbContext)
        {
            _carDbContext = carDbContext;
        }

        public IEnumerable<Car> GetAllCars()
        {
            return _carDbContext.Cars.OrderBy(c => c.Id).AsNoTracking().AsEnumerable();
        }

        public Car? GetCarById(ObjectId id)
        {
            return _carDbContext.Cars.FirstOrDefault(c => c.Id == id);
        }

        public void AddCar(Car car)
        {
            _carDbContext.Cars.Add(car);

            _carDbContext.ChangeTracker.DetectChanges();
            Console.WriteLine(_carDbContext.ChangeTracker.DebugView.LongView);

            _carDbContext.SaveChanges();
        }

        public void EditCar(Car car)
        {
            var carToUpdate = _carDbContext.Cars.FirstOrDefault(c => c.Id == car.Id);

            if (carToUpdate != null)
            {
                carToUpdate.Model = car.Model;
                carToUpdate.NumberPlate = car.NumberPlate;
                carToUpdate.Location = car.Location;
                carToUpdate.IsBooked = car.IsBooked;

                _carDbContext.Cars.Update(carToUpdate);

                _carDbContext.ChangeTracker.DetectChanges();
                Console.WriteLine(_carDbContext.ChangeTracker.DebugView.LongView);

                _carDbContext.SaveChanges();

            }
            else
            {
                throw new ArgumentException("The car to update cannot be found. ");
            }
        }


        public void DeleteCar(Car car)
        {
            var carToDelete = _carDbContext.Cars.Where(c => c.Id == car.Id).FirstOrDefault();

            if (carToDelete != null)
            {
                _carDbContext.Cars.Remove(carToDelete);
                _carDbContext.ChangeTracker.DetectChanges();
                Console.WriteLine(_carDbContext.ChangeTracker.DebugView.LongView);
                _carDbContext.SaveChanges();
            }
            else
            {
                throw new ArgumentException("The car to delete cannot be found.");
            }
        }


    }
}
