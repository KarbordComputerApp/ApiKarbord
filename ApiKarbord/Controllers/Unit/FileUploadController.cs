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

namespace ApiKarbordAccount.Controllers
{
    public class FileUploadController : ApiController
    {

        /* [Route("api/FileUpload/UploadFile/{LockNumber}")]
         public async Task<IHttpActionResult> UploadFile(string LockNumber)
         {
             var folder = "C://App//Upload//" + LockNumber + "//";
             //var folder = "C://Test//App//Upload//" + LockNumber + "//";
             if (!Directory.Exists(folder))
             {
                 Directory.CreateDirectory(folder);
             }
             var httpRequest = HttpContext.Current.Request.Files[0];
             var name = httpRequest.FileName.Split('.');
             string tempName = name[0] + "-" + DateTime.Now.ToString("yyMMddHHmmss") + "." + name[1];
             var filePath = folder + tempName;
             httpRequest.SaveAs(filePath);
             tempName = name[0] + "-" + DateTime.Now.ToString("yyMMddHHmmss") + "--" + name[1];
             return Ok(tempName);
         }
         */



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

        [Route("api/FileUpload/UploadFile/{ace}/{sal}/{group}")]
        public async Task<IHttpActionResult> UploadFile(string ace, string sal, string group)
        {
            var Atch = System.Web.HttpContext.Current.Request.Files["Atch"];

            //BinaryReader file = new BinaryReader(httpRequest.InputStream);

            string SerialNumber = HttpContext.Current.Request["SerialNumber"];
            string ProgName = HttpContext.Current.Request["ProgName"];
            string ModeCode = HttpContext.Current.Request["ModeCode"];
            string BandNo = HttpContext.Current.Request["BandNo"];
            string Code = HttpContext.Current.Request["Code"];
            string Comm = HttpContext.Current.Request["Comm"];
            string FName = HttpContext.Current.Request["FName"];

            var req = HttpContext.Current.Request;
            var guid = req.Form["guid"];
            var file = req.Files[req.Files.Keys.Get(0)];




            string fname;
            fname = Atch.FileName;
            fname = Path.Combine("c:\\a\\", fname);
            Atch.SaveAs(fname);




            int lenght = file.ContentLength;
            byte[] data = new byte[lenght];
            file.InputStream.Read(data, 0, lenght);

           


            string s = Convert.ToBase64String(data);
            byte[] htmlCode = Encoding.ASCII.GetBytes(s);


            /* string queryStmt = "INSERT INTO dbo.YourTable(Content) VALUES(@Content)";

            using (SqlConnection _con = new SqlConnection(--your - connection - string - here--))
            using (SqlCommand _cmd = new SqlCommand(queryStmt, _con))
            {
                SqlParameter param = _cmd.Parameters.Add("@Content", SqlDbType.VarBinary);
                param.Value = YourByteArrayVariableHere;

                _con.Open();
                _cmd.ExecuteNonQuery();
                _con.Close();
            }*/


            // var fileSavePath = Path.Combine(HostingEnvironment.MapPath("~/Uploaded/"), Atch.FileName);



            //string base64String = Encoding.UTF8.GetString(fileData, 0, fileData.Length);
            /* using (var ms = new MemoryStream())
             {
                 file.CopyTo(ms);
                 byte[] fileBytes = ms.ToArray();
                 string s = Convert.ToBase64String(fileBytes);
                 // act on the Base64 data
             }


            StreamReader stream = new StreamReader(HttpContext.Current.Request.Files[0].InputStream);
            string xmls = stream.ReadToEnd();

           
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            string body1 = "default for one";

            using (var reader = new StreamReader(HttpContext.Current.Request.InputStream))
            {
                body1 = reader.ReadToEnd();
            }



            var bodyStream = new StreamReader(HttpContext.Current.Request.InputStream);
            bodyStream.BaseStream.Seek(0, SeekOrigin.Begin);
            var bodyText = bodyStream.ReadToEnd();
            */








            int value = 0;
            var dataAccount = UnitDatabase.ReadUserPassHeader(this.Request.Headers);
            string con = UnitDatabase.CreateConection(dataAccount[0], dataAccount[1], dataAccount[2], ace, sal, group, 0, "", 0, 0);
            if (con == "ok")
            {
                try
                {
                    string sql = "";
                    sql = string.Format(CultureInfo.InvariantCulture,
                         @" DECLARE	@return_value int

                                      EXEC	@return_value = [dbo].[Web_DocAttach_Save]
                                              @ProgName = '{0}',
                                              @ModeCode = '{1}',
                                              @SerialNumber = {2},
                                              @BandNo = {3},
                                              @Code = '{4}',
                                              @Comm = '{5}',
                                              @FName = '{6}',
                                              @Atch = '{7}'

                                      SELECT	'Return Value' = @return_value",
                       ProgName, ModeCode, SerialNumber, BandNo, Code, Comm, FName, s
                        );
                    value = UnitDatabase.db.Database.SqlQuery<int>(sql).Single();

                    await UnitDatabase.db.SaveChangesAsync();

                    if (value > 0)
                    {
                        await UnitDatabase.db.SaveChangesAsync();
                    }
                }
                catch (Exception e)
                {
                    throw;
                }
                return Ok(value);
            }
            return Ok(con);

        }



        /*        public class FileDownload
                {
                    public string LockNumber { get; set; }

                    public string FileName { get; set; }

                }
                */


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

