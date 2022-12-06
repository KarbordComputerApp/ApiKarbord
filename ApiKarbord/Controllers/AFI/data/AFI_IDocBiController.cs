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
    public class AFI_IDocBiController : ApiController
    {


        // PUT: api/AFI_IDocBi/5
        [Route("api/AFI_IDocBi/{ace}/{sal}/{group}/{BandNo}")]
        [ResponseType(typeof(AFI_IDocBi))]
        public async Task<IHttpActionResult> PutAFI_IDocBi(string ace, string sal, string group, long BandNo, AFI_IDocBi aFI_IDocBi)
        {
            string value;
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var dataAccount = UnitDatabase.ReadUserPassHeader(this.Request.Headers);
            string conStr = UnitDatabase.CreateConnectionString(dataAccount[0], dataAccount[1], dataAccount[2], dataAccount[3], ace, sal, group, aFI_IDocBi.SerialNumber, aFI_IDocBi.InOut == 1 ? "IIDoc" : "IODoc", 4, aFI_IDocBi.BandNo ?? 0);
            if (conStr.Length > 100)
            {
                ApiModel db = new ApiModel(conStr);
                try
                {
                    string sql = string.Format(CultureInfo.InvariantCulture,
                          @"DECLARE	@return_value int, @outputSt nvarchar(1000) = ''
                            EXEC	@return_value = [dbo].[Web_SaveIDoc_BU]
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
                                    @OprCode = '{12}',
                                    @MkzCode = '{13}',
                                    @BandSpec = N'{14}',
                                    @ArzCode = N'{15}',
                                    @ArzRate = {16},
                                    @ArzValue = {17},
                                    @MjdControl = {18},
                                    @outputSt = @outputSt OUTPUT
                             SELECT	@outputSt as outputSt",
                        aFI_IDocBi.SerialNumber,
                        aFI_IDocBi.BandNo,
                        aFI_IDocBi.KalaCode,
                        aFI_IDocBi.Amount1 ?? 0,
                        aFI_IDocBi.Amount2 ?? 0,
                        aFI_IDocBi.Amount3 ?? 0,
                        aFI_IDocBi.UnitPrice ?? 0,
                        aFI_IDocBi.TotalPrice ?? 0,
                        aFI_IDocBi.Discount ?? 0,
                        aFI_IDocBi.MainUnit ?? 1,
                        UnitPublic.ConvertTextWebToWin(aFI_IDocBi.Comm ?? ""),
                        aFI_IDocBi.Up_Flag,
                        aFI_IDocBi.OprCode,
                        aFI_IDocBi.MkzCode,
                        UnitPublic.ConvertTextWebToWin(aFI_IDocBi.BandSpec ?? ""),
                        aFI_IDocBi.ArzCode ?? "",
                        aFI_IDocBi.ArzRate ?? 0,
                        aFI_IDocBi.ArzValue ?? 0,
                        aFI_IDocBi.MjdControl ?? 0
                        );
                    value = db.Database.SqlQuery<string>(sql).Single();
                    if (value == "")
                    {
                        await db.SaveChangesAsync();
                    }
                }
                catch (Exception)
                {
                    throw;
                }

                UnitDatabase.SaveLog(dataAccount[0], dataAccount[1], dataAccount[2], ace, sal, group, aFI_IDocBi.SerialNumber, aFI_IDocBi.InOut == 1 ? "IIDoc" : "IODoc", 2, aFI_IDocBi.flagLog, 0);


                if ((aFI_IDocBi.MjdControl ?? 0) == 0)
                {
                    string sql1 = string.Format(@"SELECT SerialNumber,BandNo,KalaCode,KalaName,MainUnit,MainUnitName,Amount1,Amount2,Amount3,UnitPrice,TotalPrice,Comm,Up_Flag,
                                                         KalaDeghatR1,KalaDeghatR2,KalaDeghatR3,KalaDeghatM1,KalaDeghatM2,KalaDeghatM3,DeghatR,BandSpec,ArzValue
                                                  FROM   Web_IDocB WHERE SerialNumber = {0}", aFI_IDocBi.SerialNumber);
                    var list = db.Database.SqlQuery<Web_IDocB>(sql1);
                    return Ok(list);
                }
                else
                {
                    return Ok(value);
                }
            }
            return Ok(conStr);
        }

        // POST: api/AFI_IDocBi
        [Route("api/AFI_IDocBi/{ace}/{sal}/{group}/{bandno}")]
        [ResponseType(typeof(AFI_IDocBi))]
        public async Task<IHttpActionResult> PostAFI_IDocBi(string ace, string sal, string group, long bandNo, AFI_IDocBi aFI_IDocBi)
        {
            string value;
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var dataAccount = UnitDatabase.ReadUserPassHeader(this.Request.Headers);
            string conStr = UnitDatabase.CreateConnectionString(dataAccount[0], dataAccount[1], dataAccount[2], dataAccount[3], ace, sal, group, aFI_IDocBi.SerialNumber, aFI_IDocBi.InOut == 1 ? "IIDoc" : "IODoc", 5, bandNo == 0 ? aFI_IDocBi.BandNo ?? 0 : Convert.ToInt32(bandNo));
            if (conStr.Length > 100)
            {
                ApiModel db = new ApiModel(conStr);
                try
                {
                    string fieldBandNo;
                    string tableName;
                    if (ace == "Web1")
                    {
                        tableName = "Afi1IDocB";
                        fieldBandNo = "BandNo";
                    }
                    else
                    {
                        tableName = "inv5docb";
                        fieldBandNo = "radif";
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
                                                           aFI_IDocBi.SerialNumber,
                                                           bandNo,
                                                           fieldBandNo);
                        int valueUpdateBand = db.Database.SqlQuery<int>(sqlUpdateBand).Single();
                    }
                    string sql = string.Format(CultureInfo.InvariantCulture,
                          @"DECLARE	@return_value int , @outputSt nvarchar(1000) = ''
                            EXEC	@return_value = [dbo].[Web_SaveIDoc_BI]
		                            @SerialNumber = {0},
		                            @BandNo = {1},
		                            @KalaCode = N'{2}',
		                            @Amount1 = {3},
		                            @Amount2 = {4},
		                            @Amount3 = {5},
		                            @UnitPrice = {6},
		                            @TotalPrice = {7},
		                            @MainUnit = {8},
		                            @Comm = N'{9}',
                                    @Up_Flag = {10},
                                    @OprCode = '{11}',
                                    @MkzCode = '{12}',
                                    @BandSpec = N'{13}',
                                    @ArzCode = N'{14}',
                                    @ArzRate = {15},
                                    @ArzValue = {16},
                                    @MjdControl = {17},
                                    @outputSt = @outputSt OUTPUT
                             SELECT	@outputSt as outputSt",
                        aFI_IDocBi.SerialNumber,
                        bandNo == 0 ? aFI_IDocBi.BandNo.ToString() : bandNo.ToString(),
                        aFI_IDocBi.KalaCode,
                        aFI_IDocBi.Amount1 ?? 0,
                        aFI_IDocBi.Amount2 ?? 0,
                        aFI_IDocBi.Amount3 ?? 0,
                        aFI_IDocBi.UnitPrice ?? 0,
                        aFI_IDocBi.TotalPrice ?? 0,
                        aFI_IDocBi.MainUnit ?? 1,
                        UnitPublic.ConvertTextWebToWin(aFI_IDocBi.Comm ?? ""),
                        aFI_IDocBi.Up_Flag,
                        aFI_IDocBi.OprCode,
                        aFI_IDocBi.MkzCode,
                        UnitPublic.ConvertTextWebToWin(aFI_IDocBi.BandSpec ?? ""),
                        aFI_IDocBi.ArzCode ?? "",
                        aFI_IDocBi.ArzRate ?? 0,
                        aFI_IDocBi.ArzValue ?? 0,
                        aFI_IDocBi.MjdControl ?? 0
                        );
                    value = db.Database.SqlQuery<string>(sql).Single();
                    if (value == "")
                    {
                        await db.SaveChangesAsync();
                    }
                }
                catch (Exception)
                {
                    throw;
                }

                UnitDatabase.SaveLog(dataAccount[0], dataAccount[1], dataAccount[2], ace, sal, group, aFI_IDocBi.SerialNumber, aFI_IDocBi.InOut == 1 ? "IIDoc" : "IODoc", 2, aFI_IDocBi.flagLog, 0);

                if ((aFI_IDocBi.MjdControl ?? 0) == 0)
                {
                    string sql1 = string.Format(@"SELECT SerialNumber,BandNo,KalaCode,KalaName,MainUnit,MainUnitName,Amount1,Amount2,Amount3,UnitPrice,TotalPrice,Comm,Up_Flag,
                                                         KalaDeghatR1,KalaDeghatR2,KalaDeghatR3,KalaDeghatM1,KalaDeghatM2,KalaDeghatM3,DeghatR,BandSpec,ArzValue 
                                                 FROM    Web_IDocB WHERE SerialNumber = {0}", aFI_IDocBi.SerialNumber);
                    var list = db.Database.SqlQuery<Web_IDocB>(sql1);
                    return Ok(list);
                }
                else
                {
                    return Ok(value);
                }
            }
            return Ok(conStr);
        }

        // DELETE: api/AFI_IDocBi/5
        [Route("api/AFI_IDocBi/{ace}/{sal}/{group}/{SerialNumber}/{BandNo}/{InOut}/{FlagLog}")]
        [ResponseType(typeof(AFI_IDocBi))]
        public async Task<IHttpActionResult> DeleteAFI_IDocBi(string ace, string sal, string group, long SerialNumber, int BandNo, int InOut, string FlagLog)
        {
            var dataAccount = UnitDatabase.ReadUserPassHeader(this.Request.Headers);
            string conStr = UnitDatabase.CreateConnectionString(dataAccount[0], dataAccount[1], dataAccount[2], dataAccount[3], ace, sal, group, SerialNumber, InOut == 1 ? "IIDoc" : "IODoc", 6, BandNo);
            if (conStr.Length > 100)
            {
                ApiModel db = new ApiModel(conStr);
                try
                {
                    string sql = string.Format(@"DECLARE	@return_value int
                                                 EXEC	@return_value = [dbo].[Web_SaveIDoc_BD]
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

                    string sqlUpdateBand = string.Format(@"DECLARE	@return_value int
                                                           EXEC	@return_value = [dbo].[Web_Doc_BOrder]
	                                                            @TableName = '{0}',
                                                                @SerialNumber = {1},
                                                                @BandNoFld = '{2}'
                                                           SELECT	'Return Value' = @return_value",
                                                           //ace == "Afi1" ? "Afi1IDocB" : "Inv5DocB",
                                                           ace == "Web1" ? "Afi1IDocB" : "Inv5DocB",
                                                           SerialNumber,
                                                           ace == "Web1" ? "BandNo" : "Radif");
                    int valueUpdateBand = db.Database.SqlQuery<int>(sqlUpdateBand).Single();
                    //await db.SaveChangesAsync();
                }
                catch (Exception e)
                {
                    throw;
                }

                string sql1 = string.Format(@"SELECT SerialNumber,BandNo,KalaCode,KalaName,MainUnit,MainUnitName,Amount1,Amount2,Amount3,UnitPrice,TotalPrice,Comm,Up_Flag,KalaDeghatR1,KalaDeghatR2,KalaDeghatR3,KalaDeghatM1,KalaDeghatM2,KalaDeghatM3
                                            ,DeghatR,BandSpec,ArzValue
                                         FROM Web_IDocB WHERE SerialNumber = {0}", SerialNumber.ToString());
                var listFactor = db.Database.SqlQuery<Web_IDocB>(sql1);
                UnitDatabase.SaveLog(dataAccount[0], dataAccount[1], dataAccount[2], ace, sal, group, SerialNumber, InOut == 1 ? "IIDoc" : "IODoc", 2, FlagLog, 0);

                return Ok(listFactor);
            }
            return Ok(conStr);
        }






        // POST: api/AFI_IDocBi
        [Route("api/AFI_IDocBi/SaveAllDocB/{ace}/{sal}/{group}/{serialNumber}")]
        [ResponseType(typeof(AFI_IDocBi))]
        public async Task<IHttpActionResult> PostAFI_SaveAllDocB(string ace, string sal, string group, long serialNumber, [FromBody]List<AFI_IDocBi> AFI_IDocBi)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var dataAccount = UnitDatabase.ReadUserPassHeader(this.Request.Headers);
            string conStr = UnitDatabase.CreateConnectionString(dataAccount[0], dataAccount[1], dataAccount[2], dataAccount[3], ace, sal, group, serialNumber, AFI_IDocBi[0].InOut == 1 ? "IIDoc" : "IODoc", 5, 0);
            if (conStr.Length > 100)
            {
                ApiModel db = new ApiModel(conStr);
                int value;
                int i = 0;
                try
                {
                    foreach (var item in AFI_IDocBi)
                    {
                        i++;
                        string sql = string.Format(CultureInfo.InvariantCulture,


                             @"DECLARE	@return_value int
                            EXEC	@return_value = [dbo].[Web_SaveIDoc_BI_Temp]
		                            @SerialNumber = {0},
		                            @BandNo = {1},
		                            @KalaCode = N'{2}',
		                            @Amount1 = {3},
		                            @Amount2 = {4},
		                            @Amount3 = {5},
		                            @UnitPrice = {6},
		                            @TotalPrice = {7},
		                            @MainUnit = {8},
		                            @Comm = N'{9}',
                                    @Up_Flag = {10},
                                    @OprCode = N'{11}',
		                            @MkzCode = N'{12}',
                                    @BandSpec = N'{13}',
                                    @ArzCode = N'{14}',
                                    @ArzRate = {15},
                                    @ArzValue = {16}
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
                        item.MainUnit ?? 1,
                        UnitPublic.ConvertTextWebToWin(item.Comm ?? ""),
                        item.Up_Flag,
                        item.OprCode,
                        item.MkzCode,
                        UnitPublic.ConvertTextWebToWin(item.BandSpec ?? ""),
                        item.ArzCode ?? "",
                        item.ArzRate ?? 0,
                        item.ArzValue ?? 0
                        );
                        value = db.Database.SqlQuery<int>(sql).Single();
                    }
                    await db.SaveChangesAsync();

                }
                catch (Exception)
                {
                    throw;
                }
                UnitDatabase.SaveLog(dataAccount[0], dataAccount[1], dataAccount[2], ace, sal, group, serialNumber, "IDoc", 1, "Y", 0);
                return Ok("OK");
            }
            else
                return Ok(conStr);

        }








        public class ConvertObject
        {
            public long SerialNumber { get; set; }

            public long TempSerialNumber { get; set; }

        }



        // POST: api/AFI_IDocBi
        [Route("api/AFI_IDocBi/Convert/{ace}/{sal}/{group}/{InOut}")]
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PostAFI_Convert(string ace, string sal, string group, string InOut, ConvertObject ConvertObject)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var dataAccount = UnitDatabase.ReadUserPassHeader(this.Request.Headers);
            string conStr = UnitDatabase.CreateConnectionString(dataAccount[0], dataAccount[1], dataAccount[2], dataAccount[3], ace, sal, group, ConvertObject.SerialNumber, InOut, 5, 0);
            if (conStr.Length > 100)
            {
                ApiModel db = new ApiModel(conStr);
                try
                {
                    string sql = string.Format(CultureInfo.InvariantCulture,
                                  @"DECLARE	@return_value int
                                    EXEC	@return_value = [dbo].[Web_SaveIDocB_Convert]
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