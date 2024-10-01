using System.Data.Entity.Infrastructure;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Http.Results;
using ApiKarbord.Controllers.Unit;
using ApiKarbord.Models;
using Newtonsoft.Json;

namespace ApiKarbord.Controllers.AFI.data
{
    public class AFI_LinkController : ApiController
    {

        public class LinkFDocADocObject
        {
            public long SerialNumber { get; set; }

            public int AddminMode { get; set; }

            public string TahieShode { get; set; }

        }


        public class LinkFDocADoc
        {
            public byte Test { get; set; }

            public string TestName { get; set; }

            public string TestCap { get; set; }

            public int BandNo { get; set; }

            public string AccCode { get; set; }

        }



        [Route("api/Link/LinkFDocADoc/{ace}/{sal}/{group}")]
        public async Task<IHttpActionResult> PostWeb_LinkFDocADoc(string ace, string sal, string group, LinkFDocADocObject d)
        {
            string dBName = UnitDatabase.DatabaseName(ace, sal, group);
            var dataAccount = UnitDatabase.ReadUserPassHeader(this.Request.Headers);
            string sql = string.Format(CultureInfo.InvariantCulture,
                     @"EXEC	{0}.[dbo].[Web_LinkFDocADoc] 
                            @serialNumber = {1} ,
                            @addminMode = {2} ,
                            @userCode = N'{3}' , 
                            @TahieShode = N'{4}'",
                     dBName,
                     d.SerialNumber,
                     d.AddminMode,
                     dataAccount[2],
                     d.TahieShode);

            var DBase = UnitDatabase.dataDB.Where(p => p.UserName.ToUpper() == dataAccount[0].ToUpper() && p.Password == dataAccount[1]).Single();
            string res = UnitDatabase.TestAcount(DBase, dataAccount[3], ace, group, UnitPublic.access_ADOC);
            if (res == "")
            {
                var result = DBase.DB.Database.SqlQuery<LinkFDocADoc>(sql).ToList();
                UnitDatabase.SaveLog(dataAccount[0], dataAccount[1], dataAccount[2], ace, sal, group, 0, UnitPublic.access_ADOC, UnitPublic.act_New, "Y", 1, 0);
                return Ok(result);

            }
            else
                return Ok(res);
        }




        public class LinkFDocIDocObject
        {
            public long SerialNumber { get; set; }


            public string TahieShode { get; set; }

        }


        public class LinkFDocIDoc
        {
            public byte Test { get; set; }

            public string TestName { get; set; }

            public string TestCap { get; set; }

            public int BandNo { get; set; }

            public string KalaCode { get; set; }

            public string KalaName { get; set; }

        }



        [Route("api/Link/LinkFDocIDoc/{ace}/{sal}/{group}")]
        public async Task<IHttpActionResult> PostWeb_LinkFDocIDoc(string ace, string sal, string group, LinkFDocIDocObject d)
        {
            string dBName = UnitDatabase.DatabaseName(ace, sal, group);
            var dataAccount = UnitDatabase.ReadUserPassHeader(this.Request.Headers);
            string sql = string.Format(CultureInfo.InvariantCulture,
                     @"EXEC	{0}.[dbo].[Web_LinkFDocIDoc_New] 
                            @serialNumber = {1} ,
                            @userCode = N'{2}' , 
                            @TahieShode = N'{3}' ",
                     dBName,
                     d.SerialNumber,
                     dataAccount[2],
                     d.TahieShode);

            var DBase = UnitDatabase.dataDB.Where(p => p.UserName.ToUpper() == dataAccount[0].ToUpper() && p.Password == dataAccount[1]).Single();
            string res = UnitDatabase.TestAcount(DBase, dataAccount[3], ace, group, UnitPublic.access_IODOC);
            if (res == "")
            {
                var result = DBase.DB.Database.SqlQuery<LinkFDocIDoc>(sql).ToList();
                UnitDatabase.SaveLog(dataAccount[0], dataAccount[1], dataAccount[2], ace, sal, group, 0, UnitPublic.access_IODOC, UnitPublic.act_New, "Y", 1, 0);
                return Ok(result);

            }
            else
                return Ok(res);
        }




    }
}