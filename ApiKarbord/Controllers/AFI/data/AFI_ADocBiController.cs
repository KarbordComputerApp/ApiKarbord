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

        }

        // PUT: api/AFI_ADocBi/5
        [Route("api/AFI_ADocBi/{ace}/{sal}/{group}")]
        [ResponseType(typeof(AFI_ADocBi))]
        public async Task<IHttpActionResult> PutAFI_ADocBi(string ace, string sal, string group, AFI_ADocBi aFI_ADocBi)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var dataAccount = UnitDatabase.ReadUserPassHeader(this.Request.Headers);
            string con = UnitDatabase.CreateConection(dataAccount[0], dataAccount[1], dataAccount[2], ace, sal, group, aFI_ADocBi.SerialNumber ?? 0, "ADoc", 4, aFI_ADocBi.BandNo ?? 0);
            if (con == "ok")
            {
                try
                {
                    string sql = string.Format(CultureInfo.InvariantCulture,
                          @" DECLARE	@return_value int
                             EXEC	@return_value = [dbo].[Web_SaveADoc_BU]
		                            @SerialNumber = {0},
		                            @BandNo = {1},
		                            @AccCode = '{2}',
                                    @AccZCode = '{3}',		                            
                                    @Bede = {4},
		                            @Best = {5},
		                            @Comm = '{6}',
		                            @BandSpec = '{7}',
		                            @CheckNo = '{8}',
		                            @CheckDate = N'{9}',
		                            @Bank = '{10}',
		                            @Shobe = '{11}',
		                            @Jari = '{12}',
		                            @BaratNo = '{13}',
		                            @TrafCode = '{14}',
                                    @TrafZCode = '{15}',
		                            @CheckRadif = {16},
		                            @CheckComm = '{17}',
                                    @CheckStatus = '{18}',
		                            @CheckVosoolDate = N'{19}',
		                            @OprCode = '{20}',
		                            @MkzCode = '{21}',
		                            @ArzCode = '{22}',
		                            @ArzRate = {23},
		                            @ArzValue = {24}
                             SELECT	'Return Value' = @return_value",
                        aFI_ADocBi.SerialNumber,
                        aFI_ADocBi.BandNo,
                        aFI_ADocBi.AccCode,
                        aFI_ADocBi.AccZCode,
                        aFI_ADocBi.Bede ?? 0,
                        aFI_ADocBi.Best ?? 0,
                        aFI_ADocBi.Comm,
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
                        aFI_ADocBi.CheckComm,
                        aFI_ADocBi.CheckStatus,
                        aFI_ADocBi.CheckVosoolDate,
                        aFI_ADocBi.OprCode,
                        aFI_ADocBi.MkzCode,
                        aFI_ADocBi.ArzCode,
                        aFI_ADocBi.ArzRate ?? 0,
                        aFI_ADocBi.arzValue ?? 0);
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
                string sql1 = string.Format(@"SELECT * FROM Web_ADocB WHERE SerialNumber = {0}", aFI_ADocBi.SerialNumber);
                var listSanad = UnitDatabase.db.Database.SqlQuery<Web_ADocB>(sql1);
                UnitDatabase.SaveLog(dataAccount[0], dataAccount[1], dataAccount[2], ace, sal, group, aFI_ADocBi.SerialNumber ?? 0, "ADoc", 1, aFI_ADocBi.flagLog, 0);
                return Ok(listSanad);
            }
            else
                return Ok(con);

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
            string con = UnitDatabase.CreateConection(dataAccount[0], dataAccount[1], dataAccount[2], ace, sal, group, aFI_ADocBi.SerialNumber ?? 0, "ADoc", 5, bandNo == 0 ? aFI_ADocBi.BandNo ?? 0 : bandNo);
            if (con == "ok")
            {
                try
                {
                    if (bandNo > 0)
                    {
                        string sqlUpdateBand = string.Format(@"DECLARE	@return_value int
                                                                 EXEC	@return_value = [dbo].[Web_Doc_BShift]
                                                                      @TableName = '{0}',
                                                                      @SerialNumber = {1},
                                                                      @BandNo = {2},
                                                                      @BandNoFld = '{3}'
                                                                 SELECT	'Return Value' = @return_value",
                                                               ace == "Web1" ? "Afi1ADocB" : "Acc5DocB",
                                                               aFI_ADocBi.SerialNumber,
                                                               bandNo,
                                                               "BandNo");
                        int valueUpdateBand = UnitDatabase.db.Database.SqlQuery<int>(sqlUpdateBand).Single();
                    }
                    string sql = string.Format(CultureInfo.InvariantCulture,
                        @" DECLARE	@return_value int
                             EXEC	@return_value = [dbo].[Web_SaveADoc_BI]
		                            @SerialNumber = {0},
		                            @BandNo = {1},
		                            @AccCode = '{2}',
                                    @AccZCode = '{3}',		                            
                                    @Bede = {4},
		                            @Best = {5},
		                            @Comm = '{6}',
		                            @BandSpec = '{7}',
		                            @CheckNo = '{8}',
		                            @CheckDate = N'{9}',
		                            @Bank = '{10}',
		                            @Shobe = '{11}',
		                            @Jari = '{12}',
		                            @BaratNo = '{13}',
		                            @TrafCode = '{14}',
                                    @TrafZCode = '{15}',
		                            @CheckRadif = {16},
		                            @CheckComm = '{17}',
                                    @CheckStatus = '{18}',
		                            @CheckVosoolDate = N'{19}',
		                            @OprCode = '{20}',
		                            @MkzCode = '{21}',
		                            @ArzCode = '{22}',
		                            @ArzRate = {23},
		                            @ArzValue = {24}
                             SELECT	'Return Value' = @return_value",
                        aFI_ADocBi.SerialNumber,
                        bandNo == 0 ? aFI_ADocBi.BandNo : bandNo,
                        aFI_ADocBi.AccCode,
                        aFI_ADocBi.AccZCode,
                        aFI_ADocBi.Bede ?? 0,
                        aFI_ADocBi.Best ?? 0,
                        aFI_ADocBi.Comm,
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
                        aFI_ADocBi.CheckComm,
                        aFI_ADocBi.CheckStatus,
                        aFI_ADocBi.CheckVosoolDate,
                        aFI_ADocBi.OprCode,
                        aFI_ADocBi.MkzCode,
                        aFI_ADocBi.ArzCode,
                        aFI_ADocBi.ArzRate ?? 0,
                        aFI_ADocBi.arzValue ?? 0);
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
                string sql1 = string.Format(@"SELECT * FROM Web_ADocB WHERE SerialNumber = {0}", aFI_ADocBi.SerialNumber);
                var listSanad = UnitDatabase.db.Database.SqlQuery<Web_ADocB>(sql1);
                UnitDatabase.SaveLog(dataAccount[0], dataAccount[1], dataAccount[2], ace, sal, group, aFI_ADocBi.SerialNumber ?? 0, "ADoc", 1, aFI_ADocBi.flagLog, 0);
                return Ok(listSanad);
            }
            else
                return Ok(con);

        }

        // DELETE: api/AFI_ADocBi/5
        [Route("api/AFI_ADocBi/{ace}/{sal}/{group}/{SerialNumber}/{BandNo}/{FlagLog}")]
        [ResponseType(typeof(AFI_ADocBi))]
        public async Task<IHttpActionResult> DeleteAFI_ADocBi(string ace, string sal, string group, long SerialNumber, int BandNo, string FlagLog)
        {
            var dataAccount = UnitDatabase.ReadUserPassHeader(this.Request.Headers);
            string con = UnitDatabase.CreateConection(dataAccount[0], dataAccount[1], dataAccount[2], ace, sal, group, SerialNumber, "ADoc", 6, BandNo);
            if (con == "ok")
            {
                try
                {
                    string sql = string.Format(@"DECLARE	@return_value int
                                                 EXEC	@return_value = [dbo].[Web_SaveADoc_BD]
		                                                @SerialNumber = {0},
		                                                @BandNo = {1}
                                                 SELECT	'Return Value' = @return_value",
                                                SerialNumber,
                                                BandNo);
                    int value = UnitDatabase.db.Database.SqlQuery<int>(sql).Single();
                    if (value == 0)
                    {
                        await UnitDatabase.db.SaveChangesAsync();
                    }

                    string sqlUpdateBand = string.Format(@"DECLARE	@return_value int
                                                             EXEC	@return_value = [dbo].[Web_Doc_BOrder]
                                                                  @TableName = '{0}',
                                                                  @SerialNumber = {1},
                                                                  @BandNoFld = '{2}'
                                                             SELECT	'Return Value' = @return_value",
                                                             ace == "Web1" ? "Afi1ADocB" : "Acc5DocB",
                                                             SerialNumber,
                                                             "BandNo");
                    int valueUpdateBand = UnitDatabase.db.Database.SqlQuery<int>(sqlUpdateBand).Single();
                    await UnitDatabase.db.SaveChangesAsync();
                }
                catch (Exception e)
                {
                    throw;
                }
                string sql1 = string.Format(@"SELECT * FROM Web_ADocB WHERE SerialNumber = {0}", SerialNumber.ToString());
                var listSanad = UnitDatabase.db.Database.SqlQuery<Web_ADocB>(sql1);
                UnitDatabase.SaveLog(dataAccount[0], dataAccount[1], dataAccount[2], ace, sal, group, SerialNumber, "ADoc", 1, FlagLog, 0);
                return Ok(listSanad);
            }
            else
                return Ok(con);
        }
    }
}