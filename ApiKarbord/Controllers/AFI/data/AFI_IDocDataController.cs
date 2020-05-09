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
    public class AFI_IDocDataController : ApiController
    {

        // GET: api/IDocData/IDocH اطلاعات تکمیلی سند انبار  
        [Route("api/IDocData/IDocH/{ace}/{sal}/{group}/{serialNumber}")]
        public IQueryable<Web_IDocH> GetWeb_IDocH(string ace, string sal, string group, long serialNumber)
        {
            if (UnitDatabase.CreateConection(ace, sal, group))
            {
                var a = UnitDatabase.db.Web_IDocH.Where(c => c.SerialNumber == serialNumber);
                return a;//UnitDatabase.db.Web_IDocH.Where(c => c.SerialNumber == serialNumber);
            }
            return null;
        }

        // GET: api/IDocData/IDocH تعداد رکورد ها    
        [Route("api/IDocData/IDocH/{ace}/{sal}/{group}/{InOut}/Count")]
        public async Task<IHttpActionResult> GetWeb_IDocHCount(string ace, string sal, string group, byte InOut)
        {
            if (UnitDatabase.CreateConection(ace, sal, group))
            {
                string sql = string.Format(@"SELECT count(SerialNumber) FROM Web_IDocH WHERE InOut = {0} ", InOut);
                int count = UnitDatabase.db.Database.SqlQuery<int>(sql).Single();
                return Ok(count);
            }
            return null;
        }
        // GET: api/IDocData/IDocH اخرین تاریخ    
        [Route("api/IDocData/IDocH/LastDate/{ace}/{sal}/{group}/{InOut}")]
        public async Task<IHttpActionResult> GetWeb_IDocHLastDate(string ace, string sal, string group, byte InOut)
        {
            if (UnitDatabase.CreateConection(ace, sal, group))
            {
                string lastdate;
                string sql = string.Format(@"SELECT count(DocDate) FROM Web_IDocH WHERE InOut = '{0}' ", InOut);
                int count = UnitDatabase.db.Database.SqlQuery<int>(sql).Single();
                if (count > 0)
                {
                    sql = string.Format(@"SELECT top(1) DocDate FROM Web_IDocH WHERE InOut = '{0}' order by DocDate desc", InOut);
                    lastdate = UnitDatabase.db.Database.SqlQuery<string>(sql).Single();
                }
                else
                    lastdate = "";
                return Ok(lastdate);
            }
            return null;
        }

        // GET: api/IDocData/IDocH لیست سند انبار   
        [Route("api/IDocData/IDocH/{ace}/{sal}/{group}/{InOut}/top{select}/{invSelect}/{userName}/{AccessSanad}")]
        public async Task<IHttpActionResult> GetAllWeb_IDocHMin(string ace, string sal, string group, byte InOut, int select, long invSelect, string userName, bool accessSanad)
        {

            if (UnitDatabase.CreateConection(ace, sal, group))
            {
                string sql = "declare @enddate nvarchar(20) ";
                //if (select == 1) // اگر انتخاب برای اخرین روز بود
                //    sql += string.Format(@" select @enddate = max(DocDate) from Web_IDocH where InOut = {0}", InOut);
                // else if (select == 2) // اگر انتخاب برای اخرین ماه بود
                //    sql += string.Format(@" select @enddate = substring(max(DocDate), 1, 7) from Web_IDocH where InOut = {0}", InOut);

                //if (ModeCode == "in" && (select == 1 || select == 2))
                //    sql += " (101,102,103,106,108,110) ";
                //else if (ModeCode == "out" && (select == 1 || select == 2))
                //    sql += " (104,105,107,109,111)";

                sql += "select ";
                if (select == 0)
                    sql += " top(100) ";

                sql += string.Format(@"SerialNumber,InOut,DocNo,SortDocNo,DocDate,ThvlCode,thvlname,Spec,KalaPriceCode,InvCode,ModeCode," +
                       "Status,PaymentType,Footer,Tanzim,Taeed,FinalPrice,Eghdam,ModeName,InvName " +
                       "from Web_IDocH where InOut = {0} ", InOut);

                //if (ModeCode == "in")
                //   sql += " (101,102,103,106,108,110) ";
                //else if (ModeCode == "out")
                //    sql += " (104,105,107,109,111)";

                if (invSelect > 0)
                {
                    sql += " and InvCode = '" + invSelect.ToString() + "' ";
                }

                if (select == 1)
                    sql += " and DocDate =  @enddate ";
                else if (select == 2)
                    sql += " and DocDate like  @enddate + '%' ";

                if (accessSanad == false)
                    sql += " and Eghdam = '" + userName + "' ";


                sql += " order by SortDocNo desc";
                var listIDocH = UnitDatabase.db.Database.SqlQuery<Web_IDocHMini>(sql);
                return Ok(listIDocH);
            }
            return null;
        }

        // GET: api/IDocData/IDocB اطلاعات تکمیلی سند انبار   
        [Route("api/IDocData/IDocB/{ace}/{sal}/{group}/{serialNumber}")]
        public async Task<IHttpActionResult> GetWeb_IDocB(string ace, string sal, string group, long serialNumber)
        {
            if (UnitDatabase.CreateConection(ace, sal, group))
            {
                string sql = string.Format(@"SELECT SerialNumber,BandNo,KalaCode,KalaName,MainUnit,MainUnitName,Amount1,Amount2,Amount3,UnitPrice,TotalPrice,Comm,Up_Flag,DeghatR1,DeghatR2,DeghatR3,DeghatM1,DeghatM2,DeghatM3,DeghatR
                                         FROM Web_IDocB WHERE SerialNumber = {0}", serialNumber);
                var listIDocB = UnitDatabase.db.Database.SqlQuery<Web_IDocB>(sql);
                return Ok(listIDocB);
            }
            return null;
        }

        [Route("api/IDocData/UpdatePriceAnbar/{ace}/{sal}/{group}/{serialnumber}")]
        [ResponseType(typeof(AFI_IDocBi))]
        public async Task<IHttpActionResult> PostWeb_UpdatePriceAnbar(string ace, string sal, string group, long serialnumber)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (UnitDatabase.CreateConection(ace, sal, group))
            {
                try
                {
                    string sql = string.Format(CultureInfo.InvariantCulture,
                          @"DECLARE	@return_value int
                            EXEC    @return_value = [dbo].[Web_IDocB_SetKalaPrice]
		                            @SerialNumber = {0}
                            SELECT  'Return Value' = @return_value",
                          serialnumber);
                    int value = UnitDatabase.db.Database.SqlQuery<int>(sql).Single();
                    if (value == 0)
                    {
                        await UnitDatabase.db.SaveChangesAsync();
                    }
                }
                catch (Exception)
                {
                    throw;
                }
            }

            string sql1 = string.Format(@"SELECT SerialNumber,BandNo,KalaCode,KalaName,MainUnit,MainUnitName,Amount1,Amount2,Amount3,UnitPrice,TotalPrice,Comm,Up_Flag,DeghatR1,DeghatR2,DeghatR3,DeghatM1,DeghatM2,DeghatM3,DeghatR
                                         FROM Web_IDocB WHERE SerialNumber = {0}", serialnumber);
            var listIDocB = UnitDatabase.db.Database.SqlQuery<Web_IDocB>(sql1);
            return Ok(listIDocB);
        }

        // GET: api/IDocData/IMode اطلاعات نوع سند انبار   
        [Route("api/IDocData/IMode/{ace}/{sal}/{group}/{InOut}")]
        public async Task<IHttpActionResult> GetWeb_IMode(string ace, string sal, string group, int InOut)
        {
            if (UnitDatabase.CreateConection(ace, sal, group))
            {
                string sql = string.Format(@"SELECT * FROM Web_IMode WHERE InOut = {0}", InOut);
                var listIMode = UnitDatabase.db.Database.SqlQuery<Web_IMode>(sql);
                return Ok(listIMode);
            }
            return null;
        }


    }

}
