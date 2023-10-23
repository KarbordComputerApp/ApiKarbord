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

using System.Data;
using System.Drawing;
using System.Globalization;

namespace ApiKarbord.Controllers.Unit
{
    public class UnitPublic
    {


        public static int VerDB = 48;
        public static string conString = "";

        //public static string titleVer = "API KarbordComputer Test : Ver ";
        //public static int VerNumber = 518;

        public static string titleVer = "API KarbordComputer : Ver ";
        public static int VerNumber = 1034;


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


        public const string Web1 = "Web1"; // مالی بازرگانی
        public const string Web2 = "Web2"; // اتوماسیون 
        public const string Web8 = "Web8"; // سیستم جامع

        public const string access_View = "View";

        public const string access_ADOC = "ADOC";
        public const string access_SPFCT = "SPFCT";
        public const string access_SFCT = "SFCT";
        public const string access_SRFCT = "SRFCT";
        public const string access_SHVL = "SHVL";
        public const string access_SEXT = "SEXT";
        public const string access_PFORD = "PFORD";
        public const string access_PPFCT = "PPFCT";
        public const string access_PFCT = "PFCT";
        public const string access_PRFCT = "PRFCT";
        public const string access_SFORD = "SFORD";
        public const string access_IIDOC = "IIDOC";
        public const string access_IODOC = "IODOC";
        public const string access_TrzAcc = "TrzAcc";
        public const string access_Dftr = "Dftr";
        public const string access_ADocR = "ADocR";
        public const string access_TChk = "TChk";
        public const string access_FDocR_S = "FDocR_S";
        public const string access_FDocR_P = "FDocR_P";
        public const string access_TrzFKala_S = "TrzFKala_S";
        public const string access_TrzFKala_P = "TrzFKala_P";
        public const string access_TrzFCust_S = "TrzFCust_S";
        public const string access_TrzFCust_P = "TrzFCust_P";
        public const string access_Krdx = "Krdx";
        public const string access_TrzIKala = "TrzIKala";
        public const string access_TrzIKalaExf = "TrzIKalaExf";
        public const string access_IDocR = "IDocR";
        public const string access_Kala = "Kala";
        public const string access_Cust = "Cust";
        public const string access_Acc = "Acc";
        public const string access_Opr = "Opr";
        public const string access_Mkz = "Mkz";
        public const string access_AGMkz = "AGMkz";
        public const string access_AGOpr = "AGOpr";
        public const string access_Arz = "Arz";
        public const string access_ZAcc = "ZAcc";
        public const string access_ErjDocK = "ErjDocK";
        public const string access_ErjDocErja = "ErjDocErja";
        public const string access_ErjDoc = "ErjDoc";
        public const string access_Erja_Resive = "Erja_Resive";
        public const string access_Erja_Send = "Erja_Send";
        public const string access_Chante_FDoc_Moved = "Chante_FDoc_Moved";


        public const int act_View = 0;
        public const int act_Edit = 1;
        public const int act_New = 2;
        public const int act_Delete = 3;
        public const int act_EditBand = 4;
        public const int act_NewBand = 5;
        public const int act_DeleteBand = 6;
        public const int act_ChangrStatus = 7;
        public const int act_Move = 8;
        public const int act_Report = 9;

        public static string ModeCodeConnection(string modeCode)
        {
            if (modeCode == "51") modeCode = access_SPFCT;
            else if (modeCode == "52") modeCode = access_SFCT;
            else if (modeCode == "53") modeCode = access_SRFCT;
            else if (modeCode == "54") modeCode = access_PPFCT;
            else if (modeCode == "55") modeCode = access_PFCT;
            else if (modeCode == "56") modeCode = access_PRFCT;
            else if (modeCode == "SORD") modeCode = access_SFORD;
            else if (modeCode == "PORD") modeCode = access_PFORD;
            return modeCode;
        }

        public class listDatabase
        {
            public string name { get; set; }
        }


        //دریافت اطلاعات کاربر
        /*  public static bool UserInformation(string acc, string inv, string fct, string afi)
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
          */
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



