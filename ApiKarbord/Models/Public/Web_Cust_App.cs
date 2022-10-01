namespace ApiKarbord.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Web_Cust_App
    {
        [Key]
        public string Code { get; set; }

        public string Name { get; set; }

        public string CGruCode { get; set; }

        public string Address { get; set; }

        [Column(TypeName = "image")]
        public byte[] CustImage { get; set; }
    }
}
