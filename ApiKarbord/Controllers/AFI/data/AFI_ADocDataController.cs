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
    public class AFI_ADocDataController : ApiController
    {
        // GET: api/ADocData/AMode اطلاعات نوع سند حسابداری   
        [Route("api/ADocData/AMode/{ace}/{sal}/{group}")]
        public async Task<IHttpActionResult> GetWeb_AMode(string ace, string sal, string group)
        {
            var dataAccount = UnitDatabase.ReadUserPassHeader(this.Request.Headers);
            string con = UnitDatabase.CreateConection(dataAccount[0], dataAccount[1], dataAccount[2], ace, sal, group, 0, "", 0, 0);
            if (con == "ok")
            {
                string sql = "SELECT * FROM Web_AMode";
                var listAMode = UnitDatabase.db.Database.SqlQuery<Web_AMode>(sql);
                return Ok(listAMode);
            }
            return Ok(con);
        }

        // GET: api/ADocData/CheckStatus اطلاعات نوع سند حسابداری   
        [Route("api/ADocData/CheckStatus/{ace}/{sal}/{group}/{PDMode}/{Report}")]

        public IQueryable<Web_CheckStatus> GetWeb_CheckStatus(string ace, string sal, string group, int PDMode, int Report)
        {
            var dataAccount = UnitDatabase.ReadUserPassHeader(this.Request.Headers);
            string con = UnitDatabase.CreateConection(dataAccount[0], dataAccount[1], dataAccount[2], ace, sal, group, 0, "", 0, 0);
            if (con == "ok")
            {
                return UnitDatabase.db.Web_CheckStatus.Where(c => c.PDMode == PDMode && c.Report == Report);
            }
            return null;
        }



        public class ADocHObject
        {

            public int Select { get; set; }

            public string User { get; set; }

            public bool AccessSanad { get; set; }

            public string Sort { get; set; }

            public string ModeSort { get; set; }

            public string DocNo { get; set; }

        }


        // GET: api/ADocData/ADocH لیست سند    
        [Route("api/ADocData/ADocH/{ace}/{sal}/{group}/")]
        public async Task<IHttpActionResult> PostAllWeb_ADocH(string ace, string sal, string group, ADocHObject ADocHObject)
        {
            var dataAccount = UnitDatabase.ReadUserPassHeader(this.Request.Headers);
            string con = UnitDatabase.CreateConection(dataAccount[0], dataAccount[1], dataAccount[2], ace, sal, group, 0, "", 0, 0);
            if (con == "ok")
            {

                string sql = string.Format(CultureInfo.InvariantCulture,
                             @"declare @DocNo nvarchar(50) = '{0}'  select ",
                             ADocHObject.DocNo);

                if (ADocHObject.Select == 0)
                    sql += " top(100) ";
                sql += string.Format(@" * from Web_ADocH where (@DocNo = ''  or DocNo = @DocNo)  ");

                if (ADocHObject.AccessSanad == false)
                    sql += " and Eghdam = '" + ADocHObject.User + "' ";



                sql += " order by ";

                if (ADocHObject.Sort == "" || ADocHObject.Sort == null)
                {
                    ADocHObject.Sort = "DocDate Desc,SortDocNo Desc";
                }
                else if (ADocHObject.Sort == "DocDate")
                {
                    if (ADocHObject.ModeSort == "ASC")
                        ADocHObject.Sort = "DocDate Asc,SortDocNo Asc";
                    else
                        ADocHObject.Sort = "DocDate Desc,SortDocNo Desc";
                }
                else if (ADocHObject.Sort == "Status")
                {
                    if (ADocHObject.ModeSort == "ASC")
                        ADocHObject.Sort = "Status Asc, DocDate Asc,SortDocNo Asc";
                    else
                        ADocHObject.Sort = "Status Desc, DocDate Desc,SortDocNo Desc";
                }
                else
                {
                    ADocHObject.Sort = ADocHObject.Sort + " " + ADocHObject.ModeSort;
                }

                sql += ADocHObject.Sort;


                var listADocH = UnitDatabase.db.Database.SqlQuery<Web_ADocH>(sql);
                return Ok(listADocH);
            }
            return Ok(con);
        }

        // GET: api/ADocData/ADocH اطلاعات تکمیلی فاکتور    
        [Route("api/ADocData/ADocH/{ace}/{sal}/{group}/{serialNumber}")]
        public async Task<IQueryable<Web_ADocH>> GetWeb_ADocH(string ace, string sal, string group, long serialNumber)
        {
            var dataAccount = UnitDatabase.ReadUserPassHeader(this.Request.Headers);

            string con = UnitDatabase.CreateConection(dataAccount[0], dataAccount[1], dataAccount[2], ace, sal, group, serialNumber, "", 0, 0);
            if (con == "ok")
            {
                var a = UnitDatabase.db.Web_ADocH.Where(c => c.SerialNumber == serialNumber);
                return a;
            }
            return null;
        }


        // GET: api/ADocData/ADocB اطلاعات تکمیلی سند    
        [Route("api/ADocData/ADocB/{ace}/{sal}/{group}/{serialNumber}")]
        public async Task<IHttpActionResult> GetWeb_ADocB(string ace, string sal, string group, long serialNumber)
        {
            var dataAccount = UnitDatabase.ReadUserPassHeader(this.Request.Headers);

            string con = UnitDatabase.CreateConection(dataAccount[0], dataAccount[1], dataAccount[2], ace, sal, group, serialNumber, "", 0, 0);
            if (con == "ok")
            {
                string sql = string.Format(@"SELECT *  FROM Web_ADocB WHERE SerialNumber = {0}", serialNumber);
                var listSanad = UnitDatabase.db.Database.SqlQuery<Web_ADocB>(sql);
                return Ok(listSanad);
            }
            return Ok(con);
        }


        // GET: api/ADocData/ADocH اخرین تاریخ    
        [Route("api/ADocData/ADocH/LastDate/{ace}/{sal}/{group}")]
        public async Task<IHttpActionResult> GetWeb_ADocHLastDate(string ace, string sal, string group)
        {
            var dataAccount = UnitDatabase.ReadUserPassHeader(this.Request.Headers);

            string con = UnitDatabase.CreateConection(dataAccount[0], dataAccount[1], dataAccount[2], ace, sal, group, 0, "", 0, 0);
            if (con == "ok")
            {
                string sql = string.Format(@"EXEC[dbo].[Web_Doc_Dates]
                                             @TableName = '{0}',
		                                     @Mode = ''", "Acc5doch");
                string lastdate = UnitDatabase.db.Database.SqlQuery<string>(sql).Single();
                return Ok(lastdate);
            }
            return Ok(con);
        }



        // GET: api/ADocData/CheckList اطلاعات چک    
        [Route("api/ADocData/CheckList/{ace}/{sal}/{group}/{PDMode}")]

        public IQueryable<Web_CheckList> GetWeb_CheckList(string ace, string sal, string group, int PDMode)
        {
            var dataAccount = UnitDatabase.ReadUserPassHeader(this.Request.Headers);

            string con = UnitDatabase.CreateConection(dataAccount[0], dataAccount[1], dataAccount[2], ace, sal, group, 0, "", 0, 0);
            if (con == "ok")
            {
                return UnitDatabase.db.Web_CheckList.Where(c => c.PDMode == PDMode && c.CheckStatus != 2 && c.CheckStatus != 4);
            }
            return null;
        }


        public class Web_ValueBank
        {
            public string Name { get; set; }

        }

        // GET: api/ADocData/Bank اطلاعات بانک    
        [Route("api/ADocData/Bank/{ace}/{sal}/{group}")]
        public async Task<IHttpActionResult> GetWeb_Bank(string ace, string sal, string group)
        {
            var dataAccount = UnitDatabase.ReadUserPassHeader(this.Request.Headers);

            string con = UnitDatabase.CreateConection(dataAccount[0], dataAccount[1], dataAccount[2], ace, sal, group, 0, "", 0, 0);
            if (con == "ok")
            {
                string sql = string.Format(@"SELECT distinct bank as name FROM Web_CheckList  where bank <> ''");
                var listBank = UnitDatabase.db.Database.SqlQuery<Web_ValueBank>(sql);
                return Ok(listBank);
            }
            return Ok(con);
        }

        // GET: api/ADocData/Shobe اطلاعات شعبه    
        [Route("api/ADocData/Shobe/{ace}/{sal}/{group}")]
        public async Task<IHttpActionResult> GetWeb_Shobe(string ace, string sal, string group)
        {
            var dataAccount = UnitDatabase.ReadUserPassHeader(this.Request.Headers);

            string con = UnitDatabase.CreateConection(dataAccount[0], dataAccount[1], dataAccount[2], ace, sal, group, 0, "", 0, 0);
            if (con == "ok")
            {
                string sql = string.Format(@"SELECT distinct Shobe as name FROM Web_CheckList  where Shobe <> ''");
                var listShobe = UnitDatabase.db.Database.SqlQuery<Web_ValueBank>(sql);
                return Ok(listShobe);
            }
            return Ok(con);
        }

        // GET: api/ADocData/Jari اطلاعات جاری    
        [Route("api/ADocData/Jari/{ace}/{sal}/{group}")]
        public async Task<IHttpActionResult> GetWeb_Jari(string ace, string sal, string group)
        {
            var dataAccount = UnitDatabase.ReadUserPassHeader(this.Request.Headers);

            string con = UnitDatabase.CreateConection(dataAccount[0], dataAccount[1], dataAccount[2], ace, sal, group, 0, "", 0, 0);
            if (con == "ok")
            {
                string sql = string.Format(@"SELECT distinct Jari as name FROM Web_CheckList where Jari <> ''");
                var listJari = UnitDatabase.db.Database.SqlQuery<Web_ValueBank>(sql);
                return Ok(listJari);
            }
            return Ok(con);
        }



        public class AFI_Move
        {
            public byte? DocNoMode { get; set; }

            public byte? InsertMode { get; set; }

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



        [Route("api/ADocData/MoveSanad/{ace}/{sal}/{group}")]
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PostWeb_MoveSanad(string ace, string sal, string group, AFI_Move AFI_Move)
        {
            long value = 0;
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var dataAccount = UnitDatabase.ReadUserPassHeader(this.Request.Headers);
            string con = UnitDatabase.CreateConection(dataAccount[0], dataAccount[1], dataAccount[2], ace, sal, group, AFI_Move.SerialNumber ?? 0, "", 0, 0);
            if (con == "ok")
            {
                try
                {
                    string sql = string.Format(CultureInfo.InvariantCulture,
                          @"DECLARE	@return_value int,
		                            @oSerialNumber bigint

                            EXEC	@return_value = [dbo].[Web_SaveADoc_Move]
		                            @DocNoMode = {0},
		                            @InsertMode = {1},
		                            @DocNo = {2},
		                            @StartNo = {3},
		                            @EndNo = {4},
		                            @BranchCode = {5},
		                            @UserCode = '''{6}''',
		                            @TahieShode = '{7}',
		                            @SerialNumber = {8},
		                            @DocDate = '{9}',
                                    @MoveMode = {10} ,
		                            @oSerialNumber = @oSerialNumber OUTPUT
                            SELECT	@oSerialNumber as N'@oSerialNumber'",
                          AFI_Move.DocNoMode,
                          AFI_Move.InsertMode,
                          AFI_Move.DocNo,
                          AFI_Move.StartNo,
                          AFI_Move.EndNo,
                          AFI_Move.BranchCode,
                          AFI_Move.UserCode,
                          AFI_Move.TahieShode,
                          AFI_Move.SerialNumber,
                          AFI_Move.DocDate,
                          AFI_Move.MoveMode);

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

                var list = UnitDatabase.db.Web_ADocH.Where(c => c.SerialNumber == value);
                UnitDatabase.SaveLog(dataAccount[0], dataAccount[1], dataAccount[2], ace, sal, group, value, "ADoc", 2, "Y", 0);
                return Ok(list);
            }
            else
                return Ok(con);
        }



        public class AFI_StatusChange
        {
            public byte DMode { get; set; }

            public string UserCode { get; set; }

            public long SerialNumber { get; set; }

            public string Status { get; set; }
        }

        [Route("api/ADocData/ChangeStatus/{ace}/{sal}/{group}")]
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PostWeb_ChangeStatus(string ace, string sal, string group, AFI_StatusChange AFI_StatusChange)
        {
            int value = 0;
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var dataAccount = UnitDatabase.ReadUserPassHeader(this.Request.Headers);
            string con = UnitDatabase.CreateConection(dataAccount[0], dataAccount[1], dataAccount[2], ace, sal, group, AFI_StatusChange.SerialNumber, "Adoc", 0, 0);
            if (con == "ok")
            {
                try
                {
                    string sql = string.Format(CultureInfo.InvariantCulture,
                          @"DECLARE	@return_value int
                            EXEC	@return_value = [dbo].[Web_SaveADoc_Status]
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

                UnitDatabase.SaveLog(dataAccount[0], dataAccount[1], dataAccount[2], ace, sal, group, AFI_StatusChange.SerialNumber, "ADoc", 7, "Y", 0);
                return Ok(200);
            }
            else
            {
                return Ok(con);
            }
        }





        // GET: api/ADocData/ADocP لیست سند    
        [Route("api/ADocData/ADocP/{ace}/{sal}/{group}/{SerialNumber}")]
        public async Task<IHttpActionResult> GetAllWeb_ADocP(string ace, string sal, string group, long SerialNumber)
        {
            var dataAccount = UnitDatabase.ReadUserPassHeader(this.Request.Headers);
            string con = UnitDatabase.CreateConection(dataAccount[0], dataAccount[1], dataAccount[2], ace, sal, group, SerialNumber, "", 0, 0);
            if (con == "ok")
            {
                string sql = string.Format(@"select * from Web_ADocP where SerialNumber = {0} order by BandNo", SerialNumber);
                var listADocP = UnitDatabase.db.Database.SqlQuery<Web_ADocP>(sql);
                return Ok(listADocP);
            }
            return Ok(con);
        }




        public class AFI_TestADocB
        {
            public long SerialNumber { get; set; }

        }


        [Route("api/ADocData/TestADoc/{ace}/{sal}/{group}")]
        [ResponseType(typeof(TestDocB))]
        public async Task<IHttpActionResult> PostWeb_TestADoc(string ace, string sal, string group, AFI_TestADocB AFI_TestADocB)
        {
            var dataAccount = UnitDatabase.ReadUserPassHeader(this.Request.Headers);
            string con = UnitDatabase.CreateConection(dataAccount[0], dataAccount[1], dataAccount[2], ace, sal, group, 0, "", 0, 0);
            if (con == "ok")
            {
                string sql = string.Format(CultureInfo.InvariantCulture,
                      @"EXEC	[dbo].[Web_TestADoc] @serialNumber = {0} , @UserCode = '{1}' ",
                      AFI_TestADocB.SerialNumber,
                      dataAccount[2]);
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
            return Ok(con);

        }




        public class AFI_SaveADoc_HZ
        {
            public long SerialNumber { get; set; }

            public string Tanzim { get; set; }

        }


        [Route("api/ADocData/SaveADoc_HZ/{ace}/{sal}/{group}")]
        [ResponseType(typeof(TestDocB))]
        public async Task<IHttpActionResult> PostWeb_SaveADoc_HZ(string ace, string sal, string group, AFI_SaveADoc_HZ AFI_SaveADoc_HZ)
        {
            var dataAccount = UnitDatabase.ReadUserPassHeader(this.Request.Headers);
            string con = UnitDatabase.CreateConection(dataAccount[0], dataAccount[1], dataAccount[2], ace, sal, group, 0, "", 0, 0);
            if (con == "ok")
            {
                string sql = string.Format(CultureInfo.InvariantCulture,
                                           @"DECLARE	@return_value int
                                             EXEC	@return_value = [dbo].[Web_SaveADoc_HZ]
		                                            @SerialNumber = {0}, 
		                                            @Tanzim = '{1}' 
                                             SELECT	'Return Value' = @return_value ",
                                           AFI_SaveADoc_HZ.SerialNumber, AFI_SaveADoc_HZ.Tanzim);
                var result = UnitDatabase.db.Database.SqlQuery<int>(sql).ToList();
                return Ok("ok");
            }
            return Ok(con);
        }





        public class TestADoc_DeleteObject
        {
            public long SerialNumber { get; set; }

        }

        public class TestADoc_Delete
        {
            public int id { get; set; }

            public byte Test { get; set; }

            public string TestName { get; set; }

            public string TestCap { get; set; }

            public int BandNo { get; set; }

        }



        [Route("api/ADocData/TestADoc_Delete/{ace}/{sal}/{group}")]
        [ResponseType(typeof(TestADoc_Delete))]
        public async Task<IHttpActionResult> PostWeb_TestADoc_Delete(string ace, string sal, string group, TestADoc_DeleteObject TestADoc_DeleteObject)
        {
            var dataAccount = UnitDatabase.ReadUserPassHeader(this.Request.Headers);
            string con = UnitDatabase.CreateConection(dataAccount[0], dataAccount[1], dataAccount[2], ace, sal, group, 0, "", 0, 0);
            if (con == "ok")
            {
                string sql = string.Format(CultureInfo.InvariantCulture,
                                           @"EXEC	[dbo].[Web_TestADoc_Delete] @serialNumber = {0} , @UserCode = '{1}' ", TestADoc_DeleteObject.SerialNumber, dataAccount[2]);
                try
                {
                    var result = UnitDatabase.db.Database.SqlQuery<TestADoc_Delete>(sql).ToList();
                    var jsonResult = JsonConvert.SerializeObject(result);
                    return Ok(jsonResult);
                }
                catch (Exception e)
                {
                    throw;
                }

            }
            return Ok(con);

        }




        public class AFI_TestADoc_New
        {

            public string DocDate { get; set; }

            public string ModeCode { get; set; }

        }


        [Route("api/ADocData/TestADoc_New/{ace}/{sal}/{group}")]
        [ResponseType(typeof(TestDocB))]
        public async Task<IHttpActionResult> PostWeb_TestADoc_New(string ace, string sal, string group, AFI_TestADoc_New AFI_TestADoc_New)
        {
            var dataAccount = UnitDatabase.ReadUserPassHeader(this.Request.Headers);
            string con = UnitDatabase.CreateConection(dataAccount[0], dataAccount[1], dataAccount[2], ace, sal, group, 0, "", 0, 0);
            if (con == "ok")
            {
                string sql = string.Format(CultureInfo.InvariantCulture,
                      @"EXEC	[dbo].[Web_TestADoc_New] @UserCode = '{0}',  @DocDate = '{1}', @ModeCode = '{2}'",
                      dataAccount[2],
                       AFI_TestADoc_New.DocDate,
                       AFI_TestADoc_New.ModeCode
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
            return Ok(con);

        }


        public class AFI_TestADoc_Edit
        {
            public long Serialnumber { get; set; }

        }

        [Route("api/ADocData/TestADoc_Edit/{ace}/{sal}/{group}")]
        [ResponseType(typeof(TestDocB))]
        public async Task<IHttpActionResult> PostWeb_TestADoc_Edit(string ace, string sal, string group, AFI_TestADoc_Edit AFI_TestADoc_Edit)
        {
            var dataAccount = UnitDatabase.ReadUserPassHeader(this.Request.Headers);
            string con = UnitDatabase.CreateConection(dataAccount[0], dataAccount[1], dataAccount[2], ace, sal, group, 0, "", 0, 0);
            if (con == "ok")
            {
                string sql = string.Format(CultureInfo.InvariantCulture,
                      @"EXEC	[dbo].[Web_TestADoc_Edit] @UserCode = '{0}',  @Serialnumber = '{1}'",
                      dataAccount[2],
                       AFI_TestADoc_Edit.Serialnumber
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
            return Ok(con);

        }



    }
}
