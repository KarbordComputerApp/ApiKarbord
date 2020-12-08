namespace ApiKarbord.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Web_FDocP
    {
        [StringLength(250)]
        public string CoName { get; set; }

        [StringLength(20)]
        public string CoRegNo { get; set; }

        [StringLength(20)]
        public string CoMelliCode { get; set; }

        [StringLength(20)]
        public string CoEcoCode { get; set; }

        [StringLength(30)]
        public string CoTel { get; set; }

        [StringLength(30)]
        public string CoFax { get; set; }

        [StringLength(30)]
        public string CoMobile { get; set; }

        [StringLength(100)]
        public string CoEMail { get; set; }

        [StringLength(50)]
        public string CoCountry { get; set; }

        [StringLength(50)]
        public string CoOstan { get; set; }

        [StringLength(20)]
        public string CoCity { get; set; }

        [StringLength(250)]
        public string CoStreet { get; set; }

        [StringLength(30)]
        public string CoAlley { get; set; }

        [StringLength(10)]
        public string CoPlack { get; set; }

        [StringLength(10)]
        public string CoZipCode { get; set; }

        [StringLength(50)]
        public string CoActivity { get; set; }

        [StringLength(250)]
        public string CoAddress { get; set; }

        [StringLength(20)]
        public string CustTel { get; set; }

        [StringLength(20)]
        public string CustFax { get; set; }

        [StringLength(20)]
        public string CustMobile { get; set; }

        [StringLength(50)]
        public string CustEmail { get; set; }

        [StringLength(20)]
        public string CustMelliCode { get; set; }

        [StringLength(20)]
        public string CustEcoCode { get; set; }

        [StringLength(30)]
        public string CustCity { get; set; }

        [StringLength(250)]
        public string CustStreet { get; set; }

        [StringLength(30)]
        public string CustAlley { get; set; }

        [StringLength(10)]
        public string CustPlack { get; set; }

        [StringLength(10)]
        public string CustZipCode { get; set; }

        [StringLength(250)]
        public string CustAddress { get; set; }

        public double? FinalPrice { get; set; }

        [StringLength(20)]
        public string PaymentTypeSt { get; set; }

        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long SerialNumber { get; set; }

        [StringLength(100)]
        public string KalaCode { get; set; }

        [Column(TypeName = "ntext")]
        public string Comm { get; set; }

        public short? MainUnit { get; set; }

        [StringLength(250)]
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

        [StringLength(50)]
        public string CustCode { get; set; }

        [StringLength(250)]
        public string KalaName { get; set; }

        public double? KalaZarib1 { get; set; }

        public double? KalaZarib2 { get; set; }

        public double? KalaZarib3 { get; set; }

        [StringLength(50)]
        public string KalaUnitName1 { get; set; }

        [StringLength(50)]
        public string KalaUnitName2 { get; set; }

        [StringLength(50)]
        public string KalaUnitName3 { get; set; }

        [StringLength(50)]
        public string KalaFanniNo { get; set; }

        public byte? KalaDeghatR1 { get; set; }

        public byte? KalaDeghatR2 { get; set; }

        public byte? KalaDeghatR3 { get; set; }

        public byte? KalaDeghatM1 { get; set; }

        public byte? KalaDeghatM2 { get; set; }

        public byte? KalaDeghatM3 { get; set; }

        [StringLength(50)]
        public string KGruCode { get; set; }

        [StringLength(50)]
        public string MainUnitName { get; set; }

        public int? DeghatR { get; set; }

        [StringLength(10)]
        public string DocNo { get; set; }

        [StringLength(10)]
        public string DocDate { get; set; }

        [StringLength(250)]
        public string Spec { get; set; }

        [StringLength(250)]
        public string CustName { get; set; }

        [StringLength(4000)]
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

        [StringLength(100)]
        public string AddMinSpec1 { get; set; }

        [StringLength(100)]
        public string AddMinSpec2 { get; set; }

        [StringLength(100)]
        public string AddMinSpec3 { get; set; }

        [StringLength(100)]
        public string AddMinSpec4 { get; set; }

        [StringLength(100)]
        public string AddMinSpec5 { get; set; }

        [StringLength(100)]
        public string AddMinSpec6 { get; set; }

        [StringLength(100)]
        public string AddMinSpec7 { get; set; }

        [StringLength(100)]
        public string AddMinSpec8 { get; set; }

        [StringLength(100)]
        public string AddMinSpec9 { get; set; }

        [StringLength(100)]
        public string AddMinSpec10 { get; set; }

        [StringLength(50)]
        public string UnitName { get; set; }

        public double? Amount { get; set; }

        [StringLength(50)]
        public string EghdamName { get; set; }

        [StringLength(50)]
        public string TanzimName { get; set; }

        [StringLength(50)]
        public string TaeedName { get; set; }

        [StringLength(50)]
        public string TasvibName { get; set; }

    }
}
