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
        public async Task<IHttpActionResult> GetWeb_IDocH(string ace, string sal, string group, long serialNumber)
        {
            string dBName = UnitDatabase.DatabaseName(ace, sal, group);
            var dataAccount = UnitDatabase.ReadUserPassHeader(this.Request.Headers);
            string sql = string.Format(@"select * from {0}.dbo.Web_IDocH where SerialNumber = {1}", dBName, serialNumber);
            var DBase = UnitDatabase.dataDB.Where(p => p.UserName == dataAccount[0] && p.Password == dataAccount[1]).Single();
            string res = UnitDatabase.TestAcount(DBase, dataAccount[3], ace, group, UnitPublic.access_View);
            if (res == "")
            {
                return Ok(DBase.DB.Database.SqlQuery<Web_IDocH>(sql));
            }
            else
                return Ok(res);

            //string conStr = UnitDatabase.CreateConnectionString(dataAccount[0], dataAccount[1], dataAccount[2], dataAccount[3], ace, sal, group, serialNumber, UnitPublic.access_View, UnitPublic.act_View, 0);
        }

        // GET: api/IDocData/IDocH تعداد رکورد ها    
        [Route("api /IDocData/IDocH/{ace}/{sal}/{group}/{InOut}/Count")]
        public async Task<IHttpActionResult> GetWeb_IDocHCount(string ace, string sal, string group, byte InOut)
        {
            string dBName = UnitDatabase.DatabaseName(ace, sal, group);
            var dataAccount = UnitDatabase.ReadUserPassHeader(this.Request.Headers);
            var DBase = UnitDatabase.dataDB.Where(p => p.UserName == dataAccount[0] && p.Password == dataAccount[1]).Single();
            string res = UnitDatabase.TestAcount(DBase, dataAccount[3], ace, group, UnitPublic.access_View);
            if (res == "")
            {
                string sql = string.Format(@"SELECT count(SerialNumber) FROM {0}.dbo.Web_IDocH WHERE InOut = {1} ", dBName, InOut);
                int count = DBase.DB.Database.SqlQuery<int>(sql).Single();
                return Ok(count);
            }
            else
                return Ok(res);


            //string conStr = UnitDatabase.CreateConnectionString(dataAccount[0], dataAccount[1], dataAccount[2], dataAccount[3], ace, sal, group, 0, UnitPublic.access_View, UnitPublic.act_View, 0);
        }
        // GET: api/IDocData/IDocH اخرین تاریخ    
        [Route("api/IDocData/IDocH/LastDate/{ace}/{sal}/{group}/{InOut}")]
        public async Task<IHttpActionResult> GetWeb_IDocHLastDate(string ace, string sal, string group, byte InOut)
        {
            string dBName = UnitDatabase.DatabaseName(ace, sal, group);
            var dataAccount = UnitDatabase.ReadUserPassHeader(this.Request.Headers);
            var DBase = UnitDatabase.dataDB.Where(p => p.UserName == dataAccount[0] && p.Password == dataAccount[1]).Single();
            string res = UnitDatabase.TestAcount(DBase, dataAccount[3], ace, group, UnitPublic.access_View);
            if (res == "")
            {
                string sql = string.Format(@"EXEC {0}.[dbo].[Web_Doc_Dates]
                                             @TableName = '{1}',
		                                     @Mode = '''{2}'''", dBName, "inv5doch", InOut);
                string lastdate = DBase.DB.Database.SqlQuery<string>(sql).Single();
                return Ok(lastdate);
            }
            else
                return Ok(res);

            //string conStr = UnitDatabase.CreateConnectionString(dataAccount[0], dataAccount[1], dataAccount[2], dataAccount[3], ace, sal, group, 0, UnitPublic.access_View, UnitPublic.act_View, 0);
        }



        public class IDocHMinObject
        {
            public byte InOut { get; set; }

            public int select { get; set; }

            public string invSelect { get; set; }

            public string user { get; set; }

            public bool accessSanad { get; set; }

            public string updatedate { get; set; }

            public string ModeCode { get; set; }

            public string Sort { get; set; }

            public string ModeSort { get; set; }

            public string DocNo { get; set; }

        }

        // Post: api/IDocData/IDocH لیست سند انبار   
        [Route("api/IDocData/IDocH/{ace}/{sal}/{group}")]
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PostAllWeb_IDocHMin(string ace, string sal, string group, IDocHMinObject IDocHMinObject)
        {
            string dBName = UnitDatabase.DatabaseName(ace, sal, group);

            var dataAccount = UnitDatabase.ReadUserPassHeader(this.Request.Headers);
            var DBase = UnitDatabase.dataDB.Where(p => p.UserName == dataAccount[0] && p.Password == dataAccount[1]).Single();
            string res = UnitDatabase.TestAcount(DBase, dataAccount[3], ace, group, UnitPublic.access_View);
            if (res == "")
            {
                string sql = string.Format(CultureInfo.InvariantCulture,
                            @"declare @enddate nvarchar(20)
                            declare @DocNo nvarchar(50) = '{0}' 
                            declare @ModeCode nvarchar(50) = '{1}'
                            select ",
                            IDocHMinObject.DocNo, IDocHMinObject.ModeCode);

                if (IDocHMinObject.select == 0)
                    sql += " top(100) ";

                sql += string.Format(@"SerialNumber,
                                       InOut,
                                       DocNo,
                                       SortDocNo,
                                       DocDate,
                                       ThvlCode,
                                       ThvlName,
                                       Spec,
                                       KalaPriceCode,
                                       InvCode,
                                       ModeCode,
                                       Status,
                                       PaymentType,
                                       Footer,
                                       Tanzim,
                                       Taeed,
                                       Tasvib,
                                       FinalPrice,
                                       Eghdam,
                                       ModeName,
                                       InvName,
                                       MkzCode,
                                       MkzName,
                                       OprCode,
                                       OprName,                                        
                                       UpdateDate,
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
                                       ThvlRegion,
                                       ThvlCity,
                                       ThvlStreet,
                                       ThvlAlley,
                                       ThvlPlack,
                                       ThvlZipCode,
                                       ThvlTel,
                                       ThvlMobile,
                                       ThvlFax,
                                       ThvlEMail,
                                       ThvlAddress,
                                       ThvlOstan,
                                       ThvlShahrestan,
                                       ThvlEcoCode,
                                       ThvlMelliCode,ArzCode,ArzName,ArzRate
                                       from {0}.dbo.Web_IDocH_F(3,'{1}') where 1 = 1 and (@DocNo = ''  or DocNo = @DocNo) and (@ModeCode = ''  or ModeCode = @ModeCode)   ", dBName, IDocHMinObject.user);


                //if (ModeCode == "in")
                //   sql += " (101,102,103,106,108,110) ";
                //else if (ModeCode == "out")
                //    sql += " (104,105,107,109,111)";

                if (IDocHMinObject.invSelect != "")
                {
                    sql += UnitPublic.SpiltCodeAnd("InvCode", IDocHMinObject.invSelect);
                    //sql += " and InvCode = '" + IDocHMinObject.invSelect + "' ";
                }

                if (IDocHMinObject.select == 1)
                    sql += " and DocDate =  @enddate ";
                else if (IDocHMinObject.select == 2)
                    sql += " and DocDate like  @enddate + '%' ";

                if (IDocHMinObject.accessSanad == false)
                    sql += " and Eghdam = '" + IDocHMinObject.user + "' ";

                if (IDocHMinObject.updatedate != null)
                    sql += " and updatedate >= CAST('" + IDocHMinObject.updatedate + "' AS DATETIME2)";

                if (IDocHMinObject.ModeCode != null)
                    sql += UnitPublic.SpiltCodeAnd("ModeCode", IDocHMinObject.ModeCode);
                else
                {
                    if (IDocHMinObject.InOut > 0)
                        sql += " and InOut = " + IDocHMinObject.InOut;
                }


                sql += " order by ";

                if (IDocHMinObject.Sort == "" || IDocHMinObject.Sort == null)
                {
                    IDocHMinObject.Sort = "DocDate Desc,SortDocNo Desc";
                }
                else if (IDocHMinObject.Sort == "DocDate")
                {
                    if (IDocHMinObject.ModeSort == "ASC")
                        IDocHMinObject.Sort = "DocDate Asc,SortDocNo Asc";
                    else
                        IDocHMinObject.Sort = "DocDate Desc,SortDocNo Desc";
                }
                else if (IDocHMinObject.Sort == "Status")
                {
                    if (IDocHMinObject.ModeSort == "ASC")
                        IDocHMinObject.Sort = "Status Asc, DocDate Asc,SortDocNo Asc";
                    else
                        IDocHMinObject.Sort = "Status Desc, DocDate Desc,SortDocNo Desc";
                }
                else
                {
                    IDocHMinObject.Sort = IDocHMinObject.Sort + " " + IDocHMinObject.ModeSort;
                }

                sql += IDocHMinObject.Sort;


                var listIDocH = DBase.DB.Database.SqlQuery<Web_IDocHMini>(sql);
                return Ok(listIDocH);
            }
            else
                return Ok(res);

            //string conStr = UnitDatabase.CreateConnectionString(dataAccount[0], dataAccount[1], dataAccount[2], dataAccount[3], ace, sal, group, 0, UnitPublic.access_View, UnitPublic.act_View, 0);
        }

        // GET: api/IDocData/IDocB اطلاعات تکمیلی سند انبار   
        [Route("api/IDocData/IDocB/{ace}/{sal}/{group}/{serialNumber}")]
        public async Task<IHttpActionResult> GetWeb_IDocB(string ace, string sal, string group, long serialNumber)
        {
            string dBName = UnitDatabase.DatabaseName(ace, sal, group);
            var dataAccount = UnitDatabase.ReadUserPassHeader(this.Request.Headers);
            var DBase = UnitDatabase.dataDB.Where(p => p.UserName == dataAccount[0] && p.Password == dataAccount[1]).Single();
            string res = UnitDatabase.TestAcount(DBase, dataAccount[3], ace, group, UnitPublic.access_View);
            if (res == "")
            {
                string sql = string.Format(@"SELECT SerialNumber,BandNo,KalaCode,KalaName,MainUnit,MainUnitName,Amount1,Amount2,Amount3,UnitPrice,TotalPrice,Comm,Up_Flag,KalaDeghatR1,KalaDeghatR2,KalaDeghatR3,KalaDeghatM1,KalaDeghatM2,KalaDeghatM3,
                                                    KalaFileNo,KalaState,KalaExf1,KalaExf2,KalaExf3,KalaExf4,KalaExf5,KalaExf6,KalaExf7,KalaExf8,KalaExf9,KalaExf10,KalaExf11,KalaExf12,KalaExf13,KalaExf14,KalaExf15,
                                                    DeghatR,BandSpec,ArzValue
                                             FROM   {0}.dbo.Web_IDocB WHERE SerialNumber = {1}", dBName, serialNumber);
                var listIDocB = DBase.DB.Database.SqlQuery<Web_IDocB>(sql);
                return Ok(listIDocB);
            }
            else
                return Ok(res);

            //string conStr = UnitDatabase.CreateConnectionString(dataAccount[0], dataAccount[1], dataAccount[2], dataAccount[3], ace, sal, group, serialNumber, UnitPublic.access_View, UnitPublic.act_View, 0);
        }

        [Route("api/IDocData/UpdatePriceAnbar/{ace}/{sal}/{group}/{serialnumber}")]
        [ResponseType(typeof(AFI_IDocBi))]
        public async Task<IHttpActionResult> PostWeb_UpdatePriceAnbar(string ace, string sal, string group, long serialnumber)
        {
            string dBName = UnitDatabase.DatabaseName(ace, sal, group);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var dataAccount = UnitDatabase.ReadUserPassHeader(this.Request.Headers);
            var DBase = UnitDatabase.dataDB.Where(p => p.UserName == dataAccount[0] && p.Password == dataAccount[1]).Single();
            string res = UnitDatabase.TestAcount(DBase, dataAccount[3], ace, group, UnitPublic.access_View);
            if (res == "")
            {
                try
                {
                    string sql = string.Format(CultureInfo.InvariantCulture,
                          @"DECLARE	@return_value int
                            EXEC    @return_value = {0}.[dbo].[Web_IDocB_SetKalaPrice]
		                            @SerialNumber = {1}
                            SELECT  'Return Value' = @return_value",
                          dBName,
                          serialnumber);
                    int value = DBase.DB.Database.SqlQuery<int>(sql).Single();
                    if (value == 0)
                    {
                        await DBase.DB.SaveChangesAsync();
                    }
                }
                catch (Exception)
                {
                    throw;
                }


                string sql1 = string.Format(@"SELECT SerialNumber,BandNo,KalaCode,KalaName,MainUnit,MainUnitName,Amount1,Amount2,Amount3,UnitPrice,TotalPrice,Comm,Up_Flag,KalaDeghatR1,KalaDeghatR2,KalaDeghatR3,KalaDeghatM1,KalaDeghatM2,KalaDeghatM3,
                                                     KalaFileNo,KalaState,KalaExf1,KalaExf2,KalaExf3,KalaExf4,KalaExf5,KalaExf6,KalaExf7,KalaExf8,KalaExf9,KalaExf10,KalaExf11,KalaExf12,KalaExf13,KalaExf14,KalaExf15,
                                                     DeghatR,BandSpec,ArzValue
                                              FROM   {0}.dbo.Web_IDocB WHERE SerialNumber = {1}", dBName, serialnumber);
                var listIDocB = DBase.DB.Database.SqlQuery<Web_IDocB>(sql1);
                return Ok(listIDocB);
            }
            else
                return Ok(res);

            //string conStr = UnitDatabase.CreateConnectionString(dataAccount[0], dataAccount[1], dataAccount[2], dataAccount[3], ace, sal, group, serialnumber, UnitPublic.access_View, UnitPublic.act_View, 0);
        }



        public class IModeObject
        {
            public byte Mode { get; set; }

            public int InOut { get; set; }

            public string UserCode { get; set; }
        }

        // Post: api/IDocData/IMode اطلاعات نوع سند انبار   
        [Route("api/IDocData/IMode/{ace}/{sal}/{group}")]
        public async Task<IHttpActionResult> PostWeb_IMode(string ace, string sal, string group, IModeObject iModeObject)
        {
            string dBName = UnitDatabase.DatabaseName(ace, sal, group);
            var dataAccount = UnitDatabase.ReadUserPassHeader(this.Request.Headers);

            var DBase = UnitDatabase.dataDB.Where(p => p.UserName == dataAccount[0] && p.Password == dataAccount[1]).Single();
            string res = UnitDatabase.TestAcount(DBase, dataAccount[3], ace, group, UnitPublic.access_View);
            if (res == "")
            {
                string sql;
                if (iModeObject.InOut == 0)
                    sql = string.Format(@"SELECT * FROM {0}.dbo.Web_IMode_F({1},'{2}') order by OrderFld ", dBName, iModeObject.Mode, iModeObject.UserCode);
                else
                    sql = string.Format(@"SELECT * FROM {0}.dbo.Web_IMode_F({1},'{2}') WHERE InOut = {3} order by OrderFld ", dBName, iModeObject.Mode, iModeObject.UserCode, iModeObject.InOut);
                var listIMode = DBase.DB.Database.SqlQuery<Web_IMode>(sql);

                /* var jsonResult = JsonConvert.SerializeObject(listIMode);
                 var aaa = jsonResult;
                 var properties = (from t in typeof(Web_IMode).GetProperties()
                                   select t.Name).ToList();

                 string fields = "";
                 string res;
                 for (int i = 0; i < properties.Count; i++)
                 {
                     fields = fields + properties[i] + ",";
                     res = jsonResult.Replace("\"" + properties[i] + "\"", "");
                     jsonResult = res;
                 }

                 var a = jsonResult.Replace(":\"", "!");
                 a = a.Replace("\"", "!");
                 a = a.Replace(",:", ",");
                 a = a.Replace("[{", "");
                 a = a.Replace("}]", "");
                 a = a.Replace("},{", "~");


                 return Ok(a);*/
                return Ok(listIMode);
            }
            else
                return Ok(res);

            //string conStr = UnitDatabase.CreateConnectionString(dataAccount[0], dataAccount[1], dataAccount[2], dataAccount[3], ace, sal, group, 0, UnitPublic.access_View, UnitPublic.act_View, 0);
        }


        public class AFI_Move
        {
            public byte? DocNoMode { get; set; }

            public byte? InsertMode { get; set; }

            public string ModeCode { get; set; }

            public string InvCode { get; set; }

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



        [Route("api/IDocData/MoveSanad/{ace}/{sal}/{group}")]
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PostWeb_MoveSanad(string ace, string sal, string group, AFI_Move AFI_Move)
        {
            string dBName = UnitDatabase.DatabaseName(ace, sal, group);
            long value = 0;
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var dataAccount = UnitDatabase.ReadUserPassHeader(this.Request.Headers);
            var DBase = UnitDatabase.dataDB.Where(p => p.UserName == dataAccount[0] && p.Password == dataAccount[1]).Single();
            string res = UnitDatabase.TestAcount(DBase, dataAccount[3], ace, group, UnitPublic.access_View);
            if (res == "")
            {
                try
                {
                    string sql = string.Format(CultureInfo.InvariantCulture,
                          @"DECLARE	@return_value int,
		                            @oSerialNumber bigint

                            EXEC	@return_value = {13}.[dbo].[Web_SaveIDoc_Move]
		                            @DocNoMode = {0},
		                            @InsertMode = {1},
		                            @ModeCode = N'{2}',
                                    @InvCode = N'{3}',
		                            @DocNo = {4},
		                            @StartNo = {5},
		                            @EndNo = {6},
		                            @BranchCode = {7},
		                            @UserCode = '''{8}''',
		                            @TahieShode = '{9}',
		                            @SerialNumber = {10},
		                            @DocDate = '{11}',
                                    @MoveMode = {12} ,
		                            @oSerialNumber = @oSerialNumber OUTPUT
                            SELECT	@oSerialNumber as N'@oSerialNumber'",
                          AFI_Move.DocNoMode,
                          AFI_Move.InsertMode,
                          AFI_Move.ModeCode,
                          AFI_Move.InvCode,
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

                    value = DBase.DB.Database.SqlQuery<long>(sql).Single();
                    if (value == 0)
                    {
                        await DBase.DB.SaveChangesAsync();
                    }
                }
                catch (Exception)
                {
                    throw;
                }

                var list = DBase.DB.Web_IDocH.Where(c => c.SerialNumber == value && c.ModeCode == AFI_Move.ModeCode && c.InvCode == AFI_Move.InvCode);
                var listTemp = list.Single();
                UnitDatabase.SaveLog(dataAccount[0], dataAccount[1], dataAccount[2], ace, sal, group, value, listTemp.InOut == 1 ? UnitPublic.access_IIDOC : UnitPublic.access_IODOC, 2, "Y", 1, 0);
                return Ok(list);
            }
            else
                return Ok(res);


            //string conStr = UnitDatabase.CreateConnectionString(dataAccount[0], dataAccount[1], dataAccount[2], dataAccount[3], ace, sal, group, AFI_Move.SerialNumber ?? 0, UnitPublic.access_View, UnitPublic.act_View, 0);
        }







        public class AFI_StatusChange
        {
            public byte DMode { get; set; }

            public string UserCode { get; set; }

            public long SerialNumber { get; set; }

            public string Status { get; set; }

            public int InOut { get; set; }
        }

        [Route("api/IDocData/ChangeStatus/{ace}/{sal}/{group}")]
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
            var DBase = UnitDatabase.dataDB.Where(p => p.UserName == dataAccount[0] && p.Password == dataAccount[1]).Single();
            string res = UnitDatabase.TestAcount(DBase, dataAccount[3], ace, group, UnitPublic.access_View);
            if (res == "")
            {
                try
                {
                    string sql = string.Format(CultureInfo.InvariantCulture,
                          @"DECLARE	@return_value int
                            EXEC	@return_value = {0}.[dbo].[Web_SaveIDoc_Status]
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
                }
                catch (Exception)
                {
                    throw;
                }

                UnitDatabase.SaveLog(dataAccount[0], dataAccount[1], dataAccount[2], ace, sal, group, AFI_StatusChange.SerialNumber, AFI_StatusChange.InOut == 1 ? UnitPublic.access_IIDOC : UnitPublic.access_IODOC, 7, "Y", 1, 0);
                return Ok(200);
            }
            else
                return Ok(res);


            //string conStr = UnitDatabase.CreateConnectionString(dataAccount[0], dataAccount[1], dataAccount[2], dataAccount[3], ace, sal, group, AFI_StatusChange.SerialNumber, AFI_StatusChange.InOut == 1 ? UnitPublic.access_IIDOC : UnitPublic.access_IODOC, UnitPublic.act_Edit, 0);
        }

        // GET: api/IDocData/IDocP لیست سند    
        [Route("api/IDocData/IDocP/{ace}/{sal}/{group}/{SerialNumber}/{InOut}")]
        public async Task<IHttpActionResult> GetAllWeb_IDocP(string ace, string sal, string group, long SerialNumber, int InOut = 1)
        {
            string dBName = UnitDatabase.DatabaseName(ace, sal, group);
            var dataAccount = UnitDatabase.ReadUserPassHeader(this.Request.Headers);
            var DBase = UnitDatabase.dataDB.Where(p => p.UserName == dataAccount[0] && p.Password == dataAccount[1]).Single();
            string res = UnitDatabase.TestAcount(DBase, dataAccount[3], ace, group, UnitPublic.access_View);
            if (res == "")
            {
                string sql = string.Format(@"select * from {0}.dbo.Web_IDocP where SerialNumber = {1} order by BandNo", dBName, SerialNumber);
                var listIDocP = DBase.DB.Database.SqlQuery<Web_IDocP>(sql);
                return Ok(listIDocP);
            }
            else
                return Ok(res);

            //string conStr = UnitDatabase.CreateConnectionString(dataAccount[0], dataAccount[1], dataAccount[2], dataAccount[3], ace, sal, group, SerialNumber, InOut == 1 ? UnitPublic.access_IIDOC : UnitPublic.access_IODOC, UnitPublic.act_Print, 0);
        }


        public class AFI_TestIDocB
        {
            public long SerialNumber { get; set; }

            public long Last_SerialNumber { get; set; }

            public string flagTest { get; set; }

        }


        [Route("api/IDocData/TestIDoc/{ace}/{sal}/{group}")]
        [ResponseType(typeof(TestDocB))]
        public async Task<IHttpActionResult> PostWeb_TestIDoc(string ace, string sal, string group, AFI_TestIDocB AFI_TestIDocB)
        {
            string dBName = UnitDatabase.DatabaseName(ace, sal, group);
            var dataAccount = UnitDatabase.ReadUserPassHeader(this.Request.Headers);
            var DBase = UnitDatabase.dataDB.Where(p => p.UserName == dataAccount[0] && p.Password == dataAccount[1]).Single();
            string res = UnitDatabase.TestAcount(DBase, dataAccount[3], ace, group, UnitPublic.access_View);
            if (res == "")
            {
                string sql = string.Format(CultureInfo.InvariantCulture,
                            @"EXEC	{0}.[dbo].[{1}] @serialNumber = {2}  ,@last_SerialNumber = {3}, @UserCode = '{4}' ",
                            dBName,
                            AFI_TestIDocB.flagTest == "Y" ? "Web_TestIDoc_Temp" : "Web_TestIDoc",
                            AFI_TestIDocB.SerialNumber,
                            AFI_TestIDocB.Last_SerialNumber,
                            dataAccount[2]);
                try
                {
                    var result = DBase.DB.Database.SqlQuery<TestDocB>(sql).ToList();
                    var jsonResult = JsonConvert.SerializeObject(result);
                    return Ok(jsonResult);
                }
                catch (Exception e)
                {
                    throw;
                }

            }
            else
                return Ok(res);
            //string conStr = UnitDatabase.CreateConnectionString(dataAccount[0], dataAccount[1], dataAccount[2], dataAccount[3], ace, sal, group, 0, UnitPublic.access_View, UnitPublic.act_Report, 0);
        }


        public class AFI_SaveIDoc_HZ
        {
            public long SerialNumber { get; set; }

            public string Tanzim { get; set; }

        }


        [Route("api/IDocData/SaveIDoc_HZ/{ace}/{sal}/{group}")]
        [ResponseType(typeof(TestDocB))]
        public async Task<IHttpActionResult> PostWeb_SaveIDoc_HZ(string ace, string sal, string group, AFI_SaveIDoc_HZ AFI_SaveIDoc_HZ)
        {
            string dBName = UnitDatabase.DatabaseName(ace, sal, group);
            var dataAccount = UnitDatabase.ReadUserPassHeader(this.Request.Headers);
            var DBase = UnitDatabase.dataDB.Where(p => p.UserName == dataAccount[0] && p.Password == dataAccount[1]).Single();
            string res = UnitDatabase.TestAcount(DBase, dataAccount[3], ace, group, UnitPublic.access_View);
            if (res == "")
            {
                string sql = string.Format(CultureInfo.InvariantCulture,
                           @"DECLARE	@return_value int
                                             EXEC	@return_value = {0}.[dbo].[Web_SaveIDoc_HZ]
		                                            @SerialNumber = {1},
		                                            @Tanzim = '{2}'
                                             SELECT	'Return Value' = @return_value ",
                           dBName, AFI_SaveIDoc_HZ.SerialNumber, AFI_SaveIDoc_HZ.Tanzim);
                var result = DBase.DB.Database.SqlQuery<int>(sql).ToList();
                return Ok("ok");
            }
            else
                return Ok(res);


            //string conStr = UnitDatabase.CreateConnectionString(dataAccount[0], dataAccount[1], dataAccount[2], dataAccount[3], ace, sal, group, 0, UnitPublic.access_View, UnitPublic.act_View, 0);
        }



        public class TestIDoc_DeleteObject
        {
            public long SerialNumber { get; set; }

        }

        public class TestIDoc_Delete
        {
            public int id { get; set; }

            public byte Test { get; set; }

            public string TestName { get; set; }

            public string TestCap { get; set; }

            public int BandNo { get; set; }

        }



        [Route("api/IDocData/TestIDoc_Delete/{ace}/{sal}/{group}")]
        [ResponseType(typeof(TestIDoc_Delete))]
        public async Task<IHttpActionResult> PostWeb_TestIDoc_Delete(string ace, string sal, string group, TestIDoc_DeleteObject TestIDoc_DeleteObject)
        {
            string dBName = UnitDatabase.DatabaseName(ace, sal, group);
            var dataAccount = UnitDatabase.ReadUserPassHeader(this.Request.Headers);
            var DBase = UnitDatabase.dataDB.Where(p => p.UserName == dataAccount[0] && p.Password == dataAccount[1]).Single();
            string res = UnitDatabase.TestAcount(DBase, dataAccount[3], ace, group, UnitPublic.access_View);
            if (res == "")
            {
                string sql = string.Format(CultureInfo.InvariantCulture,
                            @"EXEC	{0}.[dbo].[Web_TestIDoc_Delete] @serialNumber = {1}, @UserCode = '{2}' ", dBName, TestIDoc_DeleteObject.SerialNumber, dataAccount[2]);
                try
                {
                    var result = DBase.DB.Database.SqlQuery<TestIDoc_Delete>(sql).ToList();
                    var jsonResult = JsonConvert.SerializeObject(result);
                    return Ok(jsonResult);
                }
                catch (Exception e)
                {
                    throw;
                }
            }
            else
                return Ok(res);


            //string conStr = UnitDatabase.CreateConnectionString(dataAccount[0], dataAccount[1], dataAccount[2], dataAccount[3], ace, sal, group, 0, UnitPublic.access_View, UnitPublic.act_Report, 0);
        }



        public class AFI_TestIDoc_New
        {
            public string DocDate { get; set; }

            public string ModeCode { get; set; }

            public string DocNo { get; set; }

            public long SerialNumber { get; set; }

            public string InvCode { get; set; }


        }




        [Route("api/IDocData/TestIDoc_New/{ace}/{sal}/{group}")]
        [ResponseType(typeof(TestDocB))]
        public async Task<IHttpActionResult> PostWeb_TestIDoc_New(string ace, string sal, string group, AFI_TestIDoc_New AFI_TestIDoc_New)
        {
            string dBName = UnitDatabase.DatabaseName(ace, sal, group);
            var dataAccount = UnitDatabase.ReadUserPassHeader(this.Request.Headers);
            var DBase = UnitDatabase.dataDB.Where(p => p.UserName == dataAccount[0] && p.Password == dataAccount[1]).Single();
            string res = UnitDatabase.TestAcount(DBase, dataAccount[3], ace, group, UnitPublic.access_View);
            if (res == "")
            {
                string sql = string.Format(CultureInfo.InvariantCulture,
                      @"EXEC	{0}.[dbo].[Web_TestIDoc_New]  @UserCode = '{1}',  @DocDate = '{2}', @ModeCode = '{3}' , @DocNo = '{4}' , @SerialNumber = {5}, @InvCode = N'{6}'",
                      dBName,
                      dataAccount[2],
                       AFI_TestIDoc_New.DocDate,
                       AFI_TestIDoc_New.ModeCode,
                       AFI_TestIDoc_New.DocNo,
                       AFI_TestIDoc_New.SerialNumber,
                       AFI_TestIDoc_New.InvCode
                      );

                try
                {
                    var result = DBase.DB.Database.SqlQuery<TestDocB>(sql).ToList();
                    var jsonResult = JsonConvert.SerializeObject(result);
                    return Ok(jsonResult);
                }
                catch (Exception e)
                {
                    throw;
                }
            }
            else
                return Ok(res);


            //string conStr = UnitDatabase.CreateConnectionString(dataAccount[0], dataAccount[1], dataAccount[2], dataAccount[3], ace, sal, group, 0, UnitPublic.access_View, UnitPublic.act_Report, 0);
        }




        public class AFI_TestIDoc_Edit
        {
            public long Serialnumber { get; set; }

        }

        [Route("api/IDocData/TestIDoc_Edit/{ace}/{sal}/{group}")]
        [ResponseType(typeof(TestDocB))]
        public async Task<IHttpActionResult> PostWeb_TestIDoc_Edit(string ace, string sal, string group, AFI_TestIDoc_Edit AFI_TestIDoc_Edit)
        {
            string dBName = UnitDatabase.DatabaseName(ace, sal, group);
            var dataAccount = UnitDatabase.ReadUserPassHeader(this.Request.Headers);
            var DBase = UnitDatabase.dataDB.Where(p => p.UserName == dataAccount[0] && p.Password == dataAccount[1]).Single();
            string res = UnitDatabase.TestAcount(DBase, dataAccount[3], ace, group, UnitPublic.access_View);
            if (res == "")
            {
                string sql = string.Format(CultureInfo.InvariantCulture,
                      @"EXEC	{0}.[dbo].[Web_TestIDoc_Edit] @UserCode = '{1}',  @Serialnumber = '{2}'",
                      dBName,
                      dataAccount[2],
                       AFI_TestIDoc_Edit.Serialnumber
                      );
                try
                {
                    var result = DBase.DB.Database.SqlQuery<TestDocB>(sql).ToList();
                    var jsonResult = JsonConvert.SerializeObject(result);
                    return Ok(jsonResult);
                }
                catch (Exception e)
                {
                    throw;
                }
            }
            else
                return Ok(res);


            //string conStr = UnitDatabase.CreateConnectionString(dataAccount[0], dataAccount[1], dataAccount[2], dataAccount[3], ace, sal, group, 0, UnitPublic.access_View, UnitPublic.act_Report, 0);
        }


    }

}
