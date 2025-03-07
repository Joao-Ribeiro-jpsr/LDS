using FakeAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FakeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecintosController : ControllerBase
    {

        private List<Recintos> recintosList = new List<Recintos>
         {
             new Recintos
             {
                 recintoID = 1,
                 name = "Recinto1",
                 horario = new List<string> { "14:00 - 15:00", "15:00 - 16:00", "16:00 - 17:00", "17:00 - 18:00" , "18:00 - 19:00" , "21:15 - 22:00", "22:00 - 23:00" , "23:00 - 00:00" }
             },
             new Recintos
             {
                 recintoID = 2,
                 name = "Recinto2",
                 horario = new List<string> { "14:00 - 15:00", "15:00 - 16:00", "16:00 - 17:00", "17:00 - 18:00", "18:00 - 19:00", "21:00 - 22:00", "22:00 - 23:00", "23:00 - 00:00" }
             },
             new Recintos
             {
                 recintoID = 3,
                 name = "Recinto3",
                 horario = new List<string> { "14:00 - 15:00", "15:00 - 16:00", "16:00 - 17:00", "17:00 - 18:00", "18:00 - 19:00", "21:00 - 22:00", "22:00 - 23:00", "23:00 - 00:00" }
             },
             new Recintos
             {
                 recintoID = 4,
                 name = "Recinto4",
                 horario = new List<string> { "17:00 - 18:00", "18:00 - 19:00", "21:00 - 22:00", "22:00 - 23:00", "23:00 - 00:00", "00:00 - 01:00" , "01:00 - 02:00" }
             },
         };

        [HttpGet]
        public ActionResult<Recintos> GetAllRecintos(int id)
        {


            return Ok(recintosList);
        }

        [HttpGet("{id}")]
        public ActionResult<Recintos> GetRecintos(int id)
        {
            var recinto = recintosList.FirstOrDefault(r => r.recintoID == id);

            if (recinto == null)
            {
                return NotFound();
            }

            return Ok(recinto);
        }

    }
}
