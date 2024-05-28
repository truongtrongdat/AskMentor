namespace AskMentor.Controllers.api
{
    [Route("api/[controller]")]
    [ApiController]
    public class EvaluateController : ControllerBase
    {
        private EvaluateService evaluateService;

        public EvaluateController(EvaluateService evaluateService)
        {
            this.evaluateService = evaluateService;
        }

        // GET: api/<EvaluateController>
        [HttpGet]
        public IActionResult getEvaluatesList()
        {
            List<Evaluate> evaluateList = evaluateService.getEvaluatesList();
            return Ok(new { status = true, message = "", data = evaluateList });
        }

        // GET api/<EvaluateController>/5
        [HttpGet("{id}")]
        public IActionResult getEvaluate(int id)
        {
            Evaluate evaluate = evaluateService.getEvaluateById(id);
            return Ok(new { status = true, message = "", data = evaluate });
        }

        // POST api/<EvaluateController>
        [HttpPost]
        public IActionResult Post([FromBody] Evaluate evaluate)
        {
            evaluateService.insertEvaluate(evaluate);
            return Ok(new { status = true, message = "Evaluate created successfully", data = evaluate });
        }

        // PUT api/<EvaluateController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Evaluate evaluate)
        {
            if (evaluate == null || id != evaluate.Id)
            {
                return BadRequest(new { status = false, message = "Invalid Evaluate data or ID", data = "" });
            }

            try
            {
                evaluateService.updateEvaluate(evaluate);
                return Ok(new { status = true, message = "Evaluate updated successfully", data = evaluate });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { status = false, message = $"An error occurred: {ex.Message}", data = "" });
            }
        }

        // DELETE api/<EvaluateController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                evaluateService.deleteEvaluate(id);
                return Ok(new { status = true, message = "Evaluate deleted successfully", data = "" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { status = false, message = $"An error occurred: {ex.Message}", data = "" });
            }
        }

        [HttpPost("Rating/{userId}/{rating}/{mentorId}")]
        public async Task<IActionResult> Rating(string userId, int rating, string mentorId, [FromBody] EvaluateRatingDto request)
        {
            try
            {
                await evaluateService.RatingAsync(userId, rating, mentorId, request.Content);
                return Ok(new { status = true, message = "Evaluate rating successfully", data = "" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { status = false, message = $"An error occurred: {ex.Message}", data = "" });
            }
        }
    }
}
