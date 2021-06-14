using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using ApiKarbord.Controllers.Unit;
using ApiKarbord.Models;

namespace ApiKarbord.Controllers.Unit
{
    public class UnitPublic
    {


        public static int VerDB = 30;
        public static string conString = "";

        public static string titleVer = "API KarbordComputer Test : Ver ";
        public static int VerNumber = 513;

        // public static string titleVer = "API KarbordComputer : Ver ";
        // public static int VerNumber = 1016;


        // Server.MapPath("ini/SqlServerConfig.Ini");
        public static string Appddress; //ادرس نرم افزار
        public static IniFile MyIni;
        public static IniFile MyIniServer;


        public static List<SelectListItem> free = new List<SelectListItem>();
        public static List<SelectListItem> aceList = new List<SelectListItem>(); //لیست نرم افزار ها
        public static List<SelectListItem> accList = new List<SelectListItem>();//لیست گروه های حسابداری
        public static List<SelectListItem> invList = new List<SelectListItem>();//لیست گروه های انبار
        public static List<SelectListItem> fctList = new List<SelectListItem>();//لیست گروه های فروش
        public static List<SelectListItem> afiList = new List<SelectListItem>();//لیست گروه های مالی و بازرگانی

        public static List<SelectListItem> accSalList = new List<SelectListItem>();//لیست سال حسابداری
        public static List<SelectListItem> invSalList = new List<SelectListItem>();//لیست سال انبار
        public static List<SelectListItem> fctSalList = new List<SelectListItem>();//لیست سال فروش
        public static List<SelectListItem> afiSalList = new List<SelectListItem>();//لیست سال مالی و بازرگانی
        private static object str;

        public class listDatabase
        {
            public string name { get; set; }
        }


        //دریافت اطلاعات کاربر
        public static bool UserInformation(string acc, string inv, string fct, string afi)
        {
            try
            {
                aceList.Clear();
                invList.Clear();
                fctList.Clear();
                accList.Clear();
                afiList.Clear();
                invSalList.Clear();
                fctSalList.Clear();
                accSalList.Clear();
                afiSalList.Clear();

                if (!string.IsNullOrEmpty(acc))
                {
                    aceList.Add(new SelectListItem { Value = "ACC", Text = "نرم افزار حسابداری" });
                    string[] accTemp = acc.Split('-');
                    foreach (string accs in accTemp)
                    {
                        accList.Add(new SelectListItem { Value = accs, Text = accs });
                    }

                    var salDB = UnitDatabase.db.Database.SqlQuery<listDatabase>(@"select DISTINCT substring(name,11,4) as name from sys.sysdatabases where name like 'ACE_ACC5%' order by name");
                    foreach (var item in salDB)
                    {
                        accSalList.Add(new SelectListItem { Value = item.name, Text = item.name });
                    }
                }


                if (!string.IsNullOrEmpty(inv))
                {
                    aceList.Add(new SelectListItem { Value = "INV", Text = "نرم افزار انبار" });
                    string[] invTemp = inv.Split('-');
                    foreach (string invs in invTemp)
                    {
                        invList.Add(new SelectListItem { Value = invs, Text = invs });
                    }

                    var salDB = UnitDatabase.db.Database.SqlQuery<listDatabase>(@"select DISTINCT substring(name,11,4) as name from sys.sysdatabases where name like 'ACE_INV5%' order by name");
                    foreach (var item in salDB)
                    {
                        invSalList.Add(new SelectListItem { Value = item.name, Text = item.name });
                    }
                }

                if (!string.IsNullOrEmpty(fct))
                {
                    aceList.Add(new SelectListItem { Value = "FCT", Text = "نرم افزار فروش" });
                    string[] fctTemp = fct.Split('-');
                    foreach (string fcts in fctTemp)
                    {
                        fctList.Add(new SelectListItem { Value = fcts, Text = fcts });
                    }

                    var salDB = UnitDatabase.db.Database.SqlQuery<listDatabase>(@"select DISTINCT substring(name,11,4) as name from sys.sysdatabases where name like 'ACE_FCT5%' order by name");
                    foreach (var item in salDB)
                    {
                        fctSalList.Add(new SelectListItem { Value = item.name, Text = item.name });
                    }
                }

                if (!string.IsNullOrEmpty(afi))
                {
                    aceList.Add(new SelectListItem { Value = "AFI", Text = "نرم افزار مالی بازرگانی" });
                    string[] afiTemp = afi.Split('-');
                    foreach (string afis in afiTemp)
                    {
                        afiList.Add(new SelectListItem { Value = afis, Text = afis });
                    }

                    var salDB = UnitDatabase.db.Database.SqlQuery<listDatabase>(@"select DISTINCT substring(name,11,4) as name from sys.sysdatabases where name like 'ACE_AFI1%' order by name");
                    foreach (var item in salDB)
                    {
                        afiSalList.Add(new SelectListItem { Value = item.name, Text = item.name });
                    }
                }
                return true;
            }
            catch (Exception)
            {
                return false;
                throw;
            }
        }

