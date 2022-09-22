using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NewsApp.Data;
using NewsApp.Models;

namespace NewsApp.Controllers
{
    //[Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<User> _userManager;
        public AdminController(RoleManager<IdentityRole> roleManager, UserManager<User> userManager, ApplicationDbContext db)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _db = db;
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

        public IActionResult AssignUserRole(string Id)
        {
            TempData["UserId"] = Id;

            //Create list of roles for the dropdown
            var userroles = _db.Roles.ToList();
            var selectList = new SelectList(userroles, "Id", "Name");
            ViewBag.RoleType = selectList;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AssignUserRole(string roleId, Guid userId)
        {
            userId = (Guid)TempData["UserId"];
            var theUser = (from u in _db.Users where u.Id == userId.ToString() select u).FirstOrDefault();

            //Create list of roles for the dropdown
            var userroles = _db.Roles.ToList();
            var selectList = new SelectList(userroles, "Id", "Name");
            ViewBag.RoleType = selectList;

            //Does the user already have a role?
            var roles = await _userManager.GetRolesAsync(theUser);

            //If user has a role remove it
            foreach (var item in roles)
            {
                await _userManager.RemoveFromRoleAsync(theUser, item);
            }

            foreach (var item in selectList)
            {
                if (item.Value == roleId)
                {
                    await _userManager.AddToRoleAsync(theUser, item.Text);
                }
            }
            
            return View();
        }

        // GET: AdminController/Delete/5
        public ActionResult Delete(string id)
        {
            var roleToDelete = (from r in _db.Roles where r.Id == id select r);
            return View(roleToDelete.FirstOrDefault());
        }

        // POST: AdminController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(string id, IdentityRole roleToDelete)
        {
            try
            {
                roleToDelete = _db.Roles.Find(id);
                _db.Remove(roleToDelete);
                _db.SaveChanges();
                return RedirectToAction(nameof(Roles));
            }
            catch
            {
                return View();
            }
        }
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

        
    }
}
