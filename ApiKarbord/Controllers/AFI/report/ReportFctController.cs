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
    public class ReportFctController : ApiController
    {

        public class FDocRObject
        {
            public string azTarikh { get; set; }

            public string taTarikh { get; set; }

            public string ModeCode1 { get; set; }

            public string ModeCode2 { get; set; }

            public string InvCode { get; set; }

            public string KGruCode { get; set; }

            public string KalaCode { get; set; }

            public string CustCode { get; set; }

            public string MkzCode { get; set; }

            public string OprCode { get; set; }

        }

        // Post: api/ReportFct/FDocR گزارش ريز گردش اسناد خرید و فروش
        // HE_Report_FDocR
        [Route("api/ReportFct/FDocR/{ace}/{sal}/{group}")]
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PostWeb_FDocR(string ace, string sal, string group, FDocRObject FDocRObject)
        {
            if (UnitDatabase.CreateConection(ace, sal, group))
            {

                string sql = string.Format(CultureInfo.InvariantCulture,
                          @"select * FROM  dbo.Web_FDocR('{0}', '{1}') AS FDocR where 1 = 1 ",
                          FDocRObject.ModeCode1, FDocRObject.ModeCode2);

                sql += UnitPublic.SpiltCodeAnd("InvCode", FDocRObject.InvCode);
                sql += UnitPublic.SpiltCodeAnd("KGruCode", FDocRObject.KGruCode);
                sql += UnitPublic.SpiltCodeAnd("KalaCode", FDocRObject.KalaCode);
                sql += UnitPublic.SpiltCodeAnd("CustCode", FDocRObject.CustCode);
                sql += UnitPublic.SpiltCodeAnd("OprCode", FDocRObject.OprCode);
                sql += UnitPublic.SpiltCodeAnd("MkzCode", FDocRObject.MkzCode);

                if (FDocRObject.azTarikh != "")
                    sql += string.Format(" and DocDate >= '{0}' ", FDocRObject.azTarikh);

                if (FDocRObject.taTarikh != "")
                    sql += string.Format(" and DocDate <= '{0}' ", FDocRObject.taTarikh);

                var listFDocR = UnitDatabase.db.Database.SqlQuery<Web_FDocR>(sql);
                return Ok(listFDocR);
            }
            return null;
        }
    }
}
