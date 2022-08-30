namespace ApiKarbord.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Web_Cust_Info
    {
        [Key]
        public string Code { get; set; }

        public string Name { get; set; }

        public string Spec { get; set; }

        public string CGruCode { get; set; }

        public string Mobile { get; set; }

        public string Tel { get; set; }

        public string Email { get; set; }

        public string Fax { get; set; }

        public double? Etebar { get; set; }

        public double? Etebar2 { get; set; }

        public double? MinSefaresh { get; set; }

        public double? MaxSefaresh { get; set; }

        public double? DarJaryan { get; set; }

        public double? CheckEtebarMondeh { get; set; }

        public double? AccMondeh { get; set; }

        public string LastKharidDate { get; set; }

        public string Address { get; set; }

        [Column(TypeName = "image")]
        public byte[] CustImage { get; set; }

        public string Comm { get; set; }
    }
}
