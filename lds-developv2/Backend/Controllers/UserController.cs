using Friendly_.Data;
using Friendly_.Interfaces;
using Friendly_.Models;
using Microsoft.AspNetCore.Mvc;

namespace Friendly_.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        public readonly IMailSendService _mailService;

        public UserController(ApplicationDbContext context, IMailSendService mailService)
        {
            this.context = context;
            this._mailService = mailService;
        }

        /**
         * Este metodo permite obter todos utilizadores da base de dados
         */
        [HttpGet]
        public ActionResult<IEnumerable<User>> GetUsers()
        {
            if (context.User == null) return NotFound();

            return Ok(context.User.ToList());
        }

        /**
         * Este metodo permite apgar os utilizadores da base de dados
         */
        [HttpGet("{id}")]
        public ActionResult<User> GetUser(int id)
        {
            if (context.User == null) return NotFound();

            User user = context.User.SingleOrDefault(x => x.userID == id);
            if (user == null) return NotFound();

            return Ok(user);

        }

        /**
         * Este metodo permite o utilizador se registar na plataforma e
           envia um email para o utilizador confirmar o seu email.
         */
        [HttpPost("register")]
        public async Task<ActionResult<User>> RegisterUser(User newUser)
        {
            if (newUser == null) return NotFound();

            context.User.Add(newUser);
            context.SaveChanges();
            
            
            try
            {
                String templatePath = "Views/EmailTemplates/EmailConfirmation.html";
                string emailBody = System.IO.File.ReadAllText(templatePath);
                emailBody = emailBody.Replace("[NOME]", newUser.nome);
                emailBody = emailBody.Replace("[SUBJECT]", "Confirmação de email");
                emailBody = emailBody.Replace("[USERID]", newUser.userID.ToString());
                await _mailService.SendMailAsync(newUser.email, "Confirmação de email", emailBody, "");

            }
            catch (Exception)
            {
                return StatusCode(500, "Email não enviado!");
            }

            return Ok(newUser.userID);
        }


        public class LoginModel
        {
            public string? email { get; set; }
            public string? password { get; set; }
            public string? userId { get; set; }
        }

        /**
         * Este metodo permite realizar o login dos utilizadores.
         */
        [HttpPost("login")]
        public ActionResult<User> LoginUser([FromBody] LoginModel model)
        {
            var user = context.User.FirstOrDefault(u => u.email == model.email && u.password == model.password);

            var contextHttp = HttpContext;

            string userId = contextHttp.Request.Query["userId"];

            if (user == null)
            {
                return NotFound("Invalid email or password");
            }
            try
            {
                if (!string.IsNullOrEmpty(userId) && int.TryParse(userId, out int userIdValue))
                {
                    if (userIdValue == user.userID)
                    {
                        user.isAccountActivated = true;
                        context.User.Update(user);
                        context.SaveChanges();

                    }
                }
            }
            catch(Exception ex)
            {
                return StatusCode(500, "Error updating user: " + ex);
            }
            if (user.isAccountActivated == false)
            {
                return StatusCode(401, "Valide o email primeiro!");
            }
            return Ok( user.userID);
        }

        /**
         * Este metodo permite realizar o login dos utilizadores da base de dados
         */
        [HttpPost("loginMovel")]
        public async Task<ActionResult<User>> LoginUserCMU([FromBody] LoginModel model)
        {
            var user = context.User.FirstOrDefault(u => u.email == model.email && u.password == model.password);

            if (user == null)
            {
                return NotFound("Invalid email or password");
            }

            return Ok(user);
        }

        /**
         * Este metodo permite registar um utilizador através da app móvel.
         */
        [HttpPost("registoMovel")]
        public async Task<ActionResult<User>> RegistoUserCMU(User user)
        {

            if (user == null)
            {
                return NotFound("Não foi possivel criar o utilizador!");
            }

            context.User.Add(user);
            context.SaveChanges();

            return Ok(user);
        }


        /**
         * Este metodo permite atualizar os utilizadores do frontend base de dados
         */
        [HttpPost("editar/{id}")]
        public ActionResult<User> UpdateUser(User updatedUser, int id)
        {
            if (id == null) return NotFound();
            if (updatedUser == null) return NotFound();

            if (id != updatedUser.userID) return BadRequest();


            context.User.Update(updatedUser);
            context.SaveChanges();

            return Ok(updatedUser);
        }


    }
}
