using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace VerstaTest.Models.DTOEntities
{
    public partial class OrderDTO : ModelBase
    {
        public override int Id { get; set; }
        public string SenderCity { get; set; }
        public string SenderAddress { get; set; }
        public string ReceiverCity { get; set; }
        public string ReceiverAddress { get; set; }
        public decimal? PackageWeight { get; set; }
        public DateTime ReceiveDate { get; set; }
        public DateTime? CreationTime { get; set; }
    }
}
