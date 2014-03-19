using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using RestSys.Models;

namespace RestSys.Controllers
{
    public class StylesController : ApiController
    {
        RSDbContext db = new RSDbContext();
        
        // GET api/<controller>
        public IEnumerable<RSStyle> Get()
        {
            return db.Styles.ToList();
        }

        // GET api/<controller>/5
        public RSStyle Get(int id)
        {
            return db.Styles.Find(id);
        }

        // POST api/<controller>
        public void Post([FromBody]RSStyle value)
        {
            db.Styles.Add(value);
            db.SaveChanges();
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]RSStyle value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
            RSStyle victim = db.Styles.Find(id);
            if (victim != null)
                db.Styles.Remove(victim);

            db.SaveChanges();
        }
    }
}