using ApiTest.Commands.Students.Aggregate;
using ApiTest.Queries.Students.GetStats;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ApiTest.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly ISender _sender;

        public StudentsController(ISender sender)
        {
            _sender = sender;
        }

        /// <summary>
        /// Get students statistics
        /// </summary>
        /// <returns></returns>
        [HttpGet("stats")]
        public async Task<IActionResult> GetStudentsStats()
        {
            var result = await this._sender.Send(new GetStudentsStatsQuery());
            return Ok(result);
        }

        /// <summary>
        /// Submit students statistics
        /// </summary>
        /// <param name="aggregateStudentsCommand"></param>
        /// <returns></returns>
        [HttpPost("aggregate")]
        public async Task<IActionResult> PostStudentsStats([FromBody] AggregateStudentsCommand aggregateStudentsCommand)
        {
            var result = await this._sender.Send(aggregateStudentsCommand);
            return Ok(result);
        }
    }
}
