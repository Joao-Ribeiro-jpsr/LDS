using Friendly_.Data;
using Friendly_.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace Friendly_.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RatingController : ControllerBase
    {

        private readonly ApplicationDbContext _db;
        public RatingController(ApplicationDbContext db)
        {
            _db = db;
        }
        //Método para obter os ratings de um determinado recinto 
        [HttpGet("{recintoid}")]
        public ActionResult<Rating> GetRatings(int recintoid)
        {
            List<Rating> ratings = _db.Ratings
                                    .Where(r => r.recintoID == recintoid)
                                    .OrderBy(r => r.recintoID)
                                    .ToList();

            if(ratings.Count <= 0 )return NotFound();

            return Ok(ratings);
        }
        //Método para adicionar um rating ao recinto 
        [HttpPost("rate")]
        public ActionResult<Rating> CreateRate(Rating rate)
        {

            if (rate == null)
            {
                return NotFound();
            }


            _db.Ratings.Add(rate);
            _db.SaveChanges();

            return Ok();
        }

    }
}
