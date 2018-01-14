using GameStore.Models;

namespace OwinAuth.Identity
{
    public class IdentityDbInitializer
        : System.Data.Entity.DropCreateDatabaseIfModelChanges<ApplicationIdentityDbContext>
    {
        protected override void Seed(ApplicationIdentityDbContext context) {
            PerformInitialSetup(context);
            base.Seed(context);
        }

        private void PerformInitialSetup(ApplicationIdentityDbContext context) {
        }
    }
}
