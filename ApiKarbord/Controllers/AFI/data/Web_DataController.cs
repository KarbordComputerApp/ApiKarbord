using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Http.Results;
using ApiKarbord.Controllers.Unit;
using ApiKarbord.Models;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.IO;
using System.Drawing;
using System.IO.Compression;

using System.Web;
using System.Data.SqlClient;
using System.Reflection;
using System.Runtime.InteropServices;

namespace ApiKarbord.Controllers.AFI.data
{
    public class Web_DataController : ApiController
    {


        public class CustObject
        {
            public string updatedate { get; set; }

            public bool? forSale { get; set; }

            public byte Mode { get; set; }

            public string UserCode { get; set; }

        }

        public static readonly object LockObject = new object();

        // Post: api/Web_Data/Cust لیست اشخاص
        [Route("api/Web_Data/Cust/{ace}/{sal}/{group}")]
        public async Task<IHttpActionResult> PostWeb_Cust(string ace, string sal, string group, CustObject custObject)
        {
            // var dataAccount = UnitDatabase.ReadUserPassHeader(this.Request.Headers);
            //      string conStr = UnitDatabase.CreateConnectionString(dataAccount[0], dataAccount[1], dataAccount[2], ace, sal, group, 0, "", 0, 0);
            string sql = "";
            if (custObject.forSale == null)
            {
                sql = string.Format("select  * FROM  Web_Cust_F({0},'{1}') where 1 = 1 ", custObject.Mode, custObject.UserCode);
            }
            else if (custObject.forSale == true)
            {
                sql = string.Format("select * FROM  Web_Cust_F({0},'{1}') where CustMode = 0 or CustMode = 1 ", custObject.Mode, custObject.UserCode);
            }
            else if (custObject.forSale == false)
            {
                sql = string.Format("select  * FROM  Web_Cust_F({0},'{1}') where CustMode = 0 or CustMode = 2 ", custObject.Mode, custObject.UserCode);
            }

            if (custObject.updatedate != null)
                sql += " and updatedate >= CAST('" + custObject.updatedate + "' AS DATETIME2)";

            var dataAccount = UnitDatabase.ReadUserPassHeader(this.Request.Headers);
            string conStr = UnitDatabase.CreateConnectionString(dataAccount[0], dataAccount[1], dataAccount[2], ace, sal, group, 0, "", 0, 0);

            if (conStr.Length > 100)
            {
                ApiModel db = new ApiModel(conStr);
                var listCust = db.Database.SqlQuery<Web_Cust>(sql);
                return Ok(listCust);
            }
            return Ok(conStr);
        }

        public class KalaObject
        {
            public bool? withimage { get; set; }

            public string updatedate { get; set; }

            public byte Mode { get; set; }

            public string UserCode { get; set; }
        }

        // Post: api/Web_Data/Kala لیست کالا ها
        [Route("api/Web_Data/Kala/{ace}/{sal}/{group}")]
        public async Task<IHttpActionResult> PostWeb_Kala(string ace, string sal, string group, KalaObject kalaObject)
        {
            string sql = @"SELECT Eghdam, EghdamDate, UpdateUser, UpdateDate, Code, Name,SortName, Spec, UnitName1, UnitName2, UnitName3, Zarib1, Zarib2, Zarib3, FanniNo,isnull(KGruCode,'') as KGruCode,isnull(KGruName,'') as KGruName,
                                   KalaF01, KalaF02, KalaF03, KalaF04, KalaF05, KalaF06, KalaF07, KalaF08, KalaF09, KalaF10, KalaF11, KalaF12, KalaF13, KalaF14, KalaF15, KalaF16, KalaF17, KalaF18, KalaF19, KalaF20, DeghatR1, DeghatR2
                                   , DeghatR3, DeghatM1, DeghatM2, DeghatM3, PPrice1, PPrice2, PPrice3, SPrice1, SPrice2, SPrice3,BarCode,DefaultUnit";
            if (kalaObject.withimage == true)
                sql += ",KalaImage ";
            else
                sql += ",null as KalaImage ";

            sql += string.Format(" FROM Web_Kala_f({0},'{1}') where 1 = 1 ", kalaObject.Mode, kalaObject.UserCode);

            if (kalaObject.updatedate != null)
                sql += " and updatedate >= CAST('" + kalaObject.updatedate + "' AS DATETIME2)";

            var dataAccount = UnitDatabase.ReadUserPassHeader(this.Request.Headers);
            string conStr = UnitDatabase.CreateConnectionString(dataAccount[0], dataAccount[1], dataAccount[2], ace, sal, group, 0, "", 0, 0);

            if (conStr.Length > 100)
            {
                ApiModel db = new ApiModel(conStr);
                var listKala = db.Database.SqlQuery<Web_Kala>(sql);
                return Ok(listKala);
            }
            return Ok(conStr);
        }



        public class CGruObject
        {
            public byte Mode { get; set; }

            public short ModeGru { get; set; }

            public string UserCode { get; set; }
        }

        // Post: api/Web_Data/CGru لیست گروه اشخاص 
        [Route("api/Web_Data/CGru/{ace}/{sal}/{group}")]
        public async Task<IHttpActionResult> PostWeb_CGru(string ace, string sal, string group, CGruObject cGruObject)
        {
            string sql = string.Format("select  * FROM  Web_CGru_F({0},'{1}')", cGruObject.Mode, cGruObject.UserCode);
            var dataAccount = UnitDatabase.ReadUserPassHeader(this.Request.Headers);
            string conStr = UnitDatabase.CreateConnectionString(dataAccount[0], dataAccount[1], dataAccount[2], ace, sal, group, 0, "", 0, 0);
            if (conStr.Length > 100)
            {
                ApiModel db = new ApiModel(conStr);
                var listCGru = db.Database.SqlQuery<Web_CGru>(sql);
                return Ok(listCGru);
            }
            return Ok(conStr);
        }


        public class AccObject
        {
            public byte Mode { get; set; }

            public string UserCode { get; set; }

        }

        // Post: api/Web_Data/Acc لیست حساب ها
        [Route("api/Web_Data/Acc/{ace}/{sal}/{group}")]
        public async Task<IHttpActionResult> PostWeb_Acc(string ace, string sal, string group, AccObject accObject)
        {
            string sql = string.Format("select  *  FROM  Web_Acc_F({0},'{1}') where 1 = 1 order by SortCode ", accObject.Mode, accObject.UserCode);

            var dataAccount = UnitDatabase.ReadUserPassHeader(this.Request.Headers);
            string conStr = UnitDatabase.CreateConnectionString(dataAccount[0], dataAccount[1], dataAccount[2], ace, sal, group, 0, "", 0, 0);
            if (conStr.Length > 100)
            {
                ApiModel db = new ApiModel(conStr);
                var listAcc = db.Database.SqlQuery<Web_Acc>(sql);
                return Ok(listAcc);
            }
            return Ok(conStr);
        }

        // GET: api/Web_Data/ZAcc لیست زیر حساب ها
        [Route("api/Web_Data/ZAcc/{ace}/{sal}/{group}/{filter}")]
        public async Task<IHttpActionResult> GetWeb_ZAcc(string ace, string sal, string group, string filter)
        {
            string sql;
            if (filter == "null" || filter == "0")
                sql = string.Format(@" select *  from Web_ZAcc");
            else
                sql = string.Format(@" select *  from Web_ZAcc where ZGruCode in ({0})", filter);

            var dataAccount = UnitDatabase.ReadUserPassHeader(this.Request.Headers);
            string conStr = UnitDatabase.CreateConnectionString(dataAccount[0], dataAccount[1], dataAccount[2], ace, sal, group, 0, "", 0, 0);
            if (conStr.Length > 100)
            {
                ApiModel db = new ApiModel(conStr);
                var list = db.Database.SqlQuery<Web_ZAcc>(sql);
                return Ok(list);
            }
            return Ok(conStr);
        }



        // GET: api/Web_Data/KalaPrice لیست گروه قیمت خرید و فروش
        [Route("api/Web_Data/KalaPrice/{ace}/{sal}/{group}/{insert}")]
        public IQueryable<Web_KalaPrice> GetWeb_KalaPrice(string ace, string sal, string group, bool insert)
        {
            var dataAccount = UnitDatabase.ReadUserPassHeader(this.Request.Headers);
            string conStr = UnitDatabase.CreateConnectionString(dataAccount[0], dataAccount[1], dataAccount[2], ace, sal, group, 0, "", 0, 0);
            if (conStr.Length > 100)
            {
                ApiModel db = new ApiModel(conStr);
                if (insert)
                    return db.Web_KalaPrice.Where(c => c.Cancel == false);
                else
                    return db.Web_KalaPrice;
            }
            return null;
        }




        // GET: api/Web_Data/KalaPriceB  لیست قیمت کالا بر اساس قیمت گروه
        [Route("api/Web_Data/KalaPriceB/{ace}/{sal}/{group}/{code}/{kalacode}")]
        public IQueryable<Web_KalaPriceB> GetWeb_KalaPriceB(string ace, string sal, string group, int code, string kalacode)
        {
            var dataAccount = UnitDatabase.ReadUserPassHeader(this.Request.Headers);
            string conStr = UnitDatabase.CreateConnectionString(dataAccount[0], dataAccount[1], dataAccount[2], ace, sal, group, 0, "", 0, 0);
            if (conStr.Length > 100)
            {
                ApiModel db = new ApiModel(conStr);
                if (kalacode == "null")
                    return db.Web_KalaPriceB.Where(c => c.Code == code);
                else
                    return db.Web_KalaPriceB.Where(c => c.Code == code && c.KalaCode == kalacode);
            }
            return null;
        }


        // GET: api/Web_Data/Unit لیست واحد کالا
        [Route("api/Web_Data/Unit/{ace}/{sal}/{group}/{codekala}")]
        public IQueryable<Web_Unit> GetWeb_Unit(string ace, string sal, string group, string codeKala)
        {
            var dataAccount = UnitDatabase.ReadUserPassHeader(this.Request.Headers);
            string conStr = UnitDatabase.CreateConnectionString(dataAccount[0], dataAccount[1], dataAccount[2], ace, sal, group, 0, "", 0, 0);
            if (conStr.Length > 100)
            {
                ApiModel db = new ApiModel(conStr);
                return from p in db.Web_Unit where p.KalaCode == codeKala && p.Name != "" select p;
            }
            return null;
        }




        // GET: api/Web_Data/Inv لیست انبار ها
        [Route("api/Web_Data/Inv/{ace}/{sal}/{group}/{Mode}/{UserCode}")]
        public async Task<IHttpActionResult> GetWeb_Inv(string ace, string sal, string group, int Mode, string UserCode)
        {
            string sql = string.Format(@"select * from Web_Inv_F({0},'{1}')", Mode, UserCode);

            var dataAccount = UnitDatabase.ReadUserPassHeader(this.Request.Headers);
            string conStr = UnitDatabase.CreateConnectionString(dataAccount[0], dataAccount[1], dataAccount[2], ace, sal, group, 0, "", 0, 0);
            if (conStr.Length > 100)
            {
                ApiModel db = new ApiModel(conStr);
                var list = db.Database.SqlQuery<Web_Inv>(sql);
                return Ok(list);
            }
            return Ok(conStr);
        }

        // GET: api/Web_Data/Param لیست پارامتر ها  
        [Route("api/Web_Data/Param/{ace}/{sal}/{group}")]
        public IQueryable<Web_Param> GetWeb_Param(string ace, string sal, string group)
        {
            var dataAccount = UnitDatabase.ReadUserPassHeader(this.Request.Headers);
            string conStr = UnitDatabase.CreateConnectionString(dataAccount[0], dataAccount[1], dataAccount[2], ace, sal, group, 0, "Param", 0, 0);
            if (conStr.Length > 100)
            {
                ApiModel db = new ApiModel(conStr);
                return db.Web_Param;
            }
            return null;
        }

        // GET: api/Web_Data/Payment لیست نحوه پرداخت  
        [Route("api/Web_Data/Payment/{ace}/{sal}/{group}")]
        public IQueryable<Web_Payment> GetWeb_Payment(string ace, string sal, string group)
        {
            var dataAccount = UnitDatabase.ReadUserPassHeader(this.Request.Headers);
            string conStr = UnitDatabase.CreateConnectionString(dataAccount[0], dataAccount[1], dataAccount[2], ace, sal, group, 0, "", 0, 0);
            if (conStr.Length > 100)
            {
                ApiModel db = new ApiModel(conStr);
                return db.Web_Payment.OrderBy(c => c.OrderFld);
            }
            return null;
        }

        // GET: api/Web_Data/Status لیست وضعیت پرداخت  
        [Route("api/Web_Data/Status/{ace}/{sal}/{group}/{progname}")]
        public IQueryable<Web_Status> GetWeb_Status(string ace, string sal, string group, string progname)
        {
            var dataAccount = UnitDatabase.ReadUserPassHeader(this.Request.Headers);
            string conStr = UnitDatabase.CreateConnectionString(dataAccount[0], dataAccount[1], dataAccount[2], ace, sal, group, 0, "", 0, 0);
            if (conStr.Length > 100)
            {
                ApiModel db = new ApiModel(conStr);
                return db.Web_Status.Where(c => c.Prog == progname);
            }
            return null;
        }


        public class CalcAddmin
        {
            public bool forSale { get; set; }

            public long serialNumber { get; set; }

            public string custCode { get; set; }

            public byte typeJob { get; set; }
            public string spec1 { get; set; }
            public string spec2 { get; set; }
            public string spec3 { get; set; }
            public string spec4 { get; set; }
            public string spec5 { get; set; }
            public string spec6 { get; set; }
            public string spec7 { get; set; }
            public string spec8 { get; set; }
            public string spec9 { get; set; }
            public string spec10 { get; set; }

            public double? MP1 { get; set; }
            public double? MP2 { get; set; }
            public double? MP3 { get; set; }
            public double? MP4 { get; set; }
            public double? MP5 { get; set; }
            public double? MP6 { get; set; }
            public double? MP7 { get; set; }
            public double? MP8 { get; set; }
            public double? MP9 { get; set; }
            public double? MP10 { get; set; }

            public string flagTest { get; set; }
        }

        // Post: api/Web_Data/AddMin لیست کسورات و افزایشات   
        // [Route("api/Web_Data/AddMin/{ace}/{sal}/{group}/{forSale}/{serialNumber}/{custCode}/{TypeJob}/{Spec1}/{Spec2}/{Spec3}/{Spec4}/{Spec5}/{Spec6}/{Spec7}/{Spec8}/{Spec9}/{Spec10}/{MP1}/{MP2}/{MP3}/{MP4}/{MP5}/{MP6}/{MP7}/{MP8}/{MP9}/{MP10}/")]
        [ResponseType(typeof(CalcAddmin))]
        [Route("api/Web_Data/AddMin/{ace}/{sal}/{group}")]

        public async Task<IHttpActionResult> PostGetAddMin(string ace, string sal, string group, CalcAddmin calcAddmin)
        {

            string sql = string.Format(CultureInfo.InvariantCulture, @"EXEC	[dbo].[{24}]
		                                            @serialNumber = {0},
                                                    @forSale = {1},
                                                    @custCode = {2},
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
                                                    @MP1 = '{14}',
                                                    @MP2 = {15},
                                                    @MP3 = {16},
		                                            @MP4 = {17},
		                                            @MP5 = {18},
		                                            @MP6 = {19},
		                                            @MP7 = {20},
		                                            @MP8 = {21},
		                                            @MP9 = {22},
		                                            @MP10 = {23}
                                                    ",
                                    calcAddmin.serialNumber,
                                    calcAddmin.forSale,
                                    calcAddmin.custCode ?? "null",
                                    calcAddmin.typeJob,
                                    calcAddmin.spec1,
                                    calcAddmin.spec2,
                                    calcAddmin.spec3,
                                    calcAddmin.spec4,
                                    calcAddmin.spec5,
                                    calcAddmin.spec6,
                                    calcAddmin.spec7,
                                    calcAddmin.spec8,
                                    calcAddmin.spec9,
                                    calcAddmin.spec10,
                                    calcAddmin.MP1 ?? 0,
                                    calcAddmin.MP2 ?? 0,
                                    calcAddmin.MP3 ?? 0,
                                    calcAddmin.MP4 ?? 0,
                                    calcAddmin.MP5 ?? 0,
                                    calcAddmin.MP6 ?? 0,
                                    calcAddmin.MP7 ?? 0,
                                    calcAddmin.MP8 ?? 0,
                                    calcAddmin.MP9 ?? 0,
                                    calcAddmin.MP10 ?? 0,
                                    calcAddmin.flagTest == "Y" ? "Web_Calc_AddMin_EffPrice_Temp" : "Web_Calc_AddMin_EffPrice"
                                    );


            var dataAccount = UnitDatabase.ReadUserPassHeader(this.Request.Headers);
            string conStr = UnitDatabase.CreateConnectionString(dataAccount[0], dataAccount[1], dataAccount[2], ace, sal, group, calcAddmin.serialNumber, "", 0, 0);
            if (conStr.Length > 100)
            {
                ApiModel db = new ApiModel(conStr);
                var result = db.Database.SqlQuery<AddMin>(sql).Where(c => c.Name != "").ToList();
                var jsonResult = JsonConvert.SerializeObject(result);
                return Ok(jsonResult);
            }
            return Ok(conStr);
        }








        public class TashimBand
        {
            public long SerialNumber { get; set; }

            public bool ForSale { get; set; }

            public int Deghat { get; set; }

            public double? MP1 { get; set; }

            public double? MP2 { get; set; }

            public double? MP3 { get; set; }

            public double? MP4 { get; set; }

            public double? MP5 { get; set; }

            public double? MP6 { get; set; }

            public double? MP7 { get; set; }

            public double? MP8 { get; set; }

            public double? MP9 { get; set; }

            public double? MP10 { get; set; }
        }

        // Post: api/Web_Data/TashimBand لیست کسورات و افزایشات   
        [ResponseType(typeof(void))]
        [Route("api/Web_Data/TashimBand/{ace}/{sal}/{group}")]

        public async Task<IHttpActionResult> PostTashimBand(string ace, string sal, string group, TashimBand tashimBand)
        {

            string sql = string.Format(CultureInfo.InvariantCulture, @"DECLARE	@return_value int
                             EXEC	@return_value = [dbo].[Web_FDocB_CalcAddMin_Temp]
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
                              tashimBand.SerialNumber,
                              tashimBand.Deghat,
                              tashimBand.ForSale,
                              tashimBand.MP1,
                              tashimBand.MP2,
                              tashimBand.MP3,
                              tashimBand.MP4,
                              tashimBand.MP5,
                              tashimBand.MP6,
                              tashimBand.MP7,
                              tashimBand.MP8,
                              tashimBand.MP9,
                              tashimBand.MP10);

            var dataAccount = UnitDatabase.ReadUserPassHeader(this.Request.Headers);
            string conStr = UnitDatabase.CreateConnectionString(dataAccount[0], dataAccount[1], dataAccount[2], ace, sal, group, tashimBand.SerialNumber, "", 0, 0);
            if (conStr.Length > 100)
            {
                ApiModel db = new ApiModel(conStr);
                int list = db.Database.SqlQuery<int>(sql).Single();
                return Ok(list);
            }
            return Ok(conStr);
        }


        //انبار---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------



        public class ThvlObject
        {
            public byte Mode { get; set; }

            public string UserCode { get; set; }
        }

        // Post: api/Web_Data/Thvl  لیست تحویل دهنده گیرنده  
        [Route("api/Web_Data/Thvl/{ace}/{sal}/{group}")]
        public async Task<IHttpActionResult> PostWeb_Thvl(string ace, string sal, string group, ThvlObject thvlObject)
        {

            string sql = string.Format("select  * FROM  Web_Thvl_F({0},'{1}')", thvlObject.Mode, thvlObject.UserCode);
            var dataAccount = UnitDatabase.ReadUserPassHeader(this.Request.Headers);
            string conStr = UnitDatabase.CreateConnectionString(dataAccount[0], dataAccount[1], dataAccount[2], ace, sal, group, 0, "", 0, 0);
            if (conStr.Length > 100)
            {
                ApiModel db = new ApiModel(conStr);
                var list = db.Database.SqlQuery<Web_Thvl>(sql);
                return Ok(list);
            }
            return Ok(conStr);

        }



        public class TGruObject
        {
            public byte Mode { get; set; }

            public string UserCode { get; set; }
        }

        // Post: api/Web_Data/TGru  لیست گروه تحویل دهنده گیرنده  
        [Route("api/Web_Data/TGru/{ace}/{sal}/{group}")]
        public async Task<IHttpActionResult> PostWeb_TGru(string ace, string sal, string group, TGruObject tGruObject)
        {
            string sql = string.Format("select  * FROM  Web_TGru_F({0},'{1}')", tGruObject.Mode, tGruObject.UserCode);
            var dataAccount = UnitDatabase.ReadUserPassHeader(this.Request.Headers);
            string conStr = UnitDatabase.CreateConnectionString(dataAccount[0], dataAccount[1], dataAccount[2], ace, sal, group, 0, "", 0, 0);
            if (conStr.Length > 100)
            {
                ApiModel db = new ApiModel(conStr);
                var list = db.Database.SqlQuery<Web_TGru>(sql);
                return Ok(list);
            }
            return Ok(conStr);
        }



        public class LoginObject
        {
            public string userName { get; set; }

            public string pass { get; set; }

            public string param1 { get; set; }

            public string param2 { get; set; }

        }

        // Post: api/Web_Data/ اطلاعات لاگین   
        [Route("api/Web_Data/Login")]
        public async Task<IHttpActionResult> PostWeb_Login(LoginObject LoginObject)//string user, string pass, string param1, string param2)
        {
            if (LoginObject.pass == "null")
                LoginObject.pass = "";
            string sql = string.Format(@" DECLARE  @return_value int, @name nvarchar(100)
                                              EXEC     @return_value = [dbo].[Web_Login]
                                                       @Code1 = '{0}',
		                                               @UserCode = N'{1}',
                                                       @Code2 = '{2}',
		                                               @Psw = N'{3}',
                                                       @Name = @name OUTPUT
                                              SELECT   'Return Value' = CONVERT(nvarchar, @return_value) + '-' +  @Name ",
                                           LoginObject.param1, LoginObject.userName, LoginObject.param2, LoginObject.pass);


            var dataAccount = UnitDatabase.ReadUserPassHeader(this.Request.Headers);
            string conStr = UnitDatabase.CreateConnectionString(dataAccount[0], dataAccount[1], dataAccount[2], "Config", "", "00", 0, "", 0, 0);
            if (conStr.Length > 100)
            {
                ApiModel db = new ApiModel(conStr);

                string value = db.Database.SqlQuery<string>(sql).Single();
                string[] values = value.Split('-');
                if (values[0] == "1")
                    return Ok(value);
                else
                    return Ok(0);
            }
            return Ok(conStr);
        }


