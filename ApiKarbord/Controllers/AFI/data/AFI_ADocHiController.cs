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
        // [DllImport("Acc6_Web.dll", CharSet = CharSet.Unicode)]

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

        public static string CallRecoverChecks(string ConnetionString, string wDBase, string UserCode, long SerialNumber, string DarChecks, string ParChecks)
        {
            string dllName = HttpContext.Current.Server.MapPath("~/Content/Dll/Acc6_Web.dll");
            const string functionName = "RecoverChecks";

            int libHandle = LoadLibrary(dllName);
            if (libHandle == 0)
                return string.Format("Could not load library \"{0}\"", dllName);
            try
            {
                var delphiFunctionAddress = GetProcAddress(libHandle, functionName);
                if (delphiFunctionAddress == IntPtr.Zero)
                    return string.Format("Can't find function \"{0}\" in library \"{1}\"", functionName, dllName);

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
            string con = UnitDatabase.CreateConection(dataAccount[0], dataAccount[1], dataAccount[2], ace, sal, group, AFI_ADocHi_u.SerialNumber, "ADoc", 1, 0);
            if (con == "ok")
            {
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
		                            @Tanzim = '{6}',
		                            @Taeed = '{7}',
		                            @Tasvib = '{8}',
		                            @TahieShode = '{9}',
		                            @Status = N'{10}',
		                            @Spec = '{11}',
		                            @Footer = '{12}',
		                            @F01 = '{13}',
		                            @F02 = '{14}',
		                            @F03 = '{15}',
		                            @F04 = '{16}',
		                            @F05 = '{17}',
		                            @F06 = '{18}',
		                            @F07 = '{19}',
		                            @F08 = '{20}',
		                            @F09 = '{21}',
		                            @F10 = '{22}',
		                            @F11 = '{23}',
		                            @F12 = '{24}',
		                            @F13 = '{25}',
		                            @F14 = '{26}',
		                            @F15 = '{27}',
		                            @F16 = '{28}',
		                            @F17 = '{29}',
		                            @F18 = '{30}',
		                            @F19 = '{31}',
		                            @F20 = '{32}'
                            SELECT	'Return Value' = @return_value",
                            AFI_ADocHi_u.SerialNumber,
                            AFI_ADocHi_u.ModeCode,
                            AFI_ADocHi_u.DocNo,
                            AFI_ADocHi_u.DocDate ?? string.Format("{0:yyyy/MM/dd}", DateTime.Now.AddDays(-1)),
                            AFI_ADocHi_u.BranchCode,
                            AFI_ADocHi_u.UserCode,
                            AFI_ADocHi_u.Tanzim,
                            AFI_ADocHi_u.Taeed == "null" ? "" : AFI_ADocHi_u.Taeed,
                            AFI_ADocHi_u.Tasvib,
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
                            AFI_ADocHi_u.F20
                            );
                    value = UnitDatabase.db.Database.SqlQuery<string>(sql).Single();

                    await UnitDatabase.db.SaveChangesAsync();
                }
                catch (Exception e)
                {
                    throw;
                }


                sql = string.Format(@"SELECT * FROM Web_ADocB WHERE SerialNumber = {0}", AFI_ADocHi_u.SerialNumber);
                string darChecks = "";
                string parChecks = "";
                var listSanad = UnitDatabase.db.Database.SqlQuery<Web_ADocB>(sql);

                foreach (var item in listSanad)
                {
                    if (item.PDMode == 1)
                        darChecks += darChecks + item.CheckNo + ',';

                    if (item.PDMode == 2)
                        parChecks += parChecks + item.CheckNo + ',';
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

                CallRecoverChecks(
                    connectionString,
                    dbName,
                    dataAccount[2],
                    AFI_ADocHi_u.SerialNumber,
                    darChecks,
                    parChecks);

                //FreeLibrary(hDLL)
                //string log = str.ToString();

                //  StringBuilder str = new StringBuilder(256);
                //   GetVer(str);


                UnitDatabase.SaveLog(dataAccount[0], dataAccount[1], dataAccount[2], ace, sal, group, AFI_ADocHi_u.SerialNumber, "ADoc", 1, AFI_ADocHi_u.flagLog, 0);
                return Ok(value);
            }
            return Ok(con);

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
            string con = UnitDatabase.CreateConection(dataAccount[0], dataAccount[1], dataAccount[2], ace, sal, group, AFI_ADocHi_i.SerialNumber, "ADoc", 2, 0);
            if (con == "ok")
            {
                try
                {
                    sql = string.Format(
                         @" DECLARE	@return_value nvarchar(50),
		                            @DocNo_Out int
                            EXEC	@return_value = [dbo].[{38}]
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
		                            @Tanzim = '{10}',
		                            @Taeed = '{11}',
		                            @Tasvib = '{12}',
		                            @TahieShode = '{13}',
		                            @Eghdam = '''{14}''',
		                            @Status = N'{15}',
		                            @Spec = '{16}',
		                            @Footer = '{17}',
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
                            AFI_ADocHi_i.Tanzim,
                            AFI_ADocHi_i.Taeed == "null" ? "" : AFI_ADocHi_i.Taeed,
                            AFI_ADocHi_i.Tasvib,
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
                            AFI_ADocHi_i.flagTest == "Y" ? "Web_SaveADoc_HI_Temp" : "Web_SaveADoc_HI"
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


                string[] serials = value.Split('-');
                if (AFI_ADocHi_i.flagTest != "Y")
                {

                    sql = string.Format(@"SELECT * FROM Web_ADocB WHERE SerialNumber = {0}", serials[0]);
                    string darChecks = "";
                    string parChecks = "";
                    var listSanad = UnitDatabase.db.Database.SqlQuery<Web_ADocB>(sql);

                    foreach (var item in listSanad)
                    {
                        if (item.PDMode == 1)
                            darChecks += darChecks + item.CheckNo + ',';

                        if (item.PDMode == 2)
                            parChecks += parChecks + item.CheckNo + ',';
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
                        connectionString,
                        dbName,
                        dataAccount[2],
                        Convert.ToInt64(serials[0]),
                        darChecks,
                        parChecks
                        );
                }
                UnitDatabase.SaveLog(dataAccount[0], dataAccount[1], dataAccount[2], ace, sal, group, Convert.ToInt64(serials[0]), "ADoc", 2, AFI_ADocHi_i.flagLog, 0);
                return Ok(value);
            }
            return Ok(con);

        }



        // DELETE: api/AFI_ADocHi/5
        [Route("api/AFI_ADocHi/{ace}/{sal}/{group}/{SerialNumber}")]
        public async Task<IHttpActionResult> DeleteAFI_ADocHi(string ace, string sal, string group, long SerialNumber)
        {
            var dataAccount = UnitDatabase.ReadUserPassHeader(this.Request.Headers);
            string con = UnitDatabase.CreateConection(dataAccount[0], dataAccount[1], dataAccount[2], ace, sal, group, SerialNumber, "ADoc", 3, 0);
            if (con == "ok")
            {

                string sql = string.Format(@"SELECT * FROM Web_ADocB WHERE SerialNumber = {0}", SerialNumber);
                string darChecks = "";
                string parChecks = "";
                var listSanad = UnitDatabase.db.Database.SqlQuery<Web_ADocB>(sql);

                foreach (var item in listSanad)
                {
                    if (item.PDMode == 1)
                        darChecks += darChecks + item.CheckNo + ',';

                    if (item.PDMode == 2)
                        parChecks += parChecks + item.CheckNo + ',';
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

                int value = UnitDatabase.db.Database.SqlQuery<int>(sql).Single();
                if (value > 0)
                {
                    await UnitDatabase.db.SaveChangesAsync();
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
                    connectionString,
                    dbName,
                    dataAccount[2],
                    0,
                    darChecks,
                    parChecks
                    );

                UnitDatabase.SaveLog(dataAccount[0], dataAccount[1], dataAccount[2], ace, sal, group, SerialNumber, "ADoc", 3, "Y", 0);
            }
            return Ok(con);
        }

    }
}