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
using ProvisioAPI.DAL;
using ProvisioAPI.Models;

namespace ProvisioAPI.Controllers
{
    public class CustomerAccountDTOesController : ApiController
    {
        private PROVISIODBContext db = new PROVISIODBContext();

        // GET: api/CustomerAccountDTOes
        public IQueryable<CustomerAccountDTO> GetCustomerAccountDTOes()
        {
            return db.CustomerAccountDTOes;
        }

        // GET: api/CustomerAccountDTOes/5
        [ResponseType(typeof(CustomerAccountDTO))]
        public async Task<IHttpActionResult> GetCustomerAccountDTO(int id)
        {
            CustomerAccountDTO customerAccountDTO = await db.CustomerAccountDTOes.FindAsync(id);
            if (customerAccountDTO == null)
            {
                return NotFound();
            }

            return Ok(customerAccountDTO);
        }

        // PUT: api/CustomerAccountDTOes/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutCustomerAccountDTO(int id, CustomerAccountDTO customerAccountDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != customerAccountDTO.Id)
            {
                return BadRequest();
            }

            db.Entry(customerAccountDTO).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomerAccountDTOExists(id))
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

        // POST: api/CustomerAccountDTOes
        [ResponseType(typeof(CustomerAccountDTO))]
        public async Task<IHttpActionResult> PostCustomerAccountDTO(CustomerAccountDTO customerAccountDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.CustomerAccountDTOes.Add(customerAccountDTO);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = customerAccountDTO.Id }, customerAccountDTO);
        }

        // DELETE: api/CustomerAccountDTOes/5
        [ResponseType(typeof(CustomerAccountDTO))]
        public async Task<IHttpActionResult> DeleteCustomerAccountDTO(int id)
        {
            CustomerAccountDTO customerAccountDTO = await db.CustomerAccountDTOes.FindAsync(id);
            if (customerAccountDTO == null)
            {
                return NotFound();
            }

            db.CustomerAccountDTOes.Remove(customerAccountDTO);
            await db.SaveChangesAsync();

            return Ok(customerAccountDTO);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CustomerAccountDTOExists(int id)
        {
            return db.CustomerAccountDTOes.Count(e => e.Id == id) > 0;
        }
    }
}