        public class LoginTestObject
        {
            public string MachineId { get; set; }

            public string IPWan { get; set; }

            public string Country { get; set; }

            public string City { get; set; }

            public string UserCode { get; set; }

            //public string LoginTimeas { get; set; }
            //public string LoginDate { get; set; }
            public string ProgName { get; set; }

            public string ProgVer { get; set; }

            public string ProgCaption { get; set; }

            public Boolean FlagTest { get; set; }

            public string GroupNo { get; set; }

            public string Year { get; set; }
        }

        public class Web_LoginTestObject
        {
            public int? ID { get; set; }

            public string CompName { get; set; }

            public string UserCode { get; set; }

            public string LoginTime { get; set; }

            public string LoginDate { get; set; }

            public string ProgName { get; set; }

            public string ProgVer { get; set; }

            public string ProgCaption { get; set; }

            public string SrvDate { get; set; }

            public int CountErja { get; set; }

            public DateTime? UpdateDate { get; set; }

        }



        // Post: api/Web_Data/ تست وجود کاربر    
        [Route("api/Web_Data/LoginTest")]
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PostWeb_LoginTest(LoginTestObject LoginTestObject)
        {
            PersianCalendar pc = new System.Globalization.PersianCalendar();

            string year = pc.GetYear(DateTime.Now).ToString();
            string month = pc.GetMonth(DateTime.Now).ToString();
            string day = pc.GetDayOfMonth(DateTime.Now).ToString();

            month = month.Length == 1 ? "0" + month : month;
            day = day.Length == 1 ? "0" + day : day;

            string PDate = year + "/" + month + "/" + day;

            string sql = string.Format("SELECT count(ID) as id FROM Ace_Config.dbo.UserIn WHERE ProgName IN ('Web1', 'Web2', 'Web8') and usercode <> '{0}'", LoginTestObject.UserCode);

            var dataAccount = UnitDatabase.ReadUserPassHeader(this.Request.Headers);
            string conStr = UnitDatabase.CreateConnectionString(dataAccount[0], dataAccount[1], dataAccount[2], "Config", "", "", 0, "", 0, 0);
            if (conStr.Length > 100)
            {
                ApiModel db = new ApiModel(conStr);
                var countUserIn = db.Database.SqlQuery<int>(sql).First().ToString();

                var list = UnitDatabase.model.First();
                int userCount = list.userCount ?? 0;
                if (Int32.Parse(countUserIn) >= userCount)
                {
                    return Ok("MaxCount");
                }

                string Time = DateTime.Now.ToString("HH:mm");
                sql = string.Format(@" EXEC	[dbo].[Web_TestLogin]
		                                            @MachineId = N'{0}',
		                                            @IPWan = N'{1}',
		                                            @Country = N'{2}',
		                                            @City = N'{3}',
		                                            @UserCode = N'{4}',
		                                            @LoginTime = N'{5}',
		                                            @LoginDate = N'{6}',
		                                            @ProgName = N'{7}',
		                                            @ProgVer = N'{8}',
		                                            @ProgCaption = N'{9}',
		                                            @FlagTest = {10},
                                                    @GroupNo = '{11}',
                                                    @Year = '{12}'",
                                              LoginTestObject.MachineId,
                                              LoginTestObject.IPWan,
                                              LoginTestObject.Country,
                                              LoginTestObject.City,
                                              LoginTestObject.UserCode,
                                              Time,
                                              PDate,
                                              LoginTestObject.ProgName,
                                              LoginTestObject.ProgVer,
                                              LoginTestObject.ProgCaption,
                                              LoginTestObject.FlagTest,
                                              LoginTestObject.GroupNo,
                                              LoginTestObject.Year
                                             );
                conStr = UnitDatabase.CreateConnectionString(dataAccount[0], dataAccount[1], dataAccount[2], "Config", "", "", 0, "", 0, 0);
                if (conStr.Length > 100)
                {
                    db = new ApiModel(conStr);
                    var value = db.Database.SqlQuery<Web_LoginTestObject>(sql).Single();
                    return Ok(value);
                }

            }
            return Ok(conStr);
        }


        public class LogOutObject
        {
            public string MachineId { get; set; }

            public string UserCode { get; set; }

            public string ProgName { get; set; }
        }




        // Post: api/Web_Data/ خروج کاربر    
        [Route("api/Web_Data/LogOut")]
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PostWeb_LogOut(LogOutObject LogOutObject)
        {
            string sql = string.Format(@"DECLARE	@return_value int
                                                    EXEC	@return_value =[dbo].[Web_LogOut]
                                                            @MachineId = N'{0}',
		                                                    @UserCode = N'{1}',
		                                                    @ProgName = N'{2}'
                                                 SELECT	'Return Value' = @return_value",
                                                  LogOutObject.MachineId,
                                                  LogOutObject.UserCode,
                                                  LogOutObject.ProgName
                                                 );

            var dataAccount = UnitDatabase.ReadUserPassHeader(this.Request.Headers);
            string conStr = UnitDatabase.CreateConnectionString(dataAccount[0], dataAccount[1], dataAccount[2], "Config", "", "", 0, "", 0, 0);
            if (conStr.Length > 100)
            {
                ApiModel db = new ApiModel(conStr);
                int value = db.Database.SqlQuery<int>(sql).Single();
                return Ok(value);
            }
            return Ok(conStr);
        }


        // دریافت اطلاعات سالهای موجود در اس کیو ال متصل به ای پی ای
        public class DatabseSal
        {
            public string Code { get; set; }

            public string Name { get; set; }
        }

        public class DatabseSalObject
        {
            public string ProgName { get; set; }

            public string Group { get; set; }

            public string UserCode { get; set; }
        }



        [Route("api/Web_Data/DatabseSal")]
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PostWeb_DatabseSal(DatabseSalObject DatabseSalObject)
        {
            string sql = string.Format(@" EXEC	[dbo].[Web_Years]
		                                            @BaseProg = '{0}',
		                                            @GroupNo = '{1}',
		                                            @UserCode = '{2}'",
                                           DatabseSalObject.ProgName,
                                           DatabseSalObject.Group,
                                           DatabseSalObject.UserCode);

            var dataAccount = UnitDatabase.ReadUserPassHeader(this.Request.Headers);
            string conStr = UnitDatabase.CreateConnectionString(dataAccount[0], dataAccount[1], dataAccount[2], "Config", "", "", 0, "", 0, 0);
            if (conStr.Length > 100)
            {
                ApiModel db = new ApiModel(conStr);
                var list = db.Database.SqlQuery<DatabseSal>(sql).ToList();
                return Ok(list);
            }
            return Ok(conStr);
        }





        // دریافت اطلاعات سطح دسترسی کاربر
        public class AccessUser
        {
            public string OrgProgName { get; set; }

            public string TrsName { get; set; }
        }

        [Route("api/Web_Data/AccessUser/{ace}/{group}/{user}")]
        public async Task<IHttpActionResult> GetWeb_AccessUser(string ace, string group, string user)
        {
            string sql = string.Format(@"EXEC [dbo].[Web_UserTrs]
		                                               @ProgName = '{0}',
		                                               @GroupNo = {1},
		                                               @UserCode = '{2}'",
                                                ace, group, user);

            var dataAccount = UnitDatabase.ReadUserPassHeader(this.Request.Headers);
            string conStr = UnitDatabase.CreateConnectionString(dataAccount[0], dataAccount[1], dataAccount[2], "Config", "", "", 0, "", 0, 0);
            if (conStr.Length > 100)
            {
                ApiModel db = new ApiModel(conStr);
                if (!string.IsNullOrEmpty(ace) || !string.IsNullOrEmpty(group) || !string.IsNullOrEmpty(user))
                {
                    var list = db.Database.SqlQuery<AccessUser>(sql).ToList();
                    return Ok(list);
                }
            }
            return Ok(conStr);
        }

        public class AccessUserReport
        {
            public string Code { get; set; }
            public bool Trs { get; set; }
        }

        [Route("api/Web_Data/AccessUserReport/{ace}/{group}/{user}")]
        public async Task<IHttpActionResult> GetWeb_AccessUserReport(string ace, string group, string user)
        {
            string sql = string.Format(@"declare @ace nvarchar(5), @group int , @username nvarchar(20)
                                                 set @ace = '{0}'
                                                 set @group = {1}
                                                 set @username = '{2}'
                                                 select 'TrzIKala'as Code , [dbo].[Web_RprtTrs](@group,@ace,@username,'TrzIKala') as Trs
                                                 union all
                                                 select 'TrzIKalaExf'as Code , [dbo].[Web_RprtTrs](@group,@ace,@username,'TrzIKalaExf') as Trs
                                                 union all
                                                 select 'IDocR'as Code , [dbo].[Web_RprtTrs](@group,@ace,@username,'IDocR') as Trs
                                                 union all
                                                 select 'FDocR_S'as Code , [dbo].[Web_RprtTrs](@group,@ace,@username,'FDocR_S') as Trs
                                                 union all
                                                 select 'FDocR_P'as Code , [dbo].[Web_RprtTrs](@group,@ace,@username,'FDocR_P') as Trs
                                                 union all
                                                 select 'TrzAcc' as Code, [dbo].[Web_RprtTrs](@group, @ace, @username, 'TrzAcc') as Trs
                                                 union all
                                                 select 'Dftr' as Code, [dbo].[Web_RprtTrs](@group, @ace, @username, 'Dftr') as Trs
                                                 union all
                                                 select 'ADocR' as Code, [dbo].[Web_RprtTrs](@group, @ace, @username, 'ADocR') as Trs
                                                 union all
                                                 select 'TChk' as Code, [dbo].[Web_RprtTrs](@group, @ace, @username, 'TChk') as Trs
                                                 union all
                                                 select 'TrzFKala_S' as Code, [dbo].[Web_RprtTrs](@group, @ace, @username, 'TrzFKala_S') as Trs
                                                 union all
                                                 select 'TrzFKala_P' as Code, [dbo].[Web_RprtTrs](@group, @ace, @username, 'TrzFKala_P') as Trs
                                                 union all
                                                 select 'TrzFCust_S' as Code, [dbo].[Web_RprtTrs](@group, @ace, @username, 'TrzFCust_S') as Trs
                                                 union all
                                                 select 'TrzFCust_P' as Code, [dbo].[Web_RprtTrs](@group, @ace, @username, 'TrzFCust_P') as Trs
                                                 union all
                                                 select 'Krdx' as Code, [dbo].[Web_RprtTrs](@group, @ace, @username, 'Krdx') as Trs
                                                 union all
                                                 select 'AGMkz' as Code, [dbo].[Web_RprtTrs](@group, @ace, @username, 'AGMkz') as Trs
                                                 union all
                                                 select 'AGOpr' as Code, [dbo].[Web_RprtTrs](@group, @ace, @username, 'AGOpr') as Trs"
                                       , ace, group, user);


            var dataAccount = UnitDatabase.ReadUserPassHeader(this.Request.Headers);
            string conStr = UnitDatabase.CreateConnectionString(dataAccount[0], dataAccount[1], dataAccount[2], "Config", "", "", 0, "", 0, 0);
            if (conStr.Length > 100)
            {
                ApiModel db = new ApiModel(conStr);
                if (!string.IsNullOrEmpty(ace) || !string.IsNullOrEmpty(group) || !string.IsNullOrEmpty(user))
                {
                    var list = db.Database.SqlQuery<AccessUserReport>(sql).ToList();
                    return Ok(list);
                }
            }
            return Ok(conStr);
        }


        public class AccessUserReportErj
        {
            public string Code { get; set; }
            public bool Trs { get; set; }
        }

        [Route("api/Web_Data/AccessUserReportErj/{ace}/{group}/{user}")]
        public async Task<IHttpActionResult> GetWeb_AccessUserReportErj(string ace, string group, string user)
        {
            string sql = string.Format(@"declare @ace nvarchar(5), @group int , @username nvarchar(20)
                                                 set @ace = '{0}'
                                                 set @group = {1}
                                                 set @username = '{2}'
                                                 select 'ErjDocK'as Code , [dbo].[Web_RprtTrs](@group,@ace,@username,'ErjDocK') as Trs
                                                 union all
                                                 select 'ErjDocErja'as Code , [dbo].[Web_RprtTrs](@group,@ace,@username,'ErjDocErja') as Trs"
                                               , ace, group, user);


            var dataAccount = UnitDatabase.ReadUserPassHeader(this.Request.Headers);
            string conStr = UnitDatabase.CreateConnectionString(dataAccount[0], dataAccount[1], dataAccount[2], "Config", "", "", 0, "", 0, 0);
            if (conStr.Length > 100)
            {
                ApiModel db = new ApiModel(conStr);
                if (!string.IsNullOrEmpty(ace) || !string.IsNullOrEmpty(group) || !string.IsNullOrEmpty(user))
                {
                    var list = db.Database.SqlQuery<AccessUserReportErj>(sql).ToList();
                    return Ok(list);
                }
            }
            return Ok(conStr);
        }



        ///////Erj-----------------------------------------------------------------------------------------------------------------------------



        public class ErjCustObject
        {
            public string userCode { get; set; }

            public byte Mode { get; set; }
        }


        public partial class Web_ErjCust
        {
            public string Code { get; set; }

            public string Name { get; set; }

            public string Spec { get; set; }
        }


        //  Post: api/Web_Data/ErjCust لیست مشتریان ارجاعات

        [Route("api/Web_Data/ErjCust/{ace}/{sal}/{group}")]
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PostWeb_ErjCust(string ace, string sal, string group, ErjCustObject ErjCustObject)
        {
            string sql = string.Format(@"Select code,name,spec from Web_ErjCust_F({0},'{1}')",
                   ErjCustObject.Mode, ErjCustObject.userCode);


            var dataAccount = UnitDatabase.ReadUserPassHeader(this.Request.Headers);
            string conStr = UnitDatabase.CreateConnectionString(dataAccount[0], dataAccount[1], dataAccount[2], ace, sal, group, 0, "", 0, 0);
            if (conStr.Length > 100)
            {
                ApiModel db = new ApiModel(conStr);
                var list = db.Database.SqlQuery<Web_ErjCust>(sql);
                return Ok(list);
            }
            return Ok(conStr);
        }

        public partial class Web_Khdt
        {

            public int Code { get; set; }

            public string Name { get; set; }

            public string Spec { get; set; }

            public byte HasTime { get; set; }
        }


        //  GET: api/Web_Data/Khdt

        [Route("api/Web_Data/Khdt/{ace}/{sal}/{group}")]
        public async Task<IHttpActionResult> GetWeb_Khdt(string ace, string sal, string group)
        {
            string sql = string.Format(@"Select * from Web_Khdt");

            var dataAccount = UnitDatabase.ReadUserPassHeader(this.Request.Headers);
            string conStr = UnitDatabase.CreateConnectionString(dataAccount[0], dataAccount[1], dataAccount[2], ace, sal, group, 0, "", 0, 0);
            if (conStr.Length > 100)
            {
                ApiModel db = new ApiModel(conStr);
                var list = db.Database.SqlQuery<Web_Khdt>(sql);
                return Ok(list);
            }
            return Ok(conStr);
        }




        public class Web_ErjStatus
        {
            public int OrderFld { get; set; }
            public string Status { get; set; }
        }

        // GET: api/Web_Data/ErjStatus لیست وضعیت پرداخت  
        [Route("api/Web_Data/ErjStatus/{ace}/{sal}/{group}")]
        public async Task<IHttpActionResult> GetWeb_ErjStatus(string ace, string sal, string group)
        {
            string sql = string.Format(@"Select * from Web_ErjStatus");
            var dataAccount = UnitDatabase.ReadUserPassHeader(this.Request.Headers);
            string conStr = UnitDatabase.CreateConnectionString(dataAccount[0], dataAccount[1], dataAccount[2], ace, sal, group, 0, "", 0, 0);
            if (conStr.Length > 100)
            {
                ApiModel db = new ApiModel(conStr);
                var list = db.Database.SqlQuery<Web_ErjStatus>(sql);
                return Ok(list);
            }
            return Ok(conStr);
        }


        public class Web_ErjDocYears
        {
            public string Year { get; set; }
        }

        // GET: api/Web_Data/DocYears لیست سال پرونده ها  
        [Route("api/Web_Data/ErjDocYears/{ace}/{sal}/{group}")]
        public async Task<IHttpActionResult> GetWeb_ErjDocYears(string ace, string sal, string group)
        {
            string sql = string.Format(@"Select * from Web_DocYears order by Year Desc");
            var dataAccount = UnitDatabase.ReadUserPassHeader(this.Request.Headers);
            string conStr = UnitDatabase.CreateConnectionString(dataAccount[0], dataAccount[1], dataAccount[2], ace, sal, group, 0, "", 0, 0);
            if (conStr.Length > 100)
            {
                ApiModel db = new ApiModel(conStr);
                var list = db.Database.SqlQuery<Web_ErjDocYears>(sql);
                return Ok(list);
            }
            return Ok(conStr);
        }



        public class Web_RjStatus
        {
            public string Name { get; set; }
        }

        // GET: api/Web_Data/RjStatus لیست وضعیت ارجاع  
        [Route("api/Web_Data/RjStatus/{ace}/{sal}/{group}")]
        public async Task<IHttpActionResult> GetWeb_RjStatus(string ace, string sal, string group)
        {
            string sql = string.Format(@"Select * from Web_RjStatus");
            var dataAccount = UnitDatabase.ReadUserPassHeader(this.Request.Headers);
            string conStr = UnitDatabase.CreateConnectionString(dataAccount[0], dataAccount[1], dataAccount[2], ace, sal, group, 0, "", 0, 0);
            if (conStr.Length > 100)
            {
                ApiModel db = new ApiModel(conStr);
                var list = db.Database.SqlQuery<Web_RjStatus>(sql);
                return Ok(list);
            }
            return Ok(conStr);

        }


        public partial class Web_ErjDocK
        {

            public long SerialNumber { get; set; }

            public long? DocNo { get; set; }

            public string DocDate { get; set; }

            public string Spec { get; set; }

            public string Status { get; set; }

            public string CustCode { get; set; }

            public int? KhdtCode { get; set; }

            public string Eghdam { get; set; }

            public string Tanzim { get; set; }

            public string DocDesc { get; set; }

            public string FinalComm { get; set; }

            public string EghdamComm { get; set; }

            public string MhltDate { get; set; }

            public string AmalDate { get; set; }

            public string EndDate { get; set; }

            public string SpecialComm { get; set; }

            public short? Mahramaneh { get; set; }

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

            public string CustName { get; set; }

            public string KhdtName { get; set; }

            public int HasNewErja { get; set; }

            public string ToUserCode { get; set; }

            public string MahramanehName { get; set; }

            public double? RjTime { get; set; }

            public string RjTimeSt { get; set; }

            public string RelatedDocs { get; set; }

            public byte? SpecialCommTrs { get; set; }

        }

        public class ErjDocKObject
        {
            public string userName { get; set; }

            public string userMode { get; set; }

            public string azTarikh { get; set; }

            public string taTarikh { get; set; }

            public string Status { get; set; }

            public string CustCode { get; set; }

            public string KhdtCode { get; set; }

            public string SrchSt { get; set; } // جستجو برای

            public long SerialNumber { get; set; }

        }


        // Post: api/Web_Data/ErjDocK گزارش فهرست پرونده ها  
        [Route("api/Web_Data/ErjDocK/{ace}/{sal}/{group}")]
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PostWeb_ErjDocK(string ace, string sal, string group, ErjDocKObject ErjDocKObject)
        {
            var dataAccount = UnitDatabase.ReadUserPassHeader(this.Request.Headers);
            string sql = string.Format(CultureInfo.InvariantCulture,
                          @"select top (10000)  * FROM  Web_ErjDocK('{0}','{1}') AS ErjDocK where 1 = 1 and ShowDocTrs = 1 ",
                          ErjDocKObject.SrchSt, dataAccount[2]);

            //if (ErjDocKObject.userMode == "USER")  // بعدا باید درست شود
            //    sql += string.Format(" and Eghdam = '{0}' ", ErjDocKObject.userName);

            if (ErjDocKObject.azTarikh != "")
                sql += string.Format(" and DocDate >= '{0}' ", ErjDocKObject.azTarikh);

            if (ErjDocKObject.taTarikh != "")
                sql += string.Format(" and DocDate <= '{0}' ", ErjDocKObject.taTarikh);


            if (ErjDocKObject.Status != "")
                sql += string.Format(" and Status = '{0}' ", ErjDocKObject.Status);


            sql += UnitPublic.SpiltCodeAnd("CustCode", ErjDocKObject.CustCode);
            sql += UnitPublic.SpiltCodeAnd("KhdtCode", ErjDocKObject.KhdtCode);

            if (ErjDocKObject.SerialNumber > 0)
                sql += " and SerialNumber = " + ErjDocKObject.SerialNumber;

            string conStr = UnitDatabase.CreateConnectionString(dataAccount[0], dataAccount[1], dataAccount[2], ace, sal, group, 0, "", 0, 0);
            if (conStr.Length > 100)
            {
                ApiModel db = new ApiModel(conStr);
                var list = db.Database.SqlQuery<Web_ErjDocK>(sql);
                return Ok(list);
            }
            return Ok(conStr);
        }



        public class ErjUsersObject
        {
            public string userCode { get; set; }

            public long SerialNumber { get; set; }
        }


        public partial class Web_ErjUsers
        {
            public string Code { get; set; }

            public string Name { get; set; }

            public string Spec { get; set; }
        }


        // Post: api/Web_Data/Web_ErjUsers   ارجاع شونده/ارجاع دهنده
        [Route("api/Web_Data/Web_ErjUsers/{ace}/{sal}/{group}")]
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PostWeb_ErjUsers(string ace, string sal, string group, ErjUsersObject ErjUsersObject)
        {
            string sql = string.Format(@"Select * from Web_ErjUsers('{0}',{1}) order by SrchOrder Desc,Name Asc", ErjUsersObject.userCode, ErjUsersObject.SerialNumber);
            var dataAccount = UnitDatabase.ReadUserPassHeader(this.Request.Headers);
            string conStr = UnitDatabase.CreateConnectionString(dataAccount[0], dataAccount[1], dataAccount[2], ace, sal, group, 0, "", 0, 0);
            if (conStr.Length > 100)
            {
                ApiModel db = new ApiModel(conStr);
                var list = db.Database.SqlQuery<Web_ErjUsers>(sql);
                return Ok(list);
            }
            return Ok(conStr);
        }


