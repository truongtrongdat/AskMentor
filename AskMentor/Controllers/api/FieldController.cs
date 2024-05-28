namespace AskMentor.Controllers.api
{
    [Route("api/[controller]")]
    [ApiController]
    public class FieldController : ControllerBase
    {

        private FieldService fieldService;

        public FieldController(FieldService fieldService)
        {
            this.fieldService = fieldService;
        }

        // GET: api/<FieldController>
        [HttpGet]
        public IActionResult getFieldsList()
        {
            List<Field> fieldList = fieldService.getFieldsList();
            return Ok(new { status = true, message = "", data = fieldList });
        }

        // GET api/<FieldController>/5
        [HttpGet("{id}")]
        public IActionResult getField(int id)
        {
            Field field = fieldService.getFieldById(id);
            return Ok(new { status = true, message = "", data = field });
        }

        // POST api/<FieldController>
        [HttpPost]
        public IActionResult Post([FromBody] Field field)
        {
            fieldService.insertField(field);
            return Ok(new { status = true, message = "Field created successfully", data = field });
        }

        // PUT api/<FieldController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Field field)
        {
            if (field == null || id != field.Id)
            {
                return BadRequest(new { status = false, message = "Invalid field data or ID", data = "" });
            }

            try
            {
                fieldService.updateField(field);
                return Ok(new { status = true, message = "Field updated successfully", data = field });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { status = false, message = $"An error occurred: {ex.Message}", data = "" });
            }
        }

        // DELETE api/<FieldController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                fieldService.deleteField(id);
                return Ok(new { status = true, message = "Field deleted successfully", data = "" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { status = false, message = $"An error occurred: {ex.Message}", data = "" });
            }
        }
    }
}
