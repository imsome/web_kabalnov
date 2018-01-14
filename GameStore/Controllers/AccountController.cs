using System;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.Owin;
using GameStore.Models;

namespace GameStore.Controllers
{
    public class AccountController : Controller
    {
        public ActionResult AchieveRegistration()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Login() {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Login(LoginViewModel creds, string returnUrl) {
            if (ModelState.IsValid) {
                //The OwinContext will get the user manager we defined in our IdentityConfig startup class
                var userMgr = HttpContext.GetOwinContext().GetUserManager<OwinAuth.Identity.ApplicationUserManager>();
                //By first finding the user we can easily create the identity below knowing the user exists. 
                //Alternatively you can create a set of claims and generate your identity from those value. 
                //Find uses the UserStore - EF in our case. You can find by email or name, but those
                //operations will leave you to then also validate the password against the hash.
                var user = await userMgr.FindAsync(creds.Email, creds.Password);
                if (user == null)
                    ModelState.AddModelError(String.Empty, "User not found, or credentials invalid.");
                else {
                    //The IIdentity implementation created by our user manager instance
                    //Note that the authentication type here must match the one setup on OWIN middleware
                    //in our IdentityConfig 'UseCookieAuthentication' call
                    var identity = await userMgr.CreateIdentityAsync(user
                        , Microsoft.Owin.Security.Cookies.CookieAuthenticationDefaults.AuthenticationType);
                    //just being cautious to clear out existing credentials
                    HttpContext.GetOwinContext().Authentication.SignOut();
                    HttpContext.GetOwinContext().Authentication.SignIn(
                        new Microsoft.Owin.Security.AuthenticationProperties { IsPersistent = creds.RememberMe }
                        , identity);
                    //Do not simply redirect wherever you are told - make sure it is local.
                    return Redirect(Url.IsLocalUrl(returnUrl) ? returnUrl : "/");
                }
            }
            return View();
        }

        public ActionResult Logout() {
            HttpContext.GetOwinContext().Authentication.SignOut();
            return View();
        }

        [HttpGet]
        public ActionResult Register() {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Register(RegisterViewModel registration) {
            if (ModelState.IsValid) {
                //The OwinContext will get the user manager we defined in our IdentityConfig startup class
                var userMgr = HttpContext.GetOwinContext().GetUserManager<OwinAuth.Identity.ApplicationUserManager>();
                //This is an instance of our own ApplicationUser, which is just a thin wrapper on the EF code-first class
                var user = new GameStore.Models.ApplicationUser { UserName = registration.Email, Email = registration.Email };
                //Recall that the manager uses the store, which in our case is configured to be an EF context.
                //If your database doesn't exist yet, it will be created here which could cause lag during this step.
                var identityResult = await userMgr.CreateAsync(user, registration.Password);
               
                var userReg = await userMgr.FindAsync(registration.Email, registration.Password);
                if (identityResult.Succeeded) {
                   
                    //We could be nice and login the newly created user - here we do not.//BUT WE OWNed YA, BITHES!!!!!

                    //Note that the authentication type here must match the one setup on OWIN middleware
                    //in our IdentityConfig 'UseCookieAuthentication' call
                    var identity = await userMgr.CreateIdentityAsync(userReg
                        , Microsoft.Owin.Security.Cookies.CookieAuthenticationDefaults.AuthenticationType);
                    //just being cautious to clear out existing credentials
                    HttpContext.GetOwinContext().Authentication.SignOut();
                    HttpContext.GetOwinContext().Authentication.SignIn(
                        new Microsoft.Owin.Security.AuthenticationProperties { IsPersistent = true }
                        , identity);
                    //Do not simply redirect wherever you are told - make sure it is local.
                    

                    return RedirectToAction("AchieveRegistration", "Account");
                }
                else {
                    foreach (var error in identityResult.Errors)
                        ModelState.AddModelError(String.Empty, error);
                }

            }
            return View();
        }
    }
}