using Microsoft.AspNetCore.Mvc;
using ServicePaymentsAPI.DTOs;
using ServicePaymentsAPI.DTOs.Requests;
using ServicePaymentsAPI.DTOs.Responses;
using System.IO.Pipelines;

namespace ServicePaymentsAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class getPaymentsController : ControllerBase
    {
        private readonly IPaymentService _paymentService;

        public getPaymentsController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        [HttpGet("getPayment")]
        public async Task<ActionResult<GetPaymentResponseDto>> getPayment([FromBody] GetPaymentRequestDto request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var response = await _paymentService.GetPaymentAsync(request);

            if (response == null || response.Count == 0)
                return StatusCode(500, response);

            return Ok(response);
        }
        [HttpGet("getCustomer")]
        public async Task<ActionResult<GetCustomerResponseDto>> getPayment([FromBody] GetCustomerRequestDto request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var response = await _paymentService.GetCustomerAsync(request);

            if (response == null || response.Count == 0)
                return StatusCode(500, response);

            return Ok(response);
        }
        [HttpGet("getProvider")]
        public async Task<ActionResult<GetProviderResponseDto>> GetProvider([FromBody] GetProviderRequestDto request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var response = await _paymentService.GetServiceProviderAsync(request);

            if (response == null || response.Count == 0)
                return StatusCode(500, response);

            return Ok(response);
        }
    }
}