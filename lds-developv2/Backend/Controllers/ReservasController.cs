using Friendly_.Data;
using Friendly_.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Friendly_.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservasController : Controller
    {
        private readonly ApplicationDbContext _db;
        public ReservasController(ApplicationDbContext db)
        {
            _db = db;
        }

        /**
         * Este metodo permite obter uma reserva da base de dados
         */
        [HttpGet("{id}")]
        public ActionResult<Reserva> GetReserva(int id)
        {
            if (_db.Reserva == null) return NotFound();

            Reserva reserva = _db.Reserva.SingleOrDefault(x => x.reservaID == id);

            if (reserva == null) return NotFound();

            return Ok(reserva);

        }

        /**
         * Este metodo permite obter  as reservas da base de dados
         */
        [HttpGet]
        public ActionResult<IEnumerable<Reserva>> GetReservas()
        {
            if (_db.Reserva == null) return NotFound();

            return Ok(_db.Reserva.ToList());
        }

        /**
         * Este metodo permite adicionar  as reservas à base de dados
         */
        [HttpPost("createreserva")]
        public ActionResult<Reserva> CreateReserva(Reserva newReserva)
        {
            if (newReserva == null)
            {
                return NotFound();
            }

            List<Reserva> reservasNoMesmoRecinto = _db.Reserva
                .Where(r => r.recintoID == newReserva.recintoID && r.dataInicial == newReserva.dataInicial
                && r.horaJogo == newReserva.horaJogo)
                .OrderBy(r => r.reservaID)
                .ToList();

            if (reservasNoMesmoRecinto.Any())
            {
                // Verifica se há pelo menos uma reserva confirmada ou pendente
                bool existeReservaConfirmadaOuPendente = reservasNoMesmoRecinto.Any(r => r.estado == "Confirmada" || r.estado == "Pendente");

                if (existeReservaConfirmadaOuPendente)
                {
                    return BadRequest("Já existe uma reserva confirmada ou pendente para essa hora!");
                }
            }

            _db.Reserva.Add(newReserva);
            _db.SaveChanges();

            return Ok(newReserva.reservaID);
        }



        /*
         * Este método recebe uma reserva e verifica se ela ja foi efetuada à mais de uma hora.
         */
        public static bool VerificarTempoExpirado(Reserva reserva)
        {

            if(reserva.estado == "Pendente")
            {
                DateTime horaAtual = DateTime.Now;

                DateTime horaReserva = DateTime.ParseExact(reserva.horaReserva, "HH:mm", null);

                TimeSpan diferenca = horaAtual - horaReserva;

                bool tempoExpirado = diferenca.TotalHours > 1;

                return tempoExpirado;
            }
            return false;
        }
        /*
         * Este método encontra todas as reservas com o estado pendente que estão expiradas e
           passa o estado para cancelada.
         */
        [HttpPut("reservaExpirada")]
        public ActionResult<Reserva> ReservaExpirada(Reserva[] reservas)
        {
            for (int i = 0; i < reservas.Length; i++)
            {
                if (reservas[i].estado == "Pendente" && VerificarTempoExpirado(reservas[i]))
                {
                    Reserva reserva = reservas[i];
                    reserva.estado = "Cancelada";

                    DateTime horaAtual = DateTime.Now;
                    reserva.horaCancelamento = horaAtual.ToString();

                    _db.Reserva.Update(reserva);
                    _db.SaveChanges();
                }
            }

            return Ok(reservas);
        }
        /*
         * Este método encontra as reservas pelo id do utilizador e retorna a lista das mesmas.
         */
        [HttpGet("reserveHistory/{userid}")]
        public ActionResult<Reserva> ReserveHistory(int userid)
        {

            List<Reserva> reservas = _db.Reserva
                                    .Where(r => r.userID == userid)
                                    .OrderBy(r => r.recintoID)
                                    .ToList();

            foreach (Reserva reserva in reservas)
            {

               if(reserva.estado == "Pendente")
                {
                    if (VerificarTempoExpirado(reserva) == true)
                    {
                        reserva.estado = "Cancelada";
                        _db.Reserva.Update(reserva);
                        _db.SaveChanges();

                    }
                }

            }

            return Ok(reservas);
        }
        /*
         * Este método encontra as reservas com o estado pendente e retorna a lista das mesmas.
         */
        [HttpGet("pendentReserves/{userid}")]
        public ActionResult<Reserva> GetPendentReserves(int userid)
        {
            List<Reserva> reservas = _db.Reserva
                                        .Where(r => r.userID == userid && r.estado == "Pendente")
                                        .OrderBy(r => r.reservaID)
                                        .ToList();

            return Ok(reservas.Count);
        }
        /*
         * Este método recebe o id de uma reserva e se o estado dessa reserva for pendente então muda o
          estado para cancelada.
         */
        [HttpPost("cancelarreserva/{id}")]
        public ActionResult CancelarReserva(int id)
        {
            Reserva reserva = _db.Reserva.Find(id);

            if (reserva == null)
            {
                return NotFound("Reserva não encontrada");
            }
           
            if(reserva.estado == "Pendente")
            {
                reserva.estado = "Cancelada";

                DateTime horaAtual = DateTime.Now;
                reserva.horaCancelamento = horaAtual.ToString();
            }

            _db.Reserva.Update(reserva);
            _db.SaveChanges();

            return Ok();
        }

    }
}
