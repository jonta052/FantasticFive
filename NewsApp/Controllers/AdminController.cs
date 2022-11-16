using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NewsApp.Data;
using NewsApp.Models;
using NewsApp.Services;

namespace NewsApp.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<User> _userManager;
        private readonly IUserService _userService;
        private readonly SignInManager<User> _signInManager;

        public AdminController(
            RoleManager<IdentityRole> roleManager,
            UserManager<User> userManager, 
            ApplicationDbContext db, 
            IUserService userService, 
            SignInManager<User> signInManager
            )
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _db = db;
            _userService = userService;
            _signInManager = signInManager;
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
            if (string.IsNullOrEmpty(Id))
            {
                return View();
            }
            //Create list of roles for the dropdown
            var userroles = _db.Roles.ToList();
            var selectList = new SelectList(userroles, "Id", "Name");
            ViewBag.RoleType = selectList;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AssignUserRole(string roleId, string Id)
        {
            var userId = Id;

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

            return RedirectToAction("Index", "User");
        }

        // GET: AdminController/Delete/5
        public IActionResult Delete(string id)
        {
            var roleToDelete = (from r in _db.Roles where r.Id == id select r);
            return View(roleToDelete.FirstOrDefault());
        }

        // POST: AdminController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(string id, IdentityRole roleToDelete)
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

        [HttpGet]
        public IActionResult ListUsers()
        {
            var users = _userManager.Users;
            return View(users);
        }

        [HttpGet]
        public async Task<IActionResult> EditUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
            {
                ViewBag.ErrorMassage = $"User with Id ={id} cannot be found";
                //return View("NotFound");
                return NotFound();
            }

            var userRoles = await _userManager.GetRolesAsync(user);

            var model = new EditUserViewModel
            {
                Id = user.Id,
                EmailAddress = user.Email,
                UserName = user.UserName,
                FirstName=user.FirstName,
                LastName=user.LastName
               
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditUser(EditUserViewModel user)
        {
            var existingUsers = _db.Users.ToList();
            foreach (var item in existingUsers)
            {
                if (item.Email == user.EmailAddress && item.Id != user.Id)
                {
                    ModelState.AddModelError("Email", "Email already exists");
                    return View(user);
                }
               
            }
            var existingUser = _userManager.FindByIdAsync(user.Id).Result;
            existingUser.Email = user.EmailAddress;
            existingUser.UserName = user.UserName;
            existingUser.FirstName = user.FirstName;
            existingUser.LastName = user.LastName;

            var updatedUser = _userService.UpdateUser(existingUser);
            if (user.Id == _userManager.GetUserId(User))
            {
                _signInManager.RefreshSignInAsync(updatedUser).Wait();
            }
            return RedirectToAction("index", "user");
        }

      

    }
}
