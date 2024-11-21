using Microsoft.AspNetCore.Mvc;
using restAPI.Services;
using restAPI.Models;
using Microsoft.AspNetCore.Http.HttpResults;

namespace restAPI.Controllers
{
    public class PointsController : Controller
    {
        //Read only Transaction Service
        private readonly PointsService _pointsService;

        //Dependency inject the pointsService
        public PointsController(PointsService pointsService)
        {
            _pointsService = pointsService;
        }

        //POST /add - Add points transactions
        [HttpPost("add")]
        public IActionResult AddTransaction([FromBody] Transaction transaction)
        {
            if (transaction == null)
            {
                return BadRequest("Invalid transaction data");
            }

            _pointsService.AddTransaction(transaction);   

            return Ok();
        }

        //POST /spend - Spend points
        [HttpPost("spend")]
        public IActionResult SpendPoints([FromBody] SpendRequest request)
        {
            if (request.Points <= 0)
            {
                return BadRequest("The number of points to spend must be positive.");
            }

            try
            {
                var result = _pointsService.SpendPoints(request.Points);

                return Ok(result);
            }
            catch (InvalidOperationException)
            {
                return BadRequest("User does not have enough points.");
            }
        }

        //GET /balance - Gets points balance
        [HttpGet("balance")]
        public IActionResult GetBalance()
        {
            var balance = _pointsService.GetPointsBalance();
            return Ok(balance);
        }
    }
}