        public partial class Web_RepToUsers
        {
            public int Tag { get; set; }

            public string Code { get; set; }

            public string Name { get; set; }
        }

        // GET: api/Web_Data/Web_RepToUsers   ارجاع شونده
        [Route("api/Web_Data/Web_RepToUsers/{ace}/{sal}/{group}/{UserCode}")]
        public async Task<IHttpActionResult> GetWeb_RepToUsers(string ace, string sal, string group, string userCode)
        {
            string sql = string.Format(@"Select * from Web_RepToUsers('{0}') order by code", userCode);
            var dataAccount = UnitDatabase.ReadUserPassHeader(this.Request.Headers);
            string conStr = UnitDatabase.CreateConnectionString(dataAccount[0], dataAccount[1], dataAccount[2], ace, sal, group, 0, "", 0, 0);
            if (conStr.Length > 100)
            {
                ApiModel db = new ApiModel(conStr);
                var list = db.Database.SqlQuery<Web_RepToUsers>(sql);
                return Ok(list);
            }
            return Ok(conStr);
        }


        public partial class Web_RepFromUsers
        {

            public string Code { get; set; }

            public string Name { get; set; }
        }

        // GET: api/Web_Data/Web_RepFromUsers   ارجاع دهنده
        [Route("api/Web_Data/Web_RepFromUsers/{ace}/{sal}/{group}/{UserCode}")]
        public async Task<IHttpActionResult> GetWeb_RepFromUsers(string ace, string sal, string group, string userCode)
        {
            string sql = string.Format(@"Select * from Web_RepFromUsers('{0}') order by code", userCode);
            var dataAccount = UnitDatabase.ReadUserPassHeader(this.Request.Headers);
            string conStr = UnitDatabase.CreateConnectionString(dataAccount[0], dataAccount[1], dataAccount[2], ace, sal, group, 0, "", 0, 0);
            if (conStr.Length > 100)
            {
                ApiModel db = new ApiModel(conStr);
                var list = db.Database.SqlQuery<Web_RepFromUsers>(sql);
                return Ok(list);
            }
            return Ok(conStr);
        }



        public partial class Web_Mahramaneh
        {
            public int Code { get; set; }

            public string Name { get; set; }
        }

        // GET: api/Web_Data/Web_Mahramaneh   محرمانه یا نوع در اتوماسیون
        [Route("api/Web_Data/Web_Mahramaneh/{ace}/{sal}/{group}")]
        public async Task<IHttpActionResult> GetWeb_Mahramaneh(string ace, string sal, string group)
        {
            var dataAccount = UnitDatabase.ReadUserPassHeader(this.Request.Headers);
            string sql = string.Format(@"Select * from Web_Mahramaneh('{0}')", dataAccount[2]);
            string conStr = UnitDatabase.CreateConnectionString(dataAccount[0], dataAccount[1], dataAccount[2], ace, sal, group, 0, "", 0, 0);
            if (conStr.Length > 100)
            {
                ApiModel db = new ApiModel(conStr);
                var list = db.Database.SqlQuery<Web_Mahramaneh>(sql);
                return Ok(list);
            }
            return Ok(conStr);
        }




        public partial class Web_RooneveshtUsersList_Object
        {
            public long SerialNumber { get; set; }

            public int BandNo { get; set; }
        }

        public partial class Web_RooneveshtUsersList
        {
            public string ToUserCode { get; set; }

            public string ToUserName { get; set; }
        }

        // Post: api/Web_Data/Web_RooneveshtUsersList  لیست افرادی که رونوشت دارند
        [Route("api/Web_Data/Web_RooneveshtUsersList/{ace}/{sal}/{group}")]
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PostWeb_RooneveshtUsersList(string ace, string sal, string group, Web_RooneveshtUsersList_Object Web_RooneveshtUsersList_Object)
        {
            string sql = string.Format(@"Select * from Web_ErjRooneveshtUsersList({0},{1})",
                                                Web_RooneveshtUsersList_Object.SerialNumber,
                                                Web_RooneveshtUsersList_Object.BandNo);

            var dataAccount = UnitDatabase.ReadUserPassHeader(this.Request.Headers);
            string conStr = UnitDatabase.CreateConnectionString(dataAccount[0], dataAccount[1], dataAccount[2], ace, sal, group, 0, "", 0, 0);
            if (conStr.Length > 100)
            {
                ApiModel db = new ApiModel(conStr);
                var list = db.Database.SqlQuery<Web_RooneveshtUsersList>(sql);
                return Ok(list);
            }
            return Ok(conStr);
        }





        public class ErjDocHObject
        {
            public byte Mode { get; set; }

            public string UserCode { get; set; }

            public int select { get; set; }

            public bool accessSanad { get; set; }

            public string Sal { get; set; }

            public string Status { get; set; }

            public string DocNo { get; set; }

            public string Sort { get; set; }

            public string ModeSort { get; set; }

        }


        // Post: api/Web_Data/ErjDocH  فهرست پرونده ها  
        [Route("api/Web_Data/ErjDocH/{ace}/{sal}/{group}")]
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PostWeb_ErjDocH_F(string ace, string sal, string group, ErjDocHObject ErjDocHObject)
        {
            var dataAccount = UnitDatabase.ReadUserPassHeader(this.Request.Headers);

            string sql = string.Format(CultureInfo.InvariantCulture,
                            @"declare @Sal nvarchar(10) = '{0}'
                               declare @Status nvarchar(30) = N'{1}'
                               declare @DocNo nvarchar(50) = '{2}' ",
                             ErjDocHObject.Sal,
                             ErjDocHObject.Status,
                             ErjDocHObject.DocNo);

            sql += "select ";
            if (ErjDocHObject.select == 0)
                sql += " top(100) ";

            sql += string.Format(CultureInfo.InvariantCulture,
                        @" * FROM  Web_ErjDocH_F({0},'{1}') AS ErjDocH where 
                              (@sal = ''  or substring(docdate, 1, 4) = @Sal) and
                              (@Status = ''  or Status = @Status) and
                              (@DocNo = ''  or DocNo = @DocNo) ",
                          ErjDocHObject.Mode, dataAccount[2]);
            if (ErjDocHObject.accessSanad == false)
                sql += " and Eghdam = '" + ErjDocHObject.UserCode + "' ";

            sql += " order by ";

            if (ErjDocHObject.Sort == "" || ErjDocHObject.Sort == null)
            {
                ErjDocHObject.Sort = "DocDate Desc,DocNo Desc";
            }
            else if (ErjDocHObject.Sort == "DocDate")
            {
                if (ErjDocHObject.ModeSort == "ASC")
                    ErjDocHObject.Sort = "DocDate Asc,DocNo Asc";
                else
                    ErjDocHObject.Sort = "DocDate Desc,DocNo Desc";
            }
            else if (ErjDocHObject.Sort == "Status")
            {
                if (ErjDocHObject.ModeSort == "ASC")
                    ErjDocHObject.Sort = "Status Asc, DocDate Asc,DocNo Asc";
                else
                    ErjDocHObject.Sort = "Status Desc, DocDate Desc,DocNo Desc";
            }
            else
            {
                ErjDocHObject.Sort = ErjDocHObject.Sort + " " + ErjDocHObject.ModeSort;
            }

            sql += ErjDocHObject.Sort;

            string conStr = UnitDatabase.CreateConnectionString(dataAccount[0], dataAccount[1], dataAccount[2], ace, sal, group, 0, "", 0, 0);
            if (conStr.Length > 100)
            {
                ApiModel db = new ApiModel(conStr);
                var list = db.Database.SqlQuery<Web_ErjDocH>(sql);
                return Ok(list);
            }
            return Ok(conStr);
        }


        public class DocAttachObject
        {
            public int ModeCode { get; set; }

            public long SerialNumber { get; set; }
        }


        // Post: api/Web_Data/DocAttach پیوست  
        [Route("api/Web_Data/DocAttach/{ace}/{sal}/{group}")]
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PostWeb_DocAttach(string ace, string sal, string group, DocAttachObject DocAttachObject)
        {
            string sql = string.Format(CultureInfo.InvariantCulture,
                            @"select  SerialNumber,Comm,FName,BandNo FROM Web_DocAttach
                                             where   ModeCode = {0} and ProgName='{1}' and SerialNumber = {2} order by BandNo desc",
                              DocAttachObject.ModeCode,
                              "ERJ1",
                              DocAttachObject.SerialNumber);

            var dataAccount = UnitDatabase.ReadUserPassHeader(this.Request.Headers);
            string conStr = UnitDatabase.CreateConnectionString(dataAccount[0], dataAccount[1], dataAccount[2], ace, sal, group, 0, "", 0, 0);
            if (conStr.Length > 100)
            {
                ApiModel db = new ApiModel(conStr);
                var list = db.Database.SqlQuery<Web_DocAttach>(sql);
                return Ok(list);
            }
            return Ok(conStr);
        }


        public class DownloadAttachObject
        {
            public long SerialNumber { get; set; }
            public int BandNo { get; set; }
        }

        public class DownloadAttach
        {
            public byte[] Atch { get; set; }
        }



        // Post: api/Web_Data/DownloadAttach   دانلود پیوست  
        [Route("api/Web_Data/DownloadAttach/{ace}/{sal}/{group}")]
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PostWeb_DownloadAttach(string ace, string sal, string group, DownloadAttachObject DownloadAttachObject)
        {
            string sql = string.Format(CultureInfo.InvariantCulture,
             @"select Atch FROM Web_DocAttach where SerialNumber = {0} and BandNo = {1}",
             DownloadAttachObject.SerialNumber,
             DownloadAttachObject.BandNo);

            var dataAccount = UnitDatabase.ReadUserPassHeader(this.Request.Headers);
            string conStr = UnitDatabase.CreateConnectionString(dataAccount[0], dataAccount[1], dataAccount[2], ace, sal, group, 0, "", 0, 0);
            if (conStr.Length > 100)
            {
                ApiModel db = new ApiModel(conStr);
                var list = db.Database.SqlQuery<DownloadAttach>(sql);
                return Ok(list);
            }
            return Ok(conStr);
        }



        public class ErjResultObject
        {
            public string SerialNumber { get; set; }

            public string DocBMode { get; set; }

            public string ToUserCode { get; set; }

            public int? BandNo { get; set; }
        }


        public partial class Web_ErjResult
        {
            public int DocBMode { get; set; }

            public string ToUserCode { get; set; }

            public string ToUserName { get; set; }

            public long SerialNumber { get; set; }

            public int? BandNo { get; set; }

            public string RjResult { get; set; }
        }

        // Post: api/Web_Data/Web_ErjResult   نتیجه در اتوماسیون
        [Route("api/Web_Data/Web_ErjResult/{ace}/{sal}/{group}")]
        public async Task<IHttpActionResult> PostWeb_ErjResult(string ace, string sal, string group, ErjResultObject ErjResultObject)
        {
            string sql = string.Format(@"Select * from Web_ErjResult where SerialNumber = {0}", ErjResultObject.SerialNumber);

            if (ErjResultObject.BandNo != null)
            {

                sql += string.Format(@" and  BandNo = {0} ", ErjResultObject.BandNo);
            }

            if (ErjResultObject.DocBMode != null)
                sql += string.Format(@" and  DocBMode = {0} and ToUserCode = '{1}'",
                     ErjResultObject.DocBMode,
                     ErjResultObject.DocBMode == "0" ? "" : ErjResultObject.ToUserCode
                    );

            var dataAccount = UnitDatabase.ReadUserPassHeader(this.Request.Headers);
            string conStr = UnitDatabase.CreateConnectionString(dataAccount[0], dataAccount[1], dataAccount[2], ace, sal, group, 0, "", 0, 0);
            if (conStr.Length > 100)
            {
                ApiModel db = new ApiModel(conStr);
                var list = db.Database.SqlQuery<Web_ErjResult>(sql);
                return Ok(list);
            }
            return Ok(conStr);
        }



        public partial class Web_ErjDocB_Last
        {
            public long? DocNo { get; set; }

            public string DocDate { get; set; }

            public string CustName { get; set; }

            public string KhdtName { get; set; }

            public string Spec { get; set; }

            public string Status { get; set; }

            public long SerialNumber { get; set; }

            public string MhltDate { get; set; }

            public int? DocBStep { get; set; }

            public string RjRadif { get; set; }

            public int? BandNo { get; set; }

            public int? DocBMode { get; set; }

            public string RjDate { get; set; }

            public string RjStatus { get; set; }

            public long? SortRjStatus { get; set; }

            public string RjEndDate { get; set; }

            public string RjMhltDate { get; set; }

            public DateTime? RjUpdateDate { get; set; }

            public string RjUpdateUser { get; set; }

            public int? ErjaCount { get; set; }

            public double? RjTime { get; set; }

            public string RjTimeSt { get; set; }

            public string FromUserCode { get; set; }

            public string FromUserName { get; set; }

            public string ToUserCode { get; set; }

            public string ToUserName { get; set; }

            public string RjReadSt { get; set; }

            public int? FarayandCode { get; set; }

            public string FarayandName { get; set; }

            public byte? FinalCommTrs { get; set; }

            public byte DocAttachExists { get; set; }

            public long? SortRjDate { get; set; }

        }


        public class ErjDocB_Last
        {
            public string erjaMode { get; set; }

            public string docBMode { get; set; }

            public string fromUserCode { get; set; }

            public string toUserCode { get; set; }

            public string srchSt { get; set; }

            public string azDocDate { get; set; }

            public string taDocDate { get; set; }

            public string azRjDate { get; set; }

            public string taRjDate { get; set; }

            public string azMhltDate { get; set; }

            public string taMhltDate { get; set; }

            public string status { get; set; }

            public string custCode { get; set; }

            public string khdtCode { get; set; }
        }

        // Post: api/Web_Data/Web_ErjDocB_Last گزارش فهرست ارجاعات  
        [Route("api/Web_Data/Web_ErjDocB_Last/{ace}/{sal}/{group}")]
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PostWeb_ErjDocB_Last(string ace, string sal, string group, ErjDocB_Last ErjDocB_Last)
        {
            var dataAccount = UnitDatabase.ReadUserPassHeader(this.Request.Headers);
            string sql = string.Format(CultureInfo.InvariantCulture,
                        @"select  top (10000) * FROM  Web_ErjDocB_Last({0}, {1},'{2}','{3}','{4}','{5}') AS ErjDocB_Last where 1 = 1 "
                        , ErjDocB_Last.erjaMode
                        , ErjDocB_Last.docBMode
                        , ErjDocB_Last.fromUserCode
                        , ErjDocB_Last.toUserCode
                        , ErjDocB_Last.srchSt
                        , dataAccount[2]);

            if (ErjDocB_Last.azDocDate != "")
                sql += string.Format(" and DocDate >= '{0}' ", ErjDocB_Last.azDocDate);

            if (ErjDocB_Last.taDocDate != "")
                sql += string.Format(" and DocDate <= '{0}' ", ErjDocB_Last.taDocDate);

            if (ErjDocB_Last.azRjDate != "")
                sql += string.Format(" and RjDate >= '{0}' ", ErjDocB_Last.azRjDate);

            if (ErjDocB_Last.taRjDate != "")
                sql += string.Format(" and RjDate <= '{0}' ", ErjDocB_Last.taRjDate);

            if (ErjDocB_Last.azMhltDate != "")
                sql += string.Format(" and MhltDate >= '{0}' ", ErjDocB_Last.azMhltDate);

            if (ErjDocB_Last.taMhltDate != "")
                sql += string.Format(" and MhltDate <= '{0}' ", ErjDocB_Last.taMhltDate);

            if (ErjDocB_Last.status != "")
                sql += string.Format(" and Status = '{0}' ", ErjDocB_Last.status);


            sql += UnitPublic.SpiltCodeAnd("KhdtCode", ErjDocB_Last.khdtCode);
            sql += UnitPublic.SpiltCodeAnd("CustCode", ErjDocB_Last.custCode);

            if (ErjDocB_Last.erjaMode == "1")
                sql += "order by SortRjStatus";
            else
                sql += "order by SortRjDate";

            string conStr = UnitDatabase.CreateConnectionString(dataAccount[0], dataAccount[1], dataAccount[2], ace, sal, group, 0, "", 0, 0);
            if (conStr.Length > 100)
            {
                ApiModel db = new ApiModel(conStr);
                var list = db.Database.SqlQuery<Web_ErjDocB_Last>(sql);
                return Ok(list);
            }
            return Ok(conStr);
        }






        [Route("api/Web_Data/Web_CountErjDocB_Last/{ace}/{sal}/{group}")]
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PostWeb_CountErjDocB_Last(string ace, string sal, string group, ErjDocB_Last ErjDocB_Last)
        {
            var dataAccount = UnitDatabase.ReadUserPassHeader(this.Request.Headers);
            string sql = string.Format(CultureInfo.InvariantCulture,
                          @"select  count(RjReadSt) FROM  Web_ErjDocB_Last({0}, {1},'{2}','{3}','{4}','{5}') AS ErjDocB_Last where RjReadSt = 'T' and Status = '{6}' "
                          , ErjDocB_Last.erjaMode
                          , ErjDocB_Last.docBMode
                          , ErjDocB_Last.fromUserCode
                          , ErjDocB_Last.toUserCode
                          , ErjDocB_Last.srchSt
                          , dataAccount[2],
                          "فعال");

            string conStr = UnitDatabase.CreateConnectionString(dataAccount[0], dataAccount[1], dataAccount[2], ace, sal, group, 0, "", 0, 0);
            if (conStr.Length > 100)
            {
                ApiModel db = new ApiModel(conStr);
                var list = db.Database.SqlQuery<int>(sql);
                return Ok(list);
            }
            return Ok(conStr);
        }






        public class Web_ErjDocErja
        {
            public long SerialNumber { get; set; }

            public int? BandNo { get; set; }

            public int? DocBMode { get; set; }

            public string RjComm { get; set; }

            public string RjDate { get; set; }

            public string RjStatus { get; set; }

            public string RjTimeSt { get; set; }

            public string FromUserCode { get; set; }

            public string FromUserName { get; set; }

            public string ToUserCode { get; set; }

            public string ToUserName { get; set; }

            public string RjReadSt { get; set; }

            public string RooneveshtUsers { get; set; }

            public string FarayandName { get; set; }
        }

        public class ErjDocErja
        {
            public long SerialNumber { get; set; }
        }

        // Post: api/Web_Data/Web_ErjDocErja گزارش ریز ارجاعات  
        [Route("api/Web_Data/Web_ErjDocErja/{ace}/{sal}/{group}")]
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PostWeb_ErjDocErja(string ace, string sal, string group, ErjDocErja ErjDocErja)
        {
            string sql = string.Format(CultureInfo.InvariantCulture,
                         @"select top (10000)  * FROM  Web_ErjDocErja({0}) AS ErjDocErja where 1 = 1 order by BandNo,DocBMode "
                         , ErjDocErja.SerialNumber);

            var dataAccount = UnitDatabase.ReadUserPassHeader(this.Request.Headers);
            string conStr = UnitDatabase.CreateConnectionString(dataAccount[0], dataAccount[1], dataAccount[2], ace, sal, group, 0, "", 0, 0);
            if (conStr.Length > 100)
            {
                ApiModel db = new ApiModel(conStr);
                var list = db.Database.SqlQuery<Web_ErjDocErja>(sql);
                return Ok(list);
            }
            return Ok(conStr);
        }



        public class Web_ErjSaveDoc_BSave
        {
            public long SerialNumber { get; set; }

            public string Natijeh { get; set; }

            public string FromUserCode { get; set; }

            public string ToUserCode { get; set; }

            public string RjDate { get; set; }

            public string RjTime { get; set; }

            public string RjMhltDate { get; set; }

            public int BandNo { get; set; }

            public int SrMode { get; set; }

            public string RjStatus { get; set; }

            public int? FarayandCode { get; set; }
        }


        // POST: api/Web_Data/ErjSaveDoc_BSave
        [Route("api/Web_Data/ErjSaveDoc_BSave/{ace}/{sal}/{group}")]
        public async Task<IHttpActionResult> PostErjSaveDoc_BSave(string ace, string sal, string group, Web_ErjSaveDoc_BSave Web_ErjSaveDoc_BSave)
        {
            string sql = string.Format(
                        @" DECLARE	@return_value int,
		                            @BandNo nvarchar(10)
                            EXEC	@return_value = [dbo].[Web_ErjSaveDoc_BSave]
		                            @SerialNumber = {0},
		                            @BandNo = {1} ,
		                            @Natijeh = N'{2}',
		                            @FromUserCode = N'{3}',
		                            @ToUserCode = N'{4}',
		                            @RjDate = N'{5}',
		                            @RjTime = {6},
		                            @RjMhltDate = N'{7}',
                                    @SrMode = {8},
                                    @RjStatus = '{9}',
                                    @FarayandCode = {10}
                            SELECT	@BandNo as N'@BandNo' ",
                        Web_ErjSaveDoc_BSave.SerialNumber,
                        Web_ErjSaveDoc_BSave.BandNo,
                        UnitPublic.ConvertTextWebToWin(Web_ErjSaveDoc_BSave.Natijeh ?? ""),
                        Web_ErjSaveDoc_BSave.FromUserCode,
                        Web_ErjSaveDoc_BSave.ToUserCode,
                        Web_ErjSaveDoc_BSave.RjDate,
                        Web_ErjSaveDoc_BSave.RjTime,
                        Web_ErjSaveDoc_BSave.RjMhltDate,
                        Web_ErjSaveDoc_BSave.SrMode,
                        Web_ErjSaveDoc_BSave.RjStatus,
                        Web_ErjSaveDoc_BSave.FarayandCode ?? 0
                        );

            var dataAccount = UnitDatabase.ReadUserPassHeader(this.Request.Headers);
            string conStr = UnitDatabase.CreateConnectionString(dataAccount[0], dataAccount[1], dataAccount[2], ace, sal, group, Web_ErjSaveDoc_BSave.SerialNumber, "", 0, 0);
            if (conStr.Length > 100)
            {
                ApiModel db = new ApiModel(conStr);
                string list = db.Database.SqlQuery<string>(sql).Single();
                if (!string.IsNullOrEmpty(list))
                {
                    await db.SaveChangesAsync();
                }
                return Ok(list);
            }
            return Ok(conStr);
        }



        public class Web_ErjSaveDoc_CSave
        {
            public long SerialNumber { get; set; }

            public int BandNo { get; set; }

            public string Natijeh { get; set; }

            public string ToUserCode { get; set; }

            public string RjDate { get; set; }

