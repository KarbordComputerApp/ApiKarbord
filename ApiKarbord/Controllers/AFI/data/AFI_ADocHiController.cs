using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
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
    public class AFI_ADocHiController : ApiController
    {
        public class AFI_ADocHi_i
        {
            public byte DocNoMode { get; set; }

            public byte InsertMode { get; set; }

            public string ModeCode { get; set; }

            public int DocNo { get; set; }

            public int StartNo { get; set; }

            public int EndNo { get; set; }

            public byte BranchCode { get; set; }

            public string UserCode { get; set; }

            public long SerialNumber { get; set; }

            public string DocDate { get; set; }

            public string Spec { get; set; }

            public string mDocDate { get; set; }

            public string Tanzim { get; set; }

            public string TahieShode { get; set; }

            public string Eghdam { get; set; }

            public int DocNo_Out { get; set; }

        }


        public class AFI_ADocHi_u
        {
            public long SerialNumber { get; set; }

            public string ModeCode { get; set; }

            public int DocNo { get; set; }

            public byte BranchCode { get; set; }

            public string UserCode { get; set; }

            public string DocDate { get; set; }

            public string Spec { get; set; }

            public string Eghdam { get; set; }

            public string Tanzim { get; set; }

            public string Taeed { get; set; }

            public string TahieShode { get; set; }

            public string Status { get; set; }
        }



        // PUT: api/AFI_ADocHi/5
        [Route("api/AFI_ADocHi/{ace}/{sal}/{group}/{userName}/{password}")]
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutAFI_ADocHi(string ace, string sal, string group, string userName, string password, AFI_ADocHi_u AFI_ADocHi_u)
        {
            string value = "";
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
                    string sql = string.Format(
                         @" DECLARE	@return_value int
                            EXEC	@return_value = [dbo].[Web_SaveADoc_HU]
		                            @SerialNumber = {0},
		                            @ModeCode = N'{1}',
		                            @DocNo = {2},
		                            @BranchCode = {3},
		                            @UserCode = N'{4}',
		                            @DocDate = N'{5}',
		                            @Spec = N'{6}',
		                            @Eghdam = N'{7}',
		                            @Tanzim = N'{8}',
		                            @Taeed = N'{9}',
		                            @TahieShode = N'{10}',
		                            @Status = N'{11}'
                            SELECT	'Return Value' = @return_value",
                            AFI_ADocHi_u.SerialNumber,
                            AFI_ADocHi_u.ModeCode,
                            AFI_ADocHi_u.DocNo,
                            AFI_ADocHi_u.BranchCode,
                            AFI_ADocHi_u.UserCode,
                            AFI_ADocHi_u.DocDate ?? string.Format("{0:yyyy/MM/dd}", DateTime.Now.AddDays(-1)),
                            AFI_ADocHi_u.Spec,
                            AFI_ADocHi_u.Eghdam,
                            AFI_ADocHi_u.Tanzim,
                            AFI_ADocHi_u.Taeed == "null" ? "" : AFI_ADocHi_u.Taeed,
                            AFI_ADocHi_u.TahieShode,
                            AFI_ADocHi_u.Status);
                    value = UnitDatabase.db.Database.SqlQuery<string>(sql).Single();

                    await UnitDatabase.db.SaveChangesAsync();
                }
                catch (Exception e)
                {
                    throw;
                }
            }
            return Ok(value);
        }

        // POST: api/AFI_ADocHi
        [Route("api/AFI_ADocHi/{ace}/{sal}/{group}/{userName}/{password}")]
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PostAFI_ADocHi(string ace, string sal, string group, string userName, string password, AFI_ADocHi_i AFI_ADocHi_i)
        {
            string value = "";
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (UnitDatabase.CreateConection(userName, password, ace, sal, group))
            {
                try
                {
                    string sql = string.Format(
                         @" DECLARE	@return_value nvarchar(50),
		                            @DocNo_Out int

                            EXEC	@return_value = [dbo].[Web_SaveADoc_HI]
		                            @DocNoMode = {0},
		                            @InsertMode = {1},
		                            @ModeCode = '{2}',
		                            @DocNo = {3},
		                            @StartNo = {4},
		                            @EndNo = {5},
		                            @BranchCode = {6},
		                            @UserCode = '''{7}''',
		                            @SerialNumber = {8},
		                            @DocDate = '{9}',
		                            @Spec = '{10}',
		                            @mDocDate = {11},
		                            @Tanzim = '''{12}''',
		                            @TahieShode = '{13}',
		                            @Eghdam = '''{14}''',
		                            @DocNo_Out = @DocNo_Out OUTPUT
                                    SELECT	'return_value' = @return_value +'-'+  CONVERT(nvarchar, @DOCNO_OUT)",
                            AFI_ADocHi_i.DocNoMode,
                            AFI_ADocHi_i.InsertMode,
                            AFI_ADocHi_i.ModeCode,
                            AFI_ADocHi_i.DocNo,
                            AFI_ADocHi_i.StartNo,
                            AFI_ADocHi_i.EndNo,
                            AFI_ADocHi_i.BranchCode,
                            AFI_ADocHi_i.UserCode,
                            AFI_ADocHi_i.SerialNumber,
                            AFI_ADocHi_i.DocDate ?? string.Format("{0:yyyy/MM/dd}", DateTime.Now.AddDays(-1)),
                            AFI_ADocHi_i.Spec,
                            AFI_ADocHi_i.mDocDate, 
                            AFI_ADocHi_i.Tanzim,
                            AFI_ADocHi_i.TahieShode,
                            AFI_ADocHi_i.Eghdam);
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
            }
            return Ok(value);
        }



        // DELETE: api/AFI_ADocHi/5
        [Route("api/AFI_ADocHi/{ace}/{sal}/{group}/{SerialNumber}/{userName}/{password}")]
        public async Task<IHttpActionResult> DeleteAFI_ADocHi(string ace, string sal, string group, long SerialNumber, string userName, string password)
        {
            if (UnitDatabase.CreateConection(userName, password, ace, sal, group))
            {
                string sql = string.Format(@"DECLARE	@return_value int
                                                 EXEC	@return_value = [dbo].[Web_SaveADoc_Del]
		                                                @SerialNumber = {0}
                                                 SELECT	'Return Value' = @return_value"
                                            , SerialNumber);

                int value = UnitDatabase.db.Database.SqlQuery<int>(sql).Single();
                if (value > 0)
                {
                    await UnitDatabase.db.SaveChangesAsync();
                }
            }
            return Ok(1);
        }

    }
}