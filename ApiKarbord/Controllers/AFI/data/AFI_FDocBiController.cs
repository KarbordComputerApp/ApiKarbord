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
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var dataAccount = UnitDatabase.ReadUserPassHeader(this.Request.Headers);
            string conStr = UnitDatabase.CreateConnectionString(dataAccount[0], dataAccount[1], dataAccount[2],dataAccount[3], ace, sal, group, aFI_FDocBi.SerialNumber, aFI_FDocBi.ModeCode, 4, aFI_FDocBi.BandNo ?? 0);
            if (conStr.Length > 100)
            {
                ApiModel db = new ApiModel(conStr);
                try
                {
                    string sql = string.Format(CultureInfo.InvariantCulture,
                          @"DECLARE	@return_value int
                            EXEC	@return_value = [dbo].[Web_SaveFDoc_BU]
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
                                    @ArzValue = {18}
                            SELECT	'Return Value' = @return_value
                            ",
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
                        aFI_FDocBi.ArzValue ?? 0
                        );
                    int value = db.Database.SqlQuery<int>(sql).Single();
                    if (value == 0)
                    {
                        await db.SaveChangesAsync();
                    }
                }
                catch (Exception)
                {
                    throw;
                }
                string sql1 = string.Format(@"SELECT SerialNumber,BandNo,KalaCode,KalaName,MainUnit,MainUnitName,Amount1,Amount2,Amount3,UnitPrice,TotalPrice,Discount,Comm,Up_Flag,KalaDeghatR1,KalaDeghatR2,KalaDeghatR3,KalaDeghatM1,KalaDeghatM2,KalaDeghatM3,DeghatR,InvSerialNumber,LFctSerialNumber,LinkNumber,LinkYear,LinkProg,BandSpec,ArzValue
                                          FROM Web_FDocB WHERE SerialNumber = {0}", aFI_FDocBi.SerialNumber);
                var listFactor = db.Database.SqlQuery<Web_FDocB>(sql1);
                UnitDatabase.SaveLog(dataAccount[0], dataAccount[1], dataAccount[2], ace, sal, group, aFI_FDocBi.SerialNumber, aFI_FDocBi.ModeCode, 1, aFI_FDocBi.flagLog, 0);
                return Ok(listFactor);
            }
            return Ok(conStr);
        }

        // POST: api/AFI_FDocBi
        [Route("api/AFI_FDocBi/{ace}/{sal}/{group}/{bandno}")]
        [ResponseType(typeof(AFI_FDocBi))]
        public async Task<IHttpActionResult> PostAFI_FDocBi(string ace, string sal, string group, long bandNo, AFI_FDocBi aFI_FDocBi)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var dataAccount = UnitDatabase.ReadUserPassHeader(this.Request.Headers);
            string conStr = UnitDatabase.CreateConnectionString(dataAccount[0], dataAccount[1], dataAccount[2],dataAccount[3], ace, sal, group, aFI_FDocBi.SerialNumber, aFI_FDocBi.ModeCode, 5, bandNo == 0 ? aFI_FDocBi.BandNo ?? 0 : Convert.ToInt32(bandNo));
            if (conStr.Length > 100)
            {
                ApiModel db = new ApiModel(conStr);
                try
                {
                    string fieldBandNo;
                    string tableName;
                    if (ace == "Web1")
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
                                                           EXEC	@return_value = [dbo].[Web_Doc_BShift]
	                                                            @TableName = '{0}',
                                                                @SerialNumber = {1},
                                                                @BandNo = {2},
                                                                @BandNoFld = '{3}'
                                                           SELECT	'Return Value' = @return_value",
                                                           tableName,
                                                           aFI_FDocBi.SerialNumber,
                                                           bandNo,
                                                           fieldBandNo);
                        int valueUpdateBand = db.Database.SqlQuery<int>(sqlUpdateBand).Single();
                    }
                    string sql = string.Format(CultureInfo.InvariantCulture,
                          @"DECLARE	@return_value int
                            EXEC	@return_value = [dbo].[Web_SaveFDoc_BI]
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
                                    @ArzValue = {18}
                            SELECT	'Return Value' = @return_value
                            ",
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
                        aFI_FDocBi.ArzValue ?? 0
                        );
                    int value = db.Database.SqlQuery<int>(sql).Single();
                    if (value == 0)
                    {
                        await db.SaveChangesAsync();
                    }
                }
                catch (Exception e)
                {
                    throw;
                }
                string sql1 = string.Format(@"SELECT SerialNumber,BandNo,KalaCode,KalaName,MainUnit,MainUnitName,Amount1,Amount2,Amount3,UnitPrice,TotalPrice,Discount,Comm,Up_Flag,KalaDeghatR1,KalaDeghatR2,KalaDeghatR3,KalaDeghatM1,KalaDeghatM2,KalaDeghatM3,DeghatR,InvSerialNumber,LFctSerialNumber,LinkNumber,LinkYear,LinkProg,BandSpec,ArzValue
                                         FROM Web_FDocB WHERE SerialNumber = {0}", aFI_FDocBi.SerialNumber);
                var listFactor = db.Database.SqlQuery<Web_FDocB>(sql1);
                UnitDatabase.SaveLog(dataAccount[0], dataAccount[1], dataAccount[2], ace, sal, group, aFI_FDocBi.SerialNumber, aFI_FDocBi.ModeCode, 1, aFI_FDocBi.flagLog, 0);
                return Ok(listFactor);
            }
            return Ok(conStr);

        }

        // DELETE: api/AFI_FDocBi/5
        [Route("api/AFI_FDocBi/{ace}/{sal}/{group}/{SerialNumber}/{BandNo}/{ModeCode}/{FlagLog}")]
        [ResponseType(typeof(AFI_FDocBi))]
        public async Task<IHttpActionResult> DeleteAFI_FDocBi(string ace, string sal, string group, long SerialNumber, int BandNo, string ModeCode, string FlagLog)
        {
            var dataAccount = UnitDatabase.ReadUserPassHeader(this.Request.Headers);
            string conStr = UnitDatabase.CreateConnectionString(dataAccount[0], dataAccount[1], dataAccount[2],dataAccount[3], ace, sal, group, SerialNumber, ModeCode, 6, BandNo);
            if (conStr.Length > 100)
            {
                ApiModel db = new ApiModel(conStr);
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
                                                 EXEC	@return_value = [dbo].[Web_SaveFDoc_BD]
		                                                @SerialNumber = {0},
		                                                @BandNo = {1}
                                                 SELECT	'Return Value' = @return_value",
                                                SerialNumber,
                                                BandNo);

                    int value = db.Database.SqlQuery<int>(sql).Single();
                    if (value == 0)
                    {
                        await db.SaveChangesAsync();
                    }
                    if (BandNo > 0)
                    {
                        string sqlUpdateBand = string.Format(@"DECLARE	@return_value int
                                                           EXEC	@return_value = [dbo].[Web_Doc_BOrder]
	                                                            @TableName = '{0}',
                                                                @SerialNumber = {1},
                                                                @BandNoFld = '{2}'
                                                           SELECT	'Return Value' = @return_value",
                                                           ace == "Web1" ? "Afi1FDocB" : "Fct5DocB",
                                                           SerialNumber,
                                                           ace == "Web1" ? "BandNo" : "Radif");
                        int valueUpdateBand = db.Database.SqlQuery<int>(sqlUpdateBand).Single();
                        //await db.SaveChangesAsync();
                    }
                }
                catch (Exception e)
                {
                    throw;
                }
                string sql1 = string.Format(@"SELECT SerialNumber,BandNo,KalaCode,KalaName,MainUnit,MainUnitName,Amount1,Amount2,Amount3,UnitPrice,TotalPrice,Discount,Comm,Up_Flag,KalaDeghatR1,KalaDeghatR2,KalaDeghatR3,KalaDeghatM1,KalaDeghatM2,KalaDeghatM3,DeghatR,InvSerialNumber,LFctSerialNumber,LinkNumber,LinkYear,LinkProg,BandSpec,ArzValue
                                         FROM Web_FDocB WHERE SerialNumber = {0}", SerialNumber.ToString());
                var listFactor = db.Database.SqlQuery<Web_FDocB>(sql1);
                UnitDatabase.SaveLog(dataAccount[0], dataAccount[1], dataAccount[2], ace, sal, group, SerialNumber, ModeCode, 1, FlagLog, 0);
                return Ok(listFactor);
            }
            return Ok(conStr);

        }


        // POST: api/AFI_FDocBi
        [Route("api/AFI_FDocBi/SaveAllDocB/{ace}/{sal}/{group}/{serialNumber}")]
        [ResponseType(typeof(AFI_FDocBi))]
        public async Task<IHttpActionResult> PostAFI_SaveAllDocB(string ace, string sal, string group, long serialNumber, [FromBody]List<AFI_FDocBi> AFI_FDocBi)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var dataAccount = UnitDatabase.ReadUserPassHeader(this.Request.Headers);
            string conStr = UnitDatabase.CreateConnectionString(dataAccount[0], dataAccount[1], dataAccount[2],dataAccount[3], ace, sal, group, serialNumber, AFI_FDocBi[0].ModeCode, 5, 0);
            if (conStr.Length > 100)
            {
                ApiModel db = new ApiModel(conStr);
                int value;
                int i = 0;
                try
                {
                    foreach (var item in AFI_FDocBi)
                    {
                        i++;
                        string sql = string.Format(CultureInfo.InvariantCulture,


                             @"DECLARE	@return_value int
                            EXEC	@return_value = [dbo].[Web_SaveFDoc_BI_Temp]
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
                                    @ArzValue = {23}
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
                        item.ArzValue ?? 0
                        //item.flagTest == "Y" ? "Web_SaveFDoc_BI_Temp" : "Web_SaveFDoc_BI"
                        );
                        value = db.Database.SqlQuery<int>(sql).Single();
                    }
                    await db.SaveChangesAsync();

                }
                catch (Exception)
                {
                    throw;
                }
                UnitDatabase.SaveLog(dataAccount[0], dataAccount[1], dataAccount[2], ace, sal, group, serialNumber, "FDoc", 1, "Y", 0);
                return Ok("OK");
            }
            else
                return Ok(conStr);

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

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var dataAccount = UnitDatabase.ReadUserPassHeader(this.Request.Headers);
            string conStr = UnitDatabase.CreateConnectionString(dataAccount[0], dataAccount[1], dataAccount[2],dataAccount[3], ace, sal, group, ConvertObject.SerialNumber, ConvertObject.ModeCode, 5, 0);
            if (conStr.Length > 100)
            {
                ApiModel db = new ApiModel(conStr);
                try
                {
                    string sql = string.Format(CultureInfo.InvariantCulture,
                                  @"DECLARE	@return_value int
                                    EXEC	@return_value = [dbo].[Web_SaveFDocB_Convert]
		                                    @SerialNumber = {0},
		                                    @TempSerialNumber = {1}
                                    SELECT	'Return Value' = @return_value",
                                  ConvertObject.SerialNumber,
                                  ConvertObject.TempSerialNumber);
                    int value = db.Database.SqlQuery<int>(sql).Single();

                    await db.SaveChangesAsync();

                }
                catch (Exception)
                {
                    throw;
                }
                UnitDatabase.SaveLog(dataAccount[0], dataAccount[1], dataAccount[2], ace, sal, group, ConvertObject.SerialNumber, "FDoc", 1, "Y", 0);
                return Ok("OK");
            }
            else
                return Ok(conStr);

        }



    }
}