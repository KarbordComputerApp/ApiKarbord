using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ApiKarbord.Models;
using System.Data.SqlClient;
using System.Web.Mvc;
using System.Net.Http;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Globalization;
using System.Threading.Tasks;
using System.Web.Http;
using System.IO;

namespace ApiKarbord.Controllers.Unit
{
    public static class UnitDatabase
    {
        // public static ApiModel db;
        //public static ApiModel dbChange;




        class testMaster
        {
            public string TABLE_SCHEMA { get; set; }
        }

        //اتصال به دیتابیس با توجه به گروه و سال 

        public static string DatabaseName(string ace, string sal, string group)
        {
            string dbName;
            if (string.IsNullOrEmpty(ace) || string.IsNullOrEmpty(sal) || string.IsNullOrEmpty(group))
            {
                if (ace == "Config")
                {
                    return "Ace_WebConfig";
                }
                else if (ace == "Master")
                {
                    return "Master";
                }
                else
                    return null;
            }
            else
            {
                dbName = "ACE_" + ace;
                dbName += group + sal;
                return dbName;
            }
        }

        public static string UnEncript(string value)
        {
            int temp;
            if (value != "" && value != null)
            {
                string[] User = value.Split(',');
                int count = Int32.Parse(User[User.Length - 1]);
                char[] c = new char[count];
                for (int i = 0; i < User.Length - 1; i++)
                {
                    temp = Int32.Parse(User[i]) / 1024;
                    c[i] = (Char)temp;
                }
                return new string(c);
            }
            else
                return null;
        }


        //Debug

        //static string addressApiAccounting = "http://127.0.0.1:902/";
        // static string addressApiAccounting = "http://localhost:49961/";

        // static string addressApiAccounting = "http://192.168.6.204:902/"; //  Canada
        // static string addressApiAccounting = "http://192.168.0.109:902/"; //  Office 109
        // static string addressApiAccounting = "http://185.208.174.64:902/";//  Interanet


        static string IniPath = HttpContext.Current.Server.MapPath("~/Content/ini/ServerConfig.Ini");
        static string IniLogPath = HttpContext.Current.Server.MapPath("~/Content/ini/SysLog.Ini");

        static IniFile MyIni = new IniFile(IniPath);
        static IniFile MyIniLog = new IniFile(IniLogPath);
        public static PersianCalendar persianCalendar = new System.Globalization.PersianCalendar();

        public static string addressApiAccounting = MyIni.Read("serverName");
        public static string addressFileSql = MyIni.Read("FileSql");
        public static string addressPrintForms = MyIni.Read("PrintForms");
        public static string lockNumber;

        public static string SqlServerName;
        public static string SqlUserName;
        public static string SqlPassword;

        public static List<Access> model = null;

        //ایجاد کانکشن استرینگ 
        //اگر سایت ترو باشد یعنی به اس کیو ال ای پی ای
        /*
        public static string CreateConnectionString(string userName, string password, string userKarbord, string device, string ace, string sal, string group, long serialNumber, string modecode, int act, int bandNo)
        {

            // addressApiAccounting = MyIni.Read("serverName");
            string IniLogPath = HttpContext.Current.Server.MapPath("~/Content/ini/SysLog.Ini");

            IniFile MyIniLog = new IniFile(IniLogPath);

            PersianCalendar pc = new System.Globalization.PersianCalendar();

            MyIniLog.Write("DateTime.Now", DateTime.Now.ToString());
            MyIniLog.Write("GetYear", pc.GetYear(DateTime.Now).ToString());
            MyIniLog.Write("GetMonth", pc.GetMonth(DateTime.Now).ToString());
            MyIniLog.Write("GetDayOfMonth", pc.GetDayOfMonth(DateTime.Now).ToString());

            string year = pc.GetYear(DateTime.Now).ToString();
            string month = pc.GetMonth(DateTime.Now).ToString();
            string day = pc.GetDayOfMonth(DateTime.Now).ToString();


            month = month.Length == 1 ? "0" + month : month;
            day = day.Length == 1 ? "0" + day : day;

            string PDate = year + "/" + month + "/" + day;

            //string PDate = string.Format("{0:yyyy/MM/dd}", Convert.ToDateTime(pc.GetYear(DateTime.Now).ToString() + "/" + pc.GetMonth(DateTime.Now).ToString() + "/" + pc.GetDayOfMonth(DateTime.Now).ToString()));

            MyIniLog.Write("PDate", PDate);
            try
            {
                string address = String.Format(addressApiAccounting + "api/Account/InformationSql/{0}/{1}", userName, password);
                MyIniLog.Write("address", address);

                var client = new HttpClient();

                var task = client.GetAsync(address)
                  .ContinueWith((taskwithresponse) =>
                  {
                      var response = taskwithresponse.Result;
                      var jsonString = response.Content.ReadAsStringAsync();
                      jsonString.Wait();
                      MyIniLog.Write("jsonString.Result", jsonString.Result);
                      model = JsonConvert.DeserializeObject<List<Access>>(jsonString.Result);
                  });
                task.Wait();

                var list = model.First();
                //Data.Add(list.SqlServerName);
                //Data.Add(list.SqlUserName);
                //Data.Add(list.SqlPassword);

                if (list.toDate != "" && list.fromDate != "")
                {
                    if (UnitPublic.GetPersianDaysDiffDate(list.toDate, PDate) > 0)
                    {
                        //Data.Add("Expire Account");
                        return "Expire Account";
                    }
                }

                if (list.active == false)
                {
                    // Data.Add("Disable Account");
                    return "Disable Account";
                }

                if (device == "" && list.IsApi == false)
                {
                    return "Not Access Api";
                }

                if (device == "Web" && list.IsWeb == false)
                {
                    return "Not Access Web";
                }

                if (device == "App" && list.IsApp == false)
                {
                    return "Not Access App";
                }

                string[] listAccess = new string[0];
                string[] listGroup = new string[0];
                bool fullAccess = false;
                bool groupAccess = false;
                bool accept = false;

                if (ace == "web1" || ace == "WEB1") ace = UnitPublic.Web1;
                if (ace == "web2" || ace == "WEB2") ace = UnitPublic.Web2;
                if (ace == "web8" || ace == "WEB8") ace = UnitPublic.Web8;


                if (model.Count > 0)
                {
                    SqlServerName = list.SqlServerName;
                    SqlUserName = list.SqlUserName;
                    SqlPassword = list.SqlPassword;

                    if (ace == UnitPublic.Web1)
                    {
                        fullAccess = list.AFI1_Access == "*";
                        listAccess = list.AFI1_Access.Split('*');
                        listGroup = list.AFI1_Group.Split('-');
                        groupAccess = listGroup.Contains(group);
                    }
                    else if (ace == UnitPublic.Web2)
                    {
                        fullAccess = list.ERJ_Access == "*";
                        listAccess = list.ERJ_Access.Split('*');
                        listGroup = list.ERJ_Group.Split('-');
                        groupAccess = listGroup.Contains(group);
                    }
                    else if (ace == UnitPublic.Web8)
                    {
                        fullAccess = list.AFI8_Access == "*";
                        listAccess = list.AFI8_Access.Split('*');
                        listGroup = list.AFI8_Group.Split('-');
                        groupAccess = listGroup.Contains(group);
                    }
                    else
                    {
                        groupAccess = true;
                        fullAccess = true;
                    }


                    if (fullAccess == false)
                    {
                        string mode = UnitPublic.ModeCodeConnection(modecode);
                        if (mode == UnitPublic.access_View || mode == UnitPublic.access_Chante_FDoc_Moved)
                            accept = true;
                        //else if (modeCodes.Contains(mode.ToUpper()) )
                        else
                            accept = listAccess.Contains(mode);
                        //else     accept = true;
                    }


                    if (fullAccess == true) accept = true;
                    if (groupAccess == false) accept = false;

                    if (accept)
                    {
                        string connectionString = String.Format(
                                        @"data source = {0};initial catalog = {1};persist security info = True;user id = {2}; password = {3};  multipleactiveresultsets = True; application name = EntityFramework",
                                        list.SqlServerName, DatabaseName(ace, sal, group), list.SqlUserName, list.SqlPassword);
                        MyIniLog.Write("connectionString_10", connectionString);

                        if (act > 0)
                        {
                            SaveLog(userName, password, userKarbord, ace, sal, group, serialNumber, modecode, act, "Y", 0, bandNo);
                        }

                        return connectionString;
                    }
                    else
                    {
                        return groupAccess == false ? "Not access to the group" : "Not access to the method";
                    }
                }
                else
                    // Data.Add("");
                    return "";

                //return Data;
            }
            catch (Exception e)
            {
                MyIniLog.Write("Exception_10", e.Message.ToString());
                return null;
                throw;
            }
        }
        */
        public static string CreateConnectionString(bool api)
        {
            try
            {
                string serverName;
                string userName;
                string password;
                if (api)
                {
                    serverName = UnitPublic.MyIni.Read("serverName");
                    userName = UnitPublic.MyIni.Read("userName");
                    password = UnitPublic.MyIni.Read("password");
                }
                else
                {
                    serverName = UnitPublic.MyIniServer.Read("serverName");
                    userName = UnitPublic.MyIniServer.Read("userName");
                    password = UnitPublic.MyIniServer.Read("password");
                }
                string connection = String.Format(
                                    @"data source = {0};initial catalog = {1};persist security info = True;user id = {2}; password = {3}; multipleactiveresultsets = True; application name = EntityFramework",
                   serverName, "master", userName, password);
                return connection;
            }
            catch (Exception)
            {
                return null;
                throw;
            }
        }


