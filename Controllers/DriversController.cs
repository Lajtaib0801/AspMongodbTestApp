using AspMongodbTestApp.Models;
using AspMongodbTestApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace AspMongodbTestApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DriversController : ControllerBase
    {
        private readonly DriverService _driverService;
        public DriversController(DriverService driverService) => _driverService = driverService;

        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<Driver>> Get(string id)
        {
            var driver = await _driverService.GetAsync(id);
            if (driver is null)
            {
                return NotFound();
            }
            return Ok(driver);
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Driver>>> Get()
        {
            var drivers = await _driverService.GetAsync();
            if (drivers.Count() == 0)
            {
                return NotFound();
            }
            return Ok(drivers);
        }
        [HttpPost]
        public async Task<ActionResult<Driver>> Post(Driver driver)
        {
            await _driverService.CreateAsync(driver);
            return CreatedAtAction(nameof(Get), new { id = driver.Id }, driver);
        }
        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Update(string id, Driver driverData)
        {
            var driver = await _driverService.GetAsync(id);
            if (driver is null)
            {
                return BadRequest();
            }
            driverData.Id = driver.Id;
            await _driverService.UpdateAsync(driverData);
            return NoContent();
        }
        [HttpDelete]
        public async Task<IActionResult> Delete(string id)
        {
            var driver = await _driverService.GetAsync(id);
            if (driver is null)
            {
                return BadRequest();
            }
            await _driverService.RemoveAsync(id);
            return NoContent();
        }
    }
}

