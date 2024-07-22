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
using System.Web.Http.Results;
using ApiKarbord.Controllers.Unit;
using ApiKarbord.Models;
using Newtonsoft.Json;

namespace ApiKarbord.Controllers.AFI.data
{
    public class AFI_IDocHiController : ApiController
    {

        // PUT: api/AFI_IDocHi/5
        [Route("api/AFI_IDocHi/{ace}/{sal}/{group}")]
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutAFI_IDocHi(string ace, string sal, string group, AFI_IDocHi aFI_IDocHi)
        {
            string dBName = UnitDatabase.DatabaseName(ace, sal, group);
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
            string res = UnitDatabase.TestAcount(DBase, dataAccount[3], ace, group, aFI_IDocHi.InOut == 1 ? UnitPublic.access_IIDOC : UnitPublic.access_IODOC);
            if (res == "")
            {
                try
                {
                    string sql = string.Format(
                         @"DECLARE	@return_value nvarchar(50),
		                            @DocNo_Out int
                          EXEC	@return_value = {40}.[dbo].[Web_SaveIDoc_HU]
		                            @DOCNOMODE = {0},
		                            @INSERTMODE = {1},
		                            @MODECODE = '{2}' ,
		                            @DOCNO = {3},
		                            @STARTNO = {4},
		                            @ENDNO = {5},
		                            @BRANCHCODE = {6},
		                            @USERCODE = '{7}',
		                            @SERIALNUMBER = {8},
		                            @DOCDATE = '{9}',
		                            @SPEC = N'{10}',
		                            @TAHIESHODE = '{11}',
		                            @ThvlCODE = '{12}',
		                            @KALAPRICECODE = {13},
                                    @InvCode = '{14}',
                                    @Status = N'{15}',
                                    @Footer = N'{16}',
                                    @F01 = N'{17}',
                                    @F02 = N'{18}',
                                    @F03 = N'{19}',
                                    @F04 = N'{20}',
                                    @F05 = N'{21}',
                                    @F06 = N'{22}',
                                    @F07 = N'{23}',
                                    @F08 = N'{24}',
                                    @F09 = N'{25}',
                                    @F10 = N'{26}',
                                    @F11 = N'{27}',
                                    @F12 = N'{28}',
                                    @F13 = N'{29}',
                                    @F14 = N'{30}',
                                    @F15 = N'{31}',
                                    @F16 = N'{32}',
                                    @F17 = N'{33}',
                                    @F18 = N'{34}',
                                    @F19 = N'{35}',
                                    @F20 = N'{36}',
                                    @OprCode = '{37}',
                                    @MkzCode = '{38}',
                                    @Tanzim = '{39}',
		                            @DOCNO_OUT = @DOCNO_OUT OUTPUT
                            SELECT	'return_value' = @return_value +'-'+  CONVERT(nvarchar, @DOCNO_OUT)",
                            aFI_IDocHi.DocNoMode,
                            aFI_IDocHi.InsertMode,
                            aFI_IDocHi.ModeCode,
                            aFI_IDocHi.DocNo,
                            aFI_IDocHi.StartNo,
                            aFI_IDocHi.EndNo,
                            aFI_IDocHi.BranchCode,
                            aFI_IDocHi.UserCode,
                            aFI_IDocHi.SerialNumber,
                            aFI_IDocHi.DocDate ?? string.Format("{0:yyyy/MM/dd}", DateTime.Now.AddDays(-1)),
                            aFI_IDocHi.Spec,
                            //aFI_IDocHi.Tanzim,
                            aFI_IDocHi.TahieShode,
                            aFI_IDocHi.ThvlCode,
                            aFI_IDocHi.KalaPriceCode ?? 0,
                            aFI_IDocHi.InvCode,
                            aFI_IDocHi.Status,

                             //UnitPublic.ConvertTextWebToWin(aFI_IDocHi.Footer),
                             UnitPublic.ConvertTextWebToWin(aFI_IDocHi.Footer ?? ""),
                            //aFI_IDocHi.Taeed == "null" ? "" : aFI_IDocHi.Taeed,
                            aFI_IDocHi.F01,
                            aFI_IDocHi.F02,
                            aFI_IDocHi.F03,
                            aFI_IDocHi.F04,
                            aFI_IDocHi.F05,
                            aFI_IDocHi.F06,
                            aFI_IDocHi.F07,
                            aFI_IDocHi.F08,
                            aFI_IDocHi.F09,
                            aFI_IDocHi.F10,
                            aFI_IDocHi.F11,
                            aFI_IDocHi.F12,
                            aFI_IDocHi.F13,
                            aFI_IDocHi.F14,
                            aFI_IDocHi.F15,
                            aFI_IDocHi.F16,
                            aFI_IDocHi.F17,
                            aFI_IDocHi.F18,
                            aFI_IDocHi.F19,
                            aFI_IDocHi.F20,
                            //aFI_IDocHi.Tasvib,
                            aFI_IDocHi.OprCode,
                            aFI_IDocHi.MkzCode,
                            aFI_IDocHi.Tanzim,
                            dBName
                            );
                    value = DBase.DB.Database.SqlQuery<string>(sql).Single();

                    await DBase.DB.SaveChangesAsync();
                }
                catch (Exception e)
                {
                    throw;
                }

               // UnitDatabase.SaveLog(dataAccount[0], dataAccount[1], dataAccount[2], ace, sal, group, aFI_IDocHi.SerialNumber, aFI_IDocHi.InOut == 1 ? UnitPublic.access_IIDOC : UnitPublic.access_IODOC, 1, aFI_IDocHi.flagLog, 1, 0);
                UnitDatabase.SaveLog(dataAccount[0], dataAccount[1], dataAccount[2], ace, sal, group, aFI_IDocHi.SerialNumber, aFI_IDocHi.InOut == 1 ? UnitPublic.access_IIDOC : UnitPublic.access_IODOC, UnitPublic.act_Edit, aFI_IDocHi.flagLog, 1, 0);

                return Ok(value);
            }
            else
                return Ok(res);

            //string conStr = UnitDatabase.CreateConnectionString(dataAccount[0], dataAccount[1], dataAccount[2], dataAccount[3], ace, sal, group, aFI_IDocHi.SerialNumber, aFI_IDocHi.InOut == 1 ? UnitPublic.access_IIDOC : UnitPublic.access_IODOC, UnitPublic.act_Edit, 0);
        }

