namespace ApiKarbord.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Web_DocAttach
    {
        [Key]
        public int IId { get; set; }

        [StringLength(20)]
        public string ProgName { get; set; }

        [StringLength(20)]
        public string ModeCode { get; set; }

        public long? SerialNumber { get; set; }

        public int? BandNo { get; set; }

        [StringLength(100)]
        public string Code { get; set; }

        [StringLength(250)]
        public string Comm { get; set; }

        [StringLength(100)]
        public string FName { get; set; }

        [Column(TypeName = "image")]
        public byte[] Atch { get; set; }
    }
}