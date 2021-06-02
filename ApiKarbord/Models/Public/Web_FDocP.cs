namespace ApiKarbord.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Web_FDocP
    {

        public string CoName { get; set; }


        public string CoRegNo { get; set; }


        public string CoMelliCode { get; set; }


        public string CoEcoCode { get; set; }


        public string CoTel { get; set; }


        public string CoFax { get; set; }


        public string CoMobile { get; set; }


        public string CoEMail { get; set; }


        public string CoCountry { get; set; }


        public string CoOstan { get; set; }


        public string CoCity { get; set; }


        public string CoStreet { get; set; }


        public string CoAlley { get; set; }


        public string CoPlack { get; set; }


        public string CoZipCode { get; set; }


        public string CoActivity { get; set; }


        public string CoAddress { get; set; }


        public string CustTel { get; set; }


        public string CustFax { get; set; }


        public string CustMobile { get; set; }


        public string CustEmail { get; set; }


        public string CustMelliCode { get; set; }


        public string CustEcoCode { get; set; }


        public string CustCity { get; set; }


        public string CustStreet { get; set; }


        public string CustAlley { get; set; }


        public string CustPlack { get; set; }


        public string CustZipCode { get; set; }


        public string CustAddress { get; set; }

        public double? FinalPrice { get; set; }


        public string PaymentTypeSt { get; set; }

        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long SerialNumber { get; set; }


        public string KalaCode { get; set; }

        [Column(TypeName = "ntext")]
        public string Comm { get; set; }

        public short? MainUnit { get; set; }


        public string BandSpec { get; set; }

        public bool? UP_Flag { get; set; }

        public double? Amount1 { get; set; }

        public double? Amount2 { get; set; }

        public double? Amount3 { get; set; }

        public double? UnitPrice { get; set; }

        public double? TotalPrice { get; set; }

        public double? Discount { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int BandNo { get; set; }


        public string CustCode { get; set; }


        public string KalaName { get; set; }

        public double? KalaZarib1 { get; set; }

        public double? KalaZarib2 { get; set; }

        public double? KalaZarib3 { get; set; }


        public string KalaUnitName1 { get; set; }


        public string KalaUnitName2 { get; set; }


        public string KalaUnitName3 { get; set; }


        public string KalaFanniNo { get; set; }

        public byte? KalaDeghatR1 { get; set; }

        public byte? KalaDeghatR2 { get; set; }

        public byte? KalaDeghatR3 { get; set; }

        public byte? KalaDeghatM1 { get; set; }

        public byte? KalaDeghatM2 { get; set; }

        public byte? KalaDeghatM3 { get; set; }


        public string KGruCode { get; set; }


        public string MainUnitName { get; set; }

        public byte? DeghatR { get; set; }


        public string DocNo { get; set; }


        public string DocDate { get; set; }


        public string Spec { get; set; }


        public string CustName { get; set; }


        public string Footer { get; set; }

        public double? AddMinPrice1 { get; set; }

        public double? AddMinPrice2 { get; set; }

        public double? AddMinPrice3 { get; set; }

        public double? AddMinPrice4 { get; set; }

        public double? AddMinPrice5 { get; set; }

        public double? AddMinPrice6 { get; set; }

        public double? AddMinPrice7 { get; set; }

        public double? AddMinPrice8 { get; set; }

        public double? AddMinPrice9 { get; set; }

        public double? AddMinPrice10 { get; set; }


        public string AddMinSpec1 { get; set; }


        public string AddMinSpec2 { get; set; }


        public string AddMinSpec3 { get; set; }


        public string AddMinSpec4 { get; set; }


        public string AddMinSpec5 { get; set; }


        public string AddMinSpec6 { get; set; }


        public string AddMinSpec7 { get; set; }


        public string AddMinSpec8 { get; set; }


        public string AddMinSpec9 { get; set; }


        public string AddMinSpec10 { get; set; }


        public string UnitName { get; set; }

        public double? Amount { get; set; }


        public string EghdamName { get; set; }


        public string TanzimName { get; set; }


        public string TaeedName { get; set; }


        public string TasvibName { get; set; }

    }
}
