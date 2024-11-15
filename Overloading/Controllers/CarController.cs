using Microsoft.AspNetCore.Mvc;
using Overloading.Models;

namespace Overloading.Controllers
{
    public class CarController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(string? make, string? model, int? year)
        {
            Car car;
            if(year == null && string.IsNullOrEmpty(make) && string.IsNullOrEmpty(model))
            {
                car = new();
            }
            else if (year == null && string.IsNullOrEmpty(model))
            {
                car = new Car(make);
            }
            else if (year == null)
            {
                car = new Car(make,model);
            }
            else
            {
                car = new Car(make, model, year);
            }
            return View(car);
        }
    }
}
