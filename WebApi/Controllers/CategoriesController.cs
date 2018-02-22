using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using WebApi.Models;

namespace WebApi.Controllers
{
    public class CategoriesController : ApiController
    {
        private NorthwindEntities db = new NorthwindEntities();

        // GET: api/Categories
        public IQueryable<CategoriesDTO> GetCategories()
        {
            return db.Categories.Select(c => new CategoriesDTO
            {
                CategoryID = c.CategoryID,
                CategoryName = c.CategoryName,
                Description = c.Description
            });
        }

        // GET: api/Categories/5
        [ResponseType(typeof(CategoriesDTO))]
        public IHttpActionResult GetCategories(int id)
        {
            CategoriesDTO categories = db.Categories.Where(c => c.CategoryID == id).Select(c => new CategoriesDTO {
                CategoryID = c.CategoryID,
                CategoryName = c.CategoryName,
                Description = c.Description
            }).Single();
            if (categories == null)
            {
                return NotFound();
            }

            return Ok(categories);
        }

        // PUT: api/Categories/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCategories(int id, Categories categories)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != categories.CategoryID)
            {
                return BadRequest();
            }

            db.Entry(categories).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CategoriesExists(id))
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

        // POST: api/Categories
        [ResponseType(typeof(Categories))]
        public IHttpActionResult PostCategories(Categories categories)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Categories.Add(categories);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = categories.CategoryID }, categories);
        }

        // DELETE: api/Categories/5
        [ResponseType(typeof(Categories))]
        public IHttpActionResult DeleteCategories(int id)
        {
            Categories categories = db.Categories.Find(id);
            if (categories == null)
            {
                return NotFound();
            }

            db.Categories.Remove(categories);
            db.SaveChanges();

            return Ok(categories);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CategoriesExists(int id)
        {
            return db.Categories.Count(e => e.CategoryID == id) > 0;
        }
    }
}