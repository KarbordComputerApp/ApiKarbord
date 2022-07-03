﻿using System;
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
    public class ReportInvController : ApiController
    {



        // گزارشات -----------------------------------------------------------------------------------------------------------------------
        public class TrzIObject
        {
            public string azTarikh { get; set; }

            public string taTarikh { get; set; }

            public string ModeCode { get; set; }

            public string InvCode { get; set; }

            public string KGruCode { get; set; }

            public string KalaCode { get; set; }

            public string ThvlCode { get; set; }

            public string TGruCode { get; set; }

            public string MkzCode { get; set; }

            public string OprCode { get; set; }

            public string StatusCode { get; set; }
        }

        // Post: api/ReportInv/TrzI گزارش موجودی انبار  
        // HE_Report_TrzIKala
        [Route("api/ReportInv/TrzI/{ace}/{sal}/{group}")]
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PostWeb_TrzIKala(string ace, string sal, string group, TrzIObject TrzIObject)
        {
            var dataAccount = UnitDatabase.ReadUserPassHeader(this.Request.Headers);
            string conStr = UnitDatabase.CreateConnectionString(dataAccount[0], dataAccount[1], dataAccount[2], ace, sal, group, 0, "22", 9, 0);
            if (conStr.Length > 100)
            {
                ApiModel db = new ApiModel(conStr);
                string modeCode = UnitPublic.SpiltCodeCama(TrzIObject.ModeCode);
                string kGruCode = UnitPublic.SpiltCodeCama(TrzIObject.KGruCode);
                string thvlCode = UnitPublic.SpiltCodeCama(TrzIObject.ThvlCode);
                string tGruCode = UnitPublic.SpiltCodeCama(TrzIObject.TGruCode);
                string oprCode = UnitPublic.SpiltCodeCama(TrzIObject.OprCode);
                string mkzCode = UnitPublic.SpiltCodeCama(TrzIObject.MkzCode);
                string invCode = UnitPublic.SpiltCodeCama(TrzIObject.InvCode);
                string statusCode = UnitPublic.SpiltCodeCama(TrzIObject.StatusCode);

                string sql = string.Format(CultureInfo.InvariantCulture,
                          @"select  top (10000) * FROM  dbo.Web_TrzIKala('{0}', '{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}') AS TrzI where 1 = 1 ",
                          TrzIObject.azTarikh,
                          TrzIObject.taTarikh,
                          modeCode,
                          kGruCode,
                          thvlCode,
                          tGruCode,
                          mkzCode,
                          oprCode,
                          invCode,
                          statusCode,
                          dataAccount[2]
                          );
                sql += UnitPublic.SpiltCodeAnd("KalaCode", TrzIObject.KalaCode);

                var listTrzI = db.Database.SqlQuery<Web_TrzIKala>(sql);
                return Ok(listTrzI);
            }
            return Ok(conStr);
        }

        public class TrzIExfObject
        {

            public string azTarikh { get; set; }

            public string taTarikh { get; set; }

            public string ModeCode { get; set; }

            public string InvCode { get; set; }

            public string KGruCode { get; set; }

            public string KalaCode { get; set; }

            public string ThvlCode { get; set; }

            public string TGruCode { get; set; }

            public string MkzCode { get; set; }

            public string OprCode { get; set; }

            public string StatusCode { get; set; }

        }
        // Post: api/ReportInv/TrzIExf گزارش موجودی انبار  
        // HE_Report_TrzIKalaExf
        [Route("api/ReportInv/TrzIExf/{ace}/{sal}/{group}")]
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PostWeb_TrzIKalaExf(string ace, string sal, string group, TrzIExfObject TrzIExfObject)
        {
            var dataAccount = UnitDatabase.ReadUserPassHeader(this.Request.Headers);
            string conStr = UnitDatabase.CreateConnectionString(dataAccount[0], dataAccount[1], dataAccount[2], ace, sal, group, 0, "23", 9, 0);
            if (conStr.Length > 100)
            {
                ApiModel db = new ApiModel(conStr);
                string modeCode = UnitPublic.SpiltCodeCama(TrzIExfObject.ModeCode);
                string kGruCode = UnitPublic.SpiltCodeCama(TrzIExfObject.KGruCode);
                string thvlCode = UnitPublic.SpiltCodeCama(TrzIExfObject.ThvlCode);
                string tGruCode = UnitPublic.SpiltCodeCama(TrzIExfObject.TGruCode);
                string oprCode = UnitPublic.SpiltCodeCama(TrzIExfObject.OprCode);
                string mkzCode = UnitPublic.SpiltCodeCama(TrzIExfObject.MkzCode);
                string invCode = UnitPublic.SpiltCodeCama(TrzIExfObject.InvCode);
                string statusCode = UnitPublic.SpiltCodeCama(TrzIExfObject.StatusCode);

                string sql = string.Format(CultureInfo.InvariantCulture,
                          @"select  top (10000) * FROM  dbo.Web_TrzIKalaExf('{0}', '{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}') AS TrzIExf where 1 = 1 ",
                          TrzIExfObject.azTarikh,
                          TrzIExfObject.taTarikh,
                          modeCode,
                          kGruCode,
                          thvlCode,
                          tGruCode,
                          mkzCode,
                          oprCode,
                          invCode,
                          statusCode,
                          dataAccount[2]
                          );
                sql += UnitPublic.SpiltCodeAnd("KalaCode", TrzIExfObject.KalaCode);

                sql += " order by KalaCode,KalaFileNo,KalaState,KalaExf1,KalaExf2,KalaExf3,KalaExf4,KalaExf5,KalaExf6,KalaExf7,KalaExf8,KalaExf9,KalaExf10,KalaExf11,KalaExf12,KalaExf13,KalaExf14,KalaExf15,InvCode,Tag ";

                var listTrzIExf = db.Database.SqlQuery<Web_TrzIKalaExf>(sql);
                return Ok(listTrzIExf);
            }
            return Ok(conStr);
        }


        public class IDocRObject
        {
            public string azTarikh { get; set; }

            public string taTarikh { get; set; }

            public long DocNo { get; set; }

            public string ModeCode { get; set; }

            public string NoSanadAnbar { get; set; }

            public string InvCode { get; set; }

            public string KGruCode { get; set; }

            public string KalaCode { get; set; }

            public string ThvlCode { get; set; }

            public string MkzCode { get; set; }

            public string OprCode { get; set; }

            public string StatusCode { get; set; }

        }

        // Post: api/ReportInv/IDocR گزارش ريز گردش اسناد انبارداري
        // HE_Report_IDocR
        [Route("api/ReportInv/IDocR/{ace}/{sal}/{group}")]
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PostWeb_IDocR(string ace, string sal, string group, IDocRObject IDocRObject)
        {
            var dataAccount = UnitDatabase.ReadUserPassHeader(this.Request.Headers);
            string conStr = UnitDatabase.CreateConnectionString(dataAccount[0], dataAccount[1], dataAccount[2], ace, sal, group, 0, "24", 9, 0);
            if (conStr.Length > 100)
            {
                ApiModel db = new ApiModel(conStr);
                string sql = string.Format(CultureInfo.InvariantCulture,
                          @"select top (10000) * FROM  dbo.Web_IDocR('{0}', '{1}','{2}') AS IDocR where 1 = 1 ",
                          IDocRObject.azTarikh, IDocRObject.taTarikh, dataAccount[2]);

                sql += UnitPublic.SpiltCodeAnd("InvCode", IDocRObject.InvCode);
                sql += UnitPublic.SpiltCodeAnd("KGruCode", IDocRObject.KGruCode);
                sql += UnitPublic.SpiltCodeAnd("KalaCode", IDocRObject.KalaCode);
                sql += UnitPublic.SpiltCodeAnd("ThvlCode", IDocRObject.ThvlCode);
                sql += UnitPublic.SpiltCodeAnd("OprCode", IDocRObject.OprCode);
                sql += UnitPublic.SpiltCodeAnd("MkzCode", IDocRObject.MkzCode);
                sql += UnitPublic.SpiltCodeAnd("Status", IDocRObject.StatusCode);
                sql += UnitPublic.SpiltCodeAnd("ModeCode", IDocRObject.ModeCode);

                if (IDocRObject.DocNo > 0)
                    sql += " and DocNo = " + IDocRObject.DocNo;

                sql += " order by DocNo ";

                var listIDocR = db.Database.SqlQuery<Web_IDocR>(sql);
                return Ok(listIDocR);
            }
            return Ok(conStr);
        }

        public class KrdxObject
        {
            public string azTarikh { get; set; }

            public string taTarikh { get; set; }

            public string ModeCode { get; set; }

            public string InvCode { get; set; }

            public string KGruCode { get; set; }

            public string KalaCode { get; set; }

            public string ThvlCode { get; set; }

            public string MkzCode { get; set; }

            public string OprCode { get; set; }

            public string StatusCode { get; set; }

            public string byKalaExf { get; set; }

            public byte Naghl { get; set; }


        }

        // Post: api/ReportInv/Krdx گزارش کاردکس کالا
        // HE_Report_Krdx
        [Route("api/ReportInv/Krdx/{ace}/{sal}/{group}")]
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PostWeb_Krdx(string ace, string sal, string group, KrdxObject KrdxObject)
        {
            var dataAccount = UnitDatabase.ReadUserPassHeader(this.Request.Headers);
            string conStr = UnitDatabase.CreateConnectionString(dataAccount[0], dataAccount[1], dataAccount[2], ace, sal, group, 0, "21", 9, 0);
            if (conStr.Length > 100)
            {
                ApiModel db = new ApiModel(conStr);
                string invCode = UnitPublic.SpiltCodeCama(KrdxObject.InvCode);
                string sql = string.Format(CultureInfo.InvariantCulture,
                          @"select * from (select top (10000) * FROM  dbo.Web_Krdx('{0}', '{1}',{2},'{3}','{4}','{5}') AS Krdx where 1 = 1 ",
                          KrdxObject.azTarikh,
                          KrdxObject.taTarikh,
                          KrdxObject.Naghl,
                          KrdxObject.KalaCode,
                          invCode,
                          dataAccount[2]);

                sql += UnitPublic.SpiltCodeAnd("KGruCode", KrdxObject.KGruCode);
                sql += UnitPublic.SpiltCodeAnd("ThvlCode", KrdxObject.ThvlCode);
                sql += UnitPublic.SpiltCodeAnd("OprCode", KrdxObject.OprCode);
                sql += UnitPublic.SpiltCodeAnd("MkzCode", KrdxObject.MkzCode);
                sql += UnitPublic.SpiltCodeAnd("Status", KrdxObject.StatusCode);
                sql += UnitPublic.SpiltCodeAnd("ModeCode", KrdxObject.ModeCode);


                sql += "or BodyTag = 0  order by BodyTag,DocNo ";
                sql += " ) as a where VAmount1 is not null";

                var listKrdx = db.Database.SqlQuery<Web_Krdx>(sql);
                return Ok(listKrdx);
            }
            return Ok(conStr);
        }


        // Get: api/ReportInv/Chante_FDoc_Moved گزارش وضعیت ها شرکت چنته 
        // HE_Report_Chante_FDoc_Moved
        [Route("api/ReportInv/Chante_FDoc_Moved/{ace}/{sal}/{group}")]
        public async Task<IHttpActionResult> GetWeb_Chante_FDoc_Moved(string ace, string sal, string group)
        {
            var dataAccount = UnitDatabase.ReadUserPassHeader(this.Request.Headers);
            string conStr = UnitDatabase.CreateConnectionString(dataAccount[0], dataAccount[1], dataAccount[2], ace, sal, group, 0, "27", 9, 0);
            if (conStr.Length > 100)
            {
                ApiModel db = new ApiModel(conStr);
                string sql = string.Format(CultureInfo.InvariantCulture,
                          @"select * from Web_Chante_FDoc_Moved where 1 = 1 ");

                var listChante_FDoc_Moved = db.Database.SqlQuery<Web_Chante_FDoc_Moved>(sql);
                return Ok(listChante_FDoc_Moved);
            }
            return Ok(conStr);
        }





        public class Web_TrzIKalaVstr
        {
            public string KalaCode { get; set; }
            public string KalaName { get; set; }

            //public string SortKalaCode { get; set; }

            //public byte? KalaDeghatM1 { get; set; }

            //public byte? KalaDeghatM2 { get; set; }

            //public byte? KalaDeghatM3 { get; set; }

            public double? MAmount1 { get; set; }

            public double? MAmount2 { get; set; }

            public double? MAmount3 { get; set; }

            public double MTotalPrice { get; set; }

        }

        public class TrzIKalaVstrObject
        {
            public string ModeCode { get; set; }

            public string DocDate { get; set; }

            public string InvCode { get; set; }

            public string KalaCode { get; set; }


        }

        // Post: api/ReportInv/TrzIKalaVstr گزارش موجودی انبار  
        // HE_Report_TrzIKalaVstr
        [Route("api/ReportInv/TrzIKalaVstr/{ace}/{sal}/{group}")]
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PostWeb_TrzIKalaVstr(string ace, string sal, string group, TrzIKalaVstrObject TrzIKalaVstrbject)
        {
            var dataAccount = UnitDatabase.ReadUserPassHeader(this.Request.Headers);
            string conStr = UnitDatabase.CreateConnectionString(dataAccount[0], dataAccount[1], dataAccount[2], ace, sal, group, 0, "22", 9, 0);
            if (conStr.Length > 100)
            {
                ApiModel db = new ApiModel(conStr);
                string sql = string.Format(CultureInfo.InvariantCulture,
                          @"select  top (10000) * FROM  dbo.Web_TrzIKalaVstr('{0}','{1}','{2}','{3}') AS TrzI where 1 = 1 ",
                          TrzIKalaVstrbject.ModeCode,
                          TrzIKalaVstrbject.DocDate,
                          TrzIKalaVstrbject.InvCode,
                          dataAccount[2]
                          );
                if (TrzIKalaVstrbject.KalaCode != "")
                {
                    sql += " and KalaCode = " + TrzIKalaVstrbject.KalaCode;
                }
                
                var listTrzIKalaVstr = db.Database.SqlQuery<Web_TrzIKalaVstr>(sql);
                return Ok(listTrzIKalaVstr);
            }
            return Ok(conStr);
        }



    }
}