        public static string MD5Hash(string itemToHash)
        {
            return string.Join("", MD5.Create().ComputeHash(Encoding.ASCII.GetBytes(itemToHash)).Select(s => s.ToString("x2")));
        }

        public static string Encrypt(string str)
        {
            string EncrptKey = "2013;[pnuLIT)WebCodeExpert";
            byte[] byKey = { };
            byte[] IV = { 18, 52, 86, 120, 144, 171, 205, 239 };
            byKey = System.Text.Encoding.UTF8.GetBytes(EncrptKey.Substring(0, 8));
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            byte[] inputByteArray = Encoding.UTF8.GetBytes(str);
            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(byKey, IV), CryptoStreamMode.Write);
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();
            return Convert.ToBase64String(ms.ToArray());
        }

        public static string Decrypt(string str)
        {
            str = str.Replace(" ", "+");
            string DecryptKey = "2013;[pnuLIT)WebCodeExpert";
            byte[] byKey = { };
            byte[] IV = { 18, 52, 86, 120, 144, 171, 205, 239 };
            byte[] inputByteArray = new byte[str.Length];

            byKey = System.Text.Encoding.UTF8.GetBytes(DecryptKey.Substring(0, 8));
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            inputByteArray = Convert.FromBase64String(str.Replace(" ", "+"));
            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(byKey, IV), CryptoStreamMode.Write);
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();
            System.Text.Encoding encoding = System.Text.Encoding.UTF8;
            return encoding.GetString(ms.ToArray());
        }
        public static string ConvertTextWebToWin(string text)
        {
            int i = 0;
            string data = "";
            string[] splitted = text.Split('\n');
            foreach (string substring in splitted)
            {
                i++;
                if (i <= splitted.Count() - 1)
                    data += substring + (char)(13) + (char)(10);
                else
                    data += substring;
            }
            return data;
        }


        public static string SpiltCodeCama(string code)
        {
            string sql = "";
            if (code != "")
            {
                string[] Code = code.Split('*');
                for (int i = 0; i < Code.Length; i++)
                {
                    if (i < Code.Length - 1)
                        sql += string.Format("{0} ,", Code[i]);
                    else
                        sql += string.Format("{0}", Code[i]);
                }
            }
            return sql;
        }


        public static string SpiltCodeAnd(string field, string code)
        {
            string sql = "";
            if (code != "" && code != null)
            {
                sql += " and ( ";
                string[] Code = code.Split('*');
                for (int i = 0; i < Code.Length; i++)
                {
                    if (i < Code.Length - 1)
                        sql += string.Format("  {0} = N'{1}' Or ", field, Code[i]);
                    else
                        sql += string.Format("  {0} = N'{1}' )", field, Code[i]);
                }
            }
            return sql;
        }

        public static string SpiltCodeLike(string field, string code)
        {
            string sql = "";
            if (code != "")
            {
                //(accCode like '110-01-%' or accCode = '110-01'
                sql += " and ( ";
                string[] Code = code.Split('*');
                for (int i = 0; i < Code.Length; i++)
                {
                    if (i < Code.Length - 1)
                        sql += string.Format(" ( {0} like N'{1}-%' Or {0} = N'{1}' ) or ", field, Code[i]);
                    else
                        sql += string.Format(" ( {0} like N'{1}-%' Or {0} = N'{1}' ) )", field, Code[i]);
                }
            }
            return sql;
        }

        public static int GetPersianDaysDiffDate(string Date1, string Date2)
        {
            try
            {
                int year1 = Convert.ToInt16(Date1.Substring(0, 4));
                int month1 = Convert.ToInt16(Date1.Substring(5, 2));
                int day1 = Convert.ToInt16(Date1.Substring(8, 2));

                int year2 = Convert.ToInt16(Date2.Substring(0, 4));
                int month2 = Convert.ToInt16(Date2.Substring(5, 2));
                int day2 = Convert.ToInt16(Date2.Substring(8, 2));

                System.Globalization.PersianCalendar calendar = new System.Globalization.PersianCalendar();
                DateTime dt1 = calendar.ToDateTime(year1, month1, day1, 0, 0, 0, 0);
                DateTime dt2 = calendar.ToDateTime(year2, month2, day2, 0, 0, 0, 0);
                TimeSpan ts = dt2.Subtract(dt1);

                return ts.Days;
            }
            catch (Exception e)
            {
                return 1;
                throw;
            }
        }

    }
}