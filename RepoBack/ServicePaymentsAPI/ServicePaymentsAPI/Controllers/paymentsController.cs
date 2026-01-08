using Microsoft.AspNetCore.Mvc;
using ServicePaymentsAPI.DTOs;
using ServicePaymentsAPI.DTOs.Requests;
using ServicePaymentsAPI.DTOs.Responses;

namespace ServicePaymentsAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaymentsController : ControllerBase
    {
        private readonly IPaymentService _paymentService;

        public PaymentsController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        [HttpPost("register")]
        public async Task<ActionResult<PaymentResponseDto>> Register([FromBody] PaymentRequestDto request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var response = await _paymentService.RegisterPaymentAsync(request);

            if (!response.Success)
                return StatusCode(500, response);

            return Ok(response);
        }
    }
}