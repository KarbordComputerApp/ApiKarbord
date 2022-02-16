using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Threading.Tasks;
using System.Web.Http.Description;
using ApiKarbord.Controllers.Unit;
using ApiKarbord.Models;
using Newtonsoft.Json;
using System.Globalization;

namespace ApiKarbord.Controllers.AFI.data
{
    public class ErjDocHController : ApiController
    {


        public class ErjDocH
        {
            public string ModeCode { get; set; }

            public int DocNo { get; set; }

            public long SerialNumber { get; set; }

            public string DocDate { get; set; }

            public string MhltDate { get; set; }

            public string AmalDate { get; set; }

            public string EndDate { get; set; }

            public byte BranchCode { get; set; }

            public string UserCode { get; set; }

            public string Eghdam { get; set; }

            public string Tanzim { get; set; }

            public string TahieShode { get; set; }

            public string Status { get; set; }

            public string Spec { get; set; }

            public string CustCode { get; set; }

            public string KhdtCode { get; set; }

            public string DocDesc { get; set; }

            public string EghdamComm { get; set; }

            public string FinalComm { get; set; }

            public string SpecialComm { get; set; }

            public string RelatedDocs { get; set; }

            public byte Mahramaneh { get; set; }

            public string F01 { get; set; }

            public string F02 { get; set; }

            public string F03 { get; set; }

            public string F04 { get; set; }

            public string F05 { get; set; }

            public string F06 { get; set; }

            public string F07 { get; set; }

            public string F08 { get; set; }

            public string F09 { get; set; }

            public string F10 { get; set; }

            public string F11 { get; set; }

            public string F12 { get; set; }

            public string F13 { get; set; }

            public string F14 { get; set; }

            public string F15 { get; set; }

            public string F16 { get; set; }

            public string F17 { get; set; }

            public string F18 { get; set; }

            public string F19 { get; set; }

            public string F20 { get; set; }
        }



        // POST: api/ErjDocH
        [Route("api/ErjDocH/{ace}/{sal}/{group}")]
        public async Task<IHttpActionResult> PostErjDocH(string ace, string sal, string group, ErjDocH erjDocH)
        {
            int value = 0;
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var dataAccount = UnitDatabase.ReadUserPassHeader(this.Request.Headers);
            string con = UnitDatabase.CreateConection(dataAccount[0], dataAccount[1], dataAccount[2], ace, sal, group, erjDocH.SerialNumber, "", 2, 0);
            if (con == "ok")
            {
                try
                {
                    string sql = string.Format(
                         @"DECLARE	@return_value int,
		                            @DocNo_Out int

                            EXEC	@return_value = [dbo].[Web_ErjSaveDoc_HI]
		                            @ModeCode = '{0}',
		                            @DocNo = {1},
		                            @SerialNumber = {2},
		                            @DocDate = '{3}',
		                            @MhltDate = '{4}',
                                    @AmalDate = '{5}',
                                    @EndDate = '{6}',
		                            @BranchCode = {7},
		                            @UserCode = '{8}', 
		                            @Eghdam = N'{9}',
		                            @Tanzim = '{10}',
		                            @TahieShode = '{11}',
		                            @Status = '{12}',
		                            @Spec = '{13}',
		                            @CustCode = '{14}',
		                            @KhdtCode = '{15}',
                                    @EghdamComm = '{16}',		                            
                                    @DocDesc = '{17}',
		                            @FinalComm = '{18}',
		                            @SpecialComm = '{19}',
		                            @RelatedDocs = '{20}',
		                            @Mahramaneh = {21},
		                            @F01 = '{22}',
		                            @F02 = '{23}',
		                            @F03 = '{24}',
		                            @F04 = '{25}',
		                            @F05 = '{26}',
		                            @F06 = '{27}',
		                            @F07 = '{28}',
		                            @F08 = '{29}',
		                            @F09 = '{30}',
		                            @F10 = '{31}',
		                            @F11 = '{32}',
		                            @F12 = '{33}',
		                            @F13 = '{34}',
		                            @F14 = '{35}',
		                            @F15 = '{36}',
		                            @F16 = '{37}',
		                            @F17 = '{38}',
		                            @F18 = '{39}',
		                            @F19 = '{40}',
		                            @F20 = '{41}',
		                            @DocNo_Out = @DocNo_Out OUTPUT
                            SELECT	@DocNo_Out as '@DocNo_Out' ",
                            erjDocH.ModeCode,
                            erjDocH.DocNo,
                            erjDocH.SerialNumber,
                            erjDocH.DocDate ?? string.Format("{0:yyyy/MM/dd}", DateTime.Now.AddDays(-1)),
                            erjDocH.MhltDate,
                            erjDocH.AmalDate,
                            erjDocH.EndDate,
                            erjDocH.BranchCode,
                            erjDocH.UserCode,
                            erjDocH.Eghdam,
                            erjDocH.Tanzim,
                            erjDocH.TahieShode,
                            erjDocH.Status,
                            erjDocH.Spec,
                            erjDocH.CustCode ?? "",
                            erjDocH.KhdtCode ?? "",
                            UnitPublic.ConvertTextWebToWin(erjDocH.EghdamComm ?? ""),
                            UnitPublic.ConvertTextWebToWin(erjDocH.DocDesc ?? ""),
                            UnitPublic.ConvertTextWebToWin(erjDocH.FinalComm ?? ""),
                            UnitPublic.ConvertTextWebToWin(erjDocH.SpecialComm ?? ""),
                            erjDocH.RelatedDocs ?? "",
                            erjDocH.Mahramaneh,
                            erjDocH.F01,
                            erjDocH.F02,
                            erjDocH.F03,
                            erjDocH.F04,
                            erjDocH.F05,
                            erjDocH.F06,
                            erjDocH.F07,
                            erjDocH.F08,
                            erjDocH.F09,
                            erjDocH.F10,
                            erjDocH.F11,
                            erjDocH.F12,
                            erjDocH.F13,
                            erjDocH.F14,
                            erjDocH.F15,
                            erjDocH.F16,
                            erjDocH.F17,
                            erjDocH.F18,
                            erjDocH.F19,
                            erjDocH.F20);
                    value = UnitDatabase.db.Database.SqlQuery<int>(sql).Single();
                    if (value > 0)
                    {
                        await UnitDatabase.db.SaveChangesAsync();
                    }
                }
                catch (Exception e)
                {
                    throw;
                }

                return Ok(value);
            }
            return Ok(con);
        }




