using Microsoft.AspNetCore.Mvc;
using OdeToFood.Models;
using OdeToFood.Services;

namespace OdeToFood.Controllers
{
  public class HomeController : Controller
  {
    private IRestaurantData _restaurantData;

    public HomeController(IRestaurantData restarauntData)
    {
      _restaurantData = restarauntData;
    }
    public IActionResult Index()
    {

      // var model = new Restaurant { Id = 1, Name = "Pizza Joe's" };
      var model = _restaurantData.GetAll();

      // return new ObjectResult(model);
      return View(model);
    }
  }
}