        public static string CreateSql_IDocB(AFI_IDocBi band, long serialNumber, int bandNumber)
        {
            string sql = string.Format(CultureInfo.InvariantCulture,
                          @"DECLARE	@return_value int
                            EXEC	@return_value = [dbo].[Web_SaveIDoc_BI_Temp]
		                            @SerialNumber = {0},
		                            @BandNo = {1},
		                            @KalaCode = N'{2}',
		                            @Amount1 = {3},
		                            @Amount2 = {4},
		                            @Amount3 = {5},
		                            @UnitPrice = {6},
		                            @TotalPrice = {7},
		                            @MainUnit = {8},
		                            @Comm = N'{9}',
                                    @Up_Flag = {10},
                                    @OprCode = N'{11}',
		                            @MkzCode = N'{12}',
                                    @BandSpec = N'{13}',
                                    @ArzCode = N'{14}',
                                    @ArzRate = {15},
                                    @ArzValue = {16},
                                    @KalaFileNo = N'{17}',
                                    @KalaState = N'{18}',
                                    @KalaExf1 = N'{19}',
                                    @KalaExf2 = N'{20}',
                                    @KalaExf3 = N'{21}',
                                    @KalaExf4 = N'{22}',
                                    @KalaExf5 = N'{23}',
                                    @KalaExf6 = N'{24}',
                                    @KalaExf7 = N'{25}',
                                    @KalaExf8 = N'{26}',
                                    @KalaExf9 = N'{27}',
                                    @KalaExf10 = N'{28}',
                                    @KalaExf11 = N'{29}',
                                    @KalaExf12 = N'{30}',
                                    @KalaExf13 = N'{31}',
                                    @KalaExf14 = N'{32}',
                                    @KalaExf15 = N'{33}'
                            SELECT	'Return Value' = @return_value",
                            serialNumber,
                            bandNumber,
                            band.KalaCode,
                            band.Amount1 ?? 0,
                            band.Amount2 ?? 0,
                            band.Amount3 ?? 0,
                            band.UnitPrice ?? 0,
                            band.TotalPrice ?? 0,
                            band.MainUnit ?? 1,
                            UnitPublic.ConvertTextWebToWin(band.Comm ?? ""),
                            band.Up_Flag,
                            band.OprCode,
                            band.MkzCode,
                            UnitPublic.ConvertTextWebToWin(band.BandSpec ?? ""),
                            band.ArzCode ?? "",
                            band.ArzRate ?? 0,
                            band.ArzValue ?? 0,
                            band.KalaFileNo,
                            band.KalaState,
                            band.KalaExf1,
                            band.KalaExf2,
                            band.KalaExf3,
                            band.KalaExf4,
                            band.KalaExf5,
                            band.KalaExf6,
                            band.KalaExf7,
                            band.KalaExf8,
                            band.KalaExf9,
                            band.KalaExf10,
                            band.KalaExf11,
                            band.KalaExf12,
                            band.KalaExf13,
                            band.KalaExf14,
                            band.KalaExf15
                            );
            return sql;
        }
        public static string CreateSql_IDocH(AFI_IDocHi head, bool flagTest)
        {
            string sql = "";
            if (head.SerialNumber == 0 || flagTest == true)
            {
                sql = string.Format(
                        @"DECLARE	@return_value nvarchar(50),
		                            @DocNo_Out int
                          EXEC	@return_value = [dbo].[{38}]
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
                                    @Status = N'{38}',
		                            @DOCNO_OUT = @DOCNO_OUT OUTPUT
                            SELECT	'return_value' = @return_value +'-'+  CONVERT(nvarchar, @DOCNO_OUT)",
                                        head.DocNoMode,
                                        head.InsertMode,
                                        head.ModeCode,
                                        head.DocNo ?? 0,
                                        head.StartNo,
                                        head.EndNo,
                                        head.BranchCode,
                                        head.UserCode,
                                        head.SerialNumber,
                                        head.DocDate ?? string.Format("{0:yyyy/MM/dd}", DateTime.Now.AddDays(-1)),
                                        head.Spec,
                                        head.TahieShode,
                                        head.ThvlCode ?? "",
                                        head.KalaPriceCode ?? 0,
                                        head.InvCode,
                                        head.Eghdam,
                                        head.F01,
                                        head.F02,
                                        head.F03,
                                        head.F04,
                                        head.F05,
                                        head.F06,
                                        head.F07,
                                        head.F08,
                                        head.F09,
                                        head.F10,
                                        head.F11,
                                        head.F12,
                                        head.F13,
                                        head.F14,
                                        head.F15,
                                        head.F16,
                                        head.F17,
                                        head.F18,
                                        head.F19,
                                        head.F20,
                                        head.Tanzim,
                                        UnitPublic.ConvertTextWebToWin(head.Footer ?? ""),
                                        flagTest == true ? "Web_SaveIDoc_HI_Temp" : "Web_SaveIDoc_HI",
                                        head.Status
                                        );
            }
            else if (head.SerialNumber > 0 && flagTest == false) // update
            {
                sql = string.Format(
                        @"DECLARE	@return_value nvarchar(50),
		                            @DocNo_Out int
                          EXEC	@return_value = [dbo].[Web_SaveIDoc_HU]
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
                                    head.DocNoMode,
                                    head.InsertMode,
                                    head.ModeCode,
                                    head.DocNo,
                                    head.StartNo,
                                    head.EndNo,
                                    head.BranchCode,
                                    head.UserCode,
                                    head.SerialNumber,
                                    head.DocDate ?? string.Format("{0:yyyy/MM/dd}", DateTime.Now.AddDays(-1)),
                                    head.Spec,
                                    head.TahieShode,
                                    head.ThvlCode,
                                    head.KalaPriceCode ?? 0,
                                    head.InvCode,
                                    head.Status,
                                    UnitPublic.ConvertTextWebToWin(head.Footer ?? ""),
                                    head.F01,
                                    head.F02,
                                    head.F03,
                                    head.F04,
                                    head.F05,
                                    head.F06,
                                    head.F07,
                                    head.F08,
                                    head.F09,
                                    head.F10,
                                    head.F11,
                                    head.F12,
                                    head.F13,
                                    head.F14,
                                    head.F15,
                                    head.F16,
                                    head.F17,
                                    head.F18,
                                    head.F19,
                                    head.F20,
                                    head.OprCode,
                                    head.MkzCode,
                                    head.Tanzim);
            }
            return sql;
        }