            public string RjTime { get; set; }

        }

        // POST: api/Web_Data/ErjSaveDoc_CSave
        [Route("api/Web_Data/ErjSaveDoc_CSave/{ace}/{sal}/{group}")]
        public async Task<IHttpActionResult> PostErjSaveDoc_CSave(string ace, string sal, string group, [FromBody]List<Web_ErjSaveDoc_CSave> Web_ErjSaveDoc_CSave)
        {
            string value = "";
            string sql = "";

            var dataAccount = UnitDatabase.ReadUserPassHeader(this.Request.Headers);
            string conStr = UnitDatabase.CreateConnectionString(dataAccount[0], dataAccount[1], dataAccount[2], ace, sal, group, 0, "", 0, 0);
            if (conStr.Length > 100)
            {
                ApiModel db = new ApiModel(conStr);

                foreach (var item in Web_ErjSaveDoc_CSave)
                {
                    sql = string.Format(CultureInfo.InvariantCulture,
                         @" DECLARE	@return_value int,
                                        @BandNo nvarchar(10)
                               EXEC	@return_value = [dbo].[Web_ErjSaveDoc_CSave]
		                            @SerialNumber = {0},
		                            @BandNo = {1},
		                            @Natijeh = N'{2}',
		                            @ToUserCode = N'{3}',
		                            @RjDate = N'{4}',
                                    @RjTime = {5}
                               SELECT	@BandNo as N'@BandNo'",

                        item.SerialNumber,
                        item.BandNo,
                        UnitPublic.ConvertTextWebToWin(item.Natijeh ?? ""),
                        item.ToUserCode,
                        item.RjDate,
                        item.RjTime);
                    value = db.Database.SqlQuery<string>(sql).Single();
                }

                await db.SaveChangesAsync();
                if (!string.IsNullOrEmpty(value))
                {
                    await db.SaveChangesAsync();
                }
                return Ok(value);
            }
            return Ok(conStr);
        }



        public class Web_ErjSaveDoc_Rooneveshts
        {
            public long SerialNumber { get; set; }

            //public string FromUserCode { get; set; }

            public string ToUserCodes { get; set; }

            public int BandNo { get; set; }

            // public string RjDate { get; set; }

        }

        // POST: api/Web_Data/ErjSaveDoc_Rooneveshts
        [Route("api/Web_Data/ErjSaveDoc_Rooneveshts/{ace}/{sal}/{group}")]
        public async Task<IHttpActionResult> PostErjSaveDoc_Rooneveshts(string ace, string sal, string group, Web_ErjSaveDoc_Rooneveshts Web_ErjSaveDoc_Rooneveshts)
        {
            string sql = string.Format(CultureInfo.InvariantCulture,
                        @" DECLARE	@return_value int
                               EXEC	@return_value = [dbo].[Web_ErjSaveDoc_Rooneveshts]
		                            @SerialNumber = {0},
                                    @ToUserCodes = N'{1}',
                                    @BandNo = {2}
                           SELECT	'Return Value' = @return_value",
                       Web_ErjSaveDoc_Rooneveshts.SerialNumber,
                       Web_ErjSaveDoc_Rooneveshts.ToUserCodes,
                       Web_ErjSaveDoc_Rooneveshts.BandNo);

            var dataAccount = UnitDatabase.ReadUserPassHeader(this.Request.Headers);
            string conStr = UnitDatabase.CreateConnectionString(dataAccount[0], dataAccount[1], dataAccount[2], ace, sal, group, 0, "", 0, 0);
            if (conStr.Length > 100)
            {
                ApiModel db = new ApiModel(conStr);
                var list = db.Database.SqlQuery<int>(sql).Single();
                await db.SaveChangesAsync();
                return Ok(list);
            }
            return Ok(conStr);
        }



        public class Web_ErjSaveDoc_CD
        {
            public long SerialNumber { get; set; }

            public int BandNo { get; set; }
        }

        // POST: api/Web_Data/ErjSaveDoc_CD
        [Route("api/Web_Data/ErjSaveDoc_CD/{ace}/{sal}/{group}")]
        public async Task<IHttpActionResult> PostErjSaveDoc_CD(string ace, string sal, string group, Web_ErjSaveDoc_CD Web_ErjSaveDoc_CD)
        {
            string sql = string.Format(CultureInfo.InvariantCulture,
                 @" DECLARE	@return_value int,
                                        @BandNo nvarchar(10)
                               EXEC	@return_value = [dbo].[Web_ErjSaveDoc_CD]
		                            @SerialNumber = {0},
		                            @BandNo = {1}
                               SELECT	@BandNo as N'@BandNo'",
                Web_ErjSaveDoc_CD.SerialNumber,
                Web_ErjSaveDoc_CD.BandNo);

            var dataAccount = UnitDatabase.ReadUserPassHeader(this.Request.Headers);
            string conStr = UnitDatabase.CreateConnectionString(dataAccount[0], dataAccount[1], dataAccount[2], ace, sal, group, 0, "", 0, 0);
            if (conStr.Length > 100)
            {
                ApiModel db = new ApiModel(conStr);
                var list = db.Database.SqlQuery<string>(sql).Single();
                await db.SaveChangesAsync();
                return Ok(list);
            }
            return Ok(conStr);
        }



        public class Web_ErjSaveDoc_RjRead
        {
            public int DocBMode { get; set; }

            public long SerialNumber { get; set; }

            public int BandNo { get; set; }

            public string RjReadSt { get; set; }
        }

        // POST: api/Web_Data/ErjSaveDoc_RjRead
        [Route("api/Web_Data/ErjSaveDoc_RjRead/{ace}/{sal}/{group}")]
        public async Task<IHttpActionResult> PostErjSaveDoc_RjRead(string ace, string sal, string group, Web_ErjSaveDoc_RjRead Web_ErjSaveDoc_RjRead)
        {
            string sql = string.Format(CultureInfo.InvariantCulture,
                         @" DECLARE	@return_value int
                            EXEC	@return_value = [dbo].[Web_ErjSaveDoc_RjRead]
                                    @DocBMode = {0},		                            
                                    @SerialNumber = {1},
		                            @BandNo = {2},
		                            @RjReadSt = '{3}'
                            SELECT	'Return Value' = @return_value",
                        Web_ErjSaveDoc_RjRead.DocBMode,
                        Web_ErjSaveDoc_RjRead.SerialNumber,
                        Web_ErjSaveDoc_RjRead.BandNo,
                        Web_ErjSaveDoc_RjRead.RjReadSt
                        );

            var dataAccount = UnitDatabase.ReadUserPassHeader(this.Request.Headers);
            string conStr = UnitDatabase.CreateConnectionString(dataAccount[0], dataAccount[1], dataAccount[2], ace, sal, group, 0, "", 0, 0);
            if (conStr.Length > 100)
            {
                ApiModel db = new ApiModel(conStr);
                var list = db.Database.SqlQuery<int>(sql).Single();
                await db.SaveChangesAsync();
                return Ok(list);
            }
            return Ok(conStr);
        }




        public class Web_ErjSaveDoc_HStatus
        {
            public long SerialNumber { get; set; }

            public string Status { get; set; }
        }


        // POST: api/Web_Data/ErjSaveDoc_HStatus
        [Route("api/Web_Data/ErjSaveDoc_HStatus/{ace}/{sal}/{group}")]
        public async Task<IHttpActionResult> PostErjSaveDoc_HStatus(string ace, string sal, string group, Web_ErjSaveDoc_HStatus Web_ErjSaveDoc_HStatus)
        {
            string sql = string.Format(
                        @" DECLARE	@return_value int
                            EXEC	@return_value = [dbo].[Web_ErjSaveDoc_HStatus]
		                            @SerialNumber = {0},
		                            @Status = '{1}'
                            SELECT	'Return Value' = @return_value",
                        Web_ErjSaveDoc_HStatus.SerialNumber,
                        Web_ErjSaveDoc_HStatus.Status
                        );

            var dataAccount = UnitDatabase.ReadUserPassHeader(this.Request.Headers);
            string conStr = UnitDatabase.CreateConnectionString(dataAccount[0], dataAccount[1], dataAccount[2], ace, sal, group, Web_ErjSaveDoc_HStatus.SerialNumber, "", 0, 0);
            if (conStr.Length > 100)
            {
                ApiModel db = new ApiModel(conStr);
                var list = db.Database.SqlQuery<int>(sql).Single();
                await db.SaveChangesAsync();
                return Ok(list);
            }
            return Ok(conStr);
        }



        public class Web_DocAttach_Save
        {
            public string ProgName { get; set; }

            public string ModeCode { get; set; }

            public long SerialNumber { get; set; }

            public int BandNo { get; set; }

            public string Code { get; set; }

            public string Comm { get; set; }

            public string FName { get; set; }

            public string Atch { get; set; }
        }



        // POST: api/Web_Data/ErjDocAttach_Save
        [Route("api/Web_Data/ErjDocAttach_Save")]
        public HttpResponseMessage PostErjDocAttach_Save()
        {
            //Create the Directory.
            string path = HttpContext.Current.Server.MapPath("~/Uploads/");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            //Fetch the File.
            HttpPostedFile postedFile = HttpContext.Current.Request.Files[0];

            //Fetch the File Name.
            string fileName = HttpContext.Current.Request.Form["fileName"] + Path.GetExtension(postedFile.FileName);

            //Save the File.
            postedFile.SaveAs(path + fileName);

            //Send OK Response to Client.
            return Request.CreateResponse(HttpStatusCode.OK, fileName);
        }


        public class Web_DocAttach_Del
        {
            public string ProgName { get; set; }

            public string ModeCode { get; set; }

            public long SerialNumber { get; set; }

            public int BandNo { get; set; }

        }

        // POST: api/Web_Data/ErjDocAttach_Del
        [Route("api/Web_Data/ErjDocAttach_Del/{ace}/{sal}/{group}")]
        public async Task<IHttpActionResult> PostErjDocAttach_Del(string ace, string sal, string group, Web_DocAttach_Del Web_DocAttach_Del)
        {
            string sql = string.Format(CultureInfo.InvariantCulture,
                        @" DECLARE	@return_value int
                            
                            EXEC	@return_value = [dbo].[Web_DocAttach_Del]
		                            @ProgName = '{0}',
		                            @ModeCode = '{1}',
		                            @SerialNumber = {2},
		                            @BandNo = {3}

                            SELECT	'Return Value' = @return_value",
                       Web_DocAttach_Del.ProgName,
                       Web_DocAttach_Del.ModeCode,
                       Web_DocAttach_Del.SerialNumber,
                       Web_DocAttach_Del.BandNo
                       );
            var dataAccount = UnitDatabase.ReadUserPassHeader(this.Request.Headers);
            string conStr = UnitDatabase.CreateConnectionString(dataAccount[0], dataAccount[1], dataAccount[2], ace, sal, group, 0, "", 0, 0);
            if (conStr.Length > 100)
            {
                ApiModel db = new ApiModel(conStr);
                var list = db.Database.SqlQuery<int>(sql).Single();
                await db.SaveChangesAsync();
                return Ok(list);
            }
            return Ok(conStr);
        }


        public class KGruObject
        {
            public byte Mode { get; set; }

            public string UserCode { get; set; }
        }

        // Post: api/Web_Data/KGru لیست کالا گروه ها
        [Route("api/Web_Data/KGru/{ace}/{sal}/{group}")]
        public async Task<IHttpActionResult> PostWeb_KGru(string ace, string sal, string group, KGruObject kGruObject)
        {
            string sql = string.Format("select  * FROM  Web_KGru_F({0},'{1}') ", kGruObject.Mode, kGruObject.UserCode);
            var dataAccount = UnitDatabase.ReadUserPassHeader(this.Request.Headers);
            string conStr = UnitDatabase.CreateConnectionString(dataAccount[0], dataAccount[1], dataAccount[2], ace, sal, group, 0, "", 0, 0);
            if (conStr.Length > 100)
            {
                ApiModel db = new ApiModel(conStr);
                var list = db.Database.SqlQuery<Web_KGru>(sql);
                return Ok(list);
            }
            return Ok(conStr);
        }


        // GET: api/Web_Data/Mkz لیست مراکز هزینه
        [Route("api/Web_Data/Mkz/{ace}/{sal}/{group}")]
        public IQueryable<Web_Mkz> GetWeb_Mkz(string ace, string sal, string group)
        {
            var dataAccount = UnitDatabase.ReadUserPassHeader(this.Request.Headers);
            string conStr = UnitDatabase.CreateConnectionString(dataAccount[0], dataAccount[1], dataAccount[2], ace, sal, group, 0, "", 0, 0);
            if (conStr.Length > 100)
            {
                ApiModel db = new ApiModel(conStr);
                var list = db.Web_Mkz.OrderBy(c => c.SortCode);
            }
            return null;
        }

        // GET: api/Web_Data/Opr لیست پروژه ها
        [Route("api/Web_Data/Opr/{ace}/{sal}/{group}")]
        public IQueryable<Web_Opr> GetWeb_Opr(string ace, string sal, string group)
        {
            var dataAccount = UnitDatabase.ReadUserPassHeader(this.Request.Headers);
            string conStr = UnitDatabase.CreateConnectionString(dataAccount[0], dataAccount[1], dataAccount[2], ace, sal, group, 0, "", 0, 0);
            if (conStr.Length > 100)
            {
                ApiModel db = new ApiModel(conStr);
                return db.Web_Opr;
            }
            return null;
        }


        // GET: api/Web_Data/Arz لیست ارز ها
        [Route("api/Web_Data/Arz/{ace}/{sal}/{group}")]
        public IQueryable<Web_Arz> GetWeb_Arz(string ace, string sal, string group)
        {
            var dataAccount = UnitDatabase.ReadUserPassHeader(this.Request.Headers);
            string conStr = UnitDatabase.CreateConnectionString(dataAccount[0], dataAccount[1], dataAccount[2], ace, sal, group, 0, "", 0, 0);
            if (conStr.Length > 100)
            {
                ApiModel db = new ApiModel(conStr);
                return db.Web_Arz;
            }
            return null;
        }

        // GET: api/Web_Data/Web_RprtCols لیست ستونها
        [Route("api/Web_Data/RprtCols/{ace}/{sal}/{group}/{RprtId}/{UserCode}")]
        public async Task<IHttpActionResult> GetWeb_RprtCols(string ace, string sal, string group, string RprtId, string UserCode)
        {
            string sql;
            if (RprtId == "all")
                sql = string.Format(@"select * from Web_RprtCols where (UserCode = '{0}' or UserCode = '*Default*')", UserCode);
            else
                sql = string.Format(@"
                                  if exists (select 1 from Web_RprtCols where RprtId = '{0}' and UserCode = '{1}')
                                     select * from Web_RprtCols where RprtId = '{0}' and UserCode = '{1}'-- and Name<> ''
                                  else
                                     select * from Web_RprtCols where RprtId = '{0}' and UserCode = '*Default*'-- and Name <> ''",
                                  RprtId, UserCode);
            var dataAccount = UnitDatabase.ReadUserPassHeader(this.Request.Headers);
            string conStr = UnitDatabase.CreateConnectionString(dataAccount[0], dataAccount[1], dataAccount[2], ace, sal, group, 0, "", 0, 0);
            if (conStr.Length > 100)
            {
                ApiModel db = new ApiModel(conStr);
                var list = db.Database.SqlQuery<Web_RprtCols>(sql).ToList();
                return Ok(list);
            }
            return Ok(conStr);
        }


        // GET: api/Web_Data/Web_RprtColsDefult لیست ستون های پیش فرض
        [Route("api/Web_Data/RprtColsDefult/{ace}/{sal}/{group}/{RprtId}")]
        public async Task<IHttpActionResult> GetWeb_RprtColsDefult(string ace, string sal, string group, string RprtId)
        {
            string sql = string.Format(@"  select* from Web_RprtCols where RprtId = '{0}' and UserCode = '*Default*' and Name <> ''", RprtId);
            var dataAccount = UnitDatabase.ReadUserPassHeader(this.Request.Headers);
            string conStr = UnitDatabase.CreateConnectionString(dataAccount[0], dataAccount[1], dataAccount[2], ace, sal, group, 0, "", 0, 0);
            if (conStr.Length > 100)
            {
                ApiModel db = new ApiModel(conStr);
                var list = db.Database.SqlQuery<Web_RprtCols>(sql).ToList();
                return Ok(list);
            }
            return Ok(conStr);
        }


        public class RprtColsSave
        {
            public string UserCode { get; set; }

            public string RprtId { get; set; }

            public string Code { get; set; }

            public byte? Visible { get; set; }

            public byte? Position { get; set; }

            public Int16? Width { get; set; }
        }



        // POST: api/RprtColsSave
        [Route("api/Web_Data/RprtColsSave/{ace}/{sal}/{group}")]
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PostRprtColsSave(string ace, string sal, string group, [FromBody]List<RprtColsSave> RprtColsSave)
        {
            var dataAccount = UnitDatabase.ReadUserPassHeader(this.Request.Headers);
            string conStr = UnitDatabase.CreateConnectionString(dataAccount[0], dataAccount[1], dataAccount[2], ace, sal, group, 0, "", 0, 0);
            if (conStr.Length > 100)
            {
                ApiModel db = new ApiModel(conStr);
                string sql;
                foreach (var item in RprtColsSave)
                {
                    sql = string.Format(CultureInfo.InvariantCulture,
                     @" DECLARE	@return_value int
                                                EXEC	@return_value = [dbo].[Web_RprtColsSave]
		                                                @UserCode = '{0}',
		                                                @RprtId = '{1}',
		                                                @Code = '{2}',
		                                                @Visible = {3},
		                                                @Position = {4},
		                                                @Width = {5}
                                                SELECT	'Return Value' = @return_value ",

                    item.UserCode,
                    item.RprtId,
                    item.Code,
                    item.Visible ?? 0,
                    item.Position ?? 0,
                    item.Width ?? 100
                    );
                    var list = db.Database.SqlQuery<int>(sql).Single();
                }
                await db.SaveChangesAsync();

                return Ok("Ok");
            }
            return Ok(conStr);
        }






        // GET: api/Web_Data/ExtraFields لیست مشخصات اضافی
        [Route("api/Web_Data/ExtraFields/{ace}/{sal}/{group}/{modeCode}")]
        public IQueryable<Web_ExtraFields> GetWeb_ExtraFields(string ace, string sal, string group, string modeCode)
        {
            var dataAccount = UnitDatabase.ReadUserPassHeader(this.Request.Headers);
            string conStr = UnitDatabase.CreateConnectionString(dataAccount[0], dataAccount[1], dataAccount[2], ace, sal, group, 0, "", 0, 0);
            if (conStr.Length > 100)
            {
                ApiModel db = new ApiModel(conStr);
                return db.Web_ExtraFields.Where(c => c.ModeCode == modeCode).OrderBy(c => c.BandNo);
            }
            return null;
        }

        //-------------------------------------------------------------------------------------------------------------------------------

        // GET: api/Web_Data/CountTable تعداد رکورد ها    
        [Route("api/Web_Data/CountTable/{ace}/{sal}/{group}/{tableName}/{modeCode}/{inOut}")]
        public async Task<IHttpActionResult> GetWeb_CountTable(string ace, string sal, string group, string tableName, string modeCode, string inOut)
        {
            string sql = string.Format(@"SELECT count(SerialNumber) FROM Web_{0}", tableName);
            if (modeCode != "null" && inOut == "null")
                sql += string.Format(@" WHERE ModeCode = '{0}'", modeCode);
            else if (modeCode == "null" && inOut != "null")
                sql += string.Format(@" WHERE InOut = '{0}'", inOut);

            var dataAccount = UnitDatabase.ReadUserPassHeader(this.Request.Headers);
            string conStr = UnitDatabase.CreateConnectionString(dataAccount[0], dataAccount[1], dataAccount[2], ace, sal, group, 0, "", 0, 0);
            if (conStr.Length > 100)
            {
                ApiModel db = new ApiModel(conStr);
                var list = db.Database.SqlQuery<int>(sql);
                return Ok(list);
            }
            return Ok(conStr);
        }




        public class AFI_SaveCust
        {
            public byte? BranchCode { get; set; }

            public string UserCode { get; set; }

            public string Code { get; set; }

            public string Name { get; set; }

            public string Spec { get; set; }

            public string MelliCode { get; set; }

            public string EcoCode { get; set; }

            public string Ostan { get; set; }

            public string Shahrestan { get; set; }

            public string Region { get; set; }

            public string City { get; set; }

            public string Street { get; set; }

            public string Alley { get; set; }

            public string Plack { get; set; }

            public string ZipCode { get; set; }

            public string Tel { get; set; }

            public string Mobile { get; set; }

            public string Fax { get; set; }

            public string Email { get; set; }

            public string CGruCode { get; set; }

            public double? EtebarNaghd { get; set; }

            public double? EtebarCheck { get; set; }

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

