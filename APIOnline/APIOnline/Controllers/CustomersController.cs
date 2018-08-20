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
using APIOnline.Models;

namespace APIOnline.Controllers
{
    public class CustomersController : ApiController
    {
        private JVKCRMEntities db = new JVKCRMEntities();

        // GET: api/Customers
        public IQueryable<tblCustomer> GettblCustomers()
        {
            return db.tblCustomers;
        }

        // GET: api/Customers/5
        [ResponseType(typeof(tblCustomer))]
        public IHttpActionResult GettblCustomer(string id)
        {
            tblCustomer tblCustomer = db.tblCustomers.Find(id);
            if (tblCustomer == null)
            {
                return NotFound();
            }

            return Ok(tblCustomer);
        }

        // PUT: api/Customers/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PuttblCustomer(string id, tblCustomer tblCustomer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tblCustomer.CusId)
            {
                return BadRequest();
            }

            db.Entry(tblCustomer).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!tblCustomerExists(id))
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

        // POST: api/Customers
        [ResponseType(typeof(tblCustomer))]
        public IHttpActionResult PosttblCustomer(tblCustomer tblCustomer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.tblCustomers.Add(tblCustomer);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (tblCustomerExists(tblCustomer.CusId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = tblCustomer.CusId }, tblCustomer);
        }

        // DELETE: api/Customers/5
        [ResponseType(typeof(tblCustomer))]
        public IHttpActionResult DeletetblCustomer(string id)
        {
            tblCustomer tblCustomer = db.tblCustomers.Find(id);
            if (tblCustomer == null)
            {
                return NotFound();
            }

            db.tblCustomers.Remove(tblCustomer);
            db.SaveChanges();

            return Ok(tblCustomer);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool tblCustomerExists(string id)
        {
            return db.tblCustomers.Count(e => e.CusId == id) > 0;
        }
    }
}