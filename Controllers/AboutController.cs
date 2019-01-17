using Microsoft.AspNetCore.Mvc;

namespace OdeToFood.Controllers
{
  [Route("about")]   //or with token [Route("[controller]")]
  public class AboutController
  {
    [Route("")]
    public string Phone()
    {
      return "1-555-555-5555";
    }

    [Route("address")]  // or with token [Route("[action]")]
    public string Address()
    {
      return "USA";
    }
  }
}