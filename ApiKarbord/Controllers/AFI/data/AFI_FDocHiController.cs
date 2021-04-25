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
    public class AFI_FDocHiController : ApiController
    {
        // PUT: api/AFI_FDocHi/5
        [Route("api/AFI_FDocHi/{ace}/{sal}/{group}")]
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutAFI_FDocHi(string ace, string sal, string group, AFI_FDocHi aFI_FDocHi)
        {

            byte forSale;


            if (aFI_FDocHi.ModeCode == "51" || aFI_FDocHi.ModeCode == "52" || aFI_FDocHi.ModeCode == "53" ||
                aFI_FDocHi.ModeCode == "SPFCT" || aFI_FDocHi.ModeCode == "SFCT" || aFI_FDocHi.ModeCode == "SRFCT" || aFI_FDocHi.ModeCode == "SORD")
                forSale = 1;
            else
                forSale = 0;


            string value = "";
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var dataAccount = UnitDatabase.ReadUserPassHeader(this.Request.Headers);
            string con = UnitDatabase.CreateConection(dataAccount[0], dataAccount[1], dataAccount[2], ace, sal, group, aFI_FDocHi.SerialNumber, aFI_FDocHi.ModeCode, 1, 0);
            if (con == "ok")
            {
                try
                {
                    string sql = string.Format(CultureInfo.InvariantCulture, @"EXEC	[dbo].[Web_Calc_AddMin_EffPrice]
		                                            @serialNumber = {0},
                                                    @forSale = {1},
                                                    @custCode = '{2}',
                                                    @TypeJob = {3},                                                    
                                                    @Spec1 = '{4}',
                                                    @Spec2 = '{5}',
                                                    @Spec3 = '{6}',
                                                    @Spec4 = '{7}',
                                                    @Spec5 = '{8}',
                                                    @Spec6 = '{9}',
                                                    @Spec7 = '{10}',
                                                    @Spec8 = '{11}',
                                                    @Spec9 = '{12}',
                                                    @Spec10 = '{13}',                                                    
                                                    @MP1 = {14},
                                                    @MP2 = {15},
                                                    @MP3 = {16},
		                                            @MP4 = {17},
		                                            @MP5 = {18},
		                                            @MP6 = {19},
		                                            @MP7 = {20},
		                                            @MP8 = {21},
		                                            @MP9 = {22},
		                                            @MP10 = {23} ",
                                                    aFI_FDocHi.SerialNumber,
                                                    forSale,
                                                    aFI_FDocHi.CustCode,
                                                    0,
                                                    aFI_FDocHi.AddMinSpec1,
                                                    aFI_FDocHi.AddMinSpec2,
                                                    aFI_FDocHi.AddMinSpec3,
                                                    aFI_FDocHi.AddMinSpec4,
                                                    aFI_FDocHi.AddMinSpec5,
                                                    aFI_FDocHi.AddMinSpec6,
                                                    aFI_FDocHi.AddMinSpec7,
                                                    aFI_FDocHi.AddMinSpec8,
                                                    aFI_FDocHi.AddMinSpec9,
                                                    aFI_FDocHi.AddMinSpec10,
                                                    Math.Abs(aFI_FDocHi.AddMinPrice1 ?? 0),
                                                    Math.Abs(aFI_FDocHi.AddMinPrice2 ?? 0),
                                                    Math.Abs(aFI_FDocHi.AddMinPrice3 ?? 0),
                                                    Math.Abs(aFI_FDocHi.AddMinPrice4 ?? 0),
                                                    Math.Abs(aFI_FDocHi.AddMinPrice5 ?? 0),
                                                    Math.Abs(aFI_FDocHi.AddMinPrice6 ?? 0),
                                                    Math.Abs(aFI_FDocHi.AddMinPrice7 ?? 0),
                                                    Math.Abs(aFI_FDocHi.AddMinPrice8 ?? 0),
                                                    Math.Abs(aFI_FDocHi.AddMinPrice9 ?? 0),
                                                    Math.Abs(aFI_FDocHi.AddMinPrice10 ?? 0));
                    var result = UnitDatabase.db.Database.SqlQuery<AddMin>(sql).Where(c => c.Name != "").ToList();



                    foreach (var item in result)
                    {
                        if (item.Code == 1) aFI_FDocHi.AddMinPrice1 = Math.Round(item.AddMinPrice ?? 0, aFI_FDocHi.deghat);
                        if (item.Code == 2) aFI_FDocHi.AddMinPrice2 = Math.Round(item.AddMinPrice ?? 0, aFI_FDocHi.deghat);
                        if (item.Code == 3) aFI_FDocHi.AddMinPrice3 = Math.Round(item.AddMinPrice ?? 0, aFI_FDocHi.deghat);
                        if (item.Code == 4) aFI_FDocHi.AddMinPrice4 = Math.Round(item.AddMinPrice ?? 0, aFI_FDocHi.deghat);
                        if (item.Code == 5) aFI_FDocHi.AddMinPrice5 = Math.Round(item.AddMinPrice ?? 0, aFI_FDocHi.deghat);
                        if (item.Code == 6) aFI_FDocHi.AddMinPrice6 = Math.Round(item.AddMinPrice ?? 0, aFI_FDocHi.deghat);
                        if (item.Code == 7) aFI_FDocHi.AddMinPrice7 = Math.Round(item.AddMinPrice ?? 0, aFI_FDocHi.deghat);
                        if (item.Code == 8) aFI_FDocHi.AddMinPrice8 = Math.Round(item.AddMinPrice ?? 0, aFI_FDocHi.deghat);
                        if (item.Code == 9) aFI_FDocHi.AddMinPrice9 = Math.Round(item.AddMinPrice ?? 0, aFI_FDocHi.deghat);
                        if (item.Code == 10) aFI_FDocHi.AddMinPrice10 = Math.Round(item.AddMinPrice ?? 0, aFI_FDocHi.deghat);
                    }

                    var sql1 = string.Format(CultureInfo.InvariantCulture, @"DECLARE	@return_value int
                            EXEC	@return_value = [dbo].[Web_FDocB_CalcAddMin]
		                                @serialNumber = {0},
		                                @deghat = {1},
                                        @forSale = {2},
		                                @MP1 = {3},
		                                @MP2 = {4},
		                                @MP3 = {5},
		                                @MP4 = {6},
		                                @MP5 = {7},
		                                @MP6 = {8},
		                                @MP7 = {9},
		                                @MP8 = {10},
		                                @MP9 = {11},
		                                @MP10 = {12}
                            SELECT	'Return Value' = @return_value",
                            aFI_FDocHi.SerialNumber,
                            aFI_FDocHi.deghat,
                            forSale,
                            aFI_FDocHi.AddMinPrice1,
                            aFI_FDocHi.AddMinPrice2,
                            aFI_FDocHi.AddMinPrice3,
                            aFI_FDocHi.AddMinPrice4,
                            aFI_FDocHi.AddMinPrice5,
                            aFI_FDocHi.AddMinPrice6,
                            aFI_FDocHi.AddMinPrice7,
                            aFI_FDocHi.AddMinPrice8,
                            aFI_FDocHi.AddMinPrice9,
                            aFI_FDocHi.AddMinPrice10);
                    int test = UnitDatabase.db.Database.SqlQuery<int>(sql1).Single();

                    string sql2 = string.Format(CultureInfo.InvariantCulture,
                         @"DECLARE	@return_value nvarchar(50),
                                    @DocNo_Out nvarchar(50)
                          EXEC	@return_value = [dbo].[Web_SaveFDoc_HU]
		                            @DOCNOMODE = {0},
		                            @INSERTMODE = {1},
		                            @MODECODE = {2} ,
		                            @DOCNO = '{3}',
		                            @STARTNO = {4},
		                            @ENDNO = {5},
		                            @BRANCHCODE = {6},
		                            @USERCODE = '{7}',
		                            @SERIALNUMBER = {8},
		                            @DOCDATE = '{9}',
		                            @SPEC = '{10}',
		                            @TANZIM = {11},
		                            @TAHIESHODE = {12},
		                            @CUSTCODE = '{13}',
		                            @VSTRCODE = {14},
		                            @VSTRPER = {15},
		                            @PAKHSHCODE = '{16}',
		                            @KALAPRICECODE = {17},
		                            @ADDMINSPEC1 = '{18}',
		                            @ADDMINSPEC2 = '{19}',
		                            @ADDMINSPEC3 = '{20}',
		                            @ADDMINSPEC4 = '{21}',
		                            @ADDMINSPEC5 = '{22}',
		                            @ADDMINSPEC6 = '{23}',
		                            @ADDMINSPEC7 = '{24}',
		                            @ADDMINSPEC8 = '{25}',
		                            @ADDMINSPEC9 = '{26}',
		                            @ADDMINSPEC10 = '{27}',
		                            @ADDMINPRICE1 = {28},
		                            @ADDMINPRICE2 = {29},
		                            @ADDMINPRICE3 = {30},
		                            @ADDMINPRICE4 = {31},
		                            @ADDMINPRICE5 = {32},
		                            @ADDMINPRICE6 = {33},
		                            @ADDMINPRICE7 = {34},
		                            @ADDMINPRICE8 = {35},
		                            @ADDMINPRICE9 = {36},
		                            @ADDMINPRICE10 = {37},
                                    @InvCode = '{38}',
                                    @Status = N'{39}',
									@PaymentType = {40},
                                    @Footer = '{41}',
                                    @Taeed='{42}',
                                    @F01 = '{43}',
                                    @F02 = '{44}',
                                    @F03 = '{45}',
                                    @F04 = '{46}',
                                    @F05 = '{47}',
                                    @F06 = '{48}',
                                    @F07 = '{49}',
                                    @F08 = '{50}',
                                    @F09 = '{51}',
                                    @F10 = '{52}',
                                    @F11 = '{53}',
                                    @F12 = '{54}',
                                    @F13 = '{55}',
                                    @F14 = '{56}',
                                    @F15 = '{57}',
                                    @F16 = '{58}',
                                    @F17 = '{59}',
                                    @F18 = '{60}',
                                    @F19 = '{61}',
                                    @F20 = '{62}',
                                    @Tasvib = '{63}', 
                                    @OprCode = '{64}', 
                                    @MkzCode = '{65}',
                                    @DOCNO_OUT = @DOCNO_OUT OUTPUT
                            SELECT	'return_value' = ltrim(@DOCNO_OUT)
                           ",
                            aFI_FDocHi.DocNoMode,
                            aFI_FDocHi.InsertMode,
                            aFI_FDocHi.ModeCode,
                            aFI_FDocHi.DocNo,
                            aFI_FDocHi.StartNo,
                            aFI_FDocHi.EndNo,
                            aFI_FDocHi.BranchCode,
                            aFI_FDocHi.UserCode,
                            aFI_FDocHi.SerialNumber,
                            aFI_FDocHi.DocDate ?? string.Format("{ 0:yyyy/MM/dd}", DateTime.Now.AddDays(-1)),
                            aFI_FDocHi.Spec,
                            aFI_FDocHi.Tanzim,
                            aFI_FDocHi.TahieShode,
                            aFI_FDocHi.CustCode,
                            aFI_FDocHi.VstrCode,
                            aFI_FDocHi.VstrPer,
                            aFI_FDocHi.PakhshCode,
                            aFI_FDocHi.KalaPriceCode ?? 0,
                            aFI_FDocHi.AddMinSpec1,
                            aFI_FDocHi.AddMinSpec2,
                            aFI_FDocHi.AddMinSpec3,
                            aFI_FDocHi.AddMinSpec4,
                            aFI_FDocHi.AddMinSpec5,
                            aFI_FDocHi.AddMinSpec6,
                            aFI_FDocHi.AddMinSpec7,
                            aFI_FDocHi.AddMinSpec8,
                            aFI_FDocHi.AddMinSpec9,
                            aFI_FDocHi.AddMinSpec10,
                            Math.Abs((decimal)aFI_FDocHi.AddMinPrice1),
                            Math.Abs((decimal)aFI_FDocHi.AddMinPrice2),
                            Math.Abs((decimal)aFI_FDocHi.AddMinPrice3),
                            Math.Abs((decimal)aFI_FDocHi.AddMinPrice4),
                            Math.Abs((decimal)aFI_FDocHi.AddMinPrice5),
                            Math.Abs((decimal)aFI_FDocHi.AddMinPrice6),
                            Math.Abs((decimal)aFI_FDocHi.AddMinPrice7),
                            Math.Abs((decimal)aFI_FDocHi.AddMinPrice8),
                            Math.Abs((decimal)aFI_FDocHi.AddMinPrice9),
                            Math.Abs((decimal)aFI_FDocHi.AddMinPrice10),
                            aFI_FDocHi.InvCode,
                            aFI_FDocHi.Status,
                            aFI_FDocHi.PaymentType,
                            UnitPublic.ConvertTextWebToWin(aFI_FDocHi.Footer),
                            aFI_FDocHi.Taeed == "null" ? "" : aFI_FDocHi.Taeed,
                            aFI_FDocHi.F01,
                            aFI_FDocHi.F02,
                            aFI_FDocHi.F03,
                            aFI_FDocHi.F04,
                            aFI_FDocHi.F05,
                            aFI_FDocHi.F06,
                            aFI_FDocHi.F07,
                            aFI_FDocHi.F08,
                            aFI_FDocHi.F09,
                            aFI_FDocHi.F10,
                            aFI_FDocHi.F11,
                            aFI_FDocHi.F12,
                            aFI_FDocHi.F13,
                            aFI_FDocHi.F14,
                            aFI_FDocHi.F15,
                            aFI_FDocHi.F16,
                            aFI_FDocHi.F17,
                            aFI_FDocHi.F18,
                            aFI_FDocHi.F19,
                            aFI_FDocHi.F20,
                            aFI_FDocHi.Tasvib,
                            aFI_FDocHi.OprCode,
                            aFI_FDocHi.MkzCode
                            );
                    value = UnitDatabase.db.Database.SqlQuery<string>(sql2).Single();

                    await UnitDatabase.db.SaveChangesAsync();
                }
                catch (Exception e)
                {
                    throw;
                }

                UnitDatabase.SaveLog(dataAccount[0], dataAccount[1], dataAccount[2], ace, sal, group, aFI_FDocHi.SerialNumber, aFI_FDocHi.ModeCode, 1, aFI_FDocHi.flagLog, 0);
                return Ok(value);
            }
            return Ok(con);
        }

        // POST: api/AFI_FDocHi
        [Route("api/AFI_FDocHi/{ace}/{sal}/{group}")]
        [ResponseType(typeof(AFI_FDocHi))]
        public async Task<IHttpActionResult> PostAFI_FDocHi(string ace, string sal, string group, AFI_FDocHi aFI_FDocHi)
        {
            string value = "";
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var dataAccount = UnitDatabase.ReadUserPassHeader(this.Request.Headers);
            string con = UnitDatabase.CreateConection(dataAccount[0], dataAccount[1], dataAccount[2], ace, sal, group, aFI_FDocHi.SerialNumber, aFI_FDocHi.ModeCode, 2, 0);
            if (con == "ok")
            {
                try
                {
                    string sql = string.Format(
                         @"DECLARE	@return_value nvarchar(50),
		                            @DocNo_Out nvarchar(50)
                          EXEC	@return_value = [dbo].[Web_SaveFDoc_HI]
		                            @DOCNOMODE = {0},
		                            @INSERTMODE = {1},
		                            @MODECODE = {2} ,
		                            @DOCNO = '{3}',
		                            @STARTNO = {4},
		                            @ENDNO = {5},
		                            @BRANCHCODE = {6},
		                            @USERCODE = '''{7}''',
		                            @SERIALNUMBER = {8},
		                            @DOCDATE = '{9}',
		                            @SPEC = '{10}',
		                            @TANZIM = '{11}',
		                            @TAHIESHODE = '{12}',
		                            @CUSTCODE = '{13}',
		                            @VSTRCODE = '{14}',
		                            @VSTRPER = {15},
		                            @PAKHSHCODE = '{16}',
		                            @KALAPRICECODE = {17},
		                            @ADDMINSPEC1 = '{18}',
		                            @ADDMINSPEC2 = '{19}',
		                            @ADDMINSPEC3 = '{20}',
		                            @ADDMINSPEC4 = '{21}',
		                            @ADDMINSPEC5 = '{22}',
		                            @ADDMINSPEC6 = '{23}',
		                            @ADDMINSPEC7 = '{24}',
		                            @ADDMINSPEC8 = '{25}',
		                            @ADDMINSPEC9 = '{26}',
		                            @ADDMINSPEC10 = '{27}',
		                            @ADDMINPRICE1 = {28},
		                            @ADDMINPRICE2 = {29},
		                            @ADDMINPRICE3 = {30},
		                            @ADDMINPRICE4 = {31},
		                            @ADDMINPRICE5 = {32},
		                            @ADDMINPRICE6 = {33},
		                            @ADDMINPRICE7 = {34},
		                            @ADDMINPRICE8 = {35},
		                            @ADDMINPRICE9 = {36},
		                            @ADDMINPRICE10 = {37},
                                    @InvCode = '{38}',
                                    @Eghdam = '''{39}''',
                                    @F01 = '{40}',
                                    @F02 = '{41}',
                                    @F03 = '{42}',
                                    @F04 = '{43}',
                                    @F05 = '{44}',
                                    @F06 = '{45}',
                                    @F07 = '{46}',
                                    @F08 = '{47}',
                                    @F09 = '{48}',
                                    @F10 = '{49}',
                                    @F11 = '{50}',
                                    @F12 = '{51}',
                                    @F13 = '{52}',
                                    @F14 = '{53}',
                                    @F15 = '{54}',
                                    @F16 = '{55}',
                                    @F17 = '{56}',
                                    @F18 = '{57}',
                                    @F19 = '{58}',
                                    @F20 = '{59}',
		                            @DOCNO_OUT = @DOCNO_OUT OUTPUT
                            SELECT	'return_value' = @return_value +'@'+ ltrim(@DOCNO_OUT)",
                            aFI_FDocHi.DocNoMode,
                            aFI_FDocHi.InsertMode,
                            aFI_FDocHi.ModeCode,
                            aFI_FDocHi.DocNo,
                            aFI_FDocHi.StartNo,
                            aFI_FDocHi.EndNo,
                            aFI_FDocHi.BranchCode,
                            aFI_FDocHi.UserCode,
                            aFI_FDocHi.SerialNumber,
                            aFI_FDocHi.DocDate ?? string.Format("{ 0:yyyy/MM/dd}", DateTime.Now.AddDays(-1)),
                            aFI_FDocHi.Spec,
                            aFI_FDocHi.Tanzim,
                            aFI_FDocHi.TahieShode,
                            aFI_FDocHi.CustCode,
                            aFI_FDocHi.VstrCode,
                            aFI_FDocHi.VstrPer,
                            aFI_FDocHi.PakhshCode,
                            aFI_FDocHi.KalaPriceCode ?? 0,
                            aFI_FDocHi.AddMinSpec1,
                            aFI_FDocHi.AddMinSpec2,
                            aFI_FDocHi.AddMinSpec3,
                            aFI_FDocHi.AddMinSpec4,
                            aFI_FDocHi.AddMinSpec5,
                            aFI_FDocHi.AddMinSpec6,
                            aFI_FDocHi.AddMinSpec7,
                            aFI_FDocHi.AddMinSpec8,
                            aFI_FDocHi.AddMinSpec9,
                            aFI_FDocHi.AddMinSpec10,
                            aFI_FDocHi.AddMinPrice1,
                            aFI_FDocHi.AddMinPrice2,
                            aFI_FDocHi.AddMinPrice3,
                            aFI_FDocHi.AddMinPrice4,
                            aFI_FDocHi.AddMinPrice5,
                            aFI_FDocHi.AddMinPrice6,
                            aFI_FDocHi.AddMinPrice7,
                            aFI_FDocHi.AddMinPrice8,
                            aFI_FDocHi.AddMinPrice9,
                            aFI_FDocHi.AddMinPrice10,
                            aFI_FDocHi.InvCode,
                            aFI_FDocHi.Eghdam,
                            aFI_FDocHi.F01,
                            aFI_FDocHi.F02,
                            aFI_FDocHi.F03,
                            aFI_FDocHi.F04,
                            aFI_FDocHi.F05,
                            aFI_FDocHi.F06,
                            aFI_FDocHi.F07,
                            aFI_FDocHi.F08,
                            aFI_FDocHi.F09,
                            aFI_FDocHi.F10,
                            aFI_FDocHi.F11,
                            aFI_FDocHi.F12,
                            aFI_FDocHi.F13,
                            aFI_FDocHi.F14,
                            aFI_FDocHi.F15,
                            aFI_FDocHi.F16,
                            aFI_FDocHi.F17,
                            aFI_FDocHi.F18,
                            aFI_FDocHi.F19,
                            aFI_FDocHi.F20);
                    value = UnitDatabase.db.Database.SqlQuery<string>(sql).Single();
                    if (!string.IsNullOrEmpty(value))
                    {
                        await UnitDatabase.db.SaveChangesAsync();
                    }
                }
                catch (Exception e)
                {
                    throw;
                }

                string[] serials = value.Split('@');
                UnitDatabase.SaveLog(dataAccount[0], dataAccount[1], dataAccount[2], ace, sal, group, Convert.ToInt64(serials[0]), aFI_FDocHi.ModeCode, 2, aFI_FDocHi.flagLog, 0);
                return Ok(value);
            }
            return Ok(con);
        }

        // DELETE: api/AFI_FDocHi/5
        [Route("api/AFI_FDocHi/{ace}/{sal}/{group}/{SerialNumber}/{ModeCode}")]
        [ResponseType(typeof(AFI_FDocHi))]
        public async Task<IHttpActionResult> DeleteAFI_FDocHi(string ace, string sal, string group, long SerialNumber, string ModeCode)
        {
            var dataAccount = UnitDatabase.ReadUserPassHeader(this.Request.Headers);
            string con = UnitDatabase.CreateConection(dataAccount[0], dataAccount[1], dataAccount[2], ace, sal, group, SerialNumber, ModeCode, 3, 0);
            if (con == "ok")
            {
                try
                {
                    string sql = string.Format(@"DECLARE	@return_value int
                                                 EXEC	@return_value = [dbo].[Web_SaveFDoc_Del]
		                                                @SerialNumber = {0}
                                                 SELECT	'Return Value' = @return_value"
                                                , SerialNumber);

                    int value = UnitDatabase.db.Database.SqlQuery<int>(sql).Single();
                    if (value > 0)
                    {
                        await UnitDatabase.db.SaveChangesAsync();
                    }
                }
                catch (Exception e)
                {
                    throw;
                }



                UnitDatabase.SaveLog(dataAccount[0], dataAccount[1], dataAccount[2], ace, sal, group, SerialNumber, ModeCode, 3, "Y", 0);
                return Ok(1);
            }
            return Ok(con);
        }

    }
}