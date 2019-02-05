using Microsoft.AspNetCore.Mvc;
using OdeToFood.Models;
using OdeToFood.Services;
using OdeToFood.ViewModels;

namespace OdeToFood.Controllers
{
    public class HomeController : Controller
    {
        private IRestaurantData _restaurantData;
        private IGreeter _greeter;
        public HomeController(IRestaurantData restarauntData,
                            IGreeter greeter)
        {
            _restaurantData = restarauntData;
            _greeter = greeter;
        }
        public IActionResult Index()
        {

            // var model = new Restaurant { Id = 1, Name = "Pizza Joe's" };
            var model = new HomeIndexViewModel();
            model.Restaurants = _restaurantData.GetAll();
            model.CurrentMessage = _greeter.GetMessageOfTheDay();

            // return new ObjectResult(model);
            return View(model);
        }

        public IActionResult Details(int id)
        {

            // return Content(id.ToString());
            var model = _restaurantData.Get(id);
            if (model == null)
            {
                return RedirectToAction("Index");
            }
            return View(model);
        }
    }
}