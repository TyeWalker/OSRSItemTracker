using Microsoft.AspNetCore.Mvc;
using TrackerAPI.Models;

namespace TrackerAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PriceController : Controller
    {
        private ModelFactory _modelFactory = new ModelFactory();

        public PriceController()
        {

        }
    }
}
