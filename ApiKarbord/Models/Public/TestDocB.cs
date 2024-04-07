namespace ApiKarbord.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public class TestDocB
    {
        public byte? Test { get; set; }

        public string TestName { get; set; }

        public string TestCap { get; set; }

        public int? BandNo { get; set; }

        public string AccCode { get; set; }

    }

    public class TestDoc
    {
        public byte? Mode { get; set; }

        public string ModeName { get; set; }

        public string Spec { get; set; }

    }


    public class TestAddmin
    {
        public byte? Mode { get; set; }

        public string ModeName { get; set; }

        public string Spec { get; set; }

    }


    public class ResTest
    {
        public string Status { get; set; }

        public long SerialNumber { get; set; }

        public int CountError { get; set; }

        public int CountWarnning { get; set; }

        public double? TotalValue { get; set; }

        public List<TestDoc> Data { get; set; }

        public List<AddMin> DataAddmin { get; set; }


    }
}