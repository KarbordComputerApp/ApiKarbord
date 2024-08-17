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
            string dBName = UnitDatabase.DatabaseName(ace, sal, group);
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
            var DBase = UnitDatabase.dataDB.Where(p => p.UserName.ToUpper() == dataAccount[0].ToUpper() && p.Password == dataAccount[1]).Single();
            string res = UnitDatabase.TestAcount(DBase, dataAccount[3], ace, group, aFI_IDocBi.InOut == 1 ? UnitPublic.access_IIDOC : UnitPublic.access_IODOC);
            if (res == "")
            {
                try
                {
                    string sql = string.Format(CultureInfo.InvariantCulture,
                          @"DECLARE	@return_value int, @outputSt nvarchar(1000) = ''
                            EXEC	@return_value = {36}.[dbo].[Web_SaveIDoc_BU]
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
                                    @KalaFileNo = N'{19}',
                                    @KalaState = N'{20}',
                                    @KalaExf1 = N'{21}',
                                    @KalaExf2 = N'{22}',
                                    @KalaExf3 = N'{23}',
                                    @KalaExf4 = N'{24}',
                                    @KalaExf5 = N'{25}',
                                    @KalaExf6 = N'{26}',
                                    @KalaExf7 = N'{27}',
                                    @KalaExf8 = N'{28}',
                                    @KalaExf9 = N'{29}',
                                    @KalaExf10 = N'{30}',
                                    @KalaExf11 = N'{31}',
                                    @KalaExf12 = N'{32}',
                                    @KalaExf13 = N'{33}',
                                    @KalaExf14 = N'{34}',
                                    @KalaExf15 = N'{35}',
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
                        aFI_IDocBi.MjdControl ?? 0,
                        aFI_IDocBi.KalaFileNo,
                        aFI_IDocBi.KalaState,
                        aFI_IDocBi.KalaExf1,
                        aFI_IDocBi.KalaExf2,
                        aFI_IDocBi.KalaExf3,
                        aFI_IDocBi.KalaExf4,
                        aFI_IDocBi.KalaExf5,
                        aFI_IDocBi.KalaExf6,
                        aFI_IDocBi.KalaExf7,
                        aFI_IDocBi.KalaExf8,
                        aFI_IDocBi.KalaExf9,
                        aFI_IDocBi.KalaExf10,
                        aFI_IDocBi.KalaExf11,
                        aFI_IDocBi.KalaExf12,
                        aFI_IDocBi.KalaExf13,
                        aFI_IDocBi.KalaExf14,
                        aFI_IDocBi.KalaExf15,
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

               // UnitDatabase.SaveLog(dataAccount[0], dataAccount[1], dataAccount[2], ace, sal, group, aFI_IDocBi.SerialNumber, aFI_IDocBi.InOut == 1 ? UnitPublic.access_IIDOC : UnitPublic.access_IODOC, 2, aFI_IDocBi.flagLog, 1, 0);
                UnitDatabase.SaveLog(dataAccount[0], dataAccount[1], dataAccount[2], ace, sal, group, aFI_IDocBi.SerialNumber, aFI_IDocBi.InOut == 1 ? UnitPublic.access_IIDOC : UnitPublic.access_IODOC, UnitPublic.act_EditBand, "Y", 1, aFI_IDocBi.BandNo ?? 0);


                if ((aFI_IDocBi.MjdControl ?? 0) == 0)
                {
                    string sql1 = string.Format(@"SELECT SerialNumber,BandNo,KalaCode,KalaName,MainUnit,MainUnitName,Amount1,Amount2,Amount3,UnitPrice,TotalPrice,Comm,Up_Flag,KalaDeghatR1,KalaDeghatR2,KalaDeghatR3,KalaDeghatM1,KalaDeghatM2,KalaDeghatM3,
                                                         KalaFileNo,KalaState,KalaExf1,KalaExf2,KalaExf3,KalaExf4,KalaExf5,KalaExf6,KalaExf7,KalaExf8,KalaExf9,KalaExf10,KalaExf11,KalaExf12,KalaExf13,KalaExf14,KalaExf15,
                                                         DeghatR,BandSpec,ArzValue
                                                  FROM   {0}.dbo.Web_IDocB WHERE SerialNumber = {1}", dBName, aFI_IDocBi.SerialNumber);
                    var list = DBase.DB.Database.SqlQuery<Web_IDocB>(sql1);
                    return Ok(list);

                }
                else
                {
                    return Ok(value);
                }


            }
            else
                return Ok(res);


            //string conStr = UnitDatabase.CreateConnectionString(dataAccount[0], dataAccount[1], dataAccount[2], dataAccount[3], ace, sal, group, aFI_IDocBi.SerialNumber, aFI_IDocBi.InOut == 1 ? UnitPublic.access_IIDOC : UnitPublic.access_IODOC, UnitPublic.act_EditBand, aFI_IDocBi.BandNo ?? 0);
        }

        // POST: api/AFI_IDocBi
        [Route("api/AFI_IDocBi/{ace}/{sal}/{group}/{bandno}")]
        [ResponseType(typeof(AFI_IDocBi))]
        public async Task<IHttpActionResult> PostAFI_IDocBi(string ace, string sal, string group, long bandNo, AFI_IDocBi aFI_IDocBi)
        {
            string dBName = UnitDatabase.DatabaseName(ace, sal, group);
            string value;
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var dataAccount = UnitDatabase.ReadUserPassHeader(this.Request.Headers);
            var DBase = UnitDatabase.dataDB.Where(p => p.UserName.ToUpper() == dataAccount[0].ToUpper() && p.Password == dataAccount[1]).Single();
            string res = UnitDatabase.TestAcount(DBase, dataAccount[3], ace, group, aFI_IDocBi.InOut == 1 ? UnitPublic.access_IIDOC : UnitPublic.access_IODOC);
            if (res == "")
            {
                try
                {
                    string fieldBandNo;
                    string tableName;
                    if (ace == UnitPublic.Web1)
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
                                                           EXEC	@return_value = {0}.[dbo].[Web_Doc_BShift]
	                                                            @TableName = '{1}',
                                                                @SerialNumber = {2},
                                                                @BandNo = {3},
                                                                @BandNoFld = '{4}'
                                                           SELECT	'Return Value' = @return_value",
                                                           dBName,
                                                           tableName,
                                                           aFI_IDocBi.SerialNumber,
                                                           bandNo,
                                                           fieldBandNo);
                        int valueUpdateBand = DBase.DB.Database.SqlQuery<int>(sqlUpdateBand).Single();
                    }
                    string sql = string.Format(CultureInfo.InvariantCulture,
                          @"DECLARE	@return_value int , @outputSt nvarchar(1000) = ''
                            EXEC	@return_value = {35}.[dbo].[Web_SaveIDoc_BI]
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
                                    @KalaFileNo = N'{18}',
                                    @KalaState = N'{19}',
                                    @KalaExf1 = N'{20}',
                                    @KalaExf2 = N'{21}',
                                    @KalaExf3 = N'{22}',
                                    @KalaExf4 = N'{23}',
                                    @KalaExf5 = N'{24}',
                                    @KalaExf6 = N'{25}',
                                    @KalaExf7 = N'{26}',
                                    @KalaExf8 = N'{27}',
                                    @KalaExf9 = N'{28}',
                                    @KalaExf10 = N'{29}',
                                    @KalaExf11 = N'{30}',
                                    @KalaExf12 = N'{31}',
                                    @KalaExf13 = N'{32}',
                                    @KalaExf14 = N'{33}',
                                    @KalaExf15 = N'{34}',
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
                        aFI_IDocBi.MjdControl ?? 0,
                        aFI_IDocBi.KalaFileNo,
                        aFI_IDocBi.KalaState,
                        aFI_IDocBi.KalaExf1,
                        aFI_IDocBi.KalaExf2,
                        aFI_IDocBi.KalaExf3,
                        aFI_IDocBi.KalaExf4,
                        aFI_IDocBi.KalaExf5,
                        aFI_IDocBi.KalaExf6,
                        aFI_IDocBi.KalaExf7,
                        aFI_IDocBi.KalaExf8,
                        aFI_IDocBi.KalaExf9,
                        aFI_IDocBi.KalaExf10,
                        aFI_IDocBi.KalaExf11,
                        aFI_IDocBi.KalaExf12,
                        aFI_IDocBi.KalaExf13,
                        aFI_IDocBi.KalaExf14,
                        aFI_IDocBi.KalaExf15,
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

               // UnitDatabase.SaveLog(dataAccount[0], dataAccount[1], dataAccount[2], ace, sal, group, aFI_IDocBi.SerialNumber, aFI_IDocBi.InOut == 1 ? UnitPublic.access_IIDOC : UnitPublic.access_IODOC, 2, aFI_IDocBi.flagLog, 1, 0);

                UnitDatabase.SaveLog(dataAccount[0], dataAccount[1], dataAccount[2], ace, sal, group, aFI_IDocBi.SerialNumber, aFI_IDocBi.InOut == 1 ? UnitPublic.access_IIDOC : UnitPublic.access_IODOC, UnitPublic.act_NewBand, "Y", 1, bandNo == 0 ? aFI_IDocBi.BandNo ?? 0 : Convert.ToInt32(bandNo));


                if ((aFI_IDocBi.MjdControl ?? 0) == 0)
                {
                    string sql1 = string.Format(@"SELECT SerialNumber,BandNo,KalaCode,KalaName,MainUnit,MainUnitName,Amount1,Amount2,Amount3,UnitPrice,TotalPrice,Comm,Up_Flag,KalaDeghatR1,KalaDeghatR2,KalaDeghatR3,KalaDeghatM1,KalaDeghatM2,KalaDeghatM3,
                                                         KalaFileNo,KalaState,KalaExf1,KalaExf2,KalaExf3,KalaExf4,KalaExf5,KalaExf6,KalaExf7,KalaExf8,KalaExf9,KalaExf10,KalaExf11,KalaExf12,KalaExf13,KalaExf14,KalaExf15,
                                                         DeghatR,BandSpec,ArzValue
                                                  FROM   {0}.dbo.Web_IDocB WHERE SerialNumber = {1}", dBName, aFI_IDocBi.SerialNumber);
                    var list = DBase.DB.Database.SqlQuery<Web_IDocB>(sql1);
                    return Ok(list);
                }
                else
                {
                    return Ok(value);
                }
            }
            else
                return Ok(res);

            //string conStr = UnitDatabase.CreateConnectionString(dataAccount[0], dataAccount[1], dataAccount[2], dataAccount[3], ace, sal, group, aFI_IDocBi.SerialNumber, aFI_IDocBi.InOut == 1 ? UnitPublic.access_IIDOC : UnitPublic.access_IODOC, UnitPublic.act_NewBand, bandNo == 0 ? aFI_IDocBi.BandNo ?? 0 : Convert.ToInt32(bandNo));
        }

        // DELETE: api/AFI_IDocBi/5
        [Route("api/AFI_IDocBi/{ace}/{sal}/{group}/{SerialNumber}/{BandNo}/{InOut}/{FlagLog}")]
        [ResponseType(typeof(AFI_IDocBi))]
        public async Task<IHttpActionResult> DeleteAFI_IDocBi(string ace, string sal, string group, long SerialNumber, int BandNo, int InOut, string FlagLog)
        {
            string dBName = UnitDatabase.DatabaseName(ace, sal, group);
            var dataAccount = UnitDatabase.ReadUserPassHeader(this.Request.Headers);
            var DBase = UnitDatabase.dataDB.Where(p => p.UserName.ToUpper() == dataAccount[0].ToUpper() && p.Password == dataAccount[1]).Single();
            string res = UnitDatabase.TestAcount(DBase, dataAccount[3], ace, group, InOut == 1 ? UnitPublic.access_IIDOC : UnitPublic.access_IODOC);
            if (res == "")
            {
                try
                {
                    string sql = string.Format(@"DECLARE	@return_value int
                                                 EXEC	@return_value = {0}.[dbo].[Web_SaveIDoc_BD]
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

                    string sqlUpdateBand = string.Format(@"DECLARE	@return_value int
                                                           EXEC	@return_value = {0}.[dbo].[Web_Doc_BOrder]
	                                                            @TableName = '{1}',
                                                                @SerialNumber = {2},
                                                                @BandNoFld = '{3}'
                                                           SELECT	'Return Value' = @return_value",
                                                           //ace == "Afi1" ? "Afi1IDocB" : "Inv5DocB",
                                                           dBName,
                                                           ace == UnitPublic.Web1 ? "Afi1IDocB" : "Inv5DocB",
                                                           SerialNumber,
                                                           ace == UnitPublic.Web1 ? "BandNo" : "Radif");
                    int valueUpdateBand = DBase.DB.Database.SqlQuery<int>(sqlUpdateBand).Single();
                    //await db.SaveChangesAsync();
                }
                catch (Exception e)
                {
                    throw;
                }

                string sql1 = string.Format(@"SELECT SerialNumber,BandNo,KalaCode,KalaName,MainUnit,MainUnitName,Amount1,Amount2,Amount3,UnitPrice,TotalPrice,Comm,Up_Flag,KalaDeghatR1,KalaDeghatR2,KalaDeghatR3,KalaDeghatM1,KalaDeghatM2,KalaDeghatM3,
                                                     KalaFileNo,KalaState,KalaExf1,KalaExf2,KalaExf3,KalaExf4,KalaExf5,KalaExf6,KalaExf7,KalaExf8,KalaExf9,KalaExf10,KalaExf11,KalaExf12,KalaExf13,KalaExf14,KalaExf15,
                                                     DeghatR,BandSpec,ArzValue
                                              FROM   {0}.dbo.Web_IDocB WHERE SerialNumber = {1}", dBName, SerialNumber.ToString());
                var listFactor = DBase.DB.Database.SqlQuery<Web_IDocB>(sql1);

               // UnitDatabase.SaveLog(dataAccount[0], dataAccount[1], dataAccount[2], ace, sal, group, SerialNumber, InOut == 1 ? UnitPublic.access_IIDOC : UnitPublic.access_IODOC, 2, FlagLog, 1, 0);
                UnitDatabase.SaveLog(dataAccount[0], dataAccount[1], dataAccount[2], ace, sal, group, SerialNumber, InOut == 1 ? UnitPublic.access_IIDOC : UnitPublic.access_IODOC, UnitPublic.act_DeleteBand, "Y", 1, BandNo);


                return Ok(listFactor);
            }
            else
                return Ok(res);

            //string conStr = UnitDatabase.CreateConnectionString(dataAccount[0], dataAccount[1], dataAccount[2], dataAccount[3], ace, sal, group, SerialNumber, InOut == 1 ? UnitPublic.access_IIDOC : UnitPublic.access_IODOC, UnitPublic.act_DeleteBand, BandNo);
        }



        // POST: api/AFI_IDocBi
        [Route("api/AFI_IDocBi/SaveAllDocB/{ace}/{sal}/{group}/{serialNumber}")]
        [ResponseType(typeof(AFI_IDocBi))]
        public async Task<IHttpActionResult> PostAFI_SaveAllDocB(string ace, string sal, string group, long serialNumber, [FromBody]List<AFI_IDocBi> AFI_IDocBi)
        {
            string dBName = UnitDatabase.DatabaseName(ace, sal, group);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var dataAccount = UnitDatabase.ReadUserPassHeader(this.Request.Headers);
            var DBase = UnitDatabase.dataDB.Where(p => p.UserName.ToUpper() == dataAccount[0].ToUpper() && p.Password == dataAccount[1]).Single();
            string res = UnitDatabase.TestAcount(DBase, dataAccount[3], ace, group, AFI_IDocBi[0].InOut == 1 ? UnitPublic.access_IIDOC : UnitPublic.access_IODOC);
            if (res == "")
            {
                int value;
                int i = 0;
                try
                {
                    foreach (var item in AFI_IDocBi)
                    {
                        i++;
                        string sql = string.Format(CultureInfo.InvariantCulture,
                            @"DECLARE	@return_value int
                            EXEC	@return_value = {34}.[dbo].[Web_SaveIDoc_BI_Temp]
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
                                    @ArzValue = {16},
                                    @KalaFileNo = N'{17}',
                                    @KalaState = N'{18}',
                                    @KalaExf1 = N'{19}',
                                    @KalaExf2 = N'{20}',
                                    @KalaExf3 = N'{21}',
                                    @KalaExf4 = N'{22}',
                                    @KalaExf5 = N'{23}',
                                    @KalaExf6 = N'{24}',
                                    @KalaExf7 = N'{25}',
                                    @KalaExf8 = N'{26}',
                                    @KalaExf9 = N'{27}',
                                    @KalaExf10 = N'{28}',
                                    @KalaExf11 = N'{29}',
                                    @KalaExf12 = N'{30}',
                                    @KalaExf13 = N'{31}',
                                    @KalaExf14 = N'{32}',
                                    @KalaExf15 = N'{33}'
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
                        );
                        value = DBase.DB.Database.SqlQuery<int>(sql).Single();
                    }
                    await DBase.DB.SaveChangesAsync();

                }
                catch (Exception)
                {
                    throw;
                }
               // UnitDatabase.SaveLog(dataAccount[0], dataAccount[1], dataAccount[2], ace, sal, group, serialNumber, AFI_IDocBi[0].InOut == 1 ? UnitPublic.access_IIDOC : UnitPublic.access_IODOC, 5, "Y", 1, 0);
                UnitDatabase.SaveLog(dataAccount[0], dataAccount[1], dataAccount[2], ace, sal, group, serialNumber, AFI_IDocBi[0].InOut == 1 ? UnitPublic.access_IIDOC : UnitPublic.access_IODOC, UnitPublic.act_NewBand, "Y", 1, 0);
                return Ok("OK");
            }
            else
                return Ok(res);

            //string conStr = UnitDatabase.CreateConnectionString(dataAccount[0], dataAccount[1], dataAccount[2], dataAccount[3], ace, sal, group, serialNumber, AFI_IDocBi[0].InOut == 1 ? UnitPublic.access_IIDOC : UnitPublic.access_IODOC, UnitPublic.act_NewBand, 0);
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
            string dBName = UnitDatabase.DatabaseName(ace, sal, group);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var dataAccount = UnitDatabase.ReadUserPassHeader(this.Request.Headers);
            var DBase = UnitDatabase.dataDB.Where(p => p.UserName.ToUpper() == dataAccount[0].ToUpper() && p.Password == dataAccount[1]).Single();
            string res = UnitDatabase.TestAcount(DBase, dataAccount[3], ace, group, InOut == "1" ? UnitPublic.access_IIDOC : UnitPublic.access_IODOC);
            if (res == "")
            {
                try
                {
                    string sql = string.Format(CultureInfo.InvariantCulture,
                                  @"DECLARE	@return_value int
                                    EXEC	@return_value = {0}.[dbo].[Web_SaveIDocB_Convert]
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
                // UnitDatabase.SaveLog(dataAccount[0], dataAccount[1], dataAccount[2], ace, sal, group, ConvertObject.SerialNumber, InOut == "1" ? UnitPublic.access_IIDOC : UnitPublic.access_IODOC, 1, "Y", 1, 0);
                UnitDatabase.SaveLog(dataAccount[0], dataAccount[1], dataAccount[2], ace, sal, group, ConvertObject.SerialNumber, InOut == "1" ? UnitPublic.access_IIDOC : UnitPublic.access_IODOC, UnitPublic.act_NewBand, "Y", 1, 0);

                return Ok("OK");
            }
            else
                return Ok(res);

            //string conStr = UnitDatabase.CreateConnectionString(dataAccount[0], dataAccount[1], dataAccount[2], dataAccount[3], ace, sal, group, ConvertObject.SerialNumber, InOut == "1" ? UnitPublic.access_IIDOC : UnitPublic.access_IODOC, UnitPublic.act_NewBand, 0);
        }

    }
}