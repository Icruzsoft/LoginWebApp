using System.Web.Mvc;
using LoginWebApp.Models;
using LoginWebApp.Services;
using LoginWebApp.Service; // Asegúrate de incluir el espacio de nombres correcto para UtilityService

namespace LoginWebApp.Controllers
{
    public class PasswordController : Controller
    {
        private readonly UserService _userService;
        private readonly EmailService _emailService;

        public PasswordController()
        {
            _userService = new UserService();
            _emailService = new EmailService();
        }

        // GET: Password/ForgotPassword
        [AllowAnonymous]
        public ActionResult ForgotPassword()
        {
            return View();
        }

        // POST: Password/ForgotPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ForgotPassword(ForgotPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = _userService.FindByEmail(model.Email);
            if (user != null)
            {
                string token = UtilityService.GenerarToken();
                string resetUrl = Url.Action("ResetPassword", "Password", new { token = token }, protocol: Request.Url.Scheme);
                _emailService.Enviar(new EmailSend
                {
                    EmailTo = user.Email,
                    Subject = "Restablecer Contraseña",
                    Report = $"Para restablecer su contraseña, haga clic <a href='{resetUrl}'>aquí</a>."
                });

                return RedirectToAction("ForgotPasswordConfirmation");
            }

            ModelState.AddModelError("", "No se encontró una cuenta con ese correo electrónico.");
            return View(model);
        }

        // GET: Password/ForgotPasswordConfirmation
        [AllowAnonymous]
        public ActionResult ForgotPasswordConfirmation()
        {
            return View();
        }

        // GET: Password/ResetPassword
        [AllowAnonymous]
        public ActionResult ResetPassword(string token)
        {
            var user = _userService.FindByToken(token);
            if (user != null)
            {
                return View(new ResetPasswordViewModel { Token = token });
            }

            return View("Error");
        }

        // POST: Password/ResetPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ResetPassword(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = _userService.FindByToken(model.Token);
            if (user != null)
            {
                user.Password = UtilityService.ConvertirSHA256(model.Password);
                _userService.UpdateUser(user);

                return RedirectToAction("ResetPasswordConfirmation");
            }

            ModelState.AddModelError("", "No se pudo restablecer la contraseña.");
            return View(model);
        }

        // GET: Password/ResetPasswordConfirmation
        [AllowAnonymous]
        public ActionResult ResetPasswordConfirmation()
        {
            return View();
        }

        // Aquí puedes añadir más métodos si son necesarios, como ChangePassword.
    }
}
