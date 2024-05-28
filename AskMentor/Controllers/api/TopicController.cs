namespace AskMentor.Controllers.api
{
    [Route("api/[controller]")]
    [ApiController]
    public class TopicController : ControllerBase
    {
        private TopicService topicService;

        public TopicController(TopicService topicService)
        {
            this.topicService = topicService;
        }

        // GET: api/<TopicController>
        [HttpGet]
        public IActionResult getTopicsList()
        {
            List<Topic> topicList = topicService.getTopicsList();
            return Ok(new { status = true, message = "", data = topicList });
        }

        // GET api/<TopicController>/5
        [HttpGet("{id}")]
        public IActionResult getTopic(int id)
        {
            Topic topic = topicService.getTopicById(id);
            return Ok(new { status = true, message = "", data = topic });
        }

        // POST api/<topicController>
        [HttpPost]
        public IActionResult Post([FromBody] Topic topic)
        {
            topicService.insertTopic(topic);
            return Ok(new { status = true, message = "Topic created successfully", data = topic });
        }

        // PUT api/<TopicController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Topic topic)
        {
            if (topic == null || id != topic.Id)
            {
                return BadRequest(new { status = false, message = "Invalid topic data or ID", data = "" });
            }

            try
            {
                topicService.updateTopic(topic);
                return Ok(new { status = true, message = "Topic updated successfully", data = topic });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { status = false, message = $"An error occurred: {ex.Message}", data = "" });
            }
        }

        // DELETE api/<topicController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                topicService.deleteTopic(id);
                return Ok(new { status = true, message = "Topic deleted successfully", data = "" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { status = false, message = $"An error occurred: {ex.Message}", data = "" });
            }
        }
    }
}
