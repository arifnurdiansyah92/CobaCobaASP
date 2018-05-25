using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CobaCoba.Models
{
    public class SalesOrderHeader
    {
        public int SalesOrderHeaderId { get; set; }
        public string Customer { get; set; }

        public List<SalesOrderLine> SalesOrderLines;
    }
    public class SalesOrderLine
    {
        public int SalesOrderLineId { get; set; }
        public string Product { get; set; }
        public float Qty { get; set; }
        public decimal Price { get; set; }

        public SalesOrderHeader SalesOrderHeader { get; set; }
        public int SalesOrderHeaderId { get; set; }
    }
}
