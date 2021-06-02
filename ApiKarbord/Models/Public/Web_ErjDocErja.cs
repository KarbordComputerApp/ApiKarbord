namespace ApiKarbord.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public class Web_ErjDocErja
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long SerialNumber { get; set; }

        public int? BandNo { get; set; }
        
        public int? DocBMode { get; set; }

        [Column(TypeName = "ntext")]
        public string RjComm { get; set; }


        public string RjDate { get; set; }


        public string RjStatus { get; set; }

        public string RjTimeSt { get; set; }


        public string FromUserCode { get; set; }


        public string FromUserName { get; set; }

        public string ToUserCode { get; set; }

        public string ToUserName { get; set; }

        [Key]
        [Column(Order = 1)]

        public string RjReadSt { get; set; }
    }
}