namespace ApiKarbord.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Web_ADocP
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

        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int BandNo { get; set; }

        public long? bSerialNumber { get; set; }

        [Key]
        [Column(Order = 1)]

        public string AccFullCode { get; set; }

        [Key]
        [Column(Order = 2)]

        public string TrafFullCode { get; set; }


        public string MkzCode { get; set; }


        public string OprCode { get; set; }

        [Key]
        [Column(Order = 3)]

        public string BandSpec { get; set; }

        [Key]
        [Column(Order = 4)]

        public string Comm { get; set; }

        [Key]
        [Column(Order = 5)]

        public string CheckNo { get; set; }

        [Key]
        [Column(Order = 6)]

        public string CheckDate { get; set; }

        [Key]
        [Column(Order = 7)]

        public string Bank { get; set; }

        [Key]
        [Column(Order = 8)]

        public string Shobe { get; set; }

        [Key]
        [Column(Order = 9)]

        public string Jari { get; set; }

        [Key]
        [Column(Order = 10)]

        public string BaratNo { get; set; }

        [Key]
        [Column(Order = 11)]

        public string CheckComm { get; set; }

        [Key]
        [Column(Order = 12)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public short CheckMode { get; set; }

        public int? CheckRadif { get; set; }


        public string CheckVosoolDate { get; set; }

        public double? Bede { get; set; }

        public double? Best { get; set; }

        [Key]
        [Column(Order = 13)]
        public double Amount { get; set; }

        [Key]
        [Column(Order = 14)]

        public string ArzCode { get; set; }

        [Key]
        [Column(Order = 15)]
        public double ArzValue { get; set; }

        [Key]
        [Column(Order = 16)]
        public double ArzRate { get; set; }


        public string CheckStatus { get; set; }

        public long? SerialNumber { get; set; }

        [Key]
        [Column(Order = 17)]

        public string AccFullName { get; set; }

        [Key]
        [Column(Order = 18)]

        public string AccZCode { get; set; }

        [Key]
        [Column(Order = 19)]

        public string ZGru { get; set; }

        public short? Mkz { get; set; }

        public short? Opr { get; set; }

        public short? Arzi { get; set; }

        public short? PDMode { get; set; }

        [Key]
        [Column(Order = 20)]

        public string ArzName { get; set; }

        [Key]
        [Column(Order = 21)]

        public string TrafFullName { get; set; }

        [Key]
        [Column(Order = 22)]

        public string TrafZCode { get; set; }

        [Key]
        [Column(Order = 23)]

        public string MkzName { get; set; }

        [Key]
        [Column(Order = 24)]

        public string OprName { get; set; }

        [Key]
        [Column(Order = 25)]

        public string AccZName { get; set; }

        [Key]
        [Column(Order = 26)]

        public string TrafZName { get; set; }


        public string AccCode { get; set; }


        public string TrafCode { get; set; }

        [Key]
        [Column(Order = 27)]

        public string AccName { get; set; }

        [Key]
        [Column(Order = 28)]

        public string TrafName { get; set; }

        public long? DocNo { get; set; }


        public string DocDate { get; set; }


        public string Spec { get; set; }


        public string EghdamName { get; set; }


        public string TanzimName { get; set; }


        public string TaeedName { get; set; }
    }
}
