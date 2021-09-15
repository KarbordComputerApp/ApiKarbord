namespace ApiKarbord.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Web_IDocP
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

        public double? jAmount1 { get; set; }

        public double? jAmount2 { get; set; }

        public double? jAmount3 { get; set; }

        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int BandNo { get; set; }


        public string BandSpec { get; set; }


        public string Comm { get; set; }


        public string KalaCode { get; set; }

        public short? MainUnit { get; set; }


        public string MkzCode { get; set; }


        public string OprCode { get; set; }


        public string PrdCode { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long SerialNumber { get; set; }

        public double? TotalPrice { get; set; }

        public double? UnitPrice { get; set; }

        public bool? UP_Flag { get; set; }


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

        public double? Amount1 { get; set; }

        public double? Amount2 { get; set; }

        public double? Amount3 { get; set; }


        public string MainUnitName { get; set; }

        public byte? DeghatR { get; set; }


        public string DocNo { get; set; }


        public string DocDate { get; set; }


        public string Spec { get; set; }

        public int? InOut { get; set; }


        public string ThvlCode { get; set; }


        public string ThvlName { get; set; }


        public string InvCode { get; set; }


        public string InvName { get; set; }


        public string ModeCode { get; set; }


        public string ModeName { get; set; }


        public string Footer { get; set; }


        public string UnitName { get; set; }

        public double? Amount { get; set; }


        public string EghdamName { get; set; }


        public string TanzimName { get; set; }


        public string TaeedName { get; set; }


        public string TasvibName { get; set; }


        [Column(TypeName = "image")]
        public byte[] EghdamEmza { get; set; }

         [Column(TypeName = "image")]
        public byte[] TaeedEmza { get; set; }

         [Column(TypeName = "image")]
        public byte[] TanzimEmza { get; set; }

         [Column(TypeName = "image")]
        public byte[] TasvibEmza { get; set; }

    }
}
