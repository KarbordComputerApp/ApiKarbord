namespace ApiKarbord.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Web_Acc
    {
        [Key]
        [StringLength(50)]
        public string Code { get; set; }

        [StringLength(100)]
        public string Name { get; set; }

        [StringLength(250)]
        public string Spec { get; set; }

        public byte? HasChild { get; set; }

        public byte? AutoCreate { get; set; }

        public byte? NextLevelFromZAcc { get; set; }

        public Int16? Mkz { get; set; }

        public Int16? Opr { get; set; }

        public Int16? Arzi { get; set; }

        public Int16? PDMode { get; set; }

        public Int16? Level { get; set; }


    }
}
