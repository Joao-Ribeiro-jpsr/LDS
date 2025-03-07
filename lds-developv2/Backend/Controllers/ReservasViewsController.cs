using Microsoft.AspNetCore.Mvc;
using Friendly_.Data;
using Friendly_.Models;

namespace Friendly_.Controllers
{
    public class ReservasViewsController : Controller
    {

        private readonly ApplicationDbContext _db;
        public ReservasViewsController(ApplicationDbContext db)
        {
            _db = db;
        }

        /**
         * Este metodo permite listar todos as reservas da base de dados
         */
        public IActionResult ReservasList()
        {
            List<Reserva> reservas = _db.Reserva.ToList();
            return View(reservas);
        }

        /**
         * Este metodo permite cancelar as reservas os utilizadores da base de dados
         */
        [HttpPost, ActionName("CancelReserva")]
        [ValidateAntiForgeryToken]
        public IActionResult CancelReserva(int id)
        {


            Reserva reserva = _db.Reserva.Find(id);

            if (reserva == null) return NotFound();

            if (reserva.estado != "Cancelada")
            {
                reserva.estado = "Cancelada";
            }




            _db.Reserva.Update(reserva);
            _db.SaveChanges();

            return RedirectToAction("ReservasList", "ReservasViews");
        }

    }
}