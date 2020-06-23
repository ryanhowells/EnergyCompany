using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace EnergyCompanyAPI.Features.Customers.MeterReadings
{
    [ApiController]
    [Route("/customers/meter-readings")]
    public class MeterReadingsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public MeterReadingsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Upload([FromBody]Upload.Command request)
        {
            UploadMeterReadingViewModel uploadViewModel = await _mediator.Send(request);

            return Ok(uploadViewModel); 
        }
    }
}
