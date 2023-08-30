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
    public class AFI_ADocHiController : ApiController
    {

        /*   [DllImport("kernel32.dll", EntryPoint = "LoadLibrary")]
           static extern int LoadLibrary([MarshalAs(UnmanagedType.LPStr)] string lpLibFileName);

           [DllImport("kernel32.dll", EntryPoint = "GetProcAddress")]
           static extern IntPtr GetProcAddress(int hModule, [MarshalAs(UnmanagedType.LPStr)] string lpProcName);

           [DllImport("kernel32.dll", EntryPoint = "FreeLibrary")]
           static extern bool FreeLibrary(int hModule);


           [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
           delegate bool RecoverChecks(string ConnetionString, string wDBase, string UserCode, long SerialNumber, ref string RetVal);


           public static void CallRecoverChecks(string ConnetionString, string wDBase, string UserCode, long SerialNumber)
           {
               string dllName = "Acc6_Web.dll";//HttpContext.Current.Server.MapPath("~\\Content\\dll\\Acc6_Web.dll");
               const string functionName = "RecoverChecks";

               int libHandle = LoadLibrary(dllName);
               if (libHandle == 0)
                   throw new Exception(string.Format("Could not load library \"{0}\"", dllName));
               try
               {
                   var delphiFunctionAddress = GetProcAddress(libHandle, functionName);
                   if (delphiFunctionAddress == IntPtr.Zero)
                       throw new Exception(string.Format("Can't find function \"{0}\" in library \"{1}\"", functionName, dllName));

                   var delphiFunction = (RecoverChecks)Marshal.GetDelegateForFunctionPointer(delphiFunctionAddress, typeof(RecoverChecks));

                   const int stringBufferSize = 1024;
                   var outputStringBuffer = new String('\x00', stringBufferSize);

                   var a = delphiFunction(ConnetionString, wDBase, UserCode, SerialNumber, ref outputStringBuffer);
               }
               finally
               {
                   FreeLibrary(libHandle);
               }
           }
           */

        //string a = HttpContext.Current.Server.MapPath("~\\Content\\dll\\Acc6_Web.dll");
        //  [DllImport("Acc6_Web.dll", CharSet = CharSet.Unicode)]

        //  public static extern bool GetVer(StringBuilder RetVal);
        // public static extern bool RecoverChecks(string ConnetionString, string wDBase, string UserCode, long SerialNumber, string DarChecks, string ParChecks, StringBuilder RetVal);


        [DllImport("kernel32.dll", EntryPoint = "LoadLibrary")]
        static extern int LoadLibrary([MarshalAs(UnmanagedType.LPStr)] string lpLibFileName);

        [DllImport("kernel32.dll", EntryPoint = "GetProcAddress")]
        static extern IntPtr GetProcAddress(int hModule, [MarshalAs(UnmanagedType.LPStr)] string lpProcName);

        [DllImport("kernel32.dll", EntryPoint = "FreeLibrary")]
        static extern bool FreeLibrary(int hModule);


        [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Unicode)]
        delegate bool RecoverChecks(string ConnetionString, string wDBase, string UserCode, long SerialNumber, string DarChecks, string ParChecks, StringBuilder RetVal);

        public static string CallRecoverChecks(string ace, string ConnetionString, string wDBase, string UserCode, long SerialNumber, string DarChecks, string ParChecks)
        {
            string dllName = ace == UnitPublic.Web8 ? "Acc6_Web.dll" : "Afi2_Web.dll";
            string dllPath = HttpContext.Current.Server.MapPath("~/Content/Dll/" + dllName);
            const string functionName = "RecoverChecks";

            int libHandle = LoadLibrary(dllPath);
            if (libHandle == 0)
                return string.Format("Could not load library \"{0}\"", dllPath);
            try
            {
                var delphiFunctionAddress = GetProcAddress(libHandle, functionName);
                if (delphiFunctionAddress == IntPtr.Zero)
                    return string.Format("Can't find function \"{0}\" in library \"{1}\"", functionName, dllPath);

                var delphiFunction = (RecoverChecks)Marshal.GetDelegateForFunctionPointer(delphiFunctionAddress, typeof(RecoverChecks));

                StringBuilder RetVal = new StringBuilder(1024);
                delphiFunction(
                    ConnetionString,
                    wDBase,
                    UserCode,
                    SerialNumber,
                    DarChecks,
                    ParChecks,
                    RetVal);
                return RetVal.ToString();
            }
            finally
            {
                FreeLibrary(libHandle);
            }
        }


        public class AFI_ADocHi_i
        {

            public byte DocNoMode { get; set; }

            public byte InsertMode { get; set; }

            public string ModeCode { get; set; }

            public int DocNo { get; set; }

            public int StartNo { get; set; }

            public int EndNo { get; set; }

            public long SerialNumber { get; set; }

            public string DocDate { get; set; }

            public byte BranchCode { get; set; }

            public string UserCode { get; set; }

            public string Tanzim { get; set; }

            public string Taeed { get; set; }

            public string Tasvib { get; set; }

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

            public int DocNo_Out { get; set; }

            public string flagLog { get; set; }

            public string flagTest { get; set; }

        }


        public class AFI_ADocHi_u
        {
            public long SerialNumber { get; set; }

            public string ModeCode { get; set; }

            public int DocNo { get; set; }

            public string DocDate { get; set; }

            public byte BranchCode { get; set; }

            public string UserCode { get; set; }

            public string Tanzim { get; set; }

            public string Taeed { get; set; }

            public string Tasvib { get; set; }

            public string TahieShode { get; set; }

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

            public string flagLog { get; set; }

        }



        // PUT: api/AFI_ADocHi/5
        [Route("api/AFI_ADocHi/{ace}/{sal}/{group}")]
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutAFI_ADocHi(string ace, string sal, string group, AFI_ADocHi_u AFI_ADocHi_u)
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
            string sql = "";
            var dataAccount = UnitDatabase.ReadUserPassHeader(this.Request.Headers);
            string conStr = UnitDatabase.CreateConnectionString(dataAccount[0], dataAccount[1], dataAccount[2],dataAccount[3], ace, sal, group, AFI_ADocHi_u.SerialNumber, UnitPublic.access_ADOC, UnitPublic.act_Edit, 0);
            if (conStr.Length > 100)
            {
                ApiModel db = new ApiModel(conStr);
                try
                {
                    sql = string.Format(
                         @" DECLARE	@return_value nvarchar(50)
                            EXEC	@return_value = [dbo].[Web_SaveADoc_HU]
		                            @SerialNumber = {0},
		                            @ModeCode = '{1}',
		                            @DocNo = {2},
		                            @DocDate = '{3}',
		                            @BranchCode = {4},
		                            @UserCode = '{5}',
		                            @TahieShode = '{6}',
		                            @Status = N'{7}',
		                            @Spec = N'{8}',
		                            @Footer = N'{9}',
		                            @F01 = N'{10}',
		                            @F02 = N'{11}',
		                            @F03 = N'{12}',
		                            @F04 = N'{13}',
		                            @F05 = N'{14}',
		                            @F06 = N'{15}',
		                            @F07 = N'{16}',
		                            @F08 = N'{17}',
		                            @F09 = N'{18}',
		                            @F10 = N'{19}',
		                            @F11 = N'{20}',
		                            @F12 = N'{21}',
		                            @F13 = N'{22}',
		                            @F14 = N'{23}',
		                            @F15 = N'{24}',
		                            @F16 = N'{25}',
		                            @F17 = N'{26}',
		                            @F18 = N'{27}',
		                            @F19 = N'{28}',
		                            @F20 = N'{29}',
		                            @Tanzim = '{30}'
                            SELECT	'Return Value' = @return_value",
                            AFI_ADocHi_u.SerialNumber,
                            AFI_ADocHi_u.ModeCode,
                            AFI_ADocHi_u.DocNo,
                            AFI_ADocHi_u.DocDate ?? string.Format("{0:yyyy/MM/dd}", DateTime.Now.AddDays(-1)),
                            AFI_ADocHi_u.BranchCode,
                            AFI_ADocHi_u.UserCode,
                            //AFI_ADocHi_u.Tanzim,
                            //AFI_ADocHi_u.Taeed == "null" ? "" : AFI_ADocHi_u.Taeed,
                            //AFI_ADocHi_u.Tasvib,
                            AFI_ADocHi_u.TahieShode,
                            AFI_ADocHi_u.Status,
                            AFI_ADocHi_u.Spec,
                            UnitPublic.ConvertTextWebToWin(AFI_ADocHi_u.Footer ?? ""),
                            AFI_ADocHi_u.F01,
                            AFI_ADocHi_u.F02,
                            AFI_ADocHi_u.F03,
                            AFI_ADocHi_u.F04,
                            AFI_ADocHi_u.F05,
                            AFI_ADocHi_u.F06,
                            AFI_ADocHi_u.F07,
                            AFI_ADocHi_u.F08,
                            AFI_ADocHi_u.F09,
                            AFI_ADocHi_u.F10,
                            AFI_ADocHi_u.F11,
                            AFI_ADocHi_u.F12,
                            AFI_ADocHi_u.F13,
                            AFI_ADocHi_u.F14,
                            AFI_ADocHi_u.F15,
                            AFI_ADocHi_u.F16,
                            AFI_ADocHi_u.F17,
                            AFI_ADocHi_u.F18,
                            AFI_ADocHi_u.F19,
                            AFI_ADocHi_u.F20,
                            AFI_ADocHi_u.Tanzim
                            );
                    value = db.Database.SqlQuery<string>(sql).Single();

                    await db.SaveChangesAsync();
                }
                catch (Exception e)
                {
                    throw;
                }


                sql = string.Format(@"SELECT * FROM Web_ADocB WHERE SerialNumber = {0}", AFI_ADocHi_u.SerialNumber);
                string darChecks = "";
                string parChecks = "";
                var listSanad = db.Database.SqlQuery<Web_ADocB>(sql);

                foreach (var item in listSanad)
                {
                    if (item.PDMode == 1)
                        darChecks += item.CheckNo + ',';

                    if (item.PDMode == 2)
                        parChecks += item.CheckNo + ',';
                }

                if (darChecks.Length > 0)
                    darChecks = darChecks.Remove(darChecks.Length - 1);

                if (parChecks.Length > 0)
                    parChecks = parChecks.Remove(parChecks.Length - 1);


                string dbName = UnitDatabase.DatabaseName(ace, sal, group);

                //   public static extern bool RecoverChecks(string ConnetionString, string wDBase, string UserCode, long SerialNumber, string DarChecks, string ParChecks, StringBuilder RetVal);

                StringBuilder str = new StringBuilder(1024);

                string connectionString = string.Format(
                         @"Provider =SQLOLEDB.1;Password={0};Persist Security Info=True;User ID={1};Initial Catalog={2};Data Source={3}",
                         UnitDatabase.SqlPassword,
                         UnitDatabase.SqlUserName,
                         dbName,
                         UnitDatabase.SqlServerName
                         );

                //string dllPath = @"C:\a\Acc6_Web.dll";
                //Assembly a = Assembly.Load(dllPath);

                string log = CallRecoverChecks(
                    ace,
                    connectionString,
                    dbName,
                    dataAccount[2],
                    AFI_ADocHi_u.SerialNumber,
                    darChecks,
                    parChecks);


                UnitDatabase.SaveLog(dataAccount[0], dataAccount[1], dataAccount[2], ace, sal, group, AFI_ADocHi_u.SerialNumber, UnitPublic.access_ADOC, 1, AFI_ADocHi_u.flagLog, 1, 0);
                return Ok(value);
            }
            return Ok(conStr);

        }

        // POST: api/AFI_ADocHi
        [Route("api/AFI_ADocHi/{ace}/{sal}/{group}")]
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PostAFI_ADocHi(string ace, string sal, string group, AFI_ADocHi_i AFI_ADocHi_i)
        {
            string value = "";
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            string sql = "";
            var dataAccount = UnitDatabase.ReadUserPassHeader(this.Request.Headers);
            string conStr = UnitDatabase.CreateConnectionString(dataAccount[0], dataAccount[1], dataAccount[2],dataAccount[3], ace, sal, group, AFI_ADocHi_i.SerialNumber, UnitPublic.access_ADOC, UnitPublic.act_New, 0);
            if (conStr.Length > 100)
            {
                ApiModel db = new ApiModel(conStr);
                try
                {
                    sql = string.Format(
                         @" DECLARE	@return_value nvarchar(50),
		                            @DocNo_Out int
                            EXEC	@return_value = [dbo].[{36}]
		                            @DocNoMode = {0},
		                            @InsertMode = {1},
		                            @ModeCode = '{2}',
		                            @DocNo = {3},
		                            @StartNo = {4},
		                            @EndNo = {5},
		                            @SerialNumber = {6},
                                    @DocDate = '{7}',
		                            @BranchCode = {8},
		                            @UserCode = '''{9}''',
		                            @TahieShode = '{10}',
		                            @Eghdam = '''{11}''',
		                            @Status = N'{12}',
		                            @Spec = N'{13}',
		                            @Footer = N'{14}',
		                            @F01 = N'{15}',
		                            @F02 = N'{16}',
		                            @F03 = N'{17}',
		                            @F04 = N'{18}',
		                            @F05 = N'{19}',
		                            @F06 = N'{20}',
		                            @F07 = N'{21}',
		                            @F08 = N'{22}',
		                            @F09 = N'{23}',
		                            @F10 = N'{24}',
		                            @F11 = N'{25}',
		                            @F12 = N'{26}',
		                            @F13 = N'{27}',
		                            @F14 = N'{28}',
		                            @F15 = N'{29}',
		                            @F16 = N'{30}',
		                            @F17 = N'{31}',
		                            @F18 = N'{32}',
		                            @F19 = N'{33}',
		                            @F20 = N'{34}',		                            
		                            @Tanzim = '{35}',		                            
		                            @DocNo_Out = @DocNo_Out OUTPUT
                            SELECT	'return_value' = @return_value +'-'+  CONVERT(nvarchar, @DOCNO_OUT)",
                            AFI_ADocHi_i.DocNoMode,
                            AFI_ADocHi_i.InsertMode,
                            AFI_ADocHi_i.ModeCode,
                            AFI_ADocHi_i.DocNo,
                            AFI_ADocHi_i.StartNo,
                            AFI_ADocHi_i.EndNo,
                            AFI_ADocHi_i.SerialNumber,
                            AFI_ADocHi_i.DocDate ?? string.Format("{0:yyyy/MM/dd}", DateTime.Now.AddDays(-1)),
                            AFI_ADocHi_i.BranchCode,
                            AFI_ADocHi_i.UserCode,
                            //AFI_ADocHi_i.Tanzim,
                            //AFI_ADocHi_i.Taeed == "null" ? "" : AFI_ADocHi_i.Taeed,
                            //AFI_ADocHi_i.Tasvib,
                            AFI_ADocHi_i.TahieShode,
                            AFI_ADocHi_i.Eghdam,
                            AFI_ADocHi_i.Status,
                            AFI_ADocHi_i.Spec,
                            UnitPublic.ConvertTextWebToWin(AFI_ADocHi_i.Footer ?? ""),
                            AFI_ADocHi_i.F01,
                            AFI_ADocHi_i.F02,
                            AFI_ADocHi_i.F03,
                            AFI_ADocHi_i.F04,
                            AFI_ADocHi_i.F05,
                            AFI_ADocHi_i.F06,
                            AFI_ADocHi_i.F07,
                            AFI_ADocHi_i.F08,
                            AFI_ADocHi_i.F09,
                            AFI_ADocHi_i.F10,
                            AFI_ADocHi_i.F11,
                            AFI_ADocHi_i.F12,
                            AFI_ADocHi_i.F13,
                            AFI_ADocHi_i.F14,
                            AFI_ADocHi_i.F15,
                            AFI_ADocHi_i.F16,
                            AFI_ADocHi_i.F17,
                            AFI_ADocHi_i.F18,
                            AFI_ADocHi_i.F19,
                            AFI_ADocHi_i.F20,
                            AFI_ADocHi_i.Tanzim,
                            AFI_ADocHi_i.flagTest == "Y" ? "Web_SaveADoc_HI_Temp" : "Web_SaveADoc_HI"
                            );
                    value = db.Database.SqlQuery<string>(sql).Single();
                    if (!string.IsNullOrEmpty(value))
                    {
                        await db.SaveChangesAsync();
                    }
                }
                catch (Exception e)
                {
                    throw;
                }


                string[] serials = value.Split('-');
                if (AFI_ADocHi_i.flagTest != "Y")
                {

                    sql = string.Format(@"SELECT * FROM Web_ADocB WHERE SerialNumber = {0}", serials[0]);
                    string darChecks = "";
                    string parChecks = "";
                    var listSanad = db.Database.SqlQuery<Web_ADocB>(sql);

                    foreach (var item in listSanad)
                    {
                        if (item.PDMode == 1)
                            darChecks += item.CheckNo + ',';

                        if (item.PDMode == 2)
                            parChecks += item.CheckNo + ',';
                    }

                    if (darChecks.Length > 0)
                        darChecks = darChecks.Remove(darChecks.Length - 1);

                    if (parChecks.Length > 0)
                        parChecks = parChecks.Remove(parChecks.Length - 1);


                    string dbName = UnitDatabase.DatabaseName(ace, sal, group);
                    string connectionString = string.Format(
                             @"Provider =SQLOLEDB.1;Password={0};Persist Security Info=True;User ID={1};Initial Catalog={2};Data Source={3}",
                             UnitDatabase.SqlPassword,
                             UnitDatabase.SqlUserName,
                             dbName,
                             UnitDatabase.SqlServerName
                             );


                    string log = CallRecoverChecks(
                        ace,
                        connectionString,
                        dbName,
                        dataAccount[2],
                        Convert.ToInt64(serials[0]),
                        darChecks,
                        parChecks
                        );
                }
                UnitDatabase.SaveLog(dataAccount[0], dataAccount[1], dataAccount[2], ace, sal, group, Convert.ToInt64(serials[0]), UnitPublic.access_ADOC, 2, AFI_ADocHi_i.flagLog, 1, 0);
                return Ok(value);
            }
            return Ok(conStr);

        }



        // DELETE: api/AFI_ADocHi/5
        [Route("api/AFI_ADocHi/{ace}/{sal}/{group}/{SerialNumber}")]
        public async Task<IHttpActionResult> DeleteAFI_ADocHi(string ace, string sal, string group, long SerialNumber)
        {
            var dataAccount = UnitDatabase.ReadUserPassHeader(this.Request.Headers);
            string conStr = UnitDatabase.CreateConnectionString(dataAccount[0], dataAccount[1], dataAccount[2],dataAccount[3], ace, sal, group, SerialNumber, UnitPublic.access_ADOC, UnitPublic.act_Delete, 0);
            if (conStr.Length > 100)
            {
                ApiModel db = new ApiModel(conStr);
                string sql = string.Format(@"SELECT * FROM Web_ADocB WHERE SerialNumber = {0}", SerialNumber);
                string darChecks = "";
                string parChecks = "";
                var listSanad = db.Database.SqlQuery<Web_ADocB>(sql);

                foreach (var item in listSanad)
                {
                    if (item.PDMode == 1)
                        darChecks += item.CheckNo + ',';

                    if (item.PDMode == 2)
                        parChecks += item.CheckNo + ',';
                }

                if (darChecks.Length > 0)
                    darChecks = darChecks.Remove(darChecks.Length - 1);

                if (parChecks.Length > 0)
                    parChecks = parChecks.Remove(parChecks.Length - 1);

                sql = string.Format(@"DECLARE	@return_value int
                                                 EXEC	@return_value = [dbo].[Web_SaveADoc_Del]
		                                                @SerialNumber = {0}
                                                 SELECT	'Return Value' = @return_value"
                                        , SerialNumber);

                int value = db.Database.SqlQuery<int>(sql).Single();
                if (value > 0)
                {
                    await db.SaveChangesAsync();
                }

                string dbName = UnitDatabase.DatabaseName(ace, sal, group);


                string connectionString = string.Format(
                         @"Provider =SQLOLEDB.1;Password={0};Persist Security Info=True;User ID={1};Initial Catalog={2};Data Source={3}",
                         UnitDatabase.SqlPassword,
                         UnitDatabase.SqlUserName,
                         dbName,
                         UnitDatabase.SqlServerName
                         );

                string log = CallRecoverChecks(
                    ace, connectionString,
                    dbName,
                    dataAccount[2],
                    0,
                    darChecks,
                    parChecks
                    );

                UnitDatabase.SaveLog(dataAccount[0], dataAccount[1], dataAccount[2], ace, sal, group, SerialNumber, UnitPublic.access_ADOC, 3, "Y", 1, 0);
            }
            return Ok(conStr);
        }

    }
}