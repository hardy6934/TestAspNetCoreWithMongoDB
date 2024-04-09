using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using TestAspNetCoreWithMongoDB.Models;
using TestAspNetCoreWithMongoDB.Services;
using TestAspNetCoreWithMongoDB.ViewModels;

namespace TestAspNetCoreWithMongoDB.Controllers
{
    public class CarController : Controller
    {
        private readonly ICarService carService; 
        public CarController(ICarService carService)
        {
            this.carService = carService;
        }


        public IActionResult Index()
        {
            CarListViewModel viewModel = new()
            {
                Cars = carService.GetAllCars(),
            };
            return View(viewModel);
        }



        public IActionResult Add()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Add(CarAddViewModel carAddViewModel)
        {
            if (ModelState.IsValid)
            {
                Car newCar = new()
                {
                    Model = carAddViewModel.Car.Model,
                    Location = carAddViewModel.Car.Location,
                    NumberPlate = carAddViewModel.Car.NumberPlate
                };
                 
                carService.AddCar(newCar);
                return RedirectToAction("Index");
            }
             
            return View(carAddViewModel);
        }


        public IActionResult Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }


            var selectedCar = carService.GetCarById(new ObjectId(id));
            return View(selectedCar);
        }


        [HttpPost]
        public IActionResult Edit(Car car)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    carService.EditCar(car);
                    return RedirectToAction("Index");
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Updating the car failed, please try again! Error: {ex.Message}");
            }


            return View(car);
        }



        public IActionResult Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }


            var selectedCar = carService.GetCarById(new ObjectId(id));
            return View(selectedCar);
        }


        [HttpPost]
        public IActionResult Delete(Car car)
        {
            if (car.Id == null)
            {
                ViewData["ErrorMessage"] = "Deleting the car failed, invalid ID!";
                return View();
            }


            try
            {
                carService.DeleteCar(car);
                TempData["CarDeleted"] = "Car deleted successfully!";


                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewData["ErrorMessage"] = $"Deleting the car failed, please try again! Error: {ex.Message}";
            }


            var selectedCar = carService.GetCarById(car.Id);
            return View(selectedCar);
        }



    }
}
