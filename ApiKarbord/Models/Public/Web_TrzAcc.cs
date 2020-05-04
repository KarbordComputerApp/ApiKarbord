namespace ApiKarbord.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public class Web_TrzAcc
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Level { get; set; }

        [StringLength(50)]
        public string Acc_Code { get; set; }

        [StringLength(100)]
        public string Acc_Name { get; set; }

        [StringLength(9)]
        public string Acc_Code1 { get; set; }

        [StringLength(9)]
        public string Acc_Code2 { get; set; }

        [StringLength(9)]
        public string Acc_Code3 { get; set; }

        [StringLength(9)]
        public string Acc_Code4 { get; set; }

        [StringLength(9)]
        public string Acc_Code5 { get; set; }

        public double? Bede { get; set; }

        public double? Best { get; set; }

        public double? MonBede { get; set; }

        public double? MonBest { get; set; }

        [Key]
        [Column(Order = 1)]
        public double MonTotal { get; set; }
    }
}