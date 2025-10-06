using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Text;
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
            string dBName = UnitDatabase.DatabaseName(ace, sal, group);
            string sql = String.Format("SELECT * FROM {0}.dbo.Web_AMode", dBName);
            var dataAccount = UnitDatabase.ReadUserPassHeader(this.Request.Headers);

            var DBase = UnitDatabase.dataDB.Where(p => p.UserName.ToUpper() == dataAccount[0].ToUpper() && p.Password == dataAccount[1]).Single();
            string res = UnitDatabase.TestAcount(DBase, dataAccount[3], ace, group, UnitPublic.access_View);
            if (res == "")
            {
                return Ok(DBase.DB.Database.SqlQuery<Web_AMode>(sql));
            }
            else
                return Ok(res);

            //string conStr = UnitDatabase.CreateConnectionString(dataAccount[0], dataAccount[1], dataAccount[2],dataAccount[3], ace, sal, group, 0, UnitPublic.access_View, UnitPublic.act_View, 0);
        }

        // GET: api/ADocData/CheckStatus اطلاعات نوع سند حسابداری   
        [Route("api/ADocData/CheckStatus/{ace}/{sal}/{group}/{PDMode}/{Report}")]

        public async Task<IHttpActionResult> GetWeb_CheckStatus(string ace, string sal, string group, int PDMode, int Report)
        {
            string dBName = UnitDatabase.DatabaseName(ace, sal, group);
            var dataAccount = UnitDatabase.ReadUserPassHeader(this.Request.Headers);
            string sql = String.Format("SELECT * FROM {0}.dbo.Web_CheckStatus where PDMode = {1} and Report = {2}", dBName, PDMode, Report);

            var DBase = UnitDatabase.dataDB.Where(p => p.UserName.ToUpper() == dataAccount[0].ToUpper() && p.Password == dataAccount[1]).Single();
            string res = UnitDatabase.TestAcount(DBase, dataAccount[3], ace, group, UnitPublic.access_View);
            if (res == "")
            {
                return Ok(DBase.DB.Database.SqlQuery<Web_CheckStatus>(sql));
            }
            else
                return Ok(res);

            // string conStr = UnitDatabase.CreateConnectionString(dataAccount[0], dataAccount[1], dataAccount[2],dataAccount[3], ace, sal, group, 0, UnitPublic.access_View, UnitPublic.act_View, 0);
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
            string dBName = UnitDatabase.DatabaseName(ace, sal, group);
            string sql = string.Format(CultureInfo.InvariantCulture,
             @"declare @DocNo nvarchar(50) = '{0}'  select ",
             ADocHObject.DocNo);

            if (ADocHObject.Select == 0)
                sql += " top(100) ";
            sql += string.Format(@" * from {0}.dbo.Web_ADocH where (@DocNo = ''  or DocNo = @DocNo)  ", dBName);

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


            var dataAccount = UnitDatabase.ReadUserPassHeader(this.Request.Headers);

            var DBase = UnitDatabase.dataDB.Where(p => p.UserName.ToUpper() == dataAccount[0].ToUpper() && p.Password == dataAccount[1]).Single();
            string res = UnitDatabase.TestAcount(DBase, dataAccount[3], ace, group, UnitPublic.access_View);
            if (res == "")
            {
                return Ok(DBase.DB.Database.SqlQuery<Web_ADocH>(sql));
            }
            else
                return Ok(res);


            //string conStr = UnitDatabase.CreateConnectionString(dataAccount[0], dataAccount[1], dataAccount[2],dataAccount[3], ace, sal, group, 0, UnitPublic.access_View, UnitPublic.act_View, 0);
        }

        // GET: api/ADocData/ADocH اطلاعات تکمیلی فاکتور    
        [Route("api/ADocData/ADocH/{ace}/{sal}/{group}/{serialNumber}")]
        public async Task<IHttpActionResult> GetWeb_ADocH(string ace, string sal, string group, long serialNumber)
        {
            string dBName = UnitDatabase.DatabaseName(ace, sal, group);
            var dataAccount = UnitDatabase.ReadUserPassHeader(this.Request.Headers);
            string sql = String.Format("SELECT * FROM {0}.dbo.Web_ADocH where SerialNumber = {1} ", dBName, serialNumber);

            var DBase = UnitDatabase.dataDB.Where(p => p.UserName.ToUpper() == dataAccount[0].ToUpper() && p.Password == dataAccount[1]).Single();
            string res = UnitDatabase.TestAcount(DBase, dataAccount[3], ace, group, UnitPublic.access_View);
            if (res == "")
            {
                return Ok(DBase.DB.Database.SqlQuery<Web_ADocH>(sql));
            }
            else
                return Ok(res);

            //string conStr = UnitDatabase.CreateConnectionString(dataAccount[0], dataAccount[1], dataAccount[2],dataAccount[3], ace, sal, group, serialNumber, UnitPublic.access_View, UnitPublic.act_View, 0);
        }


        // GET: api/ADocData/ADocB اطلاعات تکمیلی سند    
        [Route("api/ADocData/ADocB/{ace}/{sal}/{group}/{serialNumber}")]
        public async Task<IHttpActionResult> GetWeb_ADocB(string ace, string sal, string group, long serialNumber)
        {
            string dBName = UnitDatabase.DatabaseName(ace, sal, group);
            string sql = string.Format(@"SELECT * FROM {0}.dbo.Web_ADocB WHERE SerialNumber = {1} order by bandno", dBName, serialNumber);

            var dataAccount = UnitDatabase.ReadUserPassHeader(this.Request.Headers);

            var DBase = UnitDatabase.dataDB.Where(p => p.UserName.ToUpper() == dataAccount[0].ToUpper() && p.Password == dataAccount[1]).Single();
            string res = UnitDatabase.TestAcount(DBase, dataAccount[3], ace, group, UnitPublic.access_View);
            if (res == "")
            {
                var list = DBase.DB.Database.SqlQuery<Web_ADocB>(sql);
                return Ok(list);
            }
            else
                return Ok(res);

            //string conStr = UnitDatabase.CreateConnectionString(dataAccount[0], dataAccount[1], dataAccount[2],dataAccount[3], ace, sal, group, serialNumber, UnitPublic.access_View, UnitPublic.act_View, 0);
        }


        // GET: api/ADocData/ADocH اخرین تاریخ    
        [Route("api/ADocData/ADocH/LastDate/{ace}/{sal}/{group}")]
        public async Task<IHttpActionResult> GetWeb_ADocHLastDate(string ace, string sal, string group)
        {
            string dBName = UnitDatabase.DatabaseName(ace, sal, group);
            string sql = string.Format(@"EXEC {0}.[dbo].[Web_Doc_Dates]
                                             @TableName = '{1}',
		                                     @Mode = ''", dBName, "Acc5doch");

            var dataAccount = UnitDatabase.ReadUserPassHeader(this.Request.Headers);

            var DBase = UnitDatabase.dataDB.Where(p => p.UserName.ToUpper() == dataAccount[0].ToUpper() && p.Password == dataAccount[1]).Single();
            string res = UnitDatabase.TestAcount(DBase, dataAccount[3], ace, group, UnitPublic.access_View);
            if (res == "")
            {
                return Ok(DBase.DB.Database.SqlQuery<string>(sql));
            }
            else
                return Ok(res);

            //string conStr = UnitDatabase.CreateConnectionString(dataAccount[0], dataAccount[1], dataAccount[2], dataAccount[3], ace, sal, group, 0, UnitPublic.access_View, UnitPublic.act_View, 0);
        }



        // GET: api/ADocData/CheckList اطلاعات چک    
        [Route("api/ADocData/CheckList/{ace}/{sal}/{group}/{PDMode}")]

        public async Task<IHttpActionResult> GetWeb_CheckList(string ace, string sal, string group, int PDMode)
        {
            string dBName = UnitDatabase.DatabaseName(ace, sal, group);
            var dataAccount = UnitDatabase.ReadUserPassHeader(this.Request.Headers);
            string sql = String.Format("SELECT * FROM {0}.dbo.Web_CheckList where PDMode = {1} and CheckStatus <> 2 and  CheckStatus <> 4 ", dBName, PDMode);

            var DBase = UnitDatabase.dataDB.Where(p => p.UserName.ToUpper() == dataAccount[0].ToUpper() && p.Password == dataAccount[1]).Single();
            string res = UnitDatabase.TestAcount(DBase, dataAccount[3], ace, group, UnitPublic.access_View);
            if (res == "")
            {
                return Ok(DBase.DB.Database.SqlQuery<Web_CheckList>(sql));
            }
            else
                return Ok(res);
            //string conStr = UnitDatabase.CreateConnectionString(dataAccount[0], dataAccount[1], dataAccount[2],dataAccount[3], ace, sal, group, 0, UnitPublic.access_View, UnitPublic.act_View, 0);
        }


        public class Web_ValueBank
        {
            public string Name { get; set; }

        }

        // GET: api/ADocData/Bank اطلاعات بانک    
        [Route("api/ADocData/Bank/{ace}/{sal}/{group}")]
        public async Task<IHttpActionResult> GetWeb_Bank(string ace, string sal, string group)
        {
            string dBName = UnitDatabase.DatabaseName(ace, sal, group);
            string sql = string.Format(@"SELECT distinct bank as name FROM {0}.[dbo].Web_CheckList  where bank <> ''", dBName);

            var dataAccount = UnitDatabase.ReadUserPassHeader(this.Request.Headers);

            var DBase = UnitDatabase.dataDB.Where(p => p.UserName.ToUpper() == dataAccount[0].ToUpper() && p.Password == dataAccount[1]).Single();
            string res = UnitDatabase.TestAcount(DBase, dataAccount[3], ace, group, UnitPublic.access_View);
            if (res == "")
            {
                return Ok(DBase.DB.Database.SqlQuery<Web_ValueBank>(sql));
            }
            else
                return Ok(res);

            //string conStr = UnitDatabase.CreateConnectionString(dataAccount[0], dataAccount[1], dataAccount[2], dataAccount[3], ace, sal, group, 0, UnitPublic.access_View, UnitPublic.act_View, 0);

        }

        // GET: api/ADocData/Shobe اطلاعات شعبه    
        [Route("api/ADocData/Shobe/{ace}/{sal}/{group}")]
        public async Task<IHttpActionResult> GetWeb_Shobe(string ace, string sal, string group)
        {
            string dBName = UnitDatabase.DatabaseName(ace, sal, group);
            string sql = string.Format(@"SELECT distinct Shobe as name FROM {0}.dbo.Web_CheckList  where Shobe <> ''", dBName);

            var dataAccount = UnitDatabase.ReadUserPassHeader(this.Request.Headers);

            var DBase = UnitDatabase.dataDB.Where(p => p.UserName.ToUpper() == dataAccount[0].ToUpper() && p.Password == dataAccount[1]).Single();
            string res = UnitDatabase.TestAcount(DBase, dataAccount[3], ace, group, UnitPublic.access_View);
            if (res == "")
            {
                return Ok(DBase.DB.Database.SqlQuery<Web_ValueBank>(sql));
            }
            else
                return Ok(res);

            //string conStr = UnitDatabase.CreateConnectionString(dataAccount[0], dataAccount[1], dataAccount[2], dataAccount[3], ace, sal, group, 0, UnitPublic.access_View, UnitPublic.act_View, 0);
        }

        // GET: api/ADocData/Jari اطلاعات جاری    
        [Route("api/ADocData/Jari/{ace}/{sal}/{group}")]
        public async Task<IHttpActionResult> GetWeb_Jari(string ace, string sal, string group)
        {
            string dBName = UnitDatabase.DatabaseName(ace, sal, group);
            string sql = string.Format(@"SELECT distinct Jari as name FROM {0}.dbo.Web_CheckList where Jari <> ''", dBName);

            var dataAccount = UnitDatabase.ReadUserPassHeader(this.Request.Headers);

            var DBase = UnitDatabase.dataDB.Where(p => p.UserName.ToUpper() == dataAccount[0].ToUpper() && p.Password == dataAccount[1]).Single();
            string res = UnitDatabase.TestAcount(DBase, dataAccount[3], ace, group, UnitPublic.access_View);
            if (res == "")
            {
                return Ok(DBase.DB.Database.SqlQuery<Web_ValueBank>(sql));
            }
            else
                return Ok(res);


            //string conStr = UnitDatabase.CreateConnectionString(dataAccount[0], dataAccount[1], dataAccount[2], dataAccount[3], ace, sal, group, 0, UnitPublic.access_View, UnitPublic.act_View, 0);
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
            string dBName = UnitDatabase.DatabaseName(ace, sal, group);
            long value = 0;
            string sql = string.Format(CultureInfo.InvariantCulture,
                          @"DECLARE	@return_value int,
		                            @oSerialNumber bigint

                            EXEC	@return_value = {11}.[dbo].[Web_SaveADoc_Move]
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
                          AFI_Move.MoveMode,
                          dBName);

            var dataAccount = UnitDatabase.ReadUserPassHeader(this.Request.Headers);

            var DBase = UnitDatabase.dataDB.Where(p => p.UserName.ToUpper() == dataAccount[0].ToUpper() && p.Password == dataAccount[1]).Single();
            string res = UnitDatabase.TestAcount(DBase, dataAccount[3], ace, group, UnitPublic.access_ADOC);
            if (res == "")
            {
                value = DBase.DB.Database.SqlQuery<long>(sql).Single();
                await DBase.DB.SaveChangesAsync();

                sql = String.Format("SELECT * FROM {0}.dbo.Web_ADocH where SerialNumber = {1}", dBName, value);
                var list = DBase.DB.Database.SqlQuery<Web_ADocH>(sql).ToList();
                UnitDatabase.SaveLog(dataAccount[0], dataAccount[1], dataAccount[2], ace, sal, group, AFI_Move.SerialNumber ?? 0, UnitPublic.access_ADOC, UnitPublic.act_New, "Y", 1, 0);
                return Ok(list);
            }
            else
                return Ok(res);
            // UnitDatabase.SaveLog(dataAccount[0], dataAccount[1], dataAccount[2], ace, sal, group, value, UnitPublic.access_ADOC, 2, "Y", 1, 0);
            //string conStr = UnitDatabase.CreateConnectionString(dataAccount[0], dataAccount[1], dataAccount[2], dataAccount[3], ace, sal, group, AFI_Move.SerialNumber ?? 0, UnitPublic.access_ADOC, UnitPublic.act_New, 0);
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
            string dBName = UnitDatabase.DatabaseName(ace, sal, group);
            int value = 0;
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var dataAccount = UnitDatabase.ReadUserPassHeader(this.Request.Headers);

            var DBase = UnitDatabase.dataDB.Where(p => p.UserName.ToUpper() == dataAccount[0].ToUpper() && p.Password == dataAccount[1]).Single();
            string res = UnitDatabase.TestAcount(DBase, dataAccount[3], ace, group, UnitPublic.access_ADOC);
            if (res == "")
            {
                try
                {
                    string sql = string.Format(CultureInfo.InvariantCulture,
                         @"DECLARE	@return_value int
                            EXEC	@return_value = {0}.[dbo].[Web_SaveADoc_Status]
		                            @DMode = {1},
		                            @UserCode = N'{2}',
		                            @SerialNumber = {3},
		                            @Status = N'{4}'
                            SELECT	'Return Value' = @return_value",
                         dBName,
                         AFI_StatusChange.DMode,
                         AFI_StatusChange.UserCode,
                         AFI_StatusChange.SerialNumber,
                         AFI_StatusChange.Status);

                    value = DBase.DB.Database.SqlQuery<int>(sql).Single();
                    if (value == 0)
                    {
                        await DBase.DB.SaveChangesAsync();
                    }
                    UnitDatabase.SaveLog(dataAccount[0], dataAccount[1], dataAccount[2], ace, sal, group, AFI_StatusChange.SerialNumber, UnitPublic.access_ADOC, UnitPublic.act_Edit, "Y", 1, 0);
                    return Ok("200");
                }
                catch (Exception)
                {
                    throw;
                }

            }
            else
                return Ok(res);
            //string conStr = UnitDatabase.CreateConnectionString(dataAccount[0], dataAccount[1], dataAccount[2], dataAccount[3], ace, sal, group, AFI_StatusChange.SerialNumber, UnitPublic.access_ADOC, UnitPublic.act_Edit, 0);
        }





        // GET: api/ADocData/ADocP لیست سند    
        [Route("api/ADocData/ADocP/{ace}/{sal}/{group}/{SerialNumber}")]
        public async Task<IHttpActionResult> GetAllWeb_ADocP(string ace, string sal, string group, long SerialNumber)
        {
            string dBName = UnitDatabase.DatabaseName(ace, sal, group);
            string sql = string.Format(@"select * from {0}.dbo.Web_ADocP where SerialNumber = {1} order by BandNo", dBName, SerialNumber);
            var dataAccount = UnitDatabase.ReadUserPassHeader(this.Request.Headers);

            var DBase = UnitDatabase.dataDB.Where(p => p.UserName.ToUpper() == dataAccount[0].ToUpper() && p.Password == dataAccount[1]).Single();
            string res = UnitDatabase.TestAcount(DBase, dataAccount[3], ace, group, UnitPublic.access_ADOC);
            if (res == "")
            {
                UnitDatabase.SaveLog(dataAccount[0], dataAccount[1], dataAccount[2], ace, sal, group, 0, UnitPublic.access_ADOC, UnitPublic.act_Print, "Y", 1, 0);
                return Ok(DBase.DB.Database.SqlQuery<Web_ADocP>(sql));
            }
            else
                return Ok(res);

            //string conStr = UnitDatabase.CreateConnectionString(dataAccount[0], dataAccount[1], dataAccount[2], dataAccount[3], ace, sal, group, SerialNumber, UnitPublic.access_ADOC, UnitPublic.act_Print, 0);
        }




        public class AFI_TestADocB
        {
            public long SerialNumber { get; set; }

            public string flagTest { get; set; }

        }


        [Route("api/ADocData/TestADoc/{ace}/{sal}/{group}")]
        [ResponseType(typeof(TestDocB))]
        public async Task<IHttpActionResult> PostWeb_TestADoc(string ace, string sal, string group, AFI_TestADocB AFI_TestADocB)
        {
            string dBName = UnitDatabase.DatabaseName(ace, sal, group);
            var dataAccount = UnitDatabase.ReadUserPassHeader(this.Request.Headers);
            string sql = string.Format(CultureInfo.InvariantCulture,
                     @"EXEC	{0}.[dbo].[{3}] @serialNumber = {1} , @UserCode = '{2}' ",
                     dBName,
                     AFI_TestADocB.SerialNumber,
                     dataAccount[2],
                     AFI_TestADocB.flagTest == "Y" ? "Web_TestADoc_Temp" : "Web_TestADoc"
                     );


            var DBase = UnitDatabase.dataDB.Where(p => p.UserName.ToUpper() == dataAccount[0].ToUpper() && p.Password == dataAccount[1]).Single();
            string res = UnitDatabase.TestAcount(DBase, dataAccount[3], ace, group, UnitPublic.access_ADOC);
            if (res == "")
            {
                var result = DBase.DB.Database.SqlQuery<TestDocB>(sql).ToList();
                var list = JsonConvert.SerializeObject(result);
                UnitDatabase.SaveLog(dataAccount[0], dataAccount[1], dataAccount[2], ace, sal, group, 0, UnitPublic.access_ADOC, UnitPublic.act_Report, "Y", 1, 0);
                return Ok(list);

            }
            else
                return Ok(res);


            //string conStr = UnitDatabase.CreateConnectionString(dataAccount[0], dataAccount[1], dataAccount[2], dataAccount[3], ace, sal, group, 0, UnitPublic.access_ADOC, UnitPublic.act_Report, 0);
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
            string dBName = UnitDatabase.DatabaseName(ace, sal, group);
            string sql = string.Format(CultureInfo.InvariantCulture,
                                       @"DECLARE	@return_value int
                                             EXEC	@return_value = {0}.[dbo].[Web_SaveADoc_HZ]
		                                            @SerialNumber = {1}, 
		                                            @Tanzim = '{2}' 
                                             SELECT	'Return Value' = @return_value ",
                                       dBName, AFI_SaveADoc_HZ.SerialNumber, AFI_SaveADoc_HZ.Tanzim);

            var dataAccount = UnitDatabase.ReadUserPassHeader(this.Request.Headers);

            var DBase = UnitDatabase.dataDB.Where(p => p.UserName.ToUpper() == dataAccount[0].ToUpper() && p.Password == dataAccount[1]).Single();
            string res = UnitDatabase.TestAcount(DBase, dataAccount[3], ace, group, UnitPublic.access_ADOC);
            if (res == "")
            {
                UnitDatabase.SaveLog(dataAccount[0], dataAccount[1], dataAccount[2], ace, sal, group, 0, UnitPublic.access_ADOC, UnitPublic.act_Edit, "Y", 1, 0);
                return Ok(DBase.DB.Database.SqlQuery<int>(sql));
            }
            else
                return Ok(res);

            //string conStr = UnitDatabase.CreateConnectionString(dataAccount[0], dataAccount[1], dataAccount[2], dataAccount[3], ace, sal, group, 0, UnitPublic.access_ADOC, UnitPublic.act_Edit, 0);
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
            string dBName = UnitDatabase.DatabaseName(ace, sal, group);
            var dataAccount = UnitDatabase.ReadUserPassHeader(this.Request.Headers);
            string sql = string.Format(CultureInfo.InvariantCulture,
                                       @"EXEC	{0}.[dbo].[Web_TestADoc_Delete] @serialNumber = {1} , @UserCode = '{2}' ", dBName, TestADoc_DeleteObject.SerialNumber, dataAccount[2]);


            var DBase = UnitDatabase.dataDB.Where(p => p.UserName.ToUpper() == dataAccount[0].ToUpper() && p.Password == dataAccount[1]).Single();
            string res = UnitDatabase.TestAcount(DBase, dataAccount[3], ace, group, UnitPublic.access_ADOC);
            if (res == "")
            {
                var result = DBase.DB.Database.SqlQuery<TestADoc_Delete>(sql).ToList();
                var list = JsonConvert.SerializeObject(result);
                UnitDatabase.SaveLog(dataAccount[0], dataAccount[1], dataAccount[2], ace, sal, group, 0, UnitPublic.access_ADOC, UnitPublic.act_Report, "Y", 1, 0);
                return Ok(list);
            }
            else
                return Ok(res);


            //string conStr = UnitDatabase.CreateConnectionString(dataAccount[0], dataAccount[1], dataAccount[2], dataAccount[3], ace, sal, group, 0, UnitPublic.access_ADOC, UnitPublic.act_Report, 0);
        }




        public class AFI_TestADoc_New
        {

            public string DocDate { get; set; }

            public string ModeCode { get; set; }

            public string DocNo { get; set; }

            public long SerialNumber { get; set; }

        }


        [Route("api/ADocData/TestADoc_New/{ace}/{sal}/{group}")]
        [ResponseType(typeof(TestDocB))]
        public async Task<IHttpActionResult> PostWeb_TestADoc_New(string ace, string sal, string group, AFI_TestADoc_New AFI_TestADoc_New)
        {
            string dBName = UnitDatabase.DatabaseName(ace, sal, group);
            var dataAccount = UnitDatabase.ReadUserPassHeader(this.Request.Headers);
            string sql = string.Format(CultureInfo.InvariantCulture,
                     @"EXEC	{0}.[dbo].[Web_TestADoc_New] @UserCode = '{1}',  @DocDate = '{2}', @ModeCode = '{3}' , @DocNo = '{4}' , @SerialNumber = {5}",
                     dBName,
                     dataAccount[2],
                     AFI_TestADoc_New.DocDate,
                     AFI_TestADoc_New.ModeCode,
                     AFI_TestADoc_New.DocNo,
                     AFI_TestADoc_New.SerialNumber);

            var DBase = UnitDatabase.dataDB.Where(p => p.UserName.ToUpper() == dataAccount[0].ToUpper() && p.Password == dataAccount[1]).Single();
            string res = UnitDatabase.TestAcount(DBase, dataAccount[3], ace, group, UnitPublic.access_ADOC);
            if (res == "")
            {
                var result = DBase.DB.Database.SqlQuery<TestDocB>(sql).ToList();
                var list = JsonConvert.SerializeObject(result);
                UnitDatabase.SaveLog(dataAccount[0], dataAccount[1], dataAccount[2], ace, sal, group, 0, UnitPublic.access_ADOC, UnitPublic.act_Report, "Y", 1, 0);
                return Ok(list);
            }
            else
                return Ok(res);

            //string conStr = UnitDatabase.CreateConnectionString(dataAccount[0], dataAccount[1], dataAccount[2], dataAccount[3], ace, sal, group, 0, UnitPublic.access_ADOC, UnitPublic.act_Report, 0);
        }


        public class AFI_TestADoc_Edit
        {
            public long Serialnumber { get; set; }

        }

        [Route("api/ADocData/TestADoc_Edit/{ace}/{sal}/{group}")]
        [ResponseType(typeof(TestDocB))]
        public async Task<IHttpActionResult> PostWeb_TestADoc_Edit(string ace, string sal, string group, AFI_TestADoc_Edit AFI_TestADoc_Edit)
        {
            string dBName = UnitDatabase.DatabaseName(ace, sal, group);
            var dataAccount = UnitDatabase.ReadUserPassHeader(this.Request.Headers);
            string sql = string.Format(CultureInfo.InvariantCulture,
                                      @"EXEC	{0}.[dbo].[Web_TestADoc_Edit] @UserCode = '{1}',  @Serialnumber = '{2}'",
                                      dBName,
                                      dataAccount[2],
                                      AFI_TestADoc_Edit.Serialnumber);


            var DBase = UnitDatabase.dataDB.Where(p => p.UserName.ToUpper() == dataAccount[0].ToUpper() && p.Password == dataAccount[1]).Single();
            string res = UnitDatabase.TestAcount(DBase, dataAccount[3], ace, group, UnitPublic.access_ADOC);
            if (res == "")
            {
                var result = DBase.DB.Database.SqlQuery<TestDocB>(sql).ToList();
                var list = JsonConvert.SerializeObject(result);
                UnitDatabase.SaveLog(dataAccount[0], dataAccount[1], dataAccount[2], ace, sal, group, 0, UnitPublic.access_ADOC, UnitPublic.act_Report, "Y", 1, 0);
                return Ok(list);
            }
            else
                return Ok(res);

            //string conStr = UnitDatabase.CreateConnectionString(dataAccount[0], dataAccount[1], dataAccount[2], dataAccount[3], ace, sal, group, 0, UnitPublic.access_ADOC, UnitPublic.act_Report, 0);
        }


        /*  [DllImport("Acc6_Web.dll", CharSet = CharSet.Unicode)]
          public static extern bool GetVer(StringBuilder RetVal);

          [Route("api/ADocData/GetVerDllAcc6")]
          public async Task<IHttpActionResult> GetVerDllAcc6()
          {
              try
              {
                  StringBuilder RetVal = new StringBuilder(1024);
                  GetVer(RetVal);
                  return Ok(RetVal.ToString());
              }
              catch (Exception e)
              {
                  return Ok("Error : " + e.Message.ToString());
              }
          }*/




        public class SaveADocH_RelatedGroup
        {
            public long Serialnumber { get; set; }

            public string TahieShode { get; set; }
        }

        [Route("api/ADocData/SaveADocH_RelatedGroup/{ace}/{sal}/{group}")]
        public async Task<IHttpActionResult> PostWeb_ADOCH_RelatedGroup(string ace, string sal, string group, SaveADocH_RelatedGroup d)
        {
            string dBName = UnitDatabase.DatabaseName(ace, sal, group);
            var dataAccount = UnitDatabase.ReadUserPassHeader(this.Request.Headers);
            string sql = string.Format(CultureInfo.InvariantCulture,
                                      @"EXEC	{0}.[dbo].[Web_SaveADocH_RelatedGroup]
		                                        @SerialNumber = {1},
		                                        @UserCode = N'''{2}''',
		                                        @TahieShode = N'{3}'
                                       select 0",
                                      dBName,
                                      d.Serialnumber,
                                      dataAccount[2],
                                      d.TahieShode);

            var DBase = UnitDatabase.dataDB.Where(p => p.UserName.ToUpper() == dataAccount[0].ToUpper() && p.Password == dataAccount[1]).Single();
            string res = UnitDatabase.TestAcount(DBase, dataAccount[3], ace, group, UnitPublic.access_ADOC);
            if (res == "")
            {
                var result = DBase.DB.Database.SqlQuery<int>(sql).ToList();
                return Ok(result);
            }
            else
                return Ok(res);
        }





    }

}
