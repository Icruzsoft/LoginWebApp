using System;
using System.Web.Mvc;
using LoginWebApp.Models;
using LoginWebApp.Services;

namespace LoginWebApp.Controllers
{
    public class RegisterController : Controller
    {
        private readonly UserService _userService; // Asume que UserService gestiona la lógica de usuarios.
        private readonly EmailService _emailService; // Asume que EmailService gestiona el envío de correos.

        public RegisterController()
        {
            _userService = new UserService();
            _emailService = new EmailService(); // Inicializa tu servicio de correo.
        }

        // GET: Register
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        // POST: Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterViewModel model, string userType)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // Aquí puedes añadir la lógica para registrar al usuario.
            // userType puede ser "Cliente" o "Experto".
            // Asegúrate de que los modelos de cliente y experto estén correctamente definidos.
            var result = userType == "Cliente"
                         ? _userService.RegisterClient(model)
                         : _userService.RegisterExpert(model);

            if (result.Success)
            {
                // Enviar correo de confirmación.
                string confirmationUrl = Url.Action("ConfirmEmail", "Register", new { email = model.Email }, protocol: Request.Url.Scheme);
                _emailService.SendConfirmationEmail(model.Email, confirmationUrl);

                // Redirigir a una página de éxito o a la página de inicio de sesión.
                return RedirectToAction("RegistrationSuccess", "Register");
            }
            else
            {
                // Añadir mensaje de error al ModelState.
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error);
                }
            }

            return View(model);
        }

        // GET: ConfirmEmail
        [AllowAnonymous]
        public ActionResult ConfirmEmail(string email)
        {
            // Verificar el email y confirmar la cuenta del usuario.
            var result = _userService.ConfirmEmail(email);

            if (result.Success)
            {
                ViewBag.Message = "Correo electrónico confirmado. Ahora puede iniciar sesión.";
            }
            else
            {
                ViewBag.Message = "Error al confirmar el correo electrónico.";
            }

            return View();
        }

        // GET: RegistrationSuccess
        public ActionResult RegistrationSuccess()
        {
            return View();
        }
    }
}
