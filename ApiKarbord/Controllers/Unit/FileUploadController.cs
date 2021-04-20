using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Threading.Tasks;
using System.Web;
using System.IO;
using System.Net.Http.Headers;

using System.Globalization;
using ApiKarbord.Controllers.Unit;
using System.Text;
using System.Web.Hosting;
using System.Data.SqlClient;
using System.Data;

namespace ApiKarbordAccount.Controllers
{
    public class FileUploadController : ApiController
    {

        public class Web_DocAttach_Save
        {
            public string ProgName { get; set; }

            public string ModeCode { get; set; }

            public long SerialNumber { get; set; }

            public int BandNo { get; set; }

            public string Code { get; set; }

            public string Comm { get; set; }

            public string FName { get; set; }

            public HttpPostedFileBase Atch { get; set; }
        }


        private static byte[] ConvertStream(Stream stream)
        {
            using (var memoryStream = new MemoryStream())
            {
                stream.CopyTo(memoryStream);
                return memoryStream.ToArray();
            }
        }



        public static string StreamToString(Stream stream)
        {
            stream.Position = 0;
            using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
            {
                return reader.ReadToEnd();
            }
        }


        [Route("api/FileUpload/UploadFile/{ace}/{sal}/{group}")]
        public async Task<IHttpActionResult> UploadFile(string ace, string sal, string group)
        {
            string SerialNumber = HttpContext.Current.Request["SerialNumber"];
            string ProgName = HttpContext.Current.Request["ProgName"];
            string ModeCode = HttpContext.Current.Request["ModeCode"];
            string BandNo = HttpContext.Current.Request["BandNo"];
            string Code = HttpContext.Current.Request["Code"];
            string Comm = HttpContext.Current.Request["Comm"];
            string FName = HttpContext.Current.Request["FName"];
            var Atch = System.Web.HttpContext.Current.Request.Files["Atch"];

            var req = HttpContext.Current.Request;
            var file = req.Files[req.Files.Keys.Get(0)];


            int lenght = Atch.ContentLength;
            byte[] filebyte = new byte[lenght];
            Atch.InputStream.Read(filebyte, 0, lenght);


            var dataAccount = UnitDatabase.ReadUserPassHeader(this.Request.Headers);
            string con = UnitDatabase.CreateConection(dataAccount[0], dataAccount[1], dataAccount[2], ace, sal, group, 0, "", 0, 0);
            if (con == "ok")
            {
                SqlConnection connection = new SqlConnection(UnitPublic.conString);
                connection.Open();

                SqlCommand cmd = new SqlCommand("Web_DocAttach_Save", connection);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@ProgName", ProgName);
                cmd.Parameters.AddWithValue("@ModeCode", ModeCode);
                cmd.Parameters.AddWithValue("@SerialNumber", SerialNumber);
                cmd.Parameters.AddWithValue("@BandNo", BandNo);
                cmd.Parameters.AddWithValue("@Code", Code);
                cmd.Parameters.AddWithValue("@Comm", Comm);
                cmd.Parameters.AddWithValue("@FName", FName);
                cmd.Parameters.AddWithValue("@Atch", filebyte);

                cmd.ExecuteNonQuery();
                connection.Close();
                return Ok(1);
            }
            else
                return Ok(con);
        }



        [HttpGet]
        [Route("api/FileUpload/DownloadFile/{LockNumber}/{FileName}")]
        public HttpResponseMessage Download(string LockNumber, string FileName)
        {
            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);
            FileName = FileName.Replace("--", ".");
            string filePath = "C://App//Upload//" + LockNumber + "//" + FileName;

            if (!File.Exists(filePath))
            {
                response.StatusCode = HttpStatusCode.NotFound;
                response.ReasonPhrase = string.Format("File not found: {0} .", FileName);
                throw new HttpResponseException(response);
            }


            byte[] bytes = File.ReadAllBytes(filePath);

            //Set the Response Content.
            response.Content = new ByteArrayContent(bytes);

            //Set the Response Content Length.
            response.Content.Headers.ContentLength = bytes.LongLength;

            //Set the Content Disposition Header Value and FileName.
            response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment");
            response.Content.Headers.ContentDisposition.FileName = FileName;

            //Set the File Content Type.
            response.Content.Headers.ContentType = new MediaTypeHeaderValue(MimeMapping.GetMimeMapping(FileName));
            return response;
        }


        [HttpGet]
        [Route("api/FileUpload/DeleteFile/{LockNumber}/{FileName}")]
        public async Task<IHttpActionResult> DeleteFile(string LockNumber, string FileName)
        {
            FileName = FileName.Replace("--", ".");

            string fullPath = "C://App//Upload//" + LockNumber + "//" + FileName;

            if (System.IO.File.Exists(fullPath))
            {
                System.IO.File.Delete(fullPath);
            }
            return Ok("Ok");
        }


    }
}

