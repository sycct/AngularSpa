using System;
using System.Collections.Generic;
using System.Text;

namespace SpaDatasource.Entitites
{
    public class Order
    {
        public long Id;
        public DateTime Time;
        public int UserId;
        public string EmailAddress;
        public decimal Currency;
        public string OrderDescription;
    }
}