        //public static readonly object LockObject = new object();
        //اگر سایت ترو باشد یعنی به اس کیو ال ای پی ای
        /* public static string CreateConection(string userName, string password, string userKarbord, string ace, string sal, string group, long serialnumber, string modecode, int act, int bandNo)
         {
             string IniLogPath = HttpContext.Current.Server.MapPath("~/Content/ini/SysLog.Ini");
             string res = "";
             IniFile MyIniLog = new IniFile(IniLogPath);
             try
             {
                 MyIniLog.Write("1001 Start", "ok");
                 //lock (LockObject)
                // {
                     string conStr = CreateConnectionString(userName, password, userKarbord, ace, sal, group, serialnumber, modecode, act, bandNo);
                     MyIniLog.Write("conStr", conStr);
                     if (conStr.Length > 100)
                     {
                         UnitPublic.conString = conStr;
                         MyIniLog.Write("conStr_", UnitPublic.conString);
                         db = new ApiModel(conStr);
                         if (ace == "Config" && group == "00")
                         {
                             //string sql = string.Format("SELECT count(ID) as id FROM Ace_Config.dbo.UserIn WHERE ProgName IN ('Web1', 'Web2', 'Web8')");
                             // string countUserIn = db.Database.SqlQuery<int>(sql).First().ToString();
                             // var list = model.First();
                             // int userCount = list.userCount ?? 0;
                             //  if (Int32.Parse(countUserIn) >= userCount)
                             //  {
                             //     return "MaxCount";
                             // }

                             MyIniLog.Write("conStr_1", ace);
                             res = ChangeDatabaseConfig(userKarbord, sal, userName, password);
                             if (res == "UseLog")
                             {
                                 return res;

                             }
                             db = new ApiModel(conStr);
                         }
                         return "ok";
                     }
                     else
                         return conStr;
                // }
             }
             catch (Exception e)
             {
                 MyIniLog.Write("error", e.Message.ToString());
                 return "error";
                 throw;
             }
         }*/

        //اگر سایت ترو باشد یعنی به اس کیو ال ای پی ای
        /*  public static Boolean TestSqlServer(bool api)
          {
              try
              {
                  string conStr = CreateConnectionString(api);
                  db = new ApiModel(conStr);
                  var table = db.Database.SqlQuery<testMaster>("use master SELECT TABLE_SCHEMA FROM INFORMATION_SCHEMA.TABLES");
                  if (table.Count() == 0) //اگر جدولی در دیتابیس مستر پیدا نشد 
                  {
                      return false;
                  }
                  else
                  {
                      return true;
                  }
              }
              catch (Exception)
              {
                  return false;
                  throw;
              }
          }
          */



        public static List<string> ReadUserPassHeader(HttpRequestHeaders header)
        {
            List<string> list = new List<string>();

            string userName = string.Empty;
            string password = string.Empty;
            string userKarbord = string.Empty;
            string device = string.Empty;


            if (header.Contains("userName"))
            {
                userName = header.GetValues("userName").First();
            }
            if (header.Contains("password"))
            {
                password = header.GetValues("password").First();
            }
            if (header.Contains("userKarbord"))
            {
                userKarbord = header.GetValues("userKarbord").First();
            }

            if (header.Contains("device"))
            {
                device = header.GetValues("device").First();
            }

            list.Add(userName);
            list.Add(password);
            list.Add(userKarbord);
            list.Add(device);

            return (list);
        }



        // ذخیره لاگ
        public static void SaveLog(string userName, string password, string userKarbord, string ace, string sal, string group, long serialNumber, string modecode, int act, string ins, int flag, int bandNo)
        {
            try
            {
                if (addressApiAccounting != "")
                {

                    if (ins == "Y")
                    {
                        var client = new HttpClient();

                        string address = String.Format(addressApiAccounting + "api/Account/Log/{0}/{1}/{2}/{3}/{4}/{5}/{6}/{7}/{8}/{9}/{10}", userName, password, userKarbord, ace, group, sal, serialNumber, modecode, act, flag, bandNo);

                        var task = client.GetAsync(address);
                        /*.ContinueWith((taskwithresponse) =>
                        {
                            var response = taskwithresponse.Result;
                            var jsonString = response.Content.ReadAsStringAsync();
                            jsonString.Wait();
                        });
                      task.Wait();*/
                    }
                }
            }
            catch (Exception e)
            {
                throw;
            }
        }




