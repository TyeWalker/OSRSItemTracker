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
    public class VolumeController : Controller
    {
        private ModelFactory _modelFactory = new ModelFactory();
        private readonly IVolumeService _volumeService;
        private readonly IItemService _itemService;
        public VolumeController(IVolumeService volumeService, IItemService itemService)
        {
            _volumeService = volumeService;
            _itemService = itemService;
        }

        #region GET
        // Get last hour for item
        [HttpGet("lasthour/{itemId}")]
        public async Task<IActionResult> Get(long itemId)
        {
            var result = await _volumeService.GetLastHourVolumeForItem(itemId);
            if (result == null)
                return NotFound("Volume for Item not Found");

            return Ok();
        }

        // Get last hour for all items
        [HttpGet("/lasthour")]
        public async Task<IActionResult> Get()
        {
            var result = await _volumeService.GetLastHourForAllItems();
            if (result == null || result.Count == 0)
                return NotFound();

            return Ok(result);
        }

        #endregion

        #region POST

        // create a new volume entity
        [HttpPost("{itemId}")]
        public async Task<IActionResult> Post(long itemId, [FromBody] VolumePostModel model)
        {
            if (model == null)
                return BadRequest("Volume model is null");
            if (model.ItemId == 0)
                return BadRequest("ItemId cannot be 0");
            if (itemId != model.ItemId)
                return BadRequest("ItemId does not match request URL");

            var existingItem = _itemService.GetByItemId(itemId);
            if (existingItem.Result == null)
                return NotFound("Item does not exist");
            var created = _modelFactory.Create(model);

            var existingVolume = await _volumeService.VerifyVolumeDoesNotExist(created);
            if (existingVolume != null)
                return BadRequest("Volume already exists.");

            var result = await _volumeService.Create(created);
            if (result == null)
                return BadRequest("Server Error");

            return Ok(result);
        }

        // create a batch of volume entities
        [HttpPost("batch")]
        public async Task<IActionResult> Post([FromBody] List<VolumePostModel> postModels)
        {
            List<VolumeEntity> volumeEntities = new List<VolumeEntity>();
            foreach (var model in postModels)
            {
                var volume = _modelFactory.Create(model);
                if (volume == null)
                    return BadRequest("Error in post models");
                volumeEntities.Add(volume);
            }
            var result = await _volumeService.CreateBatch(volumeEntities);
            if (result == null)
                return BadRequest("Server error");


            return Ok(result);
        }

        #endregion

        #region PUTPATCH
        [HttpPut("{itemId}")]
        public async Task<IActionResult> Put(long itemId, [FromBody] VolumePostModel model)
        {
            // validation
            // update


            return Ok();
        }


        [HttpPatch("{itemId}")]
        public async Task<IActionResult> Patch(long itemId, [FromBody] VolumePostModel model)
        {
            // validation
            // update


            return Ok();
        }

        #endregion

        #region DELETE

        [HttpDelete("{itemId}")]
        public async Task<IActionResult> Delete(long itemId)
        {
            // find item and delete

            return Ok();
        }

        #endregion
    }
}
