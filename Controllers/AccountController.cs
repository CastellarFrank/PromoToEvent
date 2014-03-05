using System.Web.Mvc;
using PromoToEvents.Logic.Session;
using PromoToEvents.Models;

namespace PromoToEvents.Controllers
{
    public class AccountController : Controller
    {

        private readonly ISessionManagement _session = SessionLayer.Instance;
        //
        // GET: /Account/
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel model, string returnUrl)
        {
            if (_session.LogIn(model.UserEmail, model.Password, model.RememberMe))
            {
                return RedirectToLocal(returnUrl);
            }

            
            ModelState.AddModelError("", "The user or the password isn't correct.");
            return View(model);
        }

        public ActionResult Logout(string returnUrl)
        {
            _session.LogOut();

            return RedirectToAction("Index", "Home");
        }

        // POST: /Account/Logout
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Logout()
        {
            _session.LogOut();

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
