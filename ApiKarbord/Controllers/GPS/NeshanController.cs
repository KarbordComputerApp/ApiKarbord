using System;
using System.Web.Http;
using ApiKarbord.SaleService;
using ApiKarbord.ConfirmService;
using System.Threading.Tasks;
using System.Net.Http;
using ApiKarbord.Models;
using ApiKarbord.Controllers.Unit;
using System.Web;
using System.Net;

namespace ApiKarbord.Controllers.GPS
{
    public class NeshanController : ApiController
    {
        static string IniPathGPS = HttpContext.Current.Server.MapPath("~/Content/ini/GpsConfig.Ini");

        static IniFile MyIniGPS = new IniFile(IniPathGPS);

        public static string apiKey = MyIniGPS.Read("ApiKey", "Neshan");
        public static string site = MyIniGPS.Read("Site", "Neshan");


        public class reverseObject
        {
            public float lat { get; set; }
            public float lng { get; set; }
        }

        // Post: api/Neshan/reverse  تبدیل مختصات به آدرس

        [Route("api/Neshan/reverse/")]
        public async Task<IHttpActionResult> Postreverse(reverseObject c)
        {
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            string uri = site + string.Format(@"/v5/reverse?lat={0}&lng={1}", c.lat, c.lng);

            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Get, uri);
            request.Headers.Add("Api-Key", apiKey);
            //var content = new StringContent(uri, null, "application/json");
            //request.Content = content;
            var response = await client.SendAsync(request);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var json = await response.Content.ReadAsStringAsync();
                Reverse r = Newtonsoft.Json.JsonConvert.DeserializeObject<Reverse>(json);
                return Ok(r);
            }
            return Ok(response.StatusCode);
        }


        public class GeocodingObject
        {
            public string Addr { get; set; }
        }

        // Post: api/Neshan/Geocoding   تبدیل آدرس به مختصات

        [Route("api/Neshan/Geocoding/")]
        public async Task<IHttpActionResult> PostGeocoding(GeocodingObject c)
        {
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            string uri = site + string.Format(@"/v4/geocoding?address={0}", c.Addr);

            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Get, uri);
            request.Headers.Add("Api-Key", "service.e59b517b0eca4b3c91a0ca3ec75dfb63");


            var response = await client.SendAsync(request);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var json = await response.Content.ReadAsStringAsync();
                Geocoding r = Newtonsoft.Json.JsonConvert.DeserializeObject<Geocoding>(json);
                return Ok(r);
            }
            return Ok(response.StatusCode);

        }



        public class MapMatchingObject
        {
            public string Path { get; set; }
        }


        // Post: api/Neshan/MapMatching 
        [Route("api/Neshan/MapMatching/")]
        public async Task<IHttpActionResult> PostMapMatching(MapMatchingObject c)
        {
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            string uri = site + string.Format(@"/v3/map-matching");
            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Post, uri);
            request.Headers.Add("Api-Key", apiKey);

            string dataPost = string.Format(@"{{""path"": ""{0}""}}", c.Path);

            var content = new StringContent(dataPost, null, "application/json");
            request.Content = content;

            var response = await client.SendAsync(request);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var json = await response.Content.ReadAsStringAsync();
                MapMatching r = Newtonsoft.Json.JsonConvert.DeserializeObject<MapMatching>(json);
                return Ok(r);
            }
            return Ok(response.StatusCode);
        }


        public class DistancetObject
        {
            public long SerialNumber { get; set; }
            public int BandNo { get; set; }
        }


        // Post: api/Neshan/Distancet   لیست پیوست  
        [Route("api/Neshan/Distance/")]
        public async Task<IHttpActionResult> PostDistance(DistancetObject c)
        {
            string uri = site +
                string.Format(@"/v1/distance-matrix?type=car&origins={0}{1}|{2},{3}&destinations={4},{5}|{6},{7}");

            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Get, uri);
            request.Headers.Add("Api-Key", apiKey);
            //var content = new StringContent(uri, null, "application/json");
            //request.Content = content;
            var response = await client.SendAsync(request);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var json = await response.Content.ReadAsStringAsync();
                Reverse r = Newtonsoft.Json.JsonConvert.DeserializeObject<Reverse>(json);
                return Ok(r);
            }
            return Ok(response.StatusCode);
        }




    }
}