        public class ErjSaveDoc_HUObject
        {
            public long SerialNumber { get; set; }

            public string Tanzim { get; set; }

            public string Status { get; set; }

            public string Spec { get; set; }

            public string DocDesc { get; set; }

            public string EghdamComm { get; set; }

            public string FinalComm { get; set; }

            public string SpecialComm { get; set; }

            public string F01 { get; set; }

            public string F02 { get; set; }

            public string F03 { get; set; }

            public string F04 { get; set; }

            public string F05 { get; set; }

            public string F06 { get; set; }

            public string F07 { get; set; }

            public string F08 { get; set; }

            public string F09 { get; set; }

            public string F10 { get; set; }

            public string F11 { get; set; }

            public string F12 { get; set; }

            public string F13 { get; set; }

            public string F14 { get; set; }

            public string F15 { get; set; }

            public string F16 { get; set; }

            public string F17 { get; set; }

            public string F18 { get; set; }

            public string F19 { get; set; }

            public string F20 { get; set; }
        }



        // POST: api/ErjDocH/ErjSaveDoc_HU
        [Route("api/ErjDocH/ErjSaveDoc_HU/{ace}/{sal}/{group}")]
        public async Task<IHttpActionResult> PostErjSaveDoc_HU(string ace, string sal, string group, ErjSaveDoc_HUObject ErjSaveDoc_HUObject)
        {
            int value = 0;
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var dataAccount = UnitDatabase.ReadUserPassHeader(this.Request.Headers);
            string con = UnitDatabase.CreateConection(dataAccount[0], dataAccount[1], dataAccount[2], ace, sal, group, ErjSaveDoc_HUObject.SerialNumber, "", 2, 0);
            if (con == "ok")
            {
                try
                {
                    string sql = string.Format(
                         @"DECLARE	@return_value int

                            EXEC	@return_value = [dbo].[Web_ErjSaveDoc_HU]
                                    @SerialNumber = {0},
		                            @UserCode = '{1}',
		                            @Tanzim = '{2}',
		                            @Status = '{3}',
		                            @Spec = '{4}',
		                            @DocDesc = '{5}',
		                            @EghdamComm = '{6}',
		                            @FinalComm = '{7}',
		                            @SpecialComm = '{8}',
		                            @F01 = '{9}',
		                            @F02 = '{10}',
		                            @F03 = '{11}',
		                            @F04 = '{12}',
		                            @F05 = '{13}',
		                            @F06 = '{14}',
		                            @F07 = '{15}',
		                            @F08 = '{16}',
		                            @F09 = '{17}',
		                            @F10 = '{18}',
		                            @F11 = '{19}',
		                            @F12 = '{20}',
		                            @F13 = '{21}',
		                            @F14 = '{22}',
		                            @F15 = '{23}',
		                            @F16 = '{24}',
		                            @F17 = '{25}',
		                            @F18 = '{26}',
		                            @F19 = '{27}',
		                            @F20 = '{28}'
                            SELECT	'Return Value' = @return_value ",
                            ErjSaveDoc_HUObject.SerialNumber,
                            dataAccount[2],
                            ErjSaveDoc_HUObject.Tanzim,
                            ErjSaveDoc_HUObject.Status,
                            ErjSaveDoc_HUObject.Spec,
                            UnitPublic.ConvertTextWebToWin(ErjSaveDoc_HUObject.DocDesc ?? ""),
                            UnitPublic.ConvertTextWebToWin(ErjSaveDoc_HUObject.EghdamComm ?? ""),
                            UnitPublic.ConvertTextWebToWin(ErjSaveDoc_HUObject.FinalComm ?? ""),
                            UnitPublic.ConvertTextWebToWin(ErjSaveDoc_HUObject.SpecialComm ?? ""),
                            ErjSaveDoc_HUObject.F01,
                            ErjSaveDoc_HUObject.F02,
                            ErjSaveDoc_HUObject.F03,
                            ErjSaveDoc_HUObject.F04,
                            ErjSaveDoc_HUObject.F05,
                            ErjSaveDoc_HUObject.F06,
                            ErjSaveDoc_HUObject.F07,
                            ErjSaveDoc_HUObject.F08,
                            ErjSaveDoc_HUObject.F09,
                            ErjSaveDoc_HUObject.F10,
                            ErjSaveDoc_HUObject.F11,
                            ErjSaveDoc_HUObject.F12,
                            ErjSaveDoc_HUObject.F13,
                            ErjSaveDoc_HUObject.F14,
                            ErjSaveDoc_HUObject.F15,
                            ErjSaveDoc_HUObject.F16,
                            ErjSaveDoc_HUObject.F17,
                            ErjSaveDoc_HUObject.F18,
                            ErjSaveDoc_HUObject.F19,
                            ErjSaveDoc_HUObject.F20);
                    value = UnitDatabase.db.Database.SqlQuery<int>(sql).Single();
                    if (value > 0)
                    {
                        await UnitDatabase.db.SaveChangesAsync();
                    }
                }
                catch (Exception e)
                {
                    throw;
                }

                return Ok(value);
            }
            return Ok(con);
        }



