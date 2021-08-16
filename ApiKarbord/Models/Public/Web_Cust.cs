namespace ApiKarbord.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public class Web_Cust
    {
        [Key]
        public string Code { get; set; }

        public string Name { get; set; }

        public long SortName { get; set; }

        public string Spec { get; set; }

        public string Eghdam { get; set; }

        public DateTime? EghdamDate { get; set; }

        public string UpdateUser { get; set; }

        public DateTime? UpdateDate { get; set; }

        public int CustMode { get; set; }


        public string MelliCode { get; set; }

        public string EcoCode { get; set; }

        public string Ostan { get; set; }

        public string Shahrestan { get; set; }

        public string Region { get; set; }

        public string City { get; set; }

        public string Street { get; set; }

        public string Alley { get; set; }

        public string Plack { get; set; }

        public string ZipCode { get; set; }

        public string Tel { get; set; }

        public string Mobile { get; set; }

        public string Fax { get; set; }

        public string Email { get; set; }

        public string CGruCode { get; set; }

        public string CGruName { get; set; }

        public double? EtebarNaghd { get; set; }

        public double? EtebarCheck { get; set; }

        /*public string SAddMin1 { get; set; }
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



        public int? KalaPriceCode_P { get; set; }
        public int? KalaPriceCode_S { get; set; }
        public int? CGruKalaPriceCode_P { get; set; }
        public int? CGruKalaPriceCode_S { get; set; }

        public string CustF01 { get; set; }

        public string CustF02 { get; set; }

        public string CustF03 { get; set; }

        public string CustF04 { get; set; }

        public string CustF05 { get; set; }

        public string CustF06 { get; set; }

        public string CustF07 { get; set; }

        public string CustF08 { get; set; }

        public string CustF09 { get; set; }

        public string CustF10 { get; set; }

        public string CustF11 { get; set; }

        public string CustF12 { get; set; }

        public string CustF13 { get; set; }

        public string CustF14 { get; set; }

        public string CustF15 { get; set; }

        public string CustF16 { get; set; }

        public string CustF17 { get; set; }

        public string CustF18 { get; set; }

        public string CustF19 { get; set; }

        public string CustF20 { get; set; }

    }
}