using Application.Requests.Trucks;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TrucksController(IMediator mediator): ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> CreateTruck(CreateTruckRequest request)
        {
            var result = await mediator.Send(request);
            return CreatedAtAction(
                actionName: nameof(GetTruckById),
                routeValues: new { Id = result },
                value: result);
        }

        [HttpGet]
        public async Task<IActionResult> GetTrucks([FromQuery] GetTruckListRequest query)
        {
            var result = await mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetTruckById([FromRoute] GetTruckByIdRequest request)
        {
            var result = await mediator.Send(request);
            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateTruck(UpdateTruckRequest request) 
        {
            await mediator.Send(request);
            return NoContent();
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteTruck([FromRoute]RemoveTruckRequest request) 
        {
            await mediator.Send(request);
            return NoContent(); 
        }
    }
}
