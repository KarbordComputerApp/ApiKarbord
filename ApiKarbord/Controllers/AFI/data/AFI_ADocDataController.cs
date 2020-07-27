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
using System.Web.Http.Results;
using ApiKarbord.Controllers.Unit;
using ApiKarbord.Models;
using Newtonsoft.Json;


namespace ApiKarbord.Controllers.AFI.data
{
    public class AFI_ADocDataController : ApiController
    {
        // GET: api/ADocData/AMode اطلاعات نوع سند حسابداری   
        [Route("api/ADocData/AMode/{ace}/{sal}/{group}/{userName}/{password}")]
        public async Task<IHttpActionResult> GetWeb_AMode(string ace, string sal, string group, string userName, string password)
        {
            if (UnitDatabase.CreateConection(userName, password, ace, sal, group))
            {
                string sql = "SELECT * FROM Web_AMode";
                var listAMode = UnitDatabase.db.Database.SqlQuery<Web_AMode>(sql);
                return Ok(listAMode);
            }
            return null;
        }

        // GET: api/ADocData/CheckStatus اطلاعات نوع سند حسابداری   
        [Route("api/ADocData/CheckStatus/{ace}/{sal}/{group}/{PDMode}/{Report}/{userName}/{password}")]

        public IQueryable<Web_CheckStatus> GetWeb_CheckStatus(string ace, string sal, string group, int PDMode, int Report, string userName, string password)
        {
            if (UnitDatabase.CreateConection(userName, password, ace, sal, group))
            {
                return UnitDatabase.db.Web_CheckStatus.Where(c => c.PDMode == PDMode && c.Report == Report);
            }
            return null;
        }

        // GET: api/ADocData/ADocH لیست فاکتور    
        [Route("api/ADocData/ADocH/{ace}/{sal}/{group}/top{select}/{user}/{AccessSanad}/{userName}/{password}")]
        public async Task<IHttpActionResult> GetAllWeb_ADocH(string ace, string sal, string group, int select, string user, bool accessSanad, string userName, string password)
        {
            if (UnitDatabase.CreateConection(userName, password, ace, sal, group))
            {
                string sql = "select ";
                if (select == 0)
                    sql += " top(100) ";
                sql += string.Format(@" * from Web_ADocH where 1 = 1 ");
                if (accessSanad == false)
                    sql += " and Eghdam = '" + user + "' ";
                sql += " order by SortDocNo desc ";
                var listADocH = UnitDatabase.db.Database.SqlQuery<Web_ADocH>(sql);
                return Ok(listADocH);
            }
            return null;
        }

        // GET: api/ADocData/ADocB اطلاعات تکمیلی سند    
        [Route("api/ADocData/ADocB/{ace}/{sal}/{group}/{serialNumber}/{userName}/{password}")]
        public async Task<IHttpActionResult> GetWeb_ADocB(string ace, string sal, string group, long serialNumber, string userName, string password)
        {
            if (UnitDatabase.CreateConection(userName, password, ace, sal, group))
            {
                string sql = string.Format(@"SELECT *  FROM Web_ADocB WHERE SerialNumber = {0}", serialNumber);
                var listSanad = UnitDatabase.db.Database.SqlQuery<Web_ADocB>(sql);
                return Ok(listSanad);
            }
            return null;
        }


        // GET: api/ADocData/ADocH اخرین تاریخ    
        [Route("api/ADocData/ADocH/LastDate/{ace}/{sal}/{group}/{InOut}/{userName}/{password}")]
        public async Task<IHttpActionResult> GetWeb_ADocHLastDate(string ace, string sal, string group, string userName, string password)
        {
            if (UnitDatabase.CreateConection(userName, password, ace, sal, group))
            {
                string lastdate;
                string sql = string.Format(@"SELECT count(DocDate) FROM Web_ADocH");
                int count = UnitDatabase.db.Database.SqlQuery<int>(sql).Single();
                if (count > 0)
                {
                    sql = string.Format(@"SELECT top(1) DocDate FROM Web_ADocH order by DocDate desc");
                    lastdate = UnitDatabase.db.Database.SqlQuery<string>(sql).Single();
                }
                else
                    lastdate = "";
                return Ok(lastdate);
            }
            return null;
        }



        // GET: api/ADocData/CheckList اطلاعات چک    
        [Route("api/ADocData/CheckList/{ace}/{sal}/{group}/{userName}/{password}")]
        public async Task<IHttpActionResult> GetWeb_CheckList(string ace, string sal, string group, string userName, string password)
        {
            if (UnitDatabase.CreateConection(userName, password, ace, sal, group))
            {
                string sql = string.Format(@"SELECT *  FROM Web_CheckList");
                var listCheck = UnitDatabase.db.Database.SqlQuery<Web_CheckList>(sql);
                return Ok(listCheck);
            }
            return null;
        }



        public class Web_ValueBank
        {
            public string Name { get; set; }

        }

        // GET: api/ADocData/Bank اطلاعات بانک    
        [Route("api/ADocData/Bank/{ace}/{sal}/{group}/{userName}/{password}")]
        public async Task<IHttpActionResult> GetWeb_Bank(string ace, string sal, string group, string userName, string password)
        {
            if (UnitDatabase.CreateConection(userName, password, ace, sal, group))
            {
                string sql = string.Format(@"SELECT distinct bank as name FROM Web_CheckList  where bank <> ''");
                var listBank = UnitDatabase.db.Database.SqlQuery<Web_ValueBank>(sql);
                return Ok(listBank);
            }
            return null;
        }

        // GET: api/ADocData/Shobe اطلاعات شعبه    
        [Route("api/ADocData/Shobe/{ace}/{sal}/{group}/{userName}/{password}")]
        public async Task<IHttpActionResult> GetWeb_Shobe(string ace, string sal, string group, string userName, string password)
        {
            if (UnitDatabase.CreateConection(userName, password, ace, sal, group))
            {
                string sql = string.Format(@"SELECT distinct Shobe as name FROM Web_CheckList  where Shobe <> ''");
                var listShobe = UnitDatabase.db.Database.SqlQuery<Web_ValueBank>(sql);
                return Ok(listShobe);
            }
            return null;
        }

        // GET: api/ADocData/Jari اطلاعات جاری    
        [Route("api/ADocData/Jari/{ace}/{sal}/{group}/{userName}/{password}")]
        public async Task<IHttpActionResult> GetWeb_Jari(string ace, string sal, string group, string userName, string password)
        {
            if (UnitDatabase.CreateConection(userName, password, ace, sal, group))
            {
                string sql = string.Format(@"SELECT distinct Jari as name FROM Web_CheckList where Jari <> ''");
                var listJari = UnitDatabase.db.Database.SqlQuery<Web_ValueBank>(sql);
                return Ok(listJari);
            }
            return null;
        }

    }
}
