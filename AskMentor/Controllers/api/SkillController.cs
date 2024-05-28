namespace AskMentor.Controllers.api
{
    [Route("api/[controller]")]
    [ApiController]
    public class SkillController : ControllerBase
    {

        private SkillService skillService;

        public SkillController(SkillService skillService)
        {
            this.skillService = skillService;
        }

        // GET: api/<SkillController>
        [HttpGet]
        public IActionResult getSkillsList()
        {
            List<Skill> skillList = skillService.getSkillsList();
            return Ok(new { status = true, message = "", data = skillList });
        }

        // GET api/<SkillController>/5
        [HttpGet("{id}")]
        public IActionResult getSkill(int id)
        {
            Skill skill = skillService.getSkillById(id);
            return Ok(new { status = true, message = "", data = skill });
        }

        // POST api/<SkillController>
        [HttpPost]
        public IActionResult Post([FromBody] Skill skill)
        {
            skillService.insertSkill(skill);
            return Ok(new { status = true, message = "Skill created successfully", data = skill });
        }

        // PUT api/<SkillController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Skill skill)
        {
            if (skill == null || id != skill.Id)
            {
                return BadRequest(new { status = false, message = "Invalid Skill data or ID", data = "" });
            }

            try
            {
                skillService.updateSkill(skill);
                return Ok(new { status = true, message = "Skill updated successfully", data = skill });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { status = false, message = $"An error occurred: {ex.Message}", data = "" });
            }
        }

        // DELETE api/<SkillController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                skillService.deleteSkill(id);
                return Ok(new { status = true, message = "Skill deleted successfully", data = "" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { status = false, message = $"An error occurred: {ex.Message}", data = "" });
            }
        }
    }
}
