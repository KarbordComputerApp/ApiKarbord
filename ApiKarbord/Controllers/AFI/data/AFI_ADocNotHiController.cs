using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web;
using System.Runtime.InteropServices;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using ApiKarbord.Controllers.Unit;
using ApiKarbord.Models;
using System.Text;
using System.Reflection;

namespace ApiKarbord.Controllers.AFI.data
{
    public class AFI_ADocNotHiController : ApiController
    {
        public class AFI_ADocNot_Hi
        {
            public int DocNo { get; set; }

            public long SerialNumber { get; set; }

            public string DocDate { get; set; }

            public byte? RelatedGroupActive { get; set; }

            public byte BranchCode { get; set; }

            public string UserCode { get; set; }

            public string Tanzim { get; set; }

            public string TahieShode { get; set; }

            public string Eghdam { get; set; }

            public string Status { get; set; }

            public string Spec { get; set; }

            public string Footer { get; set; }

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

            public string ModeCode { get; set; }

        }

        // POST: api/AFI_ADocNotHi
        [Route("api/AFI_ADocNotHi/{ace}/{sal}/{group}")]
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PostAFI_ADocNotHi(string ace, string sal, string group, AFI_ADocNot_Hi d)
        {
            string value = "";
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            string sql = "";
            var dataAccount = UnitDatabase.ReadUserPassHeader(this.Request.Headers);
            string dBName = UnitDatabase.DatabaseName(ace, sal, group);


            sql = string.Format(
                 @" DECLARE	@return_value nvarchar(50),
		                            @DocNo_Out int
                            EXEC	@return_value = {0}.[dbo].[Web_SaveADoc_NotHI]
                                    @DocNo = {1},
                                    @SerialNumber = {2},
                                    @DocDate = N'{3}',
                                    @RelatedGroupActive = {4},
                                    @BranchCode = {5},
                                    @UserCode = N'''{6}''',
                                    @Tanzim = N'{7}',
                                    @TahieShode = N'{8}',
                                    @Eghdam = N'''{9}''',
                                    @Status = N'{10}',
                                    @Spec = N'{11}',
                                    @Footer = N'{12}',
                                    @F01 = N'{13}',
                                    @F02 = N'{14}',
                                    @F03 = N'{15}',
                                    @F04 = N'{16}',
                                    @F05 = N'{17}',
                                    @F06 = N'{18}',
                                    @F07 = N'{19}',
                                    @F08 = N'{20}',
                                    @F09 = N'{21}',
                                    @F10 = N'{22}',
                                    @F11 = N'{23}',
                                    @F12 = N'{24}',
                                    @F13 = N'{25}',
                                    @F14 = N'{26}',
                                    @F15 = N'{27}',
                                    @F16 = N'{28}',
                                    @F17 = N'{29}',
                                    @F18 = N'{30}',
                                    @F19 = N'{31}',
                                    @F20 = N'{32}',
                                    @ModeCode = '{33}',
                                    @DocNo_Out = @DocNo_Out OUTPUT
                            SELECT	'return_value' = @return_value +'-'+  CONVERT(nvarchar, @DOCNO_OUT)",
                    //@DocTime = N'{5}', @mDocDate = N'{6}', @IP = N'{8}',
                    dBName,
                    d.DocNo,
                    d.SerialNumber,
                    d.DocDate ?? string.Format("{0:yyyy/MM/dd}", DateTime.Now.AddDays(-1)),
                    d.RelatedGroupActive ?? 0,
                    d.BranchCode,
                    d.UserCode,
                    d.Tanzim,
                    d.TahieShode,
                    d.Eghdam,
                    d.Status,
                    d.Spec,
                    UnitPublic.ConvertTextWebToWin(d.Footer ?? ""),
                    d.F01,
                    d.F02,
                    d.F03,
                    d.F04,
                    d.F05,
                    d.F06,
                    d.F07,
                    d.F08,
                    d.F09,
                    d.F10,
                    d.F11,
                    d.F12,
                    d.F13,
                    d.F14,
                    d.F15,
                    d.F16,
                    d.F17,
                    d.F18,
                    d.F19,
                    d.F20,
                    d.ModeCode
                    );

            var DBase = UnitDatabase.dataDB.Where(p => p.UserName.ToUpper() == dataAccount[0].ToUpper() && p.Password == dataAccount[1]).Single();
            string res = UnitDatabase.TestAcount(DBase, dataAccount[3], ace, group, UnitPublic.access_ANOTE);

            if (res == "")
            {
                value = DBase.DB.Database.SqlQuery<string>(sql).Single();
            }
            else
                return Ok(res);

            if (!string.IsNullOrEmpty(value))
            {
                await DBase.DB.SaveChangesAsync();
            }

            string[] serials = value.Split('-');
            UnitDatabase.SaveLog(dataAccount[0], dataAccount[1], dataAccount[2], ace, sal, group, Convert.ToInt64(serials[0]), UnitPublic.access_ANOTE, 2, "Y", 1, 0);
            return Ok(value);
        }


        // DELETE: api/AFI_ADocNotHi/5
        [Route("api/AFI_ADocNotHi/{ace}/{sal}/{group}/{SerialNumber}")]
        public async Task<IHttpActionResult> DeleteAFI_ADocNotHi(string ace, string sal, string group, long SerialNumber)
        {
            var dataAccount = UnitDatabase.ReadUserPassHeader(this.Request.Headers);
            string dBName = UnitDatabase.DatabaseName(ace, sal, group);
            var DBase = UnitDatabase.dataDB.Where(p => p.UserName.ToUpper() == dataAccount[0].ToUpper() && p.Password == dataAccount[1]).Single();
            string res = UnitDatabase.TestAcount(DBase, dataAccount[3], ace, group, UnitPublic.access_ANOTE);
            string sql = "";
            if (res == "")
            {
                sql = string.Format(@"DECLARE	 @return_value int
                                                 EXEC	@return_value = {0}.[dbo].[Web_SaveADoc_NotDel]
		                                                @SerialNumber = {1}
                                                 SELECT	'Return Value' = @return_value"
                                        , dBName, SerialNumber);

                int value = DBase.DB.Database.SqlQuery<int>(sql).Single();
                if (value > 0)
                {
                    await DBase.DB.SaveChangesAsync();
                }

                UnitDatabase.SaveLog(dataAccount[0], dataAccount[1], dataAccount[2], ace, sal, group, SerialNumber, UnitPublic.access_ANOTE, 3, "Y", 1, 0);
                return Ok("0");
            }
            else
                return Ok(res);

        }
    }
}