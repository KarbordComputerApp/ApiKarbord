namespace ApiKarbord.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Web_CGru
    {
        [Key]

        public string Code { get; set; }
        public string Name { get; set; }

        public string Spec { get; set; }

        public int Level { get; set; }

        public int? Code1 { get; set; }

        public int? Code2 { get; set; }

        public int? Code3 { get; set; }

        public int? Code4 { get; set; }

        public int? Code5 { get; set; }

        public short? Mode { get; set; }
    }
}