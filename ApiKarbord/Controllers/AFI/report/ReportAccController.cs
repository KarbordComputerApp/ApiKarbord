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
            string dBName = UnitDatabase.DatabaseName(ace, sal, group);
            var dataAccount = UnitDatabase.ReadUserPassHeader(this.Request.Headers);
            var DBase = UnitDatabase.dataDB.Where(p => p.UserName.ToUpper() == dataAccount[0].ToUpper() && p.Password == dataAccount[1]).Single();
            string res = UnitDatabase.TestAcount(DBase, dataAccount[3], ace, group, UnitPublic.access_TrzAcc);
            if (res == "")
            {
                string oprCode = UnitPublic.SpiltCodeCama(TrzAccObject.OprCode);
                string mkzCode = UnitPublic.SpiltCodeCama(TrzAccObject.MkzCode);
                string aModeCode = UnitPublic.SpiltCodeCama(TrzAccObject.AModeCode);

                string sql = string.Format(CultureInfo.InvariantCulture,
                          @"select * FROM  {7}.dbo.Web_TrzAcc({0}, '{1}','{2}','{3}','{4}', '{5}','{6}') AS TrzAcc where 1 = 1 ",
                          TrzAccObject.Level,
                          TrzAccObject.azTarikh,
                          TrzAccObject.taTarikh,
                          oprCode,
                          mkzCode,
                          aModeCode,
                          dataAccount[2],
                          dBName);

                if (TrzAccObject.Sath == 1)
                    sql += string.Format(" and (Level = {0})", TrzAccObject.Level);
                else
                    sql += string.Format(" and (Level <= {0})", TrzAccObject.Level);

                sql += UnitPublic.SpiltCodeLike("AccCode", TrzAccObject.AccCode);

                sql += " order by  SortAccCode";

                var listTrzAcc = DBase.DB.Database.SqlQuery<Web_TrzAcc>(sql);
                UnitDatabase.SaveLog(dataAccount[0], dataAccount[1], dataAccount[2], ace, sal, group,0,  UnitPublic.access_TrzAcc, UnitPublic.act_Report, "Y", 1, 0);

                return Ok(listTrzAcc);
            }
            else
                return Ok(res);

            //string conStr = UnitDatabase.CreateConnectionString(dataAccount[0], dataAccount[1], dataAccount[2], dataAccount[3], ace, sal, group, 0, UnitPublic.access_TrzAcc, UnitPublic.act_Report, 0);
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
            string dBName = UnitDatabase.DatabaseName(ace, sal, group);
            var dataAccount = UnitDatabase.ReadUserPassHeader(this.Request.Headers);
            var DBase = UnitDatabase.dataDB.Where(p => p.UserName.ToUpper() == dataAccount[0].ToUpper() && p.Password == dataAccount[1]).Single();
            string res = UnitDatabase.TestAcount(DBase, dataAccount[3], ace, group, UnitPublic.access_Dftr);
            if (res == "")
            {
                string status = UnitPublic.SpiltCodeCama(DftrObject.StatusCode);
                string oprCode = UnitPublic.SpiltCodeCama(DftrObject.OprCode);
                string mkzCode = UnitPublic.SpiltCodeCama(DftrObject.MkzCode);
                string aModeCode = UnitPublic.SpiltCodeCama(DftrObject.AModeCode);

                string sql = string.Format(CultureInfo.InvariantCulture,
                          @"select top(10000) * FROM  {11}.dbo.Web_Dftr('{0}','{1}',{2},'{3}','{4}','{5}','{6}','{7}',{8},{9},'{10}') AS Dftr where 1 = 1 and best >= 0 ",
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
                          dataAccount[2],
                          dBName);

                if (DftrObject.azShomarh != "")
                    sql += string.Format(" and DocNo >= '{0}' ", DftrObject.azShomarh);

                if (DftrObject.taShomarh != "")
                    sql += string.Format(" and DocNo <= '{0}' ", DftrObject.taShomarh);

                //sql += UnitPublic.SpiltCodeAnd("Status", DftrObject.StatusCode);
                //sql += UnitPublic.SpiltCodeAnd("ModeCode", DftrObject.AModeCode);
                //sql += UnitPublic.SpiltCodeAnd("OprCode", DftrObject.OprCode);
                //sql += UnitPublic.SpiltCodeAnd("MkzCode", DftrObject.MkzCode);

                sql += " order by bodytag";

                var listDftr = DBase.DB.Database.SqlQuery<Web_Dftr>(sql);
                UnitDatabase.SaveLog(dataAccount[0], dataAccount[1], dataAccount[2], ace, sal, group, 0, UnitPublic.access_Dftr, UnitPublic.act_Report, "Y", 1, 0);
                return Ok(listDftr);
            }
            else
                return Ok(res);

            //string conStr = UnitDatabase.CreateConnectionString(dataAccount[0], dataAccount[1], dataAccount[2], dataAccount[3], ace, sal, group, 0, UnitPublic.access_Dftr, UnitPublic.act_Report, 0);
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
            string dBName = UnitDatabase.DatabaseName(ace, sal, group);
            var dataAccount = UnitDatabase.ReadUserPassHeader(this.Request.Headers);
            var DBase = UnitDatabase.dataDB.Where(p => p.UserName.ToUpper() == dataAccount[0].ToUpper() && p.Password == dataAccount[1]).Single();
            string res = UnitDatabase.TestAcount(DBase, dataAccount[3], ace, group, UnitPublic.access_ADocR);
            if (res == "")
            {
                string status = UnitPublic.SpiltCodeCama(ADocRObject.StatusCode);
                string oprCode = UnitPublic.SpiltCodeCama(ADocRObject.OprCode);
                string mkzCode = UnitPublic.SpiltCodeCama(ADocRObject.MkzCode);
                string aModeCode = UnitPublic.SpiltCodeCama(ADocRObject.AModeCode);
                string AccCode = UnitPublic.SpiltCodeCama(ADocRObject.AccCode);

                string sql = string.Format(CultureInfo.InvariantCulture,
                          @"select top(10000) * FROM  {0}.dbo.Web_ADocR({1},{2},'{3}') AS ADocR where 1 = 1 ",
                          dBName,
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

                var listADocR = DBase.DB.Database.SqlQuery<Web_ADocR>(sql);
                UnitDatabase.SaveLog(dataAccount[0], dataAccount[1], dataAccount[2], ace, sal, group, 0, UnitPublic.access_ADocR, UnitPublic.act_Report, "Y", 1, 0);
                return Ok(listADocR);
            }
            else
                return Ok(res);

            //string conStr = UnitDatabase.CreateConnectionString(dataAccount[0], dataAccount[1], dataAccount[2], dataAccount[3], ace, sal, group, 0, UnitPublic.access_ADocR, UnitPublic.act_Report, 0);
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
            string dBName = UnitDatabase.DatabaseName(ace, sal, group);
            var dataAccount = UnitDatabase.ReadUserPassHeader(this.Request.Headers);
            var DBase = UnitDatabase.dataDB.Where(p => p.UserName.ToUpper() == dataAccount[0].ToUpper() && p.Password == dataAccount[1]).Single();
            string res = UnitDatabase.TestAcount(DBase, dataAccount[3], ace, group, UnitPublic.access_TChk);
            if (res == "")
            {
                string sql = string.Format(CultureInfo.InvariantCulture,
                           @"select top(10000) * FROM  {0}.dbo.Web_TChk('{1}') AS TChk where 1 = 1", dBName, dataAccount[2]);

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

                var listTChk = DBase.DB.Database.SqlQuery<Web_TChk>(sql);
                UnitDatabase.SaveLog(dataAccount[0], dataAccount[1], dataAccount[2], ace, sal, group, 0, UnitPublic.access_TChk, UnitPublic.act_Report, "Y", 1, 0);
                return Ok(listTChk);
            }
            else
                return Ok(res);

            //string conStr = UnitDatabase.CreateConnectionString(dataAccount[0], dataAccount[1], dataAccount[2], dataAccount[3], ace, sal, group, 0, UnitPublic.access_TChk, UnitPublic.act_Report, 0);
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
            string dBName = UnitDatabase.DatabaseName(ace, sal, group);
            var dataAccount = UnitDatabase.ReadUserPassHeader(this.Request.Headers);
            var DBase = UnitDatabase.dataDB.Where(p => p.UserName.ToUpper() == dataAccount[0].ToUpper() && p.Password == dataAccount[1]).Single();
            string res = UnitDatabase.TestAcount(DBase, dataAccount[3], ace, group, UnitPublic.access_AGMkz);
            if (res == "")
            {
                string oprCode = UnitPublic.SpiltCodeCama(AGMkzObject.OprCode);
                string accCode = UnitPublic.SpiltCodeCama(AGMkzObject.AccCode);
                string aModeCode = UnitPublic.SpiltCodeCama(AGMkzObject.AModeCode);

                string sql = string.Format(CultureInfo.InvariantCulture,
                          @"select * FROM  {7}.dbo.Web_AGMkz({0}, '{1}','{2}','{3}','{4}', '{5}','{6}') AS AGMkz where 1 = 1 ",
                          AGMkzObject.Level,
                          AGMkzObject.azTarikh,
                          AGMkzObject.taTarikh,
                          accCode,
                          oprCode,
                          aModeCode,
                          dataAccount[2],
                          dBName);

                /* if (AGMkzObject.Sath == 1)
                     sql += string.Format(" and (Level = {0})", AGMkzObject.Level);
                 else
                     sql += string.Format(" and (Level <= {0})", AGMkzObject.Level);*/

                sql += UnitPublic.SpiltCodeLike("MkzCode", AGMkzObject.MkzCode);

                sql += " order by  SortMkzCode";

                var listAGMkz = DBase.DB.Database.SqlQuery<Web_AGMkz>(sql);
                UnitDatabase.SaveLog(dataAccount[0], dataAccount[1], dataAccount[2], ace, sal, group, 0, UnitPublic.access_AGMkz, UnitPublic.act_Report, "Y", 1, 0);
                return Ok(listAGMkz);
            }
            else
                return Ok(res);

            //string conStr = UnitDatabase.CreateConnectionString(dataAccount[0], dataAccount[1], dataAccount[2], dataAccount[3], ace, sal, group, 0, UnitPublic.access_AGMkz, UnitPublic.act_Report, 0);
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
            string dBName = UnitDatabase.DatabaseName(ace, sal, group);
            var dataAccount = UnitDatabase.ReadUserPassHeader(this.Request.Headers);
            var DBase = UnitDatabase.dataDB.Where(p => p.UserName.ToUpper() == dataAccount[0].ToUpper() && p.Password == dataAccount[1]).Single();
            string res = UnitDatabase.TestAcount(DBase, dataAccount[3], ace, group, UnitPublic.access_AGOpr);
            if (res == "")
            {
                string mkzCode = UnitPublic.SpiltCodeCama(AGOprObject.MkzCode);
                string accCode = UnitPublic.SpiltCodeCama(AGOprObject.AccCode);
                string aModeCode = UnitPublic.SpiltCodeCama(AGOprObject.AModeCode);

                string sql = string.Format(CultureInfo.InvariantCulture,
                          @"select * FROM  {6}.dbo.Web_AGOpr('{0}', '{1}','{2}','{3}','{4}', '{5}') AS AGOpr where 1 = 1 ",
                          AGOprObject.azTarikh,
                          AGOprObject.taTarikh,
                          accCode,
                          mkzCode,
                          aModeCode,
                          dataAccount[2],
                          dBName);

                sql += UnitPublic.SpiltCodeLike("OprCode", AGOprObject.OprCode);

                sql += " order by SortOprCode";

                UnitDatabase.SaveLog(dataAccount[0], dataAccount[1], dataAccount[2], ace, sal, group, 0, UnitPublic.access_AGOpr, UnitPublic.act_Report, "Y", 1, 0);
                var listAGOpr = DBase.DB.Database.SqlQuery<Web_AGOpr>(sql);
                return Ok(listAGOpr);
            }
            else
                return Ok(res);

            //string conStr = UnitDatabase.CreateConnectionString(dataAccount[0], dataAccount[1], dataAccount[2], dataAccount[3], ace, sal, group, 0, UnitPublic.access_AGOpr, UnitPublic.act_Report, 0);
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
            string dBName = UnitDatabase.DatabaseName(ace, sal, group);
            var dataAccount = UnitDatabase.ReadUserPassHeader(this.Request.Headers);
            var DBase = UnitDatabase.dataDB.Where(p => p.UserName.ToUpper() == dataAccount[0].ToUpper() && p.Password == dataAccount[1]).Single();
            string res = UnitDatabase.TestAcount(DBase, dataAccount[3], ace, group, UnitPublic.access_TChk);
            if (res == "")
            {
                string sql = string.Format(CultureInfo.InvariantCulture,
                          @"select * FROM  {0}.dbo.Web_D_TChk_Sum('{1}','{2}','{3}') AS TChk_Sum where 1 = 1",
                          dBName,
                          dataAccount[2],
                          TChk_SumObject.azTarikh,
                          TChk_SumObject.taTarikh
                          );

                sql += " order by Bank,Shobe";

                UnitDatabase.SaveLog(dataAccount[0], dataAccount[1], dataAccount[2], ace, sal, group, 0, UnitPublic.access_TChk, UnitPublic.act_Report, "Y", 1, 0);
                var listTChk_Sum = DBase.DB.Database.SqlQuery<Web_D_TChk_Sum>(sql);
                return Ok(listTChk_Sum);
            }
            else
                return Ok(res);

            //string conStr = UnitDatabase.CreateConnectionString(dataAccount[0], dataAccount[1], dataAccount[2], dataAccount[3], ace, sal, group, 0, UnitPublic.access_TChk, UnitPublic.act_Report, 0);
        }



        public class GrdZAccObject
        {
            public string azTarikh { get; set; }

            public string taTarikh { get; set; }

            public string OprCode { get; set; }

            public string MkzCode { get; set; }

            public string AModeCode { get; set; }

            public string ZAccCode { get; set; }

            public string AccCode { get; set; }

            public string ZGruCode { get; set; }

        }

        // Post: api/ReportAcc/GrdZAcc گزارش تراز دفاتر
        // HE_Report_GrdZAcc
        [Route("api/ReportAcc/GrdZAcc/{ace}/{sal}/{group}")]
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PostWeb_GrdZAcc(string ace, string sal, string group, GrdZAccObject GrdZAccObject)
        {
            string dBName = UnitDatabase.DatabaseName(ace, sal, group);
            var dataAccount = UnitDatabase.ReadUserPassHeader(this.Request.Headers);
            var DBase = UnitDatabase.dataDB.Where(p => p.UserName.ToUpper() == dataAccount[0].ToUpper() && p.Password == dataAccount[1]).Single();
            string res = UnitDatabase.TestAcount(DBase, dataAccount[3], ace, group, UnitPublic.access_GrdZAcc);
            if (res == "")
            {
                string oprCode = UnitPublic.SpiltCodeCama(GrdZAccObject.OprCode);
                string mkzCode = UnitPublic.SpiltCodeCama(GrdZAccObject.MkzCode);
                string aModeCode = UnitPublic.SpiltCodeCama(GrdZAccObject.AModeCode);

                string sql = string.Format(CultureInfo.InvariantCulture,
                          @"select Tag,cast(ZGruCode as nvarchar(250)) as ZGruCode,ZAccCode,ZAccName,AccCode,AccName,Bede,Best,MonBede,MonBest,MonTotal FROM  {0}.dbo.Web_GrdZAcc('{1}','{2}','{3}','{4}','{5}','{6}') AS GrdZAcc where 1 = 1",
                          dBName,
                          GrdZAccObject.azTarikh,
                          GrdZAccObject.taTarikh, 
                          oprCode,
                          mkzCode,
                          aModeCode,
                          dataAccount[2]
                          );
                sql += UnitPublic.SpiltCodeAnd("ZAccCode", GrdZAccObject.ZAccCode);
                sql += UnitPublic.SpiltCodeLike("ZGruCode", GrdZAccObject.ZGruCode);
                sql += UnitPublic.SpiltCodeLike("AccCode", GrdZAccObject.AccCode);
                sql += " order by ZAccCode,Tag,AccCode";

                var listGrdZAcc = DBase.DB.Database.SqlQuery<Web_GrdZAcc>(sql);
                UnitDatabase.SaveLog(dataAccount[0], dataAccount[1], dataAccount[2], ace, sal, group, 0, UnitPublic.access_GrdZAcc, UnitPublic.act_Report, "Y", 1, 0);

                return Ok(listGrdZAcc);
            }
            else
                return Ok(res);

        }




        public class FADocObject
        {
            public string azTarikh { get; set; }

            public string taTarikh { get; set; }

            public string azShomarh { get; set; }

            public string taShomarh { get; set; }

            public string StatusCode { get; set; }

            public string AModeCode { get; set; }

        }

        // Post: api/ReportAcc/Web_FADoc گزارش فهرست اسناد حسابداری
        // HE_Report_FADoc
        [Route("api/ReportAcc/FADoc/{ace}/{sal}/{group}")]
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PostWeb_FADoc(string ace, string sal, string group, FADocObject FADocObject)
        {
            string dBName = UnitDatabase.DatabaseName(ace, sal, group);
            var dataAccount = UnitDatabase.ReadUserPassHeader(this.Request.Headers);
            var DBase = UnitDatabase.dataDB.Where(p => p.UserName.ToUpper() == dataAccount[0].ToUpper() && p.Password == dataAccount[1]).Single();
            string res = UnitDatabase.TestAcount(DBase, dataAccount[3], ace, group, UnitPublic.access_FADoc);
            if (res == "")
            {
                string status = UnitPublic.SpiltCodeCama(FADocObject.StatusCode);
                string aModeCode = UnitPublic.SpiltCodeCama(FADocObject.AModeCode);

                string sql = string.Format(CultureInfo.InvariantCulture,
                          @"select top(10000) * FROM  {0}.dbo.Web_FADoc AS Web_FADoc where 1 = 1 ",
                          dBName);

                if (FADocObject.azTarikh != "")
                    sql += string.Format(" and DocDate >= '{0}' ", FADocObject.azTarikh);

                if (FADocObject.taTarikh != "")
                    sql += string.Format(" and DocDate <= '{0}' ", FADocObject.taTarikh);


                if (FADocObject.azShomarh != "")
                    sql += string.Format(" and DocNo >= '{0}' ", FADocObject.azShomarh);

                if (FADocObject.taShomarh != "")
                    sql += string.Format(" and DocNo <= '{0}' ", FADocObject.taShomarh);

                sql += UnitPublic.SpiltCodeAnd("Status", FADocObject.StatusCode);
                sql += UnitPublic.SpiltCodeAnd("ModeCode", FADocObject.AModeCode);
                sql += " order by DocNo";

                var listFADoc = DBase.DB.Database.SqlQuery<Web_FADoc>(sql);
                UnitDatabase.SaveLog(dataAccount[0], dataAccount[1], dataAccount[2], ace, sal, group, 0, UnitPublic.access_FADoc, UnitPublic.act_Report, "Y", 1, 0);
                return Ok(listFADoc);
            }
            else
                return Ok(res);
        }



        public class KhlAccObject
        {
            public string azTarikh { get; set; }

            public string taTarikh { get; set; }

            public string AccCode { get; set; }

            public string AModeCode { get; set; }

            public string OprCode { get; set; }

            public string MkzCode { get; set; }

        }


        // Post: api/ReportAcc/KhlAcc گزارش صورت خلاصه حسابها
        [Route("api/ReportAcc/KhlAcc/{ace}/{sal}/{group}")]
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PostWeb_KhlAcc(string ace, string sal, string group, KhlAccObject KhlAccObject)
        {
            string dBName = UnitDatabase.DatabaseName(ace, sal, group);
            var dataAccount = UnitDatabase.ReadUserPassHeader(this.Request.Headers);
            var DBase = UnitDatabase.dataDB.Where(p => p.UserName.ToUpper() == dataAccount[0].ToUpper() && p.Password == dataAccount[1]).Single();
            string res = UnitDatabase.TestAcount(DBase, dataAccount[3], ace, group, UnitPublic.access_KhlAcc);
            if (res == "")
            {
                string oprCode = UnitPublic.SpiltCodeCama(KhlAccObject.OprCode);
                string mkzCode = UnitPublic.SpiltCodeCama(KhlAccObject.MkzCode);
                string aModeCode = UnitPublic.SpiltCodeCama(KhlAccObject.AModeCode);

                string sql = string.Format(CultureInfo.InvariantCulture,
                          @"select * FROM  {6}.dbo.Web_KhlAcc('{0}','{1}','{2}','{3}','{4}', '{5}') AS KhlAcc where 1 = 1 ",
                          KhlAccObject.azTarikh,
                          KhlAccObject.taTarikh,
                          oprCode,
                          mkzCode,
                          aModeCode,
                          dataAccount[2],
                          dBName);

                sql += UnitPublic.SpiltCodeLike("AccCode", KhlAccObject.AccCode);

                sql += " order by SortAccCode";

                var listKhlAcc = DBase.DB.Database.SqlQuery<Web_KhlAcc>(sql);
                UnitDatabase.SaveLog(dataAccount[0], dataAccount[1], dataAccount[2], ace, sal, group, 0, UnitPublic.access_KhlAcc, UnitPublic.act_Report, "Y", 1, 0);

                return Ok(listKhlAcc);
            }
            else
                return Ok(res);

        }


        public class KhlZAccObject
        {
            public string azTarikh { get; set; }

            public string taTarikh { get; set; }

            public string ZAccCode { get; set; }

            public string AModeCode { get; set; }

            public string OprCode { get; set; }

            public string MkzCode { get; set; }

        }


        // Post: api/ReportAcc/KhlZAcc گزارش صورت خلاصه زیر حساب ها حسابها
        [Route("api/ReportAcc/KhlZAcc/{ace}/{sal}/{group}")]
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PostWeb_KhlZAcc(string ace, string sal, string group, KhlZAccObject KhlZAccObject)
        {
            string dBName = UnitDatabase.DatabaseName(ace, sal, group);
            var dataAccount = UnitDatabase.ReadUserPassHeader(this.Request.Headers);
            var DBase = UnitDatabase.dataDB.Where(p => p.UserName.ToUpper() == dataAccount[0].ToUpper() && p.Password == dataAccount[1]).Single();
            string res = UnitDatabase.TestAcount(DBase, dataAccount[3], ace, group, UnitPublic.access_KhlZAcc);
            if (res == "")
            {
                string oprCode = UnitPublic.SpiltCodeCama(KhlZAccObject.OprCode);
                string mkzCode = UnitPublic.SpiltCodeCama(KhlZAccObject.MkzCode);
                string aModeCode = UnitPublic.SpiltCodeCama(KhlZAccObject.AModeCode);

                string sql = string.Format(CultureInfo.InvariantCulture,
                          @"select * FROM  {6}.dbo.Web_KhlZAcc('{0}','{1}','{2}','{3}','{4}', '{5}') AS KhlZAcc where 1 = 1 ",
                          KhlZAccObject.azTarikh,
                          KhlZAccObject.taTarikh,
                          oprCode,
                          mkzCode,
                          aModeCode,
                          dataAccount[2],
                          dBName);

                sql += UnitPublic.SpiltCodeLike("ZAccCode", KhlZAccObject.ZAccCode);

                sql += " order by ZAccCode";

                var listKhlZAcc = DBase.DB.Database.SqlQuery<Web_KhlZAcc>(sql);
                UnitDatabase.SaveLog(dataAccount[0], dataAccount[1], dataAccount[2], ace, sal, group, 0, UnitPublic.access_KhlZAcc, UnitPublic.act_Report, "Y", 1, 0);

                return Ok(listKhlZAcc);
            }
            else
                return Ok(res);

        }


    }
}
