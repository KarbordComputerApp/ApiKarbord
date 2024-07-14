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
using ApiKarbord.Controllers.Unit;
using ApiKarbord.Models;

namespace ApiKarbord.Controllers.AFI.data
{
    public class AFI_FDocBiController : ApiController
    {
        // private ApiModel db = new ApiModel();

        // GET: api/AFI_FDocBi

        // PUT: api/AFI_FDocBi/5
        [Route("api/AFI_FDocBi/{ace}/{sal}/{group}/{BandNo}")]
        [ResponseType(typeof(AFI_FDocBi))]
        public async Task<IHttpActionResult> PutAFI_FDocBi(string ace, string sal, string group, long BandNo, AFI_FDocBi aFI_FDocBi)
        {
            string dBName = UnitDatabase.DatabaseName(ace, sal, group);
            string value;

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
                          @"DECLARE	@return_value int, @outputSt nvarchar(1000) = ''
                            EXEC	@return_value = {37}.[dbo].[Web_SaveFDoc_BU]
		                            @SerialNumber = {0},
		                            @BandNo = {1},
		                            @KalaCode = N'{2}',
		                            @Amount1 = {3},
		                            @Amount2 = {4},
		                            @Amount3 = {5},
		                            @UnitPrice = {6},
		                            @TotalPrice = {7},
                                    @Discount = {8},
		                            @MainUnit = {9},
		                            @Comm = N'{10}',
                                    @Up_Flag = {11},
                                    @OprCode = N'{12}',
		                            @MkzCode = N'{13}',
		                            @InvCode = N'{14}',
		                            @BandSpec = N'{15}',
                                    @ArzCode = N'{16}',
                                    @ArzRate = {17},
                                    @ArzValue = {18},
                                    @MjdControl = {19},
                                    @KalaFileNo = N'{20}',
                                    @KalaState = N'{21}',
                                    @KalaExf1 = N'{22}',
                                    @KalaExf2 = N'{23}',
                                    @KalaExf3 = N'{24}',
                                    @KalaExf4 = N'{25}',
                                    @KalaExf5 = N'{26}',
                                    @KalaExf6 = N'{27}',
                                    @KalaExf7 = N'{28}',
                                    @KalaExf8 = N'{29}',
                                    @KalaExf9 = N'{30}',
                                    @KalaExf10 = N'{31}',
                                    @KalaExf11 = N'{32}',
                                    @KalaExf12 = N'{33}',
                                    @KalaExf13 = N'{34}',
                                    @KalaExf14 = N'{35}',
                                    @KalaExf15 = N'{36}',
                                    @outputSt = @outputSt OUTPUT
                             SELECT	@outputSt as outputSt",
                        aFI_FDocBi.SerialNumber,
                        aFI_FDocBi.BandNo,
                        aFI_FDocBi.KalaCode,
                        aFI_FDocBi.Amount1 ?? 0,
                        aFI_FDocBi.Amount2 ?? 0,
                        aFI_FDocBi.Amount3 ?? 0,
                        aFI_FDocBi.UnitPrice ?? 0,
                        aFI_FDocBi.TotalPrice ?? 0,
                        aFI_FDocBi.Discount ?? 0,
                        aFI_FDocBi.MainUnit ?? 1,
                        UnitPublic.ConvertTextWebToWin(aFI_FDocBi.Comm ?? ""),
                        aFI_FDocBi.Up_Flag,
                        aFI_FDocBi.OprCode,
                        aFI_FDocBi.MkzCode,
                        aFI_FDocBi.InvCode ?? "",
                        UnitPublic.ConvertTextWebToWin(aFI_FDocBi.BandSpec ?? ""),
                        aFI_FDocBi.ArzCode ?? "",
                        aFI_FDocBi.ArzRate ?? 0,
                        aFI_FDocBi.ArzValue ?? 0,
                        aFI_FDocBi.MjdControl ?? 0,
                        aFI_FDocBi.KalaFileNo,
                        aFI_FDocBi.KalaState,
                        aFI_FDocBi.KalaExf1,
                        aFI_FDocBi.KalaExf2,
                        aFI_FDocBi.KalaExf3,
                        aFI_FDocBi.KalaExf4,
                        aFI_FDocBi.KalaExf5,
                        aFI_FDocBi.KalaExf6,
                        aFI_FDocBi.KalaExf7,
                        aFI_FDocBi.KalaExf8,
                        aFI_FDocBi.KalaExf9,
                        aFI_FDocBi.KalaExf10,
                        aFI_FDocBi.KalaExf11,
                        aFI_FDocBi.KalaExf12,
                        aFI_FDocBi.KalaExf13,
                        aFI_FDocBi.KalaExf14,
                        aFI_FDocBi.KalaExf15,
                        dBName
                        );
                    value = DBase.DB.Database.SqlQuery<string>(sql).Single();
                    if (value == "")
                    {
                        await DBase.DB.SaveChangesAsync();
                    }
                }
                catch (Exception)
                {
                    throw;
                }

