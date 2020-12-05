namespace ApiKarbord.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public class Web_RprtCols
    {
        [StringLength(50)]
        public string RprtId { get; set; }

        [StringLength(10)]
        public string UserCode { get; set; }

        [Key]
        [StringLength(50)]
        public string Code { get; set; }


        public int Type { get; set; }

        public byte? Visible { get; set; }

        [StringLength(10)]
        public string Prog { get; set; }

        [StringLength(100)]
        public string Name { get; set; }
    }
}