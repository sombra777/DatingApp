using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
        public class MembersController(AppDbContext context) : BaseApiController
    {
        [HttpGet]
        public ActionResult<IReadOnlyList<AppUser>> GetMembers()
        {
            var members = context.Users.ToList();
            return members;
        }

        [Authorize]
        [HttpGet("{id}")]
        public ActionResult<AppUser> GetMembers(string id)
        {
            var member = context.Users.Find(id);

            if (member == null) return NotFound();

            return member;
        }
    }
}
