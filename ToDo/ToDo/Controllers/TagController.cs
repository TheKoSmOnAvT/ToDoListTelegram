using Microsoft.AspNetCore.Mvc;
using ToDo.Model;
using ToDo.Service;

namespace ToDo.Controllers
{
    [Route("/tag")]
    public class TagController : Controller
    {
        private readonly TagService tagService;
        public TagController(TagService tagService)
        {
            this.tagService = tagService;
        }

        [HttpGet]
        public List<TagModel> GetTags()
        {
            return tagService.Get();
        }

        [HttpPost]
        public int AddTag([FromBody]TagModel newTag)
        {
            return tagService.Create(newTag);
        }

        [HttpPut]
        public bool ChangeTag([FromBody] TagModel newTag)
        {
            return tagService.Change(newTag);
        }

        [HttpDelete("/{id}")]
        public bool DeleteTag([FromQuery] int id)
        {
            return tagService.Remove(id);
        }
    }
}
