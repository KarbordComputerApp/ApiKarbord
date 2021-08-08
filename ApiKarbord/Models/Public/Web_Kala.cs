namespace ApiKarbord.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public class Web_Kala
    {
        [Key]
        public string Code { get; set; }

        public string Name { get; set; }

        public string SortName { get; set; }

        public string Spec { get; set; }

        public string Eghdam { get; set; }

        public DateTime? EghdamDate { get; set; }

        public string UpdateUser { get; set; }

        public DateTime? UpdateDate { get; set; }

        public string UnitName1 { get; set; }

        public string UnitName2 { get; set; }

        public string UnitName3 { get; set; }

        public double? zarib1 { get; set; }

        public double? zarib2 { get; set; }

        public double? zarib3 { get; set; }

        public string FanniNo { get; set; }

        public int? DeghatM1 { get; set; }

        public int? DeghatM2 { get; set; }

        public int? DeghatM3 { get; set; }

        public int? DeghatR1 { get; set; }

        public int? DeghatR2 { get; set; }

        public int? DeghatR3 { get; set; }

        public double? PPrice1 { get; set; }

        public double? PPrice2 { get; set; }

        public double? PPrice3 { get; set; }

        public double? SPrice1 { get; set; }

        public double? SPrice2 { get; set; }

        public double? SPrice3 { get; set; }

        public string BarCode { get; set; }

        public byte? DefaultUnit { get; set; }

        /*
                public string SAddMin1 { get; set; }
                public string SAddMin2 { get; set; }
                public string SAddMin3 { get; set; }
                public string SAddMin4 { get; set; }
                public string SAddMin5 { get; set; }
                public string SAddMin6 { get; set; }
                public string SAddMin7 { get; set; }
                public string SAddMin8 { get; set; }
                public string SAddMin9 { get; set; }
                public string SAddMin10 { get; set; }

                public string PAddMin1 { get; set; }
                public string PAddMin2 { get; set; }
                public string PAddMin3 { get; set; }
                public string PAddMin4 { get; set; }
                public string PAddMin5 { get; set; }
                public string PAddMin6 { get; set; }
                public string PAddMin7 { get; set; }
                public string PAddMin8 { get; set; }
                public string PAddMin9 { get; set; }
                public string PAddMin10 { get; set; }*/

        public string KGruCode { get; set; }

        public string KGruName { get; set; }

        public string KalaF01 { get; set; }

        public string KalaF02 { get; set; }

        public string KalaF03 { get; set; }

        public string KalaF04 { get; set; }

        public string KalaF05 { get; set; }

        public string KalaF06 { get; set; }

        public string KalaF07 { get; set; }

        public string KalaF08 { get; set; }

        public string KalaF09 { get; set; }

        public string KalaF10 { get; set; }

        public string KalaF11 { get; set; }

        public string KalaF12 { get; set; }

        public string KalaF13 { get; set; }

        public string KalaF14 { get; set; }

        public string KalaF15 { get; set; }

        public string KalaF16 { get; set; }

        public string KalaF17 { get; set; }

        public string KalaF18 { get; set; }

        public string KalaF19 { get; set; }

        public string KalaF20 { get; set; }

        [Column(TypeName = "image")]
        public byte[] KalaImage { get; set; }
    }
}