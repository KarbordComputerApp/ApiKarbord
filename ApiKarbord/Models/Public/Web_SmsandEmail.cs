namespace ApiKarbord.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Web_SmsandEmail
    {
        public int? Code { get; set; }

        public string Mode { get; set; }

        [Key]
        public string Key { get; set; }

        public string Value { get; set; }

        public string UserCode { get; set; }

    }
}