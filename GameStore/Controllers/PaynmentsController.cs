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
    public class PaynmentsController : ApiController
    {
        private ApplicationIdentityDbContext db = new ApplicationIdentityDbContext();

        // GET: api/Paynments
        public IQueryable<Paynment> GetPaynments()
        {
            return db.Paynments;
        }

        // GET: api/Paynments/5
        [ResponseType(typeof(Paynment))]
        public async Task<IHttpActionResult> GetPaynment(int id)
        {
            Paynment paynment = await db.Paynments.FindAsync(id);
            if (paynment == null)
            {
                return NotFound();
            }

            return Ok(paynment);
        }

        // PUT: api/Paynments/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutPaynment(int id, Paynment paynment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != paynment.Id)
            {
                return BadRequest();
            }

            db.Entry(paynment).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PaynmentExists(id))
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

        // POST: api/Paynments
        [ResponseType(typeof(Paynment))]
        public async Task<RedirectResult> PostPaynment(Paynment paynment)
        {
            if (!ModelState.IsValid)
            {
                return Redirect(new Uri("/ItemCard/Success", UriKind.Relative));
            }

            db.Paynments.Add(paynment);
            await db.SaveChangesAsync();

            return Redirect(new Uri("/ItemCard/Success", UriKind.Relative));
        }

        // DELETE: api/Paynments/5
        [ResponseType(typeof(Paynment))]
        public async Task<IHttpActionResult> DeletePaynment(int id)
        {
            Paynment paynment = await db.Paynments.FindAsync(id);
            if (paynment == null)
            {
                return NotFound();
            }

            db.Paynments.Remove(paynment);
            await db.SaveChangesAsync();

            return Ok(paynment);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PaynmentExists(int id)
        {
            return db.Paynments.Count(e => e.Id == id) > 0;
        }
    }
}