using Microsoft.AspNetCore.Mvc;
using RestaurantOrderApp.Core.Interfaces.Services;
using System.Threading.Tasks;

namespace RestaurantOrderApp.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DishesController : ControllerBase
    {
        private readonly IDishService _dishService;
        public DishesController(IDishService dishService)
        {
            _dishService = dishService;
        }

        [HttpGet("timeofday/{timeOfDay}/dishTypes/{dishTypes}")]
        public async Task<IActionResult> Get(string timeOfDay, string dishTypes)
        {
            return Ok(await _dishService.GetByTimeOfDayAndTypes(timeOfDay, dishTypes));
        }
    }
}
