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

            public string StatusCode { get; set; }

        }

        // Post: api/ReportFct/FDocR گزارش ريز گردش اسناد خرید و فروش
        // HE_Report_FDocR
        [Route("api/ReportFct/FDocR/{ace}/{sal}/{group}")]
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PostWeb_FDocR(string ace, string sal, string group, FDocRObject FDocRObject)
        {
            string dBName = UnitDatabase.DatabaseName(ace, sal, group);
            var dataAccount = UnitDatabase.ReadUserPassHeader(this.Request.Headers);
            var DBase = UnitDatabase.dataDB.Where(p => p.UserName == dataAccount[0] && p.Password == dataAccount[1]).Single();
            string res = UnitDatabase.TestAcount(DBase, dataAccount[3], ace, group, UnitPublic.access_View);
            if (res == "")
            {
                string sql = string.Format(CultureInfo.InvariantCulture,
                          @"select top(10000)  * FROM  {0}.dbo..Web_FDocR('{1}', '{2}','{3}') AS FDocR where 1 = 1 ",
                          dBName, FDocRObject.ModeCode1, FDocRObject.ModeCode2, dataAccount[2]);

                sql += UnitPublic.SpiltCodeAnd("InvCode", FDocRObject.InvCode);
                sql += UnitPublic.SpiltCodeAnd("KGruCode", FDocRObject.KGruCode);
                sql += UnitPublic.SpiltCodeAnd("KalaCode", FDocRObject.KalaCode);
                sql += UnitPublic.SpiltCodeAnd("CustCode", FDocRObject.CustCode);
                sql += UnitPublic.SpiltCodeAnd("OprCode", FDocRObject.OprCode);
                sql += UnitPublic.SpiltCodeAnd("MkzCode", FDocRObject.MkzCode);
                sql += UnitPublic.SpiltCodeAnd("Status", FDocRObject.StatusCode);


                if (FDocRObject.azTarikh != "")
                    sql += string.Format(" and DocDate >= '{0}' ", FDocRObject.azTarikh);

                if (FDocRObject.taTarikh != "")
                    sql += string.Format(" and DocDate <= '{0}' ", FDocRObject.taTarikh);

                var listFDocR = DBase.DB.Database.SqlQuery<Web_FDocR>(sql);
                return Ok(listFDocR);
            }
            else
                return Ok(res);

            //string conStr = UnitDatabase.CreateConnectionString(dataAccount[0], dataAccount[1], dataAccount[2], dataAccount[3], ace, sal, group, 0, FDocRObject.ModeCode1.Substring(0, 1) == "S" ? UnitPublic.access_FDocR_S : UnitPublic.access_FDocR_P, UnitPublic.act_Report, 0);
        }


        public class TrzFKalaObject
        {
            public string azTarikh { get; set; }

            public string taTarikh { get; set; }

            public string azShomarh { get; set; }

            public string taShomarh { get; set; }

            public string ZeroValue { get; set; }

            public string ModeCode1 { get; set; }

            public string ModeCode2 { get; set; }

            public string InvCode { get; set; }

            public string KGruCode { get; set; }

            public string KalaCode { get; set; }

            public string CustCode { get; set; }

            public string CGruCode { get; set; }

            public string MkzCode { get; set; }

            public string OprCode { get; set; }

            public string StatusCode { get; set; }

            public string Top { get; set; }

            public string Sort { get; set; }

        }
        // Post: api/ReportFct/TrzFKala   گزارش تراز خرید و  فروش کالا
        // HE_Report_TrzFKala
        [Route("api/ReportFct/TrzFKala/{ace}/{sal}/{group}")]
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PostWeb_TrzFKala(string ace, string sal, string group, TrzFKalaObject TrzFKalaObject)
        {
            string dBName = UnitDatabase.DatabaseName(ace, sal, group);
            var dataAccount = UnitDatabase.ReadUserPassHeader(this.Request.Headers);
            var DBase = UnitDatabase.dataDB.Where(p => p.UserName == dataAccount[0] && p.Password == dataAccount[1]).Single();
            string res = UnitDatabase.TestAcount(DBase, dataAccount[3], ace, group, UnitPublic.access_View);
            if (res == "")
            {
                string oprCode = UnitPublic.SpiltCodeCama(TrzFKalaObject.OprCode);
                string mkzCode = UnitPublic.SpiltCodeCama(TrzFKalaObject.MkzCode);
                string custCode = UnitPublic.SpiltCodeCama(TrzFKalaObject.CustCode);
                string cGruCode = UnitPublic.SpiltCodeCama(TrzFKalaObject.CGruCode);
                string invCode = UnitPublic.SpiltCodeCama(TrzFKalaObject.InvCode);
                string statusCode = UnitPublic.SpiltCodeCama(TrzFKalaObject.StatusCode);

                string sql = string.Format(CultureInfo.InvariantCulture,
                          @"select  top ({14}) * FROM  {15}.dbo.Web_TrzFKala('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}', '{10}', '{11}',{12},'{13}') AS TrzFKala where 1 = 1 ",
                          TrzFKalaObject.ModeCode1,
                          TrzFKalaObject.ModeCode2,
                          TrzFKalaObject.azTarikh,
                          TrzFKalaObject.taTarikh,
                          TrzFKalaObject.azShomarh,
                          TrzFKalaObject.taShomarh,
                          custCode,
                          cGruCode,
                          mkzCode,
                          oprCode,
                          invCode,
                          statusCode,
                          TrzFKalaObject.ZeroValue,
                          dataAccount[2],
                          TrzFKalaObject.Top == null ? "10000" : TrzFKalaObject.Top,
                          dBName
                          );

                sql += UnitPublic.SpiltCodeAnd("KalaCode", TrzFKalaObject.KalaCode);
                sql += UnitPublic.SpiltCodeAnd("KGruCode", TrzFKalaObject.KGruCode);

                if (TrzFKalaObject.Sort != null)
                {
                    sql += " order by " + TrzFKalaObject.Sort;
                }

                var listTrzFKala = DBase.DB.Database.SqlQuery<Web_TrzFKala>(sql);
                return Ok(listTrzFKala);
            }
            else
                return Ok(res);

            //string conStr = UnitDatabase.CreateConnectionString(dataAccount[0], dataAccount[1], dataAccount[2], dataAccount[3], ace, sal, group, 0, TrzFKalaObject.ModeCode1.Substring(0, 1) == "S" ? UnitPublic.access_TrzFKala_S : UnitPublic.access_TrzFKala_P, UnitPublic.act_Report, 0);
        }




        public class TrzFCustObject
        {
            public string azTarikh { get; set; }

            public string taTarikh { get; set; }

            public string azShomarh { get; set; }

            public string taShomarh { get; set; }

            public string ZeroValue { get; set; }

            public string ModeCode1 { get; set; }

            public string ModeCode2 { get; set; }

            public string InvCode { get; set; }

            public string KGruCode { get; set; }

            public string KalaCode { get; set; }

            public string CustCode { get; set; }

            public string CGruCode { get; set; }

            public string MkzCode { get; set; }

            public string OprCode { get; set; }

            public string StatusCode { get; set; }

            public string Top { get; set; }

        }
        // Post: api/ReportFct/TrzFCust گزارش تراز خرید و  فروش مشتریان
        // HE_Report_TrzFCust
        [Route("api/ReportFct/TrzFCust/{ace}/{sal}/{group}")]
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PostWeb_TrzFCust(string ace, string sal, string group, TrzFCustObject TrzFCustObject)
        {
            string dBName = UnitDatabase.DatabaseName(ace, sal, group);
            var dataAccount = UnitDatabase.ReadUserPassHeader(this.Request.Headers);
            var DBase = UnitDatabase.dataDB.Where(p => p.UserName == dataAccount[0] && p.Password == dataAccount[1]).Single();
            string res = UnitDatabase.TestAcount(DBase, dataAccount[3], ace, group, UnitPublic.access_View);
            if (res == "")
            {
                string oprCode = UnitPublic.SpiltCodeCama(TrzFCustObject.OprCode);
                string mkzCode = UnitPublic.SpiltCodeCama(TrzFCustObject.MkzCode);
                string kalaCode = UnitPublic.SpiltCodeCama(TrzFCustObject.KalaCode);
                string kGruCode = UnitPublic.SpiltCodeCama(TrzFCustObject.KGruCode);
                string invCode = UnitPublic.SpiltCodeCama(TrzFCustObject.InvCode);
                string statusCode = UnitPublic.SpiltCodeCama(TrzFCustObject.StatusCode);

                string sql = string.Format(CultureInfo.InvariantCulture,
                          @"select  top ({14})  * FROM  {15}.dbo.Web_TrzFCust('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}', '{10}', '{11}',{12},'{13}') AS TrzFCust where 1 = 1 ",
                          TrzFCustObject.ModeCode1,
                          TrzFCustObject.ModeCode2,
                          TrzFCustObject.azTarikh,
                          TrzFCustObject.taTarikh,
                          TrzFCustObject.azShomarh,
                          TrzFCustObject.taShomarh,
                          kalaCode,
                          kGruCode,
                          mkzCode,
                          oprCode,
                          invCode,
                          statusCode,
                          TrzFCustObject.ZeroValue,
                          dataAccount[2],
                          TrzFCustObject.Top == null ? "10000" : TrzFCustObject.Top,
                          dBName);

                sql += UnitPublic.SpiltCodeAnd("CustCode", TrzFCustObject.CustCode);
                sql += UnitPublic.SpiltCodeAnd("CGruCode", TrzFCustObject.CGruCode);

                var listTrzFCust = DBase.DB.Database.SqlQuery<Web_TrzFCust>(sql);
                return Ok(listTrzFCust);
            }
            else
                return Ok(res);

            //string conStr = UnitDatabase.CreateConnectionString(dataAccount[0], dataAccount[1], dataAccount[2], dataAccount[3], ace, sal, group, 0, TrzFCustObject.ModeCode1.Substring(0, 1) == "S" ? UnitPublic.access_TrzFCust_S : UnitPublic.access_TrzFCust_P, UnitPublic.act_Report, 0);
        }






        public class TrzFDorehObject
        {
            public string mode { get; set; }

            public string azTarikh { get; set; }

            public string taTarikh { get; set; }

        }

        public class Web_D_TrzFDoreh
        {
            public Int64 totalvalue { get; set; }

            public string docdate { get; set; }

        }

        // Post: api/ReportFct/TrzFDoreh گزارش تراز خرید و  فروش مشتریان
        // HE_Report_TrzFDoreh
        [Route("api/ReportFct/TrzFDoreh/{ace}/{sal}/{group}")]
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PostWeb_D_TrzFDoreh(string ace, string sal, string group, TrzFDorehObject TrzFDorehObject)
        {
            string dBName = UnitDatabase.DatabaseName(ace, sal, group);
            var dataAccount = UnitDatabase.ReadUserPassHeader(this.Request.Headers);
            var DBase = UnitDatabase.dataDB.Where(p => p.UserName == dataAccount[0] && p.Password == dataAccount[1]).Single();
            string res = UnitDatabase.TestAcount(DBase, dataAccount[3], ace, group, UnitPublic.access_View);
            if (res == "")
            {
                string sql = string.Format(CultureInfo.InvariantCulture, @"select   * FROM  {0}.dbo.Web_D_TrzFDoreh({1}, '{2}', '{3}') AS TrzFDoreh where 1 = 1 ",
                                          dBName,
                                          TrzFDorehObject.mode,
                                          TrzFDorehObject.azTarikh,
                                          TrzFDorehObject.taTarikh);

                var listTrzFDoreh = DBase.DB.Database.SqlQuery<Web_D_TrzFDoreh>(sql);
                return Ok(listTrzFDoreh);
            }
            else
                return Ok(res);

            //string conStr = UnitDatabase.CreateConnectionString(dataAccount[0], dataAccount[1], dataAccount[2], dataAccount[3], ace, sal, group, 0,   UnitPublic.access_View, UnitPublic.act_Report, 0);
        }

    }
}
