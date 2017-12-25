using System;
using System.Collections.Generic;
using System.Text;

namespace SpaDatasource.Entitites
{
    public class InventoryLog
    {
        public int Id;
        public int ProductId;
        public DateTime ActionDate;
        public string Action;
        public int Quantity;
        public int UserId;
    }
}
