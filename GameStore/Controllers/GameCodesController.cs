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
    public class GameCodesController : ApiController
    {
        private ApplicationIdentityDbContext db = new ApplicationIdentityDbContext();

        // GET: api/GameCodes
        public IQueryable<GameCode> GetGameCodes()
        {
            return db.GameCodes;
        }

        // GET: api/GameCodes/5
        [ResponseType(typeof(GameCode))]
        public async Task<IHttpActionResult> GetGameCode(int id)
        {
            GameCode gameCode = await db.GameCodes.FindAsync(id);
            if (gameCode == null)
            {
                return NotFound();
            }

            return Ok(gameCode);
            //List<GameCode> codes = new List<GameCode>();
            //var codesList = db.GameCodes.ToList();

            //foreach(var code in codesList)
            //{
            //    if(code.GameId==gameId)
            //    {
            //        codes.Add(new GameCode() { Code = "asdasd" });
            //    }
            //}
            //return Json(codes);
        }

        // PUT: api/GameCodes/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutGameCode(int id, GameCode gameCode)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != gameCode.Id)
            {
                return BadRequest();
            }

            db.Entry(gameCode).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GameCodeExists(id))
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

        // POST: api/GameCodes
        [ResponseType(typeof(GameCode))]
        public async Task<RedirectResult> PostGameCode(GameCode gameCode)
        {
            if (!ModelState.IsValid)
            {
                return Redirect(new Uri("/AdminMenu/AddCodes", UriKind.Relative));
            }

            db.GameCodes.Add(gameCode);
            await db.SaveChangesAsync();


            return Redirect(new Uri("/AdminMenu/AddCodes", UriKind.Relative));
        }

        // DELETE: api/GameCodes/5
        [ResponseType(typeof(GameCode))]
        public async Task<IHttpActionResult> DeleteGameCode(int id)
        {
            GameCode gameCode = await db.GameCodes.FindAsync(id);
            if (gameCode == null)
            {
                return NotFound();
            }

            db.GameCodes.Remove(gameCode);
            await db.SaveChangesAsync();

            return Ok(gameCode);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool GameCodeExists(int id)
        {
            return db.GameCodes.Count(e => e.Id == id) > 0;
        }
    }
}