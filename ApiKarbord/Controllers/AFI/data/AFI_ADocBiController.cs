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
    public class AFI_ADocBiController : ApiController
    {
        public class AFI_ADocBi
        {
            public long? SerialNumber { get; set; }

            public int? BandNo { get; set; }

            public string AccCode { get; set; }

            public string AccZCode { get; set; }

            public double? Bede { get; set; }

            public double? Best { get; set; }

            public string Comm { get; set; }

            public string BandSpec { get; set; }

            public string CheckNo { get; set; }

            public string CheckDate { get; set; }

            public string Bank { get; set; }

            public string Shobe { get; set; }

            public string Jari { get; set; }

            public string BaratNo { get; set; }

            public string TrafCode { get; set; }
            public string TrafZCode { get; set; }

            public int? CheckRadif { get; set; }

            public string CheckComm { get; set; }

            public string CheckStatus { get; set; }

            public string CheckVosoolDate { get; set; }

            public string OprCode { get; set; }

            public string MkzCode { get; set; }

            public string ArzCode { get; set; }

            public double? ArzRate { get; set; }

            public double? arzValue { get; set; }

            public string flagLog { get; set; }

            public string flagTest { get; set; }

            public double? Amount { get; set; }

            public byte? MjdControl { get; set; }
        }

        // PUT: api/AFI_ADocBi/5
        [Route("api/AFI_ADocBi/{ace}/{sal}/{group}")]
        [ResponseType(typeof(AFI_ADocBi))]
        public async Task<IHttpActionResult> PutAFI_ADocBi(string ace, string sal, string group, AFI_ADocBi aFI_ADocBi)
        {
            string sql = string.Format(CultureInfo.InvariantCulture,
                          @" DECLARE	@return_value int, @outputSt nvarchar(1000) = ''
                             EXEC	@return_value = [dbo].[Web_SaveADoc_BU]
		                            @SerialNumber = {0},
		                            @BandNo = {1},
		                            @AccCode = '{2}',
                                    @AccZCode = '{3}',		                            
                                    @Bede = {4},
		                            @Best = {5},
		                            @Comm = N'{6}',
		                            @BandSpec = N'{7}',
		                            @CheckNo = '{8}',
		                            @CheckDate = N'{9}',
		                            @Bank = N'{10}',
		                            @Shobe = N'{11}',
		                            @Jari = N'{12}',
		                            @BaratNo = N'{13}',
		                            @TrafCode = '{14}',
                                    @TrafZCode = '{15}',
		                            @CheckRadif = {16},
		                            @CheckComm = N'{17}',
                                    @CheckStatus = '{18}',
		                            @CheckVosoolDate = N'{19}',
		                            @OprCode = '{20}',
		                            @MkzCode = '{21}',
		                            @ArzCode = '{22}',
		                            @ArzRate = {23},
		                            @ArzValue = {24},
		                            @Amount = {25},
                                    @MjdControl = {26},
                                    @outputSt = @outputSt OUTPUT
                             SELECT	@outputSt as outputSt",
                        aFI_ADocBi.SerialNumber,
                        aFI_ADocBi.BandNo,
                        aFI_ADocBi.AccCode,
                        aFI_ADocBi.AccZCode,
                        aFI_ADocBi.Bede ?? 0,
                        aFI_ADocBi.Best ?? 0,
                        UnitPublic.ConvertTextWebToWin(aFI_ADocBi.Comm ?? ""),
                        aFI_ADocBi.BandSpec,
                        aFI_ADocBi.CheckNo,
                        aFI_ADocBi.CheckDate,
                        aFI_ADocBi.Bank,
                        aFI_ADocBi.Shobe,
                        aFI_ADocBi.Jari,
                        aFI_ADocBi.BaratNo,
                        aFI_ADocBi.TrafCode,
                        aFI_ADocBi.TrafZCode,
                        aFI_ADocBi.CheckRadif ?? 0,
                        UnitPublic.ConvertTextWebToWin(aFI_ADocBi.CheckComm ?? ""),
                        aFI_ADocBi.CheckStatus,
                        aFI_ADocBi.CheckVosoolDate,
                        aFI_ADocBi.OprCode,
                        aFI_ADocBi.MkzCode,
                        aFI_ADocBi.ArzCode,
                        aFI_ADocBi.ArzRate ?? 0,
                        aFI_ADocBi.arzValue ?? 0,
                        aFI_ADocBi.Amount ?? 0,
                        aFI_ADocBi.MjdControl ?? 0
                        );

            var dataAccount = UnitDatabase.ReadUserPassHeader(this.Request.Headers);
            string conStr = UnitDatabase.CreateConnectionString(dataAccount[0], dataAccount[1], dataAccount[2], dataAccount[3], ace, sal, group, aFI_ADocBi.SerialNumber ?? 0, UnitPublic.access_ADOC, UnitPublic.act_EditBand, aFI_ADocBi.BandNo ?? 0);
            if (conStr.Length > 100)
            {
                ApiModel db = new ApiModel(conStr);
                string value = db.Database.SqlQuery<string>(sql).Single();
                if (value == "")
                {
                    await db.SaveChangesAsync();
                }
                //await db.SaveChangesAsync();

                UnitDatabase.SaveLog(dataAccount[0], dataAccount[1], dataAccount[2], ace, sal, group, aFI_ADocBi.SerialNumber ?? 0, UnitPublic.access_ADOC, 1, aFI_ADocBi.flagLog, 1, 0);

                if ((aFI_ADocBi.MjdControl ?? 0) == 0)
                {
                    string sql1 = string.Format(@"SELECT * FROM Web_ADocB WHERE SerialNumber = {0}", aFI_ADocBi.SerialNumber);
                    var listSanad = db.Database.SqlQuery<Web_ADocB>(sql1);
                    return Ok(listSanad);
                }
                else
                {
                    return Ok(value);
                }
            }
            return Ok(conStr);
        }

        // POST: api/AFI_ADocBi
        [Route("api/AFI_ADocBi/{ace}/{sal}/{group}/{bandno}")]
        [ResponseType(typeof(AFI_ADocBi))]
        public async Task<IHttpActionResult> PostAFI_ADocBi(string ace, string sal, string group, int bandNo, AFI_ADocBi aFI_ADocBi)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var dataAccount = UnitDatabase.ReadUserPassHeader(this.Request.Headers);
            string conStr = UnitDatabase.CreateConnectionString(dataAccount[0], dataAccount[1], dataAccount[2], dataAccount[3], ace, sal, group, aFI_ADocBi.SerialNumber ?? 0, UnitPublic.access_ADOC, UnitPublic.act_NewBand, bandNo == 0 ? aFI_ADocBi.BandNo ?? 0 : bandNo);
            if (conStr.Length > 100)
            {
                try
                {
                    ApiModel db = new ApiModel(conStr);

                    if (bandNo > 0)
                    {
                        string sqlUpdateBand = string.Format(@"DECLARE	@return_value int
                                                                 EXEC	@return_value = [dbo].[Web_Doc_BShift]
                                                                      @TableName = '{0}',
                                                                      @SerialNumber = {1},
                                                                      @BandNo = {2},
                                                                      @BandNoFld = '{3}'
                                                                 SELECT	'Return Value' = @return_value",
                                                               ace == UnitPublic.Web1 ? "Afi1ADocB" : "Acc5DocB",
                                                               aFI_ADocBi.SerialNumber,
                                                               bandNo,
                                                               "BandNo");
                        int valueUpdateBand = db.Database.SqlQuery<int>(sqlUpdateBand).Single();
                    }
                    string sql = string.Format(CultureInfo.InvariantCulture,
                        @" DECLARE	@return_value int, @outputSt nvarchar(1000) = ''
                             EXEC	@return_value = [dbo].[Web_SaveADoc_BI]
		                            @SerialNumber = {0},
		                            @BandNo = {1},
		                            @AccCode = '{2}',
                                    @AccZCode = '{3}',		                            
                                    @Bede = {4},
		                            @Best = {5},
		                            @Comm = N'{6}',
		                            @BandSpec = N'{7}',
		                            @CheckNo = '{8}',
		                            @CheckDate = N'{9}',
		                            @Bank = N'{10}',
		                            @Shobe = N'{11}',
		                            @Jari = N'{12}',
		                            @BaratNo = N'{13}',
		                            @TrafCode = '{14}',
                                    @TrafZCode = '{15}',
		                            @CheckRadif = {16},
		                            @CheckComm = N'{17}',
                                    @CheckStatus = '{18}',
		                            @CheckVosoolDate = N'{19}',
		                            @OprCode = '{20}',
		                            @MkzCode = '{21}',
		                            @ArzCode = '{22}',
		                            @ArzRate = {23},
		                            @ArzValue = {24},
		                            @Amount = {25},
		                            @MjdControl = {26},
                                    @outputSt = @outputSt OUTPUT
                             SELECT	@outputSt as outputSt",
                        aFI_ADocBi.SerialNumber,
                        bandNo == 0 ? aFI_ADocBi.BandNo : bandNo,
                        aFI_ADocBi.AccCode,
                        aFI_ADocBi.AccZCode,
                        aFI_ADocBi.Bede ?? 0,
                        aFI_ADocBi.Best ?? 0,
                        UnitPublic.ConvertTextWebToWin(aFI_ADocBi.Comm ?? ""),
                        aFI_ADocBi.BandSpec,
                        aFI_ADocBi.CheckNo,
                        aFI_ADocBi.CheckDate,
                        aFI_ADocBi.Bank,
                        aFI_ADocBi.Shobe,
                        aFI_ADocBi.Jari,
                        aFI_ADocBi.BaratNo,
                        aFI_ADocBi.TrafCode,
                        aFI_ADocBi.TrafZCode,
                        aFI_ADocBi.CheckRadif ?? 0,
                        UnitPublic.ConvertTextWebToWin(aFI_ADocBi.CheckComm ?? ""),
                        aFI_ADocBi.CheckStatus,
                        aFI_ADocBi.CheckVosoolDate,
                        aFI_ADocBi.OprCode,
                        aFI_ADocBi.MkzCode,
                        aFI_ADocBi.ArzCode,
                        aFI_ADocBi.ArzRate ?? 0,
                        aFI_ADocBi.arzValue ?? 0,
                        aFI_ADocBi.Amount ?? 0,
                        aFI_ADocBi.MjdControl ?? 0
                        );
                    string value = db.Database.SqlQuery<string>(sql).Single();
                    if (value == "")
                    {
                        await db.SaveChangesAsync();
                    }

                    UnitDatabase.SaveLog(dataAccount[0], dataAccount[1], dataAccount[2], ace, sal, group, aFI_ADocBi.SerialNumber ?? 0, UnitPublic.access_ADOC, 1, aFI_ADocBi.flagLog, 1, 0);

                    if ((aFI_ADocBi.MjdControl ?? 0) == 0)
                    {
                        sql = string.Format(@"SELECT * FROM Web_ADocB WHERE SerialNumber = {0}", aFI_ADocBi.SerialNumber);
                        var listSanad = db.Database.SqlQuery<Web_ADocB>(sql);
                        return Ok(listSanad);
                    }
                    else
                    {
                        return Ok(value);
                    }
                }
                catch (Exception)
                {
                    throw;
                }
            }
            else
                return Ok(conStr);

        }



        // POST: api/AFI_ADocBi
        [Route("api/AFI_ADocBi/SaveAllDocB/{ace}/{sal}/{group}/{serialNumber}")]
        [ResponseType(typeof(AFI_ADocBi))]
        public async Task<IHttpActionResult> PostAFI_SaveAllDocB(string ace, string sal, string group, long serialNumber, [FromBody]List<AFI_ADocBi> AFI_ADocBi)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var dataAccount = UnitDatabase.ReadUserPassHeader(this.Request.Headers);
            string conStr = UnitDatabase.CreateConnectionString(dataAccount[0], dataAccount[1], dataAccount[2], dataAccount[3], ace, sal, group, serialNumber, UnitPublic.access_ADOC, UnitPublic.act_NewBand, 0);
            if (conStr.Length > 100)
            {
                int value;
                int i = 0;
                ApiModel db = new ApiModel(conStr);
                try
                {
                    foreach (var item in AFI_ADocBi)
                    {
                        i++;
                        string sql = string.Format(CultureInfo.InvariantCulture,
                         @" DECLARE	@return_value int
                             EXEC	@return_value = [dbo].[{25}]
		                            @SerialNumber = {0},
		                            @BandNo = {1},
		                            @AccCode = '{2}',
                                    @AccZCode = '{3}',		                            
                                    @Bede = {4},
		                            @Best = {5},
		                            @Comm = N'{6}',
		                            @BandSpec = N'{7}',
		                            @CheckNo = '{8}',
		                            @CheckDate = N'{9}',
		                            @Bank = N'{10}',
		                            @Shobe = N'{11}',
		                            @Jari = N'{12}',
		                            @BaratNo = N'{13}',
		                            @TrafCode = '{14}',
                                    @TrafZCode = '{15}',
		                            @CheckRadif = {16},
		                            @CheckComm = N'{17}',
                                    @CheckStatus = '{18}',
		                            @CheckVosoolDate = N'{19}',
		                            @OprCode = '{20}',
		                            @MkzCode = '{21}',
		                            @ArzCode = '{22}',
		                            @ArzRate = {23},
		                            @ArzValue = {24},
                                    @Amount = {26}
                             SELECT	'Return Value' = @return_value",
                        serialNumber,
                        i,
                        item.AccCode,
                        item.AccZCode,
                        item.Bede ?? 0,
                        item.Best ?? 0,
                        UnitPublic.ConvertTextWebToWin(item.Comm ?? ""),
                        item.BandSpec,
                        item.CheckNo,
                        item.CheckDate,
                        item.Bank,
                        item.Shobe,
                        item.Jari,
                        item.BaratNo,
                        item.TrafCode,
                        item.TrafZCode,
                        item.CheckRadif ?? 0,
                        UnitPublic.ConvertTextWebToWin(item.CheckComm ?? ""),
                        item.CheckStatus,
                        item.CheckVosoolDate,
                        item.OprCode,
                        item.MkzCode,
                        item.ArzCode,
                        item.ArzRate ?? 0,
                        item.arzValue ?? 0,
                        item.flagTest == "Y" ? "Web_SaveADoc_BI_Temp" : "Web_SaveADoc_BI",
                        item.Amount ?? 0);
                        value = db.Database.SqlQuery<int>(sql).Single();
                    }
                    await db.SaveChangesAsync();

                }
                catch (Exception)
                {
                    throw;
                }
                UnitDatabase.SaveLog(dataAccount[0], dataAccount[1], dataAccount[2], ace, sal, group, serialNumber, UnitPublic.access_ADOC, 1, "Y", 1, 0);
                return Ok("OK");
            }
            else
                return Ok(conStr);

        }




        // DELETE: api/AFI_ADocBi/5
        [Route("api/AFI_ADocBi/{ace}/{sal}/{group}/{SerialNumber}/{BandNo}/{FlagLog}")]
        [ResponseType(typeof(AFI_ADocBi))]
        public async Task<IHttpActionResult> DeleteAFI_ADocBi(string ace, string sal, string group, long SerialNumber, int BandNo, string FlagLog)
        {
            var dataAccount = UnitDatabase.ReadUserPassHeader(this.Request.Headers);
            string conStr = UnitDatabase.CreateConnectionString(dataAccount[0], dataAccount[1], dataAccount[2], dataAccount[3], ace, sal, group, SerialNumber, UnitPublic.access_ADOC, UnitPublic.act_DeleteBand, BandNo);
            if (conStr.Length > 100)
            {
                ApiModel db = new ApiModel(conStr);
                try
                {
                    string sql = string.Format(@"DECLARE	@return_value int
                                                 EXEC	@return_value = [dbo].[Web_SaveADoc_BD]
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
                                                                 ace == UnitPublic.Web1 ? "Afi1ADocB" : "Acc5DocB",
                                                                 SerialNumber,
                                                                 "BandNo");
                        int valueUpdateBand = db.Database.SqlQuery<int>(sqlUpdateBand).Single();
                        await db.SaveChangesAsync();
                    }
                }
                catch (Exception e)
                {
                    throw;
                }
                UnitDatabase.SaveLog(dataAccount[0], dataAccount[1], dataAccount[2], ace, sal, group, SerialNumber, UnitPublic.access_ADOC, 1, FlagLog, 1, 0);
                if (BandNo > 0)
                {
                    string sql1 = string.Format(@"SELECT * FROM Web_ADocB WHERE SerialNumber = {0}", SerialNumber.ToString());
                    var listSanad = db.Database.SqlQuery<Web_ADocB>(sql1);
                    return Ok(listSanad);
                }
                else
                {
                    return Ok("OK");
                }
            }
            else
                return Ok(conStr);
        }

        public class ConvertObject
        {

            public long SerialNumber { get; set; }

            public long TempSerialNumber { get; set; }

        }



        // POST: api/AFI_ADocBi
        [Route("api/AFI_ADocBi/Convert/{ace}/{sal}/{group}")]
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PostAFI_Convert(string ace, string sal, string group, ConvertObject ConvertObject)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var dataAccount = UnitDatabase.ReadUserPassHeader(this.Request.Headers);
            string conStr = UnitDatabase.CreateConnectionString(dataAccount[0], dataAccount[1], dataAccount[2], dataAccount[3], ace, sal, group, ConvertObject.SerialNumber, UnitPublic.access_ADOC, UnitPublic.act_NewBand, 0);
            if (conStr.Length > 100)
            {
                ApiModel db = new ApiModel(conStr);
                try
                {
                    string sql = string.Format(CultureInfo.InvariantCulture,
                                  @"DECLARE	@return_value int
                                    EXEC	@return_value = [dbo].[Web_SaveADocB_Convert]
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
                UnitDatabase.SaveLog(dataAccount[0], dataAccount[1], dataAccount[2], ace, sal, group, ConvertObject.SerialNumber, UnitPublic.access_ADOC, 1, "Y", 1, 0);
                return Ok("OK");
            }
            else
                return Ok(conStr);

        }
    }
}