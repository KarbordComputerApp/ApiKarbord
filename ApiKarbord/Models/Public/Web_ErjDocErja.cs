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

        [StringLength(10)]
        public string RjDate { get; set; }

        [StringLength(50)]
        public string RjStatus { get; set; }

        [StringLength(61)]
        public string RjTimeSt { get; set; }

        [StringLength(20)]
        public string FromUserCode { get; set; }

        [StringLength(50)]
        public string FromUserName { get; set; }

        [StringLength(32)]
        public string ToUserCode { get; set; }

        [StringLength(62)]
        public string ToUserName { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(1)]
        public string RjReadSt { get; set; }
    }
}