        // post: api/AFI_SaveCust
        [Route("api/Web_Data/AFI_SaveCust/{ace}/{sal}/{group}")]
        public async Task<IHttpActionResult> PostAFI_SaveCust(string ace, string sal, string group, AFI_SaveCust aFI_SaveCust)
        {
            string sql = string.Format(CultureInfo.InvariantCulture,
                     @" DECLARE	@return_value int,
		                        @oCode nvarchar(50)

                        EXEC	@return_value = [dbo].[Web_SaveCust]
		                        @BranchCode = {0},
		                        @UserCode = '{1}',
		                        @Code = '{2}',
		                        @Name = N'{3}',
		                        @Spec = N'{4}',
		                        @MelliCode = N'{5}',
		                        @EcoCode = N'{6}',
                                @Ostan = N'{7}',
                                @Shahrestan = N'{8}',
		                        @Region = N'{9}',
		                        @City = N'{10}',
		                        @Street = N'{11}',
		                        @Alley = N'{12}',
		                        @Plack = N'{13}',
		                        @ZipCode = N'{14}',
		                        @Tel = N'{15}',
		                        @Mobile = N'{16}',
		                        @Fax = N'{17}',
		                        @CGruCode = N'{18}',
		                        @EtebarNaghd = {19},
		                        @EtebarCheck = {20},
		                        @F01 = N'{21}',
		                        @F02 = N'{22}',
		                        @F03 = N'{23}',
		                        @F04 = N'{24}',
		                        @F05 = N'{25}',
		                        @F06 = N'{26}',
		                        @F07 = N'{27}',
		                        @F08 = N'{28}',
		                        @F09 = N'{29}',
		                        @F10 = N'{30}',
		                        @F11 = N'{31}',
		                        @F12 = N'{32}',
		                        @F13 = N'{33}',
		                        @F14 = N'{34}',
		                        @F15 = N'{35}',
		                        @F16 = N'{36}',
		                        @F17 = N'{37}',
		                        @F18 = N'{38}',
		                        @F19 = N'{39}',
		                        @F20 = N'{40}',
		                        @Email = N'{41}',
		                        @oCode = @oCode OUTPUT

                        SELECT	@oCode as N'@oCode'",
                        aFI_SaveCust.BranchCode ?? 0,
                        aFI_SaveCust.UserCode,
                        aFI_SaveCust.Code,
                        aFI_SaveCust.Name ?? "",
                        aFI_SaveCust.Spec ?? "",
                        aFI_SaveCust.MelliCode ?? "",
                        aFI_SaveCust.EcoCode ?? "",
                        aFI_SaveCust.Ostan ?? "",
                        aFI_SaveCust.Shahrestan ?? "",
                        aFI_SaveCust.Region ?? "",
                        aFI_SaveCust.City ?? "",
                        aFI_SaveCust.Street ?? "",
                        aFI_SaveCust.Alley ?? "",
                        aFI_SaveCust.Plack ?? "",
                        aFI_SaveCust.ZipCode ?? "",
                        aFI_SaveCust.Tel ?? "",
                        aFI_SaveCust.Mobile ?? "",
                        aFI_SaveCust.Fax ?? "",
                        aFI_SaveCust.CGruCode ?? "",
                        aFI_SaveCust.EtebarNaghd ?? 0,
                        aFI_SaveCust.EtebarCheck ?? 0,
                        aFI_SaveCust.F01,
                        aFI_SaveCust.F02,
                        aFI_SaveCust.F03,
                        aFI_SaveCust.F04,
                        aFI_SaveCust.F05,
                        aFI_SaveCust.F06,
                        aFI_SaveCust.F07,
                        aFI_SaveCust.F08,
                        aFI_SaveCust.F09,
                        aFI_SaveCust.F10,
                        aFI_SaveCust.F11,
                        aFI_SaveCust.F12,
                        aFI_SaveCust.F13,
                        aFI_SaveCust.F14,
                        aFI_SaveCust.F15,
                        aFI_SaveCust.F16,
                        aFI_SaveCust.F17,
                        aFI_SaveCust.F18,
                        aFI_SaveCust.F19,
                        aFI_SaveCust.F20,
                        aFI_SaveCust.Email);

            var dataAccount = UnitDatabase.ReadUserPassHeader(this.Request.Headers);
            string conStr = UnitDatabase.CreateConnectionString(dataAccount[0], dataAccount[1], dataAccount[2], ace, sal, group, 0, "", 0, 0);
            if (conStr.Length > 100)
            {
                ApiModel db = new ApiModel(conStr);
                var list = db.Database.SqlQuery<string>(sql).Single();
                return Ok(list);
            }
            return Ok(conStr);
        }


        // get: api/AFI_DelCust
        [Route("api/Web_Data/AFI_DelCust/{ace}/{sal}/{group}/{CustCode}")]
        public async Task<IHttpActionResult> GetAFI_DelCust(string ace, string sal, string group, string CustCode)
        {
            string sql = string.Format(@"DECLARE	@return_value int
                                                    EXEC	@return_value = [dbo].[Web_DelCust]
		                                                    @Code = '{0}'
                                                    SELECT	'Return Value' = @return_value",
                                               CustCode);

            var dataAccount = UnitDatabase.ReadUserPassHeader(this.Request.Headers);
            string conStr = UnitDatabase.CreateConnectionString(dataAccount[0], dataAccount[1], dataAccount[2], ace, sal, group, 0, "", 0, 0);
            if (conStr.Length > 100)
            {
                ApiModel db = new ApiModel(conStr);
                var list = db.Database.SqlQuery<int>(sql).Single();
                return Ok(list);
            }
            return Ok(conStr);
        }




        public class AFI_SaveKala
        {
            public byte? BranchCode { get; set; }

            public string UserCode { get; set; }

            public string Code { get; set; }

            public string Name { get; set; }

            public string Spec { get; set; }

            public string KGruCode { get; set; }

            public string FanniNo { get; set; }

            public int? DefaultUnit { get; set; }

            public string UnitName1 { get; set; }

            public string UnitName2 { get; set; }

            public string UnitName3 { get; set; }

            public double? Weight1 { get; set; }

            public double? Weight2 { get; set; }

            public double? Weight3 { get; set; }

            public double? SPrice1 { get; set; }

            public double? SPrice2 { get; set; }

            public double? SPrice3 { get; set; }

            public double? PPrice1 { get; set; }

            public double? PPrice2 { get; set; }

            public double? PPrice3 { get; set; }

            public double? Zarib1 { get; set; }

            public double? Zarib2 { get; set; }

            public double? Zarib3 { get; set; }

            public Int16? DeghatM1 { get; set; }

            public Int16? DeghatM2 { get; set; }

            public Int16? DeghatM3 { get; set; }

            public Int16? DeghatR1 { get; set; }

            public Int16? DeghatR2 { get; set; }

            public Int16? DeghatR3 { get; set; }

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

        // post: api/AFI_SaveKala
        [Route("api/Web_Data/AFI_SaveKala/{ace}/{sal}/{group}")]
        public async Task<IHttpActionResult> PostAFI_SaveKala(string ace, string sal, string group, AFI_SaveKala aFI_SaveKala)
        {
            string sql = string.Format(CultureInfo.InvariantCulture,
                       @" DECLARE @oCode nvarchar(100)
                           EXEC	[dbo].[Web_SaveKala]
		                        @BranchCode = {0},
		                        @UserCode = N'{1}',
		                        @Code = N'{2}',
		                        @Name = N'{3}',
		                        @Spec = N'{4}',
		                        @KGruCode = N'{5}',
		                        @FanniNo = N'{6}',
		                        @DefaultUnit = {7},
		                        @UnitName1 = N'{8}',
		                        @UnitName2 = N'{9}',
		                        @UnitName3 = N'{10}',
		                        @Weight1 = {11},
		                        @Weight2 = {12},
		                        @Weight3 = {13},
		                        @SPrice1 = {14},
		                        @SPrice2 = {15},
		                        @SPrice3 = {16},
		                        @PPrice1 = {17},
		                        @PPrice2 = {18},
		                        @PPrice3 = {19},
		                        @Zarib1 = {20},
		                        @Zarib2 = {21},
		                        @Zarib3 = {22},
		                        @DeghatM1 = {23},
		                        @DeghatM2 = {24},
		                        @DeghatM3 = {25},
		                        @DeghatR1 = {26},
		                        @DeghatR2 = {27},
		                        @DeghatR3 = {28},
		                        @F01 = N'{29}',
		                        @F02 = N'{30}',
		                        @F03 = N'{31}',
		                        @F04 = N'{32}',
		                        @F05 = N'{33}',
		                        @F06 = N'{34}',
		                        @F07 = N'{35}',
		                        @F08 = N'{36}',
		                        @F09 = N'{37}',
		                        @F10 = N'{38}',
		                        @F11 = N'{39}',
		                        @F12 = N'{40}',
		                        @F13 = N'{41}',
		                        @F14 = N'{42}',
		                        @F15 = N'{43}',
		                        @F16 = N'{44}',
		                        @F17 = N'{45}',
		                        @F18 = N'{46}',
		                        @F19 = N'{47}',
		                        @F20 = N'{48}',
		                        @oCode = @oCode OUTPUT
                        SELECT	@oCode as N'@oCode' ",
                       aFI_SaveKala.BranchCode ?? 0,
                       aFI_SaveKala.UserCode,
                       aFI_SaveKala.Code,
                       aFI_SaveKala.Name ?? "",
                       aFI_SaveKala.Spec ?? "",
                       aFI_SaveKala.KGruCode ?? "",
                       aFI_SaveKala.FanniNo ?? "",
                       aFI_SaveKala.DefaultUnit ?? 1,
                       aFI_SaveKala.UnitName1 ?? "",
                       aFI_SaveKala.UnitName2 ?? "",
                       aFI_SaveKala.UnitName3 ?? "",
                       aFI_SaveKala.Weight1 ?? 0,
                       aFI_SaveKala.Weight2 ?? 0,
                       aFI_SaveKala.Weight3 ?? 0,
                       aFI_SaveKala.SPrice1 ?? 0,
                       aFI_SaveKala.SPrice2 ?? 0,
                       aFI_SaveKala.SPrice3 ?? 0,
                       aFI_SaveKala.PPrice1 ?? 0,
                       aFI_SaveKala.PPrice2 ?? 0,
                       aFI_SaveKala.PPrice3 ?? 0,
                       aFI_SaveKala.Zarib1 ?? 1,
                       aFI_SaveKala.Zarib2 ?? 0,
                       aFI_SaveKala.Zarib3 ?? 0,
                       aFI_SaveKala.DeghatM1 ?? 0,
                       aFI_SaveKala.DeghatM2 ?? 0,
                       aFI_SaveKala.DeghatM3 ?? 0,
                       aFI_SaveKala.DeghatR1 ?? 0,
                       aFI_SaveKala.DeghatR2 ?? 0,
                       aFI_SaveKala.DeghatR3 ?? 0,
                       aFI_SaveKala.F01,
                       aFI_SaveKala.F02,
                       aFI_SaveKala.F03,
                       aFI_SaveKala.F04,
                       aFI_SaveKala.F05,
                       aFI_SaveKala.F06,
                       aFI_SaveKala.F07,
                       aFI_SaveKala.F08,
                       aFI_SaveKala.F09,
                       aFI_SaveKala.F10,
                       aFI_SaveKala.F11,
                       aFI_SaveKala.F12,
                       aFI_SaveKala.F13,
                       aFI_SaveKala.F14,
                       aFI_SaveKala.F15,
                       aFI_SaveKala.F16,
                       aFI_SaveKala.F17,
                       aFI_SaveKala.F18,
                       aFI_SaveKala.F19,
                       aFI_SaveKala.F20);

            var dataAccount = UnitDatabase.ReadUserPassHeader(this.Request.Headers);
            string conStr = UnitDatabase.CreateConnectionString(dataAccount[0], dataAccount[1], dataAccount[2], ace, sal, group, 0, "", 0, 0);
            if (conStr.Length > 100)
            {
                ApiModel db = new ApiModel(conStr);
                var list = db.Database.SqlQuery<string>(sql);
                await db.SaveChangesAsync();
                return Ok(list);
            }
            return Ok(conStr);
        }


        // Get: api/AFI_DelKala
        [Route("api/Web_Data/AFI_DelKala/{ace}/{sal}/{group}/{KalaCode}")]
        public async Task<IHttpActionResult> GetAFI_DelKala(string ace, string sal, string group, string KalaCode)
        {
            string sql = string.Format(@"DECLARE	@return_value int
                                                    EXEC	@return_value = [dbo].[Web_DelKala]
		                                                    @Code = '{0}'
                                                 SELECT	'Return Value' = @return_value",
                                               KalaCode);
            var dataAccount = UnitDatabase.ReadUserPassHeader(this.Request.Headers);
            string conStr = UnitDatabase.CreateConnectionString(dataAccount[0], dataAccount[1], dataAccount[2], ace, sal, group, 0, "", 0, 0);
            if (conStr.Length > 100)
            {
                ApiModel db = new ApiModel(conStr);
                var list = db.Database.SqlQuery<int>(sql);
                await db.SaveChangesAsync();
                return Ok(list);
            }
            return Ok(conStr);
        }




        public partial class PrintForms_Object
        {
            public string lockNumber { get; set; }
            public string mode { get; set; }
        }


        public class Print
        {
            public int code { get; set; }
            public byte isPublic { get; set; }
            public byte accessGhimat { get; set; }
            public string Selected { get; set; }
            public string name { get; set; }
            public string namefa { get; set; }
            public string address { get; set; }
            public string Data { get; set; }
        }


        // Post: api/Web_Data/Web_PrintForms  لیست فرم های چاپ
        [Route("api/Web_Data/PrintForms/{ace}")]
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PostPrintForms(string ace, PrintForms_Object PrintForms_Object)
        {
            string fileName = "";
            string[] tempName;
            string[] tempAccess;
            string selected = "";
            byte isAccess = 0;
            int i = 0;


            string addressPrintForms = HttpContext.Current.Server.MapPath("~\\PrintForms");

            string filePublicPrintForms = addressPrintForms + "\\Public";

            if (!Directory.Exists(filePublicPrintForms))
            {
                System.IO.Directory.CreateDirectory(filePublicPrintForms);
            }


            string filePrintForms = addressPrintForms + "\\" + PrintForms_Object.lockNumber;
            string IniPath = filePrintForms + "\\data.Ini";
            IniFile MyIni = new IniFile(IniPath);

            if (!Directory.Exists(filePrintForms))
            {
                System.IO.Directory.CreateDirectory(filePrintForms);
                List<string> filteredPublicAllFiles = Directory.GetFiles(addressPrintForms + "\\Public", "*").ToList();
                foreach (var item in filteredPublicAllFiles)
                {
                    fileName = Path.GetFileName(item);
                    MyIni.Write(fileName, "1", "Public");
                }
            }

            List<string> filteredPublicFiles = Directory.GetFiles(addressPrintForms + "\\Public", PrintForms_Object.mode + "*").ToList();
            List<string> filteredFiles = Directory.GetFiles(addressPrintForms + "\\" + PrintForms_Object.lockNumber, PrintForms_Object.mode + "*").ToList();

            List<Print> listFile = new List<Print>();

            foreach (var item in filteredPublicFiles)
            {
                string lineOfText;
                string data = "";
                i++;
                FileStream filestream = new System.IO.FileStream(item,
                                                          System.IO.FileMode.Open,
                                                          System.IO.FileAccess.Read,
                                                          System.IO.FileShare.ReadWrite);
                var file = new System.IO.StreamReader(filestream, System.Text.Encoding.UTF8, true, 128);

                while ((lineOfText = file.ReadLine()) != null)
                {
                    data += lineOfText;
                }

                filestream.Close();

                isAccess = 0;
                fileName = Path.GetFileName(item);
                tempName = fileName.Split('-');



                int NoPrice = fileName.IndexOf("_NoPrice");

                if (NoPrice > -1) // no ghimat
                {
                    isAccess = 1;
                }


                selected = MyIni.Read(fileName, "Public");

                listFile.Add(new Print { code = i, isPublic = 1, accessGhimat = isAccess, Selected = selected, name = fileName, namefa = tempName[1], address = item, Data = data });
            }

            foreach (var item in filteredFiles)
            {

                string lineOfText;
                string data = "";
                i++;
                FileStream filestream = new System.IO.FileStream(item,
                                                          System.IO.FileMode.Open,
                                                          System.IO.FileAccess.Read,
                                                          System.IO.FileShare.ReadWrite);
                var file = new System.IO.StreamReader(filestream, System.Text.Encoding.UTF8, true, 128);

                while ((lineOfText = file.ReadLine()) != null)
                {
                    data += lineOfText;
                }

                filestream.Close();
                isAccess = 0;
                fileName = Path.GetFileName(item);
                tempName = fileName.Split('-');



                int NoPrice = fileName.IndexOf("_NoPrice");

                if (NoPrice > -1) // no ghimat
                {
                    isAccess = 1;
                }

                selected = MyIni.Read(fileName, "LockNumber");

                listFile.Add(new Print { code = i, isPublic = 0, accessGhimat = isAccess, Selected = selected, name = fileName, namefa = tempName[1], address = item, Data = data });
            }

            //var res = JsonConvert.SerializeObject(files);

            return Ok(listFile);
        }




        public partial class DeletePrintForm_Object
        {
            public string LockNumber { get; set; }
            public string Address { get; set; }
        }


        // Post: api/Web_Data/DeletePrintForm  حذف فرم چاپ
        [Route("api/Web_Data/DeletePrintForm/{ace}")]
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PostDeletePrintForm(string ace, DeletePrintForm_Object DeletePrintForm_Object)
        {
            if (File.Exists(DeletePrintForm_Object.Address))
            {
                string addressPrintFormsIni = HttpContext.Current.Server.MapPath("~\\PrintForms") + "\\" + DeletePrintForm_Object.LockNumber;
                string IniPath = addressPrintFormsIni + "\\data.Ini";
                IniFile MyIni = new IniFile(IniPath);
                string fileName = Path.GetFileName(DeletePrintForm_Object.Address);
                MyIni.DeleteKey(fileName, "LockNumber");


                File.Delete(DeletePrintForm_Object.Address);
            }


            return Ok("OK");
        }





        public partial class SavePrintForm_Object
        {
            public string LockNumber { get; set; }
            public string Name { get; set; }
            public string Mode { get; set; }
            public string Data { get; set; }
        }


        // Post: api/Web_Data/SavePrintForm  ذخیره فرم چاپ
        [Route("api/Web_Data/SavePrintForm/{ace}")]
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PostSavePrintForm(string ace, SavePrintForm_Object SavePrintForm_Object)
        {
            string addressPrintForms = HttpContext.Current.Server.MapPath("~\\PrintForms") + "\\" + SavePrintForm_Object.LockNumber + "\\" + SavePrintForm_Object.Mode + "-" + SavePrintForm_Object.Name;

            if (File.Exists(addressPrintForms))
            {
                File.Delete(addressPrintForms);
            }

            // fileName = Path.GetFileName(item);
            // tempName = fileName.Split('-');
            string addressPrintFormsIni = HttpContext.Current.Server.MapPath("~\\PrintForms") + "\\" + SavePrintForm_Object.LockNumber;
            string IniPath = addressPrintFormsIni + "\\data.Ini";
            IniFile MyIni = new IniFile(IniPath);
            MyIni.Write(SavePrintForm_Object.Mode + "-" + SavePrintForm_Object.Name, "0", "LockNumber");

            StreamWriter sw = File.CreateText(addressPrintForms);
            sw.WriteLine(SavePrintForm_Object.Data);
            sw.Close();
            return Ok("OK");
        }



        public partial class TestSavePrintForm_Object
        {
            public string LockNumber { get; set; }
            public string Name { get; set; }
            public string Mode { get; set; }
        }


        // Post: api/Web_Data/TestSavePrintForm  ذخیره فرم چاپ
        [Route("api/Web_Data/TestSavePrintForm/{ace}")]
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PostTestSavePrintForm(string ace, TestSavePrintForm_Object TestSavePrintForm_Object)
        {
            string addressPrintForms = HttpContext.Current.Server.MapPath("~\\PrintForms") + "\\" + TestSavePrintForm_Object.LockNumber + "\\" + TestSavePrintForm_Object.Mode + "-" + TestSavePrintForm_Object.Name;

            if (File.Exists(addressPrintForms))
            {
                return Ok("FindFile");
            }
            return Ok("");
        }


        public partial class SelectedPrintForm_Object
        {
            public string LockNumber { get; set; }
            public string Address { get; set; }
            public byte IsPublic { get; set; }

        }


        // Post: api/Web_Data/SelectedPrintForm  انتخاب فرم چاپ
        [Route("api/Web_Data/SelectedPrintForm/{ace}")]
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PostSelectedPrintForm(string ace, SelectedPrintForm_Object SelectedPrintForm_Object)
        {
            string addressPrintFormsIni = HttpContext.Current.Server.MapPath("~\\PrintForms") + "\\" + SelectedPrintForm_Object.LockNumber;
            string IniPath = addressPrintFormsIni + "\\data.Ini";
            IniFile MyIni = new IniFile(IniPath);
            string fileName = Path.GetFileName(SelectedPrintForm_Object.Address);
            string oldData = MyIni.Read(fileName, SelectedPrintForm_Object.IsPublic == 0 ? "LockNumber" : "Public");
            MyIni.Write(fileName, oldData == "0" ? "1" : "0", SelectedPrintForm_Object.IsPublic == 0 ? "LockNumber" : "Public");
            return Ok("OK");
        }





        public partial class SelectedAccessGhimatPrintForm_Object
        {
            public string LockNumber { get; set; }
            public string Address { get; set; }
            public byte IsPublic { get; set; }
        }


        // Post: api/Web_Data/SelectedAccessGhimatPrintForm  انتخاب فرم چاپ
        [Route("api/Web_Data/SelectedAccessGhimatPrintForm/{ace}")]
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PostSelectedAccessGhimatPrintForm(string ace, SelectedAccessGhimatPrintForm_Object SelectedAccessGhimatPrintForm_Object)
        {
            string[] tempName;
            string[] tempAccess;
            string address = SelectedAccessGhimatPrintForm_Object.Address;
            string name = "";

            string fileName = Path.GetFileName(address);
            string fileDir = Path.GetDirectoryName(address) + "\\";


            int NoPrice = fileName.IndexOf("_NoPrice");

            tempName = fileName.Split('-');

            tempAccess = tempName[0].Split('_');

            if (NoPrice == -1) // no ghimat
            {
                name = tempName[0] + "_NoPrice-" + tempName[1];
            }
            else
            {
                name = fileName.Remove(NoPrice, 8);
            }

            try
            {
                File.Move(address, fileDir + name);
            }
            catch (Exception)
            {
                return Ok("FindFile");
                throw;
            }



            string addressPrintFormsIni = HttpContext.Current.Server.MapPath("~\\PrintForms") + "\\" + SelectedAccessGhimatPrintForm_Object.LockNumber;
            string IniPath = addressPrintFormsIni + "\\data.Ini";
            IniFile MyIni = new IniFile(IniPath);
            fileName = Path.GetFileName(address);

            string selected = MyIni.Read(fileName, "LockNumber");

            MyIni.DeleteKey(fileName, "LockNumber");

            MyIni.Write(name, selected, "LockNumber");


            return Ok("OK");
        }




        public class ProgTrsObject
        {
            public string User { get; set; }
        }

        public class Web_ProgTrs
        {
            public int id { get; set; }

            public string status { get; set; }

            public string prog { get; set; }
        }


        // Post: api/Web_Data/ProgTrs لیست گروه ها
        [Route("api/Web_Data/ProgTrs/{ace}")]
        public async Task<IHttpActionResult> PostWeb_ProgTrs(string ace, ProgTrsObject ProgTrsObject)
        {
            string sql;
            if (ace == "Web1")
                sql = string.Format("select * FROM Web_ProgTrs('{0}') where prog in ('Afi1','Erj1') ", ProgTrsObject.User);
            else
                sql = string.Format("select * FROM Web_ProgTrs('{0}') where prog not in ('Afi1') ", ProgTrsObject.User);

            var dataAccount = UnitDatabase.ReadUserPassHeader(this.Request.Headers);
            string conStr = UnitDatabase.CreateConnectionString(dataAccount[0], dataAccount[1], dataAccount[2], "Config", "", "0", 0, "", 0, 0);
            if (conStr.Length > 100)
            {
                ApiModel db = new ApiModel(conStr);
                var list = db.Database.SqlQuery<Web_ProgTrs>(sql);
                return Ok(list);
            }
            return Ok(conStr);
        }






