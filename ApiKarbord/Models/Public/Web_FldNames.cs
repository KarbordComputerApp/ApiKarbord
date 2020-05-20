namespace ApiKarbord.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public class Web_FldNames
    {

        public byte? Mode { get; set; }

        [StringLength(10)]
        public string Prog { get; set; }

        [Key]
        [StringLength(50)]
        public string Code { get; set; }

        [StringLength(100)]
        public string Name { get; set; }

        [StringLength(20)]
        public string RprtId { get; set; }

        [Key]
        public byte InOut { get; set; }

        public byte? Visible { get; set; }

    }
}