        public static string CreateSql_FDocH(AFI_FDocHi head, bool flagTest)
        {
            string sql = string.Format(
                          @"DECLARE	@return_value nvarchar(50),
		                  @DocNo_Out nvarchar(50)
                          EXEC	@return_value = [dbo].[{0}]
		                            @DOCNOMODE = {1},
		                            @INSERTMODE = {2},
		                            @MODECODE = {3} ,
		                            @DOCNO = '{4}',
		                            @STARTNO = {5},
		                            @ENDNO = {6},
		                            @BRANCHCODE = {7},
		                            @USERCODE = '''{8}''',
		                            @SERIALNUMBER = {9},
		                            @DOCDATE = '{10}',
		                            @SPEC = N'{11}',
		                            @TAHIESHODE = '{12}',
		                            @CUSTCODE = '{13}',
		                            @VSTRCODE = '{14}',
		                            @VSTRPER = {15},
		                            @PAKHSHCODE = '{16}',
		                            @KALAPRICECODE = {17},
		                            @ADDMINSPEC1 = N'{18}',
		                            @ADDMINSPEC2 = N'{19}',
		                            @ADDMINSPEC3 = N'{20}',
		                            @ADDMINSPEC4 = N'{21}',
		                            @ADDMINSPEC5 = N'{22}',
		                            @ADDMINSPEC6 = N'{23}',
		                            @ADDMINSPEC7 = N'{24}',
		                            @ADDMINSPEC8 = N'{25}',
		                            @ADDMINSPEC9 = N'{26}',
		                            @ADDMINSPEC10 = N'{27}',
		                            @ADDMINPRICE1 = {28},
		                            @ADDMINPRICE2 = {29},
		                            @ADDMINPRICE3 = {30},
		                            @ADDMINPRICE4 = {31},
		                            @ADDMINPRICE5 = {32},
		                            @ADDMINPRICE6 = {33},
		                            @ADDMINPRICE7 = {34},
		                            @ADDMINPRICE8 = {35},
		                            @ADDMINPRICE9 = {36},
		                            @ADDMINPRICE10 = {37},
                                    @InvCode = '{38}',
                                    @Eghdam = '''{39}''',
                                    @F01 = N'{40}',
                                    @F02 = N'{41}',
                                    @F03 = N'{42}',
                                    @F04 = N'{43}',
                                    @F05 = N'{44}',
                                    @F06 = N'{45}',
                                    @F07 = N'{46}',
                                    @F08 = N'{47}',
                                    @F09 = N'{48}',
                                    @F10 = N'{49}',
                                    @F11 = N'{50}',
                                    @F12 = N'{51}',
                                    @F13 = N'{52}',
                                    @F14 = N'{53}',
                                    @F15 = N'{54}',
                                    @F16 = N'{55}',
                                    @F17 = N'{56}',
                                    @F18 = N'{57}',
                                    @F19 = N'{58}',
                                    @F20 = N'{59}',
                                    @PaymentType = {60},
                                    @Footer = N'{61}',
                                    @TotalValue = '{62}',
                                    @Tanzim = '{63}',
                                    @CustAddrValid = {64},
                                    @CustOstan = '{65}',
                                    @CustShahrestan = '{66}',
                                    @CustRegion = '{67}',
                                    @CustCity = '{68}',
                                    @CustStreet = '{69}',
                                    @CustAlley = '{70}',
                                    @CustPlack = '{71}',
                                    @CustZipCode = '{72}',
                                    @CustTel = '{73}',
                                    @CustMobile = '{74}',
		                            @DOCNO_OUT = @DOCNO_OUT OUTPUT
                            SELECT	'return_value' = @return_value +'@'+ ltrim(@DOCNO_OUT)",
                                             flagTest == true ? "Web_SaveFDoc_HI_Temp" : "Web_SaveFDoc_HI",
                                             head.DocNoMode,
                                             head.InsertMode,
                                             head.ModeCode,
                                             head.DocNo,
                                             head.StartNo,
                                             head.EndNo,
                                             head.BranchCode,
                                             head.UserCode,
                                             head.SerialNumber,
                                             head.DocDate ?? string.Format("{0:yyyy/MM/dd}", DateTime.Now.AddDays(-1)),
                                             head.Spec,
                                             head.TahieShode,
                                             head.CustCode,
                                             head.VstrCode,
                                             head.VstrPer,
                                             head.PakhshCode,
                                             head.KalaPriceCode ?? 0,
                                             head.AddMinSpec1,
                                             head.AddMinSpec2,
                                             head.AddMinSpec3,
                                             head.AddMinSpec4,
                                             head.AddMinSpec5,
                                             head.AddMinSpec6,
                                             head.AddMinSpec7,
                                             head.AddMinSpec8,
                                             head.AddMinSpec9,
                                             head.AddMinSpec10,
                                             head.AddMinPrice1,
                                             head.AddMinPrice2,
                                             head.AddMinPrice3,
                                             head.AddMinPrice4,
                                             head.AddMinPrice5,
                                             head.AddMinPrice6,
                                             head.AddMinPrice7,
                                             head.AddMinPrice8,
                                             head.AddMinPrice9,
                                             head.AddMinPrice10,
                                             head.InvCode,
                                             head.Eghdam,
                                             head.F01,
                                             head.F02,
                                             head.F03,
                                             head.F04,
                                             head.F05,
                                             head.F06,
                                             head.F07,
                                             head.F08,
                                             head.F09,
                                             head.F10,
                                             head.F11,
                                             head.F12,
                                             head.F13,
                                             head.F14,
                                             head.F15,
                                             head.F16,
                                             head.F17,
                                             head.F18,
                                             head.F19,
                                             head.F20,
                                             head.PaymentType,
                                             UnitPublic.ConvertTextWebToWin(head.Footer ?? ""),
                                             head.TotalValue ?? 0,
                                             head.Tanzim,
                                             head.CustAddrValid ?? 0,
                                             head.CustOstan,
                                             head.CustShahrestan,
                                             head.CustRegion,
                                             head.CustCity,
                                             head.CustStreet,
                                             head.CustAlley,
                                             head.CustPlack,
                                             head.CustZipCode,
                                             head.CustTel,
                                             head.CustMobile
                                             );

            return sql;
        }


