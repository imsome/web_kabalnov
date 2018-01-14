using Microsoft.AspNet.Identity.Owin;
using GameStore.Models;

namespace OwinAuth.Identity
{
    /// <summary>
    /// The UserManager provides for manipulation of all user instances.
    /// </summary>
    /// <remarks>
    /// In this case we are wrapping the provided UserManager and providing a way for it to be
    /// initialized with an EF backed store. The store provides the lower level mechanism
    /// by which operations on the manager are carried out.
    /// </remarks>
    public class ApplicationUserManager
        : Microsoft.AspNet.Identity.UserManager<ApplicationUser>
    {
        public ApplicationUserManager(Microsoft.AspNet.Identity.IUserStore<ApplicationUser> store)
            : base(store) { }

        public static ApplicationUserManager Create(
            Microsoft.AspNet.Identity.Owin.IdentityFactoryOptions<ApplicationUserManager> options
            , Microsoft.Owin.IOwinContext context) {
            //Utilizes an extension method on OwinContext provided by Microsoft.AspNet.Identity.Owin, otherwise
            // a key is needed.
            var db = context.Get<ApplicationIdentityDbContext>();
            //The user manager instance works on a user store, which in this case comes from the EF specific implementation.
            var manager =
                new ApplicationUserManager(
                    new Microsoft.AspNet.Identity.EntityFramework.UserStore<ApplicationUser>(db));
            return manager;
        }
    }
}