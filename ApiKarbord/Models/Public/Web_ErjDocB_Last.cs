namespace ApiKarbord.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Web_ErjDocB_Last")]
    public class Web_ErjDocB_Last
    {

        public string CustName { get; set; }


        public string KhdtName { get; set; }


        public string Spec { get; set; }


        public string Status { get; set; }

        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long SerialNumber { get; set; }


        public string MhltDate { get; set; }

        [Column(TypeName = "ntext")]
        public string EghdamComm { get; set; }

        [Column(TypeName = "ntext")]
        public string SpecialComm { get; set; }

        [Column(TypeName = "ntext")]
        public string FinalComm { get; set; }

        [Column(TypeName = "ntext")]
        public string DocDesc { get; set; }

        public int? DocBStep { get; set; }

        public string RjRadif { get; set; }

        public int? BandNo { get; set; }

        public int? DocBMode { get; set; }

        [Column(TypeName = "ntext")]
        public string RjComm { get; set; }


        public string RjDate { get; set; }


        public string RjStatus { get; set; }


        public string RjEndDate { get; set; }


        public string RjMhltDate { get; set; }

        public DateTime? RjUpdateDate { get; set; }


        public string RjUpdateUser { get; set; }

        public int? ErjaCount { get; set; }

        public double? RjTime { get; set; }

        public string FromUserCode { get; set; }

        public string FromUserName { get; set; }

        public string ToUserCode { get; set; }

        public string ToUserName { get; set; }

        [Key]
        [Column(Order = 1)]

        public string RjReadSt { get; set; }
    }
}