        public class GroupsObject
        {
            public string ProgName { get; set; }

            public string User { get; set; }

            public string groups { get; set; }
        }

        public class Web_Groups
        {
            public int? Code { get; set; }

            public string Name { get; set; }

        }


        // Post: api/Web_Data/Groups لیست گروه ها
        [Route("api/Web_Data/Groups")]
        public async Task<IHttpActionResult> PostWeb_Groups(GroupsObject GroupsObject)
        {
            string sql = string.Format("select * FROM  Web_Groups('{0}','{1}') where code in ({2})", GroupsObject.ProgName, GroupsObject.User, GroupsObject.groups);
            var dataAccount = UnitDatabase.ReadUserPassHeader(this.Request.Headers);
            string conStr = UnitDatabase.CreateConnectionString(dataAccount[0], dataAccount[1], dataAccount[2], "Config", "", "0", 0, "", 0, 0);
            if (conStr.Length > 100)
            {
                ApiModel db = new ApiModel(conStr);
                var list = db.Database.SqlQuery<Web_Groups>(sql);
                return Ok(list);
            }
            return Ok(conStr);
        }






        public class Web_Statements
        {
            public int? Code { get; set; }

            //public string UserCode { get; set; }

            public string Name { get; set; }

        }


        // Get: api/Web_Data/Statements لیست عبارات تعریف شده
        [Route("api/Web_Data/Statements")]
        public async Task<IHttpActionResult> GetStatements()
        {
            var dataAccount = UnitDatabase.ReadUserPassHeader(this.Request.Headers);
            string sql = string.Format("select * FROM  Web_Statements('{0}')", dataAccount[2]);
            string conStr = UnitDatabase.CreateConnectionString(dataAccount[0], dataAccount[1], dataAccount[2], "Config", "", "0", 0, "", 0, 0);
            if (conStr.Length > 100)
            {
                ApiModel db = new ApiModel(conStr);
                var list = db.Database.SqlQuery<Web_Statements>(sql);
                return Ok(list);
            }
            return Ok(conStr);
        }


        public class SaveStatementsObject
        {
            public string Comm { get; set; }

        }

        [Route("api/Web_Data/SaveStatements")]
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PostWeb_SaveStatements(SaveStatementsObject SaveStatementsObject)
        {
            var dataAccount = UnitDatabase.ReadUserPassHeader(this.Request.Headers);
            string sql = string.Format(@"DECLARE @return_value int
                                                 EXEC	 @return_value = [dbo].[Web_SaveStatements]
		                                                 @UserCode = N'{0}',
		                                                 @Comm = N'{1}'
                                                 SELECT	'Return Value' = @return_value",
                       dataAccount[2],
                       SaveStatementsObject.Comm);

            string conStr = UnitDatabase.CreateConnectionString(dataAccount[0], dataAccount[1], dataAccount[2], "Config", "", "", 0, "", 0, 0);
            if (conStr.Length > 100)
            {
                ApiModel db = new ApiModel(conStr);
                var list = db.Database.SqlQuery<int>(sql).Single();
                await db.SaveChangesAsync();
                return Ok(list);
            }
            return Ok(conStr);
        }






        // Post: api/Web_Data/ChangeDatabase 
        [Route("api/Web_Data/ChangeDatabase/{ace}/{sal}/{group}/{auto}/{lockNumber}")]
        public string GetWeb_ChangeDatabase(string ace, string sal, string group, bool auto, string lockNumber)
        {
            string IniPath = HttpContext.Current.Server.MapPath("~/Content/ini/ServerConfig.Ini");

            IniFile MyIni = new IniFile(IniPath);

            string addressFileSql = MyIni.Read("FileSql");

            string IniConfigPath = addressFileSql + "\\" + lockNumber + "\\Config_" + ace + group + sal + ".ini";

            IniFile MyIniConfig = new IniFile(IniConfigPath);
            try
            {
                string Change = MyIniConfig.Read("Change");
                string BeginDate = MyIniConfig.Read("BeginDate");
                string User = MyIniConfig.Read("User");
                string Prog = MyIniConfig.Read("Prog");
                string Group = MyIniConfig.Read("Group");
                string Sal = MyIniConfig.Read("Sal");
                string EndDate = MyIniConfig.Read("EndDate");

                string res = "";
                var dataAccount = UnitDatabase.ReadUserPassHeader(this.Request.Headers);
                if (Change != "1")
                {
                    res = UnitDatabase.ChangeDatabase(ace, sal, group, dataAccount[2], auto, lockNumber, dataAccount[0], dataAccount[1]);
                    return res;
                }
                else if (Change == "1" && dataAccount[2] == "ACE" && auto == false)
                {
                    res = UnitDatabase.ChangeDatabase(ace, sal, group, dataAccount[2], auto, lockNumber, dataAccount[0], dataAccount[1]);
                    return res;
                }
                else
                    return "کاربر " + User + " در حال بازسازی اطلاعات است . لطفا منتظر بمانید ";
            }
            catch (Exception e)
            {
                MyIniConfig.Write("Change", "0");
                MyIniConfig.Write("EndDate", DateTime.Now.ToString());
                MyIniConfig.Write("error", e.Message.ToString());
                return "error" + e.Message.ToString();
                throw;
            }
        }


        // GET: api/Web_Data/ بازسازی دستی بانک اطلاعات کانفیگ  
        [Route("api/Web_Data/ChangeDatabaseConfig/{lockNumber}/{auto}")]
        public string GetWeb_ChangeDatabaseConfig(string lockNumber, bool auto)

        {
            string IniPath = HttpContext.Current.Server.MapPath("~/Content/ini/ServerConfig.Ini");

            IniFile MyIni = new IniFile(IniPath);

            string addressFileSql = MyIni.Read("FileSql");

            string IniConfigPath = addressFileSql + "\\" + lockNumber + "\\Config.ini";

            IniFile MyIniConfig = new IniFile(IniConfigPath);
            try
            {
                string Change = MyIniConfig.Read("Change");
                string BeginDate = MyIniConfig.Read("BeginDate");
                string User = MyIniConfig.Read("User");
                string Prog = MyIniConfig.Read("Prog");
                string Group = MyIniConfig.Read("Group");
                string Sal = MyIniConfig.Read("Sal");
                string EndDate = MyIniConfig.Read("EndDate");

                string res = "";
                var dataAccount = UnitDatabase.ReadUserPassHeader(this.Request.Headers);
                if ((Change != "1") || (Change == "1" && dataAccount[2] == "ACE" && auto == false))
                {
                    return res = UnitDatabase.ChangeDatabaseConfig(dataAccount[2], auto == false ? "1234" : "", dataAccount[0], dataAccount[1]);
                }
                else
                    return "کاربر " + User + " در حال بازسازی اطلاعات است . لطفا منتظر بمانید ";
            }
            catch (Exception e)
            {
                MyIniConfig.Write("Change", "0");
                MyIniConfig.Write("EndDate", DateTime.Now.ToString());
                MyIniConfig.Write("error", e.Message.ToString());
                return "error" + e.Message.ToString();
                throw;
            }
        }



        /*

        {
            string IniPath = HttpContext.Current.Server.MapPath("~/Content/ini/ServerConfig.Ini");

            IniFile MyIni = new IniFile(IniPath);

            string addressFileSql = MyIni.Read("FileSql");

            string IniConfigPath = addressFileSql + "\\" + lockNumber + "\\" + "Config.ini";

            IniFile MyIniConfig = new IniFile(IniConfigPath);

            try
            {
                var dataAccount = UnitDatabase.ReadUserPassHeader(this.Request.Headers);

                string conStr = UnitDatabase.CreateConnectionString(dataAccount[0], dataAccount[1], dataAccount[2], "Config", "1234", "00", 0, "", 0, 0);
                if (conStr.Length > 100)
                {
                    ApiModel db = new ApiModel(conStr);
                    return "OK";
                }
            }
            catch (Exception e)
            {
                MyIniConfig.Write("Change", "0");
                MyIniConfig.Write("EndDate", DateTime.Now.ToString());
                MyIniConfig.Write("error", e.Message.ToString());
                return "error" + e.Message.ToString();
                throw;
            }
        }*/


        public class Object_ErjDocXK
        {
            public int ModeCode { get; set; }

            public string LockNo { get; set; }
        }


        [Route("api/Web_Data/Web_ErjDocXK/{ace}/{sal}/{group}")]
        public async Task<IHttpActionResult> PostWeb_ErjDocXK(string ace, string sal, string group, Object_ErjDocXK Object_ErjDocXK)
        {
            string sql = string.Format("select * from dbo.Web_ErjDocXK({0},'{1}') order by DocDate desc , SerialNumber desc", Object_ErjDocXK.ModeCode, Object_ErjDocXK.LockNo);
            var dataAccount = UnitDatabase.ReadUserPassHeader(this.Request.Headers);
            string conStr = UnitDatabase.CreateConnectionString(dataAccount[0], dataAccount[1], dataAccount[2], ace, sal, group, 0, "", 0, 0);
            if (conStr.Length > 100)
            {
                ApiModel db = new ApiModel(conStr);
                var list = db.Database.SqlQuery<Web_ErjDocXK>(sql);
                return Ok(list);
            }
            return Ok(conStr);
        }


        public class Object_TicketStatus
        {
            public string SerialNumber { get; set; }
        }


        [Route("api/Web_Data/Web_TicketStatus/{ace}/{sal}/{group}")]
        public async Task<IHttpActionResult> PostWeb_TicketStatus(string ace, string sal, string group, Object_TicketStatus Object_TicketStatus)
        {
            string sql = string.Format(@"declare @serialnumber nvarchar(100) = '{0}'
                                             select * from Web_TicketStatus where 1 = 1 and
                                            (@serialnumber = '' or serialnumber = @serialnumber)",
                                           Object_TicketStatus.SerialNumber);
            var dataAccount = UnitDatabase.ReadUserPassHeader(this.Request.Headers);
            string conStr = UnitDatabase.CreateConnectionString(dataAccount[0], dataAccount[1], dataAccount[2], ace, sal, group, 0, "", 0, 0);
            if (conStr.Length > 100)
            {
                ApiModel db = new ApiModel(conStr);
                var list = db.Database.SqlQuery<Web_TicketStatus>(sql);
                return Ok(list);
            }
            return Ok(conStr);
        }

        public class ErjSaveTicket_HI
        {
            public long SerialNumber { get; set; }

            public string DocDate { get; set; }

            public string UserCode { get; set; }

            public string Status { get; set; }

            public string Spec { get; set; }

            public string LockNo { get; set; }

            public string Text { get; set; }

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

            public string Motaghazi { get; set; }

        }


        [Route("api/Web_Data/ErjSaveTicket_HI/{ace}/{sal}/{group}")]
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PostErjSaveTicket_HI(string ace, string sal, string group, ErjSaveTicket_HI ErjSaveTicket_HI)
        {
            string sql = string.Format(@"
                                    DECLARE	@DocNo_Out int
                                    EXEC	[dbo].[Web_ErjSaveTicket_HI]
		                                    @SerialNumber = {0},
		                                    @DocDate = '{1}',
		                                    @UserCode = '{2}',
		                                    @Status = '{3}',
		                                    @Spec = '{4}',
		                                    @LockNo = '{5}',
		                                    @Text = '{6}',
		                                    @F01 = '{7}',
		                                    @F02 = '{8}',
		                                    @F03 = '{9}',
		                                    @F04 = '{10}',
		                                    @F05 = '{11}',
		                                    @F06 = '{12}',
		                                    @F07 = '{13}',
		                                    @F08 = '{14}',
		                                    @F09 = '{15}',
		                                    @F10 = '{16}',
		                                    @F11 = '{17}',
		                                    @F12 = '{18}',
		                                    @F13 = '{19}',
		                                    @F14 = '{20}',
		                                    @F15 = '{21}',
		                                    @F16 = '{22}',
		                                    @F17 = '{23}',
		                                    @F18 = '{24}',
		                                    @F19 = '{25}',
		                                    @F20 = '{26}',
		                                    @Motaghazi = '{27}',
		                                    @DocNo_Out = @DocNo_Out OUTPUT
                                    SELECT	@DocNo_Out as N'DocNo_Out'",
                                           ErjSaveTicket_HI.SerialNumber,
                                           ErjSaveTicket_HI.DocDate ?? string.Format("{0:yyyy/MM/dd}", DateTime.Now.AddDays(-1)),
                                           ErjSaveTicket_HI.UserCode,
                                           ErjSaveTicket_HI.Status,
                                           ErjSaveTicket_HI.Spec,
                                           ErjSaveTicket_HI.LockNo,
                                           UnitPublic.ConvertTextWebToWin(ErjSaveTicket_HI.Text ?? ""),
                                           ErjSaveTicket_HI.F01,
                                           ErjSaveTicket_HI.F02,
                                           ErjSaveTicket_HI.F03,
                                           ErjSaveTicket_HI.F04,
                                           ErjSaveTicket_HI.F05,
                                           ErjSaveTicket_HI.F06,
                                           ErjSaveTicket_HI.F07,
                                           ErjSaveTicket_HI.F08,
                                           ErjSaveTicket_HI.F09,
                                           ErjSaveTicket_HI.F10,
                                           ErjSaveTicket_HI.F11,
                                           ErjSaveTicket_HI.F12,
                                           ErjSaveTicket_HI.F13,
                                           ErjSaveTicket_HI.F14,
                                           ErjSaveTicket_HI.F15,
                                           ErjSaveTicket_HI.F16,
                                           ErjSaveTicket_HI.F17,
                                           ErjSaveTicket_HI.F18,
                                           ErjSaveTicket_HI.F19,
                                           ErjSaveTicket_HI.F20,
                                           ErjSaveTicket_HI.Motaghazi
                                           );

            var dataAccount = UnitDatabase.ReadUserPassHeader(this.Request.Headers);
            string conStr = UnitDatabase.CreateConnectionString(dataAccount[0], dataAccount[1], dataAccount[2], ace, sal, group, 0, "", 0, 0);
            if (conStr.Length > 100)
            {
                ApiModel db = new ApiModel(conStr);
                var list = db.Database.SqlQuery<int>(sql).Single();
                await db.SaveChangesAsync();
                return Ok(list);
            }
            return Ok(conStr);
        }




        public class AFI_TestCust
        {
            public string Code { get; set; }
        }


        public class TestCust
        {
            public byte? Test { get; set; }

            public string TestName { get; set; }

            public string TestCap { get; set; }

            public int? BandNo { get; set; }
        }


        [Route("api/Web_Data/TestCust/{ace}/{sal}/{group}")]
        [ResponseType(typeof(TestCust))]
        public async Task<IHttpActionResult> PostWeb_TestCust(string ace, string sal, string group, AFI_TestCust AFI_TestCust)
        {
            var dataAccount = UnitDatabase.ReadUserPassHeader(this.Request.Headers);
            string sql = string.Format(CultureInfo.InvariantCulture,
                           @"EXEC	[dbo].[Web_TestCust] @Code = '{0}'  , @UserCode = '{1}' ",
                           AFI_TestCust.Code,
                           dataAccount[2]);
            string conStr = UnitDatabase.CreateConnectionString(dataAccount[0], dataAccount[1], dataAccount[2], ace, sal, group, 0, "", 0, 0);
            if (conStr.Length > 100)
            {
                ApiModel db = new ApiModel(conStr);
                var result = db.Database.SqlQuery<TestCust>(sql);
                var list = JsonConvert.SerializeObject(result);
                return Ok(list);
            }
            return Ok(conStr);
        }




        public class TestCust_DeleteObject
        {
            public string Code { get; set; }

        }

        public class TestCust_Delete
        {
            public int id { get; set; }

            public byte Test { get; set; }

            public string TestName { get; set; }

            public string TestCap { get; set; }

            public int BandNo { get; set; }

        }



        [Route("api/Web_Data/TestCust_Delete/{ace}/{sal}/{group}")]
        [ResponseType(typeof(TestCust_Delete))]
        public async Task<IHttpActionResult> PostWeb_TestCust_Delete(string ace, string sal, string group, TestCust_DeleteObject TestCust_DeleteObject)
        {
            var dataAccount = UnitDatabase.ReadUserPassHeader(this.Request.Headers);
            string sql = string.Format(CultureInfo.InvariantCulture, @"EXEC	[dbo].[Web_TestCust_Delete] @Code = '{0}', @UserCode = '{1}' ", TestCust_DeleteObject.Code, dataAccount[2]);
            string conStr = UnitDatabase.CreateConnectionString(dataAccount[0], dataAccount[1], dataAccount[2], ace, sal, group, 0, "", 0, 0);
            if (conStr.Length > 100)
            {
                ApiModel db = new ApiModel(conStr);
                var result = db.Database.SqlQuery<TestCust_Delete>(sql).ToList();
                var list = JsonConvert.SerializeObject(result);
                return Ok(list);
            }
            return Ok(conStr);
        }



        public class AFI_TestKala
        {
            public string Code { get; set; }
        }


        public class TestKala
        {
            public byte? Test { get; set; }

            public string TestName { get; set; }

            public string TestCap { get; set; }

            public int? BandNo { get; set; }
        }


        [Route("api/Web_Data/TestKala/{ace}/{sal}/{group}")]
        [ResponseType(typeof(TestKala))]
        public async Task<IHttpActionResult> PostWeb_TestKala(string ace, string sal, string group, AFI_TestKala AFI_TestKala)
        {
            var dataAccount = UnitDatabase.ReadUserPassHeader(this.Request.Headers);
            string sql = string.Format(CultureInfo.InvariantCulture,
                                       @"EXEC	[dbo].[Web_TestKala] @Code = '{0}' , @UserCode = '{1}' ",
                                       AFI_TestKala.Code,
                                       dataAccount[2]);
            string conStr = UnitDatabase.CreateConnectionString(dataAccount[0], dataAccount[1], dataAccount[2], ace, sal, group, 0, "", 0, 0);
            if (conStr.Length > 100)
            {
                ApiModel db = new ApiModel(conStr);
                var result = db.Database.SqlQuery<TestKala>(sql);
                var list = JsonConvert.SerializeObject(result);
                return Ok(list);
            }
            return Ok(conStr);
        }


        public class TestKala_DeleteObject
        {
            public string Code { get; set; }

        }

        public class TestKala_Delete
        {
            public int id { get; set; }

            public byte Test { get; set; }

            public string TestName { get; set; }

            public string TestCap { get; set; }

            public int BandNo { get; set; }

        }



        [Route("api/Web_Data/TestKala_Delete/{ace}/{sal}/{group}")]
        [ResponseType(typeof(TestKala_Delete))]
        public async Task<IHttpActionResult> PostWeb_TestKala_Delete(string ace, string sal, string group, TestKala_DeleteObject TestKala_DeleteObject)
        {
            var dataAccount = UnitDatabase.ReadUserPassHeader(this.Request.Headers);
            string sql = string.Format(CultureInfo.InvariantCulture,
                                           @"EXEC	[dbo].[Web_TestKala_Delete] @Code = '{0}', @UserCode = '{1}' ", TestKala_DeleteObject.Code, dataAccount[2]);

            string conStr = UnitDatabase.CreateConnectionString(dataAccount[0], dataAccount[1], dataAccount[2], ace, sal, group, 0, "", 0, 0);
            if (conStr.Length > 100)
            {
                ApiModel db = new ApiModel(conStr);
                var result = db.Database.SqlQuery<TestKala_Delete>(sql).ToList();
                var list = JsonConvert.SerializeObject(result);
                return Ok(list);
            }
            return Ok(conStr);
        }


        // GET: api/Web_Data/Date تاریخ سرور
        [Route("api/Web_Data/Date")]
        public async Task<IHttpActionResult> GetWeb_Date()
        {
            string sql = string.Format(@"select dbo.Web_CurrentShamsiDate() as tarikh");
            var dataAccount = UnitDatabase.ReadUserPassHeader(this.Request.Headers);
            string conStr = UnitDatabase.CreateConnectionString(dataAccount[0], dataAccount[1], dataAccount[2], "Config", "", "", 0, "", 0, 0);
            if (conStr.Length > 100)
            {
                ApiModel db = new ApiModel(conStr);
                var list = db.Database.SqlQuery<string>(sql);
                return Ok(list);
            }
            return Ok(conStr);
        }




        public class Web_Farayand
        {
            public int Code { get; set; }

            public string Name { get; set; }

            public string Spec { get; set; }
        }

        // GET: api/Web_Data/Farayand لیست فرایند  
        [Route("api/Web_Data/Farayand/{ace}/{sal}/{group}/{KhdtCode}")]
        public async Task<IHttpActionResult> GetWeb_Farayand(string ace, string sal, string group, string KhdtCode)
        {
            string sql = string.Format(@"Select * from Web_Farayand({0})", KhdtCode);
            var dataAccount = UnitDatabase.ReadUserPassHeader(this.Request.Headers);
            string conStr = UnitDatabase.CreateConnectionString(dataAccount[0], dataAccount[1], dataAccount[2], ace, sal, group, 0, "", 0, 0);
            if (conStr.Length > 100)
            {
                ApiModel db = new ApiModel(conStr);
                var list = db.Database.SqlQuery<Web_Farayand>(sql);
                return Ok(list);
            }
            return Ok(conStr);
        }



        public class Web_DocInUse
        {
            public string Prog { get; set; }

            public string DMode { get; set; }

            public string GroupNo { get; set; }

            public string Year { get; set; }

            public long SerialNumber { get; set; }
        }

        public class DocInUse
        {
            public string UserCode { get; set; }
            public string UserName { get; set; }

        }

        // Post: api/Web_Data/DocInUse تست بازبودن سند در ویندوز
        [Route("api/Web_Data/DocInUse")]
        public async Task<IHttpActionResult> PostDocInUse(Web_DocInUse Web_DocInUse)
        {
            string sql = string.Format(@"
                                            DECLARE @UserCode nvarchar(100),
                                                    @UserName nvarchar(100)
                                            EXEC	[dbo].[Web_DocInUse]
		                                            @Prog = N'{0}',
		                                            @DMode = {1},
		                                            @GroupNo = {2},
		                                            @Year = {3},
		                                            @SerialNumber = {4},
		                                            @UserCode = @UserCode OUTPUT,
                                                    @UserName = @UserName OUTPUT
                                            SELECT	@UserCode as UserCode , @UserName as UserName",
                             Web_DocInUse.Prog,
                             Web_DocInUse.DMode,
                             Web_DocInUse.GroupNo,
                             Web_DocInUse.Year,
                             Web_DocInUse.SerialNumber);

            var dataAccount = UnitDatabase.ReadUserPassHeader(this.Request.Headers);
            string conStr = UnitDatabase.CreateConnectionString(dataAccount[0], dataAccount[1], dataAccount[2], "Config", "", "", 0, "", 0, 0);
            if (conStr.Length > 100)
            {
                ApiModel db = new ApiModel(conStr);
                var list = db.Database.SqlQuery<DocInUse>(sql);
                return Ok(list);
            }
            return Ok(conStr);
        }





