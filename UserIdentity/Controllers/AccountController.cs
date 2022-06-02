

namespace UserIdentity.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Register()
        {
            //Return RegisterViewModel
            RegisterViewModel registerViewModel = new RegisterViewModel(); 
            return View(registerViewModel);  
        }
    }
}
