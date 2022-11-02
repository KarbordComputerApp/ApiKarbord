﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.UI;
using ApiKarbord.Controllers.Unit;
using ApiKarbord.Models;
using Support.Models;

namespace ApiKarbord.Controllers.AFI.data
{
    public class Web_DataSupportController : ApiController
    {

        /*
        public string con = "";
        public static string ConnectionSupport()
        {
            string IniPath = HttpContext.Current.Server.MapPath("~/Content/ini/ServerConfig.Ini");
            IniFile MyIni = new IniFile(IniPath);
            string SqlServerName = MyIni.Read("SqlServerName", "ApiSupport");
            string DatabaseName = MyIni.Read("DatabaseName", "ApiSupport");
            string SqlUserName = MyIni.Read("SqlUserName", "ApiSupport");
            string SqlPassword = MyIni.Read("SqlPassword", "ApiSupport");

            string connectionString = String.Format(
                    @"data source = {0};initial catalog = {1};persist security info = True;user id = {2}; password = {3};  multipleactiveresultsets = True; application name = EntityFramework",
                    SqlServerName, DatabaseName, SqlUserName, SqlPassword);

            return connectionString;
        }


        public static string EncodePassword(string originalPassword)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            Byte[] originalBytes = ASCIIEncoding.Default.GetBytes(originalPassword);
            Byte[] encodedBytes = md5.ComputeHash(originalBytes);

            return BitConverter.ToString(encodedBytes).Replace("-", "");
        }


        public class LoginObject
        {
            public int LockNumber { get; set; }
            public string Pass { get; set; }

        }

        [Route("api/DataSupport/Login/")]
        public async Task<IHttpActionResult> PostLogin(LoginObject LoginObject)
        {
            if (con == "") con = ConnectionSupport();
            ApiModel db = new ApiModel(con);
            string sql = string.Format(@"select * from Users where (LockNumber = {0} and Password = '{1}')", LoginObject.LockNumber, EncodePassword(LoginObject.Pass));
            var list = db.Database.SqlQuery<Users>(sql).ToList();
            return Ok(list);
        }




        [Route("api/DataSupport/FAG/")]
        public async Task<IHttpActionResult> GetFAG()
        {
            if (con == "") con = ConnectionSupport();
            ApiModel db = new ApiModel(con);
            string sql = string.Format(@"select distinct 0 as id , Title , Title as Description , Title as Body , 0 as SortId  from FAQs
                                         union all
                                         select id,Title,Description,Body,SortId from FAQs
                                         order by title , SortId");
            var list = db.Database.SqlQuery<FAQs>(sql).ToList();
            return Ok(list);
        }


        [Route("api/DataSupport/AceMessages/")]
        public async Task<IHttpActionResult> GetAceMessages()
        {
            if (con == "") con = ConnectionSupport();
            ApiModel db = new ApiModel(con);
            string sql = string.Format(@"select * from AceMessages where ExtraParam = '' and Active = 1 and Expired = 0  order by id desc");
            var list = db.Database.SqlQuery<AceMessages>(sql).ToList();
            return Ok(list);
        }






        public class FinancialDocumentsObject
        {
            public int LockNumber { get; set; }

        }

        [Route("api/DataSupport/FinancialDocuments/")]
        public async Task<IHttpActionResult> PostFinancialDocuments(FinancialDocumentsObject FinancialDocumentsObject)
        {
            if (con == "") con = ConnectionSupport();
            ApiModel db = new ApiModel(con);
            string sql = string.Format(@"select * from FinancialDocuments where LockNumber = {0} order by SubmitDate desc", FinancialDocumentsObject.LockNumber);
            var list = db.Database.SqlQuery<FinancialDocuments>(sql).ToList();
            return Ok(list);
        }




        public class CustomerFilesObject
        {
            public int LockNumber { get; set; }

        }

        [Route("api/DataSupport/CustomerFiles/")]
        public async Task<IHttpActionResult> PostCustomerFiles(CustomerFilesObject CustomerFilesObject)
        {
            if (con == "") con = ConnectionSupport();
            ApiModel db = new ApiModel(con);
            string sql = string.Format(@"select *,(select count(id) from  CustomerFileDownloadInfos where FileId = c.id ) as CountDownload 
                                         from CustomerFiles as c where LockNumber in( 10000 , {0} ) and Disabled = 0  order by LockNumber desc , id desc", CustomerFilesObject.LockNumber);
            var list = db.Database.SqlQuery<CustomerFiles>(sql).ToList();
            return Ok(list);
        }




        [Route("api/DataSupport/CustomerFilesCount/")]
        public async Task<IHttpActionResult> PostCustomerFilesCount(CustomerFilesObject CustomerFilesObject)
        {
            if (con == "") con = ConnectionSupport();
            ApiModel db = new ApiModel(con);
            string sql = string.Format(@"select count(id) from CustomerFiles where LockNumber = {0} and Disabled = 0", CustomerFilesObject.LockNumber);
            var list = db.Database.SqlQuery<int>(sql).ToList();
            return Ok(list);
        }



        public string GetPath(int IdConfig)
        {
            if (con == "") con = ConnectionSupport();
            ApiModel db = new ApiModel(con);
            string sql = string.Format(@"select value from Configs where id = {0}", IdConfig.ToString());
            string list = db.Database.SqlQuery<string>(sql).Single();
            return "C:" + list;
        }


        [HttpGet]
        [Route("api/DataSupport/FinancialDocumentsDownload/{lockNo}/{idFinancial}")]
        public HttpResponseMessage FinancialDocumentsDownload(int lockNo, long idFinancial)
        {
            if (con == "") con = ConnectionSupport();
            ApiModel db = new ApiModel(con);

            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);

            string path = GetPath(8) + "\\" + lockNo.ToString();
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            string[] files = Directory.GetFiles(path, string.Format("{0}_*", idFinancial));

            FileInfo f = new FileInfo(files[0]);

            if (!File.Exists(files[0]))
            {
                response.StatusCode = HttpStatusCode.NotFound;
                response.ReasonPhrase = string.Format("File not found: {0} .", files[0]);
                throw new HttpResponseException(response);
            }


            byte[] bytes = File.ReadAllBytes(files[0].ToString());

            string sql = string.Format(@"update FinancialDocuments set ReadStatus = 1 , Download = isnull(Download,0) + 1 where id = {0} select 1", idFinancial);
            var list = db.Database.SqlQuery<int>(sql).Single();
            db.SaveChanges();

            response.Content = new ByteArrayContent(bytes);
            response.Content.Headers.ContentLength = bytes.LongLength;
            response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment");
            response.Content.Headers.ContentDisposition.FileName = f.Name;
            response.Content.Headers.ContentType = new MediaTypeHeaderValue(MimeMapping.GetMimeMapping(files[0]));
            return response;
        }


        public static string MergePaths(string part1, string part2)
        {
            return part1.TrimEnd(Path.DirectorySeparatorChar) + Path.DirectorySeparatorChar + part2.TrimStart(Path.DirectorySeparatorChar);
        }

        [HttpGet]
        [Route("api/DataSupport/CustomerDocumentsDownload/{lockNo}/{idCustomer}")]

        public HttpResponseMessage CustomerDocumentsDownload(int lockNo, long idCustomer)
        {
            if (con == "") con = ConnectionSupport();
            ApiModel db = new ApiModel(con);

            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);

            string path = GetPath(2);
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            string sql = string.Format(@"select *, 0 as CountDownload from CustomerFiles where id = {0}", idCustomer);
            var list = db.Database.SqlQuery<CustomerFiles>(sql).Single();

            string fullFileName = MergePaths(path, list.FilePath);

            FileInfo f = new FileInfo(fullFileName);
            string fullname = f.DirectoryName;

            string[] files = Directory.GetFiles(fullname, f.Name);


            if (!File.Exists(files[0]))
            {
                response.StatusCode = HttpStatusCode.NotFound;
                response.ReasonPhrase = string.Format("File not found: {0} .", files[0]);
                throw new HttpResponseException(response);
            }


            byte[] bytes = File.ReadAllBytes(files[0].ToString());

            response.Content = new ByteArrayContent(bytes);
            response.Content.Headers.ContentLength = bytes.LongLength;
            response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment");
            response.Content.Headers.ContentDisposition.FileName = f.Name;
            response.Content.Headers.ContentType = new MediaTypeHeaderValue(MimeMapping.GetMimeMapping(files[0]));

            sql = string.Format(@"INSERT INTO CustomerFileDownloadInfos (FileId,IP,DownloadTime) VALUES ({0},'',getdate()) select 1", idCustomer);
            var list1 = db.Database.SqlQuery<int>(sql).Single();
            db.SaveChanges();

            return response;
        }


        [HttpGet]
        [Route("api/DataSupport/CustomerFiles/{lockNo}/{idCustomerFiles}")]
        public HttpResponseMessage CustomerFiles(int lockNo, long idCustomerFiles)
        {
            if (con == "") con = ConnectionSupport();
            ApiModel db = new ApiModel(con);

            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);

            string path = GetPath(2) + "\\" + lockNo.ToString();
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            string[] files = Directory.GetFiles(path, string.Format("{0}_*", idCustomerFiles));
            FileInfo f = new FileInfo(files[0]);

            if (!File.Exists(files[0]))
            {
                response.StatusCode = HttpStatusCode.NotFound;
                response.ReasonPhrase = string.Format("File not found: {0} .", files[0]);
                throw new HttpResponseException(response);
            }


            byte[] bytes = File.ReadAllBytes(files[0].ToString());

            // string sql = string.Format(@"update FinancialDocuments set ReadStatus = 1 , Download = isnull(Download,0) + 1 where id = {0} select 1", idCustomerFiles);
            // var list = db.Database.SqlQuery<int>(sql).Single();
            // db.SaveChangesAsync();

            response.Content = new ByteArrayContent(bytes);
            response.Content.Headers.ContentLength = bytes.LongLength;
            response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment");
            response.Content.Headers.ContentDisposition.FileName = f.Name;
            response.Content.Headers.ContentType = new MediaTypeHeaderValue(MimeMapping.GetMimeMapping(files[0]));
            return response;
        }





        [Route("api/DataSupport/UploadFile/")]
        public async Task<IHttpActionResult> UploadFile()
        {
            if (con == "") con = ConnectionSupport();
            ApiModel db = new ApiModel(con);
            string path = GetPath(3);
            var Atch = System.Web.HttpContext.Current.Request.Files["Atch"];
            var lockNumber = System.Web.HttpContext.Current.Request["LockNumber"];

            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            string pathtemp = path + "\\TempDirectory";
            if (!Directory.Exists(pathtemp))
                Directory.CreateDirectory(pathtemp);

            pathtemp = path + "\\TempDirectory\\" + lockNumber;
            if (!Directory.Exists(pathtemp))
                Directory.CreateDirectory(pathtemp);



            int lenght = Atch.ContentLength;
            byte[] filebyte = new byte[lenght];
            Atch.InputStream.Read(filebyte, 0, lenght);
            File.WriteAllBytes(pathtemp + "\\" + Atch.FileName, filebyte);
            return Ok("Ok");
        }



        public class FinalUploadFileObject
        {
            public int LockNumber { get; set; }

            public string Desc { get; set; }

        }

        [Route("api/DataSupport/FinalUploadFile/")]
        public async Task<IHttpActionResult> FinalUploadFile(FinalUploadFileObject FinalUploadFileObject)
        {
            if (con == "") con = ConnectionSupport();
            ApiModel db = new ApiModel(con);

            string path = GetPath(3);
            string date = CustomPersianCalendar.ToPersianDate(DateTime.Now).Replace('/', '-');
            string ticket = string.Format("{0:yyyyMMddHHmmssfff}", CustomPersianCalendar.GetCurrentIRNow(false));

            string pathFile = path + "\\" + date;
            if (!Directory.Exists(pathFile))
                Directory.CreateDirectory(pathFile);

            string fullPath = string.Format("{0}\\{1}_{2}", pathFile, ticket, FinalUploadFileObject.LockNumber.ToString());
            if (!Directory.Exists(fullPath))
                Directory.CreateDirectory(fullPath);

            string pathtemp = path + "\\TempDirectory\\" + FinalUploadFileObject.LockNumber.ToString();

            if (Directory.Exists(pathtemp))
            {
                foreach (var file in new DirectoryInfo(pathtemp).GetFiles())
                {
                    file.MoveTo($@"{fullPath}\{file.Name}");
                }
            }

            File.WriteAllText(string.Format("{0}\\{1}.txt", fullPath, FinalUploadFileObject.LockNumber), FinalUploadFileObject.Desc, System.Text.Encoding.UTF8);

            return Ok("Ok");

        }
*/

    }
}
