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
        
        public string Code { get; set; }

        public string SortCode { get; set; }

        public string Name { get; set; }

        public string SortName { get; set; }

        public string Spec { get; set; }

        [Key]
        [Column(Order = 1)]
        
        public string ZGru { get; set; }

        [Key]
        [Column(Order = 2)]
        
        public string MkzName { get; set; }

        [Key]
        [Column(Order = 3)]
        
        public string OprName { get; set; }

        [Key]
        [Column(Order = 4)]
    
        public string ArzName { get; set; }

        public double ArzRate { get; set; }
        

        public byte? HasChild { get; set; }

        public byte? AutoCreate { get; set; }

        public byte? NextLevelFromZAcc { get; set; }

    
        public string MkzCode { get; set; }


    
        public string OprCode { get; set; }

    
        public string ArzCode { get; set; }

        public short? Mkz { get; set; }

        public short? Opr { get; set; }

        public short? Arzi { get; set; }

        public short? PDMode { get; set; }

        public short? Level { get; set; }


    }
}