        public static string CreateSql_CalcAddmin(CalcAddmin calcAddmin)
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
            return sql;

        }

        public static string CreateSql_TashimBand(TashimBand tashimBand)
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
            return sql;

        }


        public static string CreateSql_FDocB(AFI_FDocBi band, long serialNumber, int bandNumber)
        {
            string sql = string.Format(CultureInfo.InvariantCulture,
                          @"DECLARE	@return_value int 
                            EXEC	@return_value = [dbo].[Web_SaveFDoc_BI_Temp]
		                            @SerialNumber = {0},
		                            @BandNo = {1},
		                            @KalaCode = N'{2}',
		                            @Amount1 = {3},
		                            @Amount2 = {4},
		                            @Amount3 = {5},
		                            @UnitPrice = {6},
		                            @TotalPrice = {7},
                                    @Discount = {8},
		                            @MainUnit = {9},
		                            @Comm = N'{10}',
                                    @Up_Flag = {11},
                                    @OprCode = N'{12}',
		                            @MkzCode = N'{13}',
		                            @InvCode = N'{14}',
                                    @LFctSerialNumber = {15},
                                    @InvSerialNumber = {16},
                                    @LinkNumber = {17},
                                    @LinkYear = {18},
                                    @LinkProg = N'{19}',
                                    @BandSpec = N'{20}',
                                    @ArzCode = N'{21}',
                                    @ArzRate = {22},
                                    @ArzValue = {23},
                                    @KalaFileNo = N'{24}',
                                    @KalaState = N'{25}',
                                    @KalaExf1 = N'{26}',
                                    @KalaExf2 = N'{27}',
                                    @KalaExf3 = N'{28}',
                                    @KalaExf4 = N'{29}',
                                    @KalaExf5 = N'{30}',
                                    @KalaExf6 = N'{31}',
                                    @KalaExf7 = N'{32}',
                                    @KalaExf8 = N'{33}',
                                    @KalaExf9 = N'{34}',
                                    @KalaExf10 = N'{35}',
                                    @KalaExf11 = N'{36}',
                                    @KalaExf12 = N'{37}',
                                    @KalaExf13 = N'{38}',
                                    @KalaExf14 = N'{39}',
                                    @KalaExf15 = N'{40}'
                            SELECT	'Return Value' = @return_value
                            ",
                        serialNumber,
                        bandNumber,
                        band.KalaCode,
                        band.Amount1 ?? 0,
                        band.Amount2 ?? 0,
                        band.Amount3 ?? 0,
                        band.UnitPrice ?? 0,
                        band.TotalPrice ?? 0,
                        band.Discount ?? 0,
                        band.MainUnit ?? 1,
                        UnitPublic.ConvertTextWebToWin(band.Comm ?? ""),
                        band.Up_Flag,
                        band.OprCode,
                        band.MkzCode,
                        band.InvCode,
                        band.LFctSerialNumber ?? 0,
                        band.InvSerialNumber ?? 0,
                        band.LinkNumber ?? 0,
                        band.LinkYear ?? 0,
                        band.LinkProg,
                        UnitPublic.ConvertTextWebToWin(band.BandSpec ?? ""),
                        band.ArzCode ?? "",
                        band.ArzRate ?? 0,
                        band.ArzValue ?? 0,
                        band.KalaFileNo,
                        band.KalaState,
                        band.KalaExf1,
                        band.KalaExf2,
                        band.KalaExf3,
                        band.KalaExf4,
                        band.KalaExf5,
                        band.KalaExf6,
                        band.KalaExf7,
                        band.KalaExf8,
                        band.KalaExf9,
                        band.KalaExf10,
                        band.KalaExf11,
                        band.KalaExf12,
                        band.KalaExf13,
                        band.KalaExf14,
                        band.KalaExf15
                        );
            return sql;
        }







        public static ResTest SetErrorSanad(List<TestDocB> TestDocB)
        {
            const string FirstMess = "بند شماره ";
            ResTest resTest = new ResTest();
            List<TestDoc> res = new List<TestDoc>();

            int countError = 0;
            int countWarnning = 0;
            foreach (var item in TestDocB)
            {
                if (item.Test == 1)
                    countWarnning++;
                else
                    countError++;

                TestDoc band = new TestDoc();
                band.Mode = item.Test;
                band.ModeName = item.Test == 1 ? "Warnning" : "Error";

                string spec = FirstMess + item.BandNo + " : ";
                if (item.TestName == "Opr") spec += "پروژه مشخص نشده است";
                else if (item.TestName == "Mkz") spec += "مرکز هزینه مشخص نشده است";
                else if (item.TestName == "Arz") spec += "ارز معرفی نشده است";
                else if (item.TestName == "ZeroAmount") spec += "مقدار صفر است";
                else if (item.TestName == "ZeroPrice") spec += "مبلغ صفر است";
                else if (item.TestName == "Thvl") spec = "تحویل دهنده/گیرنده انتخاب نشده است";
                else if (item.TestCap != "") spec = item.TestCap;
                band.Spec = spec;
                res.Add(band);
            }
            resTest.CountWarnning = countWarnning;
            resTest.CountError = countError;
            resTest.Status = countError > 0 ? "Error" : "Success";
            resTest.Data = res;
            return resTest;
        }














    }
}