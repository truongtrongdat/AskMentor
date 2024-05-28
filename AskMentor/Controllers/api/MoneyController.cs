namespace AskMentor.Controllers.api
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoneyController : ControllerBase
    {
        private MoneyService moneyService;

        public MoneyController(MoneyService moneyService)
        {
            this.moneyService = moneyService;
        }

        // GET: api/<MoneyController>
        [HttpGet]
        public IActionResult getMoneysList()
        {
            List<Money> MoneyList = moneyService.getMoneysList();
            return Ok(new { status = true, message = "", data = MoneyList });
        }

        // GET api/<MoneyController>/5
        [HttpGet("{id}")]
        public IActionResult getMoney(int id)
        {
            Money Money = moneyService.getMoneyById(id);
            return Ok(new { status = true, message = "", data = Money });
        }

        // POST api/<MoneyController>
        [HttpPost]
        public IActionResult Post([FromBody] Money money)
        {
            moneyService.insertMoney(money);
            return Ok(new { status = true, message = "Money created successfully", data = money });
        }

        // PUT api/<MoneyController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Money money)
        {
            if (money == null || id != money.Id)
            {
                return BadRequest(new { status = false, message = "Invalid Money data or ID", data = "" });
            }

            try
            {
                moneyService.updateMoney(money);
                return Ok(new { status = true, message = "Money updated successfully", data = money });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { status = false, message = $"An error occurred: {ex.Message}", data = "" });
            }
        }

        // DELETE api/<MoneyController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                moneyService.deleteMoney(id);
                return Ok(new { status = true, message = "Money deleted successfully", data = "" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { status = false, message = $"An error occurred: {ex.Message}", data = "" });
            }
        }
    }
}
