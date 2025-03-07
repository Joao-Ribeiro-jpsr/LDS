using Friendly_.Data;
using Friendly_.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Friendly_.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserContactsController : ControllerBase
    {
        private readonly ApplicationDbContext context;

        public UserContactsController(ApplicationDbContext context) => this.context = context;

        /**
         * Este metodo permite receber os utilizadores da base de dados
         */
        [HttpGet]
        public ActionResult<IEnumerable<UserContacts>> GetUsers()
        {
            if (context.UserContacts == null) return NotFound();

            return Ok(context.UserContacts.ToList());
        }

    }
}