        public static string ChangeDatabase(string ace, string sal, string group, string userCode, bool auto, string lockNumber, string srv_User, string srv_Pass, string device)
        {

            string dbName;

            string[] filePaths = Directory.GetFiles(addressFileSql + "\\", "*.txt",
                                             SearchOption.TopDirectoryOnly);

            bool isCols = false;

            if (!System.IO.Directory.Exists(addressFileSql + "\\" + lockNumber))
                System.IO.Directory.CreateDirectory(addressFileSql + "\\" + lockNumber);


            string fileLog = addressFileSql + "\\" + lockNumber + "\\" + ace + "_" + group + "_" + sal + ".txt";


            if (File.Exists(fileLog))
            {
                try
                {
                    File.Delete(fileLog);
                }
                catch (Exception e)
                {
                    return "UseLog";
                }

            }

            StreamWriter sw = File.CreateText(fileLog);

            foreach (var item in filePaths)
            {

                isCols = false;
                string fileName = Path.GetFileName(item);
                var files = fileName.Split('_');
                if (files.Length >= 5)
                {
                    if (files[3].Length == 1)
                        files[3] = "0" + files[3];

                    if (files[1] == lockNumber && files[3] == group)//&& (files[1] != "Web8" && files[4] != "0000.txt"))
                    {
                        sw.WriteLine("fileName : " + fileName);
                        string addressFile = item;
                        //sal = (files[4].Split('.'))[0];
                        //group = files[3];
                        string salTemp = sal;
                        if (files[2] == "Web2")
                            salTemp = "0000";

                        dbName = ("ACE_" + files[2] + group + salTemp);

                        if ((files[2] == UnitPublic.Web1 || files[2] == UnitPublic.Web8) && (files[4] == "0000.txt" || files[4] == "0000"))
                        {
                            dbName = ("ACE_" + files[2] + group + "0000");
                        }

                        if ((files[2] == UnitPublic.Web8 || files[2] == UnitPublic.Web1) && salTemp == "0000")
                        {
                            sw.WriteLine("Break For Error Name Database : " + dbName);
                        }
                        else
                        {
                            if ((files[2] == UnitPublic.Web1 || files[2] == UnitPublic.Web8) && (files[4] == "0000.txt" || files[4] == "0000"))
                            {
                                salTemp = "0000";
                            }


                            if (files.Length == 6)
                            {
                                string nameTemp = (files[5].Split('.'))[0];
                                if (nameTemp == "Col")
                                {
                                    isCols = true;
                                }
                            }

                            sw.WriteLine("dbName : " + dbName);

                            var DBase = UnitDatabase.dataDB.Where(p => p.UserName.ToUpper() == srv_User.ToUpper() && p.Password == srv_Pass).Single();

                            //string connectionString = CreateConnectionString(srv_User, srv_Pass, device, "", "Master", "", "", 0, "", 0, 0);

                            string connectionString = String.Format(
                                            @"data source = {0};initial catalog = {1};persist security info = True;user id = {2}; password = {3};  multipleactiveresultsets = True; application name = EntityFramework",
                                            DBase.SqlServerName, "Master", DBase.SqlUserName, DBase.SqlPassword);

                            sw.WriteLine("connectionString : " + connectionString);

                            var connection = new SqlConnection(connectionString);

                            connection.Open();
                            var command = connection.CreateCommand();
                            command.CommandText = string.Format(@"IF Not EXISTS (SELECT name FROM master.dbo.sysdatabases WHERE name = N'{0}')
                                                            CREATE DATABASE [{0}] COLLATE SQL_Latin1_General_CP1256_CI_AS", dbName);
                            command.ExecuteNonQuery();
                            connection.Close();

                            string res = UnitDatabase.TestAcount(DBase, userCode, ace, group, UnitPublic.access_View);

                            if (res == "")
                            {
                                //string conStr = CreateConnectionString(srv_User, srv_Pass, device, "", files[2] == "Ace2.txt" ? "Config" : files[2], salTemp, group, 0, "", 0, 0);
                                //if (conStr.Length > 100)
                                //{
                                //    dbChange = new ApiModel(conStr);
                                //}

                                string conStr = String.Format(@"data source = {0};initial catalog = {1};persist security info = True;user id = {2}; password = {3};  multipleactiveresultsets = True; application name = EntityFramework",
                                DBase.SqlServerName, dbName, DBase.SqlUserName, DBase.SqlPassword);
                                var DB = new ApiModel(conStr);

                                string sql;
                                int oldVer = 0;
                                try
                                {
                                    try
                                    {
                                        sql = string.Format(@"if (select count(id) from dbo.web_version) = 0
                                                                select 0
                                                              else
                                                                select ver from dbo.web_version where id = (select max(id) from dbo.web_version)");
                                        oldVer = DB.Database.SqlQuery<int>(sql).Single();
                                    }
                                    catch (Exception e)
                                    {
                                        sql = string.Format(@"CREATE TABLE [dbo].[Web_Version] (
                                                                     [id][int] IDENTITY(1,1) NOT NULL,
                                                                     [ver] [int] NULL,
                                                                     [datever] [datetime] NULL,
                                                             CONSTRAINT[PK_web_ver] PRIMARY KEY CLUSTERED
                                                             ([id] ASC)WITH(PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON[PRIMARY]) ON[PRIMARY]");
                                        DB.Database.ExecuteSqlCommand(sql);
                                    }

                                    sw.WriteLine("oldVer : " + oldVer.ToString());
                                    sw.WriteLine("VerDB : " + UnitPublic.VerDB);

                                    if (oldVer < UnitPublic.VerDB || auto == false)
                                    {
                                        string IniConfigPath = addressFileSql + "\\" + lockNumber + "\\Config_" + ace + group + salTemp + ".ini";

                                        IniFile MyIniConfig = new IniFile(IniConfigPath);

                                        MyIniConfig.Write("Change", "1");
                                        MyIniConfig.Write("BeginDate", DateTime.Now.ToString());
                                        MyIniConfig.Write("User", userCode);
                                        MyIniConfig.Write("Prog", ace);
                                        MyIniConfig.Write("Group", group);
                                        MyIniConfig.Write("Sal", salTemp);


                                        if (isCols == false)
                                        {

                                            sw.WriteLine("Start Delete All");
                                            sql = string.Format(@"
                                                         --IF EXISTS(SELECT * FROM sys.tables WHERE SCHEMA_NAME(schema_id) LIKE 'dbo' AND name like 'Web_Flds') drop table Web_Flds
                                                         --IF EXISTS(SELECT * FROM sys.tables WHERE SCHEMA_NAME(schema_id) LIKE 'dbo' AND name like 'Web_T_ADOCB') drop table Web_T_ADOCB
                                                         --IF EXISTS(SELECT * FROM sys.tables WHERE SCHEMA_NAME(schema_id) LIKE 'dbo' AND name like 'Web_T_ADOCH') drop table Web_T_ADOCH
                                                    DECLARE @sql VARCHAR(MAX) = '' 
                                                    DECLARE @crlf VARCHAR(2) = CHAR(13) + CHAR(10)
                                                    SELECT @sql = @sql + 'DROP VIEW ' + QUOTENAME(SCHEMA_NAME(schema_id)) + '.' + QUOTENAME(v.name) +';' + @crlf
                                                    FROM   sys.views v
                                                    EXEC(@sql);
                                                      declare @procName varchar(500)
                                                      declare cur cursor
                                                      for select name from sys.objects where type = 'if' or type = 'tf' or type = 'fn'
                                                      open cur
                                                      fetch next from cur into @procName
                                                      while @@fetch_status = 0
                                                      begin
                                                          exec('drop function [' + @procName + ']')
                                                          fetch next from cur into @procName
                                                      end
                                                      close cur
                                                      deallocate cur
                                                      declare cur cursor
                                                      for select name from sys.objects where type = 'p' 
                                                      open cur
                                                      fetch next from cur into @procName
                                                      while @@fetch_status = 0
                                                      begin
                                                          exec('drop procedure [' + @procName + ']')
                                                          fetch next from cur into @procName
                                                      end
                                                      close cur
                                                      deallocate cur
                                                 ");
                                            DB.Database.ExecuteSqlCommand(sql);
                                            sw.WriteLine("End Delete All");

                                            sw.WriteLine("Start Delete Temp Table");
                                            sql = string.Format(@"
                                                                if  exists (select * from sysobjects where id = object_id(N'Web_T_ADocB') and OBJECTPROPERTY(id, N'IsUserTable') = 1) begin Drop Table Web_T_ADocB end
                                                                if  exists (select * from sysobjects where id = object_id(N'Web_T_ADocH') and OBJECTPROPERTY(id, N'IsUserTable') = 1) begin Drop Table Web_T_ADocH end
                                                                if  exists (select * from sysobjects where id = object_id(N'Web_T_FDocB') and OBJECTPROPERTY(id, N'IsUserTable') = 1) begin Drop Table Web_T_FDocB end
                                                                if  exists (select * from sysobjects where id = object_id(N'Web_T_FDocF') and OBJECTPROPERTY(id, N'IsUserTable') = 1) begin Drop Table Web_T_FDocF end
                                                                if  exists (select * from sysobjects where id = object_id(N'Web_T_FDocH') and OBJECTPROPERTY(id, N'IsUserTable') = 1) begin Drop Table Web_T_FDocH end
                                                                if  exists (select * from sysobjects where id = object_id(N'Web_T_IDocB') and OBJECTPROPERTY(id, N'IsUserTable') = 1) begin Drop Table Web_T_IDocB end
                                                                if  exists (select * from sysobjects where id = object_id(N'Web_T_IDocH') and OBJECTPROPERTY(id, N'IsUserTable') = 1) begin Drop Table Web_T_IDocH end ");
                                            DB.Database.ExecuteSqlCommand(sql);
                                            sw.WriteLine("End Delete Temp Table");
                                        }


                                        string lineOfText;
                                        FileStream filestream = new System.IO.FileStream(addressFile,
                                                                  System.IO.FileMode.Open,
                                                                  System.IO.FileAccess.Read,
                                                                  System.IO.FileShare.ReadWrite);
                                        var file = new System.IO.StreamReader(filestream, System.Text.Encoding.Default, true, 128);

                                        sql = "";
                                        while ((lineOfText = file.ReadLine()) != null)
                                        {
                                            if (!lineOfText.StartsWith("------"))
                                            {
                                                sql += lineOfText + " ";
                                            }
                                            else
                                            {
                                                try
                                                {
                                                    //sql = sql.Replace("ی", "ي");
                                                    sql = sql.Replace("yyyy", salTemp);

                                                    sql = sql.Replace("yyyx", (int.Parse(salTemp) - 1).ToString());
                                                    sql = sql.Replace("yyyz", (int.Parse(salTemp) + 1).ToString());

                                                    DB.Database.ExecuteSqlCommand(sql);
                                                    //sw.WriteLine("ExecuteSqlCommand OK : " + sql);
                                                    sql = "";
                                                }
                                                catch (Exception e)
                                                {
                                                    sw.WriteLine("ExecuteSqlCommand Error : " + sql);
                                                    filestream.Close();
                                                    throw;

                                                }
                                            }
                                        }

                                        if (isCols == true)
                                        {
                                            sql = string.Format(@"INSERT INTO dbo.Web_Version (ver,datever) VALUES ({0},SYSDATETIME())", UnitPublic.VerDB);
                                            DB.Database.ExecuteSqlCommand(sql);
                                            sw.WriteLine("INSERT New Version : " + UnitPublic.VerDB.ToString());
                                            MyIniConfig.Write("Change", "0");
                                            MyIniConfig.Write("EndDate", DateTime.Now.ToString());
                                        }
                                        filestream.Close();
                                        if (dbName != "Ace_WebConfig")
                                        {
                                            //File.Delete(item);
                                            //sw.WriteLine("Delete File");
                                        }

                                    }
                                    else
                                    {
                                        if (dbName != "Ace_WebConfig")
                                        {
                                            //File.Delete(item);
                                            //sw.WriteLine("Delete File");
                                        }
                                    }

                                }
                                catch (Exception e)
                                {
                                    sw.WriteLine(e.Message);
                                    sw.Close();

                                    // return "خطا در اتصال به دیتابیس های کاربرد کامپیوتر";
                                    throw;
                                }
                            }
                        }
                    }
                }
            }

            sw.WriteLine("End");
            sw.Close();
            if (File.Exists(fileLog))
            {
                File.Delete(fileLog);
            }
            return "OK";

        }










        public static string ChangeDatabaseConfig(string userCode, string flag, string srv_User, string srv_Pass, string device)
        {
            string IniLogPath = HttpContext.Current.Server.MapPath("~/Content/ini/SysLog.Ini");

            IniFile MyIniLog = new IniFile(IniLogPath);

            MyIniLog.Write("srart :", "OK");
            try
            {
                var DBase = UnitDatabase.dataDB.Where(p => p.UserName.ToUpper() == srv_User.ToUpper() && p.Password == srv_Pass).Single();

                string dbName;

                lockNumber = DBase.lockNumber;

                MyIniLog.Write("lockNumber :", lockNumber.ToString());
                MyIniLog.Write("addressFileSql :", addressFileSql);


                string[] filePaths = Directory.GetFiles(addressFileSql + "\\", "*.txt",
                                                 SearchOption.TopDirectoryOnly);


                foreach (var item in filePaths)
                {
                    MyIniLog.Write("filePaths :", item);
                }


                string sal = "";
                string group = "";
                bool isCols = false;


                if (!System.IO.Directory.Exists(addressFileSql + "\\" + lockNumber))
                    System.IO.Directory.CreateDirectory(addressFileSql + "\\" + lockNumber);

                string fileLog = addressFileSql + "\\" + lockNumber + "\\Config.txt";

                string IniConfigPath = addressFileSql + "\\" + lockNumber + "\\Config.ini";
                IniFile MyIniConfig = new IniFile(IniConfigPath);


                MyIniLog.Write("fileLog :", fileLog);

                if (File.Exists(fileLog))
                {
                    MyIniLog.Write("deletefileLog :", "start");

                    if (File.Exists(fileLog))
                    {
                        try
                        {
                            File.Delete(fileLog);
                        }
                        catch (Exception e)
                        {
                            return "UseLog";
                        }

                    }

                    MyIniLog.Write("deletefileLog :", "End");
                }

                StreamWriter sw = File.CreateText(fileLog);
                try
                {
                    foreach (var item in filePaths)
                    {
                        isCols = false;
                        string fileName = Path.GetFileName(item);
                        MyIniLog.Write("fileName :", fileName);
                        if (fileName == "WebViews_10000_Ace2.txt")
                        {
                            sw.WriteLine("fileName : " + fileName);
                            var files = fileName.Split('_');
                            string addressFile = item;
                            dbName = "Ace_WebConfig";

                            sw.WriteLine("dbName : " + dbName);


                            //string connectionString = CreateConnectionString(srv_User, srv_Pass, device, "", "Master", sal, group, 0, "", 0, 0);
                            string connectionString = String.Format(
                                            @"data source = {0};initial catalog = {1};persist security info = True;user id = {2}; password = {3};  multipleactiveresultsets = True; application name = EntityFramework",
                                            DBase.SqlServerName, "Master", DBase.SqlUserName, DBase.SqlPassword);

                            sw.WriteLine("connectionString : " + connectionString);

                            var connection = new SqlConnection(connectionString);

                            connection.Open();
                            var command = connection.CreateCommand();
                            command.CommandText = string.Format(@"IF Not EXISTS (SELECT name FROM master.dbo.sysdatabases WHERE name = N'{0}')
                                                            CREATE DATABASE [{0}] COLLATE SQL_Latin1_General_CP1256_CI_AS", dbName);
                            command.ExecuteNonQuery();

                            string res = UnitDatabase.TestAcount(DBase, userCode, "Config", "00", UnitPublic.access_View);
                            //string conStr = CreateConnectionString(srv_User, srv_Pass, device, "", "Config", sal, group, 0, "", 0, 0);
                            if (res == "")
                            {
                                string conStr = String.Format(@"data source = {0};initial catalog = {1};persist security info = True;user id = {2}; password = {3};  multipleactiveresultsets = True; application name = EntityFramework",
                                                      DBase.SqlServerName, dbName, DBase.SqlUserName, DBase.SqlPassword);
                                var DB = new ApiModel(conStr);
                                string sql;
                                int oldVer = 0;
                                try
                                {
                                    try
                                    {
                                        sql = string.Format(@"if (select count(id) from {0}.dbo.web_version) = 0
                                                                select 0
                                                              else
                                                                select ver from {0}.dbo.web_version where id = (select max(id) from {0}.dbo.web_version)",
                                                                    dbName);
                                        oldVer = DB.Database.SqlQuery<int>(sql).Single();
                                    }
                                    catch (Exception e)
                                    {
                                        sql = string.Format(@"CREATE TABLE {0}.[dbo].[Web_Version] (
                                                                     [id][int] IDENTITY(1,1) NOT NULL,
                                                                     [ver] [int] NULL,
                                                                     [datever] [datetime] NULL,
                                                             CONSTRAINT[PK_web_ver] PRIMARY KEY CLUSTERED
                                                             ([id] ASC)WITH(PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON[PRIMARY]) ON[PRIMARY]",
                                                                 dbName);
                                        DB.Database.ExecuteSqlCommand(sql);
                                    }

                                    sw.WriteLine("oldVer : " + oldVer.ToString());
                                    sw.WriteLine("VerDB : " + UnitPublic.VerDB);

                                    if (oldVer < UnitPublic.VerDB || flag == "1234")
                                    {
                                        

                                        MyIniConfig.Write("Change", "1");
                                        MyIniConfig.Write("BeginDate", DateTime.Now.ToString());
                                        MyIniConfig.Write("User", userCode);
                                        MyIniConfig.Write("Prog", "Config");

                                        if (isCols == false)
                                        {
                                            sw.WriteLine("Start Delete All");
                                            sql = string.Format(@"
                                                    DECLARE @sql VARCHAR(MAX) = '' 
                                                    DECLARE @crlf VARCHAR(2) = CHAR(13) + CHAR(10)
                                                    SELECT @sql = @sql + 'DROP VIEW ' + QUOTENAME(SCHEMA_NAME(schema_id)) + '.' + QUOTENAME(v.name) +';' + @crlf
                                                    FROM   sys.views v
                                                    EXEC(@sql);
                                                      declare @procName varchar(500)
                                                      declare cur cursor
                                                      for select name from sys.objects where type = 'if' or type = 'tf' or type = 'fn'
                                                      open cur
                                                      fetch next from cur into @procName
                                                      while @@fetch_status = 0
                                                      begin
                                                          exec('drop function [' + @procName + ']')
                                                          fetch next from cur into @procName
                                                      end
                                                      close cur
                                                      deallocate cur
                                                      declare cur cursor
                                                      for select name from sys.objects where type = 'p' 
                                                      open cur
                                                      fetch next from cur into @procName
                                                      while @@fetch_status = 0
                                                      begin
                                                          exec('drop procedure [' + @procName + ']')
                                                          fetch next from cur into @procName
                                                      end
                                                      close cur
                                                      deallocate cur
                                                 ", dbName);
                                            DB.Database.ExecuteSqlCommand(sql);
                                            sw.WriteLine("End Delete All");

                                        }


                                        string lineOfText;
                                        FileStream filestream = new System.IO.FileStream(addressFile,
                                                                  System.IO.FileMode.Open,
                                                                  System.IO.FileAccess.Read,
                                                                  System.IO.FileShare.ReadWrite);
                                        var file = new System.IO.StreamReader(filestream, System.Text.Encoding.Default, true, 128);

                                        sql = "";
                                        int counter = 0;
                                        int counter1 = 0;
                                        bool test = false;
                                        string[] func = new string[100];
                                        while ((lineOfText = file.ReadLine()) != null)
                                        {
                                            if (!lineOfText.StartsWith("------"))
                                            {
                                                if (lineOfText == "Create Function Web_UserTrs")
                                                {
                                                    test = true;
                                                }
                                                if (test == true)
                                                {
                                                    counter += 1;
                                                    if (counter == 100)
                                                    {
                                                        counter1 += 1;
                                                        counter = 0;
                                                    }
                                                    func[counter1] += lineOfText + " ";
                                                }
                                                sql += lineOfText + " ";
                                            }
                                            else
                                            {
                                                try
                                                {
                                                    if (counter1 > 0)
                                                    {
                                                        sql = func[0] + func[1] + func[2] + func[3] + func[4] + func[5] + func[6] + func[7] + func[8] + func[9] + func[10] + func[11] + func[12] + func[13];
                                                    }

                                                    DB.Database.ExecuteSqlCommand(sql);
                                                    sw.WriteLine("ExecuteSqlCommand OK : " + sql);
                                                    sql = "";
                                                }
                                                catch (Exception e)
                                                {
                                                    sw.WriteLine("ExecuteSqlCommand Error : " + sql);
                                                    filestream.Close();
                                                    throw;

                                                }
                                            }
                                        }

                                        if (isCols == false)
                                        {
                                            sql = string.Format(@"INSERT INTO {0}.dbo.Web_Version (ver,datever) VALUES ({1},SYSDATETIME())", dbName, UnitPublic.VerDB);
                                            DB.Database.ExecuteSqlCommand(sql);
                                            sw.WriteLine("INSERT New Version : " + UnitPublic.VerDB.ToString());
                                            MyIniConfig.Write("Change", "0");
                                            MyIniConfig.Write("EndDate", DateTime.Now.ToString());
                                        }
                                        filestream.Close();
                                        if (dbName != "Ace_WebConfig")
                                        {
                                        }

                                        // return "به روز رسانی انجام شد";
                                    }
                                    else
                                    {
                                        if (dbName != "Ace_WebConfig")
                                        {
                                            //File.Delete(item);
                                            //sw.WriteLine("Delete File");
                                        }
                                    }

                                }
                                catch (Exception e)
                                {
                                    sw.WriteLine(e.Message);
                                    sw.Close();

                                    // return "خطا در اتصال به دیتابیس های کاربرد کامپیوتر";
                                    throw;
                                }
                                finally
                                {
                                    MyIniConfig.Write("Change", "0");
                                    MyIniConfig.Write("EndDate", DateTime.Now.ToString());
                                }
                            }
                        }

                        sw.WriteLine("End");
                        sw.Close();
                        if (File.Exists(fileLog))
                        {
                            File.Delete(fileLog);
                        }
                    }
                    sw.Close();
                }
                catch (Exception e)
                {
                    MyIniLog.Write("mess :", e.Message);
                    sw.Close();
                }
            }
            catch (Exception e)
            {
                MyIniLog.Write("mess :", e.Message);

            }
            return "OK";

        }




        /*
        public static string ChangeDatabaseFourZero(string ace, string group, string userCode, bool auto, string lockNumber, string srv_User, string srv_Pass, string device)
        {
            string dbName;
            string sal = "0000";
            string[] filePaths = Directory.GetFiles(addressFileSql + "\\", "WebViews_" + lockNumber + "_" + ace + "_" + group + "_0000.txt",
                                             SearchOption.TopDirectoryOnly);

            bool isCols = false;

            if (!System.IO.Directory.Exists(addressFileSql + "\\" + lockNumber))
                System.IO.Directory.CreateDirectory(addressFileSql + "\\" + lockNumber);


            string fileLog = addressFileSql + "\\" + lockNumber + "\\" + ace + "_" + group + "_" + sal + ".txt";



            if (File.Exists(fileLog))
            {
                try
                {
                    File.Delete(fileLog);
                }
                catch (Exception e)
                {
                    return "UseLog";
                }

            }

            StreamWriter sw = File.CreateText(fileLog);

            foreach (var item in filePaths)
            {

                isCols = false;
                string fileName = Path.GetFileName(item);
                var files = fileName.Split('_');
                if (files.Length >= 5)
                {
                    if (files[3].Length == 1)
                        files[3] = "0" + files[3];

                    if (files[1] == lockNumber && files[3] == group)
                    {
                        sw.WriteLine("fileName : " + fileName);
                        string addressFile = item;
                        //sal = (files[4].Split('.'))[0];
                        //group = files[3];
                        string salTemp = sal;
                        if (files[2] == "Web2")
                            salTemp = "0000";

                        dbName = ("ACE_" + files[2] + group + salTemp);

                        if ((files[2] == "Web8" || files[2] == UnitPublic.Web1) && salTemp == "0000" && files.Length != 5)
                        {
                            sw.WriteLine("Break For Error Name Database : " + dbName);
                        }
                        else
                        {

                            if (files.Length == 6)
                            {
                                string nameTemp = (files[5].Split('.'))[0];
                                if (nameTemp == "Col")
                                {
                                    isCols = true;
                                }
                            }

                            sw.WriteLine("dbName : " + dbName);

                            string connectionString = CreateConnectionString(srv_User, srv_Pass, device, "", "Master", "", "", 0, "", 0, 0);
                            //string connectionString = String.Format(
                            //                @"data source = {0};initial catalog = {1};persist security info = True;user id = {2}; password = {3};  multipleactiveresultsets = True; application name = EntityFramework",
                            //                list.SqlServerName, "master", list.SqlUserName, list.SqlPassword);

                            sw.WriteLine("connectionString : " + connectionString);

                            var connection = new SqlConnection(connectionString);

                            connection.Open();
                            var command = connection.CreateCommand();
                            command.CommandText = string.Format(@"IF Not EXISTS (SELECT name FROM master.dbo.sysdatabases WHERE name = N'{0}')
                                                            CREATE DATABASE [{0}] COLLATE SQL_Latin1_General_CP1256_CI_AS", dbName);
                            command.ExecuteNonQuery();
                            connection.Close();

                            string conStr = CreateConnectionString(srv_User, srv_Pass, device, "", files[2] == "Ace2.txt" ? "Config" : files[2], salTemp, group, 0, "", 0, 0);
                            if (conStr.Length > 100)
                            {
                                dbChange = new ApiModel(conStr);
                            }


                            string sql;
                            int oldVer = 0;
                            try
                            {
                                try
                                {
                                    sql = string.Format(@"if (select count(id) from web_version) = 0
                                                                select 0
                                                              else
                                                                select ver from web_version where id = (select max(id) from web_version)");
                                    oldVer = dbChange.Database.SqlQuery<int>(sql).Single();
                                }
                                catch (Exception e)
                                {
                                    sql = string.Format(@"CREATE TABLE[dbo].[Web_Version] (
                                                                     [id][int] IDENTITY(1,1) NOT NULL,
                                                                     [ver] [int] NULL,
                                                                     [datever] [datetime] NULL,
                                                             CONSTRAINT[PK_web_ver] PRIMARY KEY CLUSTERED
                                                             ([id] ASC)WITH(PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON[PRIMARY]) ON[PRIMARY]");
                                    dbChange.Database.ExecuteSqlCommand(sql);
                                }

                                sw.WriteLine("oldVer : " + oldVer.ToString());
                                sw.WriteLine("VerDB : " + UnitPublic.VerDB);

                                if (oldVer < UnitPublic.VerDB || auto == false)
                                {
                                    string IniConfigPath = addressFileSql + "\\" + lockNumber + "\\Config_" + ace + group + sal + ".ini";

                                    IniFile MyIniConfig = new IniFile(IniConfigPath);

                                    MyIniConfig.Write("Change", "1");
                                    MyIniConfig.Write("BeginDate", DateTime.Now.ToString());
                                    MyIniConfig.Write("User", userCode);
                                    MyIniConfig.Write("Prog", ace);
                                    MyIniConfig.Write("Group", group);
                                    MyIniConfig.Write("Sal", sal);


                                    if (isCols == false)
                                    {

                                        sw.WriteLine("Start Delete All");
                                        sql = string.Format(@"
                                                         --IF EXISTS(SELECT * FROM sys.tables WHERE SCHEMA_NAME(schema_id) LIKE 'dbo' AND name like 'Web_Flds') drop table Web_Flds
                                                         --IF EXISTS(SELECT * FROM sys.tables WHERE SCHEMA_NAME(schema_id) LIKE 'dbo' AND name like 'Web_T_ADOCB') drop table Web_T_ADOCB
                                                         --IF EXISTS(SELECT * FROM sys.tables WHERE SCHEMA_NAME(schema_id) LIKE 'dbo' AND name like 'Web_T_ADOCH') drop table Web_T_ADOCH
                                                    DECLARE @sql VARCHAR(MAX) = '' 
                                                    DECLARE @crlf VARCHAR(2) = CHAR(13) + CHAR(10)
                                                    SELECT @sql = @sql + 'DROP VIEW ' + QUOTENAME(SCHEMA_NAME(schema_id)) + '.' + QUOTENAME(v.name) +';' + @crlf
                                                    FROM   sys.views v
                                                    EXEC(@sql);
                                                      declare @procName varchar(500)
                                                      declare cur cursor
                                                      for select name from sys.objects where type = 'if' or type = 'tf' or type = 'fn'
                                                      open cur
                                                      fetch next from cur into @procName
                                                      while @@fetch_status = 0
                                                      begin
                                                          exec('drop function [' + @procName + ']')
                                                          fetch next from cur into @procName
                                                      end
                                                      close cur
                                                      deallocate cur
                                                      declare cur cursor
                                                      for select name from sys.objects where type = 'p' 
                                                      open cur
                                                      fetch next from cur into @procName
                                                      while @@fetch_status = 0
                                                      begin
                                                          exec('drop procedure [' + @procName + ']')
                                                          fetch next from cur into @procName
                                                      end
                                                      close cur
                                                      deallocate cur
                                                 ");
                                        dbChange.Database.ExecuteSqlCommand(sql);
                                        sw.WriteLine("End Delete All");
                                    }


                                    string lineOfText;
                                    FileStream filestream = new System.IO.FileStream(addressFile,
                                                              System.IO.FileMode.Open,
                                                              System.IO.FileAccess.Read,
                                                              System.IO.FileShare.ReadWrite);
                                    var file = new System.IO.StreamReader(filestream, System.Text.Encoding.Default, true, 128);

                                    sql = "";
                                    while ((lineOfText = file.ReadLine()) != null)
                                    {
                                        if (!lineOfText.StartsWith("------"))
                                        {
                                            sql += lineOfText + " ";
                                        }
                                        else
                                        {
                                            try
                                            {
                                                //sql = sql.Replace("ی", "ي");
                                                sql = sql.Replace("yyyy", salTemp);

                                                sql = sql.Replace("yyyx", (int.Parse(salTemp) - 1).ToString());
                                                sql = sql.Replace("yyyz", (int.Parse(salTemp) + 1).ToString());

                                                dbChange.Database.ExecuteSqlCommand(sql);
                                                sw.WriteLine("ExecuteSqlCommand OK : " + sql);
                                                sql = "";
                                            }
                                            catch (Exception e)
                                            {
                                                sw.WriteLine("ExecuteSqlCommand Error : " + sql);
                                                filestream.Close();
                                                throw;

                                            }
                                        }
                                    }


                                    sql = string.Format(@"INSERT INTO Web_Version (ver,datever) VALUES ({0},SYSDATETIME())", UnitPublic.VerDB);
                                    dbChange.Database.ExecuteSqlCommand(sql);
                                    sw.WriteLine("INSERT New Version : " + UnitPublic.VerDB.ToString());
                                    MyIniConfig.Write("Change", "0");
                                    MyIniConfig.Write("EndDate", DateTime.Now.ToString());

                                    filestream.Close();
                                }
                                else
                                {

                                }
                            }
                            catch (Exception e)
                            {
                                sw.WriteLine(e.Message);
                                sw.Close();

                                // return "خطا در اتصال به دیتابیس های کاربرد کامپیوتر";
                                throw;
                            }
                        }
                    }
                }
            }

            sw.WriteLine("End");
            sw.Close();
            if (File.Exists(fileLog))
            {
                File.Delete(fileLog);
            }
            return "OK";

        }

        */


        /* public static void TestDatabase(string ace)
         {
             string sql;
             int oldVer = 0;
             try
             {
                 db.Database.Connection.Open();
                 try
                 {
                     sql = string.Format(@"if (select count(id) from web_version) = 0
                                                         select 0
                                                       else
                                                         select ver from web_version where id = (select max(id) from web_version)");
                     oldVer = db.Database.SqlQuery<int>(sql).Single();
                 }
                 catch (Exception e)
                 {
                     sql = string.Format(@"CREATE TABLE[dbo].[Web_Version] (
                                                              [id][int] IDENTITY(1,1) NOT NULL,
                                                              [ver] [int] NULL,
                                                              [datever] [datetime] NULL,
                                                      CONSTRAINT[PK_web_ver] PRIMARY KEY CLUSTERED
                                                      ([id] ASC)WITH(PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON[PRIMARY]) ON[PRIMARY]");
                     db.Database.ExecuteSqlCommand(sql);
                 }

                 if (oldVer < UnitPublic.VerDB)
                 {
                     sql = string.Format(@"
                                             DECLARE @sql VARCHAR(MAX) = '' 
                                             DECLARE @crlf VARCHAR(2) = CHAR(13) + CHAR(10)
                                             SELECT @sql = @sql + 'DROP VIEW ' + QUOTENAME(SCHEMA_NAME(schema_id)) + '.' + QUOTENAME(v.name) +';' + @crlf
                                             FROM   sys.views v
                                             EXEC(@sql);
                                               declare @procName varchar(500)
                                               declare cur cursor
                                               for select name from sys.objects where type = 'if' or type = 'tf' or type = 'fn'
                                               open cur
                                               fetch next from cur into @procName
                                               while @@fetch_status = 0
                                               begin
                                                   exec('drop function [' + @procName + ']')
                                                   fetch next from cur into @procName
                                               end
                                               close cur
                                               deallocate cur
                                               declare cur cursor
                                               for select name from sys.objects where type = 'p' 
                                               open cur
                                               fetch next from cur into @procName
                                               while @@fetch_status = 0
                                               begin
                                                   exec('drop procedure [' + @procName + ']')
                                                   fetch next from cur into @procName
                                               end
                                               close cur
                                               deallocate cur
                                          ");
                     db.Database.ExecuteSqlCommand(sql);


                     string textFilePath = "";

                     if (ace == UnitPublic.Web1)
                         textFilePath = @"c:\a\Ace8Views_Web1.txt";
                     else if (ace == "Web2")
                         textFilePath = @"c:\a\Ace8Views_Web2.txt";
                     else if (ace == "Web8")
                         textFilePath = @"c:\a\Ace8Views.txt";

                     string lineOfText;
                     var filestream = new System.IO.FileStream(textFilePath,
                                               System.IO.FileMode.Open,
                                               System.IO.FileAccess.Read,
                                               System.IO.FileShare.ReadWrite);
                     var file = new System.IO.StreamReader(filestream, System.Text.Encoding.UTF8, true, 128);
                     sql = "";
                     while ((lineOfText = file.ReadLine()) != null)
                     {
                         if (lineOfText != "-------------------")
                             sql += lineOfText + " ";
                         else
                         {
                             try
                             {
                                 db.Database.ExecuteSqlCommand(sql);
                                 sql = "";
                             }
                             catch (Exception e)
                             {
                                 throw;
                             }
                         }
                     }

                     sql = string.Format(@"INSERT INTO Web_Version (ver,datever) VALUES ({0},SYSDATETIME())", UnitPublic.VerDB);
                     db.Database.ExecuteSqlCommand(sql);

                     // return "به روز رسانی انجام شد";
                 }
             }
             catch (Exception e)
             {
                 // return "خطا در اتصال به دیتابیس های کاربرد کامپیوتر";
                 throw;
             }
             /* //string connectionString = String.Format(@"data source = .\sql2014;initial catalog = ACE_Web8011384 ;persist security info = True;user id = sa; password = 106;  multipleactiveresultsets = True; application name = EntityFramework");
              string connectionString = String.Format(@"data source = .\sql2014;initial catalog = ACE_Web8011384 ;
                                                        persist security info = True;user id = sa; password = 106; 
                                                        multipleactiveresultsets = True; application name = EntityFramework");

              try
              {
                  int oldVer = 0;
                  db = new ApiModel(connectionString);
                  db.Database.Connection.Open();
                  string sql;
                  try
                  {
                      sql = string.Format(@"select ver from web_version where id = (select max(id) from web_version)");
                      oldVer = db.Database.SqlQuery<int>(sql).Single();
                  }
                  catch (Exception e)
                  {
                      sql = string.Format(@"
                       CREATE TABLE[dbo].[Web_Version] (
                         [id][int] IDENTITY(1,1) NOT NULL,
                         [ver] [int] NULL,
                         [datever] [datetime] NULL,
                       CONSTRAINT[PK_web_ver] PRIMARY KEY CLUSTERED
                      (
                         [id] ASC
                      )WITH(PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON[PRIMARY]
                      ) ON[PRIMARY]");
                      db.Database.ExecuteSqlCommand(sql);
                  }

                  if (oldVer < UnitPublic.VerNumber)
                  {
                      sql = string.Format(@"  DECLARE @sql VARCHAR(MAX) = '' 
                                              DECLARE @crlf VARCHAR(2) = CHAR(13) + CHAR(10)
                                              SELECT @sql = @sql + 'DROP VIEW ' + QUOTENAME(SCHEMA_NAME(schema_id)) + '.' + QUOTENAME(v.name) +';' + @crlf
                                              FROM   sys.views v
                                              EXEC(@sql);
                                                declare @procName varchar(500)
                                                declare cur cursor
                                                for select name from sys.objects where type = 'if' or type = 'tf' or type = 'fn'
                                                open cur
                                                fetch next from cur into @procName
                                                while @@fetch_status = 0
                                                begin
                                                    exec('drop function [' + @procName + ']')
                                                    fetch next from cur into @procName
                                                end
                                                close cur
                                                deallocate cur
                                                declare cur cursor
                                                for select name from sys.objects where type = 'p' 
                                                open cur
                                                fetch next from cur into @procName
                                                while @@fetch_status = 0
                                                begin
                                                    exec('drop procedure [' + @procName + ']')
                                                    fetch next from cur into @procName
                                                end
                                                close cur
                                                deallocate cur
                                           ");
                      db.Database.ExecuteSqlCommand(sql);
                  }

                  string textFilePath = @"c:\a\Ace8Views_Web8.txt";
                  string lineOfText;
                  var filestream = new System.IO.FileStream(textFilePath,
                                            System.IO.FileMode.Open,
                                            System.IO.FileAccess.Read,
                                            System.IO.FileShare.ReadWrite);
                  var file = new System.IO.StreamReader(filestream, System.Text.Encoding.UTF8, true, 128);
                  sql = "";
                  while ((lineOfText = file.ReadLine()) != null)
                  {
                      if (lineOfText != "-------------------")
                          sql += lineOfText + " ";
                      else
                      {
                          try
                          {
                              db.Database.ExecuteSqlCommand(sql);
                          }
                          catch (Exception e)
                          {
                              throw;
                          }
                      }
                  }

                  sql = string.Format(@"INSERT INTO Web_Version (ver,datever) VALUES ({0},SYSDATETIME())", UnitPublic.VerNumber);
                  db.Database.ExecuteSqlCommand(sql);

                  return "به روز رسانی انجام شد";
              }
              catch (Exception e)
              {
                  return "خطا در اتصال به دیتابیس های کاربرد کامپیوتر";
                  throw;
              }

              */


        // }



        public static List<Access> dataDB = null;


        public static async void SetDataDB(string userName, string password)
        {
            List<Access> data = new List<Access>();
            IniFile MyIniLog = new IniFile(IniLogPath);
            if (UnitPublic.apiByFile == true)  // not Api Login for mofid       //UnitPublic.apiByFile == true or  addressApiAccounting == ""
            {
                string path = HttpContext.Current.Server.MapPath("~/Content/ini/DataAccount.dat");
                //MyIniLog.Write("Path Data File", path, "DataFile");
                string valueStr = "";
                if (File.Exists(path))
                {
                    try
                    {
                        StreamReader streamReader = new StreamReader(path, System.Text.Encoding.UTF8);
                        var readContents = await streamReader.ReadToEndAsync();
                        streamReader.Close();
                        readContents = readContents.Substring(0, readContents.Length - 2);
                        valueStr = UnitPublic.EncodeDecodeXor(readContents);
                        if (valueStr.Length > 0)
                        {
                            var item = valueStr.Split('~');
                            string serialCpu_File = item[27];
                            string serialHdd_File = item[28];

                          
                            var serialCpu = UnitPublic.GetCPU();
                            var serialHdd = UnitPublic.GetHdd();

                            if (serialCpu_File == serialCpu && serialHdd_File == serialHdd)
                            {
                                MyIniLog.Write("Read Data File id", "true", "DataFile");

                                string conStr = String.Format(@"data source = {0};initial catalog = {1};persist security info = True;user id = {2}; password = {3};  multipleactiveresultsets = True; application name = EntityFramework",
                                      item[7], "Ace_Config", item[8], item[9]);

                                var a = new Access
                                {
                                    Id = 0,
                                    lockNumber = item[0],
                                    CompanyName = item[1],
                                    UserName = item[2],
                                    Password = item[3],
                                    AddressApi = item[10],
                                    SqlServerName = item[7],
                                    SqlUserName = item[8],
                                    SqlPassword = item[9],
                                    fromDate = item[4],
                                    toDate = item[5],
                                    userCount = Convert.ToByte(item[6]),
                                    AFI1_Group = item[16],
                                    AFI1_Access = item[15],
                                    AFI8_Group = item[20],
                                    AFI8_Access = item[19],
                                    ERJ_Group = item[18],
                                    ERJ_Access = item[17],
                                    active = item[11] == "true",
                                    ProgName = item[21],
                                    Fct_or_Inv = item[22],
                                    multilang = false,
                                    logoutmin = 0,
                                    AddressApiPos = "",
                                    IsApp = item[12] == "true",
                                    IsWeb = item[13] == "true",
                                    IsApi = item[12] == "true",
                                    WhereKala = item[23],
                                    WhereCust = item[24],
                                    WhereAcc = item[25],
                                    WhereThvl = item[26],
                                    SettingApp = 0,
                                    Spec = "",
                                    DB = new ApiModel(conStr),
                                };

                                dataDB = new List<Access>();
                                dataDB.Add(a);
                            }
                            else
                            {
                                MyIniLog.Write("Read Data File id", "false", "DataFile");
                            }
                        }

                    }
                    catch (Exception e)
                    {
                        MyIniLog.Write("Read Data File", valueStr, "DataFile");
                        MyIniLog.Write("Exception Read Data File", e.Message.ToString(), "DataFile");
                        throw;
                    }
                }
                else
                {
                    MyIniLog.Write("Read Data File", "NotFound", "DataFile");
                }
            }
            else
            {
                string address = String.Format(addressApiAccounting + "api/Account/InformationSql/{0}/{1}", userName, password);
                var client = new HttpClient();
                var task = client.GetAsync(address)
                  .ContinueWith((taskwithresponse) =>
                  {
                      var response = taskwithresponse.Result;
                      var jsonString = response.Content.ReadAsStringAsync();
                      jsonString.Wait();
                      data = JsonConvert.DeserializeObject<List<Access>>(jsonString.Result);
                  });
                task.Wait();

                if (data.Count > 0)
                {
                    if (userName != "null")
                    {
                        string conStr = String.Format(@"data source = {0};initial catalog = {1};persist security info = True;user id = {2}; password = {3};  multipleactiveresultsets = True; application name = EntityFramework",
                                                      data[0].SqlServerName, "Ace_Config", data[0].SqlUserName, data[0].SqlPassword);

                        var DBase = dataDB.Where(p => p.UserName.ToUpper() == userName.ToUpper() && p.Password == password).ToList();
                        if (DBase.Count == 0) // add new user
                        {
                            dataDB.Add(new Access
                            {
                                Id = data[0].Id,
                                lockNumber = data[0].lockNumber,
                                CompanyName = data[0].CompanyName,
                                UserName = data[0].UserName,
                                Password = data[0].Password,
                                AddressApi = data[0].AddressApi,
                                SqlServerName = data[0].SqlServerName,
                                SqlUserName = data[0].SqlUserName,
                                SqlPassword = data[0].SqlPassword,
                                fromDate = data[0].fromDate,
                                toDate = data[0].toDate,
                                userCount = data[0].userCount,
                                AFI1_Group = data[0].AFI1_Group,
                                AFI1_Access = data[0].AFI1_Access,
                                AFI8_Group = data[0].AFI8_Group,
                                AFI8_Access = data[0].AFI8_Access,
                                ERJ_Group = data[0].ERJ_Group,
                                ERJ_Access = data[0].ERJ_Access,
                                active = data[0].active,
                                ProgName = data[0].ProgName,
                                Fct_or_Inv = data[0].Fct_or_Inv,
                                multilang = data[0].multilang,
                                logoutmin = data[0].logoutmin,
                                AddressApiPos = data[0].AddressApiPos,
                                IsApp = data[0].IsApp,
                                IsWeb = data[0].IsWeb,
                                IsApi = data[0].IsApi,
                                WhereKala = data[0].WhereKala,
                                WhereCust = data[0].WhereCust,
                                WhereAcc = data[0].WhereAcc,
                                WhereThvl = data[0].WhereThvl,
                                SettingApp = data[0].SettingApp,
                                Spec = data[0].Spec,
                                DB = new ApiModel(conStr),
                            });
                        }
                        else // update user
                        {
                            var item = DBase[0];
                            item.Id = data[0].Id;
                            item.lockNumber = data[0].lockNumber;
                            item.CompanyName = data[0].CompanyName;
                            item.UserName = data[0].UserName;
                            item.Password = data[0].Password;
                            item.AddressApi = data[0].AddressApi;
                            item.SqlServerName = data[0].SqlServerName;
                            item.SqlUserName = data[0].SqlUserName;
                            item.SqlPassword = data[0].SqlPassword;
                            item.fromDate = data[0].fromDate;
                            item.toDate = data[0].toDate;
                            item.userCount = data[0].userCount;
                            item.AFI1_Group = data[0].AFI1_Group;
                            item.AFI1_Access = data[0].AFI1_Access;
                            item.AFI8_Group = data[0].AFI8_Group;
                            item.AFI8_Access = data[0].AFI8_Access;
                            item.ERJ_Group = data[0].ERJ_Group;
                            item.ERJ_Access = data[0].ERJ_Access;
                            item.active = data[0].active;
                            item.ProgName = data[0].ProgName;
                            item.Fct_or_Inv = data[0].Fct_or_Inv;
                            item.multilang = data[0].multilang;
                            item.logoutmin = data[0].logoutmin;
                            item.AddressApiPos = data[0].AddressApiPos;
                            item.IsApp = data[0].IsApp;
                            item.IsWeb = data[0].IsWeb;
                            item.IsApi = data[0].IsApi;
                            item.WhereKala = data[0].WhereKala;
                            item.WhereCust = data[0].WhereCust;
                            item.WhereAcc = data[0].WhereAcc;
                            item.WhereThvl = data[0].WhereThvl;
                            item.SettingApp = data[0].SettingApp;
                            item.Spec = data[0].Spec;
                            item.DB = new ApiModel(conStr);
                        }
                    }
                    else
                    {
                        dataDB = data;
                        for (int i = 0; i < dataDB.Count; i++)
                        {
                            //dataDB[i].Spec = "";
                            string conStr = String.Format(
                                                    @"data source = {0};initial catalog = {1};persist security info = True;user id = {2}; password = {3};  multipleactiveresultsets = True; application name = EntityFramework",
                                                    dataDB[i].SqlServerName, "Ace_Config", dataDB[i].SqlUserName, dataDB[i].SqlPassword);
                            dataDB[i].DB = new ApiModel(conStr);
                        }
                    }
                }
                else // delete in table
                {
                    if (userName != "null")
                    {
                        var DBase = dataDB.Where(p => p.UserName.ToUpper() == userName.ToUpper() && p.Password == password).ToList();
                        if (DBase.Count > 0)
                        {
                            dataDB.Remove(DBase.Single());
                        }
                    }
                }
            }
        }

        public static string TestAcount(Access dBase, string device, string ace, string group, string modeCode)
        {
            string res = "";
            string year = persianCalendar.GetYear(DateTime.Now).ToString();
            string month = persianCalendar.GetMonth(DateTime.Now).ToString();
            string day = persianCalendar.GetDayOfMonth(DateTime.Now).ToString();

            //MyIniLog.Write("DateTime.Now", DateTime.Now.ToString());
            //MyIniLog.Write("GetYear", year);
            //MyIniLog.Write("GetMonth", month);
            //MyIniLog.Write("GetDayOfMonth", day);

            month = month.Length == 1 ? "0" + month : month;
            day = day.Length == 1 ? "0" + day : day;
            string PDate = year + "/" + month + "/" + day;
            //MyIniLog.Write("PDate", PDate);

            if (dBase.toDate != "" && dBase.fromDate != "")
            {
                if (UnitPublic.GetPersianDaysDiffDate(dBase.toDate, PDate) > 0)
                {
                    return "Expire Account";
                }
            }

            if (dBase.active == false)
            {
                return "Disable Account";
            }

            if (device == "" && dBase.IsApi == false)
            {
                return "Not Access Api";
            }

            if (device == "Web" && dBase.IsWeb == false)
            {
                return "Not Access Web";
            }

            if (device == "App" && dBase.IsApp == false)
            {
                return "Not Access App";
            }

            string[] listAccess = new string[0];
            string[] listGroup = new string[0];
            bool fullAccess = false;
            bool groupAccess = false;
            bool accept = false;

            if (ace.ToUpper() == "WEB1") ace = UnitPublic.Web1;
            if (ace.ToUpper() == "WEB2") ace = UnitPublic.Web2;
            if (ace.ToUpper() == "WEB8") ace = UnitPublic.Web8;



            if (ace == UnitPublic.Web1)
            {
                fullAccess = dBase.AFI1_Access == "*";
                listAccess = dBase.AFI1_Access.Split('*');
                listGroup = dBase.AFI1_Group.Split('-');
                groupAccess = listGroup.Contains(group);
            }
            else if (ace == UnitPublic.Web2)
            {
                fullAccess = dBase.ERJ_Access == "*";
                listAccess = dBase.ERJ_Access.Split('*');
                listGroup = dBase.ERJ_Group.Split('-');
                groupAccess = listGroup.Contains(group);
            }
            else if (ace == UnitPublic.Web8)
            {
                fullAccess = dBase.AFI8_Access == "*";
                listAccess = dBase.AFI8_Access.Split('*');
                listGroup = dBase.AFI8_Group.Split('-');
                groupAccess = listGroup.Contains(group);
            }
            else
            {
                groupAccess = true;
                fullAccess = true;
            }


            if (fullAccess == false)
            {
                string mode = UnitPublic.ModeCodeConnection(modeCode);
                if (mode == UnitPublic.access_View || mode == UnitPublic.access_Chante_FDoc_Moved)
                    accept = true;
                else
                    accept = listAccess.Contains(mode);
            }


            if (fullAccess == true) accept = true;
            if (groupAccess == false) accept = false;

            if (dBase.DB.Database.Connection.State == System.Data.ConnectionState.Closed)
                dBase.DB.Database.Connection.Open();


            if (accept)
            {
                return "";
            }
            else
            {
                return groupAccess == false ? "Not access to the group" : "Not access to the method";
            }

        }

    }
}
