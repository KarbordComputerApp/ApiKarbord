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

        public long SortCode { get; set; }

        public string Name { get; set; }

        public long SortName { get; set; }

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

        public string Eghdam { get; set; }

        
       public string AccF01 { get; set; }
        
        public string AccF02 { get; set; }
        
       public string AccF03 { get; set; }
        
        public string AccF04 { get; set; }
       
        public string AccF05 { get; set; }
        
        public string AccF06 { get; set; }
        
        public string AccF07 { get; set; }
        
        public string AccF08 { get; set; }
        
        public string AccF09 { get; set; }
        
        public string AccF10 { get; set; }
        
        public string AccF11 { get; set; }
       
        public string AccF12 { get; set; }
       
        public string AccF13 { get; set; }
        
        public string AccF14 { get; set; }
        
        public string AccF15 { get; set; }
        
        public string AccF16 { get; set; }
        
        public string AccF17 { get; set; }
        
        public string AccF18 { get; set; }
        
        public string AccF19 { get; set; }
        
        public string AccF20 { get; set; }

        public string AGruCode { get; set; }

        public string AGruName { get; set; }


         public short AccStatus { get; set; }

        public string AccStatusComm { get; set; }

        public string AccComm { get; set; }

        public string EMail { get; set; }

        public string Mobile { get; set; }

        public short? Mahiat { get; set; }

     
        public string PDModeName { get; set; }

        public string MahiatName { get; set; }

        /*
        public long? SortCode { get; set; }

        [Key]
        public string RepUsers { get; set; }

        [Key]
        public string ADocUsers { get; set; }

        [Key]
        public string Eghdam { get; set; }

        public DateTime? EghdamDate { get; set; }

        public string UpdateUser { get; set; }

        public DateTime? UpdateDate { get; set; }

        [Key]
        public string Code { get; set; }

        public string Name { get; set; }

        public long? SortName { get; set; }
        
        public string Spec { get; set; }
        
        public string AccF01 { get; set; }
       
        public string AccF02 { get; set; }
        
        public string AccF03 { get; set; }
        
        public string AccF04 { get; set; }
        
        public string AccF05 { get; set; }
        
        public string AccF06 { get; set; }
        
        public string AccF07 { get; set; }
        
        public string AccF08 { get; set; }
        
        public string AccF09 { get; set; }
        
        public string AccF10 { get; set; }
        
        public string AccF11 { get; set; }
        
        public string AccF12 { get; set; }
        
        public string AccF13 { get; set; }
        
        public string AccF14 { get; set; }

        public string AccF15 { get; set; }
        
        public string AccF16 { get; set; }
        
        public string AccF17 { get; set; }
        
        public string AccF18 { get; set; }
        
        public string AccF19 { get; set; }
        
        public string AccF20 { get; set; }
        public string AGruCode { get; set; }

        [Key]
        public string AGruName { get; set; }

        public string ZGru { get; set; }
        
        public string MkzName { get; set; }

        public string OprName { get; set; }

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

        [Key]
        public short AccStatus { get; set; }

        [Key]
        public string AccStatusComm { get; set; }

        [Key]
        public string AccComm { get; set; }

        [Key]
        public string EMail { get; set; }

        public string Mobile { get; set; }

        public short? Mahiat { get; set; }

        public short? PDMode { get; set; }

        public short? Level { get; set; }

        public string PDModeName { get; set; }

        public string MahiatName { get; set; }
        */
    }
}
