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

namespace ApiKarbord.Controllers.AFI.report
{
    public class ReportAccController : ApiController
    {


        public class TrzAccObject
        {
            public string azTarikh { get; set; }

            public string taTarikh { get; set; }

            public string OprCode { get; set; }

            public string MkzCode { get; set; }

            public string ModeCode { get; set; }

            public string Acc_Code { get; set; }

            public int Level { get; set; }

        }

        // Post: api/Web_Data/TrzAcc گزارش تراز دفاتر
        // HE_Report_TrzAcc
        [Route("api/ReportAcc/TrzAcc/{ace}/{sal}/{group}")]
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PostWeb_TrzAcc(string ace, string sal, string group, TrzAccObject TrzAccObject)
        {
            if (UnitDatabase.CreateConection(ace, sal, group))
            {
                string sql = string.Format(CultureInfo.InvariantCulture,
                          @"select * FROM  dbo.Web_TrzAcc('{0}', '{1}','{2}','{3}','{4}') AS TrzAcc where 1 = 1 ",
                          TrzAccObject.azTarikh,
                          TrzAccObject.taTarikh,
                          TrzAccObject.OprCode,
                          TrzAccObject.MkzCode,
                          TrzAccObject.ModeCode);

                if (TrzAccObject.Level > 0)
                    sql += string.Format(" and (Level = {0})", TrzAccObject.Level);

                sql += UnitPublic.SpiltCode("Acc_Code", TrzAccObject.Acc_Code);

                sql += " order by Acc_Code ";

                var listTrzAcc = UnitDatabase.db.Database.SqlQuery<Web_TrzAcc>(sql);
                return Ok(listTrzAcc);
            }
            return null;
        }

    }
}
