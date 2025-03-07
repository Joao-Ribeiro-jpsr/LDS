using Friendly_.Data;
using Microsoft.AspNetCore.Mvc;
using Friendly_.Models;

namespace Friendly_.Controllers
{
    public class UserViewsController : Controller
    {

        private readonly ApplicationDbContext _db;
        public UserViewsController(ApplicationDbContext db)
        {
            _db = db;
        }

        /**
         * Este metodo permite visualizar todos utilizadores da base de dados
         */
        public IActionResult UsersList()
        {
            List<User> users = _db.User.ToList();
            return View(users);
        }

        /**
         * Este metodo permite visualizar a página para editar os utilizadores da base de dados
         */
        [HttpGet]
        public IActionResult UserEdit(int? id)
        {
            User user;

            if (id == null) return BadRequest();
            if (id <= 0) return NotFound();

            user = _db.User.Find(id);

            if (user == null) return NotFound();

            return View(user);
        }

        /**
         * Este metodo permite editar os utilizadores da base de dados
         */
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UserEdit(int id, User newuser)
        {

            if(id == null)return BadRequest();

            newuser.userID = id;


            if (!ModelState.IsValid) return View(newuser);

            _db.User.Update(newuser);
            _db.SaveChanges();

            return RedirectToAction("UsersList", "UserViews");
        }
        /**
         * Este metodo permite desativar a conta dos utilizadores da base de dados
         */
        [HttpPost, ActionName("DeactivateAccount")]
        [ValidateAntiForgeryToken]
        public IActionResult DeactivateAccount(int id)
        {

            if (id == null) return BadRequest();

            User user = _db.User.Find(id);

            if (user == null) return NotFound();

            if (user.isAccountActivated)
            {
                user.isAccountActivated = false;
            }else if(!user.isAccountActivated)
            {
                user.isAccountActivated=true;
            }
            


            _db.User.Update(user);
            _db.SaveChanges();

            return RedirectToAction("UsersList", "UserViews");
        }

        /**
         * Este metodo permite visualizar a página para apagar os utilizadores da base de dados
         */
        [HttpGet]
        public IActionResult UserDelete(int? id)
        {
            User user;

            if (id == null) return BadRequest();
            if (id <= 0) return NotFound();

            user = _db.User.Find(id);

            if (user == null) return NotFound();

            return View(user);
        }

        
        /**
         * Este metodo permite apagar os utilizadores da base de dados
         */
        [HttpPost, ActionName("UserDelete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int? id)
        {
           
            User user = _db.User.Find(id);  
            if (user == null) return NotFound();

            _db.User.Remove(user);
            _db.SaveChanges();

            return RedirectToAction("UsersList", "UserViews");
        }

    }
}