                UnitDatabase.SaveLog(dataAccount[0], dataAccount[1], dataAccount[2], ace, sal, group, aFI_FDocBi.SerialNumber, UnitPublic.ModeCodeConnection(aFI_FDocBi.ModeCode), 1, aFI_FDocBi.flagLog, 1, 0);

                if ((aFI_FDocBi.MjdControl ?? 0) == 0)
                {
                    string sql1 = string.Format(@"SELECT SerialNumber,BandNo,KalaCode,KalaName,MainUnit,MainUnitName,Amount1,Amount2,Amount3,UnitPrice,TotalPrice,Discount,Comm,Up_Flag,
                                                         KalaDeghatR1,KalaDeghatR2,KalaDeghatR3,KalaDeghatM1,KalaDeghatM2,KalaDeghatM3,DeghatR,InvSerialNumber,LFctSerialNumber,LinkNumber,
                                                         KalaFileNo,KalaState,KalaExf1,KalaExf2,KalaExf3,KalaExf4,KalaExf5,KalaExf6,KalaExf7,KalaExf8,KalaExf9,KalaExf10,KalaExf11,KalaExf12,KalaExf13,KalaExf14,KalaExf15,
                                                         LinkYear,LinkProg,BandSpec,ArzValue
                                                  FROM   {0}.dbo.Web_FDocB WHERE SerialNumber = {1}",
                                                 dBName, aFI_FDocBi.SerialNumber);
                    var list = DBase.DB.Database.SqlQuery<Web_FDocB>(sql1);
                    return Ok(list);
                }
                else
                {
                    return Ok(value);
                }
            }
            else
                return Ok(res);


            //string conStr = UnitDatabase.CreateConnectionString(dataAccount[0], dataAccount[1], dataAccount[2],dataAccount[3], ace, sal, group, aFI_FDocBi.SerialNumber, UnitPublic.ModeCodeConnection(aFI_FDocBi.ModeCode), UnitPublic.act_EditBand, aFI_FDocBi.BandNo ?? 0);
        }

        // POST: api/AFI_FDocBi
        [Route("api/AFI_FDocBi/{ace}/{sal}/{group}/{bandno}")]
        [ResponseType(typeof(AFI_FDocBi))]
        public async Task<IHttpActionResult> PostAFI_FDocBi(string ace, string sal, string group, long bandNo, AFI_FDocBi aFI_FDocBi)
        {
            string dBName = UnitDatabase.DatabaseName(ace, sal, group);
            string value;
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
                    string fieldBandNo;
                    string tableName;
                    if (ace == UnitPublic.Web1)
                    {
                        fieldBandNo = "BandNo";
                        tableName = "Afi1FDocB";
                    }
                    else
                    {
                        fieldBandNo = "Radif";
                        tableName = "fct5docb";
                    }


                    if (bandNo > 0)
                    {
                        string sqlUpdateBand = string.Format(@"DECLARE	@return_value int
                                                           EXEC	@return_value = {0}.[dbo].[Web_Doc_BShift]
	                                                            @TableName = '{1}',
                                                                @SerialNumber = {2},
                                                                @BandNo = {3},
                                                                @BandNoFld = '{4}'
                                                           SELECT	'Return Value' = @return_value",
                                                           dBName,
                                                           tableName,
                                                           aFI_FDocBi.SerialNumber,
                                                           bandNo,
                                                           fieldBandNo);
                        int valueUpdateBand = DBase.DB.Database.SqlQuery<int>(sqlUpdateBand).Single();
                    }
                    string sql = string.Format(CultureInfo.InvariantCulture,
                          @"DECLARE	@return_value int , @outputSt nvarchar(1000) = ''
                            EXEC	@return_value = {37}.[dbo].[Web_SaveFDoc_BI]
		                            @SerialNumber = {0},
		                            @BandNo = {1},
		                            @KalaCode = N'{2}',
		                            @Amount1 = {3},
		                            @Amount2 = {4},
		                            @Amount3 = {5},
		                            @UnitPrice = {6},
		                            @TotalPrice = {7},
                                    @Discount = {8},
		                            @MainUnit = {9},
		                            @Comm = N'{10}',
                                    @Up_Flag = {11},
                                    @OprCode = N'{12}',
		                            @MkzCode = N'{13}',
		                            @InvCode = N'{14}',
		                            @BandSpec = N'{15}',
                                    @ArzCode = N'{16}',
                                    @ArzRate = {17},
                                    @ArzValue = {18},
                                    @MjdControl = {19},
                                    @KalaFileNo = N'{20}',
                                    @KalaState = N'{21}',
                                    @KalaExf1 = N'{22}',
                                    @KalaExf2 = N'{23}',
                                    @KalaExf3 = N'{24}',
                                    @KalaExf4 = N'{25}',
                                    @KalaExf5 = N'{26}',
                                    @KalaExf6 = N'{27}',
                                    @KalaExf7 = N'{28}',
                                    @KalaExf8 = N'{29}',
                                    @KalaExf9 = N'{30}',
                                    @KalaExf10 = N'{31}',
                                    @KalaExf11 = N'{32}',
                                    @KalaExf12 = N'{33}',
                                    @KalaExf13 = N'{34}',
                                    @KalaExf14 = N'{35}',
                                    @KalaExf15 = N'{36}',
                                    @outputSt = @outputSt OUTPUT
                             SELECT	@outputSt as outputSt",
                        aFI_FDocBi.SerialNumber,
                        bandNo == 0 ? aFI_FDocBi.BandNo.ToString() : bandNo.ToString(),
                        aFI_FDocBi.KalaCode,
                        aFI_FDocBi.Amount1 ?? 0,
                        aFI_FDocBi.Amount2 ?? 0,
                        aFI_FDocBi.Amount3 ?? 0,
                        aFI_FDocBi.UnitPrice ?? 0,
                        aFI_FDocBi.TotalPrice ?? 0,
                        aFI_FDocBi.Discount ?? 0,
                        aFI_FDocBi.MainUnit ?? 1,
                        UnitPublic.ConvertTextWebToWin(aFI_FDocBi.Comm ?? ""),
                        aFI_FDocBi.Up_Flag,
                        aFI_FDocBi.OprCode,
                        aFI_FDocBi.MkzCode,
                        aFI_FDocBi.InvCode,
                        UnitPublic.ConvertTextWebToWin(aFI_FDocBi.BandSpec ?? ""),
                        aFI_FDocBi.ArzCode ?? "",
                        aFI_FDocBi.ArzRate ?? 0,
                        aFI_FDocBi.ArzValue ?? 0,
                        aFI_FDocBi.MjdControl ?? 0,
                         aFI_FDocBi.KalaFileNo,
                        aFI_FDocBi.KalaState,
                        aFI_FDocBi.KalaExf1,
                        aFI_FDocBi.KalaExf2,
                        aFI_FDocBi.KalaExf3,
                        aFI_FDocBi.KalaExf4,
                        aFI_FDocBi.KalaExf5,
                        aFI_FDocBi.KalaExf6,
                        aFI_FDocBi.KalaExf7,
                        aFI_FDocBi.KalaExf8,
                        aFI_FDocBi.KalaExf9,
                        aFI_FDocBi.KalaExf10,
                        aFI_FDocBi.KalaExf11,
                        aFI_FDocBi.KalaExf12,
                        aFI_FDocBi.KalaExf13,
                        aFI_FDocBi.KalaExf14,
                        aFI_FDocBi.KalaExf15,
                        dBName);
                    value = DBase.DB.Database.SqlQuery<string>(sql).Single();
                    if (value == "")
                    {
                        await DBase.DB.SaveChangesAsync();
                    }
                }
                catch (Exception e)
                {
                    throw;
                }

                UnitDatabase.SaveLog(dataAccount[0], dataAccount[1], dataAccount[2], ace, sal, group, aFI_FDocBi.SerialNumber, UnitPublic.ModeCodeConnection(aFI_FDocBi.ModeCode), 1, aFI_FDocBi.flagLog, 1, 0);

                if ((aFI_FDocBi.MjdControl ?? 0) == 0)
                {
                    string sql1 = string.Format(@"SELECT SerialNumber,BandNo,KalaCode,KalaName,MainUnit,MainUnitName,Amount1,Amount2,Amount3,UnitPrice,TotalPrice,Discount,Comm,Up_Flag,
                                                         KalaDeghatR1,KalaDeghatR2,KalaDeghatR3,KalaDeghatM1,KalaDeghatM2,KalaDeghatM3,DeghatR,InvSerialNumber,LFctSerialNumber,LinkNumber,
                                                         KalaFileNo,KalaState,KalaExf1,KalaExf2,KalaExf3,KalaExf4,KalaExf5,KalaExf6,KalaExf7,KalaExf8,KalaExf9,KalaExf10,KalaExf11,KalaExf12,KalaExf13,KalaExf14,KalaExf15,
                                                         LinkYear,LinkProg,BandSpec,ArzValue
                                                  FROM   {0}.dbo.Web_FDocB WHERE SerialNumber = {1}",
                                                  dBName, aFI_FDocBi.SerialNumber);
                    var list = DBase.DB.Database.SqlQuery<Web_FDocB>(sql1);
                    return Ok(list);
                }
                else
                {
                    return Ok(value);
                }

            }
            else
                return Ok(res);
            //  string conStr = UnitDatabase.CreateConnectionString(dataAccount[0], dataAccount[1], dataAccount[2], dataAccount[3], ace, sal, group, aFI_FDocBi.SerialNumber, UnitPublic.ModeCodeConnection(aFI_FDocBi.ModeCode), UnitPublic.act_NewBand, bandNo == 0 ? aFI_FDocBi.BandNo ?? 0 : Convert.ToInt32(bandNo));


        }

        // DELETE: api/AFI_FDocBi/5
        [Route("api/AFI_FDocBi/{ace}/{sal}/{group}/{SerialNumber}/{BandNo}/{ModeCode}/{FlagLog}")]
        [ResponseType(typeof(AFI_FDocBi))]
        public async Task<IHttpActionResult> DeleteAFI_FDocBi(string ace, string sal, string group, long SerialNumber, int BandNo, string ModeCode, string FlagLog)
        {
            string dBName = UnitDatabase.DatabaseName(ace, sal, group);
            var dataAccount = UnitDatabase.ReadUserPassHeader(this.Request.Headers);
            var DBase = UnitDatabase.dataDB.Where(p => p.UserName == dataAccount[0] && p.Password == dataAccount[1]).Single();
            string res = UnitDatabase.TestAcount(DBase, dataAccount[3], ace, group, UnitPublic.access_View);
            if (res == "")
            {
                try
                {
                    // Fct5: 'Fct5DocB'   'Radif'
                    // Inv5: 'Inv5DocB'   'Radif'
                    // Afi1: 'Afi1FDocB'  'BandNo'


                    //var list = db.AFI_FDocBi.First(c=>c.SerialNumber == SerialNumber && c.BandNo==BandNo);
                    //if (list == null)
                    //{
                    //    return NotFound();
                    //}
                    string sql = string.Format(@"DECLARE	@return_value int
                                                 EXEC	@return_value = {0}.[dbo].[Web_SaveFDoc_BD]
		                                                @SerialNumber = {1},
		                                                @BandNo = {2}
                                                 SELECT	'Return Value' = @return_value",
                                                dBName,
                                                 SerialNumber,
                                                BandNo);

                    int value = DBase.DB.Database.SqlQuery<int>(sql).Single();
                    if (value == 0)
                    {
                        await DBase.DB.SaveChangesAsync();
                    }
                    if (BandNo > 0)
                    {
                        string sqlUpdateBand = string.Format(@"DECLARE	@return_value int
                                                           EXEC	@return_value = {0}.[dbo].[Web_Doc_BOrder]
	                                                            @TableName = '{1}',
                                                                @SerialNumber = {2},
                                                                @BandNoFld = '{3}'
                                                           SELECT	'Return Value' = @return_value",
                                                           dBName,
                                                           ace == UnitPublic.Web1 ? "Afi1FDocB" : "Fct5DocB",
                                                           SerialNumber,
                                                           ace == UnitPublic.Web1 ? "BandNo" : "Radif");
                        int valueUpdateBand = DBase.DB.Database.SqlQuery<int>(sqlUpdateBand).Single();
                        //await db.SaveChangesAsync();
                    }
                }
                catch (Exception e)
                {
                    throw;
                }
                string sql1 = string.Format(@"SELECT SerialNumber,BandNo,KalaCode,KalaName,MainUnit,MainUnitName,Amount1,Amount2,Amount3,UnitPrice,TotalPrice,Discount,Comm,Up_Flag,
                                                     KalaDeghatR1,KalaDeghatR2,KalaDeghatR3,KalaDeghatM1,KalaDeghatM2,KalaDeghatM3,DeghatR,InvSerialNumber,LFctSerialNumber,LinkNumber,
                                                     KalaFileNo,KalaState,KalaExf1,KalaExf2,KalaExf3,KalaExf4,KalaExf5,KalaExf6,KalaExf7,KalaExf8,KalaExf9,KalaExf10,KalaExf11,KalaExf12,KalaExf13,KalaExf14,KalaExf15,
                                                     LinkYear,LinkProg,BandSpec,ArzValue     
                                              FROM   {0}.dbo.Web_FDocB WHERE SerialNumber = {1}", dBName, SerialNumber.ToString());
                var listFactor = DBase.DB.Database.SqlQuery<Web_FDocB>(sql1);
                UnitDatabase.SaveLog(dataAccount[0], dataAccount[1], dataAccount[2], ace, sal, group, SerialNumber, ModeCode, 1, FlagLog, 1, 0);
                return Ok(listFactor);
            }
            else
                return Ok(res);
            //string conStr = UnitDatabase.CreateConnectionString(dataAccount[0], dataAccount[1], dataAccount[2], dataAccount[3], ace, sal, group, SerialNumber, ModeCode, UnitPublic.act_DeleteBand, BandNo);

        }


        // POST: api/AFI_FDocBi
        [Route("api/AFI_FDocBi/SaveAllDocB/{ace}/{sal}/{group}/{serialNumber}")]
        [ResponseType(typeof(AFI_FDocBi))]
        public async Task<IHttpActionResult> PostAFI_SaveAllDocB(string ace, string sal, string group, long serialNumber, [FromBody]List<AFI_FDocBi> AFI_FDocBi)
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
                int value;
                int i = 0;
                try
                {
                    foreach (var item in AFI_FDocBi)
                    {
                        i++;
                        string sql = string.Format(CultureInfo.InvariantCulture,


                          @"DECLARE	@return_value int 
                            EXEC	@return_value = {41}.[dbo].[Web_SaveFDoc_BI_Temp]
		                            @SerialNumber = {0},
		                            @BandNo = {1},
		                            @KalaCode = N'{2}',
		                            @Amount1 = {3},
		                            @Amount2 = {4},
		                            @Amount3 = {5},
		                            @UnitPrice = {6},
		                            @TotalPrice = {7},
                                    @Discount = {8},
		                            @MainUnit = {9},
		                            @Comm = N'{10}',
                                    @Up_Flag = {11},
                                    @OprCode = N'{12}',
		                            @MkzCode = N'{13}',
		                            @InvCode = N'{14}',
                                    @LFctSerialNumber = {15},
                                    @InvSerialNumber = {16},
                                    @LinkNumber = {17},
                                    @LinkYear = {18},
                                    @LinkProg = N'{19}',
                                    @BandSpec = N'{20}',
                                    @ArzCode = N'{21}',
                                    @ArzRate = {22},
                                    @ArzValue = {23},
                                    @KalaFileNo = N'{24}',
                                    @KalaState = N'{25}',
                                    @KalaExf1 = N'{26}',
                                    @KalaExf2 = N'{27}',
                                    @KalaExf3 = N'{28}',
                                    @KalaExf4 = N'{29}',
                                    @KalaExf5 = N'{30}',
                                    @KalaExf6 = N'{31}',
                                    @KalaExf7 = N'{32}',
                                    @KalaExf8 = N'{33}',
                                    @KalaExf9 = N'{34}',
                                    @KalaExf10 = N'{35}',
                                    @KalaExf11 = N'{36}',
                                    @KalaExf12 = N'{37}',
                                    @KalaExf13 = N'{38}',
                                    @KalaExf14 = N'{39}',
                                    @KalaExf15 = N'{40}'
                            SELECT	'Return Value' = @return_value
                            ",
                        serialNumber,
                        i,
                        item.KalaCode,
                        item.Amount1 ?? 0,
                        item.Amount2 ?? 0,
                        item.Amount3 ?? 0,
                        item.UnitPrice ?? 0,
                        item.TotalPrice ?? 0,
                        item.Discount ?? 0,
                        item.MainUnit ?? 1,
                        UnitPublic.ConvertTextWebToWin(item.Comm ?? ""),
                        item.Up_Flag,
                        item.OprCode,
                        item.MkzCode,
                        item.InvCode,
                        item.LFctSerialNumber ?? 0,
                        item.InvSerialNumber ?? 0,
                        item.LinkNumber ?? 0,
                        item.LinkYear ?? 0,
                        item.LinkProg,
                        UnitPublic.ConvertTextWebToWin(item.BandSpec ?? ""),
                        item.ArzCode ?? "",
                        item.ArzRate ?? 0,
                        item.ArzValue ?? 0,
                        item.KalaFileNo,
                        item.KalaState,
                        item.KalaExf1,
                        item.KalaExf2,
                        item.KalaExf3,
                        item.KalaExf4,
                        item.KalaExf5,
                        item.KalaExf6,
                        item.KalaExf7,
                        item.KalaExf8,
                        item.KalaExf9,
                        item.KalaExf10,
                        item.KalaExf11,
                        item.KalaExf12,
                        item.KalaExf13,
                        item.KalaExf14,
                        item.KalaExf15,
                        dBName
                        //item.flagTest == "Y" ? "Web_SaveFDoc_BI_Temp" : "Web_SaveFDoc_BI"
                        );
                        value = DBase.DB.Database.SqlQuery<int>(sql).Single();
                    }
                    await DBase.DB.SaveChangesAsync();

                }
                catch (Exception)
                {
                    throw;
                }
                UnitDatabase.SaveLog(dataAccount[0], dataAccount[1], dataAccount[2], ace, sal, group, serialNumber, "FDoc", 1, "Y", 1, 0);
                return Ok("OK");
            }
            else
                return Ok(res);

            //string conStr = UnitDatabase.CreateConnectionString(dataAccount[0], dataAccount[1], dataAccount[2], dataAccount[3], ace, sal, group, serialNumber, AFI_FDocBi[0].ModeCode, UnitPublic.act_NewBand, 0);
        }




        public class ConvertObject
        {
            public string ModeCode { get; set; }

            public long SerialNumber { get; set; }

            public long TempSerialNumber { get; set; }

        }



        // POST: api/AFI_FDocBi
        [Route("api/AFI_FDocBi/Convert/{ace}/{sal}/{group}")]
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PostAFI_Convert(string ace, string sal, string group, ConvertObject ConvertObject)
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
                                    EXEC	@return_value = {0}.[dbo].[Web_SaveFDocB_Convert]
		                                    @SerialNumber = {1},
		                                    @TempSerialNumber = {2}
                                    SELECT	'Return Value' = @return_value",
                                  dBName,
                                  ConvertObject.SerialNumber,
                                  ConvertObject.TempSerialNumber);
                    int value = DBase.DB.Database.SqlQuery<int>(sql).Single();

                    await DBase.DB.SaveChangesAsync();

                }
                catch (Exception)
                {
                    throw;
                }
                UnitDatabase.SaveLog(dataAccount[0], dataAccount[1], dataAccount[2], ace, sal, group, ConvertObject.SerialNumber, "FDoc", 1, "Y", 1, 0);
                return Ok("OK");
            }
            else
                return Ok(res);

            //string conStr = UnitDatabase.CreateConnectionString(dataAccount[0], dataAccount[1], dataAccount[2], dataAccount[3], ace, sal, group, ConvertObject.SerialNumber, ConvertObject.ModeCode, UnitPublic.act_NewBand, 0);
        }

    }
}