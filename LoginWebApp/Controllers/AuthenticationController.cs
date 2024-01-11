using System.Web.Mvc;
using LoginWebApp.Models;
using LoginWebApp.Services;
using System.Threading.Tasks;
using LoginWebApp.Service;

namespace LoginWebApp.Controllers
{
    public class AuthenticationController : Controller
    {
        private readonly UserService _userService;

        public AuthenticationController()
        {
            _userService = new UserService();
        }

        // GET: Authentication/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View(new LoginViewModel());
        }

        // POST: Authentication/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            string passwordHash = UtilityService.ConvertirSHA256(model.Password);
            var user = _userService.ValidateUser(model.Email, passwordHash);

            if (user != null)
            {
                if (!user.IsConfirmed)
                {
                    ViewBag.Message = $"Debe confirmar su cuenta para poder iniciar sesión. Un correo electrónico de confirmación fue enviado a {model.Email}.";
                    return View(model);
                }
                else
                {
                    // Implementa aquí la lógica para crear la cookie de sesión si estás utilizando la autenticación basada en formularios.
                    // Si estás utilizando ASP.NET Identity, aquí usarías SignInManager.
                    // Redirecciona al usuario a la página de inicio o al returnUrl.
                    return RedirectToLocal(returnUrl);
                }
            }
            else
            {
                ViewBag.Message = $"Intento de inicio de sesión no válido para el usuario con correo {model.Email}.";
                return View(model);
            }
        }

        // POST: Authentication/Logout
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Logout()
        {
            _userService.SignOut();
            return RedirectToAction("Index", "Home");
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }
    }
}