        public class Web_SaveDocInUse
        {
            public string Prog { get; set; }

            public string DMode { get; set; }

            public string GroupNo { get; set; }

            public string Year { get; set; }

            public long SerialNumber { get; set; }

            public string DocNo { get; set; }
        }

        // Post: api/Web_Data/SaveDocInUse ثبت سند بازشده در ویندوز
        [Route("api/Web_Data/SaveDocInUse")]
        public async Task<IHttpActionResult> PostSaveDocInUse(Web_SaveDocInUse Web_SaveDocInUse)
        {
            var dataAccount = UnitDatabase.ReadUserPassHeader(this.Request.Headers);
            string sql = string.Format(@" EXEC	[dbo].[Web_SaveDocInUse]
		                                            @Prog = N'{0}',
		                                            @DMode = {1},
		                                            @GroupNo = {2},
		                                            @Year = {3},
		                                            @SerialNumber = {4},
		                                            @DocNo = '{5}',
		                                            @UserCode = '{6}'
                                              SELECT 'ok' ",
                                        Web_SaveDocInUse.Prog,
                                        Web_SaveDocInUse.DMode,
                                        Web_SaveDocInUse.GroupNo,
                                        Web_SaveDocInUse.Year,
                                        Web_SaveDocInUse.SerialNumber,
                                        Web_SaveDocInUse.DocNo,
                                        dataAccount[2]);
            string conStr = UnitDatabase.CreateConnectionString(dataAccount[0], dataAccount[1], dataAccount[2], "Config", "", "", 0, "", 0, 0);
            if (conStr.Length > 100)
            {
                ApiModel db = new ApiModel(conStr);
                var list = db.Database.SqlQuery<string>(sql);
                return Ok(list);
            }
            return Ok(conStr);
        }



        public class Web_DeleteDocInUse
        {
            public string Prog { get; set; }

            public string DMode { get; set; }

            public string GroupNo { get; set; }

            public string Year { get; set; }

            public string SerialNumber { get; set; }

        }

        // Post: api/Web_Data/DeleteDocInUse ثبت سند بازشده در ویندوز
        [Route("api/Web_Data/DeleteDocInUse")]
        public async Task<IHttpActionResult> PostDeleteDocInUse(Web_DeleteDocInUse Web_DeleteDocInUse)
        {
            var dataAccount = UnitDatabase.ReadUserPassHeader(this.Request.Headers);
            string sql = string.Format(@" EXEC	[dbo].[Web_DeleteDocInUse]
		                                            @Prog = N'{0}',
		                                            @DMode = {1},
		                                            @GroupNo = {2},
		                                            @Year = {3},
		                                            @SerialNumber = {4},
		                                            @UserCode = '{5}'
                                              SELECT 'ok'  ",
                                            Web_DeleteDocInUse.Prog,
                                            Web_DeleteDocInUse.DMode,
                                            Web_DeleteDocInUse.GroupNo,
                                            Web_DeleteDocInUse.Year,
                                            Web_DeleteDocInUse.SerialNumber,
                                            dataAccount[2]);
            string conStr = UnitDatabase.CreateConnectionString(dataAccount[0], dataAccount[1], dataAccount[2], "Config", "", "", 0, "", 0, 0);
            if (conStr.Length > 100)
            {
                ApiModel db = new ApiModel(conStr);
                var list = db.Database.SqlQuery<string>(sql);
                return Ok(list);
            }
            return Ok(conStr);
        }







        public class AGruObject
        {
            public byte Mode { get; set; }

            public string UserCode { get; set; }
        }

        // Post: api/Web_Data/AGru  لیست گروه حساب ها  
        [Route("api/Web_Data/AGru/{ace}/{sal}/{group}")]
        public async Task<IHttpActionResult> PostWeb_AGru(string ace, string sal, string group, AGruObject AGruObject)
        {
            string sql = string.Format("select  * FROM  Web_AGru");
            var dataAccount = UnitDatabase.ReadUserPassHeader(this.Request.Headers);
            string conStr = UnitDatabase.CreateConnectionString(dataAccount[0], dataAccount[1], dataAccount[2], ace, sal, group, 0, "", 0, 0);
            if (conStr.Length > 100)
            {
                ApiModel db = new ApiModel(conStr);
                var list = db.Database.SqlQuery<Web_AGru>(sql);
                return Ok(list);
            }
            return Ok(conStr);
        }








        public class AFI_SaveAcc
        {
            public byte? BranchCode { get; set; }

            public string UserCode { get; set; }

            public string Code { get; set; }

            public string Name { get; set; }

            public string LtnName { get; set; }

            public string Spec { get; set; }

            public string AGruCode { get; set; }

            public string ZGru { get; set; }

            public string EMail { get; set; }

            public string Mobile { get; set; }

            public Int16? PDMode { get; set; }

            public Int16? Mahiat { get; set; }

            public Int16? AccStatus { get; set; }

            public Int16? NextLevelFromZAcc { get; set; }

            public Int16? Arzi { get; set; }

            public Int16? Mkz { get; set; }

            public Int16? Opr { get; set; }

            public Int16? Amount { get; set; }

            public string Vahed { get; set; }

            public Int16? Deghat { get; set; }

            public string AccComm { get; set; }

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

        // post: api/AFI_SaveAcc
        [Route("api/Web_Data/AFI_SaveAcc/{ace}/{sal}/{group}")]
        public async Task<IHttpActionResult> PostAFI_SaveAcc(string ace, string sal, string group, AFI_SaveAcc aFI_SaveAcc)
        {
            string sql = string.Format(CultureInfo.InvariantCulture,
                        @" DECLARE @oCode nvarchar(100)
                           EXEC	[dbo].[Web_SaveAcc]
		                        @BranchCode = {0},
		                        @UserCode = N'{1}',
		                        @Code = '{2}',
		                        @Name = N'{3}',
                                @Spec = N'{4}',
		                        @LtnName = N'{5}',
		                        @AGruCode = '{6}',
		                        @NextLevelFromZAcc = {7},
		                        @ZGru = '{8}',
		                        @AccComm = '{9}',
		                        @EMail = '{10}',
		                        @Mobile = '{11}',
		                        @PDMode = {12},
		                        @Mahiat = '{13}',
		                        @AccStatus = {14},
		                        @Arzi = {15},
		                        @Opr = {16},
		                        @Mkz = {17},
		                        @Amount = {18},
		                        @Vahed = '{19}',
		                        @Deghat = {20},
		                        @F01 = N'{21}',
		                        @F02 = N'{22}',
		                        @F03 = N'{23}',
		                        @F04 = N'{24}',
		                        @F05 = N'{25}',
		                        @F06 = N'{26}',
		                        @F07 = N'{27}',
		                        @F08 = N'{28}',
		                        @F09 = N'{29}',
		                        @F10 = N'{30}',
		                        @F11 = N'{31}',
		                        @F12 = N'{32}',
		                        @F13 = N'{33}',
		                        @F14 = N'{34}',
		                        @F15 = N'{35}',
		                        @F16 = N'{36}',
		                        @F17 = N'{37}',
		                        @F18 = N'{38}',
		                        @F19 = N'{39}',
		                        @F20 = N'{40}',
		                        @oCode = @oCode OUTPUT
                        SELECT	@oCode as N'@oCode' ",
                                aFI_SaveAcc.BranchCode ?? 0,
                                aFI_SaveAcc.UserCode,
                                aFI_SaveAcc.Code,
                                aFI_SaveAcc.Name ?? "",
                                aFI_SaveAcc.Spec ?? "",
                                aFI_SaveAcc.LtnName ?? "",
                                aFI_SaveAcc.AGruCode,
                                aFI_SaveAcc.NextLevelFromZAcc,
                                aFI_SaveAcc.ZGru,
                                UnitPublic.ConvertTextWebToWin(aFI_SaveAcc.AccComm ?? ""),
                                aFI_SaveAcc.EMail,
                                aFI_SaveAcc.Mobile,
                                aFI_SaveAcc.PDMode,
                                aFI_SaveAcc.Mahiat,
                                aFI_SaveAcc.AccStatus,
                                aFI_SaveAcc.Arzi,
                                aFI_SaveAcc.Opr,
                                aFI_SaveAcc.Mkz,
                                aFI_SaveAcc.Amount ?? 0,
                                aFI_SaveAcc.Vahed,
                                aFI_SaveAcc.Deghat ?? 0,
                                aFI_SaveAcc.F01,
                                aFI_SaveAcc.F02,
                                aFI_SaveAcc.F03,
                                aFI_SaveAcc.F04,
                                aFI_SaveAcc.F05,
                                aFI_SaveAcc.F06,
                                aFI_SaveAcc.F07,
                                aFI_SaveAcc.F08,
                                aFI_SaveAcc.F09,
                                aFI_SaveAcc.F10,
                                aFI_SaveAcc.F11,
                                aFI_SaveAcc.F12,
                                aFI_SaveAcc.F13,
                                aFI_SaveAcc.F14,
                                aFI_SaveAcc.F15,
                                aFI_SaveAcc.F16,
                                aFI_SaveAcc.F17,
                                aFI_SaveAcc.F18,
                                aFI_SaveAcc.F19,
                                aFI_SaveAcc.F20);

            var dataAccount = UnitDatabase.ReadUserPassHeader(this.Request.Headers);
            string conStr = UnitDatabase.CreateConnectionString(dataAccount[0], dataAccount[1], dataAccount[2], ace, sal, group, 0, "", 0, 0);
            if (conStr.Length > 100)
            {
                ApiModel db = new ApiModel(conStr);
                var list = db.Database.SqlQuery<string>(sql);
                await db.SaveChangesAsync();
                return Ok(list);
            }
            return Ok(conStr);
        }


        // Get: api/AFI_DelAcc
        [Route("api/Web_Data/AFI_DelAcc/{ace}/{sal}/{group}/{AccCode}")]
        public async Task<IHttpActionResult> GetAFI_DelAcc(string ace, string sal, string group, string AccCode)
        {
            string sql = string.Format(@"DECLARE	@return_value int
                                                    EXEC	@return_value = [dbo].[Web_DelAcc]
		                                                    @Code = '{0}'
                                                 SELECT	'Return Value' = @return_value",
                                                AccCode);

            var dataAccount = UnitDatabase.ReadUserPassHeader(this.Request.Headers);
            string conStr = UnitDatabase.CreateConnectionString(dataAccount[0], dataAccount[1], dataAccount[2], ace, sal, group, 0, "", 0, 0);
            if (conStr.Length > 100)
            {
                ApiModel db = new ApiModel(conStr);
                var list = db.Database.SqlQuery<int>(sql);
                await db.SaveChangesAsync();
                return Ok(list);
            }
            return Ok(conStr);
        }





        // Get: api/Web_Data/ZGru لیست گروه زیر حساب ها 
        [Route("api/Web_Data/ZGru/{ace}/{sal}/{group}")]
        public async Task<IHttpActionResult> GetWeb_ZGru(string ace, string sal, string group)
        {
            string sql = string.Format("select * FROM  Web_ZGru");
            var dataAccount = UnitDatabase.ReadUserPassHeader(this.Request.Headers);
            string conStr = UnitDatabase.CreateConnectionString(dataAccount[0], dataAccount[1], dataAccount[2], ace, sal, group, 0, "", 0, 0);
            if (conStr.Length > 100)
            {
                ApiModel db = new ApiModel(conStr);
                var list = db.Database.SqlQuery<Web_ZGru>(sql);
                return Ok(list);
            }
            return Ok(conStr);
        }


        public class AFI_TestAcc
        {
            public string Code { get; set; }
        }


        public class TestAcc
        {
            public byte? Test { get; set; }

            public string TestName { get; set; }

            public string TestCap { get; set; }

            public int? BandNo { get; set; }
        }


        [Route("api/Web_Data/TestAcc/{ace}/{sal}/{group}")]
        [ResponseType(typeof(TestAcc))]
        public async Task<IHttpActionResult> PostWeb_TestAcc(string ace, string sal, string group, AFI_TestAcc AFI_TestAcc)
        {
            var dataAccount = UnitDatabase.ReadUserPassHeader(this.Request.Headers);
            string sql = string.Format(CultureInfo.InvariantCulture,
                                           @"EXEC	[dbo].[Web_TestAcc] @Code = '{0}'  , @UserCode = '{1}' ",
                                           AFI_TestAcc.Code,
                                           dataAccount[2]);

            string conStr = UnitDatabase.CreateConnectionString(dataAccount[0], dataAccount[1], dataAccount[2], ace, sal, group, 0, "", 0, 0);
            if (conStr.Length > 100)
            {
                ApiModel db = new ApiModel(conStr);
                var result = db.Database.SqlQuery<TestAcc>(sql);
                var list = JsonConvert.SerializeObject(result);
                return Ok(list);
            }
            return Ok(conStr);
        }




        public class TestAcc_DeleteObject
        {
            public string Code { get; set; }

        }

        public class TestAcc_Delete
        {
            public int id { get; set; }

            public byte Test { get; set; }

            public string TestName { get; set; }

            public string TestCap { get; set; }

            public int BandNo { get; set; }

        }



        [Route("api/Web_Data/TestAcc_Delete/{ace}/{sal}/{group}")]
        [ResponseType(typeof(TestAcc_Delete))]
        public async Task<IHttpActionResult> PostWeb_TestAcc_Delete(string ace, string sal, string group, TestAcc_DeleteObject TestAcc_DeleteObject)
        {
            var dataAccount = UnitDatabase.ReadUserPassHeader(this.Request.Headers);
            string sql = string.Format(CultureInfo.InvariantCulture,
                           @"EXEC	[dbo].[Web_TestAcc_Delete] @Code = '{0}', @UserCode = '{1}' ", TestAcc_DeleteObject.Code, dataAccount[2]);

            string conStr = UnitDatabase.CreateConnectionString(dataAccount[0], dataAccount[1], dataAccount[2], ace, sal, group, 0, "", 0, 0);
            if (conStr.Length > 100)
            {
                ApiModel db = new ApiModel(conStr);
                var result = db.Database.SqlQuery<TestAcc_Delete>(sql);
                var list = JsonConvert.SerializeObject(result);
                return Ok(list);
            }
            return Ok(conStr);
        }


        // GET: api/Web_Data/Vstr لیست ویزیتور ها
        [Route("api/Web_Data/Vstr/{ace}/{sal}/{group}")]
        public async Task<IHttpActionResult> GetWeb_Vstr(string ace, string sal, string group)
        {
            string sql = string.Format(@" select *  from Web_Vstr");
            var dataAccount = UnitDatabase.ReadUserPassHeader(this.Request.Headers);
            string conStr = UnitDatabase.CreateConnectionString(dataAccount[0], dataAccount[1], dataAccount[2], ace, sal, group, 0, "", 0, 0);
            if (conStr.Length > 100)
            {
                ApiModel db = new ApiModel(conStr);
                var list = db.Database.SqlQuery<Web_Vstr>(sql);
                return Ok(list);
            }
            return Ok(conStr);
        }

        // GET: api/Web_Data/AddMin لیست عوارض و تخفیف ها
        [Route("api/Web_Data/Web_AddMin/{ace}/{sal}/{group}")]
        public async Task<IHttpActionResult> GetWeb_AddMin(string ace, string sal, string group)
        {
            string sql = string.Format(@" select *  from Web_Addmin");
            var dataAccount = UnitDatabase.ReadUserPassHeader(this.Request.Headers);
            string conStr = UnitDatabase.CreateConnectionString(dataAccount[0], dataAccount[1], dataAccount[2], ace, sal, group, 0, "", 0, 0);
            if (conStr.Length > 100)
            {
                ApiModel db = new ApiModel(conStr);
                var list = db.Database.SqlQuery<Web_AddMin>(sql);
                return Ok(list);
            }
            return Ok(conStr);
        }


        public class CustAccountObject
        {
            public string LockNo { get; set; }

        }

        public class CustAccount
        {
            public byte TasviyeCode { get; set; }

            public string Tasviye { get; set; }

            public string ModeCode { get; set; }

            public string Status { get; set; }

            public long SerialNumber { get; set; }

            public string DocNo { get; set; }

            public double? SortDocNo { get; set; }

            public string DocDate { get; set; }

            public byte? PaymentType { get; set; }

            public string PaymentTypeSt { get; set; }

            public string CustCode { get; set; }

            public string Spec { get; set; }

            public double? TotalValue { get; set; }

            public string DownloadCount { get; set; }
        }

        [Route("api/Web_Data/CustAccount/{ace}/{sal}/{group}")]
        [ResponseType(typeof(CustAccount))]
        public async Task<IHttpActionResult> PostWeb_CustAccount(string ace, string sal, string group, CustAccountObject CustAccountObject)
        {
            string sql = string.Format(CultureInfo.InvariantCulture,
                                       @"EXEC	[dbo].[Web_CustAccount] @LockNo = '{0}' ", CustAccountObject.LockNo);

            var dataAccount = UnitDatabase.ReadUserPassHeader(this.Request.Headers);
            string conStr = UnitDatabase.CreateConnectionString(dataAccount[0], dataAccount[1], dataAccount[2], ace, sal, group, 0, "", 0, 0);
            if (conStr.Length > 100)
            {
                ApiModel db = new ApiModel(conStr);
                var list = db.Database.SqlQuery<CustAccount>(sql);
                return Ok(list);
            }
            return Ok(conStr);
        }


        public class CustAccountSaveObject
        {
            public string Year { get; set; }

            public long SerialNumber { get; set; }

            public string OnlineParLink { get; set; }

            public string DownloadCount { get; set; }
        }


        [Route("api/Web_Data/CustAccountSave/{ace}/{sal}/{group}")]
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PostWeb_CustAccountSave(string ace, string sal, string group, CustAccountSaveObject CustAccountSaveObject)
        {
            string sql = string.Format(CultureInfo.InvariantCulture, @"DECLARE @return_value int
                                                                           EXEC    @return_value = [dbo].[Web_CustAccountSave]
                                                                                   @Year = N'{0}',
                                                                                   @SerialNumber = {1}, ",
                                                                       CustAccountSaveObject.Year, CustAccountSaveObject.SerialNumber);


            if (CustAccountSaveObject.OnlineParLink != null)
                sql += string.Format(CultureInfo.InvariantCulture, " @OnlineParLink = N'''{0}''' ", CustAccountSaveObject.OnlineParLink);

            if (CustAccountSaveObject.DownloadCount != null)
                sql += string.Format(CultureInfo.InvariantCulture, " @DownloadCount = N'''{0}''' ", CustAccountSaveObject.DownloadCount);

            sql += " SELECT  'Return Value' = @return_value";

            var dataAccount = UnitDatabase.ReadUserPassHeader(this.Request.Headers);
            string conStr = UnitDatabase.CreateConnectionString(dataAccount[0], dataAccount[1], dataAccount[2], ace, sal, group, 0, "", 0, 0);
            if (conStr.Length > 100)
            {
                ApiModel db = new ApiModel(conStr);
                var list = db.Database.SqlQuery<int>(sql);
                return Ok(list);
            }
            return Ok(conStr);
        }



        public class FDocP_CustAcountObject
        {
            public string Year { get; set; }

            public long SerialNumber { get; set; }
        }


        [Route("api/Web_Data/FDocP_CustAcount/{ace}/{sal}/{group}")]
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PostWeb_FDocP_CustAcount(string ace, string sal, string group, FDocP_CustAcountObject FDocP_CustAcountObject)
        {
            string sql = string.Format(@"EXEC	 [dbo].[Web_FDocP]
		                                             @Year = N'{0}',
		                                             @SerialNumber = {1}",
                                     FDocP_CustAcountObject.Year,
                                     FDocP_CustAcountObject.SerialNumber
                          );

            var dataAccount = UnitDatabase.ReadUserPassHeader(this.Request.Headers);
            string conStr = UnitDatabase.CreateConnectionString(dataAccount[0], dataAccount[1], dataAccount[2], ace, sal, group, FDocP_CustAcountObject.SerialNumber, "", 0, 0);
            if (conStr.Length > 100)
            {
                ApiModel db = new ApiModel(conStr);
                var list = db.Database.SqlQuery<Web_FDocP>(sql);
                return Ok(list);
            }
            return Ok(conStr);
        }



        public class Web_Dictionary
        {
            public string Fa { get; set; }
            public string En { get; set; }
        }

        // GET: api/Web_Data/Web_Dictionary لیست وضعیت ارجاع  
        [Route("api/Web_Data/Web_Dictionary")]
        public async Task<IHttpActionResult> GetWeb_Dictionary()
        {
            string sql = string.Format(@"Select fa,en from Web_Dictionary");
            var dataAccount = UnitDatabase.ReadUserPassHeader(this.Request.Headers);
            string conStr = UnitDatabase.CreateConnectionString(dataAccount[0], dataAccount[1], dataAccount[2], "Config", "", "00", 0, "", 0, 0);
            if (conStr.Length > 100)
            {
                ApiModel db = new ApiModel(conStr);
                var list = db.Database.SqlQuery<Web_Dictionary>(sql);
                return Ok(list);
            }
            return Ok(conStr);
        }


        public class AFI_SaveMkz
        {
            public byte? BranchCode { get; set; }

            public string UserCode { get; set; }

            public string Code { get; set; }

            public string Name { get; set; }

            public string Spec { get; set; }

            public byte? Mode { get; set; }

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

