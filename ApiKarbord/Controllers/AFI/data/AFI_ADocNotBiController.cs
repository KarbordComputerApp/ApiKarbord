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
    public class AFI_ADocNotBiController : ApiController
    {

        public class AFI_ADocNotBi
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

            public double? ArzValue { get; set; }

            public string flagLog { get; set; }

            public string flagTest { get; set; }

            public double? Amount { get; set; }

            public byte? MjdControl { get; set; }

            public long? LinkSerialNumber { get; set; }

            public string LinkProg { get; set; }

            public int LinkYear { get; set; }

            public int LinkBandNo { get; set; }

        }

        // PUT: api/AFI_ADocNotBi/5
        [Route("api/AFI_ADocNotBi/{ace}/{sal}/{group}")]
        [ResponseType(typeof(AFI_ADocNotBi))]
        public async Task<IHttpActionResult> PutAFI_ADocNotBi(string ace, string sal, string group, AFI_ADocNotBi d)
        {
            string dBName = UnitDatabase.DatabaseName(ace, sal, group);
            string sql = string.Format(CultureInfo.InvariantCulture,
                        @" DECLARE	@return_value int, @outputSt nvarchar(1000) = ''
                             EXEC	@return_value = {0}.[dbo].[Web_SaveADoc_NotBU]
                                    @MjdControl = {1},
                                    @SerialNumber = {2},
                                    @BandNo = {3},
                                    @AccCode = N'{4}',
                                    @AccZCode = N'{5}',
                                    @Bede = {6},
                                    @Best = {7},
                                    @Amount = {8},
                                    @Comm = N'{9}',
                                    @BandSpec = N'{10}',
                                    @CheckNo = N'{11}',
                                    @CheckDate = N'{12}',
                                    @Bank = N'{13}',
                                    @Shobe = N'{14}',
                                    @Jari = N'{15}',
                                    @BaratNo = N'{16}',
                                    @TrafCode = N'{17}',
                                    @TrafZCode = N'{18}',
                                    @CheckRadif = {19},
                                    @CheckComm = N'{20}',
                                    @CheckStatus = N'{21}',
                                    @CheckVosoolDate = N'{22}',
                                    @LinkSerialNumber = {23},
                                    @LinkYear = {24},
                                    @LinkBandNo = {25},
                                    @LinkProg = N'{26}',
                                    @OprCode = N'{27}',
                                    @MkzCode = N'{28}',
                                    @ArzCode = N'{29}',
                                    @ArzRate = {30},
                                    @ArzValue = {31},
                                    @outputSt = @outputSt OUTPUT
                             SELECT	@outputSt as outputSt",
                        dBName,
                        d.MjdControl ?? 0,
                        d.SerialNumber,
                        d.BandNo,
                        d.AccCode,
                        d.AccZCode,
                        d.Bede ?? 0,
                        d.Best ?? 0,
                        d.Amount ?? 0,
                        UnitPublic.ConvertTextWebToWin(d.Comm ?? ""),
                        d.BandSpec,
                        d.CheckNo,
                        d.CheckDate,
                        d.Bank,
                        d.Shobe,
                        d.Jari,
                        d.BaratNo,
                        d.TrafCode,
                        d.TrafZCode,
                        d.CheckRadif ?? 0,
                        UnitPublic.ConvertTextWebToWin(d.CheckComm ?? ""),
                        d.CheckStatus,
                        d.CheckVosoolDate,
                        d.LinkSerialNumber ?? 0,
                        d.LinkYear,
                        d.LinkBandNo,
                        d.LinkProg,
                        d.OprCode,
                        d.MkzCode,
                        d.ArzCode,
                        d.ArzRate ?? 0,
                        d.ArzValue ?? 0
                        );
            var dataAccount = UnitDatabase.ReadUserPassHeader(this.Request.Headers);
            var DBase = UnitDatabase.dataDB.Where(p => p.UserName.ToUpper() == dataAccount[0].ToUpper() && p.Password == dataAccount[1]).Single();
            string res = UnitDatabase.TestAcount(DBase, dataAccount[3], ace, group, UnitPublic.access_ANOTE);
            if (res == "")
            {
                string value = DBase.DB.Database.SqlQuery<string>(sql).Single();
                if (value == "")
                {
                    await DBase.DB.SaveChangesAsync();
                }

                UnitDatabase.SaveLog(dataAccount[0], dataAccount[1], dataAccount[2], ace, sal, group, d.SerialNumber ?? 0, UnitPublic.access_ANOTE, 1, d.flagLog, 1, 0);
                return Ok(value);

            }
            else
                return Ok(res);

        }

        // POST: api/AFI_ADocNotBi
        [Route("api/AFI_ADocNotBi/{ace}/{sal}/{group}/{bandno}")]
        [ResponseType(typeof(AFI_ADocNotBi))]
        public async Task<IHttpActionResult> PostAFI_ADocNotBi(string ace, string sal, string group, int bandNo, AFI_ADocNotBi d)
        {
            string dBName = UnitDatabase.DatabaseName(ace, sal, group);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var dataAccount = UnitDatabase.ReadUserPassHeader(this.Request.Headers);
            var DBase = UnitDatabase.dataDB.Where(p => p.UserName.ToUpper() == dataAccount[0].ToUpper() && p.Password == dataAccount[1]).Single();
            string res = UnitDatabase.TestAcount(DBase, dataAccount[3], ace, group, UnitPublic.access_ANOTE);
            if (res == "")
            {
                try
                {
                    if (bandNo > 0)
                    {
                        string sqlUpdateBand = string.Format(@"DECLARE	@return_value int
                                                                  EXEC  @return_value = {0}.[dbo].[Web_Doc_BShift]
                                                                        @TableName = '{1}',
                                                                        @SerialNumber = {2},
                                                                        @BandNo = {3},
                                                                        @BandNoFld = '{4}'
                                                                 SELECT	'Return Value' = @return_value",
                                                               dBName,
                                                               "Acc5NOTEB",
                                                               d.SerialNumber,
                                                               bandNo,
                                                               "BandNo");
                        int valueUpdateBand = DBase.DB.Database.SqlQuery<int>(sqlUpdateBand).Single();
                    }
                    string sql = string.Format(CultureInfo.InvariantCulture,
                        @" DECLARE	@return_value int, @outputSt nvarchar(1000) = ''
                             EXEC	@return_value = {0}.[dbo].[Web_SaveADoc_NotBI]
                                    @MjdControl = {1},
                                    @SerialNumber = {2},
                                    @BandNo = {3},
                                    @AccCode = N'{4}',
                                    @AccZCode = N'{5}',
                                    @Bede = {6},
                                    @Best = {7},
                                    @Amount = {8},
                                    @Comm = N'{9}',
                                    @BandSpec = N'{10}',
                                    @CheckNo = N'{11}',
                                    @CheckDate = N'{12}',
                                    @Bank = N'{13}',
                                    @Shobe = N'{14}',
                                    @Jari = N'{15}',
                                    @BaratNo = N'{16}',
                                    @TrafCode = N'{17}',
                                    @TrafZCode = N'{18}',
                                    @CheckRadif = {19},
                                    @CheckComm = N'{20}',
                                    @CheckStatus = N'{21}',
                                    @CheckVosoolDate = N'{22}',
                                    @LinkSerialNumber = {23},
                                    @LinkYear = {24},
                                    @LinkBandNo = {25},
                                    @LinkProg = N'{26}',
                                    @OprCode = N'{27}',
                                    @MkzCode = N'{28}',
                                    @ArzCode = N'{29}',
                                    @ArzRate = {30},
                                    @ArzValue = {31},
                                    @outputSt = @outputSt OUTPUT
                             SELECT	@outputSt as outputSt",
                        dBName,
                        d.MjdControl ?? 0,
                        d.SerialNumber,
                        bandNo == 0 ? d.BandNo : bandNo,
                        d.AccCode,
                        d.AccZCode,
                        d.Bede ?? 0,
                        d.Best ?? 0,
                        d.Amount ?? 0,
                        UnitPublic.ConvertTextWebToWin(d.Comm ?? ""),
                        d.BandSpec,
                        d.CheckNo,
                        d.CheckDate,
                        d.Bank,
                        d.Shobe,
                        d.Jari,
                        d.BaratNo,
                        d.TrafCode,
                        d.TrafZCode,
                        d.CheckRadif ?? 0,
                        UnitPublic.ConvertTextWebToWin(d.CheckComm ?? ""),
                        d.CheckStatus,
                        d.CheckVosoolDate,
                        d.LinkSerialNumber ?? 0,
                        d.LinkYear,
                        d.LinkBandNo,
                        d.LinkProg,
                        d.OprCode,
                        d.MkzCode,
                        d.ArzCode,
                        d.ArzRate ?? 0,
                        d.ArzValue ?? 0
                        );
                    string value = DBase.DB.Database.SqlQuery<string>(sql).Single();
                    if (value == "")
                    {
                        await DBase.DB.SaveChangesAsync();
                    }

                    UnitDatabase.SaveLog(dataAccount[0], dataAccount[1], dataAccount[2], ace, sal, group, d.SerialNumber ?? 0, UnitPublic.access_ANOTE, 1, d.flagLog, 1, 0);
                    return Ok(value);
                }
                catch (Exception)
                {
                    throw;
                }
            }
            else
                return Ok(res);

        }

        // DELETE: api/AFI_ADocNotBi/5
        [Route("api/AFI_ADocNotBi/{ace}/{sal}/{group}/{SerialNumber}/{BandNo}/{FlagLog}")]
        [ResponseType(typeof(AFI_ADocNotBi))]
        public async Task<IHttpActionResult> DeleteAFI_ADocNotBi(string ace, string sal, string group, long SerialNumber, int BandNo, string FlagLog)
        {
            string dBName = UnitDatabase.DatabaseName(ace, sal, group);
            var dataAccount = UnitDatabase.ReadUserPassHeader(this.Request.Headers);
            var DBase = UnitDatabase.dataDB.Where(p => p.UserName.ToUpper() == dataAccount[0].ToUpper() && p.Password == dataAccount[1]).Single();
            string res = UnitDatabase.TestAcount(DBase, dataAccount[3], ace, group, UnitPublic.access_ANOTE);
            if (res == "")
            {
                try
                {
                    string sql = string.Format(@"DECLARE	@return_value int
                                                 EXEC	@return_value = {0}.[dbo].[Web_SaveADoc_NotBD]
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
                                                               EXEC	    @return_value = {0}.[dbo].[Web_Doc_BOrder]
                                                                        @TableName = '{1}',
                                                                        @SerialNumber = {2},
                                                                        @BandNoFld = '{3}'
                                                              SELECT   'Return Value' = @return_value",
                                                              dBName,
                                                              "Acc5NOTEB",
                                                              SerialNumber,
                                                              "BandNo");
                        int valueUpdateBand = DBase.DB.Database.SqlQuery<int>(sqlUpdateBand).Single();
                        await DBase.DB.SaveChangesAsync();
                    }
                }
                catch (Exception e)
                {
                    throw;
                }
                UnitDatabase.SaveLog(dataAccount[0], dataAccount[1], dataAccount[2], ace, sal, group, SerialNumber, UnitPublic.access_ANOTE, 1, FlagLog, 1, 0);
                return Ok("OK");
            }
            else
                return Ok(res);

        }

    }
}