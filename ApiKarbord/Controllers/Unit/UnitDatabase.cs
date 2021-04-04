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
        public static ApiModel db;
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

        static IniFile MyIni = new IniFile(IniPath);

        static string addressApiAccounting = MyIni.Read("serverName");
        static string addressFileSql = MyIni.Read("FileSql");

        static List<Access> model = null;

        //ایجاد کانکشن استرینگ 
        //اگر سایت ترو باشد یعنی به اس کیو ال ای پی ای
        public static string CreateConnectionString(string userName, string password, string userKarbord, string ace, string sal, string group, long serialNumber, string modecode, int act, int bandNo)
        {
            // addressApiAccounting = MyIni.Read("serverName");

            PersianCalendar pc = new System.Globalization.PersianCalendar();
            string PDate = string.Format("{0:yyyy/MM/dd}", Convert.ToDateTime(pc.GetYear(DateTime.Now).ToString() + "/" + pc.GetMonth(DateTime.Now).ToString() + "/" + pc.GetDayOfMonth(DateTime.Now).ToString()));

            try
            {
                string address = String.Format(addressApiAccounting + "api/Account/InformationSql/{0}/{1}/'{2}'/'{3}'/'{4}'/'{5}'/{6}/'{7}'/{8}/{9}", userName, password, userKarbord, ace, group, sal, serialNumber, modecode, act, bandNo);

                var client = new HttpClient();

                var task = client.GetAsync(address)
                  .ContinueWith((taskwithresponse) =>
                  {
                      var response = taskwithresponse.Result;
                      var jsonString = response.Content.ReadAsStringAsync();
                      jsonString.Wait();

                      model = JsonConvert.DeserializeObject<List<Access>>(jsonString.Result);
                  });
                task.Wait();

                var list = model.First();

                if (list.toDate != "" && list.fromDate != "")
                {
                    if (UnitPublic.GetPersianDaysDiffDate(list.toDate, PDate) > 0)
                    {
                        return "Expire Account";
                    }
                }
                if (list.active == false)
                {
                    return "Disable Account";
                }

                if (model.Count > 0)
                {
                    string connectionString = String.Format(
                                    //  @"data source = {0};initial catalog = {1};user id = {2}; password = {3}; MultipleActiveResultSets = True; App = EntityFramework",
                                    @"data source = {0};initial catalog = {1};persist security info = True;user id = {2}; password = {3};  multipleactiveresultsets = True; application name = EntityFramework",
                                    list.SqlServerName, DatabaseName(ace, sal, group), list.SqlUserName, list.SqlPassword);
                    return connectionString;
                }

                else
                    return "";
            }
            catch (Exception e)
            {
                return null;
                throw;
            }
        }

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


        //اگر سایت ترو باشد یعنی به اس کیو ال ای پی ای
        public static string CreateConection(string userName, string password, string userKarbord, string ace, string sal, string group, long serialnumber, string modecode, int act, int bandNo)
        {
            try
            {
                string conStr = CreateConnectionString(userName, password, userKarbord, ace, sal, group, serialnumber, modecode, act, bandNo);
                if (conStr.Length > 100)
                {
                    db = new ApiModel(conStr);
                    if (ace == "Config" && group == "00")
                    {
                        ChangeDatabase();
                        db = new ApiModel(conStr);
                    }
                    return "ok";
                }
                else
                    return conStr;
            }
            catch (Exception)
            {
                return "error";
                throw;
            }
        }

        //اگر سایت ترو باشد یعنی به اس کیو ال ای پی ای
        public static Boolean TestSqlServer(bool api)
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




        public static List<string> ReadUserPassHeader(HttpRequestHeaders header)
        {
            List<string> list = new List<string>();

            string userName = string.Empty;
            string password = string.Empty;
            string userKarbord = string.Empty;


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

            list.Add(userName);
            list.Add(password);
            list.Add(userKarbord);

            return (list);
        }



        // ذخیره لاگ
        public static void SaveLog(string userName, string password, string userKarbord, string ace, string sal, string group, long serialNumber, string modecode, int act, string ins, int bandNo)
        {
            try
            {
                if (ins == "Y")
                {
                    var client = new HttpClient();

                    string address = String.Format(addressApiAccounting + "api/Account/Log/{0}/{1}/'{2}'/'{3}'/'{4}'/'{5}'/{6}/'{7}'/{8}/1/{9}", userName, password, userKarbord, ace, group, sal, serialNumber, modecode, act, bandNo);

                    var task = client.GetAsync(address)
                      .ContinueWith((taskwithresponse) =>
                      {
                          var response = taskwithresponse.Result;
                          var jsonString = response.Content.ReadAsStringAsync();
                          jsonString.Wait();
                      });
                    task.Wait();
                }
            }
            catch (Exception e)
            {
                throw;
            }
        }





        public static void ChangeDatabase()
        {

            var list = model.First();
            string dbName;
            string[] filePaths = Directory.GetFiles(addressFileSql +"\\", "*.txt",
                                             SearchOption.TopDirectoryOnly);
            string sal = "";
            string group = "";
            bool isCols = false;

            foreach (var item in filePaths)
            {
                isCols = false;
                string fileName = Path.GetFileName(item);
                var files = fileName.Split('_');
                if (files[1] == list.lockNumber || files[1] == "10000")
                {
                    string addressFile = item;
                    if (files[2] == "Ace2")
                    {
                        dbName = "Ace_WebConfig";
                    }
                    else
                    {
                        sal = (files[4].Split('.'))[0];
                        group = files[3];
                        if (files[2] == "Web2")
                            sal = "0000";

                        if (group.Length == 1)
                            group = "0" + group;

                        dbName = ("ACE_" + files[2] + group + sal);

                        if (files.Length == 6)
                        {
                            string nameTemp = (files[5].Split('.'))[0];
                            if (nameTemp == "Col")
                            {
                                isCols = true;
                            }
                        }
                    }




                    string connectionString = String.Format(
                                    @"data source = {0};initial catalog = {1};persist security info = True;user id = {2}; password = {3};  multipleactiveresultsets = True; application name = EntityFramework",
                                    list.SqlServerName, "master", list.SqlUserName, list.SqlPassword);


                    var connection = new SqlConnection(connectionString);

                    connection.Open();
                    var command = connection.CreateCommand();
                    command.CommandText = string.Format(@"IF Not EXISTS (SELECT name FROM master.dbo.sysdatabases WHERE name = N'{0}')
                                                    CREATE DATABASE [{0}] COLLATE SQL_Latin1_General_CP1256_CI_AS", dbName);
                    command.ExecuteNonQuery();

                    string conStr = CreateConnectionString(list.UserName, list.Password, "", files[2] == "Ace2" ? "Config" : files[2], sal, group, 0, "", 0, 0);
                    if (conStr.Length > 100)
                    {
                        db = new ApiModel(conStr);
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

                        if (oldVer < UnitPublic.VerDB || isCols == true)
                        {
                            if (isCols == false)
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

                                        /*if (sql.StartsWith("if Not exists"))
                                        {
                                            command.CommandText = "use " + dbName + " " + sql;
                                            command.ExecuteNonQuery();
                                        }
                                        else
                                        {
                                            db.Database.ExecuteSqlCommand(sql);
                                        }*/

                                        db.Database.ExecuteSqlCommand(sql);
                                        sql = "";
                                    }
                                    catch (Exception e)
                                    {
                                        filestream.Close();
                                        throw;
                                        
                                    }
                                }
                            }

                            if (isCols == false)
                            {
                                sql = string.Format(@"INSERT INTO Web_Version (ver,datever) VALUES ({0},SYSDATETIME())", UnitPublic.VerDB);
                                db.Database.ExecuteSqlCommand(sql);
                            }
                            filestream.Close();
                            File.Delete(item);

                            // return "به روز رسانی انجام شد";
                        }
                    }
                    catch (Exception e)
                    {
                        // return "خطا در اتصال به دیتابیس های کاربرد کامپیوتر";
                        throw;
                    }
                }

            }


            /* string sql;
             int oldVer = 0;
             try
             {
                 try
                 {
                     db.Database.Connection.Open();
                 }
                 catch (Exception e)
                 {
                     string conStr = CreateConnectionString(list.SqlUserName, list.SqlPassword, "", "master", "", "", 0, "", 0, 0);
                     if (conStr.Length > 100)
                     {
                         using (var connection = new SqlConnection(conStr))
                         {
                             connection.Open();
                             var command = connection.CreateCommand();
                             command.CommandText = "CREATE DATABASE Ace_WebConfig";
                             command.ExecuteNonQuery();
                         }
                     }
                     db.Database.Connection.Close();
                     db.Database.Connection.Open();
                 }

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


                     string textFilePath = @"c:\a\WebViews_Ace2.txt";

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
             } */
        }



        public static void TestDatabase(string ace)
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

                    if (ace == "Web1")
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
            /* //string connectionString = String.Format(@"data source = .\sql2014;initial catalog = ACE_Web8011384 ;persist security info = True;user id = sa; password = 114;  multipleactiveresultsets = True; application name = EntityFramework");
             string connectionString = String.Format(@"data source = .\sql2014;initial catalog = ACE_Web8011384 ;
                                                       persist security info = True;user id = sa; password = 114; 
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


        }




    }
}
