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
            var dataAccount = UnitDatabase.ReadUserPassHeader(this.Request.Headers);
            string conStr = UnitDatabase.CreateConnectionString(dataAccount[0], dataAccount[1], dataAccount[2],dataAccount[3], ace, sal, group, 0, "14", 9, 0);
            if (conStr.Length > 100)
            {
                ApiModel db = new ApiModel(conStr);
                string oprCode = UnitPublic.SpiltCodeCama(TrzAccObject.OprCode);
                string mkzCode = UnitPublic.SpiltCodeCama(TrzAccObject.MkzCode);
                string aModeCode = UnitPublic.SpiltCodeCama(TrzAccObject.AModeCode);

                string sql = string.Format(CultureInfo.InvariantCulture,
                          @"select * FROM  dbo.Web_TrzAcc({0}, '{1}','{2}','{3}','{4}', '{5}','{6}') AS TrzAcc where 1 = 1 ",
                          TrzAccObject.Level,
                          TrzAccObject.azTarikh,
                          TrzAccObject.taTarikh,
                          oprCode,
                          mkzCode,
                          aModeCode,
                          dataAccount[2]);

                if (TrzAccObject.Sath == 1)
                    sql += string.Format(" and (Level = {0})", TrzAccObject.Level);
                else
                    sql += string.Format(" and (Level <= {0})", TrzAccObject.Level);

                sql += UnitPublic.SpiltCodeLike("AccCode", TrzAccObject.AccCode);

                sql += " order by  SortAccCode";

                var listTrzAcc =db.Database.SqlQuery<Web_TrzAcc>(sql);
                return Ok(listTrzAcc);
            }
            return Ok(conStr);
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

            public byte Naghl { get; set; }

        }

        // Post: api/ReportAcc/Web_Dftr گزارش دفتر حساب
        // HE_Report_Dftr
        [Route("api/ReportAcc/Dftr/{ace}/{sal}/{group}")]
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PostWeb_Dftr(string ace, string sal, string group, DftrObject DftrObject)
        {
            var dataAccount = UnitDatabase.ReadUserPassHeader(this.Request.Headers);
            string conStr = UnitDatabase.CreateConnectionString(dataAccount[0], dataAccount[1], dataAccount[2],dataAccount[3], ace, sal, group, 0, "15", 9, 0);
            if (conStr.Length > 100)
            {
                ApiModel db = new ApiModel(conStr);
                string status = UnitPublic.SpiltCodeCama(DftrObject.StatusCode);
                string oprCode = UnitPublic.SpiltCodeCama(DftrObject.OprCode);
                string mkzCode = UnitPublic.SpiltCodeCama(DftrObject.MkzCode);
                string aModeCode = UnitPublic.SpiltCodeCama(DftrObject.AModeCode);

                string sql = string.Format(CultureInfo.InvariantCulture,
                          @"select top(10000) * FROM  Web_Dftr('{0}','{1}',{2},'{3}','{4}','{5}','{6}','{7}',{8},{9},'{10}') AS Dftr where 1 = 1 and best >= 0 ",
                          DftrObject.azTarikh,
                          DftrObject.taTarikh,
                          DftrObject.Naghl,
                          DftrObject.AccCode,
                          aModeCode,
                          mkzCode,
                          oprCode,
                          status,
                          DftrObject.DispBands,
                          DftrObject.JamRooz,
                          dataAccount[2]);

                if (DftrObject.azShomarh != "")
                    sql += string.Format(" and DocNo >= '{0}' ", DftrObject.azShomarh);

                if (DftrObject.taShomarh != "")
                    sql += string.Format(" and DocNo <= '{0}' ", DftrObject.taShomarh);

                //sql += UnitPublic.SpiltCodeAnd("Status", DftrObject.StatusCode);
                //sql += UnitPublic.SpiltCodeAnd("ModeCode", DftrObject.AModeCode);
                //sql += UnitPublic.SpiltCodeAnd("OprCode", DftrObject.OprCode);
                //sql += UnitPublic.SpiltCodeAnd("MkzCode", DftrObject.MkzCode);

                sql += " order by bodytag";

                var listDftr =db.Database.SqlQuery<Web_Dftr>(sql);
                return Ok(listDftr);
            }
            return Ok(conStr);
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
            var dataAccount = UnitDatabase.ReadUserPassHeader(this.Request.Headers);
            string conStr = UnitDatabase.CreateConnectionString(dataAccount[0], dataAccount[1], dataAccount[2],dataAccount[3], ace, sal, group, 0, "16", 9, 0);
            if (conStr.Length > 100)
            {
                ApiModel db = new ApiModel(conStr);
                string status = UnitPublic.SpiltCodeCama(ADocRObject.StatusCode);
                string oprCode = UnitPublic.SpiltCodeCama(ADocRObject.OprCode);
                string mkzCode = UnitPublic.SpiltCodeCama(ADocRObject.MkzCode);
                string aModeCode = UnitPublic.SpiltCodeCama(ADocRObject.AModeCode);
                string AccCode = UnitPublic.SpiltCodeCama(ADocRObject.AccCode);

                string sql = string.Format(CultureInfo.InvariantCulture,
                          @"select top(10000) * FROM  Web_ADocR({0},{1},'{2}') AS ADocR where 1 = 1 ",
                          ADocRObject.DispBands,
                          ADocRObject.JamRooz,
                          dataAccount[2]);

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

                var listADocR =db.Database.SqlQuery<Web_ADocR>(sql);
                return Ok(listADocR);
            }
            return Ok(conStr);
        }



        public class TChkObject
        {
            public string azTarikh { get; set; }

            public string taTarikh { get; set; }

            public string azShomarh { get; set; }

            public string taShomarh { get; set; }

            public string AccCode { get; set; }

            public string PDMode { get; set; }

            public string CheckStatus { get; set; }

        }


        // Post: api/ReportAcc/Web_TChk گزارش صورت ریز چک ها
        // HE_Report_TChk
        [Route("api/ReportAcc/TChk/{ace}/{sal}/{group}")]
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PostWeb_TChk(string ace, string sal, string group, TChkObject TChkObject)
        {
            var dataAccount = UnitDatabase.ReadUserPassHeader(this.Request.Headers);
            string conStr = UnitDatabase.CreateConnectionString(dataAccount[0], dataAccount[1], dataAccount[2],dataAccount[3], ace, sal, group, 0, "17", 9, 0);
            if (conStr.Length > 100)
            {
                ApiModel db = new ApiModel(conStr);
                string sql = string.Format(CultureInfo.InvariantCulture,
                          @"select top(10000) * FROM  Web_TChk('{0}') AS TChk where 1 = 1", dataAccount[2]);

                sql += string.Format(" and PDMode = {0} ", TChkObject.PDMode);

                if (TChkObject.azTarikh != "")
                    sql += string.Format(" and CheckDate >= '{0}' ", TChkObject.azTarikh);

                if (TChkObject.taTarikh != "")
                    sql += string.Format(" and CheckDate <= '{0}' ", TChkObject.taTarikh);

                if (TChkObject.azShomarh != "")
                    sql += string.Format(" and CheckNo >= {0} ", TChkObject.azShomarh);

                if (TChkObject.taShomarh != "")
                    sql += string.Format(" and CheckNo <= {0} ", TChkObject.taShomarh);

                sql += UnitPublic.SpiltCodeAnd("CheckStatus", TChkObject.CheckStatus);
                sql += UnitPublic.SpiltCodeLike("AccCode", TChkObject.AccCode);
                sql += " order by CheckNo,Bank,Shobe";

                var listTChk =db.Database.SqlQuery<Web_TChk>(sql);
                return Ok(listTChk);
            }
            return Ok(conStr);
        }




        public class AGMkzObject
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

        // Post: api/ReportAcc/AGMkz گردش بر اساس مرکز هزینه
        // HE_Report_AGMkz
        [Route("api/ReportAcc/AGMkz/{ace}/{sal}/{group}")]
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PostWeb_AGMkz(string ace, string sal, string group, AGMkzObject AGMkzObject)
        {
            var dataAccount = UnitDatabase.ReadUserPassHeader(this.Request.Headers);
            string conStr = UnitDatabase.CreateConnectionString(dataAccount[0], dataAccount[1], dataAccount[2],dataAccount[3], ace, sal, group, 0, "14", 9, 0);
            if (conStr.Length > 100)
            {
                ApiModel db = new ApiModel(conStr);
                string oprCode = UnitPublic.SpiltCodeCama(AGMkzObject.OprCode);
                string accCode = UnitPublic.SpiltCodeCama(AGMkzObject.AccCode);
                string aModeCode = UnitPublic.SpiltCodeCama(AGMkzObject.AModeCode);

                string sql = string.Format(CultureInfo.InvariantCulture,
                          @"select * FROM  dbo.Web_AGMkz({0}, '{1}','{2}','{3}','{4}', '{5}','{6}') AS AGMkz where 1 = 1 ",
                          AGMkzObject.Level,
                          AGMkzObject.azTarikh,
                          AGMkzObject.taTarikh,
                          accCode,
                          oprCode,
                          aModeCode,
                          dataAccount[2]);

                /* if (AGMkzObject.Sath == 1)
                     sql += string.Format(" and (Level = {0})", AGMkzObject.Level);
                 else
                     sql += string.Format(" and (Level <= {0})", AGMkzObject.Level);*/

                sql += UnitPublic.SpiltCodeLike("MkzCode", AGMkzObject.MkzCode);

                sql += " order by  SortMkzCode";

                var listAGMkz =db.Database.SqlQuery<Web_AGMkz>(sql);
                return Ok(listAGMkz);
            }
            return Ok(conStr);
        }



        public class AGOprObject
        {
            public string azTarikh { get; set; }

            public string taTarikh { get; set; }

            public string OprCode { get; set; }

            public string MkzCode { get; set; }

            public string AModeCode { get; set; }

            public string AccCode { get; set; }

        }

        // Post: api/ReportAcc/AGOpr    گردش بر اساس  پروژه   
        // HE_Report_AGOpr
        [Route("api/ReportAcc/AGOpr/{ace}/{sal}/{group}")]
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PostWeb_AGOpr(string ace, string sal, string group, AGOprObject AGOprObject)
        {
            var dataAccount = UnitDatabase.ReadUserPassHeader(this.Request.Headers);
            string conStr = UnitDatabase.CreateConnectionString(dataAccount[0], dataAccount[1], dataAccount[2],dataAccount[3], ace, sal, group, 0, "14", 9, 0);
            if (conStr.Length > 100)
            {
                ApiModel db = new ApiModel(conStr);
                string mkzCode = UnitPublic.SpiltCodeCama(AGOprObject.MkzCode);
                string accCode = UnitPublic.SpiltCodeCama(AGOprObject.AccCode);
                string aModeCode = UnitPublic.SpiltCodeCama(AGOprObject.AModeCode);

                string sql = string.Format(CultureInfo.InvariantCulture,
                          @"select * FROM  dbo.Web_AGOpr('{0}', '{1}','{2}','{3}','{4}', '{5}') AS AGOpr where 1 = 1 ",
                          AGOprObject.azTarikh,
                          AGOprObject.taTarikh,
                          accCode,
                          mkzCode,
                          aModeCode,
                          dataAccount[2]);

                sql += UnitPublic.SpiltCodeLike("OprCode", AGOprObject.OprCode);

                sql += " order by SortOprCode";

                var listAGOpr =db.Database.SqlQuery<Web_AGOpr>(sql);
                return Ok(listAGOpr);
            }
            return Ok(conStr);
        }




        public class TChk_SumObject
        {
            public string azTarikh { get; set; }

            public string taTarikh { get; set; }

        }

        public class Web_D_TChk_Sum
        {
            public string Bank { get; set; }

            public string Shobe { get; set; }

            public double? Value { get; set; }
        }


        // Post: api/ReportAcc/Web_D_TChk_Sum گزارش صورت ریز چک ها
        // HE_Report_TChk_Sum
        [Route("api/ReportAcc/TChk_Sum/{ace}/{sal}/{group}")]
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PostWeb_D_TChk_Sum(string ace, string sal, string group, TChk_SumObject TChk_SumObject)
        {
            var dataAccount = UnitDatabase.ReadUserPassHeader(this.Request.Headers);
            string conStr = UnitDatabase.CreateConnectionString(dataAccount[0], dataAccount[1], dataAccount[2], dataAccount[3], ace, sal, group, 0, "17", 9, 0);
            if (conStr.Length > 100)
            {
                ApiModel db = new ApiModel(conStr);
                string sql = string.Format(CultureInfo.InvariantCulture,
                          @"select * FROM  Web_D_TChk_Sum('{0}','{1}','{2}') AS TChk_Sum where 1 = 1", 
                          dataAccount[2],
                          TChk_SumObject.azTarikh,
                          TChk_SumObject.taTarikh
                          );

                sql += " order by Bank,Shobe";

                var listTChk_Sum = db.Database.SqlQuery<Web_D_TChk_Sum>(sql);
                return Ok(listTChk_Sum);
            }
            return Ok(conStr);
        }



    }
}
