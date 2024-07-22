using System;
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
using System.Web;
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
            string dBName = UnitDatabase.DatabaseName(ace, sal, group);
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
            var DBase = UnitDatabase.dataDB.Where(p => p.UserName == dataAccount[0] && p.Password == dataAccount[1]).Single();
            string res = UnitDatabase.TestAcount(DBase, dataAccount[3], ace, group, UnitPublic.ModeCodeConnection(aFI_FDocHi.ModeCode));

            if (res == "")
            {
                try
                {
                    if (aFI_FDocHi.New != "Y")
                    {
                        string sql = string.Format(CultureInfo.InvariantCulture, @"EXEC	{24}.[dbo].[Web_Calc_AddMin_EffPrice]
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
                                                        Math.Abs(aFI_FDocHi.AddMinPrice10 ?? 0),
                                                        dBName);
                        var result = DBase.DB.Database.SqlQuery<AddMin>(sql).Where(c => c.Name != "").ToList();



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
                            EXEC	@return_value = {13}.[dbo].[Web_FDocB_CalcAddMin]
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
                                aFI_FDocHi.AddMinPrice10,
                                dBName);
                        int test = DBase.DB.Database.SqlQuery<int>(sql1).Single();
                    }
                    string sql2 = string.Format(CultureInfo.InvariantCulture,
                         @"DECLARE	@return_value nvarchar(50),
                                    @DocNo_Out nvarchar(50)
                          EXEC	@return_value = {75}.[dbo].[Web_SaveFDoc_HU]
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
		                            @SPEC = N'{10}',
		                            @TAHIESHODE = '{11}',
		                            @CUSTCODE = '{12}',
		                            @VSTRCODE = '{13}',
		                            @VSTRPER = {14},
		                            @PAKHSHCODE = '{15}',
		                            @KALAPRICECODE = {16},
		                            @ADDMINSPEC1 = N'{17}',
		                            @ADDMINSPEC2 = N'{18}',
		                            @ADDMINSPEC3 = N'{19}',
		                            @ADDMINSPEC4 = N'{20}',
		                            @ADDMINSPEC5 = N'{21}',
		                            @ADDMINSPEC6 = N'{22}',
		                            @ADDMINSPEC7 = N'{23}',
		                            @ADDMINSPEC8 = N'{24}',
		                            @ADDMINSPEC9 = N'{25}',
		                            @ADDMINSPEC10 = N'{26}',
		                            @ADDMINPRICE1 = {27},
		                            @ADDMINPRICE2 = {28},
		                            @ADDMINPRICE3 = {29},
		                            @ADDMINPRICE4 = {30},
		                            @ADDMINPRICE5 = {31},
		                            @ADDMINPRICE6 = {32},
		                            @ADDMINPRICE7 = {33},
		                            @ADDMINPRICE8 = {34},
		                            @ADDMINPRICE9 = {35},
		                            @ADDMINPRICE10 = {36},
                                    @InvCode = '{37}',
                                    @Status = N'{38}',
									@PaymentType = {39},
                                    @Footer = N'{40}',
                                    @F01 = N'{41}',
                                    @F02 = N'{42}',
                                    @F03 = N'{43}',
                                    @F04 = N'{44}',
                                    @F05 = N'{45}',
                                    @F06 = N'{46}',
                                    @F07 = N'{47}',
                                    @F08 = N'{48}',
                                    @F09 = N'{49}',
                                    @F10 = N'{50}',
                                    @F11 = N'{51}',
                                    @F12 = N'{52}',
                                    @F13 = N'{53}',
                                    @F14 = N'{54}',
                                    @F15 = N'{55}',
                                    @F16 = N'{56}',
                                    @F17 = N'{57}',
                                    @F18 = N'{58}',
                                    @F19 = N'{59}',
                                    @F20 = N'{60}',
                                    @OprCode = '{61}', 
                                    @MkzCode = '{62}',
                                    @Tanzim = '{63}',
                                    @CustAddrValid = {64},
                                    @CustOstan = '{65}',
                                    @CustShahrestan = '{66}',
                                    @CustRegion = '{67}',
                                    @CustCity = '{68}',
                                    @CustStreet = '{69}',
                                    @CustAlley = '{70}',
                                    @CustPlack = '{71}',
                                    @CustZipCode = '{72}',
                                    @CustTel = '{73}',
                                    @CustMobile = '{74}',
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
                            aFI_FDocHi.DocDate ?? string.Format("{0:yyyy/MM/dd}", DateTime.Now.AddDays(-1)),
                            aFI_FDocHi.Spec,
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
                            UnitPublic.ConvertTextWebToWin(aFI_FDocHi.Footer ?? ""),
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
                            aFI_FDocHi.OprCode,
                            aFI_FDocHi.MkzCode,
                            aFI_FDocHi.Tanzim,
                            aFI_FDocHi.CustAddrValid ?? 0,
                            aFI_FDocHi.CustOstan,
                            aFI_FDocHi.CustShahrestan,
                            aFI_FDocHi.CustRegion,
                            aFI_FDocHi.CustCity,
                            aFI_FDocHi.CustStreet,
                            aFI_FDocHi.CustAlley,
                            aFI_FDocHi.CustPlack,
                            aFI_FDocHi.CustZipCode,
                            aFI_FDocHi.CustTel,
                            aFI_FDocHi.CustMobile,
                            dBName
                            );
                    value = DBase.DB.Database.SqlQuery<string>(sql2).Single();

                    await DBase.DB.SaveChangesAsync();
                }
                catch (Exception e)
                {
                    throw;
                }

                UnitDatabase.SaveLog(dataAccount[0], dataAccount[1], dataAccount[2], ace, sal, group, aFI_FDocHi.SerialNumber, UnitPublic.ModeCodeConnection(aFI_FDocHi.ModeCode), UnitPublic.act_Edit, aFI_FDocHi.flagLog, 1, 0);
                return Ok(value);
            }
            else
                return Ok(res);

            //string conStr = UnitDatabase.CreateConnectionString(dataAccount[0], dataAccount[1], dataAccount[2], dataAccount[3], ace, sal, group, aFI_FDocHi.SerialNumber, UnitPublic.ModeCodeConnection(aFI_FDocHi.ModeCode), UnitPublic.act_Edit, 0);
        }

        // POST: api/AFI_FDocHi
        [Route("api/AFI_FDocHi/{ace}/{sal}/{group}")]
        [ResponseType(typeof(AFI_FDocHi))]
        public async Task<IHttpActionResult> PostAFI_FDocHi(string ace, string sal, string group, AFI_FDocHi aFI_FDocHi)
        {
            string dBName = UnitDatabase.DatabaseName(ace, sal, group);
            string value = "";
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var dataAccount = UnitDatabase.ReadUserPassHeader(this.Request.Headers);
            var DBase = UnitDatabase.dataDB.Where(p => p.UserName == dataAccount[0] && p.Password == dataAccount[1]).Single();
            string res = UnitDatabase.TestAcount(DBase, dataAccount[3], ace, group, UnitPublic.ModeCodeConnection(aFI_FDocHi.ModeCode));

            if (res == "")
            {
                try
                {
                    string sql = string.Format(
                          @"DECLARE	@return_value nvarchar(50),
		                            @DocNo_Out nvarchar(50)
                          EXEC	@return_value = {75}.[dbo].[{0}]
		                            @DOCNOMODE = {1},
		                            @INSERTMODE = {2},
		                            @MODECODE = {3} ,
		                            @DOCNO = '{4}',
		                            @STARTNO = {5},
		                            @ENDNO = {6},
		                            @BRANCHCODE = {7},
		                            @USERCODE = '''{8}''',
		                            @SERIALNUMBER = {9},
		                            @DOCDATE = '{10}',
		                            @SPEC = N'{11}',
		                            @TAHIESHODE = '{12}',
		                            @CUSTCODE = '{13}',
		                            @VSTRCODE = '{14}',
		                            @VSTRPER = {15},
		                            @PAKHSHCODE = '{16}',
		                            @KALAPRICECODE = {17},
		                            @ADDMINSPEC1 = N'{18}',
		                            @ADDMINSPEC2 = N'{19}',
		                            @ADDMINSPEC3 = N'{20}',
		                            @ADDMINSPEC4 = N'{21}',
		                            @ADDMINSPEC5 = N'{22}',
		                            @ADDMINSPEC6 = N'{23}',
		                            @ADDMINSPEC7 = N'{24}',
		                            @ADDMINSPEC8 = N'{25}',
		                            @ADDMINSPEC9 = N'{26}',
		                            @ADDMINSPEC10 = N'{27}',
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
                                    @F01 = N'{40}',
                                    @F02 = N'{41}',
                                    @F03 = N'{42}',
                                    @F04 = N'{43}',
                                    @F05 = N'{44}',
                                    @F06 = N'{45}',
                                    @F07 = N'{46}',
                                    @F08 = N'{47}',
                                    @F09 = N'{48}',
                                    @F10 = N'{49}',
                                    @F11 = N'{50}',
                                    @F12 = N'{51}',
                                    @F13 = N'{52}',
                                    @F14 = N'{53}',
                                    @F15 = N'{54}',
                                    @F16 = N'{55}',
                                    @F17 = N'{56}',
                                    @F18 = N'{57}',
                                    @F19 = N'{58}',
                                    @F20 = N'{59}',
                                    @PaymentType = {60},
                                    @Footer = N'{61}',
                                    @TotalValue = '{62}',
                                    @Tanzim = '{63}',
                                    @CustAddrValid = {64},
                                    @CustOstan = '{65}',
                                    @CustShahrestan = '{66}',
                                    @CustRegion = '{67}',
                                    @CustCity = '{68}',
                                    @CustStreet = '{69}',
                                    @CustAlley = '{70}',
                                    @CustPlack = '{71}',
                                    @CustZipCode = '{72}',
                                    @CustTel = '{73}',
                                    @CustMobile = '{74}',
		                            @DOCNO_OUT = @DOCNO_OUT OUTPUT
                            SELECT	'return_value' = @return_value +'@'+ ltrim(@DOCNO_OUT)",
                             aFI_FDocHi.flagTest == "Y" ? "Web_SaveFDoc_HI_Temp" : "Web_SaveFDoc_HI",
                             aFI_FDocHi.DocNoMode,
                             aFI_FDocHi.InsertMode,
                             aFI_FDocHi.ModeCode,
                             aFI_FDocHi.DocNo,
                             aFI_FDocHi.StartNo,
                             aFI_FDocHi.EndNo,
                             aFI_FDocHi.BranchCode,
                             aFI_FDocHi.UserCode,
                             aFI_FDocHi.SerialNumber,
                             aFI_FDocHi.DocDate ?? string.Format("{0:yyyy/MM/dd}", DateTime.Now.AddDays(-1)),
                             aFI_FDocHi.Spec,
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
                             aFI_FDocHi.F20,
                             aFI_FDocHi.PaymentType,
                             UnitPublic.ConvertTextWebToWin(aFI_FDocHi.Footer ?? ""),
                             aFI_FDocHi.TotalValue ?? 0,
                             aFI_FDocHi.Tanzim,
                             aFI_FDocHi.CustAddrValid ?? 0,
                             aFI_FDocHi.CustOstan,
                             aFI_FDocHi.CustShahrestan,
                             aFI_FDocHi.CustRegion,
                             aFI_FDocHi.CustCity,
                             aFI_FDocHi.CustStreet,
                             aFI_FDocHi.CustAlley,
                             aFI_FDocHi.CustPlack,
                             aFI_FDocHi.CustZipCode,
                             aFI_FDocHi.CustTel,
                             aFI_FDocHi.CustMobile,
                             dBName
                             );
                    value = DBase.DB.Database.SqlQuery<string>(sql).Single();
                    if (!string.IsNullOrEmpty(value))
                    {
                        await DBase.DB.SaveChangesAsync();
                    }
                }
                catch (Exception e)
                {
                    throw;
                }

                string[] serials = value.Split('@');
                if (aFI_FDocHi.flagTest != "Y")
                {
                    UnitDatabase.SaveLog(dataAccount[0], dataAccount[1], dataAccount[2], ace, sal, group, Convert.ToInt64(serials[0]), UnitPublic.ModeCodeConnection(aFI_FDocHi.ModeCode), UnitPublic.act_New, aFI_FDocHi.flagLog, 1, 0);
                }
                return Ok(value);
            }
            else
                return Ok(res);

           //string conStr = UnitDatabase.CreateConnectionString(dataAccount[0], dataAccount[1], dataAccount[2], dataAccount[3], ace, sal, group, aFI_FDocHi.SerialNumber, UnitPublic.ModeCodeConnection(aFI_FDocHi.ModeCode), UnitPublic.act_New, 0);
        }

        // DELETE: api/AFI_FDocHi/5
        [Route("api/AFI_FDocHi/{ace}/{sal}/{group}/{SerialNumber}/{ModeCode}")]
        [ResponseType(typeof(AFI_FDocHi))]
        public async Task<IHttpActionResult> DeleteAFI_FDocHi(string ace, string sal, string group, long SerialNumber, string ModeCode)
        {
            string dBName = UnitDatabase.DatabaseName(ace, sal, group);
            var dataAccount = UnitDatabase.ReadUserPassHeader(this.Request.Headers);
            var DBase = UnitDatabase.dataDB.Where(p => p.UserName == dataAccount[0] && p.Password == dataAccount[1]).Single();
            string res = UnitDatabase.TestAcount(DBase, dataAccount[3], ace, group, UnitPublic.ModeCodeConnection(ModeCode));

            if (res == "")
            {
                try
                {
                    string sql = string.Format(@"DECLARE	@return_value int
                                                 EXEC	@return_value = {0}.[dbo].[Web_SaveFDoc_Del]
		                                                @SerialNumber = {1}
                                                 SELECT	'Return Value' = @return_value",
                                                dBName, SerialNumber);

                    int value = DBase.DB.Database.SqlQuery<int>(sql).Single();
                    if (value > 0)
                    {
                        await DBase.DB.SaveChangesAsync();
                    }
                }
                catch (Exception e)
                {
                    throw;
                }
                UnitDatabase.SaveLog(dataAccount[0], dataAccount[1], dataAccount[2], ace, sal, group, SerialNumber, UnitPublic.ModeCodeConnection(ModeCode), UnitPublic.act_Delete, "Y", 1, 0);
                return Ok(1);
            }
            else
                return Ok(res);


            //string conStr = UnitDatabase.CreateConnectionString(dataAccount[0], dataAccount[1], dataAccount[2], dataAccount[3], ace, sal, group, SerialNumber, UnitPublic.ModeCodeConnection(ModeCode), UnitPublic.act_Delete, 0);
        }


        [DllImport("kernel32.dll", EntryPoint = "LoadLibrary")]
        static extern int LoadLibrary([MarshalAs(UnmanagedType.LPStr)] string lpLibFileName);

        [DllImport("kernel32.dll", EntryPoint = "GetProcAddress")]
        static extern IntPtr GetProcAddress(int hModule, [MarshalAs(UnmanagedType.LPStr)] string lpProcName);

        [DllImport("kernel32.dll", EntryPoint = "FreeLibrary")]
        static extern bool FreeLibrary(int hModule);


        [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Unicode)]
        delegate bool RegFDoctoADoc(string ConnetionString, string wDBase, string wUserCode, string wModeCode, string SerialNumbers, StringBuilder RetVal);

        public static string CallRegFDoctoADoc(string ace, string ConnetionString, string wDBase, string wUserCode, string wModeCode, string SerialNumbers)
        {
            string dllName = ace == UnitPublic.Web8 ? "Fct6_Web.dll" : "Afi2_Web.dll";
            string dllPath = HttpContext.Current.Server.MapPath("~/Content/Dll/" + dllName);
            const string functionName = "RegFDoctoADoc";

            int libHandle = LoadLibrary(dllPath);
            if (libHandle == 0)
                return string.Format("Could not load library \"{0}\"", dllPath);
            try
            {
                var delphiFunctionAddress = GetProcAddress(libHandle, functionName);
                if (delphiFunctionAddress == IntPtr.Zero)
                    return string.Format("Can't find function \"{0}\" in library \"{1}\"", functionName, dllPath);

                var delphiFunction = (RegFDoctoADoc)Marshal.GetDelegateForFunctionPointer(delphiFunctionAddress, typeof(RegFDoctoADoc));

                StringBuilder RetVal = new StringBuilder(1024);
                delphiFunction(
                    ConnetionString,
                    wDBase,
                    wUserCode,
                    wModeCode,
                    SerialNumbers,
                    RetVal);
                return RetVal.ToString();
            }
            finally
            {
                FreeLibrary(libHandle);
            }
        }




        [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Unicode)]
        delegate bool RegFDoctoIDoc(string ConnetionString, string wDBase, string wUserCode, string wModeCode, string SerialNumbers, StringBuilder RetVal);


        public static string CallRegFDoctoIDoc(string ace, string ConnetionString, string wDBase, string wUserCode, string wModeCode, string SerialNumbers)
        {
            string dllName = ace == UnitPublic.Web8 ? "Fct6_Web.dll" : "Afi2_Web.dll";
            string dllPath = HttpContext.Current.Server.MapPath("~/Content/Dll/" + dllName);
            const string functionName = "RegFDoctoIDoc";

            int libHandle = LoadLibrary(dllPath);
            if (libHandle == 0)
                return string.Format("Could not load library \"{0}\"", dllPath);
            try
            {
                var delphiFunctionAddress = GetProcAddress(libHandle, functionName);
                if (delphiFunctionAddress == IntPtr.Zero)
                    return string.Format("Can't find function \"{0}\" in library \"{1}\"", functionName, dllPath);

                var delphiFunction = (RegFDoctoIDoc)Marshal.GetDelegateForFunctionPointer(delphiFunctionAddress, typeof(RegFDoctoIDoc));

                StringBuilder RetVal = new StringBuilder(1024);
                delphiFunction(
                    ConnetionString,
                    wDBase,
                    wUserCode,
                    wModeCode,
                    SerialNumbers,
                    RetVal);
                return RetVal.ToString();
            }
            finally
            {
                FreeLibrary(libHandle);
            }
        }



        public class RegFDocToADocObject
        {
            public string SerialNumbers { get; set; }
            public string ModeCode { get; set; }

        }


        [Route("api/AFI_FDocHi/AFI_RegFDocToADoc/{ace}/{sal}/{group}")]
        public async Task<IHttpActionResult> PostAFI_RegFDocToADoc(string ace, string sal, string group, RegFDocToADocObject RegFDocToADocObject)
        {
            string dBName = UnitDatabase.DatabaseName(ace, sal, group);
            string log = "";
            var dataAccount = UnitDatabase.ReadUserPassHeader(this.Request.Headers);
            var DBase = UnitDatabase.dataDB.Where(p => p.UserName == dataAccount[0] && p.Password == dataAccount[1]).Single();
            string res = UnitDatabase.TestAcount(DBase, dataAccount[3], ace, group, UnitPublic.access_View);

            if (res == "")
            {
                string dbName = UnitDatabase.DatabaseName(ace, sal, group);
                string connectionString = string.Format(
                         @"Provider =SQLOLEDB.1;Password={0};Persist Security Info=True;User ID={1};Initial Catalog={2};Data Source={3}",
                         UnitDatabase.SqlPassword,
                         UnitDatabase.SqlUserName,
                         dbName,
                         UnitDatabase.SqlServerName
                         );

                log = CallRegFDoctoADoc(
                    ace,
                    connectionString,
                    dbName,
                    dataAccount[2],
                    RegFDocToADocObject.ModeCode,
                    RegFDocToADocObject.SerialNumbers
                    );

                UnitDatabase.SaveLog(dataAccount[0], dataAccount[1], dataAccount[2], ace, sal, group, 0, "FDoc", 3, "Y", 1, 0);
                return Ok("OK");
            }
            else
                return Ok(res);


            //string conStr = UnitDatabase.CreateConnectionString(dataAccount[0], dataAccount[1], dataAccount[2], dataAccount[3], ace, sal, group, 0, UnitPublic.access_View, UnitPublic.act_View, 0);
        }



        public class RegFDocToIDocObject
        {
            public string SerialNumbers { get; set; }
            public string ModeCode { get; set; }

        }


        [Route("api/AFI_FDocHi/AFI_RegFDocToIDoc/{ace}/{sal}/{group}")]
        public async Task<IHttpActionResult> PostAFI_RegFDocToIDoc(string ace, string sal, string group, RegFDocToIDocObject RegFDocToIDocObject)
        {
            string dBName = UnitDatabase.DatabaseName(ace, sal, group);
            string log = "";
            var dataAccount = UnitDatabase.ReadUserPassHeader(this.Request.Headers);
            var DBase = UnitDatabase.dataDB.Where(p => p.UserName == dataAccount[0] && p.Password == dataAccount[1]).Single();
            string res = UnitDatabase.TestAcount(DBase, dataAccount[3], ace, group, UnitPublic.access_View);

            if (res == "")
            {
                string dbName = UnitDatabase.DatabaseName(ace, sal, group);
                string connectionString = string.Format(
                         @"Provider =SQLOLEDB.1;Password={0};Persist Security Info=True;User ID={1};Initial Catalog={2};Data Source={3}",
                         UnitDatabase.SqlPassword,
                         UnitDatabase.SqlUserName,
                         dbName,
                         UnitDatabase.SqlServerName
                         );

                log = CallRegFDoctoIDoc(
                    ace, connectionString,
                    dbName,
                    dataAccount[2],
                    RegFDocToIDocObject.ModeCode,
                    RegFDocToIDocObject.SerialNumbers
                    );

                UnitDatabase.SaveLog(dataAccount[0], dataAccount[1], dataAccount[2], ace, sal, group, 0, "FDoc", 3, "Y", 1, 0);
                return Ok("OK");
            }
            else
                return Ok(res);

            //string conStr = UnitDatabase.CreateConnectionString(dataAccount[0], dataAccount[1], dataAccount[2], dataAccount[3], ace, sal, group, 0, UnitPublic.access_View, UnitPublic.act_View, 0);
        }






        public class SaveAllSanadObject
        {
            public long SerialSanad { get; set; }

            public AFI_FDocHi Head { get; set; }

            public List<AFI_FDocBi> Bands { get; set; }

        }



        // POST: api/AFI_FDocHi
        [Route("api/AFI_FDocHi/SaveAllSanad/{ace}/{sal}/{group}")]
        [ResponseType(typeof(AFI_FDocBi))]
        public async Task<IHttpActionResult> PostAFI_SaveAllSanad(string ace, string sal, string group, SaveAllSanadObject o)
        {
            string dBName = UnitDatabase.DatabaseName(ace, sal, group);
            string sql = "";
            long serialNumber = 0;
            long serialNumber_Test = 0;

            string modeCode = o.Head.ModeCode;

            byte forSale;

            if (modeCode == "51" || modeCode == "52" || modeCode == "53" ||
                modeCode == "SPFCT" || modeCode == "SFCT" || modeCode == "SRFCT" || modeCode == "SORD")
                forSale = 1;
            else
                forSale = 0;

            var dataAccount = UnitDatabase.ReadUserPassHeader(this.Request.Headers);
            var DBase = UnitDatabase.dataDB.Where(p => p.UserName == dataAccount[0] && p.Password == dataAccount[1]).Single();
            string res = UnitDatabase.TestAcount(DBase, dataAccount[3], ace, group, UnitPublic.ModeCodeConnection(modeCode));

            if (res == "")
            {
                try
                {
                    // save doch temp
                    sql = UnitPublic.CreateSql_FDocH(o.Head, true,dBName);
                    var value_H = DBase.DB.Database.SqlQuery<string>(sql).Single();
                    if (!string.IsNullOrEmpty(value_H))
                    {
                        await DBase.DB.SaveChangesAsync();
                    }
                    serialNumber_Test = Convert.ToInt64(value_H.Split('@')[1]);

                    // save docb temp
                    int i = 0;
                    foreach (var item in o.Bands)
                    {
                        i++;
                        sql = UnitPublic.CreateSql_FDocB(item, serialNumber_Test, i, dBName);
                        DBase.DB.Database.SqlQuery<int>(sql).Single();
                    }
                    await DBase.DB.SaveChangesAsync();


                    sql = string.Format(CultureInfo.InvariantCulture,
                                            @"EXEC	{0}.[dbo].[Web_TestFDoc_Temp] @serialNumber = {1}, 
                                                                              --@last_SerialNumber = {2},
                                                                              @UserCode = '{3}' ",
                                           dBName,
                                            serialNumber_Test,
                                           0,
                                           dataAccount[2]);
                    var result = DBase.DB.Database.SqlQuery<TestDocB>(sql).ToList();
                    var jsonResult = UnitPublic.SetErrorSanad(result);
                    if (jsonResult.Status == "Success")
                    {
                        CalcAddmin calcAddmin = new CalcAddmin();
                        calcAddmin.forSale = true;
                        calcAddmin.serialNumber = serialNumber_Test;
                        calcAddmin.custCode = o.Head.CustCode;
                        calcAddmin.spec1 = o.Head.AddMinSpec1;
                        calcAddmin.spec2 = o.Head.AddMinSpec2;
                        calcAddmin.spec3 = o.Head.AddMinSpec3;
                        calcAddmin.spec4 = o.Head.AddMinSpec4;
                        calcAddmin.spec5 = o.Head.AddMinSpec5;
                        calcAddmin.spec6 = o.Head.AddMinSpec6;
                        calcAddmin.spec7 = o.Head.AddMinSpec7;
                        calcAddmin.spec8 = o.Head.AddMinSpec8;
                        calcAddmin.spec9 = o.Head.AddMinSpec9;
                        calcAddmin.spec10 = o.Head.AddMinSpec10;
                        calcAddmin.MP1 = o.Head.AddMinPrice1;
                        calcAddmin.MP2 = o.Head.AddMinPrice2;
                        calcAddmin.MP3 = o.Head.AddMinPrice3;
                        calcAddmin.MP4 = o.Head.AddMinPrice4;
                        calcAddmin.MP5 = o.Head.AddMinPrice5;
                        calcAddmin.MP6 = o.Head.AddMinPrice6;
                        calcAddmin.MP7 = o.Head.AddMinPrice7;
                        calcAddmin.MP8 = o.Head.AddMinPrice8;
                        calcAddmin.MP9 = o.Head.AddMinPrice9;
                        calcAddmin.MP10 = o.Head.AddMinPrice10;
                        calcAddmin.flagTest = "Y";

                        sql = UnitPublic.CreateSql_CalcAddmin(calcAddmin, dBName);

                        var value_CalcAddmin = DBase.DB.Database.SqlQuery<AddMin>(sql).ToList();

                        o.Head.AddMinPrice1 = value_CalcAddmin[0].Mode == 1 ? Math.Abs(value_CalcAddmin[0].AddMinPrice ?? 0) : value_CalcAddmin[0].AddMinPrice;
                        o.Head.AddMinPrice2 = value_CalcAddmin[1].Mode == 1 ? Math.Abs(value_CalcAddmin[1].AddMinPrice ?? 0) : value_CalcAddmin[1].AddMinPrice;
                        o.Head.AddMinPrice3 = value_CalcAddmin[2].Mode == 1 ? Math.Abs(value_CalcAddmin[2].AddMinPrice ?? 0) : value_CalcAddmin[2].AddMinPrice;
                        o.Head.AddMinPrice4 = value_CalcAddmin[3].Mode == 1 ? Math.Abs(value_CalcAddmin[3].AddMinPrice ?? 0) : value_CalcAddmin[3].AddMinPrice;
                        o.Head.AddMinPrice5 = value_CalcAddmin[4].Mode == 1 ? Math.Abs(value_CalcAddmin[4].AddMinPrice ?? 0) : value_CalcAddmin[4].AddMinPrice;
                        o.Head.AddMinPrice6 = value_CalcAddmin[5].Mode == 1 ? Math.Abs(value_CalcAddmin[5].AddMinPrice ?? 0) : value_CalcAddmin[5].AddMinPrice;
                        o.Head.AddMinPrice7 = value_CalcAddmin[6].Mode == 1 ? Math.Abs(value_CalcAddmin[6].AddMinPrice ?? 0) : value_CalcAddmin[6].AddMinPrice;
                        o.Head.AddMinPrice8 = value_CalcAddmin[7].Mode == 1 ? Math.Abs(value_CalcAddmin[7].AddMinPrice ?? 0) : value_CalcAddmin[7].AddMinPrice;
                        o.Head.AddMinPrice9 = value_CalcAddmin[8].Mode == 1 ? Math.Abs(value_CalcAddmin[8].AddMinPrice ?? 0) : value_CalcAddmin[8].AddMinPrice;
                        o.Head.AddMinPrice10 = value_CalcAddmin[9].Mode == 1 ? Math.Abs(value_CalcAddmin[9].AddMinPrice ?? 0) : value_CalcAddmin[9].AddMinPrice;


                        double? totlal = 0;
                        double? discount = 0;

                        foreach (var item in o.Bands)
                        {
                            totlal += item.TotalPrice;
                            discount += item.Discount;
                        }



                        o.Head.TotalValue = (totlal - discount) + value_CalcAddmin[0].SumDiscount;




                        string Deghat = DBase.DB.Database.SqlQuery<string>("select Param from {0}.dbo.Web_Param where [key] = 'Deghat'", dBName).Single();

                        TashimBand tashimBand = new TashimBand();
                        tashimBand.ForSale = true;
                        tashimBand.Deghat = int.Parse(Deghat);
                        tashimBand.SerialNumber = serialNumber_Test;
                        tashimBand.MP1 = value_CalcAddmin[0].Mode == 0 ? value_CalcAddmin[0].AddMinPrice : value_CalcAddmin[0].AddMinPrice * -1;
                        tashimBand.MP2 = value_CalcAddmin[1].Mode == 0 ? value_CalcAddmin[1].AddMinPrice : value_CalcAddmin[1].AddMinPrice * -1;
                        tashimBand.MP3 = value_CalcAddmin[2].Mode == 0 ? value_CalcAddmin[2].AddMinPrice : value_CalcAddmin[2].AddMinPrice * -1;
                        tashimBand.MP4 = value_CalcAddmin[3].Mode == 0 ? value_CalcAddmin[3].AddMinPrice : value_CalcAddmin[3].AddMinPrice * -1;
                        tashimBand.MP5 = value_CalcAddmin[4].Mode == 0 ? value_CalcAddmin[4].AddMinPrice : value_CalcAddmin[4].AddMinPrice * -1;
                        tashimBand.MP6 = value_CalcAddmin[5].Mode == 0 ? value_CalcAddmin[5].AddMinPrice : value_CalcAddmin[5].AddMinPrice * -1;
                        tashimBand.MP7 = value_CalcAddmin[6].Mode == 0 ? value_CalcAddmin[6].AddMinPrice : value_CalcAddmin[6].AddMinPrice * -1;
                        tashimBand.MP8 = value_CalcAddmin[7].Mode == 0 ? value_CalcAddmin[7].AddMinPrice : value_CalcAddmin[7].AddMinPrice * -1;
                        tashimBand.MP9 = value_CalcAddmin[8].Mode == 0 ? value_CalcAddmin[8].AddMinPrice : value_CalcAddmin[8].AddMinPrice * -1;
                        tashimBand.MP10 = value_CalcAddmin[9].Mode == 0 ? value_CalcAddmin[9].AddMinPrice : value_CalcAddmin[9].AddMinPrice * -1;

                        sql = UnitPublic.CreateSql_TashimBand(tashimBand, dBName);
                        int livalue_TashimBandst = DBase.DB.Database.SqlQuery<int>(sql).Single();


                        sql = UnitPublic.CreateSql_FDocH(o.Head, false, dBName);

                        value_H = DBase.DB.Database.SqlQuery<string>(sql).Single();
                        if (!string.IsNullOrEmpty(value_H))
                        {
                            await DBase.DB.SaveChangesAsync();
                            serialNumber = Convert.ToInt64(value_H.Split('@')[0]);
                        }
                        else
                            serialNumber = o.Head.SerialNumber;

                        if (o.Head.SerialNumber > 0) //update
                        {
                            sql = string.Format(@"DECLARE	@return_value int
                                          EXEC	    @return_value = {0}.[dbo].[Web_SaveFDoc_BD]
		                                            @SerialNumber = {1},
		                                            @BandNo = 0
                                          SELECT	'Return Value' = @return_value", dBName, serialNumber);
                            int valueDelete = DBase.DB.Database.SqlQuery<int>(sql).Single();
                            if (valueDelete == 0)
                            {
                                await DBase.DB.SaveChangesAsync();
                            }

                            sql = string.Format(@" DECLARE	@return_value int
                                           EXEC	@return_value = {0}.[dbo].[Web_Doc_BOrder]
	                                            @TableName = '{1}',
                                                @SerialNumber = {2},
                                                @BandNoFld = '{3}'
                                            SELECT	'Return Value' = @return_value",
                                            dBName,
                                            ace == UnitPublic.Web1 ? "Afi1IDocB" : "Inv5DocB",
                                                    serialNumber,
                                                    ace == UnitPublic.Web1 ? "BandNo" : "Radif");
                            int valueUpdateBand = DBase.DB.Database.SqlQuery<int>(sql).Single();
                        }


                        sql = string.Format(CultureInfo.InvariantCulture,
                                      @"DECLARE	@return_value int
                                    EXEC	@return_value = {0}.[dbo].[Web_SaveFDocB_Convert]
		                                    @SerialNumber = {1},
		                                    @TempSerialNumber = {2}
                                    SELECT	'Return Value' = @return_value",
                                      dBName,
                                      serialNumber,
                                      serialNumber_Test);
                        DBase.DB.Database.SqlQuery<int>(sql).Single();
                        await DBase.DB.SaveChangesAsync();
                        jsonResult.SerialNumber = serialNumber;
                    }
                     UnitDatabase.SaveLog(dataAccount[0], dataAccount[1], dataAccount[2], ace, sal, group, o.Head.SerialNumber, UnitPublic.ModeCodeConnection(modeCode), UnitPublic.act_New, "Y", 1, 0);
 
                    return Ok(jsonResult);
                }
                catch (Exception e)
                {
                    return Ok(e.Message + " : " + e.InnerException.Message);
                    throw;
                }
            }
            else
                return Ok(res);

            //string conStr = UnitDatabase.CreateConnectionString(dataAccount[0], dataAccount[1], dataAccount[2], dataAccount[3], ace, sal, group, o.Head.SerialNumber, UnitPublic.ModeCodeConnection(modeCode), UnitPublic.act_New, 0);
        }








        // برای اپ استفاده شود
        public class SaveAllSanadAppObject
        {
            public AFI_FDocHi Head { get; set; }

            public List<AFI_FDocBi> Bands { get; set; }

        }



        // POST: api/AFI_FDocHi
        [Route("api/AFI_FDocHi/SaveAllSanadApp/{ace}/{sal}/{group}")]
        public async Task<IHttpActionResult> PostAFI_SaveAllSanadApp(string ace, string sal, string group, SaveAllSanadAppObject o)
        {
            string dBName = UnitDatabase.DatabaseName(ace, sal, group);
            string sql = "";
            long serialNumber = 0;
            long serialNumber_Test = 0;

            string modeCode = o.Head.ModeCode;

            byte forSale;

            if (modeCode == "51" || modeCode == "52" || modeCode == "53" ||
                modeCode == "SPFCT" || modeCode == "SFCT" || modeCode == "SRFCT" || modeCode == "SORD")
                forSale = 1;
            else
                forSale = 0;

            var dataAccount = UnitDatabase.ReadUserPassHeader(this.Request.Headers);
            var DBase = UnitDatabase.dataDB.Where(p => p.UserName == dataAccount[0] && p.Password == dataAccount[1]).Single();
            string res = UnitDatabase.TestAcount(DBase, dataAccount[3], ace, group, UnitPublic.ModeCodeConnection(modeCode));

            if (res == "")
            {
                try
                {
                    // save doch temp
                    sql = UnitPublic.CreateSql_FDocH(o.Head, true, dBName);
                    var value_H = DBase.DB.Database.SqlQuery<string>(sql).Single();
                    if (!string.IsNullOrEmpty(value_H))
                    {
                        await DBase.DB.SaveChangesAsync();
                    }
                    serialNumber_Test = Convert.ToInt64(value_H.Split('@')[1]);

                    // save docb temp
                    int i = 0;
                    foreach (var item in o.Bands)
                    {
                        i++;
                        sql = UnitPublic.CreateSql_FDocB(item, serialNumber_Test, i, dBName);
                        DBase.DB.Database.SqlQuery<int>(sql).Single();
                    }
                    await DBase.DB.SaveChangesAsync();


                    sql = string.Format(CultureInfo.InvariantCulture,
                                            @"EXEC	{0}.[dbo].[Web_TestFDoc_Temp] @serialNumber = {1}, 
                                                                              --@last_SerialNumber = {2},
                                                                              @UserCode = '{3}' ",
                                           dBName,
                                            serialNumber_Test,
                                           0,
                                           dataAccount[2]);
                    var result = DBase.DB.Database.SqlQuery<TestDocB>(sql).ToList();
                    var jsonResult = UnitPublic.SetErrorSanad(result);
                    jsonResult.SerialNumber = serialNumber_Test;
                    if (jsonResult.Status == "Success")
                    {
                        CalcAddmin calcAddmin = new CalcAddmin();
                        calcAddmin.forSale = true;
                        calcAddmin.serialNumber = serialNumber_Test;
                        calcAddmin.custCode = o.Head.CustCode;
                        calcAddmin.spec1 = o.Head.AddMinSpec1;
                        calcAddmin.spec2 = o.Head.AddMinSpec2;
                        calcAddmin.spec3 = o.Head.AddMinSpec3;
                        calcAddmin.spec4 = o.Head.AddMinSpec4;
                        calcAddmin.spec5 = o.Head.AddMinSpec5;
                        calcAddmin.spec6 = o.Head.AddMinSpec6;
                        calcAddmin.spec7 = o.Head.AddMinSpec7;
                        calcAddmin.spec8 = o.Head.AddMinSpec8;
                        calcAddmin.spec9 = o.Head.AddMinSpec9;
                        calcAddmin.spec10 = o.Head.AddMinSpec10;
                        calcAddmin.MP1 = o.Head.AddMinPrice1;
                        calcAddmin.MP2 = o.Head.AddMinPrice2;
                        calcAddmin.MP3 = o.Head.AddMinPrice3;
                        calcAddmin.MP4 = o.Head.AddMinPrice4;
                        calcAddmin.MP5 = o.Head.AddMinPrice5;
                        calcAddmin.MP6 = o.Head.AddMinPrice6;
                        calcAddmin.MP7 = o.Head.AddMinPrice7;
                        calcAddmin.MP8 = o.Head.AddMinPrice8;
                        calcAddmin.MP9 = o.Head.AddMinPrice9;
                        calcAddmin.MP10 = o.Head.AddMinPrice10;
                        calcAddmin.flagTest = "Y";

                        sql = UnitPublic.CreateSql_CalcAddmin(calcAddmin, dBName);

                        var value_CalcAddmin = DBase.DB.Database.SqlQuery<AddMin>(sql).ToList();
                        jsonResult.DataAddmin = value_CalcAddmin;

                        o.Head.AddMinPrice1 = value_CalcAddmin[0].Mode == 1 ? Math.Abs(value_CalcAddmin[0].AddMinPrice ?? 0) : value_CalcAddmin[0].AddMinPrice;
                        o.Head.AddMinPrice2 = value_CalcAddmin[1].Mode == 1 ? Math.Abs(value_CalcAddmin[1].AddMinPrice ?? 0) : value_CalcAddmin[1].AddMinPrice;
                        o.Head.AddMinPrice3 = value_CalcAddmin[2].Mode == 1 ? Math.Abs(value_CalcAddmin[2].AddMinPrice ?? 0) : value_CalcAddmin[2].AddMinPrice;
                        o.Head.AddMinPrice4 = value_CalcAddmin[3].Mode == 1 ? Math.Abs(value_CalcAddmin[3].AddMinPrice ?? 0) : value_CalcAddmin[3].AddMinPrice;
                        o.Head.AddMinPrice5 = value_CalcAddmin[4].Mode == 1 ? Math.Abs(value_CalcAddmin[4].AddMinPrice ?? 0) : value_CalcAddmin[4].AddMinPrice;
                        o.Head.AddMinPrice6 = value_CalcAddmin[5].Mode == 1 ? Math.Abs(value_CalcAddmin[5].AddMinPrice ?? 0) : value_CalcAddmin[5].AddMinPrice;
                        o.Head.AddMinPrice7 = value_CalcAddmin[6].Mode == 1 ? Math.Abs(value_CalcAddmin[6].AddMinPrice ?? 0) : value_CalcAddmin[6].AddMinPrice;
                        o.Head.AddMinPrice8 = value_CalcAddmin[7].Mode == 1 ? Math.Abs(value_CalcAddmin[7].AddMinPrice ?? 0) : value_CalcAddmin[7].AddMinPrice;
                        o.Head.AddMinPrice9 = value_CalcAddmin[8].Mode == 1 ? Math.Abs(value_CalcAddmin[8].AddMinPrice ?? 0) : value_CalcAddmin[8].AddMinPrice;
                        o.Head.AddMinPrice10 = value_CalcAddmin[9].Mode == 1 ? Math.Abs(value_CalcAddmin[9].AddMinPrice ?? 0) : value_CalcAddmin[9].AddMinPrice;


                        double? totlal = 0;
                        double? discount = 0;

                        foreach (var item in o.Bands)
                        {
                            totlal += item.TotalPrice;
                            discount += item.Discount;
                        }



                        o.Head.TotalValue = (totlal - discount) + value_CalcAddmin[0].SumDiscount;

                        jsonResult.TotalValue = o.Head.TotalValue;

                        sql = string.Format("select Param from {0}.dbo.Web_Param where [key] = 'Deghat'", dBName);
                        string Deghat = DBase.DB.Database.SqlQuery<string>(sql).Single();

                        TashimBand tashimBand = new TashimBand();
                        tashimBand.ForSale = true;
                        tashimBand.Deghat = int.Parse(Deghat);
                        tashimBand.SerialNumber = serialNumber_Test;
                        tashimBand.MP1 = value_CalcAddmin[0].AddMinPrice;
                        tashimBand.MP2 = value_CalcAddmin[1].AddMinPrice;
                        tashimBand.MP3 = value_CalcAddmin[2].AddMinPrice;
                        tashimBand.MP4 = value_CalcAddmin[3].AddMinPrice;
                        tashimBand.MP5 = value_CalcAddmin[4].AddMinPrice;
                        tashimBand.MP6 = value_CalcAddmin[5].AddMinPrice;
                        tashimBand.MP7 = value_CalcAddmin[6].AddMinPrice;
                        tashimBand.MP8 = value_CalcAddmin[7].AddMinPrice;
                        tashimBand.MP9 = value_CalcAddmin[8].AddMinPrice;
                        tashimBand.MP10 = value_CalcAddmin[9].AddMinPrice;

                        sql = UnitPublic.CreateSql_TashimBand(tashimBand, dBName);
                        int livalue_TashimBandst = DBase.DB.Database.SqlQuery<int>(sql).Single();

                        if (o.Head.flagTest == "false")
                        {

                            sql = UnitPublic.CreateSql_FDocHApp(o.Head, false, dBName);

                            value_H = DBase.DB.Database.SqlQuery<string>(sql).Single();
                            if (!string.IsNullOrEmpty(value_H))
                            {
                                await DBase.DB.SaveChangesAsync();
                                serialNumber = Convert.ToInt64(value_H.Split('@')[0]);
                            }
                            else
                                serialNumber = o.Head.SerialNumber;

                            if (o.Head.SerialNumber > 0) //update
                            {
                                sql = string.Format(@"DECLARE	@return_value int
                                          EXEC	    @return_value = {0}.[dbo].[Web_SaveFDoc_BD]
		                                            @SerialNumber = {1},
		                                            @BandNo = 0
                                          SELECT	'Return Value' = @return_value", dBName, serialNumber);
                                int valueDelete = DBase.DB.Database.SqlQuery<int>(sql).Single();
                                if (valueDelete == 0)
                                {
                                    await DBase.DB.SaveChangesAsync();
                                }

                                sql = string.Format(@" DECLARE	@return_value int
                                           EXEC	@return_value = {0}.[dbo].[Web_Doc_BOrder]
	                                            @TableName = '{1}',
                                                @SerialNumber = {2},
                                                @BandNoFld = '{3}'
                                            SELECT	'Return Value' = @return_value",
                                            dBName,
                                            ace == UnitPublic.Web1 ? "Afi1IDocB" : "Inv5DocB",
                                            serialNumber,
                                            ace == UnitPublic.Web1 ? "BandNo" : "Radif");
                                int valueUpdateBand = DBase.DB.Database.SqlQuery<int>(sql).Single();
                            }


                            sql = string.Format(CultureInfo.InvariantCulture,
                                          @"DECLARE	@return_value int
                                    EXEC	@return_value = {0}.[dbo].[Web_SaveFDocB_Convert]
		                                    @SerialNumber = {1},
		                                    @TempSerialNumber = {2}
                                    SELECT	'Return Value' = @return_value",
                                          dBName,
                                          serialNumber,
                                          serialNumber_Test);
                            DBase.DB.Database.SqlQuery<int>(sql).Single();
                            await DBase.DB.SaveChangesAsync();
                            jsonResult.SerialNumber = serialNumber;
                        }
                    }
                    UnitDatabase.SaveLog(dataAccount[0], dataAccount[1], dataAccount[2], ace, sal, group, o.Head.SerialNumber, UnitPublic.ModeCodeConnection(modeCode), UnitPublic.act_New, "Y", 1, 0);

                    return Ok(jsonResult);
                }
                catch (Exception e)
                {
                    return Ok(e.Message + " : " + e.InnerException.Message);
                    throw;
                }
            }
            else
                return Ok(res);

            //string conStr = UnitDatabase.CreateConnectionString(dataAccount[0], dataAccount[1], dataAccount[2], dataAccount[3], ace, sal, group, o.Head.SerialNumber, UnitPublic.ModeCodeConnection(modeCode), UnitPublic.act_New, 0);
        }



    }
}