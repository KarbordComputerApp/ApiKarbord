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

            public string AModeCode { get; set; }

            public string AccCode { get; set; }

            public int Level { get; set; }

            public int Sath { get; set; }

        }

        // Post: api/ReportAcc/TrzAcc گزارش تراز دفاتر
        // HE_Report_TrzAcc
        [Route("api/ReportAcc/TrzAcc/{ace}/{sal}/{group}")]
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PostWeb_TrzAcc(string ace, string sal, string group, TrzAccObject TrzAccObject)
        {
            if (UnitDatabase.CreateConection(ace, sal, group))
            {
                string oprCode = UnitPublic.SpiltCodeCama(TrzAccObject.OprCode);
                string mkzCode = UnitPublic.SpiltCodeCama(TrzAccObject.MkzCode);
                string aModeCode = UnitPublic.SpiltCodeCama(TrzAccObject.AModeCode);

                string sql = string.Format(CultureInfo.InvariantCulture,
                          @"select * FROM  dbo.Web_TrzAcc('{0}', '{1}','{2}','{3}','{4}') AS TrzAcc where 1 = 1 ",
                          TrzAccObject.azTarikh,
                          TrzAccObject.taTarikh,
                          oprCode,
                          mkzCode,
                          aModeCode);

                if (TrzAccObject.Sath == 1)
                    sql += string.Format(" and (Level = {0})", TrzAccObject.Level);
                else
                    sql += string.Format(" and (Level <= {0})", TrzAccObject.Level);

                sql += UnitPublic.SpiltCodeAnd("AccCode", TrzAccObject.AccCode);

                sql += " order by AccCode1,AccCode2,AccCode3,AccCode4,AccCode5";

                var listTrzAcc = UnitDatabase.db.Database.SqlQuery<Web_TrzAcc>(sql);
                return Ok(listTrzAcc);
            }
            return null;
        }



        public class DftrObject
        {
            public string azTarikh { get; set; }

            public string taTarikh { get; set; }

            public string azShomarh { get; set; }

            public string taShomarh { get; set; }

            public string DispBands { get; set; }

            public string JamRooz { get; set; }

            public string AccCode { get; set; }

            public string StatusCode { get; set; }

            public string AModeCode { get; set; }

            public string OprCode { get; set; }

            public string MkzCode { get; set; }

        }

        // Post: api/ReportAcc/Web_Dftr گزارش دفتر حساب
        // HE_Report_Dftr
        [Route("api/ReportAcc/Dftr/{ace}/{sal}/{group}")]
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PostWeb_Dftr(string ace, string sal, string group, DftrObject DftrObject)
        {
            if (UnitDatabase.CreateConection(ace, sal, group))
            {
                string status = UnitPublic.SpiltCodeCama(DftrObject.StatusCode);
                string oprCode = UnitPublic.SpiltCodeCama(DftrObject.OprCode);
                string mkzCode = UnitPublic.SpiltCodeCama(DftrObject.MkzCode);
                string aModeCode = UnitPublic.SpiltCodeCama(DftrObject.AModeCode);

                string sql = string.Format(CultureInfo.InvariantCulture,
                          @"select * FROM  Web_Dftr('{0}','{1}','{2}') AS Dftr where 1 = 1 ",
                          DftrObject.AccCode,
                          DftrObject.DispBands,
                          DftrObject.JamRooz);

                if (DftrObject.azTarikh != "")
                    sql += string.Format(" and DocDate >= '{0}' ", DftrObject.azTarikh);

                if (DftrObject.taTarikh != "")
                    sql += string.Format(" and DocDate <= '{0}' ", DftrObject.taTarikh);



                if (DftrObject.azShomarh != "")
                    sql += string.Format(" and DocNo >= '{0}' ", DftrObject.azShomarh);

                if (DftrObject.taShomarh != "")
                    sql += string.Format(" and DocNo <= '{0}' ", DftrObject.taShomarh);

                sql += UnitPublic.SpiltCodeAnd("Status", DftrObject.StatusCode);
                sql += UnitPublic.SpiltCodeAnd("ModeCode", DftrObject.AModeCode);
                sql += UnitPublic.SpiltCodeAnd("OprCode", DftrObject.OprCode);
                sql += UnitPublic.SpiltCodeAnd("MkzCode", DftrObject.MkzCode);

                // sql += " order by AccCode1,AccCode2,AccCode3,AccCode4,AccCode5";

                var listDftr = UnitDatabase.db.Database.SqlQuery<Web_Dftr>(sql);
                return Ok(listDftr);
            }
            return null;
        }




        public class ADocRObject
        {
            public string azTarikh { get; set; }

            public string taTarikh { get; set; }

            public string azShomarh { get; set; }

            public string taShomarh { get; set; }

            public string DispBands { get; set; }

            public string JamRooz { get; set; }

            public string AccCode { get; set; }

            public string StatusCode { get; set; }

            public string AModeCode { get; set; }

            public string OprCode { get; set; }

            public string MkzCode { get; set; }

        }

        // Post: api/ReportAcc/Web_ADocR گزارش دفتر حساب
        // HE_Report_ADocR
        [Route("api/ReportAcc/ADocR/{ace}/{sal}/{group}")]
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PostWeb_ADocR(string ace, string sal, string group, ADocRObject ADocRObject)
        {
            if (UnitDatabase.CreateConection(ace, sal, group))
            {
                string status = UnitPublic.SpiltCodeCama(ADocRObject.StatusCode);
                string oprCode = UnitPublic.SpiltCodeCama(ADocRObject.OprCode);
                string mkzCode = UnitPublic.SpiltCodeCama(ADocRObject.MkzCode);
                string aModeCode = UnitPublic.SpiltCodeCama(ADocRObject.AModeCode);
                string AccCode = UnitPublic.SpiltCodeCama(ADocRObject.AccCode);

                string sql = string.Format(CultureInfo.InvariantCulture,
                          @"select * FROM  Web_ADocR({0},{1}) AS ADocR where 1 = 1 ",
                          ADocRObject.DispBands,
                          ADocRObject.JamRooz);

                if (ADocRObject.azTarikh != "")
                    sql += string.Format(" and DocDate >= '{0}' ", ADocRObject.azTarikh);

                if (ADocRObject.taTarikh != "")
                    sql += string.Format(" and DocDate <= '{0}' ", ADocRObject.taTarikh);



                if (ADocRObject.azShomarh != "")
                    sql += string.Format(" and DocNo >= '{0}' ", ADocRObject.azShomarh);

                if (ADocRObject.taShomarh != "")
                    sql += string.Format(" and DocNo <= '{0}' ", ADocRObject.taShomarh);

                sql += UnitPublic.SpiltCodeAnd("Status", ADocRObject.StatusCode);
                sql += UnitPublic.SpiltCodeAnd("ModeCode", ADocRObject.AModeCode);
                sql += UnitPublic.SpiltCodeAnd("OprCode", ADocRObject.OprCode);
                sql += UnitPublic.SpiltCodeAnd("MkzCode", ADocRObject.MkzCode);

                sql += UnitPublic.SpiltCodeLike("AccCode", ADocRObject.AccCode);

                sql += " order by DocNo,BandNo";

                var listADocR = UnitDatabase.db.Database.SqlQuery<Web_ADocR>(sql);
                return Ok(listADocR);
            }
            return null;
        }

    }
}
