using Microsoft.AspNetCore.Mvc;
using Friendly_.Models;
using Friendly_.Data;

namespace Friendly_.Controllers
    
{
    public class RecintosViewsController : Controller
    {

        private readonly ApplicationDbContext _db;
        public RecintosViewsController(ApplicationDbContext db)
        {
            _db = db;
        }

        /**
         * Este metodo permite visualizar a página as recintos à base de dados
         */
        public IActionResult AddRecintos()
        {
            return View();
        }

        /**
        * Este metodo permite adicionar  os recintos à base de dados
        */
        [HttpPost]
        public IActionResult AddRecintos(RecintoDesportivo recinto)
        {

            if(!ModelState.IsValid) { return View(recinto); }

            _db.Recintos.Add(recinto);
            _db.SaveChanges();

            return RedirectToAction("RecintosList", "RecintosViews");
        }

        /**
        * Este metodo permite listar  os recintos à base de dados
        */
        public IActionResult RecintosList()
        {
            List<RecintoDesportivo> recintos = _db.Recintos.ToList();
            return View(recintos);
        }


        /**
        * Este metodo permite visualizar a página para editar os recintos à base de dados
        */
        [HttpGet]
        public IActionResult RecintoEdit(int? id)
        {
            RecintoDesportivo recinto;

            if (id == null) return BadRequest();
            if (id <= 0) return NotFound();

            recinto = _db.Recintos.Find(id);

            if (recinto == null) return NotFound();

            return View(recinto);
        }

        /**
        * Este metodo permite editar  os recintos à base de dados
        */
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult RecintoEdit(int? id, RecintoDesportivo newRecinto)
        {

            if (id == null) return BadRequest();

            newRecinto.recintoID = (int)id;


            if (!ModelState.IsValid) return View(newRecinto);

            _db.Recintos.Update(newRecinto);
            _db.SaveChanges();

            return RedirectToAction("RecintosList", "RecintosViews");
        }


        /**
        * Este metodo permite visualizar a página de apagar os recintos à base de dados
        */
        [HttpGet]
        public IActionResult RecintoDelete(int? id)
        {
            RecintoDesportivo recinto;

            if (id == null) return BadRequest();
            if (id <= 0) return NotFound();

            recinto = _db.Recintos.Find(id);

            if (recinto == null) return NotFound();

            return View(recinto);
        }

        /**
        * Este metodo permite apagar  os recintos à base de dados
        */
        [HttpPost, ActionName("RecintoDelete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int? id)
        {

            RecintoDesportivo recinto = _db.Recintos.Find(id);
            if (recinto == null) return NotFound();

            _db.Recintos.Remove(recinto);
            _db.SaveChanges();

            return RedirectToAction("RecintosList", "RecintosViews");
        }



    }
}
