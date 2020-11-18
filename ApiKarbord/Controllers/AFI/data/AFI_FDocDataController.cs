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
    public class AFI_FDocDataController : ApiController
    {

        // GET: api/FDocData/FMode اطلاعات نوع سند خرید و فروش   
        [Route("api/FDocData/FMode/{ace}/{sal}/{group}/{InOut}")]
        public async Task<IHttpActionResult> GetWeb_FMode(string ace, string sal, string group, int InOut)
        {
            var dataAccount = UnitDatabase.ReadUserPassHeader(this.Request.Headers);
            if (UnitDatabase.CreateConection(dataAccount[0], dataAccount[1], ace, sal, group))
            {
                string sql;
                if (InOut == 0)
                    sql = string.Format(@"SELECT * FROM Web_FMode order by OrderFld ");
                else
                    sql = string.Format(@"SELECT * FROM Web_FMode WHERE InOut = {0} order by OrderFld ", InOut);

                var listFMode = UnitDatabase.db.Database.SqlQuery<Web_FMode>(sql);
                return Ok(listFMode);
            }
            return null;
        }


        // GET: api/FDocData/FDocH اطلاعات تکمیلی فاکتور    
        [Route("api/FDocData/FDocH/{ace}/{sal}/{group}/{serialNumber}/{ModeCode}")]
        public async Task<IQueryable<Web_FDocH>> GetWeb_FDocH(string ace, string sal, string group, long serialNumber, string ModeCode)
        {
            var dataAccount = UnitDatabase.ReadUserPassHeader(this.Request.Headers);
            if (UnitDatabase.CreateConection(dataAccount[0], dataAccount[1], ace, sal, group))
            {
                var a = UnitDatabase.db.Web_FDocH.Where(c => c.SerialNumber == serialNumber && c.ModeCode == ModeCode);
                return a;
            }
            return null;
        }



        public class FDocHMinObject
        {
            public string ModeCode { get; set; }

            public int select { get; set; }

            public string user { get; set; }

            public bool AccessSanad { get; set; }

            public string updatedate { get; set; }

        }

        // Post: api/FDocData/FDocH لیست فاکتور    
        [Route("api/FDocData/FDocH/{ace}/{sal}/{group}")]
        public async Task<IHttpActionResult> PostAllWeb_FDocHMin(string ace, string sal, string group, FDocHMinObject FDocHMinObject)
        {

            var dataAccount = UnitDatabase.ReadUserPassHeader(this.Request.Headers);
            if (UnitDatabase.CreateConection(dataAccount[0], dataAccount[1], ace, sal, group))
            {

                string sql = "select ";
                if (FDocHMinObject.select == 0)
                    sql += " top(100) ";
                sql += string.Format(@"SerialNumber,                                   
                                       DocNo,
                                       SortDocNo,
                                       DocDate,
                                       CustCode,
                                       CustName,
                                       Spec,
                                       KalaPriceCode,
                                       InvCode,
                                       AddMinSpec1,
                                       AddMinSpec2,
                                       AddMinSpec3,
                                       AddMinSpec4,
                                       AddMinSpec5,
                                       AddMinSpec6,
                                       AddMinSpec7,
                                       AddMinSpec8,
                                       AddMinSpec9,
                                       AddMinSpec10,
                                       AddMinPrice1,
                                       AddMinPrice2,
                                       AddMinPrice3,
                                       AddMinPrice4,
                                       AddMinPrice5,
                                       AddMinPrice6,
                                       AddMinPrice7,
                                       AddMinPrice8,
                                       AddMinPrice9,
                                       AddMinPrice10,
                                       ModeCode,
                                       Status,
                                       PaymentType,
                                       Footer,
                                       Tanzim,
                                       Taeed,
                                       FinalPrice,
                                       Eghdam,
                                       F01,
                                       F02,
                                       F03,
                                       F04,
                                       F05,
                                       F06,
                                       F07,
                                       F08,
                                       F09,
                                       F10,
                                       F11,
                                       F12,
                                       F13,
                                       F14,
                                       F15,
                                       F16,
                                       F17,
                                       F18,
                                       F19,
                                       F20, 
                                       UpdateDate
                                       from Web_FDocH where ModeCode = '{0}' ",
                                       FDocHMinObject.ModeCode.ToString());
                if (FDocHMinObject.AccessSanad == false)
                    sql += " and Eghdam = '" + FDocHMinObject.user + "' ";

                if (FDocHMinObject.updatedate != null)
                    sql += " and UpdateDate >= CAST('" + FDocHMinObject.updatedate + "' AS DATETIME2)";

                sql += " order by SortDocNo desc ";
                var listFDocH = UnitDatabase.db.Database.SqlQuery<Web_FDocHMini>(sql);
                return Ok(listFDocH);
            }
            return null;
        }

        // GET: api/FDocData/FDocH تعداد رکورد ها    
        [Route("api/FDocData/FDocH/{ace}/{sal}/{group}/{ModeCode}")]
        public async Task<IHttpActionResult> GetWeb_FDocHCount(string ace, string sal, string group, string ModeCode)
        {
            var dataAccount = UnitDatabase.ReadUserPassHeader(this.Request.Headers);
            if (UnitDatabase.CreateConection(dataAccount[0], dataAccount[1], ace, sal, group))
            {
                string sql = string.Format(@"SELECT count(SerialNumber) FROM Web_FDocH WHERE ModeCode = '{0}'", ModeCode);
                int count = UnitDatabase.db.Database.SqlQuery<int>(sql).Single();
                return Ok(count);
            }
            return null;
        }

        // GET: api/FDocData/FDocH آخرین تاریخ فاکتور    
        [Route("api/FDocData/FDocH/LastDate/{ace}/{sal}/{group}/{ModeCode}")]
        public async Task<IHttpActionResult> GetWeb_FDocHLastDate(string ace, string sal, string group, string ModeCode)
        {
            var dataAccount = UnitDatabase.ReadUserPassHeader(this.Request.Headers);
            if (UnitDatabase.CreateConection(dataAccount[0], dataAccount[1], ace, sal, group))
            {
                string lastdate;
                string sql = string.Format(@"SELECT count(DocDate) FROM Web_FDocH WHERE ModeCode = '{0}' ", ModeCode);
                int count = UnitDatabase.db.Database.SqlQuery<int>(sql).Single();
                if (count > 0)
                {
                    sql = string.Format(@"SELECT top(1) DocDate FROM Web_FDocH WHERE ModeCode = '{0}' order by DocDate desc", ModeCode);
                    lastdate = UnitDatabase.db.Database.SqlQuery<string>(sql).Single();
                }
                else
                    lastdate = "";
                return Ok(lastdate);
            }
            return null;
        }

        // GET: api/FDocData/FDocB اطلاعات تکمیلی فاکتور    
        [Route("api/FDocData/FDocB/{ace}/{sal}/{group}/{serialNumber}")]
        public async Task<IHttpActionResult> GetWeb_FDocB(string ace, string sal, string group, long serialNumber)
        {
            var dataAccount = UnitDatabase.ReadUserPassHeader(this.Request.Headers);
            if (UnitDatabase.CreateConection(dataAccount[0], dataAccount[1], ace, sal, group))
            {
                string sql = string.Format(@"SELECT SerialNumber,BandNo,KalaCode,KalaName,MainUnit,MainUnitName,Amount1,Amount2,Amount3,UnitPrice,TotalPrice,Discount,Comm,Up_Flag,KalaDeghatR1,KalaDeghatR2,KalaDeghatR3,KalaDeghatM1,KalaDeghatM2,KalaDeghatM3,DeghatR
                                         FROM Web_FDocB WHERE SerialNumber = {0}", serialNumber);
                var listFactor = UnitDatabase.db.Database.SqlQuery<Web_FDocB>(sql);
                return Ok(listFactor);
            }
            return null;
        }

        [Route("api/FDocData/UpdatePrice/{ace}/{sal}/{group}/{serialnumber}")]
        [ResponseType(typeof(AFI_FDocBi))]
        public async Task<IHttpActionResult> PostWeb_UpdatePrice(string ace, string sal, string group, long serialnumber)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var dataAccount = UnitDatabase.ReadUserPassHeader(this.Request.Headers);
            if (UnitDatabase.CreateConection(dataAccount[0], dataAccount[1], ace, sal, group))
            {
                try
                {
                    string sql = string.Format(CultureInfo.InvariantCulture,
                          @"DECLARE	@return_value int
                            EXEC    @return_value = [dbo].[Web_FDocB_SetKalaPrice]
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
            string sql1 = string.Format(@"SELECT SerialNumber,BandNo,KalaCode,KalaName,MainUnit,MainUnitName,Amount1,Amount2,Amount3,UnitPrice,TotalPrice,Discount,Comm,Up_Flag,KalaDeghatR1,KalaDeghatR2,KalaDeghatR3,KalaDeghatM1,KalaDeghatM2,KalaDeghatM3,DeghatR
                                          FROM Web_FDocB WHERE SerialNumber = {0}", serialnumber);
            var listFactor = UnitDatabase.db.Database.SqlQuery<Web_FDocB>(sql1);
            return Ok(listFactor);
        }


        public class AFI_Move
        {
            public byte? DocNoMode { get; set; }

            public byte? InsertMode { get; set; }

            public string ModeCode { get; set; }

            public int? DocNo { get; set; }

            public int? StartNo { get; set; }

            public int? EndNo { get; set; }

            public byte? BranchCode { get; set; }

            public string UserCode { get; set; }

            public string TahieShode { get; set; }

            public long? SerialNumber { get; set; }

            public string DocDate { get; set; }

            public byte? MoveMode { get; set; }

            public long? oSerialNumber { get; set; }
        }



        [Route("api/FDocData/MoveFactor/{ace}/{sal}/{group}")]
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PostWeb_MoveFactor(string ace, string sal, string group, AFI_Move AFI_Move)
        {
            long value = 0;
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var dataAccount = UnitDatabase.ReadUserPassHeader(this.Request.Headers);
            if (UnitDatabase.CreateConection(dataAccount[0], dataAccount[1], ace, sal, group))
            {
                try
                {
                    string sql = string.Format(CultureInfo.InvariantCulture,
                          @"DECLARE	@return_value int,
		                            @oSerialNumber bigint

                            EXEC	@return_value = [dbo].[Web_SaveFDoc_Move]
		                            @DocNoMode = {0},
		                            @InsertMode = {1},
		                            @ModeCode = N'{2}',
		                            @DocNo = {3},
		                            @StartNo = {4},
		                            @EndNo = {5},
		                            @BranchCode = {6},
		                            @UserCode = '''{7}''',
		                            @TahieShode = '{8}',
		                            @SerialNumber = {9},
		                            @DocDate = '{10}',
                                    @MoveMode = {11} ,
		                            @oSerialNumber = @oSerialNumber OUTPUT
                            SELECT	@oSerialNumber as N'@oSerialNumber'",
                          AFI_Move.DocNoMode,
                          AFI_Move.InsertMode,
                          AFI_Move.ModeCode,
                          AFI_Move.DocNo,
                          AFI_Move.StartNo,
                          AFI_Move.EndNo,
                          AFI_Move.BranchCode,
                          AFI_Move.UserCode,
                          AFI_Move.TahieShode,
                          AFI_Move.SerialNumber,
                          AFI_Move.DocDate,
                          AFI_Move.MoveMode
                          );

                    value = UnitDatabase.db.Database.SqlQuery<long>(sql).Single();
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
            var list = UnitDatabase.db.Web_FDocH.Where(c => c.SerialNumber == value && c.ModeCode == AFI_Move.ModeCode);
            return Ok(list);
        }


        public class AFI_StatusChange
        {
            public byte DMode { get; set; }

            public string UserCode { get; set; }

            public long SerialNumber { get; set; }

            public string Status { get; set; }
        }

        [Route("api/FDocData/ChangeStatus/{ace}/{sal}/{group}")]
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PostWeb_ChangeStatus(string ace, string sal, string group, AFI_StatusChange AFI_StatusChange)
        {
            int value = 0;
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var dataAccount = UnitDatabase.ReadUserPassHeader(this.Request.Headers);
            if (UnitDatabase.CreateConection(dataAccount[0], dataAccount[1], ace, sal, group))
            {
                try
                {
                    string sql = string.Format(CultureInfo.InvariantCulture,
                          @"DECLARE	@return_value int
                            EXEC	@return_value = [dbo].[Web_SaveFDoc_Status]
		                            @DMode = {0},
		                            @UserCode = N'{1}',
		                            @SerialNumber = {2},
		                            @Status = N'{3}'
                            SELECT	'Return Value' = @return_value",
                          AFI_StatusChange.DMode,
                          AFI_StatusChange.UserCode,
                          AFI_StatusChange.SerialNumber,
                          AFI_StatusChange.Status);

                    value = UnitDatabase.db.Database.SqlQuery<int>(sql).Single();
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
            return Ok(200);
        }


        // GET: api/FDocData/FDocP لیست سند    
        [Route("api/FDocData/FDocP/{ace}/{sal}/{group}/{SerialNumber}")]
        public async Task<IHttpActionResult> GetAllWeb_FDocP(string ace, string sal, string group, long SerialNumber)
        {
            var dataAccount = UnitDatabase.ReadUserPassHeader(this.Request.Headers);
            if (UnitDatabase.CreateConection(dataAccount[0], dataAccount[1], ace, sal, group))
            {
                string sql = string.Format(@"select * from Web_FDocP where SerialNumber = {0} order by BandNo", SerialNumber);
                var listFDocP = UnitDatabase.db.Database.SqlQuery<Web_FDocP>(sql);
                return Ok(listFDocP);
            }
            return null;
        }


        public class AFI_TestFDocB
        {
            public long SerialNumber { get; set; }
            public string ModeCode { get; set; }

        }


        [Route("api/FDocData/TestFDocB/{ace}/{sal}/{group}")]
        [ResponseType(typeof(TestDocB))]
        public async Task<IHttpActionResult> PostWeb_TestFDocB(string ace, string sal, string group, AFI_TestFDocB AFI_TestFDocB)
        {
            var dataAccount = UnitDatabase.ReadUserPassHeader(this.Request.Headers);
            if (UnitDatabase.CreateConection(dataAccount[0], dataAccount[1], ace, sal, group))
            {
                string sql = string.Format(CultureInfo.InvariantCulture,
                                           @"EXEC	[dbo].[Web_TestFDocB] @serialNumber = {0} , @modeCode = '{1}'",
                                           AFI_TestFDocB.SerialNumber,
                                           AFI_TestFDocB.ModeCode
                                           );
                try
                {
                    var result = UnitDatabase.db.Database.SqlQuery<TestDocB>(sql).ToList();
                    var jsonResult = JsonConvert.SerializeObject(result);
                    return Ok(jsonResult);
                }
                catch (Exception e)
                {
                    throw;
                }

            }
            return null;

        }

    }
}
