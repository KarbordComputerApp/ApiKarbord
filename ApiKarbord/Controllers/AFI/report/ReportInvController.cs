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

        public class IDocRObject
        {
            public string azTarikh { get; set; }

            public string taTarikh { get; set; }

            public string NoSanadAnbar { get; set; }

            public string InvCode { get; set; }

            public string KGruCode { get; set; }

            public string KalaCode { get; set; }

            public string ThvlCode { get; set; }

            public string MkzCode { get; set; }

            public string OprCode { get; set; }

        }

        // Post: api/ReportInv/IDocR گزارش ريز گردش اسناد انبارداري
        // HE_Report_IDocR
        [Route("api/ReportInv/IDocR/{ace}/{sal}/{group}")]
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PostWeb_IDocR(string ace, string sal, string group, IDocRObject IDocRObject)
        {
            var dataAccount = UnitDatabase.ReadUserPassHeader(this.Request.Headers);
            if (UnitDatabase.CreateConection(dataAccount[0], dataAccount[1], ace, sal, group))
            {
                string sql = string.Format(CultureInfo.InvariantCulture,
                          @"select top (10000) * FROM  dbo.Web_IDocR('{0}', '{1}') AS IDocR where 1 = 1 ",
                          IDocRObject.azTarikh, IDocRObject.taTarikh);

                sql += UnitPublic.SpiltCodeAnd("InvCode", IDocRObject.InvCode);
                sql += UnitPublic.SpiltCodeAnd("KGruCode", IDocRObject.KGruCode);
                sql += UnitPublic.SpiltCodeAnd("KalaCode", IDocRObject.KalaCode);
                sql += UnitPublic.SpiltCodeAnd("ThvlCode", IDocRObject.ThvlCode);
                sql += UnitPublic.SpiltCodeAnd("OprCode", IDocRObject.OprCode);
                sql += UnitPublic.SpiltCodeAnd("MkzCode", IDocRObject.MkzCode);

                if (IDocRObject.NoSanadAnbar != "0")
                {
                    sql += string.Format(" and (InOut = {0})", IDocRObject.NoSanadAnbar);
                }

                sql += " order by DocNo ";

                var listIDocR = UnitDatabase.db.Database.SqlQuery<Web_IDocR>(sql);
                return Ok(listIDocR);
            }
            return null;
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

            public string naghlAzGhabl { get; set; }

            public string byKalaExf { get; set; }

        }

        // Post: api/ReportInv/Krdx گزارش کاردکس کالا
        // HE_Report_Krdx
        [Route("api/ReportInv/Krdx/{ace}/{sal}/{group}")]
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PostWeb_Krdx(string ace, string sal, string group, KrdxObject KrdxObject)
        {
            var dataAccount = UnitDatabase.ReadUserPassHeader(this.Request.Headers);
            if (UnitDatabase.CreateConection(dataAccount[0], dataAccount[1], ace, sal, group))
            {

                string invCode = UnitPublic.SpiltCodeCama(KrdxObject.InvCode);
                string sql = string.Format(CultureInfo.InvariantCulture,
                          @"select top (10000) * FROM  dbo.Web_Krdx('{0}', '{1}') AS Krdx where 1 = 1 and Status <> '' ",
                          KrdxObject.KalaCode, invCode);

                sql += UnitPublic.SpiltCodeAnd("KGruCode", KrdxObject.KGruCode);
                sql += UnitPublic.SpiltCodeAnd("ThvlCode", KrdxObject.ThvlCode);
                sql += UnitPublic.SpiltCodeAnd("OprCode", KrdxObject.OprCode);
                sql += UnitPublic.SpiltCodeAnd("MkzCode", KrdxObject.MkzCode);
                sql += UnitPublic.SpiltCodeAnd("Status", KrdxObject.StatusCode);
                sql += UnitPublic.SpiltCodeAnd("ModeCode", KrdxObject.ModeCode);

                if (KrdxObject.azTarikh != "")
                    sql += string.Format(" and DocDate >= '{0}' ", KrdxObject.azTarikh);

                if (KrdxObject.taTarikh != "")
                    sql += string.Format(" and DocDate <= '{0}' ", KrdxObject.taTarikh);

                sql += " order by DocNo ";

                var listKrdx = UnitDatabase.db.Database.SqlQuery<Web_Krdx>(sql);
                return Ok(listKrdx);
            }
            return null;
        }

    }
}
