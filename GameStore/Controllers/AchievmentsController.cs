using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using GameStore.Models;
using System.Web.Http.Results;

namespace GameStore.Controllers
{
    public class AchievmentsController : ApiController
    {
        private ApplicationIdentityDbContext db = new ApplicationIdentityDbContext();

        // GET: api/Achievments
        public IQueryable<Achievments> GetAchievments()
        {
            return db.Achievments;
        }

        // GET: api/Achievments/5
        [ResponseType(typeof(Achievments))]
        public async Task<IHttpActionResult> GetAchievments(string UserName)
        {
            
            Achievments achievments = await db.Achievments.FindAsync(UserName);
            if (achievments == null)
            {
                return NotFound();
            }

            return Ok(achievments);
        }

        // PUT: api/Achievments/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutAchievments(int id, Achievments achievments)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != achievments.Id)
            {
                return BadRequest();
            }

            db.Entry(achievments).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AchievmentsExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Achievments
        [ResponseType(typeof(Achievments))]
        public async Task<RedirectResult> PostAchievments(Achievments achievments)
        {
            if (!ModelState.IsValid)
            {
                return Redirect(new Uri("/Home/Index", UriKind.Relative));
            }

            db.Achievments.Add(achievments);
            await db.SaveChangesAsync();

            return Redirect(new Uri("/Home/Index", UriKind.Relative));
        }

        // DELETE: api/Achievments/5
        [ResponseType(typeof(Achievments))]
        public async Task<IHttpActionResult> DeleteAchievments(int id)
        {
            Achievments achievments = await db.Achievments.FindAsync(id);
            if (achievments == null)
            {
                return NotFound();
            }

            db.Achievments.Remove(achievments);
            await db.SaveChangesAsync();

            return Ok(achievments);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AchievmentsExists(int id)
        {
            return db.Achievments.Count(e => e.Id == id) > 0;
        }
    }
}