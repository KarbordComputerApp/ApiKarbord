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
    public class Web_DataController : ApiController
    {

        // GET: api/Web_Data/Cust لیست اشخاص
        [Route("api/Web_Data/Cust/{ace}/{sal}/{group}/{forSale}")]
        public IQueryable<Web_Cust> GetWeb_Cust(string ace, string sal, string group, bool? forSale)
        {
            if (UnitDatabase.CreateConection(ace, sal, group))
            {
                if (forSale == null)
                {
                    return UnitDatabase.db.Web_Cust;
                }
                else if (forSale == true)
                {
                    return UnitDatabase.db.Web_Cust.Where(c => c.CustMode == 0 || c.CustMode == 1);
                }
                else if (forSale == false)
                {
                    return UnitDatabase.db.Web_Cust.Where(c => c.CustMode == 0 || c.CustMode == 2);
                }
            }
            return null;
        }


        // GET: api/Web_Data/Acc لیست حساب ها
        [Route("api/Web_Data/Acc/{ace}/{sal}/{group}")]
        public IQueryable<Web_Acc> GetWeb_Acc(string ace, string sal, string group)
        {
            if (UnitDatabase.CreateConection(ace, sal, group))
            {
                return UnitDatabase.db.Web_Acc;
            }
            return null;
        }


        // GET: api/Web_Data/KalaPrice لیست گروه قیمت خرید و فروش
        [Route("api/Web_Data/KalaPrice/{ace}/{sal}/{group}/{insert}")]
        public IQueryable<Web_KalaPrice> GetWeb_KalaPrice(string ace, string sal, string group, bool insert)
        {
            if (UnitDatabase.CreateConection(ace, sal, group))
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
            if (UnitDatabase.CreateConection(ace, sal, group))
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
            if (UnitDatabase.CreateConection(ace, sal, group))
            {
                var a = from p in UnitDatabase.db.Web_Unit where p.KalaCode == codeKala && p.Name != "" select p;
                return a;
            }
            return null;
        }

        // GET: api/Web_Data/Kala لیست کالا ها
        [Route("api/Web_Data/Kala/{ace}/{sal}/{group}")]
        public IQueryable<Web_Kala> GetWeb_Kala(string ace, string sal, string group)
        {
            if (UnitDatabase.CreateConection(ace, sal, group))
            {
                return UnitDatabase.db.Web_Kala;
            }
            return null;
        }

        // GET: api/Web_Data/Inv لیست انبار ها
        [Route("api/Web_Data/Inv/{ace}/{sal}/{group}")]
        public IQueryable<Web_Inv> GetWeb_Inv(string ace, string sal, string group)
        {
            if (UnitDatabase.CreateConection(ace, sal, group))
            {
                return UnitDatabase.db.Web_Inv;
            }
            return null;
        }

        // GET: api/Web_Data/Param لیست پارامتر ها  
        [Route("api/Web_Data/Param/{ace}/{sal}/{group}")]
        public IQueryable<Web_Param> GetWeb_Param(string ace, string sal, string group)
        {
            if (UnitDatabase.CreateConection(ace, sal, group))
            {
                return UnitDatabase.db.Web_Param;
            }
            return null;
        }

        // GET: api/Web_Data/Payment لیست نحوه پرداخت  
        [Route("api/Web_Data/Payment/{ace}/{sal}/{group}")]
        public IQueryable<Web_Payment> GetWeb_Payment(string ace, string sal, string group)
        {
            if (UnitDatabase.CreateConection(ace, sal, group))
            {
                return UnitDatabase.db.Web_Payment.OrderBy(c => c.OrderFld);
            }
            return null;
        }

        // GET: api/Web_Data/Status لیست وضعیت پرداخت  
        [Route("api/Web_Data/Status/{ace}/{sal}/{group}")]
        public IQueryable<Web_Status> GetWeb_Status(string ace, string sal, string group,string prog)
        {
            if (UnitDatabase.CreateConection(ace, sal, group))
            {
                var list = UnitDatabase.db.Web_Status.Where(c => c.Prog == prog);
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
            if (UnitDatabase.CreateConection(ace, sal, group))

            {
                string sql = string.Format(@"EXEC	[dbo].[Web_Calc_AddMin_EffPrice]
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
                                                    @MP1 = {14},
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
            return null;
        }



        //انبار---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

        // GET: api/Web_Data/Thvl لیست حسابها
        [Route("api/Web_Data/Thvl/{ace}/{sal}/{group}")]
        public IQueryable<Web_Thvl> GetWeb_Thvl(string ace, string sal, string group)
        {
            if (UnitDatabase.CreateConection(ace, sal, group))
            {
                var aa = UnitDatabase.db.Web_Thvl;
                return aa;
            }
            return null;
        }


        // GET: api/Web_Data/ اطلاعات لاگین   
        [Route("api/Web_Data/Login/{user}/{pass}/{param1}/{param2}")]
        public async Task<IHttpActionResult> GetWeb_Login(string user, string pass, string param1, string param2)
        {
            if (UnitDatabase.CreateConection("Config", "", ""))
            {
                if (pass == "null")
                    pass = "";
                string sql = string.Format(@" DECLARE  @return_value int
                                              EXEC     @return_value = [dbo].[Web_Login]
                                                       @Code1 = '{0}',
		                                               @UserCode = N'{1}',
                                                       @Code2 = '{2}',
		                                               @Psw = N'{3}'
                                              SELECT   'Return Value' = @return_value",
                                              param1, user, param2, pass);
                int value = UnitDatabase.db.Database.SqlQuery<int>(sql).Single();

                if (value == 1)
                    return Ok(1);
                else
                    return Ok(0);
            }
            return null;
        }


        // دریافت اطلاعات سالهای موجود در اس کیو ال متصل به ای پی ای
        public class DatabseSal
        {
            public string Name { get; set; }
        }

        [Route("api/Web_Data/DatabseSal/{ace}/{group}")]
        public async Task<IHttpActionResult> GetWeb_DatabseSal(string ace, string group)
        {
            if (UnitDatabase.CreateConection("Config", "", ""))
            {
                if (!string.IsNullOrEmpty(ace) || !string.IsNullOrEmpty(group))
                {
                    string sql = string.Format(@" select SUBSTRING(name,11,4) as name  from sys.sysdatabases
                                          where name like 'ACE_{0}%' and SUBSTRING(name,9,2) like '%{1}' order by name"
                                              , ace, group);
                    var listDB = UnitDatabase.db.Database.SqlQuery<DatabseSal>(sql).ToList();
                    return Ok(listDB);
                }
            }
            return null;
        }





        // گزارشات -----------------------------------------------------------------------------------------------------------------------
        public class TrzIObject
        {
            public string azTarikh { get; set; }
            public string taTarikh { get; set; }
            public string InvCode { get; set; }
            public string KGruCode { get; set; }
            public string KalaCode { get; set; }
        }

        // Post: api/Web_Data/TrzI گزارش موجودی انبار  
        // HE_Report_TrzIKala
        [Route("api/Web_Data/TrzI/{ace}/{sal}/{group}")]
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PostWeb_TrzIKala(string ace, string sal, string group, TrzIObject TrzIObject)
        {
            if (UnitDatabase.CreateConection(ace, sal, group))
            {
                string sql = string.Format(CultureInfo.InvariantCulture,
                          @"select * FROM  dbo.Web_TrzIKala('{0}', '{1}') AS TrzI where 1 = 1 ",
                          TrzIObject.azTarikh, TrzIObject.taTarikh);
                //if (TrzIObject.InvCode != "0")
                //    sql += string.Format(" and InvCode = '{0}' ", TrzIObject.InvCode);

                if (TrzIObject.KGruCode != "0")
                    sql += string.Format(" and KGruCode = '{0}' ", TrzIObject.KGruCode);

                sql += UnitPublic.SpiltCodeAnd("InvCode", TrzIObject.InvCode);
                sql += UnitPublic.SpiltCodeAnd("KalaCode", TrzIObject.KalaCode);

                var listTrzI = UnitDatabase.db.Database.SqlQuery<Web_TrzIKala>(sql);
                return Ok(listTrzI);
            }
            return null;
        }

        public class TrzIExfObject
        {
            public string azTarikh { get; set; }
            public string taTarikh { get; set; }
            public string InvCode { get; set; }
            public string KGruCode { get; set; }
            public string KalaCode { get; set; }
        }
        // Post: api/Web_Data/TrzIExf گزارش موجودی انبار  
        // HE_Report_TrzIKalaExf
        [Route("api/Web_Data/TrzIExf/{ace}/{sal}/{group}")]
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PostWeb_TrzIKalaExf(string ace, string sal, string group, TrzIExfObject TrzIExfObject)
        {
            if (UnitDatabase.CreateConection(ace, sal, group))
            {
                string invCode = UnitPublic.SpiltCodeCama(TrzIExfObject.InvCode);

                string sql = string.Format(CultureInfo.InvariantCulture,
                          @"select * FROM  dbo.Web_TrzIKalaExf('{0}', '{1}','{2}') AS TrzIExf where 1 = 1 ",
                          TrzIExfObject.azTarikh, TrzIExfObject.taTarikh, invCode);

                if (TrzIExfObject.KGruCode != "0")
                    sql += string.Format(" and KGruCode = '{0}' ", TrzIExfObject.KGruCode);

                // sql += UnitPublic.SpiltCodeAnd("InvCode", TrzIExfObject.InvCode);
                sql += UnitPublic.SpiltCodeAnd("KalaCode", TrzIExfObject.KalaCode);

                sql += " order by KalaCode,KalaFileNo,KalaState,KalaExf1,KalaExf2,KalaExf3,KalaExf4,KalaExf5,KalaExf6,KalaExf7,KalaExf8,KalaExf9,KalaExf10,KalaExf11,KalaExf12,KalaExf13,KalaExf14,KalaExf15,InvCode,Tag ";

                var listTrzIExf = UnitDatabase.db.Database.SqlQuery<Web_TrzIKalaExf>(sql);
                return Ok(listTrzIExf);
            }
            return null;
        }



        // دریافت اطلاعات سطح دسترسی کاربر
        public class AccessUser
        {
            //public string Code { get; set; }
            //public string ProgName { get; set; }
            //public int GroupNo { get; set; }
            public string TrsName { get; set; }
        }

        [Route("api/Web_Data/AccessUser/{ace}/{group}/{username}")]
        public async Task<IHttpActionResult> GetWeb_AccessUser(string ace, string group, string username)
        {
            if (UnitDatabase.CreateConection("Config", "", ""))
            {
                if (!string.IsNullOrEmpty(ace) || !string.IsNullOrEmpty(group) || !string.IsNullOrEmpty(username))
                {
                    string sql = string.Format(@" select distinct TrsName from Web_UserTrs('{0}',{1},'{2}')"
                                              , ace, group, username);

                    var listDB = UnitDatabase.db.Database.SqlQuery<AccessUser>(sql).ToList();
                    return Ok(listDB);
                }
            }

            return null;
        }

        public class AccessUserReport
        {
            public string Code { get; set; }
            public bool Trs { get; set; }
        }

        [Route("api/Web_Data/AccessUserReport/{ace}/{group}/{username}")]
        public async Task<IHttpActionResult> GetWeb_AccessUserReport(string ace, string group, string username)
        {
            if (UnitDatabase.CreateConection("Config", "", ""))
            {
                if (!string.IsNullOrEmpty(ace) || !string.IsNullOrEmpty(group) || !string.IsNullOrEmpty(username))
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
                                                 select 'FDocR'as Code , [dbo].[Web_RprtTrs](@group,@ace,@username,'FDocR') as Trs
                                                 union all
                                                 select 'TrzAcc' as Code, [dbo].[Web_RprtTrs](@group, @ace, @username, 'TrzAcc') as Trs"
                                               , ace, group, username);
                    var listDB = UnitDatabase.db.Database.SqlQuery<AccessUserReport>(sql).ToList();
                    return Ok(listDB);
                }
            }
            return null;
        }

        public class AccessUserReportErj
        {
            public string Code { get; set; }
            public bool Trs { get; set; }
        }

        [Route("api/Web_Data/AccessUserReportErj/{ace}/{group}/{username}")]
        public async Task<IHttpActionResult> GetWeb_AccessUserReportErj(string ace, string group, string username)
        {
            if (UnitDatabase.CreateConection("Config", "", ""))
            {
                if (!string.IsNullOrEmpty(ace) || !string.IsNullOrEmpty(group) || !string.IsNullOrEmpty(username))
                {
                    string sql = string.Format(@"declare @ace nvarchar(5), @group int , @username nvarchar(20)
                                                 set @ace = '{0}'
                                                 set @group = {1}
                                                 set @username = '{2}'
                                                 select 'ErjDocK'as Code , [dbo].[Web_RprtTrs](@group,@ace,@username,'ErjDocK') as Trs
                                                 union all
                                                 select 'ErjDocErja'as Code , [dbo].[Web_RprtTrs](@group,@ace,@username,'ErjDocErja') as Trs"
                                               , ace, group, username);
                    var listDB = UnitDatabase.db.Database.SqlQuery<AccessUserReportErj>(sql).ToList();
                    return Ok(listDB);
                }
            }
            return null;
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
            if (UnitDatabase.CreateConection(ace, sal, group))
            {
                string sql = string.Format(@"Select * from Web_ErjCust");
                var listDB = UnitDatabase.db.Database.SqlQuery<Web_ErjCust>(sql).ToList();

                return Ok(listDB);
            }
            return null;
        }
        public partial class Web_Khdt
        {

            public int Code { get; set; }

            public string Name { get; set; }

            public string Spec { get; set; }
        }


        //  GET: api/Web_Data/Khdt

        [Route("api/Web_Data/Khdt/{ace}/{sal}/{group}")]
        public async Task<IHttpActionResult> GetWeb_Khdt(string ace, string sal, string group)
        {
            if (UnitDatabase.CreateConection(ace, sal, group))
            {
                string sql = string.Format(@"Select * from Web_Khdt");
                var listDB = UnitDatabase.db.Database.SqlQuery<Web_Khdt>(sql).ToList();

                return Ok(listDB);
            }
            return null;
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
            if (UnitDatabase.CreateConection(ace, sal, group))
            {
                string sql = string.Format(@"Select * from Web_ErjStatus");
                var listDB = UnitDatabase.db.Database.SqlQuery<Web_ErjStatus>(sql).ToList();
                return Ok(listDB);
            }
            return null;
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
        }


        // Post: api/Web_Data/ErjDocK گزارش فهرست پرونده ها  
        [Route("api/Web_Data/ErjDocK/{ace}/{sal}/{group}")]
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PostWeb_ErjDocK(string ace, string sal, string group, ErjDocKObject ErjDocKObject)
        {
            if (UnitDatabase.CreateConection(ace, sal, group))
            {
                string sql = string.Format(CultureInfo.InvariantCulture,
                          @"select * FROM  Web_ErjDocK('{0}') AS ErjDocK where 1 = 1",
                          ErjDocKObject.SrchSt);

                if (ErjDocKObject.userMode == "USER")
                    sql += string.Format(" and Eghdam = '{0}' ", ErjDocKObject.userName);

                if (ErjDocKObject.azTarikh != "")
                    sql += string.Format(" and DocDate >= '{0}' ", ErjDocKObject.azTarikh);

                if (ErjDocKObject.taTarikh != "")
                    sql += string.Format(" and DocDate <= '{0}' ", ErjDocKObject.taTarikh);


                if (ErjDocKObject.Status != "")
                    sql += string.Format(" and Status = '{0}' ", ErjDocKObject.Status);


                sql += UnitPublic.SpiltCodeAnd("CustCode", ErjDocKObject.CustCode);
                sql += UnitPublic.SpiltCodeAnd("KhdtCode", ErjDocKObject.KhdtCode);

                var listTrzI = UnitDatabase.db.Database.SqlQuery<Web_ErjDocK>(sql);
                return Ok(listTrzI);
            }
            return null;
        }



        public partial class Web_ErjUsers
        {
            public string Code { get; set; }

            //public int? GroupNo { get; set; }

            //public string Trs { get; set; }

            //public string ProgName { get; set; }

            public string Name { get; set; }

            //public string Psw { get; set; }

            //public image Emza { get; set; }

            //public byte Version { get; set; }

            //public string LtnName { get; set; }

            //public string TrsRprt { get; set; }

            public string Spec { get; set; }

            //public string VstrCode { get; set; }
        }

        // GET: api/Web_Data/Web_ErjUsers   ارجاع شونده/ارجاع دهنده
        [Route("api/Web_Data/Web_ErjUsers/{ace}/{sal}/{group}")]
        public async Task<IHttpActionResult> GetWeb_ErjUsers(string ace, string sal, string group)
        {
            if (UnitDatabase.CreateConection(ace, sal, group))
            {
                string sql = string.Format(@"Select * from Web_ErjUsers");
                var listDB = UnitDatabase.db.Database.SqlQuery<Web_ErjUsers>(sql).ToList();
                return Ok(listDB);
            }
            return null;
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

            public string EghdamComm { get; set; }

            public string SpecialComm { get; set; }

            public string FinalComm { get; set; }

            public string DocDesc { get; set; }

            public int? DocBStep { get; set; }

            public string RjRadif { get; set; }

            public int? BandNo { get; set; }

            public int? DocBMode { get; set; }

            public string RjComm { get; set; }

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
            if (UnitDatabase.CreateConection(ace, sal, group))
            {
                string sql = string.Format(CultureInfo.InvariantCulture,
                          @"select * FROM  Web_ErjDocB_Last({0}, {1},'{2}','{3}','{4}') AS ErjDocK where 1 = 1 "
                          , ErjDocB_Last.erjaMode
                          , ErjDocB_Last.docBMode
                          , ErjDocB_Last.fromUserCode
                          , ErjDocB_Last.toUserCode
                          , ErjDocB_Last.srchSt);

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

                var listErjDocB_Last = UnitDatabase.db.Database.SqlQuery<Web_ErjDocB_Last>(sql);
                return Ok(listErjDocB_Last);
            }
            return null;
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
            if (UnitDatabase.CreateConection(ace, sal, group))
            {
                string sql = string.Format(CultureInfo.InvariantCulture,
                          @"select * FROM  Web_ErjDocErja({0}) AS ErjDocErja where 1 = 1 order by BandNo,DocBMode "
                          , ErjDocErja.SerialNumber);

                var listErjDocErja = UnitDatabase.db.Database.SqlQuery<Web_ErjDocErja>(sql);
                return Ok(listErjDocErja);
            }
            return null;
        }


        // GET: api/Web_Data/KGru لیست کالا گروه ها
        [Route("api/Web_Data/KGru/{ace}/{sal}/{group}")]
        public IQueryable<Web_KGru> GetWeb_KGru(string ace, string sal, string group)
        {
            if (UnitDatabase.CreateConection(ace, sal, group))
            {
                return UnitDatabase.db.Web_KGru;
            }
            return null;
        }


        // GET: api/Web_Data/Mkz لیست مراکز هزینه
        [Route("api/Web_Data/Mkz/{ace}/{sal}/{group}")]
        public IQueryable<Web_Mkz> GetWeb_Mkz(string ace, string sal, string group)
        {
            if (UnitDatabase.CreateConection(ace, sal, group))
            {
                return UnitDatabase.db.Web_Mkz;
            }
            return null;
        }

        // GET: api/Web_Data/Opr لیست پروژه ها
        [Route("api/Web_Data/Opr/{ace}/{sal}/{group}")]
        public IQueryable<Web_Opr> GetWeb_Opr(string ace, string sal, string group)
        {
            if (UnitDatabase.CreateConection(ace, sal, group))
            {
                return UnitDatabase.db.Web_Opr;
            }
            return null;
        }

        /*   // GET: api/Web_Data/FldNames لیست ستونها
            [Route("api/Web_Data/FldNames/{ace}/{sal}/{group}")]
            public async Task<IHttpActionResult> GetWeb_FldNames(string ace, string sal, string group)
            {
                if (UnitDatabase.CreateConection(ace, sal, group))
                {
                    string sql = "EXEC WEB_FldNames";
                    var list = UnitDatabase.db.Database.SqlQuery<Web_FldNames>(sql);
                    return Ok(list);
                }
                return null;
            }
            */

        // GET: api/Web_Data/Web_RprtCols لیست ستونها
        [Route("api/Web_Data/RprtCols/{ace}/{sal}/{group}/{RprtId}/{UserCode}")]
        public IQueryable<Web_RprtCols> GetWeb_RprtCols(string ace, string sal, string group, string RprtId, string UserCode)
        {
            if (UnitDatabase.CreateConection(ace, sal, group))
            {
                return UnitDatabase.db.Web_RprtCols.Where(c => c.RprtId == RprtId && c.UserCode == UserCode && c.Visible == 1);
            }
            return null;
        }



        //-------------------------------------------------------------------------------------------------------------------------------
    }
}