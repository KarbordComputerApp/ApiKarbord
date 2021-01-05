namespace ApiKarbord.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Web_Chante_FDoc_Moved
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long mSerialNumber { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long FctSerialNumber { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long HvlSerialNumber { get; set; }

        [StringLength(10)]
        public string mDocNo { get; set; }

        public double? mSortDocNo { get; set; }

        [Key]
        [Column(Order = 3)]
        [StringLength(21)]
        public string mStatus { get; set; }

        [StringLength(10)]
        public string mStatusDate { get; set; }

        [Key]
        [Column(Order = 4)]
        [StringLength(250)]
        public string mFooter { get; set; }
    }
}