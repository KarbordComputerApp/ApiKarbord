namespace ApiKarbord.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Web_DocBDocCUser 
    {
        [Key]
        public long SerialNumber { get; set; }

        public int DocBMode { get; set; }

        public int? BandNo { get; set; }

 
        public string RjDate { get; set; }


        public string RjStatus { get; set; }


        public string RjEndDate { get; set; }


        public string RjMhltDate { get; set; }

        [Column(TypeName = "ntext")]
        public string RjComm { get; set; }

        public int? ErjaCount { get; set; }

        public double? RjTime { get; set; }

        public string FromUserCode { get; set; }


        public string FromUserName { get; set; }


        public string ToUserCode { get; set; }


        public string ToUserName { get; set; }

        [Key]
        [Column(Order = 2)]

        public string RooneveshtUserCode { get; set; }

        [Key]
        [Column(Order = 3)]

        public string RooneveshtUserName { get; set; }

        public bool? RjRead { get; set; }


        public string Radif { get; set; }
    }
}
