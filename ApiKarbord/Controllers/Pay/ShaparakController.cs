using System;
using System.Web.Http;
using ApiKarbord.SaleService;
using ApiKarbord.ConfirmService;
using System.Threading.Tasks;


namespace ApiKarbord.Controllers.Pay
{
    public class ShaparakController : ApiController
    {

        public class SalePaymentRequestModel
        {
            public string CallBackUrl { get; set; }
            public string LoginAccount { get; set; } // NRlhOcngQl7BwNOhU104
            public long Amount { get; set; }
            public virtual string AdditionalData { get; set; }
            public long OrderId { get; set; }
            public string Originator { get; set; }
        }


        public class PaymentRequestResponseModel
        {
            public short? Status { get; set; }
            public string Message { get; set; }
            public string location { get; set; }
            public long? Token { get; set; }
        }


       
        [Route("api/Shaparak/SalePayment")]
        public async Task<IHttpActionResult> PostSalePayment(SalePaymentRequestModel model)
        {
            ClientPaymentResponseDataBase responseData = null;
            using (var service = new SaleService.SaleService())
            {
                System.Net.ServicePointManager.ServerCertificateValidationCallback = new System.Net.Security.RemoteCertificateValidationCallback((o, xc, xch, sslP) => true);
                service.Url = "https://pec.shaparak.ir/NewIPGServices/Sale/SaleService.asmx";
                var saleRequest = new SaleService.ClientSaleRequestData();
                saleRequest.LoginAccount = model.LoginAccount;//NRlhOcngQl7BwNOhU104
                saleRequest.CallBackUrl = model.CallBackUrl;// "https://www.karbordcomputer.ir/";
                saleRequest.Amount = model.Amount;
                saleRequest.AdditionalData = model.AdditionalData;
                saleRequest.Originator = model.Originator;
                saleRequest.OrderId = model.OrderId + DateTime.Now.Ticks;
                responseData = service.SalePaymentRequest(saleRequest);
            }


            var res = new PaymentRequestResponseModel()
            {
                Message = responseData?.Message,
                location = "https://pec.shaparak.ir/NewIPG/?token=" + responseData?.Token,
                Status = responseData?.Status,
                Token = responseData?.Token
            };
            return Ok(res);
        }






  
        public class PaymentConfirmRequest
        {
            public string LoginAccount { get; set; } // NRlhOcngQl7BwNOhU104

            public long Token { get; set; }

        }


        public class PaymentConfirmResponseModel
        {
            public long Token { get; set; }

            public long RRN { get; set; }

            public short status { get; set; }

            public string CardNumberMasked { get; set; }

        }


        [Route("api/Shaparak/PaymentConfirm")]
        public async Task<IHttpActionResult> PostPaymentConfirm(PaymentConfirmRequest model)
        {
            using (var confirmSvc = new ConfirmService.ConfirmService())
            {
                confirmSvc.Url = "https://pec.Shaparak.ir/NewIPGServices/Confirm/ConfirmService.asmx";
                var confirmRequestData = new ConfirmService.ClientConfirmRequestData();
                confirmRequestData.LoginAccount = model.LoginAccount;
                confirmRequestData.Token = model.Token;
                var confirmResponse = confirmSvc.ConfirmPayment(confirmRequestData);
                short status = confirmResponse.Status;

                var res = new PaymentConfirmResponseModel()
                {
                    RRN = confirmResponse.RRN,
                    CardNumberMasked = confirmResponse.CardNumberMasked,
                    status = confirmResponse.Status,
                    Token = confirmResponse.Token,
                };
                return Ok(res);
            }
        }



    }
}
