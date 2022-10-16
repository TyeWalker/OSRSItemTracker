using Microsoft.AspNetCore.Mvc;
using TrackerAPI.Data;
using TrackerAPI.Models;
using TrackerAPI.Services;

namespace TrackerAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PriceController : Controller
    {
        private ModelFactory _modelFactory = new ModelFactory();
        private readonly IPriceService _priceService;
        public PriceController(IPriceService priceService)
        {
            _priceService = priceService;
        }

        #region GET
        // Get latest price for item
        [HttpGet("itemId/{itemId}")]
        public async Task<IActionResult> GetByItemId(long itemId)
        {
            var result = await _priceService.GetByItemId(itemId);
            if (result == null)
                return NotFound("Item not found");

            return Ok(result);
        }

        // Get latest price by db Id
        [HttpGet("id/{Id}")]
        public async Task<IActionResult> GetById(long Id)
        {
            var result = await _priceService.GetById(Id);
            if (result == null)
                return NotFound("Item not found");

            return Ok(result);
        }

        [HttpGet()]
        public async Task<IActionResult> Get()
        {
            var result = await _priceService.GetAll();
            if (result == null)
                return NotFound("Could not find items");

            return Ok(result);
        }

        #endregion

        #region POST

        // insert new price
        [HttpPost()]
        public async Task<IActionResult> Post([FromBody] ItemPricePostModel model)
        {
            if(model.ItemId == 0)
                return BadRequest("ItemId is 0.");
            if (model == null)
                return BadRequest("Post model is null");

            var created = _modelFactory.Create(model);
            if (created == null)
                return BadRequest("Error creating item");
            // check if latest price already exists

            var existingBuyPrice = await _priceService.VerifyPriceDoesNotExist(created[0]);
            var existingSellPrice = await _priceService.VerifyPriceDoesNotExist(created[1]);

            if (existingBuyPrice != null && existingSellPrice != null)
                return BadRequest("Item already exists, use PUT/PATCH to update the item.");

            else
            {
                var buyResult = await _priceService.Create(created[0]);
                var sellResult = await _priceService.Create(created[1]);
                if (buyResult == null && sellResult == null)
                    return BadRequest("Buy and sell price already exist");
                return Ok(created);
            }
        }

        #endregion

        #region PUTPATCH

        [HttpPut("{Id}")]
        public async Task<IActionResult> Put(long Id, [FromBody] PricePostModel model)
        {
            var existingItem = await _priceService.GetById(Id);
            if (existingItem == null)
                return NotFound("Price not found");

            var created = _modelFactory.Put(existingItem, model);
            if (created == null)
                return BadRequest("Error updating price");

            var result = await _priceService.Update(created);
            if (result == null)
                return BadRequest("Server Error");

            return Ok(result);
        }

        [HttpPatch("{Id}")]
        public async Task<IActionResult> Patch(long Id, [FromBody] PricePostModel model)
        {
            var existingItem = await _priceService.GetById(Id);
            if (existingItem == null)
                return NotFound("Price not found");

            var created = _modelFactory.Patch(existingItem, model);
            if (created == null)
                return BadRequest("Error updating price");

            var result = await _priceService.Update(created);
            if (result == null)
                return BadRequest("Server Error");

            return Ok(result);
        }

        #endregion

        #region DELETE

        [HttpDelete("{Id}")]
        public async Task<IActionResult> Delete(long Id)
        {
            var existingItem = await _priceService.GetById(Id);
            if (existingItem == null)
                return NotFound("Item not found");

            var result = await _priceService.DeleteById(existingItem.Id);
            if (result == null)
                return BadRequest("Server Error");

            return Ok();
        }

        #endregion

    }
}
