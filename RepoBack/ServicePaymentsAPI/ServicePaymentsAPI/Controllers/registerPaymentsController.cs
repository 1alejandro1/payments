using Microsoft.AspNetCore.Mvc;
using ServicePaymentsAPI.DTOs;
using ServicePaymentsAPI.DTOs.Requests;
using ServicePaymentsAPI.DTOs.Responses;

namespace ServicePaymentsAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class registerPaymentsController : ControllerBase
    {
        private readonly IPaymentService _paymentService;

        public registerPaymentsController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        [HttpPost("registerPayment")]
        public async Task<ActionResult<RegisterPaymentResponseDto>> RegisterPayment([FromBody] RegisterPaymentRequestDto request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var response = await _paymentService.RegisterPaymentAsync(request);

            if (!response.Success)
                return StatusCode(500, response);

            return Ok(response);
        }
        [HttpPost("registerProvider")]
        public async Task<ActionResult<RegisterProviderResponseDto>> RegisterProvider([FromBody] RegisterProviderRequestDto request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var response = await _paymentService.RegisterServiceProviderAsync(request);

            if (!response.Success)
                return StatusCode(500, response);

            return Ok(response);
        }
        [HttpPost("registerCustomer")]
        public async Task<ActionResult<RegisterCustomerResponseDto>> RegisterCustomer([FromBody] RegisterCustomerRequestDto request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var response = await _paymentService.RegisterCustomerAsync(request);

            if (!response.Success)
                return StatusCode(500, response);

            return Ok(response);
        }
    }
}