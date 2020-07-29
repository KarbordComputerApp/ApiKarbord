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

            public int? CheckRadif { get; set; }

            public string CheckComm { get; set; }

            public string CheckVosoolDate { get; set; }

            public string OprCode { get; set; }

            public string MkzCode { get; set; }

            public string ArzCode { get; set; }

            public double? ArzRate { get; set; }

            public double? ArzValue { get; set; }
          
        }

        // PUT: api/AFI_ADocBi/5
        [Route("api/AFI_ADocBi/{ace}/{sal}/{group}/{BandNo}/{userName}/{password}")]
        [ResponseType(typeof(AFI_ADocBi))]
        public async Task<IHttpActionResult> PutAFI_ADocBi(string ace, string sal, string group, long BandNo, string userName, string password, AFI_ADocBi aFI_ADocBi)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (UnitDatabase.CreateConection(userName, password, ace, sal, group))
            {
                try
                {
                    string sql = string.Format(CultureInfo.InvariantCulture,
                          @" DECLARE	@return_value int
                             EXEC	@return_value = [dbo].[Web_SaveADoc_BU]
		                            @SerialNumber = {0},
		                            @BandNo = {1},
		                            @AccCode = '{2}',
		                            @Bede = {3},
		                            @Best = {4},
		                            @Comm = '{5}',
		                            @BandSpec = '{6}',
		                            @CheckNo = '{7}',
		                            @CheckDate = '{8}',
		                            @Bank = '{9}',
		                            @Shobe = '{10}',
		                            @Jari = '{11}',
		                            @BaratNo = '{12}',
		                            @TrafCode = '{13}',
		                            @CheckRadif = {14},
		                            @CheckComm = '{15}',
		                            @CheckVosoolDate = '{16}',
		                            @OprCode = '{17}',
		                            @MkzCode = '{18}',
		                            @ArzCode = '{19}',
		                            @ArzRate = {20},
		                            @ArzValue = {21}
                             SELECT	'Return Value' = @return_value",
                        aFI_ADocBi.SerialNumber,
                        aFI_ADocBi.BandNo,
                        aFI_ADocBi.AccCode,
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
                        aFI_ADocBi.CheckRadif ?? 0,
                        aFI_ADocBi.CheckComm,
                        aFI_ADocBi.CheckVosoolDate,
                        aFI_ADocBi.OprCode,
                        aFI_ADocBi.MkzCode,
                        aFI_ADocBi.ArzCode,
                        aFI_ADocBi.ArzRate ?? 0,
                        aFI_ADocBi.ArzValue ?? 0);
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
            }
            string sql1 = string.Format(@"SELECT * FROM Web_ADocB WHERE SerialNumber = {0}", aFI_ADocBi.SerialNumber);
            var listSanad = UnitDatabase.db.Database.SqlQuery<Web_ADocB>(sql1);
            return Ok(listSanad);
        }

        // POST: api/AFI_ADocBi
        [Route("api/AFI_ADocBi/{ace}/{sal}/{group}/{bandno}/{userName}/{password}")]
        [ResponseType(typeof(AFI_ADocBi))]
        public async Task<IHttpActionResult> PostAFI_ADocBi(string ace, string sal, string group, long bandNo, string userName, string password, AFI_ADocBi aFI_ADocBi)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (UnitDatabase.CreateConection(userName, password, ace, sal, group))
            {
                try
                {
                    /*  if (bandNo > 0)
                      {
                          string sqlUpdateBand = string.Format(@"DECLARE	@return_value int
                                                                 EXEC	@return_value = [dbo].[Web_Doc_BShift]
                                                                      @TableName = '{0}',
                                                                      @SerialNumber = {1},
                                                                      @BandNo = {2},
                                                                      @BandNoFld = '{3}'
                                                                 SELECT	'Return Value' = @return_value",
                                                                 tableName,
                                                                 aFI_ADocBi.SerialNumber,
                                                                 bandNo,
                                                                 fieldBandNo);
                          int valueUpdateBand = UnitDatabase.db.Database.SqlQuery<int>(sqlUpdateBand).Single();
                      }*/
                    string sql = string.Format(CultureInfo.InvariantCulture,
                        @" DECLARE	@return_value int
                             EXEC	@return_value = [dbo].[Web_SaveADoc_BI]
		                            @SerialNumber = {0},
		                            @BandNo = {1},
		                            @AccCode = '{2}',
		                            @Bede = {3},
		                            @Best = {4},
		                            @Comm = '{5}',
		                            @BandSpec = '{6}',
		                            @CheckNo = '{7}',
		                            @CheckDate = '{8}',
		                            @Bank = '{9}',
		                            @Shobe = '{10}',
		                            @Jari = '{11}',
		                            @BaratNo = '{12}',
		                            @TrafCode = '{13}',
		                            @CheckRadif = {14},
		                            @CheckComm = '{15}',
		                            @CheckVosoolDate = '{16}',
		                            @OprCode = '{17}',
		                            @MkzCode = '{18}',
		                            @ArzCode = '{19}',
		                            @ArzRate = {20},
		                            @ArzValue = {21}
                             SELECT	'Return Value' = @return_value",
                      aFI_ADocBi.SerialNumber,
                      aFI_ADocBi.BandNo,
                      aFI_ADocBi.AccCode,
                      aFI_ADocBi.Bede ?? 0,
                      aFI_ADocBi.Best ?? 0,
                      aFI_ADocBi.Comm,
                      aFI_ADocBi.BandSpec,
                      aFI_ADocBi.CheckNo ,
                      aFI_ADocBi.CheckDate,
                      aFI_ADocBi.Bank,
                      aFI_ADocBi.Shobe,
                      aFI_ADocBi.Jari,
                      aFI_ADocBi.BaratNo,
                      aFI_ADocBi.TrafCode,
                      aFI_ADocBi.CheckRadif ?? 0,
                      aFI_ADocBi.CheckComm,
                      aFI_ADocBi.CheckVosoolDate,
                      aFI_ADocBi.OprCode,
                      aFI_ADocBi.MkzCode,
                      aFI_ADocBi.ArzCode,
                      aFI_ADocBi.ArzRate ?? 0,
                      aFI_ADocBi.ArzValue ?? 0);
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
            }
            string sql1 = string.Format(@"SELECT * FROM Web_ADocB WHERE SerialNumber = {0}", aFI_ADocBi.SerialNumber);
            var listSanad = UnitDatabase.db.Database.SqlQuery<Web_ADocB>(sql1);
            return Ok(listSanad);
        }

        // DELETE: api/AFI_ADocBi/5
        [Route("api/AFI_ADocBi/{ace}/{sal}/{group}/{SerialNumber}/{BandNo}/{userName}/{password}")]
        [ResponseType(typeof(AFI_ADocBi))]
        public async Task<IHttpActionResult> DeleteAFI_ADocBi(string ace, string sal, string group, long SerialNumber, int BandNo, string userName, string password)
        {
            if (UnitDatabase.CreateConection(userName, password, ace, sal, group))
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

                  /*  string sqlUpdateBand = string.Format(@"DECLARE	@return_value int
                                                           EXEC	@return_value = [dbo].[Web_Doc_BOrder]
	                                                            @TableName = '{0}',
                                                                @SerialNumber = {1},
                                                                @BandNoFld = '{2}'
                                                           SELECT	'Return Value' = @return_value",
                                                           //ace == "Afi1" ? "Afi1ADocB" : "Inv5DocB",
                                                           ace == "AFI1" ? "Afi1ADocB" : "Inv5DocB",
                                                           SerialNumber,
                                                           ace == "AFI1" ? "BandNo" : "Radif");
                    int valueUpdateBand = UnitDatabase.db.Database.SqlQuery<int>(sqlUpdateBand).Single();*/
                    //await UnitDatabase.db.SaveChangesAsync();
                }
                catch (Exception e)
                {
                    throw;
                }
            }
            string sql1 = string.Format(@"SELECT * FROM Web_ADocB WHERE SerialNumber = {0}", SerialNumber.ToString());
            var listSanad = UnitDatabase.db.Database.SqlQuery<Web_ADocB>(sql1);
            return Ok(listSanad);
        }
    }
}