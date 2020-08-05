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
        [Column(Order = 0)]
        [StringLength(50)]
        public string Code { get; set; }

        [StringLength(100)]
        public string Name { get; set; }

        [StringLength(250)]
        public string Spec { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(250)]
        public string ZGru { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(250)]
        public string MkzName { get; set; }

        [Key]
        [Column(Order = 3)]
        [StringLength(100)]
        public string OprName { get; set; }

        [Key]
        [Column(Order = 4)]
        [StringLength(50)]
        public string ArzName { get; set; }

        public byte? HasChild { get; set; }

        public byte? AutoCreate { get; set; }

        public byte? NextLevelFromZAcc { get; set; }

        [StringLength(50)]
        public string MkzCode { get; set; }

        [StringLength(50)]
        public string OprCode { get; set; }

        [StringLength(50)]
        public string ArzCode { get; set; }

        public short? Mkz { get; set; }

        public short? Opr { get; set; }

        public short? Arzi { get; set; }

        public short? PDMode { get; set; }

        public short? Level { get; set; }


    }
}
