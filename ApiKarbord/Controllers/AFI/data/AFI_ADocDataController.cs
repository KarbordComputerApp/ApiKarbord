using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Http.Results;
using ApiKarbord.Controllers.Unit;
using ApiKarbord.Models;
using Newtonsoft.Json;


namespace ApiKarbord.Controllers.AFI.data
{
    public class AFI_ADocDataController : ApiController
    {
        // GET: api/ADocData/AMode اطلاعات نوع سند حسابداری   
        [Route("api/ADocData/AMode/{ace}/{sal}/{group}")]
        public async Task<IHttpActionResult> GetWeb_AMode(string ace, string sal, string group)
        {
            if (UnitDatabase.CreateConection(ace, sal, group))
            {
                string sql = "SELECT * FROM Web_AMode";
                var listAMode = UnitDatabase.db.Database.SqlQuery<Web_AMode>(sql);
                return Ok(listAMode);
            }
            return null;
        }
    }
}
