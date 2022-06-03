

namespace UserIdentity.Controllers
{
    public class AccountController : Controller
    {
        //UserManager 
        private readonly UserManager<IdentityUser> _userManager;
        //SignInManager
        private readonly SignInManager<IdentityUser> _signInManager;

        //Getting UserManager in the constructor using Dependency Injection 
        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;

        }

        public IActionResult Index()
        {
            return View();
        }

        //Register GET
        [HttpGet]
        public async Task<IActionResult> Register(string? returnurl = null)
        {
            //Return RegisterViewModel
            ViewData["ReturnUrl"] = returnurl;
            RegisterViewModel registerViewModel = new RegisterViewModel(); 
            return View(registerViewModel);  
        }

        //Register POST
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model, string? returnurl = null)
        {
            ViewData["ReturnUrl"] = returnurl; 
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            //Populating new user object here
            var user = new ApplicationUser { UserName = model.Email, Email = model.Email, Name = model.Name };
            //Create a user || Using the UserManageService Provided by ASP.NET
            var result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                //This means the user is created 
                //In order to sign in the user we need to use SignInManager || We are gonna use it using Dependency Injection 
                await _signInManager.SignInAsync(user, isPersistent: false);
                if (returnurl != null)
                {
                    return LocalRedirect(returnurl);
                }
                return RedirectToAction("Index", "Home");
            }
            //Creating a helper action method below to add errors 
            AddErrors(result);

            //Return back to view 
            return View(model);


        }

        //LogOff  
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LogOff()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        //Login GET
        [HttpGet]
        public  IActionResult Login(string? returnurl=null)
        {
            ViewData["ReturnUrl"] = returnurl;
            return View();
        }

        //Login POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model, string? returnurl = null)
        {
            ViewData["ReturnUrl"] = returnurl; 
           if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure:true);
                if(result.Succeeded)
                {
                    if (returnurl != null)
                    {
                        return LocalRedirect(returnurl);
                    }
                    return RedirectToAction("Index", "Home");
                }
                else if(result.IsLockedOut)
                {
                    return View("Lockout");
                }
                else
                {
                    ModelState.AddModelError(String.Empty, "Invalid Login Attempt!"); 
                }
            }
           return View(model);

        }


        //Add Errors 
        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description); 
            }
        }
    }
}
