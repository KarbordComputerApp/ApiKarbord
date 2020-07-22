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