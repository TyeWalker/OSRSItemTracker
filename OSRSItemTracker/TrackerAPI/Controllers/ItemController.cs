using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TrackerAPI.Entities;
using TrackerAPI.Models;
using TrackerAPI.Services;

namespace TrackerAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class ItemController : Controller
    {
        private ModelFactory _modelFactory = new ModelFactory();
        private readonly IItemService _itemService;
        public ItemController(IItemService itemService)
        {
            _itemService = itemService;
        }

        #region GET
        // Get individual item
        [HttpGet("itemId/{itemId}")]
        public async Task<IActionResult> GetByItemId(long itemId)
        {
            var result = await _itemService.GetByItemId(itemId);
            if (result == null)
                return NotFound("Item not found");

            return Ok(result);
        }

        // Get individual item by db Id
        [HttpGet("id/{Id}")]
        public async Task<IActionResult> GetById(long Id)
        {
            var result = await _itemService.GetById(Id);
            if (result == null)
                return NotFound("Item not found");

            return Ok(result);
        }

        [HttpGet()]
        public async Task<IActionResult> Get()
        {
            var result = await _itemService.GetAll();
            if (result == null)
                return NotFound("Could not find items");

            return Ok(result);
        }

        #endregion

        #region POST

        // create a new item
        [HttpPost()]
        public async Task<IActionResult> Post([FromBody] ItemPostModel model)
        {
            if (model == null)
                return BadRequest("Post model is null");

            var existingItem = await _itemService.GetByItemId(model.ItemId);
            if (existingItem != null)
                return BadRequest("Item already exists, use PUT/PATCH to update the item.");

            if (model.Name == null)
                return BadRequest("Every item requires a name");

            var created = _modelFactory.Create(model);
            if (created == null)
                return BadRequest("Error creating item");

            var result = await _itemService.Create(created);

            if (result == null)
                return BadRequest("Server Error");

            return Ok(result);
        }

        [HttpPost("batch")]
        public async Task<IActionResult> Post([FromBody] List<ItemPostModel> models)
        {
            if (models == null)
                return BadRequest("Post model is null");

            List<ItemEntity> items = new List<ItemEntity>();
            foreach (var item in models)
            {
                var created = _modelFactory.Create(item);
                if (created == null)
                    return BadRequest("Error creating item");
                items.Add(created);
            }

            var result = await _itemService.CreateBatch(items);

            return Ok(items);
        }
        #endregion

        #region PUTPATCH

        [HttpPut("{itemId}")]
        public async Task<IActionResult> Put(long itemId, [FromBody] ItemPostModel model)
        {
            var existingItem = await _itemService.GetByItemId(itemId);
            if (existingItem == null)
                return NotFound("Item not found");

            var created = _modelFactory.Put(existingItem, model);
            if (created == null)
                return BadRequest("Error updating item");

            var result = await _itemService.Update(created);
            if (result == null)
                return BadRequest("Server Error");

            return Ok(result);
        }


        [HttpPatch("{itemId}")]
        public async Task<IActionResult> Patch(long itemId, [FromBody] ItemPostModel model)
        {
            var existingItem = await _itemService.GetByItemId(itemId);
            if (existingItem == null)
                return NotFound("Item not found");

            var created = _modelFactory.Patch(existingItem, model);
            if (created == null)
                return BadRequest("Error updating item");

            var result = await _itemService.Update(created);
            if (result == null)
                return BadRequest("Server Error");

            return Ok(result);
        }

        #endregion

        #region DELETE

        [HttpDelete("{itemId}")]
        public async Task<IActionResult> Delete(long itemId)
        {
            var existingItem = await _itemService.GetByItemId(itemId);
            if (existingItem == null)
                return NotFound("Item not found");

            var result = await _itemService.DeleteById(existingItem.Id);
            if (result == null)
                return BadRequest("Server Error");

            return Ok();
        }

        #endregion
    }
}
