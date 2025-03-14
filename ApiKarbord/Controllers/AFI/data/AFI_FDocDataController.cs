﻿using System;
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
    public class AFI_FDocDataController : ApiController
    {

        // GET: api/FDocData/FMode اطلاعات نوع سند خرید و فروش   
        [Route("api/FDocData/FMode/{ace}/{sal}/{group}/{InOut}")]
        public async Task<IHttpActionResult> GetWeb_FMode(string ace, string sal, string group, int InOut)
        {
            string dBName = UnitDatabase.DatabaseName(ace, sal, group);
            var dataAccount = UnitDatabase.ReadUserPassHeader(this.Request.Headers);
            var DBase = UnitDatabase.dataDB.Where(p => p.UserName.ToUpper() == dataAccount[0].ToUpper() && p.Password == dataAccount[1]).Single();
            string res = UnitDatabase.TestAcount(DBase, dataAccount[3], ace, group, UnitPublic.access_View);
            if (res == "")
            {
                string sql;
                if (InOut == 0)
                    sql = string.Format(@"SELECT * FROM {0}.dbo.Web_FMode order by OrderFld ", dBName);
                else
                    sql = string.Format(@"SELECT * FROM {0}.dbo.Web_FMode WHERE InOut = {1} order by OrderFld ", dBName, InOut);

                var listFMode = DBase.DB.Database.SqlQuery<Web_FMode>(sql);
                return Ok(listFMode);
            }
            else
                return Ok(res);

            //string conStr = UnitDatabase.CreateConnectionString(dataAccount[0], dataAccount[1], dataAccount[2], dataAccount[3], ace, sal, group, 0, UnitPublic.access_View, UnitPublic.act_View, 0);
        }


        // GET: api/FDocData/FDocH اطلاعات تکمیلی فاکتور    
        [Route("api/FDocData/FDocH/{ace}/{sal}/{group}/{serialNumber}/{ModeCode}")]
        public async Task<IHttpActionResult> GetWeb_FDocH(string ace, string sal, string group, long serialNumber, string ModeCode)
        {
            string dBName = UnitDatabase.DatabaseName(ace, sal, group);
            var dataAccount = UnitDatabase.ReadUserPassHeader(this.Request.Headers);
            string sql = string.Format(@"SELECT * FROM {0}.dbo.Web_FDocH where SerialNumber = {1} and ModeCode = '{2}' ", dBName, serialNumber, ModeCode);

            var DBase = UnitDatabase.dataDB.Where(p => p.UserName.ToUpper() == dataAccount[0].ToUpper() && p.Password == dataAccount[1]).Single();
            string res = UnitDatabase.TestAcount(DBase, dataAccount[3], ace, group, UnitPublic.access_View);
            if (res == "")
            {
                return Ok(DBase.DB.Database.SqlQuery<Web_FDocH>(sql));
            }
            else
                return Ok(res);

            // string conStr = UnitDatabase.CreateConnectionString(dataAccount[0], dataAccount[1], dataAccount[2], dataAccount[3], ace, sal, group, serialNumber, /*UnitPublic.ModeCodeConnection(ModeCode),*/ UnitPublic.access_View, UnitPublic.act_View, 0);
        }



        public class FDocHMinObject
        {
            public string ModeCode { get; set; }

            public int select { get; set; }

            public string user { get; set; }

            public bool AccessSanad { get; set; }

            public string updatedate { get; set; }

            public string Sort { get; set; }

            public string ModeSort { get; set; }

            public string DocNo { get; set; }

        }

        // Post: api/FDocData/FDocH لیست فاکتور    
        [Route("api/FDocData/FDocH/{ace}/{sal}/{group}")]
        public async Task<IHttpActionResult> PostAllWeb_FDocHMin(string ace, string sal, string group, FDocHMinObject FDocHMinObject)
        {
            string dBName = UnitDatabase.DatabaseName(ace, sal, group);
            var dataAccount = UnitDatabase.ReadUserPassHeader(this.Request.Headers);
            var DBase = UnitDatabase.dataDB.Where(p => p.UserName.ToUpper() == dataAccount[0].ToUpper() && p.Password == dataAccount[1]).Single();
            string res = UnitDatabase.TestAcount(DBase, dataAccount[3], ace, group, UnitPublic.access_View);
            if (res == "")
            {
                string sql = string.Format(CultureInfo.InvariantCulture,
                             @"declare @DocNo nvarchar(50) = '{0}'  select ",
                             FDocHMinObject.DocNo);

                if (FDocHMinObject.select == 0)
                    sql += " top(100) ";
                sql += string.Format(CultureInfo.InvariantCulture, @"SerialNumber,                                   
                                       DocNo,
                                       SortDocNo,
                                       DocDate,
                                       CustCode,
                                       CustName,
                                       Spec,
                                       KalaPriceCode,
                                       InvCode,
                                       MkzCode,
                                       MkzName,
                                       OprCode,
                                       OprName,                                       
                                       VstrCode,                                       
                                       VstrName,                                       
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
                                       Tasvib, 
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
                                       UpdateDate,ArzCode,ArzName,ArzRate,
                                       CustEcoCode,CustMelliCode,CustTel,CustFax,CustMobile,CustEmail,CustCity,CustStreet,CustAlley,CustPlack,CustZipCode,CustAddress,CustOstan,CustShahrestan,CustRegion,
                                       AccSerialNumber,AccDocNo,InvReg                                      
                                       from {0}.dbo.Web_FDocH_F({1},'{2}') where ModeCode = '{3}' and (@DocNo = ''  or DocNo = @DocNo) ",
                                       dBName,
                                       0,
                                        FDocHMinObject.user
                                       , FDocHMinObject.ModeCode.ToString());
                if (FDocHMinObject.AccessSanad == false)
                    sql += " and Eghdam = '" + FDocHMinObject.user + "' ";

                if (FDocHMinObject.updatedate != null)
                    sql += " and UpdateDate >= CAST('" + FDocHMinObject.updatedate + "' AS DATETIME2)";


                sql += " order by ";

                if (FDocHMinObject.Sort == "" || FDocHMinObject.Sort == null)
                {
                    FDocHMinObject.Sort = "DocDate Desc,SortDocNo Desc";
                }
                else if (FDocHMinObject.Sort == "DocDate")
                {
                    if (FDocHMinObject.ModeSort == "ASC")
                        FDocHMinObject.Sort = "DocDate Asc,SortDocNo Asc";
                    else
                        FDocHMinObject.Sort = "DocDate Desc,SortDocNo Desc";
                }
                else if (FDocHMinObject.Sort == "Status")
                {
                    if (FDocHMinObject.ModeSort == "ASC")
                        FDocHMinObject.Sort = "Status Asc, DocDate Asc,SortDocNo Asc";
                    else
                        FDocHMinObject.Sort = "Status Desc, DocDate Desc,SortDocNo Desc";
                }
                else
                {
                    FDocHMinObject.Sort = FDocHMinObject.Sort + " " + FDocHMinObject.ModeSort;
                }

                sql += FDocHMinObject.Sort;

                //sql += " order by SortDocNo desc ";
                var listFDocH = DBase.DB.Database.SqlQuery<Web_FDocHMini>(sql);
                return Ok(listFDocH);
            }
            else
                return Ok(res);


            //string conStr = UnitDatabase.CreateConnectionString(dataAccount[0], dataAccount[1], dataAccount[2], dataAccount[3], ace, sal, group, 0, UnitPublic.access_View, UnitPublic.act_View, 0);
        }





        public class FDocHMinAppObject
        {
            public string ModeCode { get; set; }

            public int select { get; set; }

            public string user { get; set; }

            public bool AccessSanad { get; set; }

            public string updatedate { get; set; }

            public string Sort { get; set; }

            public string ModeSort { get; set; }

            public string DocNo { get; set; }

            public string azTarikh { get; set; }

            public string taTarikh { get; set; }

            public string CustCode { get; set; }

            public string VstrCode { get; set; }

        }




        // Post: api/FDocData/FDocHApp لیست فاکتور    
        [Route("api/FDocData/FDocHApp/{ace}/{sal}/{group}")]
        public async Task<IHttpActionResult> PostAllWeb_FDocHMinApp(string ace, string sal, string group, FDocHMinAppObject FDocHMinAppObject)
        {
            string dBName = UnitDatabase.DatabaseName(ace, sal, group);
            var dataAccount = UnitDatabase.ReadUserPassHeader(this.Request.Headers);
            var DBase = UnitDatabase.dataDB.Where(p => p.UserName.ToUpper() == dataAccount[0].ToUpper() && p.Password == dataAccount[1]).Single();
            string res = UnitDatabase.TestAcount(DBase, dataAccount[3], ace, group, UnitPublic.access_View);
            if (res == "")
            {
                string sql = string.Format(CultureInfo.InvariantCulture,
                              @"declare @DocNo nvarchar(50) = '{0}'  select ",
                              FDocHMinAppObject.DocNo);

                sql += " top( " + FDocHMinAppObject.select + " ) ";

                sql += string.Format(CultureInfo.InvariantCulture, @"SerialNumber,                                   
                                       DocNo,
                                       SortDocNo,
                                       DocDate,
                                       CustCode,
                                       CustName,
                                       Spec,
                                       KalaPriceCode,
                                       --KalaPriceName,
                                       InvCode,
                                       MkzCode,
                                       MkzName,
                                       OprCode,
                                       OprName,                                       
                                       VstrCode,                                       
                                       VstrName,                                       
                                       ModeCode,
                                       Status,
                                       PaymentType,
                                       Tanzim,
                                       Taeed,
                                       Tasvib, 
                                       FinalPrice,
                                       Eghdam,
                                       UpdateDate,ArzCode,ArzName,ArzRate,
                                       AddMinPrice1,
                                       AddMinPrice2,
                                       AddMinPrice3,
                                       AddMinPrice4,
                                       AddMinPrice5,
                                       AddMinPrice6,
                                       AddMinPrice7,
                                       AddMinPrice8,
                                       AddMinPrice9,
                                       AddMinPrice10
                                       from {0}.dbo.Web_FDocH_F({1},'{2}') where ModeCode = '{3}' and (@DocNo = ''  or DocNo = @DocNo) ",
                                      dBName, 0, FDocHMinAppObject.user, FDocHMinAppObject.ModeCode.ToString());
                if (FDocHMinAppObject.AccessSanad == false)
                    sql += " and Eghdam = '" + FDocHMinAppObject.user + "' ";

                if (FDocHMinAppObject.updatedate != null)
                    sql += " and UpdateDate >= CAST('" + FDocHMinAppObject.updatedate + "' AS DATETIME2)";

                if (FDocHMinAppObject.azTarikh != "")
                    sql += string.Format(" and DocDate >= '{0}' ", FDocHMinAppObject.azTarikh);

                if (FDocHMinAppObject.taTarikh != "")
                    sql += string.Format(" and DocDate <= '{0}' ", FDocHMinAppObject.taTarikh);


                if (FDocHMinAppObject.CustCode != "")
                    sql += string.Format(" and CustCode = '{0}' ", FDocHMinAppObject.CustCode);

                if (FDocHMinAppObject.VstrCode != "")
                    sql += string.Format(" and VstrCode = '{0}' ", FDocHMinAppObject.VstrCode);


                sql += " order by ";

                if (FDocHMinAppObject.Sort == "" || FDocHMinAppObject.Sort == null)
                {
                    FDocHMinAppObject.Sort = "DocDate Desc,SortDocNo Desc";
                }
                else if (FDocHMinAppObject.Sort == "DocDate")
                {
                    if (FDocHMinAppObject.ModeSort == "ASC")
                        FDocHMinAppObject.Sort = "DocDate Asc,SortDocNo Asc";
                    else
                        FDocHMinAppObject.Sort = "DocDate Desc,SortDocNo Desc";
                }
                else if (FDocHMinAppObject.Sort == "Status")
                {
                    if (FDocHMinAppObject.ModeSort == "ASC")
                        FDocHMinAppObject.Sort = "Status Asc, DocDate Asc,SortDocNo Asc";
                    else
                        FDocHMinAppObject.Sort = "Status Desc, DocDate Desc,SortDocNo Desc";
                }
                else
                {
                    FDocHMinAppObject.Sort = FDocHMinAppObject.Sort + " " + FDocHMinAppObject.ModeSort;
                }

                sql += FDocHMinAppObject.Sort;

                //sql += " order by SortDocNo desc ";
                var listFDocH = DBase.DB.Database.SqlQuery<Web_FDocHMiniApp>(sql);
                return Ok(listFDocH);

            }
            else
                return Ok(res);


            //string conStr = UnitDatabase.CreateConnectionString(dataAccount[0], dataAccount[1], dataAccount[2], dataAccount[3], ace, sal, group, 0, UnitPublic.access_View, UnitPublic.act_View, 0);
        }




        // GET: api/FDocData/FDocH تعداد رکورد ها    
        [Route("api/FDocData/FDocH/{ace}/{sal}/{group}/{ModeCode}")]
        public async Task<IHttpActionResult> GetWeb_FDocHCount(string ace, string sal, string group, string ModeCode)
        {
            string dBName = UnitDatabase.DatabaseName(ace, sal, group);
            var dataAccount = UnitDatabase.ReadUserPassHeader(this.Request.Headers);
            string sql = string.Format(@"SELECT count(SerialNumber) FROM {0}.dbo.Web_FDocH WHERE ModeCode = '{1}'", dBName, ModeCode);

            var DBase = UnitDatabase.dataDB.Where(p => p.UserName.ToUpper() == dataAccount[0].ToUpper() && p.Password == dataAccount[1]).Single();
            string res = UnitDatabase.TestAcount(DBase, dataAccount[3], ace, group, UnitPublic.access_View);
            if (res == "")
            {
                return Ok(DBase.DB.Database.SqlQuery<int>(sql).Single());
            }
            else
                return Ok(res);

            //string conStr = UnitDatabase.CreateConnectionString(dataAccount[0], dataAccount[1], dataAccount[2], dataAccount[3], ace, sal, group, 0, UnitPublic.access_View, UnitPublic.act_View, 0);
        }

        // GET: api/FDocData/FDocH آخرین تاریخ فاکتور    
        [Route("api/FDocData/FDocH/LastDate/{ace}/{sal}/{group}/{ModeCode}")]
        public async Task<IHttpActionResult> GetWeb_FDocHLastDate(string ace, string sal, string group, string ModeCode)
        {
            string dBName = UnitDatabase.DatabaseName(ace, sal, group);
            var dataAccount = UnitDatabase.ReadUserPassHeader(this.Request.Headers);
            string sql = string.Format(@"EXEC {0}.[dbo].[Web_Doc_Dates]
                                              @TableName = '{1}',
		                                      @Mode = '''{2}'''",
                                         dBName, "fct5doch", ModeCode);

            var DBase = UnitDatabase.dataDB.Where(p => p.UserName.ToUpper() == dataAccount[0].ToUpper() && p.Password == dataAccount[1]).Single();
            string res = UnitDatabase.TestAcount(DBase, dataAccount[3], ace, group, UnitPublic.access_View);
            if (res == "")
            {
                return Ok(DBase.DB.Database.SqlQuery<string>(sql).Single());
            }
            else
                return Ok(res);

            //string conStr = UnitDatabase.CreateConnectionString(dataAccount[0], dataAccount[1], dataAccount[2], dataAccount[3], ace, sal, group, 0, UnitPublic.access_View, UnitPublic.act_View, 0);
        }

        // GET: api/FDocData/FDocB اطلاعات تکمیلی فاکتور    
        [Route("api/FDocData/FDocB/{ace}/{sal}/{group}/{serialNumber}")]
        public async Task<IHttpActionResult> GetWeb_FDocB(string ace, string sal, string group, long serialNumber)
        {
            string dBName = UnitDatabase.DatabaseName(ace, sal, group);
            var dataAccount = UnitDatabase.ReadUserPassHeader(this.Request.Headers);
            string sql = string.Format(@"SELECT SerialNumber,BandNo,KalaCode,KalaName,MainUnit,MainUnitName,Amount1,Amount2,Amount3,UnitPrice,TotalPrice,Discount,Comm,Up_Flag,
                                                    KalaDeghatR1,KalaDeghatR2,KalaDeghatR3,KalaDeghatM1,KalaDeghatM2,KalaDeghatM3,DeghatR,InvSerialNumber,LFctSerialNumber,LinkNumber,
                                                    KalaFileNo,KalaState,KalaExf1,KalaExf2,KalaExf3,KalaExf4,KalaExf5,KalaExf6,KalaExf7,KalaExf8,KalaExf9,KalaExf10,KalaExf11,KalaExf12,KalaExf13,KalaExf14,KalaExf15,
                                                    LinkYear,LinkProg,LinkBandNo,BandSpec,ArzValue,InvCode,InvName
                                             FROM   {0}.dbo.Web_FDocB WHERE SerialNumber = {1}", dBName, serialNumber);
            var DBase = UnitDatabase.dataDB.Where(p => p.UserName.ToUpper() == dataAccount[0].ToUpper() && p.Password == dataAccount[1]).Single();
            string res = UnitDatabase.TestAcount(DBase, dataAccount[3], ace, group, UnitPublic.access_View);
            if (res == "")
            {
                return Ok(DBase.DB.Database.SqlQuery<Web_FDocB>(sql));
            }
            else
                return Ok(res);

            //string conStr = UnitDatabase.CreateConnectionString(dataAccount[0], dataAccount[1], dataAccount[2], dataAccount[3], ace, sal, group, serialNumber, UnitPublic.access_View, UnitPublic.act_View, 0);
        }

        [Route("api/FDocData/UpdatePrice/{ace}/{sal}/{group}/{serialnumber}")]
        [ResponseType(typeof(AFI_FDocBi))]
        public async Task<IHttpActionResult> PostWeb_UpdatePrice(string ace, string sal, string group, long serialnumber)
        {
            string dBName = UnitDatabase.DatabaseName(ace, sal, group);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var dataAccount = UnitDatabase.ReadUserPassHeader(this.Request.Headers);
            var DBase = UnitDatabase.dataDB.Where(p => p.UserName.ToUpper() == dataAccount[0].ToUpper() && p.Password == dataAccount[1]).Single();
            string res = UnitDatabase.TestAcount(DBase, dataAccount[3], ace, group, UnitPublic.access_View);
            if (res == "")
            {
                try
                {
                    string sql = string.Format(CultureInfo.InvariantCulture,
                          @"DECLARE	@return_value int
                            EXEC    @return_value = {0}.[dbo].[Web_FDocB_SetKalaPrice]
		                            @SerialNumber = {1}
                            SELECT  'Return Value' = @return_value",
                          dBName, serialnumber);
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

                string sql1 = string.Format(@"SELECT SerialNumber,BandNo,KalaCode,KalaName,MainUnit,MainUnitName,Amount1,Amount2,Amount3,UnitPrice,TotalPrice,Discount,Comm,Up_Flag,
                                                     KalaDeghatR1,KalaDeghatR2,KalaDeghatR3,KalaDeghatM1,KalaDeghatM2,KalaDeghatM3,DeghatR,InvSerialNumber,LFctSerialNumber,LinkNumber,
                                                     KalaFileNo,KalaState,KalaExf1,KalaExf2,KalaExf3,KalaExf4,KalaExf5,KalaExf6,KalaExf7,KalaExf8,KalaExf9,KalaExf10,KalaExf11,KalaExf12,KalaExf13,KalaExf14,KalaExf15,
                                                     LinkYear,LinkProg,LinkBandNo,BandSpec,ArzValue,InvCode,InvName
                                              FROM   {0}.dbo.Web_FDocB WHERE SerialNumber = {1}", dBName, serialnumber);
                var listFactor = DBase.DB.Database.SqlQuery<Web_FDocB>(sql1);
                return Ok(listFactor);
            }
            else
                return Ok(res);


            //string conStr = UnitDatabase.CreateConnectionString(dataAccount[0], dataAccount[1], dataAccount[2], dataAccount[3], ace, sal, group, serialnumber, UnitPublic.access_View, UnitPublic.act_View, 0);
        }





        [Route("api/FDocData/TestMoveFactor/{ace}/{sal}/{group}/{serialNumber}/{ModeCode}")]
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PostWeb_TestMoveFactor(string ace, string sal, string group, long serialNumber, string ModeCode)
        {
            string dBName = UnitDatabase.DatabaseName(ace, sal, group);
            var dataAccount = UnitDatabase.ReadUserPassHeader(this.Request.Headers);
            var DBase = UnitDatabase.dataDB.Where(p => p.UserName.ToUpper() == dataAccount[0].ToUpper() && p.Password == dataAccount[1]).Single();
            string res = UnitDatabase.TestAcount(DBase, dataAccount[3], ace, group, UnitPublic.access_View);
            if (res == "")
            {
                string sql = string.Format(CultureInfo.InvariantCulture,
                                            @"DECLARE	@retval nvarchar(250)
                                            EXEC	{0}.[dbo].[Web_TestFDoc_Move]
		                                            @serialNumber = {1},
		                                            @MoveToModeCode = '{2}',
		                                            @retval = @retval OUTPUT

                                            SELECT	@retval as N'@retval'",
                                            dBName,
                                            serialNumber,
                                            ModeCode);
                try
                {
                    var result = DBase.DB.Database.SqlQuery<string>(sql).ToList();
                    return Ok(result);
                    // return Ok("");
                }
                catch (Exception e)
                {
                    throw;
                }

            }
            else
                return Ok(res);

            //string conStr = UnitDatabase.CreateConnectionString(dataAccount[0], dataAccount[1], dataAccount[2], dataAccount[3], ace, sal, group, serialNumber, UnitPublic.access_View, UnitPublic.act_Report, 0);
        }



        public class AFI_Move
        {
            public byte? DocNoMode { get; set; }

            public byte? InsertMode { get; set; }

            public string LastModeCode { get; set; }

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

            public double Per { get; set; }

        }



        [Route("api/FDocData/MoveFactor/{ace}/{sal}/{group}")]
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PostWeb_MoveFactor(string ace, string sal, string group, AFI_Move AFI_Move)
        {
            string dBName = UnitDatabase.DatabaseName(ace, sal, group);
            long value = 0;
            string sql = "";
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var dataAccount = UnitDatabase.ReadUserPassHeader(this.Request.Headers);
            var DBase = UnitDatabase.dataDB.Where(p => p.UserName.ToUpper() == dataAccount[0].ToUpper() && p.Password == dataAccount[1]).Single();
            string res = UnitDatabase.TestAcount(DBase, dataAccount[3], ace, group, UnitPublic.ModeCodeConnection(AFI_Move.ModeCode));
            if (res == "")
            {
                try
                {
                    sql = string.Format(CultureInfo.InvariantCulture,
                          @"DECLARE	@return_value int,
		                            @oSerialNumber bigint

                            EXEC	@return_value = {13}.[dbo].[Web_SaveFDoc_Move]
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
                                    @Per = {12} ,
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
                          AFI_Move.MoveMode,
                          AFI_Move.Per,
                          dBName
                          );

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

                sql = string.Format("select * from {0}.dbo.Web_FDocH where  SerialNumber = {1} and ModeCode = '{2}'", dBName, value, AFI_Move.ModeCode);
                var list = DBase.DB.Database.SqlQuery<Web_FDocH>(sql).ToList();
                //var list = DBase.DB.Web_FDocH.Where(c => c.SerialNumber == value && c.ModeCode == AFI_Move.ModeCode);
                if (AFI_Move.MoveMode == 0) // copy
                    UnitDatabase.SaveLog(dataAccount[0], dataAccount[1], dataAccount[2], ace, sal, group, value, AFI_Move.ModeCode, 2, "Y", 1, 0);
                else if (AFI_Move.MoveMode == 1) // move
                {
                    UnitDatabase.SaveLog(dataAccount[0], dataAccount[1], dataAccount[2], ace, sal, group, AFI_Move.SerialNumber ?? 0, AFI_Move.LastModeCode, 8, "Y", 1, 0);
                    UnitDatabase.SaveLog(dataAccount[0], dataAccount[1], dataAccount[2], ace, sal, group, value, AFI_Move.ModeCode, 2, "Y", 1, 0);
                }


                UnitDatabase.SaveLog(dataAccount[0], dataAccount[1], dataAccount[2], ace, sal, group, AFI_Move.SerialNumber ?? 0, UnitPublic.ModeCodeConnection(AFI_Move.ModeCode), UnitPublic.act_New, "Y", 1, 0);

                return Ok(list);
            }
            else
                return Ok(res);


            //string conStr = UnitDatabase.CreateConnectionString(dataAccount[0], dataAccount[1], dataAccount[2], dataAccount[3], ace, sal, group, AFI_Move.SerialNumber ?? 0, UnitPublic.ModeCodeConnection(AFI_Move.ModeCode), UnitPublic.act_New, 0);
        }


        public class AFI_StatusChange
        {
            public byte DMode { get; set; }

            public string UserCode { get; set; }

            public long SerialNumber { get; set; }

            public string Status { get; set; }

            public string ModeCode { get; set; }

        }

        [Route("api/FDocData/ChangeStatus/{ace}/{sal}/{group}")]
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
            string res = UnitDatabase.TestAcount(DBase, dataAccount[3], ace, group, UnitPublic.access_View);
            if (res == "")
            {
                try
                {
                    string sql = string.Format(CultureInfo.InvariantCulture,
                          @"DECLARE	@return_value int
                            EXEC	@return_value = {0}.[dbo].[Web_SaveFDoc_Status]
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

                UnitDatabase.SaveLog(dataAccount[0], dataAccount[1], dataAccount[2], ace, sal, group, AFI_StatusChange.SerialNumber, AFI_StatusChange.ModeCode, 7, "Y", 1, 0);
                return Ok(200);
            }
            else
                return Ok(res);


            //string conStr = UnitDatabase.CreateConnectionString(dataAccount[0], dataAccount[1], dataAccount[2], dataAccount[3], ace, sal, group, AFI_StatusChange.SerialNumber, UnitPublic.access_View, UnitPublic.act_View, 0);
        }


        // GET: api/FDocData/FDocP لیست سند    
        [Route("api/FDocData/FDocP/{ace}/{sal}/{group}/{SerialNumber}/{ModeCode}")]
        public async Task<IHttpActionResult> GetAllWeb_FDocP(string ace, string sal, string group, long SerialNumber, string ModeCode = "")
        {
            string dBName = UnitDatabase.DatabaseName(ace, sal, group);
            var dataAccount = UnitDatabase.ReadUserPassHeader(this.Request.Headers);
            var DBase = UnitDatabase.dataDB.Where(p => p.UserName.ToUpper() == dataAccount[0].ToUpper() && p.Password == dataAccount[1]).Single();
            string res = UnitDatabase.TestAcount(DBase, dataAccount[3], ace, group, UnitPublic.ModeCodeConnection(ModeCode));
            if (res == "")
            {
                string sql = string.Format(@"select * from {0}.dbo.Web_FDocP where SerialNumber = {1} order by BandNo", dBName, SerialNumber);
                var listFDocP = DBase.DB.Database.SqlQuery<Web_FDocP>(sql);
                UnitDatabase.SaveLog(dataAccount[0], dataAccount[1], dataAccount[2], ace, sal, group, SerialNumber, UnitPublic.ModeCodeConnection(ModeCode), UnitPublic.act_Print, "Y", 1, 0);

                return Ok(listFDocP);
            }
            else
                return Ok(res);

            //string conStr = UnitDatabase.CreateConnectionString(dataAccount[0], dataAccount[1], dataAccount[2], dataAccount[3], ace, sal, group, SerialNumber, UnitPublic.ModeCodeConnection(ModeCode), UnitPublic.act_Print, 0);
        }


        public class AFI_TestFDocB
        {
            public long SerialNumber { get; set; }

            public string flagTest { get; set; }
        }


        [Route("api/FDocData/TestFDoc/{ace}/{sal}/{group}")]
        [ResponseType(typeof(TestDocB))]
        public async Task<IHttpActionResult> PostWeb_TestFDoc(string ace, string sal, string group, AFI_TestFDocB AFI_TestFDocB)
        {
            string dBName = UnitDatabase.DatabaseName(ace, sal, group);
            var dataAccount = UnitDatabase.ReadUserPassHeader(this.Request.Headers);
            var DBase = UnitDatabase.dataDB.Where(p => p.UserName.ToUpper() == dataAccount[0].ToUpper() && p.Password == dataAccount[1]).Single();
            string res = UnitDatabase.TestAcount(DBase, dataAccount[3], ace, group, UnitPublic.access_View);
            if (res == "")
            {
                string sql = string.Format(CultureInfo.InvariantCulture,
                                           @"EXEC	{0}.[dbo].[{3}] @serialNumber = {1}  , @UserCode = '{2}' ",
                                           dBName,
                                           AFI_TestFDocB.SerialNumber,
                                           dataAccount[2],
                                           AFI_TestFDocB.flagTest == "Y" ? "Web_TestFDoc_Temp" : "Web_TestFDoc"
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


            //string conStr = UnitDatabase.CreateConnectionString(dataAccount[0], dataAccount[1], dataAccount[2], dataAccount[3], ace, sal, group, AFI_TestFDocB.SerialNumber, UnitPublic.access_View, UnitPublic.act_Report, 0);

        }




        public class AFI_SaveFDoc_HZ
        {
            public long SerialNumber { get; set; }

            public string Tanzim { get; set; }

        }


        [Route("api/FDocData/SaveFDoc_HZ/{ace}/{sal}/{group}")]
        [ResponseType(typeof(TestDocB))]
        public async Task<IHttpActionResult> PostWeb_SaveFDoc_HZ(string ace, string sal, string group, AFI_SaveFDoc_HZ AFI_SaveFDoc_HZ)
        {
            string dBName = UnitDatabase.DatabaseName(ace, sal, group);
            var dataAccount = UnitDatabase.ReadUserPassHeader(this.Request.Headers);
            var DBase = UnitDatabase.dataDB.Where(p => p.UserName.ToUpper() == dataAccount[0].ToUpper() && p.Password == dataAccount[1]).Single();
            string res = UnitDatabase.TestAcount(DBase, dataAccount[3], ace, group, UnitPublic.access_View);
            if (res == "")
            {
                string sql = string.Format(CultureInfo.InvariantCulture,
                            @"DECLARE	@return_value int
                                             EXEC	@return_value = {0}.[dbo].[Web_SaveFDoc_HZ]
		                                            @SerialNumber = {1},
		                                            @Tanzim = '{2}'
                                             SELECT	'Return Value' = @return_value ",
                           dBName, AFI_SaveFDoc_HZ.SerialNumber, AFI_SaveFDoc_HZ.Tanzim);
                var result = DBase.DB.Database.SqlQuery<int>(sql).ToList();
                return Ok("ok");
            }
            else
                return Ok(res);


            //string conStr = UnitDatabase.CreateConnectionString(dataAccount[0], dataAccount[1], dataAccount[2], dataAccount[3], ace, sal, group, 0, UnitPublic.access_View, UnitPublic.act_View, 0);
        }



        public class TestFDoc_DeleteObject
        {
            public long SerialNumber { get; set; }

        }

        public class TestFDoc_Delete
        {
            public int id { get; set; }

            public byte Test { get; set; }

            public string TestName { get; set; }

            public string TestCap { get; set; }

            public int BandNo { get; set; }

        }



        [Route("api/FDocData/TestFDoc_Delete/{ace}/{sal}/{group}")]
        [ResponseType(typeof(TestFDoc_Delete))]
        public async Task<IHttpActionResult> PostWeb_TestFDoc_Delete(string ace, string sal, string group, TestFDoc_DeleteObject TestFDoc_DeleteObject)
        {
            string dBName = UnitDatabase.DatabaseName(ace, sal, group);
            var dataAccount = UnitDatabase.ReadUserPassHeader(this.Request.Headers);
            var DBase = UnitDatabase.dataDB.Where(p => p.UserName.ToUpper() == dataAccount[0].ToUpper() && p.Password == dataAccount[1]).Single();
            string res = UnitDatabase.TestAcount(DBase, dataAccount[3], ace, group, UnitPublic.access_View);
            if (res == "")
            {
                string sql = string.Format(CultureInfo.InvariantCulture,
                              @"EXEC	{0}.[dbo].[Web_TestFDoc_Delete] @serialNumber = {1}, @UserCode = '{2}' ", dBName, TestFDoc_DeleteObject.SerialNumber, dataAccount[2]);
                try
                {
                    var result = DBase.DB.Database.SqlQuery<TestFDoc_Delete>(sql).ToList();
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

        public class AFI_TestFDoc_New
        {
            public string DocDate { get; set; }

            public string ModeCode { get; set; }

            public string DocNo { get; set; }

            public long SerialNumber { get; set; }
        }


        [Route("api/FDocData/TestFDoc_New/{ace}/{sal}/{group}")]
        [ResponseType(typeof(TestDocB))]
        public async Task<IHttpActionResult> PostWeb_TestFDoc_New(string ace, string sal, string group, AFI_TestFDoc_New AFI_TestFDoc_New)
        {
            string dBName = UnitDatabase.DatabaseName(ace, sal, group);
            var dataAccount = UnitDatabase.ReadUserPassHeader(this.Request.Headers);
            var DBase = UnitDatabase.dataDB.Where(p => p.UserName.ToUpper() == dataAccount[0].ToUpper() && p.Password == dataAccount[1]).Single();
            string res = UnitDatabase.TestAcount(DBase, dataAccount[3], ace, group, UnitPublic.access_View);
            if (res == "")
            {
                string sql = string.Format(CultureInfo.InvariantCulture,
                                     @"EXEC	{0}.[dbo].[Web_TestFDoc_New] @UserCode = '{1}',  @DocDate = '{2}', @ModeCode = '{3}' , @DocNo = '{4}' , @SerialNumber = {5}",
                                    dBName, dataAccount[2], AFI_TestFDoc_New.DocDate, AFI_TestFDoc_New.ModeCode, AFI_TestFDoc_New.DocNo, AFI_TestFDoc_New.SerialNumber);
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


        public class AFI_TestFDoc_Edit
        {
            public long Serialnumber { get; set; }

        }

        [Route("api/FDocData/TestFDoc_Edit/{ace}/{sal}/{group}")]
        [ResponseType(typeof(TestDocB))]
        public async Task<IHttpActionResult> PostWeb_TestFDoc_Edit(string ace, string sal, string group, AFI_TestFDoc_Edit AFI_TestFDoc_Edit)
        {
            string dBName = UnitDatabase.DatabaseName(ace, sal, group);
            var dataAccount = UnitDatabase.ReadUserPassHeader(this.Request.Headers);
            var DBase = UnitDatabase.dataDB.Where(p => p.UserName.ToUpper() == dataAccount[0].ToUpper() && p.Password == dataAccount[1]).Single();
            string res = UnitDatabase.TestAcount(DBase, dataAccount[3], ace, group, UnitPublic.access_View);
            if (res == "")
            {
                string sql = string.Format(CultureInfo.InvariantCulture,
                        @"EXEC	{0}.[dbo].[Web_TestFDoc_Edit] @UserCode = '{1}',  @Serialnumber = '{2}'",
                       dBName,
                       dataAccount[2],
                       AFI_TestFDoc_Edit.Serialnumber);
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



        /* [DllImport("Fct6_Web.dll", CharSet = CharSet.Unicode)]
         public static extern bool GetVer(StringBuilder RetVal);

         [Route("api/FDocData/GetVerDllFct6")]
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


    }
}
