using Microsoft.AspNetCore.Mvc;
using TrackerAPI.Models;

namespace TrackerAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class VolumeController : Controller
    {
        private ModelFactory _modelFactory = new ModelFactory();
        public VolumeController()
        {

        }
    }
}
