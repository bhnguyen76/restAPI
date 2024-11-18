using Microsoft.AspNetCore.Mvc;
using restAPI.Services;
using restAPI.Models;
using Microsoft.AspNetCore.Http.HttpResults;

namespace restAPI.Controllers
{
    public class PointsController : Controller
    {
        //Read only Transaction Service
        private readonly TransactionService _transactionService;

        //Dependency inject the TransactionService
        public PointsController(TransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        [HttpPost("add")]
        public IActionResult AddTransaction([FromBody] Transaction transaction)
        {
            if (transaction == null)
            {
                return BadRequest("Invalid transaction data");
            }

            _transactionService.AddTransaction(transaction);   

            return Ok();
        }

        [HttpPost("spend")]
        public IActionResult SpendPoints([FromBody] SpendRequest request)
        {
            if (request.Points <= 0)
            {
                return BadRequest("The number of points to spend must be positive.");
            }

            try
            {
                var result = _transactionService.SpendPoints(request.Points);

                return Ok(result);
            }
            catch (InvalidOperationException)
            {
                return BadRequest("User does not have enough points.");
            }
        }

        [HttpGet("balance")]
        public IActionResult GetBalance()
        {
            var balance = _transactionService.GetPointsBalance();

            return Ok(balance);
        }
    }
}
