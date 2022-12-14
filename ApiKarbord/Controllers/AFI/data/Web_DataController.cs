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

        // Post: api/Web_Data/Cust لیست اشخاص
        [Route("api/Web_Data/Cust/{ace}/{sal}/{group}")]
        public async Task<IHttpActionResult> PostWeb_Cust(string ace, string sal, string group, CustObject custObject)
        {
            var dataAccount = UnitDatabase.ReadUserPassHeader(this.Request.Headers);

            string con = UnitDatabase.CreateConection(dataAccount[0], dataAccount[1], dataAccount[2], ace, sal, group, 0, "", 0, 0);
            if (con == "ok")
            {
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

                var listCust = UnitDatabase.db.Database.SqlQuery<Web_Cust>(sql);
                return Ok(listCust);
            }
            return Ok(con);
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
            var dataAccount = UnitDatabase.ReadUserPassHeader(this.Request.Headers);
            string con = UnitDatabase.CreateConection(dataAccount[0], dataAccount[1], dataAccount[2], ace, sal, group, 0, "", 0, 0);
            if (con == "ok")
            {


                string sql = @"SELECT Eghdam, EghdamDate, UpdateUser, UpdateDate, Code, Name,SortName, Spec, UnitName1, UnitName2, UnitName3, Zarib1, Zarib2, Zarib3, FanniNo
                                   , F01, F02, F03, F04, F05, F06, F07, F08, F09, F10, F11, F12, F13, F14, F15, F16, F17, F18, F19, F20, DeghatR1, DeghatR2
                                   , DeghatR3, DeghatM1, DeghatM2, DeghatM3, PPrice1, PPrice2, PPrice3, SPrice1, SPrice2, SPrice3,BarCode,DefaultUnit";
                if (kalaObject.withimage == true)
                    sql += ",KalaImage ";
                else
                    sql += ",null as KalaImage ";

                sql += string.Format(" FROM Web_Kala_f({0},'{1}') where 1 = 1 ", kalaObject.Mode, kalaObject.UserCode);


                /* if (kalaObject.withimage == true)
                 {
                     sql = @"SELECT Eghdam, EghdamDate, UpdateUser, UpdateDate, Code, Name, Spec, UnitName1, UnitName2, UnitName3, Zarib1, Zarib2, Zarib3, FanniNo
                                    , F01, F02, F03, F04, F05, F06, F07, F08, F09, F10, F11, F12, F13, F14, F15, F16, F17, F18, F19, F20, DeghatR1, DeghatR2
                                    , DeghatR3, DeghatM1, DeghatM2, DeghatM3, PPrice1, PPrice2, PPrice3, SPrice1, SPrice2, SPrice3, KalaImage
                             FROM Web_Kala_f(0,'')";
                 }
                 else
                 {
                     sql = @"SELECT Eghdam, EghdamDate, UpdateUser, UpdateDate, Code, Name, Spec, UnitName1, UnitName2, UnitName3, Zarib1, Zarib2, Zarib3, FanniNo
                                    , F01, F02, F03, F04, F05, F06, F07, F08, F09, F10, F11, F12, F13, F14, F15, F16, F17, F18, F19, F20, DeghatR1, DeghatR2
                                    , DeghatR3, DeghatM1, DeghatM2, DeghatM3, PPrice1, PPrice2, PPrice3, SPrice1, SPrice2, SPrice3, null as KalaImage
                             FROM Web_Kala_f(0,'')";
                 }*/

                if (kalaObject.updatedate != null)
                    sql += " and updatedate >= CAST('" + kalaObject.updatedate + "' AS DATETIME2)";

                var listKala = UnitDatabase.db.Database.SqlQuery<Web_Kala>(sql);
                return Ok(listKala);

            }
            return Ok(con);
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
            var dataAccount = UnitDatabase.ReadUserPassHeader(this.Request.Headers);

            string con = UnitDatabase.CreateConection(dataAccount[0], dataAccount[1], dataAccount[2], ace, sal, group, 0, "", 0, 0);
            if (con == "ok")
            {
                //string sql = string.Format("select  * FROM  Web_CGru_F({0},'{1}') where mode = 0 or mode = {2}", cGruObject.Mode, cGruObject.UserCode, cGruObject.ModeGru);
                string sql = string.Format("select  * FROM  Web_CGru_F({0},'{1}')", cGruObject.Mode, cGruObject.UserCode);
                var listCGru = UnitDatabase.db.Database.SqlQuery<Web_CGru>(sql);
                return Ok(listCGru);
            }
            return Ok(con);
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
            var dataAccount = UnitDatabase.ReadUserPassHeader(this.Request.Headers);

            string con = UnitDatabase.CreateConection(dataAccount[0], dataAccount[1], dataAccount[2], ace, sal, group, 0, "", 0, 0);
            if (con == "ok")
            {
                string sql = string.Format("select  * FROM  Web_Acc_F({0},'{1}') where 1 = 1 order by SortCode ", accObject.Mode, accObject.UserCode);
                var listAcc = UnitDatabase.db.Database.SqlQuery<Web_Acc>(sql);
                return Ok(listAcc);
            }
            return Ok(con);
        }

        // GET: api/Web_Data/ZAcc لیست زیر حساب ها
        [Route("api/Web_Data/ZAcc/{ace}/{sal}/{group}/{filter}")]
        public async Task<IHttpActionResult> GetWeb_ZAcc(string ace, string sal, string group, string filter)
        {
            var dataAccount = UnitDatabase.ReadUserPassHeader(this.Request.Headers);
            string con = UnitDatabase.CreateConection(dataAccount[0], dataAccount[1], dataAccount[2], ace, sal, group, 0, "", 0, 0);
            if (con == "ok")
            {
                string sql;
                if (filter == "null" || filter == "0")
                    sql = string.Format(@" select *  from Web_ZAcc");
                else
                    sql = string.Format(@" select *  from Web_ZAcc where ZGruCode in ({0})", filter);
                var listDB = UnitDatabase.db.Database.SqlQuery<Web_ZAcc>(sql).ToList();
                return Ok(listDB);
            }
            return Ok(con);
        }



        // GET: api/Web_Data/KalaPrice لیست گروه قیمت خرید و فروش
        [Route("api/Web_Data/KalaPrice/{ace}/{sal}/{group}/{insert}")]
        public IQueryable<Web_KalaPrice> GetWeb_KalaPrice(string ace, string sal, string group, bool insert)
        {
            var dataAccount = UnitDatabase.ReadUserPassHeader(this.Request.Headers);
            string con = UnitDatabase.CreateConection(dataAccount[0], dataAccount[1], dataAccount[2], ace, sal, group, 0, "", 0, 0);
            if (con == "ok")
            {
                if (insert)
                    return UnitDatabase.db.Web_KalaPrice.Where(c => c.Cancel == false);
                else
                    return UnitDatabase.db.Web_KalaPrice;
            }
            return null;
        }




        // GET: api/Web_Data/KalaPriceB  لیست قیمت کالا بر اساس قیمت گروه
        [Route("api/Web_Data/KalaPriceB/{ace}/{sal}/{group}/{code}/{kalacode}")]
        public IQueryable<Web_KalaPriceB> GetWeb_KalaPriceB(string ace, string sal, string group, int code, string kalacode)
        {
            var dataAccount = UnitDatabase.ReadUserPassHeader(this.Request.Headers);
            string con = UnitDatabase.CreateConection(dataAccount[0], dataAccount[1], dataAccount[2], ace, sal, group, 0, "", 0, 0);
            if (con == "ok")
            {
                var list = UnitDatabase.db.Web_KalaPriceB.Where(c => c.Code == code && c.KalaCode == kalacode);
                return list;
            }
            return null;
        }


        // GET: api/Web_Data/Unit لیست واحد کالا
        [Route("api/Web_Data/Unit/{ace}/{sal}/{group}/{codekala}")]
        public IQueryable<Web_Unit> GetWeb_Unit(string ace, string sal, string group, string codeKala)
        {
            var dataAccount = UnitDatabase.ReadUserPassHeader(this.Request.Headers);
            string con = UnitDatabase.CreateConection(dataAccount[0], dataAccount[1], dataAccount[2], ace, sal, group, 0, "", 0, 0);
            if (con == "ok")
            {
                var a = from p in UnitDatabase.db.Web_Unit where p.KalaCode == codeKala && p.Name != "" select p;
                return a;
            }
            return null;
        }




        // GET: api/Web_Data/Inv لیست انبار ها
        [Route("api/Web_Data/Inv/{ace}/{sal}/{group}/{Mode}/{UserCode}")]
        public async Task<IHttpActionResult> GetWeb_Inv(string ace, string sal, string group, int Mode, string UserCode)
        {
            var dataAccount = UnitDatabase.ReadUserPassHeader(this.Request.Headers);
            string con = UnitDatabase.CreateConection(dataAccount[0], dataAccount[1], dataAccount[2], ace, sal, group, 0, "", 0, 0);
            if (con == "ok")
            {
                string sql = string.Format(@"select * from Web_Inv_F({0},'{1}')",
                                           Mode,
                                           UserCode);
                var listInv = UnitDatabase.db.Database.SqlQuery<Web_Inv>(sql);
                return Ok(listInv);
            }
            return Ok(con);
        }

        // GET: api/Web_Data/Param لیست پارامتر ها  
        [Route("api/Web_Data/Param/{ace}/{sal}/{group}")]
        public IQueryable<Web_Param> GetWeb_Param(string ace, string sal, string group)
        {
            var dataAccount = UnitDatabase.ReadUserPassHeader(this.Request.Headers);
            string con = UnitDatabase.CreateConection(dataAccount[0], dataAccount[1], dataAccount[2], ace, sal, group, 0, "Param", 0, 0);
            if (con == "ok")
            {
                return UnitDatabase.db.Web_Param;
            }
            return null;
        }

        // GET: api/Web_Data/Payment لیست نحوه پرداخت  
        [Route("api/Web_Data/Payment/{ace}/{sal}/{group}")]
        public IQueryable<Web_Payment> GetWeb_Payment(string ace, string sal, string group)
        {
            var dataAccount = UnitDatabase.ReadUserPassHeader(this.Request.Headers);
            string con = UnitDatabase.CreateConection(dataAccount[0], dataAccount[1], dataAccount[2], ace, sal, group, 0, "", 0, 0);
            if (con == "ok")
            {
                return UnitDatabase.db.Web_Payment.OrderBy(c => c.OrderFld);
            }
            return null;
        }

        // GET: api/Web_Data/Status لیست وضعیت پرداخت  
        [Route("api/Web_Data/Status/{ace}/{sal}/{group}/{progname}")]
        public IQueryable<Web_Status> GetWeb_Status(string ace, string sal, string group, string progname)
        {
            var dataAccount = UnitDatabase.ReadUserPassHeader(this.Request.Headers);
            string con = UnitDatabase.CreateConection(dataAccount[0], dataAccount[1], dataAccount[2], ace, sal, group, 0, "", 0, 0);
            if (con == "ok")
            {
                var list = UnitDatabase.db.Web_Status.Where(c => c.Prog == progname);
                return list;
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
        }

        // Post: api/Web_Data/AddMin لیست کسورات و افزایشات   
        // [Route("api/Web_Data/AddMin/{ace}/{sal}/{group}/{forSale}/{serialNumber}/{custCode}/{TypeJob}/{Spec1}/{Spec2}/{Spec3}/{Spec4}/{Spec5}/{Spec6}/{Spec7}/{Spec8}/{Spec9}/{Spec10}/{MP1}/{MP2}/{MP3}/{MP4}/{MP5}/{MP6}/{MP7}/{MP8}/{MP9}/{MP10}/")]
        [ResponseType(typeof(CalcAddmin))]
        [Route("api/Web_Data/AddMin/{ace}/{sal}/{group}")]

        public async Task<IHttpActionResult> PostGetAddMin(string ace, string sal, string group, CalcAddmin calcAddmin)

        //
        //string spec1, string spec2, string spec3, string spec4, string spec5, string spec6, string spec7, string spec8, string spec9, string spec10,
        //string MP1, string MP2, string MP3, string MP4, string MP5, string MP6, string MP7, string MP8, string MP9, string MP10)
        {
            var dataAccount = UnitDatabase.ReadUserPassHeader(this.Request.Headers);
            string con = UnitDatabase.CreateConection(dataAccount[0], dataAccount[1], dataAccount[2], ace, sal, group, calcAddmin.serialNumber, "", 0, 0);
            if (con == "ok")
            {
                string sql = string.Format(CultureInfo.InvariantCulture, @"EXEC	[dbo].[Web_Calc_AddMin_EffPrice]
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
                                                    calcAddmin.MP10 ?? 0);
                var result = UnitDatabase.db.Database.SqlQuery<AddMin>(sql).Where(c => c.Name != "").ToList();
                var jsonResult = JsonConvert.SerializeObject(result);
                return Ok(jsonResult);
            }
            return Ok(con);
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
            var dataAccount = UnitDatabase.ReadUserPassHeader(this.Request.Headers);

            string con = UnitDatabase.CreateConection(dataAccount[0], dataAccount[1], dataAccount[2], ace, sal, group, 0, "", 0, 0);
            if (con == "ok")
            {
                string sql = string.Format("select  * FROM  Web_Thvl_F({0},'{1}')", thvlObject.Mode, thvlObject.UserCode);
                var listThvl = UnitDatabase.db.Database.SqlQuery<Web_Thvl>(sql);
                return Ok(listThvl);
            }
            return Ok(con);
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
            var dataAccount = UnitDatabase.ReadUserPassHeader(this.Request.Headers);

            string con = UnitDatabase.CreateConection(dataAccount[0], dataAccount[1], dataAccount[2], ace, sal, group, 0, "", 0, 0);
            if (con == "ok")
            {
                string sql = string.Format("select  * FROM  Web_TGru_F({0},'{1}')", tGruObject.Mode, tGruObject.UserCode);
                var listTGru = UnitDatabase.db.Database.SqlQuery<Web_TGru>(sql);
                return Ok(listTGru);
            }
            return Ok(con);
        }


        // GET: api/Web_Data/ اطلاعات لاگین   
        [Route("api/Web_Data/Login/{user}/{pass}/{param1}/{param2}")]
        public async Task<IHttpActionResult> GetWeb_Login(string user, string pass, string param1, string param2)
        {
            var dataAccount = UnitDatabase.ReadUserPassHeader(this.Request.Headers);
            string con = UnitDatabase.CreateConection(dataAccount[0], dataAccount[1], user, "Config", "", "00", 0, "", 0, 0);
            if (con == "ok")
            {
                if (pass == "null")
                    pass = "";
                string sql = string.Format(@" DECLARE  @return_value int, @name nvarchar(100)
                                              EXEC     @return_value = [dbo].[Web_Login]
                                                       @Code1 = '{0}',
		                                               @UserCode = N'{1}',
                                                       @Code2 = '{2}',
		                                               @Psw = N'{3}',
                                                       @Name = @name OUTPUT
                                              SELECT   'Return Value' = CONVERT(nvarchar, @return_value) + '-' +  @Name ",
                                              param1, user, param2, pass);

                string value = UnitDatabase.db.Database.SqlQuery<string>(sql).Single();
                string[] values = value.Split('-');
                if (values[0] == "1")
                    return Ok(value);
                else
                    return Ok(0);
            }
            return Ok(con);
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
        }



        // Post: api/Web_Data/ تست وجود کاربر    
        [Route("api/Web_Data/LoginTest")]
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PostWeb_LoginTest(LoginTestObject LoginTestObject)
        {
            var dataAccount = UnitDatabase.ReadUserPassHeader(this.Request.Headers);
            string con = UnitDatabase.CreateConection(dataAccount[0], dataAccount[1], dataAccount[2], "Config", "", "", 0, "", 0, 0);
            if (con == "ok")
            {
                try
                {

                    PersianCalendar pc = new System.Globalization.PersianCalendar();

                    string year = pc.GetYear(DateTime.Now).ToString();
                    string month = pc.GetMonth(DateTime.Now).ToString();
                    string day = pc.GetDayOfMonth(DateTime.Now).ToString();

                    month = month.Length == 1 ? "0" + month : month;
                    day = day.Length == 1 ? "0" + day : day;

                    string PDate = year + "/" + month + "/" + day;

                    string sql = string.Format("SELECT count(ID) as id FROM Ace_Config.dbo.UserIn WHERE ProgName IN ('Web1', 'Web2', 'Web8') and usercode <> '{0}'", LoginTestObject.UserCode);
                    string countUserIn = UnitDatabase.db.Database.SqlQuery<int>(sql).First().ToString();
                    var list = UnitDatabase.model.First();
                    int userCount = list.userCount ?? 0;
                    if (Int32.Parse(countUserIn) >= userCount)
                    {
                        return Ok("MaxCount");
                    }

                    //string PDate = string.Format("{0:yyyy/MM/dd}", Convert.ToDateTime(pc.GetYear(DateTime.Now).ToString() + "/" + pc.GetMonth(DateTime.Now).ToString() + "/" + pc.GetDayOfMonth(DateTime.Now).ToString()));
                    string Time = DateTime.Now.ToString("HH:mm");
                    sql = string.Format(@" EXEC	[dbo].[Web_TestLogin]
		                                            @MachineId = N'{0}',
		                                            @IPWan = N'{1}',
		                                            @Country = N'{2}',
		                                            @City = N'{3}',
		                                            @UserCode = N'{4}',
		                                            @LoginTimeas = N'{5}',
		                                            @LoginDate = N'{6}',
		                                            @ProgName = N'{7}',
		                                            @ProgVer = N'{8}',
		                                            @ProgCaption = N'{9}',
		                                            @FlagTest = {10}",
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
                                                  LoginTestObject.FlagTest
                                                 );

                    var value = UnitDatabase.db.Database.SqlQuery<Web_LoginTestObject>(sql).Single();





                    return Ok(value);

                }
                catch (Exception e)
                {
                    throw;
                }
            }
            return Ok(con);
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
            var dataAccount = UnitDatabase.ReadUserPassHeader(this.Request.Headers);
            string con = UnitDatabase.CreateConection(dataAccount[0], dataAccount[1], dataAccount[2], "Config", "", "", 0, "", 0, 0);
            if (con == "ok")
            {
                try
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
                    int value = UnitDatabase.db.Database.SqlQuery<int>(sql).Single();
                    return Ok("Ok");
                }
                catch (Exception e)
                {
                    throw;
                }
            }
            return Ok(con);
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
            var dataAccount = UnitDatabase.ReadUserPassHeader(this.Request.Headers);
            string con = UnitDatabase.CreateConection(dataAccount[0], dataAccount[1], dataAccount[2], "Config", "", "", 0, "", 0, 0);
            if (con == "ok")
            {
                //string sql = string.Format(@" select SUBSTRING(name,11,4) as name  from sys.sysdatabases where name like 'ACE_{0}%' and SUBSTRING(name,9,2) like '%{1}' order by name", ace, group);
                string sql = string.Format(@" EXEC	[dbo].[Web_Years]
		                                            @BaseProg = '{0}',
		                                            @GroupNo = '{1}',
		                                            @UserCode = '{2}'",
                                           DatabseSalObject.ProgName,
                                           DatabseSalObject.Group,
                                           DatabseSalObject.UserCode);
                var listDB = UnitDatabase.db.Database.SqlQuery<DatabseSal>(sql).ToList();
                return Ok(listDB);
            }
            return Ok(con);
        }





        // دریافت اطلاعات سطح دسترسی کاربر
        public class AccessUser
        {
            //public string Code { get; set; }
            //public string ProgName { get; set; }
            //public int GroupNo { get; set; }
            public string TrsName { get; set; }
        }

        [Route("api/Web_Data/AccessUser/{ace}/{group}/{user}")]
        public async Task<IHttpActionResult> GetWeb_AccessUser(string ace, string group, string user)
        {
            var dataAccount = UnitDatabase.ReadUserPassHeader(this.Request.Headers);
            string con = UnitDatabase.CreateConection(dataAccount[0], dataAccount[1], dataAccount[2], "Config", "", "", 0, "", 0, 0);
            if (con == "ok")
            {
                if (!string.IsNullOrEmpty(ace) || !string.IsNullOrEmpty(group) || !string.IsNullOrEmpty(user))
                {
                    string sql = string.Format(@" select distinct TrsName from Web_UserTrs('{0}',{1},'{2}')"
                                              , ace, group, user);

                    var listDB = UnitDatabase.db.Database.SqlQuery<AccessUser>(sql).ToList();
                    return Ok(listDB);
                }
            }

            return Ok(con);
        }

        public class AccessUserReport
        {
            public string Code { get; set; }
            public bool Trs { get; set; }
        }

        [Route("api/Web_Data/AccessUserReport/{ace}/{group}/{user}")]
        public async Task<IHttpActionResult> GetWeb_AccessUserReport(string ace, string group, string user)
        {
            var dataAccount = UnitDatabase.ReadUserPassHeader(this.Request.Headers);
            string con = UnitDatabase.CreateConection(dataAccount[0], dataAccount[1], dataAccount[2], "Config", "", "", 0, "", 0, 0);
            if (con == "ok")
            {
                if (!string.IsNullOrEmpty(ace) || !string.IsNullOrEmpty(group) || !string.IsNullOrEmpty(user))
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
"
                                               , ace, group, user);
                    var listDB = UnitDatabase.db.Database.SqlQuery<AccessUserReport>(sql).ToList();
                    return Ok(listDB);
                }
            }
            return Ok(con);
        }


        public class AccessUserReportErj
        {
            public string Code { get; set; }
            public bool Trs { get; set; }
        }

        [Route("api/Web_Data/AccessUserReportErj/{ace}/{group}/{user}")]
        public async Task<IHttpActionResult> GetWeb_AccessUserReportErj(string ace, string group, string user)
        {
            var dataAccount = UnitDatabase.ReadUserPassHeader(this.Request.Headers);
            string con = UnitDatabase.CreateConection(dataAccount[0], dataAccount[1], dataAccount[2], "Config", "", "", 0, "", 0, 0);
            if (con == "ok")
            {
                if (!string.IsNullOrEmpty(ace) || !string.IsNullOrEmpty(group) || !string.IsNullOrEmpty(user))
                {
                    string sql = string.Format(@"declare @ace nvarchar(5), @group int , @username nvarchar(20)
                                                 set @ace = '{0}'
                                                 set @group = {1}
                                                 set @username = '{2}'
                                                 select 'ErjDocK'as Code , [dbo].[Web_RprtTrs](@group,@ace,@username,'ErjDocK') as Trs
                                                 union all
                                                 select 'ErjDocErja'as Code , [dbo].[Web_RprtTrs](@group,@ace,@username,'ErjDocErja') as Trs"
                                               , ace, group, user);
                    var listDB = UnitDatabase.db.Database.SqlQuery<AccessUserReportErj>(sql).ToList();
                    return Ok(listDB);
                }
            }
            return Ok(con);
        }



        ///////Erj-----------------------------------------------------------------------------------------------------------------------------

        public partial class Web_ErjCust
        {
            public string Code { get; set; }

            public string Name { get; set; }

            public string Spec { get; set; }
        }


        //  GET: api/Web_Data/ErjCust لیست مشتریان ارجاعات

        [Route("api/Web_Data/ErjCust/{ace}/{sal}/{group}")]
        public async Task<IHttpActionResult> GetWeb_ErjCust(string ace, string sal, string group)
        {
            var dataAccount = UnitDatabase.ReadUserPassHeader(this.Request.Headers);
            string con = UnitDatabase.CreateConection(dataAccount[0], dataAccount[1], dataAccount[2], ace, sal, group, 0, "", 0, 0);
            if (con == "ok")
            {
                string sql = string.Format(@"Select * from Web_ErjCust");
                var listDB = UnitDatabase.db.Database.SqlQuery<Web_ErjCust>(sql).ToList();

                return Ok(listDB);
            }
            return Ok(con);
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
            var dataAccount = UnitDatabase.ReadUserPassHeader(this.Request.Headers);
            string con = UnitDatabase.CreateConection(dataAccount[0], dataAccount[1], dataAccount[2], ace, sal, group, 0, "", 0, 0);
            if (con == "ok")
            {
                string sql = string.Format(@"Select * from Web_Khdt");
                var listDB = UnitDatabase.db.Database.SqlQuery<Web_Khdt>(sql).ToList();

                return Ok(listDB);
            }
            return Ok(con);
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
            var dataAccount = UnitDatabase.ReadUserPassHeader(this.Request.Headers);
            string con = UnitDatabase.CreateConection(dataAccount[0], dataAccount[1], dataAccount[2], ace, sal, group, 0, "", 0, 0);
            if (con == "ok")
            {
                string sql = string.Format(@"Select * from Web_ErjStatus");
                var listDB = UnitDatabase.db.Database.SqlQuery<Web_ErjStatus>(sql).ToList();
                return Ok(listDB);
            }
            return Ok(con);
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
            string con = UnitDatabase.CreateConection(dataAccount[0], dataAccount[1], dataAccount[2], ace, sal, group, 0, "", 0, 0);
            if (con == "ok")
            {
                string sql = string.Format(CultureInfo.InvariantCulture,
                          @"select top (10000)  * FROM  Web_ErjDocK('{0}','{1}') AS ErjDocK where 1 = 1",
                          ErjDocKObject.SrchSt, dataAccount[2]);

                if (ErjDocKObject.userMode == "USER")  // بعدا باید درست شود
                    sql += string.Format(" and Eghdam = '{0}' ", ErjDocKObject.userName);

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

                var listTrzI = UnitDatabase.db.Database.SqlQuery<Web_ErjDocK>(sql);
                return Ok(listTrzI);
            }
            return Ok(con);
        }



        public partial class Web_ErjUsers
        {
            public string Code { get; set; }


            public string Name { get; set; }


            public string Spec { get; set; }
        }

        // GET: api/Web_Data/Web_ErjUsers   ارجاع شونده/ارجاع دهنده
        [Route("api/Web_Data/Web_ErjUsers/{ace}/{sal}/{group}/{UserCode}")]
        public async Task<IHttpActionResult> GetWeb_ErjUsers(string ace, string sal, string group, string userCode)
        {
            var dataAccount = UnitDatabase.ReadUserPassHeader(this.Request.Headers);
            string con = UnitDatabase.CreateConection(dataAccount[0], dataAccount[1], dataAccount[2], ace, sal, group, 0, "", 0, 0);
            if (con == "ok")
            {
                string sql = string.Format(@"Select * from Web_ErjUsers('{0}')", userCode);
                var listDB = UnitDatabase.db.Database.SqlQuery<Web_ErjUsers>(sql).ToList();
                return Ok(listDB);
            }
            return Ok(con);
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
            var dataAccount = UnitDatabase.ReadUserPassHeader(this.Request.Headers);
            string con = UnitDatabase.CreateConection(dataAccount[0], dataAccount[1], dataAccount[2], ace, sal, group, 0, "", 0, 0);
            if (con == "ok")
            {
                string sql = string.Format(@"Select * from Web_RepToUsers('{0}') order by code", userCode);
                var listDB = UnitDatabase.db.Database.SqlQuery<Web_RepToUsers>(sql).ToList();
                return Ok(listDB);
            }
            return Ok(con);
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
            var dataAccount = UnitDatabase.ReadUserPassHeader(this.Request.Headers);
            string con = UnitDatabase.CreateConection(dataAccount[0], dataAccount[1], dataAccount[2], ace, sal, group, 0, "", 0, 0);
            if (con == "ok")
            {
                string sql = string.Format(@"Select * from Web_RepFromUsers('{0}') order by code", userCode);
                var listDB = UnitDatabase.db.Database.SqlQuery<Web_RepFromUsers>(sql).ToList();
                return Ok(listDB);
            }
            return Ok(con);
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
            string con = UnitDatabase.CreateConection(dataAccount[0], dataAccount[1], dataAccount[2], ace, sal, group, 0, "", 0, 0);
            if (con == "ok")
            {
                string sql = string.Format(@"Select * from Web_Mahramaneh('{0}')", dataAccount[2]);
                var listDB = UnitDatabase.db.Database.SqlQuery<Web_Mahramaneh>(sql).ToList();
                return Ok(listDB);
            }
            return Ok(con);
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
            var dataAccount = UnitDatabase.ReadUserPassHeader(this.Request.Headers);
            string con = UnitDatabase.CreateConection(dataAccount[0], dataAccount[1], dataAccount[2], ace, sal, group, 0, "", 0, 0);
            if (con == "ok")
            {
                string sql = string.Format(@"Select * from Web_ErjRooneveshtUsersList({0},{1})",
                                             Web_RooneveshtUsersList_Object.SerialNumber,
                                             Web_RooneveshtUsersList_Object.BandNo);
                var listDB = UnitDatabase.db.Database.SqlQuery<Web_RooneveshtUsersList>(sql).ToList();
                return Ok(listDB);
            }
            return Ok(con);
        }





        public class ErjDocHObject
        {
            public byte Mode { get; set; }

            public string UserCode { get; set; }

            public int select { get; set; }

            public bool accessSanad { get; set; }

        }


        // Post: api/Web_Data/ErjDocH  فهرست پرونده ها  
        [Route("api/Web_Data/ErjDocH/{ace}/{sal}/{group}")]
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PostWeb_ErjDocH_F(string ace, string sal, string group, ErjDocHObject ErjDocHObject)
        {
            var dataAccount = UnitDatabase.ReadUserPassHeader(this.Request.Headers);
            string con = UnitDatabase.CreateConection(dataAccount[0], dataAccount[1], dataAccount[2], ace, sal, group, 0, "", 0, 0);
            if (con == "ok")
            {
                string sql = "declare @enddate nvarchar(20) ";
                sql += "select ";
                if (ErjDocHObject.select == 0)
                    sql += " top(100) ";

                sql += string.Format(CultureInfo.InvariantCulture,
                          @" * FROM  Web_ErjDocH_F({0},'{1}') AS ErjDocH where 1 = 1",
                          ErjDocHObject.Mode, dataAccount[2]);
                if (ErjDocHObject.accessSanad == false)
                    sql += " and Eghdam = '" + ErjDocHObject.UserCode + "' ";

                var list = UnitDatabase.db.Database.SqlQuery<Web_ErjDocH>(sql);
                return Ok(list);
            }
            return Ok(con);
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
            var dataAccount = UnitDatabase.ReadUserPassHeader(this.Request.Headers);
            string con = UnitDatabase.CreateConection(dataAccount[0], dataAccount[1], dataAccount[2], ace, sal, group, 0, "", 0, 0);
            if (con == "ok")
            {
                string sql = string.Format(CultureInfo.InvariantCulture,
                                           @"select  SerialNumber,Comm,FName,BandNo FROM Web_DocAttach
                                             where   ModeCode = {0} and ProgName='{1}' and SerialNumber = {2} order by BandNo desc",
                                             DocAttachObject.ModeCode,
                                             "ERJ1",
                                             DocAttachObject.SerialNumber);

                var list = UnitDatabase.db.Database.SqlQuery<Web_DocAttach>(sql);
                return Ok(list);
            }
            return Ok(con);
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


        private bool SaveBytesToFile(byte[] butes, string filename)
        {
            using (var fileStream = new FileStream(@"D:\" + filename + ".pdf", FileMode.Create, FileAccess.Write))
            {
                fileStream.Write(butes, 0, butes.Length);
            }
            return true;
        }



        // Post: api/Web_Data/DownloadAttach   دانلود پیوست  
        [Route("api/Web_Data/DownloadAttach/{ace}/{sal}/{group}")]
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PostWeb_DownloadAttach(string ace, string sal, string group, DownloadAttachObject DownloadAttachObject)
        {
            var dataAccount = UnitDatabase.ReadUserPassHeader(this.Request.Headers);
            string con = UnitDatabase.CreateConection(dataAccount[0], dataAccount[1], dataAccount[2], ace, sal, group, 0, "", 0, 0);
            if (con == "ok")
            {
                string sql = string.Format(CultureInfo.InvariantCulture,
                          @"select Atch FROM Web_DocAttach where SerialNumber = {0} and BandNo = {1}",
                          DownloadAttachObject.SerialNumber,
                          DownloadAttachObject.BandNo);



                var list = UnitDatabase.db.Database.SqlQuery<DownloadAttach>(sql).Single();

                // SaveBytesToFile(list.Atch,"a.zip");
                // byte[] imageData = 
                //  MemoryStream ms = new MemoryStream(list.Atch);

                // string s = System.Text.Encoding.UTF8.GetString(list.Atch);


                return Ok(list.Atch);
            }
            return Ok(con);
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

        // GET: api/Web_Data/Web_ErjResult   نتیجه در اتوماسیون
        [Route("api/Web_Data/Web_ErjResult/{ace}/{sal}/{group}/{SerialNumber}/{DocBMode}/{ToUserCode}")]
        public async Task<IHttpActionResult> GetWeb_ErjResult(string ace, string sal, string group, string SerialNumber, string DocBMode, string ToUserCode)
        {
            var dataAccount = UnitDatabase.ReadUserPassHeader(this.Request.Headers);
            string con = UnitDatabase.CreateConection(dataAccount[0], dataAccount[1], dataAccount[2], ace, sal, group, 0, "", 0, 0);
            if (con == "ok")
            {

                string sql = string.Format(@"Select * from Web_ErjResult where SerialNumber = {0}", SerialNumber);

                if (DocBMode != "null")
                    sql += string.Format(@" and  DocBMode = {0} and ToUserCode = '{1}'",
                         DocBMode,
                         DocBMode == "0" ? "" : ToUserCode
                        );


                var listDB = UnitDatabase.db.Database.SqlQuery<Web_ErjResult>(sql).ToList();
                return Ok(listDB);
            }
            return Ok(con);
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

            // public string EghdamComm { get; set; }

            // public string SpecialComm { get; set; }

            // public string FinalComm { get; set; }

            // public string DocDesc { get; set; }

            public int? DocBStep { get; set; }

            public string RjRadif { get; set; }

            public int? BandNo { get; set; }

            public int? DocBMode { get; set; }

            //public string RjComm { get; set; }

            public string RjDate { get; set; }

            public string RjStatus { get; set; }

            public string RjEndDate { get; set; }

            public string RjMhltDate { get; set; }

            public DateTime? RjUpdateDate { get; set; }

            public string RjUpdateUser { get; set; }

            public int? ErjaCount { get; set; }

            public double? RjTime { get; set; }

            public string FromUserCode { get; set; }

            public string FromUserName { get; set; }

            public string ToUserCode { get; set; }

            public string ToUserName { get; set; }

            public string RjReadSt { get; set; }

            public byte? FinalCommTrs { get; set; }

            public byte DocAttachExists { get; set; }

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
            string con = UnitDatabase.CreateConection(dataAccount[0], dataAccount[1], dataAccount[2], ace, sal, group, 0, "", 0, 0);
            if (con == "ok")
            {
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

                //sql += "order by RjUpdateDate Desc,RjDate Desc";


                var listErjDocB_Last = UnitDatabase.db.Database.SqlQuery<Web_ErjDocB_Last>(sql);
                return Ok(listErjDocB_Last);
            }
            return Ok(con);
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
            var dataAccount = UnitDatabase.ReadUserPassHeader(this.Request.Headers);
            string con = UnitDatabase.CreateConection(dataAccount[0], dataAccount[1], dataAccount[2], ace, sal, group, ErjDocErja.SerialNumber, "", 0, 0);
            if (con == "ok")
            {
                string sql = string.Format(CultureInfo.InvariantCulture,
                          @"select top (10000)  * FROM  Web_ErjDocErja({0}) AS ErjDocErja where 1 = 1 order by BandNo,DocBMode "
                          , ErjDocErja.SerialNumber);


                var listErjDocErja = UnitDatabase.db.Database.SqlQuery<Web_ErjDocErja>(sql);
                return Ok(listErjDocErja);
            }
            return Ok(con);
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
        }


        // POST: api/Web_Data/ErjSaveDoc_BSave
        [Route("api/Web_Data/ErjSaveDoc_BSave/{ace}/{sal}/{group}")]
        public async Task<IHttpActionResult> PostErjSaveDoc_BSave(string ace, string sal, string group, Web_ErjSaveDoc_BSave Web_ErjSaveDoc_BSave)
        {
            string value = "";
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var dataAccount = UnitDatabase.ReadUserPassHeader(this.Request.Headers);
            string con = UnitDatabase.CreateConection(dataAccount[0], dataAccount[1], dataAccount[2], ace, sal, group, Web_ErjSaveDoc_BSave.SerialNumber, "", 0, 0);
            if (con == "ok")
            {
                try
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
                                    @SrMode = {8}
                            SELECT	@BandNo as N'@BandNo' ",
                         Web_ErjSaveDoc_BSave.SerialNumber,
                         Web_ErjSaveDoc_BSave.BandNo,
                         Web_ErjSaveDoc_BSave.Natijeh,
                         Web_ErjSaveDoc_BSave.FromUserCode,
                         Web_ErjSaveDoc_BSave.ToUserCode,
                         Web_ErjSaveDoc_BSave.RjDate,
                         Web_ErjSaveDoc_BSave.RjTime,
                         Web_ErjSaveDoc_BSave.RjMhltDate,
                         Web_ErjSaveDoc_BSave.SrMode
                         );
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
            return Ok(con);
        }



        public class Web_ErjSaveDoc_CSave
        {
            public long SerialNumber { get; set; }

            public int BandNo { get; set; }

            public string Natijeh { get; set; }

            public string ToUserCode { get; set; }

            public string RjDate { get; set; }
        }

        // POST: api/Web_Data/ErjSaveDoc_CSave
        [Route("api/Web_Data/ErjSaveDoc_CSave/{ace}/{sal}/{group}")]
        public async Task<IHttpActionResult> PostErjSaveDoc_CSave(string ace, string sal, string group, [FromBody]List<Web_ErjSaveDoc_CSave> Web_ErjSaveDoc_CSave)
        {
            string value = "";
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var dataAccount = UnitDatabase.ReadUserPassHeader(this.Request.Headers);
            string con = UnitDatabase.CreateConection(dataAccount[0], dataAccount[1], dataAccount[2], ace, sal, group, 0, "", 0, 0);
            if (con == "ok")
            {

                try
                {
                    string sql = "";
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
		                            @RjDate = N'{4}'
                               SELECT	@BandNo as N'@BandNo'",

                            item.SerialNumber,
                            item.BandNo,
                            item.Natijeh,
                            item.ToUserCode,
                            item.RjDate);
                        value = UnitDatabase.db.Database.SqlQuery<string>(sql).Single();
                    }
                    await UnitDatabase.db.SaveChangesAsync();

                    if (!string.IsNullOrEmpty(value))
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










        public class Web_ErjSaveDoc_CD
        {
            public long SerialNumber { get; set; }

            public int BandNo { get; set; }
        }

        // POST: api/Web_Data/ErjSaveDoc_CD
        [Route("api/Web_Data/ErjSaveDoc_CD/{ace}/{sal}/{group}")]
        public async Task<IHttpActionResult> PostErjSaveDoc_CD(string ace, string sal, string group, Web_ErjSaveDoc_CD Web_ErjSaveDoc_CD)
        {
            string value = "";
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var dataAccount = UnitDatabase.ReadUserPassHeader(this.Request.Headers);
            string con = UnitDatabase.CreateConection(dataAccount[0], dataAccount[1], dataAccount[2], ace, sal, group, 0, "", 0, 0);
            if (con == "ok")
            {
                try
                {
                    string sql = "";
                    sql = string.Format(CultureInfo.InvariantCulture,
                         @" DECLARE	@return_value int,
                                        @BandNo nvarchar(10)
                               EXEC	@return_value = [dbo].[Web_ErjSaveDoc_CD]
		                            @SerialNumber = {0},
		                            @BandNo = {1}
                               SELECT	@BandNo as N'@BandNo'",

                        Web_ErjSaveDoc_CD.SerialNumber,
                        Web_ErjSaveDoc_CD.BandNo);
                    value = UnitDatabase.db.Database.SqlQuery<string>(sql).Single();
                    await UnitDatabase.db.SaveChangesAsync();

                }
                catch (Exception e)
                {
                    throw;
                }

                return Ok(value);
            }
            return Ok(con);
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
            int value = 0;
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var dataAccount = UnitDatabase.ReadUserPassHeader(this.Request.Headers);
            string con = UnitDatabase.CreateConection(dataAccount[0], dataAccount[1], dataAccount[2], ace, sal, group, 0, "", 0, 0);
            if (con == "ok")
            {

                try
                {
                    string sql = "";
                    sql = string.Format(CultureInfo.InvariantCulture,
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
                    value = UnitDatabase.db.Database.SqlQuery<int>(sql).Single();

                    await UnitDatabase.db.SaveChangesAsync();

                    if (value == 0)
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




        public class Web_ErjSaveDoc_HStatus
        {
            public long SerialNumber { get; set; }

            public string Status { get; set; }
        }


        // POST: api/Web_Data/ErjSaveDoc_HStatus
        [Route("api/Web_Data/ErjSaveDoc_HStatus/{ace}/{sal}/{group}")]
        public async Task<IHttpActionResult> PostErjSaveDoc_HStatus(string ace, string sal, string group, Web_ErjSaveDoc_HStatus Web_ErjSaveDoc_HStatus)
        {
            int value = 0;
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var dataAccount = UnitDatabase.ReadUserPassHeader(this.Request.Headers);
            string con = UnitDatabase.CreateConection(dataAccount[0], dataAccount[1], dataAccount[2], ace, sal, group, Web_ErjSaveDoc_HStatus.SerialNumber, "", 0, 0);
            if (con == "ok")
            {
                try
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
                    value = UnitDatabase.db.Database.SqlQuery<int>(sql).Single();
                    if (value == 0)
                    {
                        await UnitDatabase.db.SaveChangesAsync();
                    }
                }
                catch (Exception e)
                {
                    throw;
                }
            }
            return Ok(con);
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





























        /*
          // POST: api/Web_Data/ErjDocAttach_Save
          [Route("api/Web_Data/ErjDocAttach_Save/{ace}/{sal}/{group}")]
          public async Task<IHttpActionResult> PostErjDocAttach_Save(string ace, string sal, string group, Web_DocAttach_Save Web_DocAttach_Save)
          {
              int value = 0;
              if (!ModelState.IsValid)
              {
                  return BadRequest(ModelState);
              }

              var dataAccount = UnitDatabase.ReadUserPassHeader(this.Request.Headers);
              string con = UnitDatabase.CreateConection(dataAccount[0], dataAccount[1], dataAccount[2], ace, sal, group, 0, "", 0, 0);
              if (con == "ok")
              {
                  try
                  {
                      SaveDataUrlToFile(Web_DocAttach_Save.Atch, @"c:\1.txt");
                      string sql = "";
                      sql = string.Format(CultureInfo.InvariantCulture,
                           @" DECLARE	@return_value int

                                      EXEC	@return_value = [dbo].[Web_DocAttach_Save]
                                              @ProgName = '{0}',
                                              @ModeCode = '{1}',
                                              @SerialNumber = {2},
                                              @BandNo = {3},
                                              @Code = '{4}',
                                              @Comm = '{5}',
                                              @FName = '{6}',
                                              @Atch = '{7}'

                                      SELECT	'Return Value' = @return_value",
                          Web_DocAttach_Save.ProgName,
                          Web_DocAttach_Save.ModeCode,
                          Web_DocAttach_Save.SerialNumber,
                          Web_DocAttach_Save.BandNo,
                          Web_DocAttach_Save.Code,
                          Web_DocAttach_Save.Comm,
                          Web_DocAttach_Save.FName,
                          Web_DocAttach_Save.Atch
                          );
                      value = UnitDatabase.db.Database.SqlQuery<int>(sql).Single();

                      await UnitDatabase.db.SaveChangesAsync();

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


      */










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
            int value = 0;
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var dataAccount = UnitDatabase.ReadUserPassHeader(this.Request.Headers);
            string con = UnitDatabase.CreateConection(dataAccount[0], dataAccount[1], dataAccount[2], ace, sal, group, 0, "", 0, 0);
            if (con == "ok")
            {
                try
                {
                    string sql = "";
                    sql = string.Format(CultureInfo.InvariantCulture,
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
                    value = UnitDatabase.db.Database.SqlQuery<int>(sql).Single();

                    await UnitDatabase.db.SaveChangesAsync();

                    if (value == 1)
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








        public class KGruObject
        {
            public byte Mode { get; set; }

            public string UserCode { get; set; }
        }

        // Post: api/Web_Data/KGru لیست کالا گروه ها
        [Route("api/Web_Data/KGru/{ace}/{sal}/{group}")]
        public async Task<IHttpActionResult> PostWeb_KGru(string ace, string sal, string group, KGruObject kGruObject)
        {
            var dataAccount = UnitDatabase.ReadUserPassHeader(this.Request.Headers);

            string con = UnitDatabase.CreateConection(dataAccount[0], dataAccount[1], dataAccount[2], ace, sal, group, 0, "", 0, 0);
            if (con == "ok")
            {
                string sql = string.Format("select  * FROM  Web_KGru_F({0},'{1}') ", kGruObject.Mode, kGruObject.UserCode);
                var listKGru = UnitDatabase.db.Database.SqlQuery<Web_KGru>(sql);
                return Ok(listKGru);
            }
            return Ok(con);
        }


        // GET: api/Web_Data/Mkz لیست مراکز هزینه
        [Route("api/Web_Data/Mkz/{ace}/{sal}/{group}")]
        public IQueryable<Web_Mkz> GetWeb_Mkz(string ace, string sal, string group)
        {
            var dataAccount = UnitDatabase.ReadUserPassHeader(this.Request.Headers);
            string con = UnitDatabase.CreateConection(dataAccount[0], dataAccount[1], dataAccount[2], ace, sal, group, 0, "", 0, 0);
            if (con == "ok")
            {
                //var a = UnitDatabase.db.Database.Connection.State;

                var list = UnitDatabase.db.Web_Mkz.OrderBy(c => c.SortCode);

                //if (UnitDatabase.db.Database.Connection.State == ConnectionState.Open)
                //{
                //    UnitDatabase.db.Database.Connection.Close();
                //}
                //UnitDatabase.db.Dispose();
                return list;

            }
            return null;
        }

        // GET: api/Web_Data/Opr لیست پروژه ها
        [Route("api/Web_Data/Opr/{ace}/{sal}/{group}")]
        public IQueryable<Web_Opr> GetWeb_Opr(string ace, string sal, string group)
        {
            var dataAccount = UnitDatabase.ReadUserPassHeader(this.Request.Headers);
            string con = UnitDatabase.CreateConection(dataAccount[0], dataAccount[1], dataAccount[2], ace, sal, group, 0, "", 0, 0);
            if (con == "ok")
            {
                return UnitDatabase.db.Web_Opr;
            }
            return null;
        }


        // GET: api/Web_Data/Arz لیست ارز ها
        [Route("api/Web_Data/Arz/{ace}/{sal}/{group}")]
        public IQueryable<Web_Arz> GetWeb_Arz(string ace, string sal, string group)
        {
            var dataAccount = UnitDatabase.ReadUserPassHeader(this.Request.Headers);
            string con = UnitDatabase.CreateConection(dataAccount[0], dataAccount[1], dataAccount[2], ace, sal, group, 0, "", 0, 0);
            if (con == "ok")
            {
                return UnitDatabase.db.Web_Arz;
            }
            return null;
        }

        // GET: api/Web_Data/Web_RprtCols لیست ستونها
        [Route("api/Web_Data/RprtCols/{ace}/{sal}/{group}/{RprtId}/{UserCode}")]
        public async Task<IHttpActionResult> GetWeb_RprtCols(string ace, string sal, string group, string RprtId, string UserCode)
        {
            var dataAccount = UnitDatabase.ReadUserPassHeader(this.Request.Headers);
            string con = UnitDatabase.CreateConection(dataAccount[0], dataAccount[1], dataAccount[2], ace, sal, group, 0, "", 0, 0);
            if (con == "ok")
            {
                string sql;
                sql = string.Format(@"
                                  if (select count(code) from Web_RprtCols where RprtId = '{0}' and UserCode = '{1}') > 0
                                     select * from Web_RprtCols where RprtId = '{0}' and UserCode = '{1}'-- and Name<> ''
                                  else
                                     select * from Web_RprtCols where RprtId = '{0}' and UserCode = '*Default*'-- and Name <> ''",
                                  RprtId, UserCode);

                var list = UnitDatabase.db.Database.SqlQuery<Web_RprtCols>(sql).ToList();
                return Ok(list);
            }
            return Ok(con);
        }


        // GET: api/Web_Data/Web_RprtColsDefult لیست ستون های پیش فرض
        [Route("api/Web_Data/RprtColsDefult/{ace}/{sal}/{group}/{RprtId}")]
        public async Task<IHttpActionResult> GetWeb_RprtColsDefult(string ace, string sal, string group, string RprtId)
        {
            var dataAccount = UnitDatabase.ReadUserPassHeader(this.Request.Headers);
            string con = UnitDatabase.CreateConection(dataAccount[0], dataAccount[1], dataAccount[2], ace, sal, group, 0, "", 0, 0);
            if (con == "ok")
            {
                string sql;
                sql = string.Format(@"  select* from Web_RprtCols where RprtId = '{0}' and UserCode = '*Default*' and Name <> ''", RprtId);
                var list = UnitDatabase.db.Database.SqlQuery<Web_RprtCols>(sql).ToList();
                return Ok(list);
            }
            return Ok(con);
        }


        public class RprtColsSave
        {
            public string UserCode { get; set; }

            public string RprtId { get; set; }

            public string Code { get; set; }

            public byte? Visible { get; set; }
        }



        // POST: api/RprtColsSave
        [Route("api/Web_Data/RprtColsSave/{ace}/{sal}/{group}")]
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PostRprtColsSave(string ace, string sal, string group, [FromBody]List<RprtColsSave> RprtColsSave)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var dataAccount = UnitDatabase.ReadUserPassHeader(this.Request.Headers);
            string con = UnitDatabase.CreateConection(dataAccount[0], dataAccount[1], dataAccount[2], ace, sal, group, 0, "", 0, 0);
            if (con == "ok")
            {
                string sql;
                int value;
                try
                {
                    foreach (var item in RprtColsSave)
                    {
                        sql = string.Format(CultureInfo.InvariantCulture,
                         @" DECLARE	@return_value int
                                                EXEC	@return_value = [dbo].[Web_RprtColsSave]
		                                                @UserCode = '{0}',
		                                                @RprtId = '{1}',
		                                                @Code = '{2}',
		                                                @Visible = {3}
                                                SELECT	'Return Value' = @return_value ",

                        item.UserCode,
                        item.RprtId,
                        item.Code,
                        item.Visible ?? 0
                        );
                        value = UnitDatabase.db.Database.SqlQuery<int>(sql).Single();
                    }
                    await UnitDatabase.db.SaveChangesAsync();
                }
                catch (Exception)
                {
                    return Ok('0');
                    throw;
                }
            }
            return Ok(con);
        }






        // GET: api/Web_Data/ExtraFields لیست مشخصات اضافی
        [Route("api/Web_Data/ExtraFields/{ace}/{sal}/{group}/{modeCode}")]
        public IQueryable<Web_ExtraFields> GetWeb_ExtraFields(string ace, string sal, string group, string modeCode)
        {
            var dataAccount = UnitDatabase.ReadUserPassHeader(this.Request.Headers);
            string con = UnitDatabase.CreateConection(dataAccount[0], dataAccount[1], dataAccount[2], ace, sal, group, 0, "", 0, 0);
            if (con == "ok")
            {
                return UnitDatabase.db.Web_ExtraFields.Where(c => c.ModeCode == modeCode).OrderBy(c => c.BandNo);
            }
            return null;
        }

        //-------------------------------------------------------------------------------------------------------------------------------

        // GET: api/Web_Data/CountTable تعداد رکورد ها    
        [Route("api/Web_Data/CountTable/{ace}/{sal}/{group}/{tableName}/{modeCode}/{inOut}")]
        public async Task<IHttpActionResult> GetWeb_CountTable(string ace, string sal, string group, string tableName, string modeCode, string inOut)
        {
            var dataAccount = UnitDatabase.ReadUserPassHeader(this.Request.Headers);
            string con = UnitDatabase.CreateConection(dataAccount[0], dataAccount[1], dataAccount[2], ace, sal, group, 0, "", 0, 0);
            if (con == "ok")
            {
                string sql = string.Format(@"SELECT count(SerialNumber) FROM Web_{0}", tableName);
                if (modeCode != "null" && inOut == "null")
                    sql += string.Format(@" WHERE ModeCode = '{0}'", modeCode);
                else if (modeCode == "null" && inOut != "null")
                    sql += string.Format(@" WHERE InOut = '{0}'", inOut);

                int count = UnitDatabase.db.Database.SqlQuery<int>(sql).Single();
                return Ok(count);
            }
            return Ok(con);
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

            public string City { get; set; }

            public string Street { get; set; }

            public string Alley { get; set; }

            public string Plack { get; set; }

            public string ZipCode { get; set; }

            public string Tel { get; set; }

            public string Mobile { get; set; }

            public string Fax { get; set; }

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
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var dataAccount = UnitDatabase.ReadUserPassHeader(this.Request.Headers);
            string con = UnitDatabase.CreateConection(dataAccount[0], dataAccount[1], dataAccount[2], ace, sal, group, 0, "", 0, 0);
            if (con == "ok")
            {
                string value;
                try
                {
                    string sql = string.Format(CultureInfo.InvariantCulture,
                     @" DECLARE	@return_value int,
		                        @oCode nvarchar(50)

                        EXEC	@return_value = [dbo].[Web_SaveCust]
		                        @BranchCode = {0},
		                        @UserCode = '{1}',
		                        @Code = '{2}',
		                        @Name = '{3}',
		                        @Spec = '{4}',
		                        @MelliCode = '{5}',
		                        @EcoCode = '{6}',
		                        @City = '{7}',
		                        @Street = '{8}',
		                        @Alley = '{9}',
		                        @Plack = '{10}',
		                        @ZipCode = '{11}',
		                        @Tel = '{12}',
		                        @Mobile = '{13}',
		                        @Fax = '{14}',
		                        @CGruCode = '{15}',
		                        @EtebarNaghd = {16},
		                        @EtebarCheck = {17},
		                        @F01 = '{18}',
		                        @F02 = '{19}',
		                        @F03 = '{20}',
		                        @F04 = '{21}',
		                        @F05 = '{22}',
		                        @F06 = '{23}',
		                        @F07 = '{24}',
		                        @F08 = '{25}',
		                        @F09 = '{26}',
		                        @F10 = '{27}',
		                        @F11 = '{28}',
		                        @F12 = '{29}',
		                        @F13 = '{30}',
		                        @F14 = '{31}',
		                        @F15 = '{32}',
		                        @F16 = '{33}',
		                        @F17 = '{34}',
		                        @F18 = '{35}',
		                        @F19 = '{36}',
		                        @F20 = '{37}',
		                        @oCode = @oCode OUTPUT

                        SELECT	@oCode as N'@oCode'",
                        aFI_SaveCust.BranchCode ?? 0,
                        aFI_SaveCust.UserCode,
                        aFI_SaveCust.Code,
                        aFI_SaveCust.Name,
                        aFI_SaveCust.Spec,
                        aFI_SaveCust.MelliCode,
                        aFI_SaveCust.EcoCode,
                        aFI_SaveCust.City,
                        aFI_SaveCust.Street,
                        aFI_SaveCust.Alley,
                        aFI_SaveCust.Plack,
                        aFI_SaveCust.ZipCode,
                        aFI_SaveCust.Tel,
                        aFI_SaveCust.Mobile,
                        aFI_SaveCust.Fax,
                        aFI_SaveCust.CGruCode,
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
                        aFI_SaveCust.F20);
                    value = UnitDatabase.db.Database.SqlQuery<string>(sql).Single();

                    await UnitDatabase.db.SaveChangesAsync();
                    return Ok(value);
                }
                catch (Exception)
                {
                    throw;
                }
            }
            return Ok(con);
        }


        // post: api/AFI_DelCust
        [Route("api/Web_Data/AFI_DelCust/{ace}/{sal}/{group}/{CustCode}")]
        public async Task<IHttpActionResult> PostAFI_DelCust(string ace, string sal, string group, string CustCode)
        {
            var dataAccount = UnitDatabase.ReadUserPassHeader(this.Request.Headers);
            string con = UnitDatabase.CreateConection(dataAccount[0], dataAccount[1], dataAccount[2], ace, sal, group, 0, "", 0, 0);
            if (con == "ok")
            {
                try
                {
                    string sql = string.Format(@"DECLARE	@return_value int
                                                    EXEC	@return_value = [dbo].[Web_DelCust]
		                                                    @Code = '{0}'
                                                 SELECT	'Return Value' = @return_value",
                                                CustCode);
                    int value = UnitDatabase.db.Database.SqlQuery<int>(sql).Single();
                    if (value == 0)
                    {
                        await UnitDatabase.db.SaveChangesAsync();
                    }
                    return Ok(value);
                }
                catch (Exception e)
                {
                    throw;
                }

            }
            return Ok(con);
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
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var dataAccount = UnitDatabase.ReadUserPassHeader(this.Request.Headers);
            string con = UnitDatabase.CreateConection(dataAccount[0], dataAccount[1], dataAccount[2], ace, sal, group, 0, "", 0, 0);
            if (con == "ok")
            {
                string value;
                try
                {
                    string sql = string.Format(CultureInfo.InvariantCulture,
                     @" DECLARE	@return_value int,
		                        @oCode nvarchar(50)

                        EXEC	@return_value = [dbo].[Web_SaveCust]
		                        @BranchCode = {0},
		                        @UserCode = '{1}',
		                        @Code = '{2}',
		                        @Name = '{3}',
		                        @Spec = '{4}',
		                        @KGruCode = '{5}',
		                        @FanniNo = '{6}',
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
		                        @oCode = @oCode OUTPUT

                        SELECT	@oCode as N'@oCode'",
                        aFI_SaveKala.BranchCode ?? 0,
                        aFI_SaveKala.UserCode,
                        aFI_SaveKala.Code,
                        aFI_SaveKala.Name,
                        aFI_SaveKala.Spec,
                        aFI_SaveKala.KGruCode,
                        aFI_SaveKala.FanniNo,
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
                    value = UnitDatabase.db.Database.SqlQuery<string>(sql).Single();

                    await UnitDatabase.db.SaveChangesAsync();
                    return Ok(value);
                }
                catch (Exception)
                {
                    throw;
                }
            }
            return Ok(con);
        }


        // post: api/AFI_DelKala
        [Route("api/Web_Data/AFI_DelKala/{ace}/{sal}/{group}/{KalaCode}")]
        public async Task<IHttpActionResult> PostAFI_DelKala(string ace, string sal, string group, string KalaCode)
        {
            var dataAccount = UnitDatabase.ReadUserPassHeader(this.Request.Headers);
            string con = UnitDatabase.CreateConection(dataAccount[0], dataAccount[1], dataAccount[2], ace, sal, group, 0, "", 0, 0);
            if (con == "ok")
            {
                try
                {
                    string sql = string.Format(@"DECLARE	@return_value int
                                                    EXEC	@return_value = [dbo].[Web_DelKala]
		                                                    @Code = '{0}'
                                                 SELECT	'Return Value' = @return_value",
                                                KalaCode);
                    int value = UnitDatabase.db.Database.SqlQuery<int>(sql).Single();
                    if (value == 0)
                    {
                        await UnitDatabase.db.SaveChangesAsync();
                    }
                    return Ok(value);
                }
                catch (Exception e)
                {
                    throw;
                }
            }
            return Ok(con);
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
            var dataAccount = UnitDatabase.ReadUserPassHeader(this.Request.Headers);
            string con = UnitDatabase.CreateConection(dataAccount[0], dataAccount[1], dataAccount[2], "Config", "", "0", 0, "", 0, 0);
            if (con == "ok")
            {
                string sql = string.Format("select * FROM  Web_Groups('{0}','{1}') where code in ({2})", GroupsObject.ProgName, GroupsObject.User, GroupsObject.groups);

                var listGroups = UnitDatabase.db.Database.SqlQuery<Web_Groups>(sql);
                return Ok(listGroups);
            }
            return Ok(con);
        }






        // Post: api/Web_Data/ChangeDatabase 
        [Route("api/Web_Data/ChangeDatabase/{ace}/{sal}/{group}/{auto}/{lockNumber}")]
        public async Task<IHttpActionResult> GetWeb_ChangeDatabase(string ace, string sal, string group, bool auto, string lockNumber)
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

                var dataAccount = UnitDatabase.ReadUserPassHeader(this.Request.Headers);
                if (Change != "1")
                {
                    UnitDatabase.ChangeDatabase(ace, sal, group, dataAccount[2], auto);
                    return Ok("OK");
                }
                else if (Change == "1" && dataAccount[2] == "ACE" && auto == false)
                {
                    UnitDatabase.ChangeDatabase(ace, sal, group, dataAccount[2], auto);
                    return Ok("OK");
                }
                else
                    return Ok("کاربر " + User + " در حال بازسازی اطلاعات است . لطفا منتظر بمانید ");
            }
            catch (Exception e)
            {
                MyIniConfig.Write("Change", "0");
                MyIniConfig.Write("EndDate", DateTime.Now.ToString());
                MyIniConfig.Write("error", e.Message.ToString());
                return Ok("error" + e.Message.ToString());
                throw;
            }
        }


        // GET: api/Web_Data/ بازسازی دستی بانک اطلاعات کانفیگ  
        [Route("api/Web_Data/ChangeDatabaseConfig/{lockNumber}")]
        public async Task<IHttpActionResult> GetWeb_ChangeDatabaseConfig(string lockNumber)
        {
            string IniPath = HttpContext.Current.Server.MapPath("~/Content/ini/ServerConfig.Ini");

            IniFile MyIni = new IniFile(IniPath);

            string addressFileSql = MyIni.Read("FileSql");

            string IniConfigPath = addressFileSql + "\\" + lockNumber + "\\" + "Config.ini";

            IniFile MyIniConfig = new IniFile(IniConfigPath);

            try
            {
                /*string Change = MyIniConfig.Read("Change");
                string BeginDate = MyIniConfig.Read("BeginDate");
                string User = MyIniConfig.Read("User");
                string Prog = MyIniConfig.Read("Prog");
                string Group = MyIniConfig.Read("Group");
                string Sal = MyIniConfig.Read("Sal");
                string EndDate = MyIniConfig.Read("EndDate");*/

                var dataAccount = UnitDatabase.ReadUserPassHeader(this.Request.Headers);

                //if (Change != "1")
                // {
                string con = UnitDatabase.CreateConection(dataAccount[0], dataAccount[1], dataAccount[2], "Config", "1234", "00", 0, "", 0, 0);
                if (con == "ok")
                {
                    return Ok("OK");
                }
                else
                    return Ok(con);
                // }
                //else
                // {
                //    return Ok("کاربر " + User + " در حال بازسازی اطلاعات است . لطفا منتظر بمانید ");
                //}
            }
            catch (Exception e)
            {
                MyIniConfig.Write("Change", "0");
                MyIniConfig.Write("EndDate", DateTime.Now.ToString());
                MyIniConfig.Write("error", e.Message.ToString());
                return Ok("error" + e.Message.ToString());
                throw;
            }
        }


        public class Object_ErjDocXK
        {
            public int ModeCode { get; set; }

            public string LockNo { get; set; }
        }


        [Route("api/Web_Data/Web_ErjDocXK/{ace}/{sal}/{group}")]
        public async Task<IHttpActionResult> PostWeb_ErjDocXK(string ace, string sal, string group, Object_ErjDocXK Object_ErjDocXK)
        {
            var dataAccount = UnitDatabase.ReadUserPassHeader(this.Request.Headers);
            string con = UnitDatabase.CreateConection(dataAccount[0], dataAccount[1], dataAccount[2], ace, sal, group, 0, "", 0, 0);
             if (con == "ok")
            {
                string sql = string.Format("select * from dbo.Web_ErjDocXK({0},'{1}') order by DocDate desc , SerialNumber desc", Object_ErjDocXK.ModeCode, Object_ErjDocXK.LockNo);
                var list = UnitDatabase.db.Database.SqlQuery<Web_ErjDocXK>(sql).ToList();
                return Ok(list);
            }
            return Ok(con);
        }


        public class Object_TicketStatus
        {
            public string SerialNumber { get; set; }
        }


        [Route("api/Web_Data/Web_TicketStatus/{ace}/{sal}/{group}")]
        public async Task<IHttpActionResult> PostWeb_TicketStatus(string ace, string sal, string group, Object_TicketStatus Object_TicketStatus)
        {
            var dataAccount = UnitDatabase.ReadUserPassHeader(this.Request.Headers);
            string con = UnitDatabase.CreateConection(dataAccount[0], dataAccount[1], dataAccount[2], ace, sal, group, 0, "", 0, 0);
            if (con == "ok")
            {
                string sql = string.Format("select * from Web_TicketStatus where serialnumber in ({0})", Object_TicketStatus.SerialNumber);
                var list = UnitDatabase.db.Database.SqlQuery<Web_TicketStatus>(sql).ToList();
                return Ok(list);
            }
            return Ok(con);
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

        }


        [Route("api/Web_Data/ErjSaveTicket_HI/{ace}/{sal}/{group}")]
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PostErjSaveTicket_HI(string ace, string sal, string group, ErjSaveTicket_HI ErjSaveTicket_HI)
        {
            var dataAccount = UnitDatabase.ReadUserPassHeader(this.Request.Headers);
            string con = UnitDatabase.CreateConection(dataAccount[0], dataAccount[1], dataAccount[2], ace, sal, group, 0, "", 0, 0);
            if (con == "ok")
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
		                                    @DocNo_Out = @DocNo_Out OUTPUT
                                    SELECT	@DocNo_Out as N'DocNo_Out'",
                    ErjSaveTicket_HI.SerialNumber,
                    ErjSaveTicket_HI.DocDate ?? string.Format("{0:yyyy/MM/dd}", DateTime.Now.AddDays(-1)),
                    ErjSaveTicket_HI.UserCode,
                    ErjSaveTicket_HI.Status,
                    ErjSaveTicket_HI.Spec,
                    ErjSaveTicket_HI.LockNo,
                    ErjSaveTicket_HI.Text,
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
                    ErjSaveTicket_HI.F20
                    );
                int value = UnitDatabase.db.Database.SqlQuery<int>(sql).Single();

                await UnitDatabase.db.SaveChangesAsync();
                return Ok(value);
            }
            return Ok(con);
        }

    }
}