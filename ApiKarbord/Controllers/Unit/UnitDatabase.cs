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
                else
                    return null;
            }
            else
            {

                dbName = "ACE_" + ace;
                //dbName += ace == "AFI" ? "1" : "5";
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


        //ایجاد کانکشن استرینگ 
        //اگر سایت ترو باشد یعنی به اس کیو ال ای پی ای
        public static string CreateConnectionString(string userName, string password, string userKarbord, string ace, string sal, string group, long serialNumber, int act)
        {
            try
            {
                List<Access> model = null;
                var client = new HttpClient();

                //Debug
                //string address = String.Format(@"http://127.0.0.1:902/api/Account/InformationSql/{0}/{1}/'{2}'/'{3}'/'{4}'/'{5}'/{6}/{7}", userName, password,userKarbord, ace, group, sal,serialNumber, act);
                string address = String.Format(@"http://localhost:49961/api/Account/InformationSql/{0}/{1}/'{2}'/'{3}'/'{4}'/'{5}'/{6}/{7}", userName, password, userKarbord, ace, group, sal, serialNumber, act);

                //string address = String.Format(@"http://192.168.6.204:902/api/Account/InformationSql/{0}/{1}/'{2}'/'{3}'/'{4}'/'{5}'/{6}/{7}", userName, password,userKarbord, ace, group, sal,serialNumber, act); //  Canada
                // string address = String.Format(@"http://192.168.0.109:902/api/Account/InformationSql/{0}/{1}/'{2}'/'{3}'/'{4}'/'{5}'/{6}/{7}", userName, password,userKarbord, ace, group, sal,serialNumber, act); //  Office 109
                // string address = String.Format(@"http://185.208.174.64:902/api/Account/InformationSql/{0}/{1}/'{2}'/'{3}'/'{4}'/'{5}'/{6}/{7}", userName, password,userKarbord, ace, group, sal,serialNumber, act);//  Interanet

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
        public static Boolean CreateConection(string userName, string password, string userKarbord, string ace, string sal, string group, long serialnumber, int act)
        {
            try
            {
                string conStr = CreateConnectionString(userName, password, userKarbord, ace, sal, group, serialnumber, act);
                if (string.IsNullOrEmpty(conStr))
                {
                    return false;
                }
                db = new ApiModel(conStr);
                //db.Configuration.ProxyCreationEnabled = false;
                return true;
            }
            catch (Exception)
            {
                return false;
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




    }
}
