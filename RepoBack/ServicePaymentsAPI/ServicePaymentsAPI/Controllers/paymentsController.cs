using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace ServicePaymentsAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class paymentsController : ControllerBase
    {
        public paymentsController(IScoreService ScoreService)
        {
            this.ScoreService = ScoreService;
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
