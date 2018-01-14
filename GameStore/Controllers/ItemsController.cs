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
    public class ItemsController : ApiController
    {
        private ApplicationIdentityDbContext db = new ApplicationIdentityDbContext();

        // GET: api/Items
        public JsonResult<DbSet<Item>> GetItems()
        {
            return Json(db.Items);
        }

        // GET: api/Items/5
        [ResponseType(typeof(Item))]
        public async Task<JsonResult<Item>> GetItem(int id)
        {
            Item item = await db.Items.FindAsync(id);
            if (item == null)
            {
                return Json(new Item() { Id = 0, Name="НЕТ ТАКОЙ ИГРЫ" });
            }

            return Json(item);
        }

        // PUT: api/Items/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutItem(int id, Item item)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != item.Id)
            {
                return BadRequest();
            }

            db.Entry(item).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ItemExists(id))
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

        // POST: api/Items
        [ResponseType(typeof(Item))]
        public async Task<RedirectResult> PostItem(Item item)
        {
            if (!ModelState.IsValid)
            {
               
                return Redirect(new Uri("/", UriKind.Relative));
            }

            db.Items.Add(item);
            await db.SaveChangesAsync();

            return Redirect(new Uri("/", UriKind.Relative));  
        }

        // DELETE: api/Items/5
        [ResponseType(typeof(Item))]
        public async Task<IHttpActionResult> DeleteItem(int id)
        {
            Item item = await db.Items.FindAsync(id);
            if (item == null)
            {
                return NotFound();
            }

            db.Items.Remove(item);
            await db.SaveChangesAsync();

            return Ok(item);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ItemExists(int id)
        {
            return db.Items.Count(e => e.Id == id) > 0;
        }
    }
}