        // POST: api/AFI_IDocHi
        [Route("api/AFI_IDocHi/{ace}/{sal}/{group}")]
        [ResponseType(typeof(AFI_IDocHi))]
        public async Task<IHttpActionResult> PostAFI_IDocHi(string ace, string sal, string group, AFI_IDocHi aFI_IDocHi)
        {
            string dBName = UnitDatabase.DatabaseName(ace, sal, group);
            string value = "";
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var dataAccount = UnitDatabase.ReadUserPassHeader(this.Request.Headers);
            var DBase = UnitDatabase.dataDB.Where(p => p.UserName == dataAccount[0] && p.Password == dataAccount[1]).Single();
            string res = UnitDatabase.TestAcount(DBase, dataAccount[3], ace, group, aFI_IDocHi.InOut == 1 ? UnitPublic.access_IIDOC : UnitPublic.access_IODOC);
            if (res == "")
            {
                try
                {
                    // به دلیل اشکال در تبدیل sql کتیشن های یوز کد اینجوریه
                    string sql = string.Format(
                         @"DECLARE	@return_value nvarchar(50),
		                            @DocNo_Out int
                          EXEC	@return_value = {39}.[dbo].[{38}]
		                            @DOCNOMODE = {0},
		                            @INSERTMODE = {1},
		                            @MODECODE = '{2}' ,
		                            @DOCNO = {3},
		                            @STARTNO = {4},
		                            @ENDNO = {5},
		                            @BRANCHCODE = {6},
		                            @USERCODE = '''{7}''', 
		                            @SERIALNUMBER = {8},
		                            @DOCDATE = '{9}',
		                            @SPEC = N'{10}',
		                            @TAHIESHODE = '{11}',
		                            @ThvlCODE = '{12}',
		                            @KALAPRICECODE = {13},
                                    @InvCode = '{14}',
                                    @Eghdam = N'''{15}''',
                                    @F01 = N'{16}',
                                    @F02 = N'{17}',
                                    @F03 = N'{18}',
                                    @F04 = N'{19}',
                                    @F05 = N'{20}',
                                    @F06 = N'{21}',
                                    @F07 = N'{22}',
                                    @F08 = N'{23}',
                                    @F09 = N'{24}',
                                    @F10 = N'{25}',
                                    @F11 = N'{26}',
                                    @F12 = N'{27}',
                                    @F13 = N'{28}',
                                    @F14 = N'{29}',
                                    @F15 = N'{30}',
                                    @F16 = N'{31}',
                                    @F17 = N'{32}',
                                    @F18 = N'{33}',
                                    @F19 = N'{34}',
                                    @F20 = N'{35}',
                                    @Tanzim = '{36}',
                                    @Footer = N'{37}',
		                            @DOCNO_OUT = @DOCNO_OUT OUTPUT
                            SELECT	'return_value' = @return_value +'-'+  CONVERT(nvarchar, @DOCNO_OUT)",
                            aFI_IDocHi.DocNoMode,
                            aFI_IDocHi.InsertMode,
                            aFI_IDocHi.ModeCode,
                            aFI_IDocHi.DocNo ?? 0,
                            aFI_IDocHi.StartNo,
                            aFI_IDocHi.EndNo,
                            aFI_IDocHi.BranchCode,
                            aFI_IDocHi.UserCode,
                            aFI_IDocHi.SerialNumber,
                            aFI_IDocHi.DocDate ?? string.Format("{0:yyyy/MM/dd}", DateTime.Now.AddDays(-1)),
                            aFI_IDocHi.Spec,
                            aFI_IDocHi.TahieShode,
                            aFI_IDocHi.ThvlCode ?? "",
                            aFI_IDocHi.KalaPriceCode ?? 0,
                            aFI_IDocHi.InvCode,
                            aFI_IDocHi.Eghdam,
                            aFI_IDocHi.F01,
                            aFI_IDocHi.F02,
                            aFI_IDocHi.F03,
                            aFI_IDocHi.F04,
                            aFI_IDocHi.F05,
                            aFI_IDocHi.F06,
                            aFI_IDocHi.F07,
                            aFI_IDocHi.F08,
                            aFI_IDocHi.F09,
                            aFI_IDocHi.F10,
                            aFI_IDocHi.F11,
                            aFI_IDocHi.F12,
                            aFI_IDocHi.F13,
                            aFI_IDocHi.F14,
                            aFI_IDocHi.F15,
                            aFI_IDocHi.F16,
                            aFI_IDocHi.F17,
                            aFI_IDocHi.F18,
                            aFI_IDocHi.F19,
                            aFI_IDocHi.F20,
                            aFI_IDocHi.Tanzim,
                            UnitPublic.ConvertTextWebToWin(aFI_IDocHi.Footer ?? ""),
                            aFI_IDocHi.flagTest == "Y" ? "Web_SaveIDoc_HI_Temp" : "Web_SaveIDoc_HI",
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

                string[] serials = value.Split('-');
                if (aFI_IDocHi.flagTest != "Y")
                {
                    UnitDatabase.SaveLog(dataAccount[0], dataAccount[1], dataAccount[2], ace, sal, group, Convert.ToInt64(serials[0]), aFI_IDocHi.InOut == 1 ? UnitPublic.access_IIDOC : UnitPublic.access_IODOC, UnitPublic.act_New, aFI_IDocHi.flagLog, 1, 0);
                }
                return Ok(value);
            }
            else
                return Ok(res);

            //string conStr = UnitDatabase.CreateConnectionString(dataAccount[0], dataAccount[1], dataAccount[2], dataAccount[3], ace, sal, group, aFI_IDocHi.SerialNumber, aFI_IDocHi.InOut == 1 ? UnitPublic.access_IIDOC : UnitPublic.access_IODOC, UnitPublic.act_New, 0);
        }

        // DELETE: api/AFI_IDocHi/5
        [Route("api/AFI_IDocHi/{ace}/{sal}/{group}/{SerialNumber}/{ModeCode}")]
        [ResponseType(typeof(AFI_IDocHi))]
        public async Task<IHttpActionResult> DeleteAFI_IDocHi(string ace, string sal, string group, long SerialNumber, string ModeCode)
        {
            string dBName = UnitDatabase.DatabaseName(ace, sal, group);
            var dataAccount = UnitDatabase.ReadUserPassHeader(this.Request.Headers);
            var DBase = UnitDatabase.dataDB.Where(p => p.UserName == dataAccount[0] && p.Password == dataAccount[1]).Single();
            string res = UnitDatabase.TestAcount(DBase, dataAccount[3], ace, group, ModeCode == "1" ? UnitPublic.access_IIDOC : UnitPublic.access_IODOC);
            if (res == "")
            {
                try
                {
                    string sql = string.Format(@"DECLARE	@return_value int
                                                 EXEC	@return_value = {0}.[dbo].[Web_SaveIDoc_Del]
		                                                @SerialNumber = {1}
                                                 SELECT	'Return Value' = @return_value"
                                                , dBName, SerialNumber);

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

                UnitDatabase.SaveLog(dataAccount[0], dataAccount[1], dataAccount[2], ace, sal, group, SerialNumber, ModeCode == "1" ? UnitPublic.access_IIDOC : UnitPublic.access_IODOC, UnitPublic.act_Delete, "Y", 1, 0);
                return Ok(1);
            }
            else
                return Ok(res);

           // string conStr = UnitDatabase.CreateConnectionString(dataAccount[0], dataAccount[1], dataAccount[2], dataAccount[3], ace, sal, group, SerialNumber, ModeCode == "1" ? UnitPublic.access_IIDOC : UnitPublic.access_IODOC, UnitPublic.act_Delete, 0);
        }

















        [DllImport("kernel32.dll", EntryPoint = "LoadLibrary")]
        static extern int LoadLibrary([MarshalAs(UnmanagedType.LPStr)] string lpLibFileName);

        [DllImport("kernel32.dll", EntryPoint = "GetProcAddress")]
        static extern IntPtr GetProcAddress(int hModule, [MarshalAs(UnmanagedType.LPStr)] string lpProcName);

        [DllImport("kernel32.dll", EntryPoint = "FreeLibrary")]
        static extern bool FreeLibrary(int hModule);


        [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Unicode)]
        delegate bool RegIDoctoADoc(string ConnetionString, string wDBase, string wUserCode, string wModeCode, string SerialNumbers, StringBuilder RetVal);

        public static string CallRegIDoctoADoc(string ace, string ConnetionString, string wDBase, string wUserCode, string wModeCode, string SerialNumbers)
        {
            string dllName = ace == UnitPublic.Web8 ? "Inv6_Web.dll" : "Afi2_Web.dll";
            string dllPath = HttpContext.Current.Server.MapPath("~/Content/Dll/" + dllName);
            const string functionName = "RegIDoctoADoc";

            int libHandle = LoadLibrary(dllPath);
            if (libHandle == 0)
                return string.Format("Could not load library \"{0}\"", dllPath);
            try
            {
                var delphiFunctionAddress = GetProcAddress(libHandle, functionName);
                if (delphiFunctionAddress == IntPtr.Zero)
                    return string.Format("Can't find function \"{0}\" in library \"{1}\"", functionName, dllPath);

                var delphiFunction = (RegIDoctoADoc)Marshal.GetDelegateForFunctionPointer(delphiFunctionAddress, typeof(RegIDoctoADoc));

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
        delegate bool RegIDoctoFDoc(string ConnetionString, string wDBase, string wUserCode, string wModeCode, string SerialNumbers, StringBuilder RetVal);


        public static string CallRegIDoctoFDoc(string ace, string ConnetionString, string wDBase, string wUserCode, string wModeCode, string SerialNumbers)
        {
            string dllName = ace == UnitPublic.Web8 ? "Inv6_Web.dll" : "Afi2_Web.dll";
            string dllPath = HttpContext.Current.Server.MapPath("~/Content/Dll/" + dllName);
            const string functionName = "RegIDoctoFDoc";

            int libHandle = LoadLibrary(dllPath);
            if (libHandle == 0)
                return string.Format("Could not load library \"{0}\"", dllPath);
            try
            {
                var delphiFunctionAddress = GetProcAddress(libHandle, functionName);
                if (delphiFunctionAddress == IntPtr.Zero)
                    return string.Format("Can't find function \"{0}\" in library \"{1}\"", functionName, dllPath);

                var delphiFunction = (RegIDoctoFDoc)Marshal.GetDelegateForFunctionPointer(delphiFunctionAddress, typeof(RegIDoctoFDoc));

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



        public class RegIDoctoADocObject
        {
            public string SerialNumbers { get; set; }
            public string ModeCode { get; set; }

        }


        [Route("api/AFI_IDocHi/AFI_RegIDoctoADoc/{ace}/{sal}/{group}")]
        public async Task<IHttpActionResult> PostAFI_RegIDoctoADoc(string ace, string sal, string group, RegIDoctoADocObject RegIDoctoADocObject)
        {
            string dBName = UnitDatabase.DatabaseName(ace, sal, group);
            string log = "";
            var dataAccount = UnitDatabase.ReadUserPassHeader(this.Request.Headers);
            var DBase = UnitDatabase.dataDB.Where(p => p.UserName == dataAccount[0] && p.Password == dataAccount[1]).Single();
            string res = UnitDatabase.TestAcount(DBase, dataAccount[3], ace, group, UnitPublic.access_View);
            if (res == "")
            {
                string connectionString = string.Format(
                         @"Provider =SQLOLEDB.1;Password={0};Persist Security Info=True;User ID={1};Initial Catalog={2};Data Source={3}",
                         DBase.SqlPassword,
                         DBase.SqlUserName,
                         dBName,
                         DBase.SqlServerName
                         );

                log = CallRegIDoctoADoc(
                    ace,
                    connectionString,
                    dBName,
                    dataAccount[2],
                    RegIDoctoADocObject.ModeCode,
                    RegIDoctoADocObject.SerialNumbers
                    );

                UnitDatabase.SaveLog(dataAccount[0], dataAccount[1], dataAccount[2], ace, sal, group, 0, "FDoc", 3, "Y", 1, 0);
                return Ok("200");
            }
            else
                return Ok(res);

           // string conStr = UnitDatabase.CreateConnectionString(dataAccount[0], dataAccount[1], dataAccount[2], dataAccount[3], ace, sal, group, 0, UnitPublic.access_View, UnitPublic.act_View, 0);
        }



        public class RegIDoctoFDocObject
        {
            public string SerialNumbers { get; set; }
            public string ModeCode { get; set; }

        }


        [Route("api/AFI_IDocHi/AFI_RegIDoctoFDoc/{ace}/{sal}/{group}")]
        public async Task<IHttpActionResult> PostAFI_RegIDoctoFDoc(string ace, string sal, string group, RegIDoctoFDocObject RegIDoctoFDocObject)
        {
            string dBName = UnitDatabase.DatabaseName(ace, sal, group);
            string log = "";
            var dataAccount = UnitDatabase.ReadUserPassHeader(this.Request.Headers);
            var DBase = UnitDatabase.dataDB.Where(p => p.UserName == dataAccount[0] && p.Password == dataAccount[1]).Single();
            string res = UnitDatabase.TestAcount(DBase, dataAccount[3], ace, group, UnitPublic.access_View);
            if (res == "")
            {
                string connectionString = string.Format(
                         @"Provider =SQLOLEDB.1;Password={0};Persist Security Info=True;User ID={1};Initial Catalog={2};Data Source={3}",
                         DBase.SqlPassword,
                         DBase.SqlUserName,
                         dBName,
                         DBase.SqlServerName
                         );

                log = CallRegIDoctoFDoc(
                    ace, connectionString,
                    dBName,
                    dataAccount[2],
                    RegIDoctoFDocObject.ModeCode,
                    RegIDoctoFDocObject.SerialNumbers
                    );

                UnitDatabase.SaveLog(dataAccount[0], dataAccount[1], dataAccount[2], ace, sal, group, 0, "FDoc", 3, "Y", 1, 0);
                return Ok("200");
            }
            else
                return Ok(res);

            //string conStr = UnitDatabase.CreateConnectionString(dataAccount[0], dataAccount[1], dataAccount[2], dataAccount[3], ace, sal, group, 0, UnitPublic.access_View, UnitPublic.act_View, 0);
        }






        public class SaveAllSanadObject
        {
            public long SerialFactor { get; set; }

            public AFI_IDocHi Head { get; set; }

            public List<AFI_IDocBi> Bands { get; set; }

        }



        // POST: api/AFI_IDocHi
        [Route("api/AFI_IDocHi/SaveAllSanad/{ace}/{sal}/{group}")]
        [ResponseType(typeof(AFI_IDocBi))]
        public async Task<IHttpActionResult> PostAFI_SaveAllSanad(string ace, string sal, string group, SaveAllSanadObject o)
        {
            string dBName = UnitDatabase.DatabaseName(ace, sal, group);
            string sql = "";
            long serialNumber = 0;
            long serialNumber_Test = 0;
            var dataAccount = UnitDatabase.ReadUserPassHeader(this.Request.Headers);
            var DBase = UnitDatabase.dataDB.Where(p => p.UserName == dataAccount[0] && p.Password == dataAccount[1]).Single();
            string res = UnitDatabase.TestAcount(DBase, dataAccount[3], ace, group, o.Head.InOut == 1 ? UnitPublic.access_IIDOC : UnitPublic.access_IODOC);
            if (res == "")
            {
                try
                {
                    // save doch temp
                    sql = UnitPublic.CreateSql_IDocH(o.Head, true, dBName);
                    var value_H = DBase.DB.Database.SqlQuery<string>(sql).Single();
                    if (!string.IsNullOrEmpty(value_H))
                    {
                        await DBase.DB.SaveChangesAsync();
                    }
                    serialNumber_Test = Convert.ToInt64(value_H.Split('-')[1]);

                    // save docb temp
                    int i = 0;
                    foreach (var item in o.Bands)
                    {
                        i++;
                        sql = UnitPublic.CreateSql_IDocB(item, serialNumber_Test, i, dBName);
                        DBase.DB.Database.SqlQuery<int>(sql).Single();
                    }
                    await DBase.DB.SaveChangesAsync();

                    //test doc
                    sql = string.Format(CultureInfo.InvariantCulture,
                                            @"EXEC	{0}.[dbo].[Web_TestIDoc_Temp] @serialNumber = {1}  ,@last_SerialNumber = {2}, @UserCode = '{3}' ",
                                           dBName,
                                            serialNumber_Test,
                                           0,
                                           dataAccount[2]);
                    var result = DBase.DB.Database.SqlQuery<TestDocB>(sql).ToList();
                    var jsonResult = UnitPublic.SetErrorSanad(result);
                    if (jsonResult.Status == "Success")
                    {

                        sql = UnitPublic.CreateSql_IDocH(o.Head, false, dBName);

                        value_H = DBase.DB.Database.SqlQuery<string>(sql).Single();
                        if (!string.IsNullOrEmpty(value_H))
                        {
                            await DBase.DB.SaveChangesAsync();
                            serialNumber = Convert.ToInt64(value_H.Split('-')[0]);
                        }
                        else
                            serialNumber = o.Head.SerialNumber;

                        if (o.Head.SerialNumber > 0)
                        {
                            sql = string.Format(@"DECLARE	@return_value int
                                          EXEC	    @return_value = {0}.[dbo].[Web_SaveIDoc_BD]
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
                                    EXEC	@return_value = {0}.[dbo].[Web_SaveIDocB_Convert]
		                                    @SerialNumber = {1},
		                                    @TempSerialNumber = {2}
                                    SELECT	'Return Value' = @return_value",
                                      dBName,
                                      serialNumber,
                                      serialNumber_Test);
                        DBase.DB.Database.SqlQuery<int>(sql).Single();
                        await DBase.DB.SaveChangesAsync();


                        if (o.SerialFactor > 0)
                        {
                            sql = string.Format(CultureInfo.InvariantCulture,
                                      @"DECLARE	@return_value int
                                    EXEC	@return_value = {0}.[dbo].[Web_LinkFDocIDoc]
		                                    @FCTserial = '{1}',
		                                    @INVserial = '{2}'
                                    SELECT	'Return Value' = @return_value",
                                      dBName,
                                      o.SerialFactor,
                                        serialNumber);

                            DBase.DB.Database.SqlQuery<int>(sql).Single();
                            await DBase.DB.SaveChangesAsync();
                        }
                        jsonResult.SerialNumber = serialNumber;
                    }
                    UnitDatabase.SaveLog(dataAccount[0], dataAccount[1], dataAccount[2], ace, sal, group, o.Head.SerialNumber, o.Head.InOut == 1 ? UnitPublic.access_IIDOC : UnitPublic.access_IODOC, UnitPublic.act_New, "Y", 1, 0);

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

            //string conStr = UnitDatabase.CreateConnectionString(dataAccount[0], dataAccount[1], dataAccount[2], dataAccount[3], ace, sal, group, o.Head.SerialNumber, o.Head.InOut == 1 ? UnitPublic.access_IIDOC : UnitPublic.access_IODOC, UnitPublic.act_New, 0);
        }



    }
}