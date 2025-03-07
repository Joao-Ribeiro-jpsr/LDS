using Friendly_.Data;
using Friendly_.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace Friendly_.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecintosController : Controller
    {
        private readonly ApplicationDbContext _db;
        public RecintosController(ApplicationDbContext db)
        {
            _db = db;    
        }

        /**
        * Este metodo permite retornar os recintos presentes na base de dados
        */
        [HttpGet]
        public ActionResult<IEnumerable<RecintoDesportivo>> GetRecintos()
        {
            if(_db.Recintos == null)return NotFound();

            return Ok(_db.Recintos.ToList());
        }

        /**
        * Este metodo permite retornar um recinto da base de dados
        */
        [HttpGet("{id}")]
        public ActionResult<RecintoDesportivo> GetRecinto(int id)
        {
            if(_db.Recintos == null)return NotFound();

            var recinto = _db.Recintos.SingleOrDefault(x=> x.recintoID == id);
            if(recinto == null) return NotFound();

            return Ok(recinto);
        }

       


    }
}