        // DELETE: api/ErjDocH/5
        [Route("api/ErjDocH/{ace}/{sal}/{group}/{SerialNumber}")]
        //[ResponseType(typeof(ErjDocH))]
        public async Task<IHttpActionResult> DeleteErjDocH(string ace, string sal, string group, long SerialNumber)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var dataAccount = UnitDatabase.ReadUserPassHeader(this.Request.Headers);
            string con = UnitDatabase.CreateConection(dataAccount[0], dataAccount[1], dataAccount[2], ace, sal, group, SerialNumber, "", 0, 0);
            if (con == "ok")
            {
                try
                {
                    string sql = string.Format(@"DECLARE	@return_value int
                                                 EXEC	@return_value = [dbo].[Web_ErjSaveDoc_Del]
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
                UnitDatabase.SaveLog(dataAccount[0], dataAccount[1], dataAccount[2], ace, sal, group, SerialNumber, "", 3, "Y", 0);
                return Ok(1);
            }
            return Ok(con);

        }





        public class TestDoc_DeleteObject 
        {
            public long SerialNumber { get; set; }

        }

        public class TestDoc_Delete
        {
            public int id { get; set; }

            public byte Test { get; set; }

            public string TestName { get; set; }

            public string TestCap { get; set; }

            public int BandNo { get; set; }

        }



        [Route("api/ErjDocH/TestDoc_Delete/{ace}/{sal}/{group}")]
        [ResponseType(typeof(TestDoc_Delete))]
        public async Task<IHttpActionResult> PostWeb_TestDoc_Delete(string ace, string sal, string group, TestDoc_DeleteObject TestDoc_DeleteObject)
        {
            var dataAccount = UnitDatabase.ReadUserPassHeader(this.Request.Headers);
            string con = UnitDatabase.CreateConection(dataAccount[0], dataAccount[1], dataAccount[2], ace, sal, group, 0, "", 0, 0);
            if (con == "ok")
            {
                string sql = string.Format(CultureInfo.InvariantCulture,
                                           @"EXEC	[dbo].[Web_TestDoc_Delete] @serialNumber = {0} , @UserCode = '{1}' ",
                                           TestDoc_DeleteObject.SerialNumber ,
                                           dataAccount[2]);
                try
                {
                    var result = UnitDatabase.db.Database.SqlQuery<TestDoc_Delete>(sql).ToList();
                    var jsonResult = JsonConvert.SerializeObject(result);
                    return Ok(jsonResult);
                }
                catch (Exception e)
                {
                    throw;
                }

            }
            return Ok(con);

        }




    }
}
