using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using RestSys.Models;
using RestSys.Models.Exports;
namespace RestSys.Controllers
{
    public class StylesController : ApiController
    {
        //RSDbContext db = new RSDbContext();

        //// GET api/<controller>
        //public IEnumerable<RSStyle> Get()
        //{
        //    return db.Styles.ToList();
        //}

        //// GET api/<controller>/5
        //public RSStyle Get(int id)
        //{
        //    return db.Styles.Find(id);
        //}

        //// POST api/<controller>
        //public int Post([FromBody]RSStyle value)
        //{
        //    if (value != null)
        //    {
        //        value = db.Styles.Add(value);
        //        db.SaveChanges();
        //        return value.Id;
        //    }
        //    return -1;
        //}

        //// PUT api/<controller>/5
        //public void Put(int id, [FromBody]RSStyle value)
        //{
        //    if (value != null)
        //    {
        //        RSStyle existing = db.Styles.Find(value.Id);
        //        if (existing != null)
        //        {
        //            value.SyncPropertiesTo(existing);
        //            db.SaveChanges();
        //        }
        //    }
        //}

        //// DELETE api/<controller>/5
        //public void Delete(int id)
        //{
        //    RSStyle victim = db.Styles.Find(id);
        //    if (victim != null)
        //        db.Styles.Remove(victim);

        //    db.SaveChanges();
        //}
    }
}