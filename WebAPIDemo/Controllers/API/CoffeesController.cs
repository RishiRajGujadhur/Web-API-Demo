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
using WebAPIDemo.Models;

namespace WebAPIDemo.Controllers.API
{
    public class CoffeesController : ApiController
    {
        private WebAPIDemoContext db = new WebAPIDemoContext();

        // GET: api/Coffees
        public IQueryable<Coffee> GetCoffees()
        {
            return db.Coffees.Include(a=> a.Company);
        }

        // GET: api/Coffees/5
        [ResponseType(typeof(Coffee))]
        public async Task<IHttpActionResult> GetCoffee(int id)
        {
            Coffee coffee = await db.Coffees.FindAsync(id);
            if (coffee == null)
            {
                return NotFound();
            }

            return Ok(coffee);
        }

        // PUT: api/Coffees/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutCoffee(int id, Coffee coffee)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != coffee.Id)
            {
                return BadRequest();
            }

            db.Entry(coffee).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CoffeeExists(id))
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

        // POST: api/Coffees
        [ResponseType(typeof(Coffee))]
        public async Task<IHttpActionResult> PostCoffee(Coffee coffee)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Coffees.Add(coffee);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = coffee.Id }, coffee);
        }

        // DELETE: api/Coffees/5
        [ResponseType(typeof(Coffee))]
        public async Task<IHttpActionResult> DeleteCoffee(int id)
        {
            Coffee coffee = await db.Coffees.FindAsync(id);
            if (coffee == null)
            {
                return NotFound();
            }

            db.Coffees.Remove(coffee);
            await db.SaveChangesAsync();

            return Ok(coffee);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CoffeeExists(int id)
        {
            return db.Coffees.Count(e => e.Id == id) > 0;
        }
    }
}