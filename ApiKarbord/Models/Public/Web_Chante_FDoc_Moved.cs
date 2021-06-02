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


        public string mDocNo { get; set; }

        public double? mSortDocNo { get; set; }

        [Key]
        [Column(Order = 3)]

        public string mStatus { get; set; }


        public string mStatusDate { get; set; }

        [Key]
        [Column(Order = 4)]

        public string mFooter { get; set; }
    }
}