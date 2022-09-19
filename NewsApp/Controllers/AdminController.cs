using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NewsApp.Data;
using NewsApp.Models;

namespace NewsApp.Controllers
{
    //[Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _db;

        public AdminController(ApplicationDbContext db)
        {
            _db = db;
        }

        private readonly RoleManager<IdentityRole> _roleManager;
        public AdminController(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }
        public IActionResult Index()
        {

            return View();
        }
        public IActionResult Roles()
        {
            var roles = _roleManager.Roles.ToList();

            return View(roles);
        }

        public IActionResult CreateRole()
        {

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateRole(string roleName)
        {
            if (!string.IsNullOrEmpty(roleName))
            {
                var roleExists = await _roleManager.RoleExistsAsync(roleName);

                if (!roleExists)
                {
                    await _roleManager.CreateAsync(new IdentityRole(roleName));
                }

                return RedirectToAction(nameof(Roles));
            }

            return View();

        }

       /* public IActionResult AssignUserRole(int userId)
        {
            return View();
        }
        [HttpPost]
        public async Task AssignUserRole(User user)
        {
            var theUser = (from u in _db.Users where u.Email == user.Email select u).FirstOrDefault();

            //Do the user already have a role?
            var roles = await _db.UserRoles.FindAsync(theUser.Id);

            if (roles != null)
            {
                _db.UserRoles.Remove(new IdentityUserRole<string>
                { UserId = theUser.Id });
            }

            _db.UserRoles.Add(new IdentityUserRole<string>
            {
                RoleId = 
                UserId = theUser.Id
            });


            return View();
        }*/
        //Stuff that we might not need below this line
        // GET: AdminController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: AdminController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AdminController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: AdminController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: AdminController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: AdminController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: AdminController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
