using Owin;
using GameStore.Models;

namespace OwinAuth.Identity
{
    /// <summary>
    /// This application startup class will be called to configure the OWIN middleware pipeline
    /// </summary>
    /// <remarks>
    /// One would normally put this class in App_Start folder by convention, but I have placed it in the  
    /// Identity folder for clarity in teaching purposes.
    /// </remarks>
    public class IdentityConfig
    {
        /// <summary>
        ///  This Configuration method will be called because the class name has been provided in the web.config file
        /// and the Microsoft.Owin.Host.SystemWeb assembly has been included in the project. An assembly level
        /// option in that assembly will cause this to be bootstrapped by the OwinHttpModule. The name and parameter
        /// list are by convention.
        /// </summary>
        /// <param name="app">The instance of the OWIN AppBuilder class used to configure the middleware pipeline</param>
        public void Configuration(Owin.IAppBuilder app) {
            //Extension methods allow us to add middleware items to the pipeline, sometimes with lifecycle hints
            //note that we are providing the factory methods to be called
            //whenever the builder needs to build this middleware
            app.CreatePerOwinContext<ApplicationIdentityDbContext>(ApplicationIdentityDbContext.Create);
            app.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create);
            app.UseCookieAuthentication(new Microsoft.Owin.Security.Cookies.CookieAuthenticationOptions {
                AuthenticationType = Microsoft.Owin.Security.Cookies.CookieAuthenticationDefaults.AuthenticationType
                                                                                                            ,
                LoginPath = Microsoft.Owin.Security.Cookies.CookieAuthenticationDefaults.LoginPath
            });
        }
    }
}