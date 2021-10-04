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
            var dataAccount = UnitDatabase.ReadUserPassHeader(this.Request.Headers);
            string con = UnitDatabase.CreateConection(dataAccount[0], dataAccount[1], dataAccount[2], ace, sal, group, serialNumber, "", 0, 0);
            if (con == "ok")
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
            var dataAccount = UnitDatabase.ReadUserPassHeader(this.Request.Headers);
            string con = UnitDatabase.CreateConection(dataAccount[0], dataAccount[1], dataAccount[2], ace, sal, group, 0, "", 0, 0);
            if (con == "ok")
            {
                string sql = string.Format(@"SELECT count(SerialNumber) FROM Web_IDocH WHERE InOut = {0} ", InOut);
                int count = UnitDatabase.db.Database.SqlQuery<int>(sql).Single();
                return Ok(count);
            }
            return Ok(con);
        }
        // GET: api/IDocData/IDocH اخرین تاریخ    
        [Route("api/IDocData/IDocH/LastDate/{ace}/{sal}/{group}/{InOut}")]
        public async Task<IHttpActionResult> GetWeb_IDocHLastDate(string ace, string sal, string group, byte InOut)
        {
            var dataAccount = UnitDatabase.ReadUserPassHeader(this.Request.Headers);
            string con = UnitDatabase.CreateConection(dataAccount[0], dataAccount[1], dataAccount[2], ace, sal, group, 0, "", 0, 0);
            if (con == "ok")
            {
                string sql = string.Format(@"EXEC[dbo].[Web_Doc_Dates]
                                             @TableName = '{0}',
		                                     @Mode = '''{1}'''", "inv5doch", InOut);
                string lastdate = UnitDatabase.db.Database.SqlQuery<string>(sql).Single();
                return Ok(lastdate);
            }
            return Ok(con);
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

            var dataAccount = UnitDatabase.ReadUserPassHeader(this.Request.Headers);
            string con = UnitDatabase.CreateConection(dataAccount[0], dataAccount[1], dataAccount[2], ace, sal, group, 0, "", 0, 0);
            if (con == "ok")
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
                                       ThvlMelliCode
                                       from Web_IDocH_F(3,'{0}') where 1 = 1 and (@DocNo = ''  or DocNo = @DocNo) and (@ModeCode = ''  or ModeCode = @ModeCode)   ", IDocHMinObject.user);


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


                var listIDocH = UnitDatabase.db.Database.SqlQuery<Web_IDocHMini>(sql);
                return Ok(listIDocH);
            }
            return Ok(con);
        }

        // GET: api/IDocData/IDocB اطلاعات تکمیلی سند انبار   
        [Route("api/IDocData/IDocB/{ace}/{sal}/{group}/{serialNumber}")]
        public async Task<IHttpActionResult> GetWeb_IDocB(string ace, string sal, string group, long serialNumber)
        {
            var dataAccount = UnitDatabase.ReadUserPassHeader(this.Request.Headers);
            string con = UnitDatabase.CreateConection(dataAccount[0], dataAccount[1], dataAccount[2], ace, sal, group, serialNumber, "", 0, 0);
            if (con == "ok")
            {
                string sql = string.Format(@"SELECT SerialNumber,BandNo,KalaCode,KalaName,MainUnit,MainUnitName,Amount1,Amount2,Amount3,UnitPrice,TotalPrice,Comm,Up_Flag,KalaDeghatR1,KalaDeghatR2,KalaDeghatR3,KalaDeghatM1,KalaDeghatM2,KalaDeghatM3,DeghatR
                                         FROM Web_IDocB WHERE SerialNumber = {0}", serialNumber);
                var listIDocB = UnitDatabase.db.Database.SqlQuery<Web_IDocB>(sql);
                return Ok(listIDocB);
            }
            return Ok(con);
        }

        [Route("api/IDocData/UpdatePriceAnbar/{ace}/{sal}/{group}/{serialnumber}")]
        [ResponseType(typeof(AFI_IDocBi))]
        public async Task<IHttpActionResult> PostWeb_UpdatePriceAnbar(string ace, string sal, string group, long serialnumber)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var dataAccount = UnitDatabase.ReadUserPassHeader(this.Request.Headers);
            string con = UnitDatabase.CreateConection(dataAccount[0], dataAccount[1], dataAccount[2], ace, sal, group, serialnumber, "", 0, 0);
            if (con == "ok")
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


                string sql1 = string.Format(@"SELECT SerialNumber,BandNo,KalaCode,KalaName,MainUnit,MainUnitName,Amount1,Amount2,Amount3,UnitPrice,TotalPrice,Comm,Up_Flag,KalaDeghatR1,KalaDeghatR2,KalaDeghatR3,KalaDeghatM1,KalaDeghatM2,KalaDeghatM3,DeghatR
                                         FROM Web_IDocB WHERE SerialNumber = {0}", serialnumber);
                var listIDocB = UnitDatabase.db.Database.SqlQuery<Web_IDocB>(sql1);
                return Ok(listIDocB);
            }
            return Ok(con);
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
            var dataAccount = UnitDatabase.ReadUserPassHeader(this.Request.Headers);

            string con = UnitDatabase.CreateConection(dataAccount[0], dataAccount[1], dataAccount[2], ace, sal, group, 0, "", 0, 0);
            if (con == "ok")
            {
                string sql;
                if (iModeObject.InOut == 0)
                    sql = string.Format(@"SELECT * FROM Web_IMode_F({0},'{1}') order by OrderFld ", iModeObject.Mode, iModeObject.UserCode);
                else
                    sql = string.Format(@"SELECT * FROM Web_IMode_F({0},'{1}') WHERE InOut = {2} order by OrderFld ", iModeObject.Mode, iModeObject.UserCode, iModeObject.InOut);
                var listIMode = UnitDatabase.db.Database.SqlQuery<Web_IMode>(sql);

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
            return Ok(con);
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

                            EXEC	@return_value = [dbo].[Web_SaveIDoc_Move]
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

                var list = UnitDatabase.db.Web_IDocH.Where(c => c.SerialNumber == value && c.ModeCode == AFI_Move.ModeCode && c.InvCode == AFI_Move.InvCode);
                var listTemp = list.Single();
                UnitDatabase.SaveLog(dataAccount[0], dataAccount[1], dataAccount[2], ace, sal, group, value, listTemp.InOut == 1 ? "IIDoc" : "IODoc", 2, "Y", 0);
                return Ok(list);
            }
            return Ok(con);
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
            int value = 0;
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var dataAccount = UnitDatabase.ReadUserPassHeader(this.Request.Headers);
            string con = UnitDatabase.CreateConection(dataAccount[0], dataAccount[1], dataAccount[2], ace, sal, group, AFI_StatusChange.SerialNumber, "", 0, 0);
            if (con == "ok")
            {
                try
                {
                    string sql = string.Format(CultureInfo.InvariantCulture,
                          @"DECLARE	@return_value int
                            EXEC	@return_value = [dbo].[Web_SaveIDoc_Status]
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

                UnitDatabase.SaveLog(dataAccount[0], dataAccount[1], dataAccount[2], ace, sal, group, AFI_StatusChange.SerialNumber, AFI_StatusChange.InOut == 1 ? "IIDoc" : "IODoc", 7, "Y", 0);
                return Ok(200);
            }
            return Ok(con);
        }

        // GET: api/IDocData/IDocP لیست سند    
        [Route("api/IDocData/IDocP/{ace}/{sal}/{group}/{SerialNumber}")]
        public async Task<IHttpActionResult> GetAllWeb_IDocP(string ace, string sal, string group, long SerialNumber)
        {
            var dataAccount = UnitDatabase.ReadUserPassHeader(this.Request.Headers);
            string con = UnitDatabase.CreateConection(dataAccount[0], dataAccount[1], dataAccount[2], ace, sal, group, SerialNumber, "", 0, 0);
            if (con == "ok")
            {
                string sql = string.Format(@"select * from Web_IDocP where SerialNumber = {0} order by BandNo", SerialNumber);
                var listIDocP = UnitDatabase.db.Database.SqlQuery<Web_IDocP>(sql);
                return Ok(listIDocP);
            }
            return Ok(con);
        }


        public class AFI_TestIDocB
        {
            public long SerialNumber { get; set; }

        }


        [Route("api/IDocData/TestIDoc/{ace}/{sal}/{group}")]
        [ResponseType(typeof(TestDocB))]
        public async Task<IHttpActionResult> PostWeb_TestIDoc(string ace, string sal, string group, AFI_TestIDocB AFI_TestIDocB)
        {
            var dataAccount = UnitDatabase.ReadUserPassHeader(this.Request.Headers);
            string con = UnitDatabase.CreateConection(dataAccount[0], dataAccount[1], dataAccount[2], ace, sal, group, 0, "", 0, 0);
            if (con == "ok")
            {
                string sql = string.Format(CultureInfo.InvariantCulture,
                                           @"EXEC	[dbo].[Web_TestIDoc] @serialNumber = {0}  , @UserCode = '{1}' ",
                                           AFI_TestIDocB.SerialNumber,
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


        public class AFI_SaveIDoc_HZ
        {
            public long SerialNumber { get; set; }

            public string Tanzim { get; set; }

        }


        [Route("api/IDocData/SaveIDoc_HZ/{ace}/{sal}/{group}")]
        [ResponseType(typeof(TestDocB))]
        public async Task<IHttpActionResult> PostWeb_SaveIDoc_HZ(string ace, string sal, string group, AFI_SaveIDoc_HZ AFI_SaveIDoc_HZ)
        {
            var dataAccount = UnitDatabase.ReadUserPassHeader(this.Request.Headers);
            string con = UnitDatabase.CreateConection(dataAccount[0], dataAccount[1], dataAccount[2], ace, sal, group, 0, "", 0, 0);
            if (con == "ok")
            {
                string sql = string.Format(CultureInfo.InvariantCulture,
                                           @"DECLARE	@return_value int
                                             EXEC	@return_value = [dbo].[Web_SaveIDoc_HZ]
		                                            @SerialNumber = {0},
		                                            @Tanzim = '{1}'
                                             SELECT	'Return Value' = @return_value ",
                                           AFI_SaveIDoc_HZ.SerialNumber, AFI_SaveIDoc_HZ.Tanzim);
                var result = UnitDatabase.db.Database.SqlQuery<int>(sql).ToList();
                return Ok("ok");
            }
            return Ok(con);
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
            var dataAccount = UnitDatabase.ReadUserPassHeader(this.Request.Headers);
            string con = UnitDatabase.CreateConection(dataAccount[0], dataAccount[1], dataAccount[2], ace, sal, group, 0, "", 0, 0);
            if (con == "ok")
            {
                string sql = string.Format(CultureInfo.InvariantCulture,
                                           @"EXEC	[dbo].[Web_TestIDoc_Delete] @serialNumber = {0}, @UserCode = '{1}' ", TestIDoc_DeleteObject.SerialNumber, dataAccount[2]);
                try
                {
                    var result = UnitDatabase.db.Database.SqlQuery<TestIDoc_Delete>(sql).ToList();
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



        public class AFI_TestIDoc_New
        {
            public string DocDate { get; set; }

            public string ModeCode { get; set; }

            public string DocNo { get; set; }

            public long SerialNumber { get; set; }

        }




            [Route("api/IDocData/TestIDoc_New/{ace}/{sal}/{group}")]
        [ResponseType(typeof(TestDocB))]
        public async Task<IHttpActionResult> PostWeb_TestIDoc_New(string ace, string sal, string group, AFI_TestIDoc_New AFI_TestIDoc_New)
        {
            var dataAccount = UnitDatabase.ReadUserPassHeader(this.Request.Headers);
            string con = UnitDatabase.CreateConection(dataAccount[0], dataAccount[1], dataAccount[2], ace, sal, group, 0, "", 0, 0);
            if (con == "ok")
            {
                string sql = string.Format(CultureInfo.InvariantCulture,
                      @"EXEC	[dbo].[Web_TestIDoc_New]  @UserCode = '{0}',  @DocDate = '{1}', @ModeCode = '{2}' , @DocNo = '{3}' , @SerialNumber = {4}",
                      dataAccount[2],
                       AFI_TestIDoc_New.DocDate,
                       AFI_TestIDoc_New.ModeCode,
                       AFI_TestIDoc_New.DocNo,
                       AFI_TestIDoc_New.SerialNumber
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




        public class AFI_TestIDoc_Edit
        {
            public long Serialnumber { get; set; }

        }

        [Route("api/IDocData/TestIDoc_Edit/{ace}/{sal}/{group}")]
        [ResponseType(typeof(TestDocB))]
        public async Task<IHttpActionResult> PostWeb_TestIDoc_Edit(string ace, string sal, string group, AFI_TestIDoc_Edit AFI_TestIDoc_Edit)
        {
            var dataAccount = UnitDatabase.ReadUserPassHeader(this.Request.Headers);
            string con = UnitDatabase.CreateConection(dataAccount[0], dataAccount[1], dataAccount[2], ace, sal, group, 0, "", 0, 0);
            if (con == "ok")
            {
                string sql = string.Format(CultureInfo.InvariantCulture,
                      @"EXEC	[dbo].[Web_TestIDoc_Edit] @UserCode = '{0}',  @Serialnumber = '{1}'",
                      dataAccount[2],
                       AFI_TestIDoc_Edit.Serialnumber
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
