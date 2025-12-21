using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Http.Results;
using ApiKarbord.Controllers.Unit;
using ApiKarbord.Models;
using Newtonsoft.Json;



namespace ApiKarbord.Controllers.AFI.data
{
    public class AFI_ADocNotDataController : ApiController
    {

        // GET: api/ADocNotData/ANotH اطلاعات سند    
        [Route("api/ADocNotData/ANotH/{ace}/{sal}/{group}/{serialNumber}")]
        public async Task<IHttpActionResult> GetWeb_ANotH(string ace, string sal, string group, long serialNumber)
        {
            string dBName = UnitDatabase.DatabaseName(ace, sal, group);
            var dataAccount = UnitDatabase.ReadUserPassHeader(this.Request.Headers);
            string sql = String.Format("SELECT * FROM {0}.dbo.Web_ANoteH where 1 = 1 {1} ", dBName, (serialNumber > 0 ? (" and SerialNumber = " + serialNumber) : "") );

            var DBase = UnitDatabase.dataDB.Where(p => p.UserName.ToUpper() == dataAccount[0].ToUpper() && p.Password == dataAccount[1]).Single();
            string res = UnitDatabase.TestAcount(DBase, dataAccount[3], ace, group, UnitPublic.access_View);
            if (res == "")
            {
                return Ok(DBase.DB.Database.SqlQuery<Web_ANotH>(sql));
            }
            else
                return Ok(res);
        }
    }

}