        // post: api/AFI_SaveMkz
        [Route("api/Web_Data/AFI_SaveMkz/{ace}/{sal}/{group}")]
        public async Task<IHttpActionResult> PostAFI_SaveMkz(string ace, string sal, string group, AFI_SaveMkz aFI_SaveMkz)
        {
            string sql = string.Format(CultureInfo.InvariantCulture,
     @" DECLARE @oCode nvarchar(100)
                           EXEC	[dbo].[Web_SaveMkz]
		                        @BranchCode = {0},
		                        @UserCode = N'{1}',
		                        @Code = '{2}',
		                        @Name = N'{3}',
                                @Spec = N'{4}',
		                        @F01 = N'{5}',
		                        @F02 = N'{6}',
		                        @F03 = N'{7}',
		                        @F04 = N'{8}',
		                        @F05 = N'{9}',
		                        @F06 = N'{10}',
		                        @F07 = N'{11}',
		                        @F08 = N'{12}',
		                        @F09 = N'{13}',
		                        @F10 = N'{14}',
		                        @F11 = N'{15}',
		                        @F12 = N'{16}',
		                        @F13 = N'{17}',
		                        @F14 = N'{18}',
		                        @F15 = N'{19}',
		                        @F16 = N'{20}',
		                        @F17 = N'{21}',
		                        @F18 = N'{22}',
		                        @F19 = N'{23}',
		                        @F20 = N'{24}',
		                        @Mode = {25},
		                        @oCode = @oCode OUTPUT
                        SELECT	@oCode as N'@oCode' ",
                                 aFI_SaveMkz.BranchCode ?? 0,
                                 aFI_SaveMkz.UserCode,
                                 aFI_SaveMkz.Code,
                                 aFI_SaveMkz.Name ?? "",
                                 aFI_SaveMkz.Spec ?? "",
                                 aFI_SaveMkz.F01,
                                 aFI_SaveMkz.F02,
                                 aFI_SaveMkz.F03,
                                 aFI_SaveMkz.F04,
                                 aFI_SaveMkz.F05,
                                 aFI_SaveMkz.F06,
                                 aFI_SaveMkz.F07,
                                 aFI_SaveMkz.F08,
                                 aFI_SaveMkz.F09,
                                 aFI_SaveMkz.F10,
                                 aFI_SaveMkz.F11,
                                 aFI_SaveMkz.F12,
                                 aFI_SaveMkz.F13,
                                 aFI_SaveMkz.F14,
                                 aFI_SaveMkz.F15,
                                 aFI_SaveMkz.F16,
                                 aFI_SaveMkz.F17,
                                 aFI_SaveMkz.F18,
                                 aFI_SaveMkz.F19,
                                 aFI_SaveMkz.F20,
                                 aFI_SaveMkz.Mode);

            var dataAccount = UnitDatabase.ReadUserPassHeader(this.Request.Headers);
            string conStr = UnitDatabase.CreateConnectionString(dataAccount[0], dataAccount[1], dataAccount[2], ace, sal, group, 0, "", 0, 0);
            if (conStr.Length > 100)
            {
                ApiModel db = new ApiModel(conStr);
                var list = db.Database.SqlQuery<string>(sql);
                await db.SaveChangesAsync();
                return Ok(list);
            }
            return Ok(conStr);
        }


        // Get: api/AFI_DelMkz
        [Route("api/Web_Data/AFI_DelMkz/{ace}/{sal}/{group}/{MkzCode}")]
        public async Task<IHttpActionResult> GetAFI_DelMkz(string ace, string sal, string group, string MkzCode)
        {
            string sql = string.Format(@"DECLARE	@return_value int
                                                    EXEC	@return_value = [dbo].[Web_DelMkz]
		                                                    @Code = '{0}'
                                                 SELECT	'Return Value' = @return_value",
                            MkzCode);

            var dataAccount = UnitDatabase.ReadUserPassHeader(this.Request.Headers);
            string conStr = UnitDatabase.CreateConnectionString(dataAccount[0], dataAccount[1], dataAccount[2], ace, sal, group, 0, "", 0, 0);
            if (conStr.Length > 100)
            {
                ApiModel db = new ApiModel(conStr);
                var list = db.Database.SqlQuery<int>(sql);
                await db.SaveChangesAsync();
                return Ok(list);
            }
            return Ok(conStr);
        }


        public class AFI_TestMkz
        {
            public string Code { get; set; }
        }


        public class TestMkz
        {
            public byte? Test { get; set; }

            public string TestName { get; set; }

            public string TestCap { get; set; }

            public int? BandNo { get; set; }
        }


        [Route("api/Web_Data/TestMkz/{ace}/{sal}/{group}")]
        [ResponseType(typeof(TestMkz))]
        public async Task<IHttpActionResult> PostWeb_TestMkz(string ace, string sal, string group, AFI_TestMkz AFI_TestMkz)
        {
            var dataAccount = UnitDatabase.ReadUserPassHeader(this.Request.Headers);
            string sql = string.Format(CultureInfo.InvariantCulture,
                           @"EXEC [dbo].[Web_TestMkz] @Code = '{0}' , @UserCode = '{1}'",
                           AFI_TestMkz.Code,
                           dataAccount[2]);

            string conStr = UnitDatabase.CreateConnectionString(dataAccount[0], dataAccount[1], dataAccount[2], ace, sal, group, 0, "", 0, 0);
            if (conStr.Length > 100)
            {
                ApiModel db = new ApiModel(conStr);
                var result = db.Database.SqlQuery<TestMkz>(sql);
                var list = JsonConvert.SerializeObject(result);
                return Ok(list);
            }
            return Ok(conStr);
        }


        public class TestMkz_DeleteObject
        {
            public string Code { get; set; }

        }

        public class TestMkz_Delete
        {
            public int id { get; set; }

            public byte Test { get; set; }

            public string TestName { get; set; }

            public string TestCap { get; set; }

            public int BandNo { get; set; }

        }



        [Route("api/Web_Data/TestMkz_Delete/{ace}/{sal}/{group}")]
        [ResponseType(typeof(TestMkz_Delete))]
        public async Task<IHttpActionResult> PostWeb_TestMkz_Delete(string ace, string sal, string group, TestMkz_DeleteObject TestMkz_DeleteObject)
        {
            var dataAccount = UnitDatabase.ReadUserPassHeader(this.Request.Headers);
            string sql = string.Format(CultureInfo.InvariantCulture,
                                           @"EXEC	[dbo].[Web_TestMkz_Delete] @Code = '{0}', @UserCode = '{1}' ", TestMkz_DeleteObject.Code, dataAccount[2]);

            string conStr = UnitDatabase.CreateConnectionString(dataAccount[0], dataAccount[1], dataAccount[2], ace, sal, group, 0, "", 0, 0);
            if (conStr.Length > 100)
            {
                ApiModel db = new ApiModel(conStr);
                var result = db.Database.SqlQuery<TestMkz_Delete>(sql);
                var list = JsonConvert.SerializeObject(result);
                return Ok(list);
            }
            return Ok(conStr);
        }



        public class AFI_SaveOpr
        {
            public byte? BranchCode { get; set; }

            public string UserCode { get; set; }

            public string Code { get; set; }

            public string Name { get; set; }

            public string Spec { get; set; }

            public byte? Mode { get; set; }

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

        // post: api/AFI_SaveOpr
        [Route("api/Web_Data/AFI_SaveOpr/{ace}/{sal}/{group}")]
        public async Task<IHttpActionResult> PostAFI_SaveOpr(string ace, string sal, string group, AFI_SaveOpr aFI_SaveOpr)
        {
            string sql = string.Format(CultureInfo.InvariantCulture,
                       @" DECLARE @oCode nvarchar(100)
                           EXEC	[dbo].[Web_SaveOpr]
		                        @BranchCode = {0},
		                        @UserCode = N'{1}',
		                        @Code = '{2}',
		                        @Name = N'{3}',
                                @Spec = N'{4}',
		                        @F01 = N'{5}',
		                        @F02 = N'{6}',
		                        @F03 = N'{7}',
		                        @F04 = N'{8}',
		                        @F05 = N'{9}',
		                        @F06 = N'{10}',
		                        @F07 = N'{11}',
		                        @F08 = N'{12}',
		                        @F09 = N'{13}',
		                        @F10 = N'{14}',
		                        @F11 = N'{15}',
		                        @F12 = N'{16}',
		                        @F13 = N'{17}',
		                        @F14 = N'{18}',
		                        @F15 = N'{19}',
		                        @F16 = N'{20}',
		                        @F17 = N'{21}',
		                        @F18 = N'{22}',
		                        @F19 = N'{23}',
		                        @F20 = N'{24}',
		                        @Mode = {25},
		                        @oCode = @oCode OUTPUT
                        SELECT	@oCode as N'@oCode' ",
                       aFI_SaveOpr.BranchCode ?? 0,
                       aFI_SaveOpr.UserCode,
                       aFI_SaveOpr.Code,
                       aFI_SaveOpr.Name ?? "",
                       aFI_SaveOpr.Spec ?? "",
                       aFI_SaveOpr.F01,
                       aFI_SaveOpr.F02,
                       aFI_SaveOpr.F03,
                       aFI_SaveOpr.F04,
                       aFI_SaveOpr.F05,
                       aFI_SaveOpr.F06,
                       aFI_SaveOpr.F07,
                       aFI_SaveOpr.F08,
                       aFI_SaveOpr.F09,
                       aFI_SaveOpr.F10,
                       aFI_SaveOpr.F11,
                       aFI_SaveOpr.F12,
                       aFI_SaveOpr.F13,
                       aFI_SaveOpr.F14,
                       aFI_SaveOpr.F15,
                       aFI_SaveOpr.F16,
                       aFI_SaveOpr.F17,
                       aFI_SaveOpr.F18,
                       aFI_SaveOpr.F19,
                       aFI_SaveOpr.F20,
                       aFI_SaveOpr.Mode);
            var dataAccount = UnitDatabase.ReadUserPassHeader(this.Request.Headers);
            string conStr = UnitDatabase.CreateConnectionString(dataAccount[0], dataAccount[1], dataAccount[2], ace, sal, group, 0, "", 0, 0);
            if (conStr.Length > 100)
            {
                ApiModel db = new ApiModel(conStr);
                var list = db.Database.SqlQuery<string>(sql);
                await db.SaveChangesAsync();
                return Ok(list);
            }
            return Ok(conStr);
        }


        // Get: api/AFI_DelOpr
        [Route("api/Web_Data/AFI_DelOpr/{ace}/{sal}/{group}/{OprCode}")]
        public async Task<IHttpActionResult> GetAFI_DelOpr(string ace, string sal, string group, string OprCode)
        {
            string sql = string.Format(@"DECLARE	@return_value int
                                                    EXEC	@return_value = [dbo].[Web_DelOpr]
		                                                    @Code = '{0}'
                                                 SELECT	'Return Value' = @return_value",
                                                  OprCode);

            var dataAccount = UnitDatabase.ReadUserPassHeader(this.Request.Headers);
            string conStr = UnitDatabase.CreateConnectionString(dataAccount[0], dataAccount[1], dataAccount[2], ace, sal, group, 0, "", 0, 0);
            if (conStr.Length > 100)
            {
                ApiModel db = new ApiModel(conStr);
                var list = db.Database.SqlQuery<int>(sql);
                await db.SaveChangesAsync();
                return Ok(list);
            }
            return Ok(conStr);
        }


        public class AFI_TestOpr
        {
            public string Code { get; set; }
        }


        public class TestOpr
        {
            public byte? Test { get; set; }

            public string TestName { get; set; }

            public string TestCap { get; set; }

            public int? BandNo { get; set; }
        }


        [Route("api/Web_Data/TestOpr/{ace}/{sal}/{group}")]
        [ResponseType(typeof(TestOpr))]
        public async Task<IHttpActionResult> PostWeb_TestOpr(string ace, string sal, string group, AFI_TestOpr AFI_TestOpr)
        {
            var dataAccount = UnitDatabase.ReadUserPassHeader(this.Request.Headers);
            string sql = string.Format(CultureInfo.InvariantCulture,
                                       @"EXEC	[dbo].[Web_TestOpr] @Code = '{0}'  , @UserCode = '{1}' ",
                                       AFI_TestOpr.Code,
                                       dataAccount[2]);

            string conStr = UnitDatabase.CreateConnectionString(dataAccount[0], dataAccount[1], dataAccount[2], ace, sal, group, 0, "", 0, 0);
            if (conStr.Length > 100)
            {
                ApiModel db = new ApiModel(conStr);
                var result = db.Database.SqlQuery<TestOpr>(sql);
                var list = JsonConvert.SerializeObject(result);
                return Ok(list);
            }
            return Ok(conStr);
        }


        public class TestOpr_DeleteObject
        {
            public string Code { get; set; }

        }

        public class TestOpr_Delete
        {
            public int id { get; set; }

            public byte Test { get; set; }

            public string TestName { get; set; }

            public string TestCap { get; set; }

            public int BandNo { get; set; }

        }



        [Route("api/Web_Data/TestOpr_Delete/{ace}/{sal}/{group}")]
        [ResponseType(typeof(TestOpr_Delete))]
        public async Task<IHttpActionResult> PostWeb_TestOpr_Delete(string ace, string sal, string group, TestOpr_DeleteObject TestOpr_DeleteObject)
        {
            var dataAccount = UnitDatabase.ReadUserPassHeader(this.Request.Headers);
            string sql = string.Format(CultureInfo.InvariantCulture,
                            @"EXEC	[dbo].[Web_TestOpr_Delete] @Code = '{0}', @UserCode = '{1}' ", TestOpr_DeleteObject.Code, dataAccount[2]);

            string conStr = UnitDatabase.CreateConnectionString(dataAccount[0], dataAccount[1], dataAccount[2], ace, sal, group, 0, "", 0, 0);
            if (conStr.Length > 100)
            {
                ApiModel db = new ApiModel(conStr);
                var result = db.Database.SqlQuery<TestOpr_Delete>(sql);
                var list = JsonConvert.SerializeObject(result);
                return Ok(list);
            }
            return Ok(conStr);
        }




        public class RprtCols_New
        {
            public string dataField { get; set; }

            public string caption { get; set; }

            public int? width { get; set; }

            public int? Type { get; set; }

        }

        // GET: api/Web_Data/RprtCols_New لیست ستونها
        [Route("api/Web_Data/RprtCols_New/{ace}/{sal}/{group}/{RprtId}/{UserCode}")]
        public async Task<IHttpActionResult> GetRprtCols_New(string ace, string sal, string group, string RprtId, string UserCode)
        {
            string sql = string.Format(@"exec Web_RprtCols_New @RprtId = '{0}' , @UserCode = '{1}' ", RprtId, UserCode);

            var dataAccount = UnitDatabase.ReadUserPassHeader(this.Request.Headers);
            string conStr = UnitDatabase.CreateConnectionString(dataAccount[0], dataAccount[1], dataAccount[2], ace, sal, group, 0, "", 0, 0);
            if (conStr.Length > 100)
            {
                ApiModel db = new ApiModel(conStr);
                var list = db.Database.SqlQuery<RprtCols_New>(sql);
                return Ok(list);
            }
            return Ok(conStr);
        }





        public class V_Del_ADocObject
        {
            public long serialNumber { get; set; }

        }



        [Route("api/Web_Data/V_Del_ADoc/{ace}/{sal}/{group}")]
        public async Task<IHttpActionResult> PostWeb_V_Del_ADoc(string ace, string sal, string group, V_Del_ADocObject V_Del_ADocObject)
        {
            var dataAccount = UnitDatabase.ReadUserPassHeader(this.Request.Headers);
            string sql = string.Format(CultureInfo.InvariantCulture,
                       @"DECLARE	@return_value int

                        EXEC	@return_value = [dbo].[Web_SaveADoc_Del_Temp]
		                        @serialNumber = {0},
		                        @UserCode = '{1}'
                        SELECT	'Return Value' = @return_value", V_Del_ADocObject.serialNumber, dataAccount[2]);

            string conStr = UnitDatabase.CreateConnectionString(dataAccount[0], dataAccount[1], dataAccount[2], ace, sal, group, 0, "", 0, 0);
            if (conStr.Length > 100)
            {
                ApiModel db = new ApiModel(conStr);
                var list = db.Database.SqlQuery<int>(sql);
                return Ok(list);
            }
            return Ok(conStr);
        }



        [DllImport("kernel32.dll", EntryPoint = "LoadLibrary")]
        static extern int LoadLibrary([MarshalAs(UnmanagedType.LPStr)] string lpLibFileName);

        [DllImport("kernel32.dll", EntryPoint = "GetProcAddress")]
        static extern IntPtr GetProcAddress(int hModule, [MarshalAs(UnmanagedType.LPStr)] string lpProcName);

        [DllImport("kernel32.dll", EntryPoint = "FreeLibrary")]
        static extern bool FreeLibrary(int hModule);

        [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Unicode)]
        delegate bool GetVer(StringBuilder RetVal);



        public static string CallGetVer()
        {
            string dllName = HttpContext.Current.Server.MapPath("~/Content/Dll/Acc6_Web.dll");
            const string functionName = "GetVer";

            int libHandle = LoadLibrary(dllName);
            if (libHandle == 0)
                return string.Format("Could not load library \"{0}\"", dllName);
            try
            {
                var delphiFunctionAddress = GetProcAddress(libHandle, functionName);
                if (delphiFunctionAddress == IntPtr.Zero)
                    return string.Format("Can't find function \"{0}\" in library \"{1}\"", functionName, dllName);

                var delphiFunction = (GetVer)Marshal.GetDelegateForFunctionPointer(delphiFunctionAddress, typeof(GetVer));

                StringBuilder RetVal = new StringBuilder(1024);
                delphiFunction(RetVal);
                return RetVal.ToString();
            }
            finally
            {
                FreeLibrary(libHandle);
            }
        }

        [Route("api/Web_Data/GetVerDll")]
        public async Task<IHttpActionResult> GetVerDll()
        {
            try
            {
                return Ok(CallGetVer());
            }
            catch (Exception e)
            {
                return Ok(e.Message.ToString());
                throw;
            }

        }







        public class LogXObject
        {
            public string ProgName_ { get; set; }
            public string IP_ { get; set; }
            public string GroupNo_ { get; set; }
            public Int16 Year_ { get; set; }
            public byte EditMode_ { get; set; }
            public byte LogMode_ { get; set; }
            public string Code_ { get; set; }
            public string DocNo_ { get; set; }
            public long SerialNumber_ { get; set; }

        }


        [Route("api/Web_Data/LogX")]
        public async Task<IHttpActionResult> PostWeb_LogX(LogXObject LogXObject)
        {
            var dataAccount = UnitDatabase.ReadUserPassHeader(this.Request.Headers);
            string sql = string.Format(CultureInfo.InvariantCulture,
                    @"DECLARE	@return_value int
                    EXEC	@return_value = Web_SaveLogX
		                    @ProgName_ = '{0}',
		                    @IP_ = '{1}',
		                    @UserCode_ = '{2}',
		                    @GroupNo_ = {3},
		                    @Year_ = {4},
		                    @EditMode_ = {5},
		                    @LogMode_ = {6},
		                    @Code_ = '{7}',
		                    @DocNo_ = '{8}',
		                    @SerialNumber_ = {9}
                    SELECT	'Return Value' = @return_value",
                    LogXObject.ProgName_,
                    LogXObject.IP_,
                    dataAccount[2],
                    LogXObject.GroupNo_,
                    LogXObject.Year_,
                    LogXObject.EditMode_,
                    LogXObject.LogMode_,
                    LogXObject.Code_,
                    LogXObject.DocNo_,
                    LogXObject.SerialNumber_
                   );

            string conStr = UnitDatabase.CreateConnectionString(dataAccount[0], dataAccount[1], dataAccount[2], "Config", "", "00", 0, "", 0, 0);
            if (conStr.Length > 100)
            {
                ApiModel db = new ApiModel(conStr);
                var list = db.Database.SqlQuery<int>(sql);
                return Ok(list);
            }
            return Ok(conStr);
        }




        public class V_Del_FDocObject
        {
            public long serialNumber { get; set; }

        }


        [Route("api/Web_Data/V_Del_FDoc/{ace}/{sal}/{group}")]
        public async Task<IHttpActionResult> PostWeb_V_Del_FDoc(string ace, string sal, string group, V_Del_FDocObject V_Del_FDocObject)
        {
            var dataAccount = UnitDatabase.ReadUserPassHeader(this.Request.Headers);
            string sql = string.Format(CultureInfo.InvariantCulture,
                       @"DECLARE	@return_value int

                        EXEC	@return_value = [dbo].[Web_SaveFDoc_Del_Temp]
		                        @serialNumber = {0},
		                        @UserCode = '{1}'
                        SELECT	'Return Value' = @return_value", V_Del_FDocObject.serialNumber, dataAccount[2]);

            string conStr = UnitDatabase.CreateConnectionString(dataAccount[0], dataAccount[1], dataAccount[2], ace, sal, group, 0, "", 0, 0);
            if (conStr.Length > 100)
            {
                ApiModel db = new ApiModel(conStr);
                var list = db.Database.SqlQuery<int>(sql);
                return Ok(list);
            }
            return Ok(conStr);
        }


        public class V_Del_IDocObject
        {
            public long serialNumber { get; set; }

        }


        [Route("api/Web_Data/V_Del_IDoc/{ace}/{sal}/{group}")]
        public async Task<IHttpActionResult> PostWeb_V_Del_IDoc(string ace, string sal, string group, V_Del_IDocObject V_Del_IDocObject)
        {
            var dataAccount = UnitDatabase.ReadUserPassHeader(this.Request.Headers);
            string sql = string.Format(CultureInfo.InvariantCulture,
                       @"DECLARE	@return_value int

                        EXEC	@return_value = [dbo].[Web_SaveIDoc_Del_Temp]
		                        @serialNumber = {0},
		                        @UserCode = '{1}'
                        SELECT	'Return Value' = @return_value", V_Del_IDocObject.serialNumber, dataAccount[2]);

            string conStr = UnitDatabase.CreateConnectionString(dataAccount[0], dataAccount[1], dataAccount[2], ace, sal, group, 0, "", 0, 0);
            if (conStr.Length > 100)
            {
                ApiModel db = new ApiModel(conStr);
                var list = db.Database.SqlQuery<int>(sql);
                return Ok(list);
            }
            return Ok(conStr);
        }



        [Route("api/Web_Data/Web_UnitName/{ace}/{sal}/{group}")]
        public async Task<IHttpActionResult> GetWeb_UnitName(string ace, string sal, string group)
        {
            string sql = string.Format(@"select distinct '' as KalaCode, 0 as Code,  Name from (
                                             select distinct UnitName1 as Name from Web_Kala
                                             union all
                                             select distinct UnitName2 as Name from Web_Kala
                                             union all
                                             select distinct UnitName3 as Name from Web_Kala) as list");

            var dataAccount = UnitDatabase.ReadUserPassHeader(this.Request.Headers);
            string conStr = UnitDatabase.CreateConnectionString(dataAccount[0], dataAccount[1], dataAccount[2], ace, sal, group, 0, "", 0, 0);
            if (conStr.Length > 100)
            {
                ApiModel db = new ApiModel(conStr);
                var list = db.Database.SqlQuery<Web_Unit>(sql);
                return Ok(list);
            }
            return Ok(conStr);
        }

    }
}