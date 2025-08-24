using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MembersController(AppDbContext context) : ControllerBase
    {
        [HttpGet]
        public ActionResult<IReadOnlyList<AppUser>> GetMembers()
        {
            var members = context.Users.ToList();
            return members;
        }

        [HttpGet("{id}")]
        public ActionResult<AppUser> GetMembers(string id)
        {
            var member = context.Users.Find(id);

            if (member == null) return NotFound();

            return member;
        }
    }
}
